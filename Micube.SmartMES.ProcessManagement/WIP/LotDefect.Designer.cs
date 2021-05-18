namespace Micube.SmartMES.ProcessManagement
{
    partial class LotDefect
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
            this.grdWIP = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabDefect = new Micube.Framework.SmartControls.SmartTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.grdTargetAll = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtDefectCode = new Micube.Framework.SmartControls.SmartLabelSelectPopupEdit();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdTargetLot = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.defectList = new Micube.SmartMES.ProcessManagement.usDefectGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.cboUOM2 = new Micube.Framework.SmartControls.SmartComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ucDataUpDownBtnCtrl = new Micube.SmartMES.Commons.Controls.ucDataUpDownBtn();
            this.spcSpliter = new Micube.Framework.SmartControls.SmartSpliterControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabDefect)).BeginInit();
            this.tabDefect.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefectCode.Properties)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboUOM2.Properties)).BeginInit();
            this.panel2.SuspendLayout();
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
            this.pnlContent.Controls.Add(this.grdWIP);
            this.pnlContent.Controls.Add(this.spcSpliter);
            this.pnlContent.Controls.Add(this.panel1);
            // 
            // grdWIP
            // 
            this.grdWIP.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdWIP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdWIP.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdWIP.IsUsePaging = false;
            this.grdWIP.LanguageKey = "WIPLIST";
            this.grdWIP.Location = new System.Drawing.Point(0, 0);
            this.grdWIP.Margin = new System.Windows.Forms.Padding(0);
            this.grdWIP.Name = "grdWIP";
            this.grdWIP.ShowBorder = true;
            this.grdWIP.Size = new System.Drawing.Size(756, 156);
            this.grdWIP.TabIndex = 1;
            this.grdWIP.UseAutoBestFitColumns = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabDefect);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 161);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(756, 328);
            this.panel1.TabIndex = 4;
            // 
            // tabDefect
            // 
            this.tabDefect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDefect.Location = new System.Drawing.Point(0, 45);
            this.tabDefect.Name = "tabDefect";
            this.tabDefect.SelectedTabPage = this.xtraTabPage1;
            this.tabDefect.Size = new System.Drawing.Size(756, 283);
            this.tabDefect.TabIndex = 3;
            this.tabDefect.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.grdTargetAll);
            this.xtraTabPage1.Controls.Add(this.panel3);
            this.tabDefect.SetLanguageKey(this.xtraTabPage1, "DEFECTALL");
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.xtraTabPage1.Size = new System.Drawing.Size(750, 254);
            this.xtraTabPage1.Text = "완불처리";
            // 
            // grdTargetAll
            // 
            this.grdTargetAll.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdTargetAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTargetAll.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdTargetAll.IsUsePaging = false;
            this.grdTargetAll.LanguageKey = "TARGETDEFECT";
            this.grdTargetAll.Location = new System.Drawing.Point(3, 36);
            this.grdTargetAll.Margin = new System.Windows.Forms.Padding(0);
            this.grdTargetAll.Name = "grdTargetAll";
            this.grdTargetAll.ShowBorder = true;
            this.grdTargetAll.ShowStatusBar = false;
            this.grdTargetAll.Size = new System.Drawing.Size(744, 215);
            this.grdTargetAll.TabIndex = 2;
            this.grdTargetAll.UseAutoBestFitColumns = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtDefectCode);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(744, 33);
            this.panel3.TabIndex = 8;
            // 
            // txtDefectCode
            // 
            this.txtDefectCode.EditorWidth = "80%";
            this.txtDefectCode.LabelText = " 불량 코드";
            this.txtDefectCode.LabelWidth = "25%";
            this.txtDefectCode.LanguageKey = "DEFECTCODE";
            this.txtDefectCode.Location = new System.Drawing.Point(8, 6);
            this.txtDefectCode.Margin = new System.Windows.Forms.Padding(0);
            this.txtDefectCode.Name = "txtDefectCode";
            this.txtDefectCode.Size = new System.Drawing.Size(381, 20);
            this.txtDefectCode.TabIndex = 1;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.tabDefect.SetLanguageKey(this.xtraTabPage2, "DEFECTBYLOT");
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.xtraTabPage2.Size = new System.Drawing.Size(750, 254);
            this.xtraTabPage2.Text = "LOT별";
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSpliterContainer1, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel1, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(744, 248);
            this.smartSplitTableLayoutPanel1.TabIndex = 9;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 33);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdTargetLot);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.defectList);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(744, 215);
            this.smartSpliterContainer1.SplitterPosition = 373;
            this.smartSpliterContainer1.TabIndex = 0;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdTargetLot
            // 
            this.grdTargetLot.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdTargetLot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTargetLot.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdTargetLot.IsUsePaging = false;
            this.grdTargetLot.LanguageKey = "TARGETDEFECT";
            this.grdTargetLot.Location = new System.Drawing.Point(0, 0);
            this.grdTargetLot.Margin = new System.Windows.Forms.Padding(0);
            this.grdTargetLot.Name = "grdTargetLot";
            this.grdTargetLot.ShowBorder = true;
            this.grdTargetLot.ShowStatusBar = false;
            this.grdTargetLot.Size = new System.Drawing.Size(373, 215);
            this.grdTargetLot.TabIndex = 4;
            this.grdTargetLot.UseAutoBestFitColumns = false;
            // 
            // defectList
            // 
            this.defectList.DataSource = null;
            this.defectList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defectList.Location = new System.Drawing.Point(0, 0);
            this.defectList.LotID = null;
            this.defectList.Name = "defectList";
            this.defectList.Size = new System.Drawing.Size(366, 215);
            this.defectList.TabIndex = 9;
            this.defectList.VisibleTopDefectCode = false;
            // 
            // smartPanel1
            // 
            this.smartPanel1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.smartPanel1.Appearance.Options.UseBackColor = true;
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.smartLabel1);
            this.smartPanel1.Controls.Add(this.cboUOM2);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(744, 33);
            this.smartPanel1.TabIndex = 1;
            // 
            // smartLabel1
            // 
            this.smartLabel1.LanguageKey = "UOM";
            this.smartLabel1.Location = new System.Drawing.Point(4, 11);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(26, 14);
            this.smartLabel1.TabIndex = 2;
            this.smartLabel1.Text = "UOM";
            // 
            // cboUOM2
            // 
            this.cboUOM2.LabelText = null;
            this.cboUOM2.LanguageKey = null;
            this.cboUOM2.Location = new System.Drawing.Point(64, 8);
            this.cboUOM2.Name = "cboUOM2";
            this.cboUOM2.PopupWidth = 0;
            this.cboUOM2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUOM2.Properties.NullText = "";
            this.cboUOM2.ShowHeader = true;
            this.cboUOM2.Size = new System.Drawing.Size(309, 20);
            this.cboUOM2.TabIndex = 1;
            this.cboUOM2.VisibleColumns = null;
            this.cboUOM2.VisibleColumnsWidth = null;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ucDataUpDownBtnCtrl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(756, 45);
            this.panel2.TabIndex = 0;
            // 
            // ucDataUpDownBtnCtrl
            // 
            this.ucDataUpDownBtnCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDataUpDownBtnCtrl.Location = new System.Drawing.Point(0, 0);
            this.ucDataUpDownBtnCtrl.Name = "ucDataUpDownBtnCtrl";
            this.ucDataUpDownBtnCtrl.Size = new System.Drawing.Size(756, 45);
            this.ucDataUpDownBtnCtrl.SourceGrid = null;
            this.ucDataUpDownBtnCtrl.TabIndex = 0;
            this.ucDataUpDownBtnCtrl.TargetGrid = null;
            // 
            // spcSpliter
            // 
            this.spcSpliter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.spcSpliter.Location = new System.Drawing.Point(0, 156);
            this.spcSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcSpliter.Name = "spcSpliter";
            this.spcSpliter.Size = new System.Drawing.Size(756, 5);
            this.spcSpliter.TabIndex = 5;
            this.spcSpliter.TabStop = false;
            // 
            // LotDefect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 538);
            this.LanguageKey = "LOTDEFECT";
            this.Name = "LotDefect";
            this.Text = "Lot 불량처리";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabDefect)).EndInit();
            this.tabDefect.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDefectCode.Properties)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboUOM2.Properties)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdWIP;
        private System.Windows.Forms.Panel panel1;
        private Framework.SmartControls.SmartSpliterControl spcSpliter;
        private System.Windows.Forms.Panel panel2;
        private Framework.SmartControls.SmartBandedGrid grdTargetAll;
        private Micube.SmartMES.Commons.Controls.ucDataUpDownBtn ucDataUpDownBtnCtrl;
        private Framework.SmartControls.SmartTabControl tabDefect;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdTargetLot;
        private usDefectGrid defectList;
        private System.Windows.Forms.Panel panel3;
        private Framework.SmartControls.SmartLabelSelectPopupEdit txtDefectCode;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartComboBox cboUOM2;
    }
}