namespace Micube.SmartMES.QualityAnalysis
{
    partial class IndependentInspectionInputOutput
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
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdMaster = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdDetail = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grdFile = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.picBox = new Micube.Framework.SmartControls.SmartPictureEdit();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 638);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1097, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlContent.Size = new System.Drawing.Size(1097, 642);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1402, 671);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdMaster, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSpliterContainer1, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSpliterControl1, 0, 1);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 3;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(1097, 642);
            this.smartSplitTableLayoutPanel1.TabIndex = 1;
            // 
            // grdMaster
            // 
            this.grdMaster.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaster.IsUsePaging = false;
            this.grdMaster.LanguageKey = "QCMINSPECTIONRESULTLIST";
            this.grdMaster.Location = new System.Drawing.Point(0, 0);
            this.grdMaster.Margin = new System.Windows.Forms.Padding(0);
            this.grdMaster.Name = "grdMaster";
            this.grdMaster.ShowBorder = true;
            this.grdMaster.Size = new System.Drawing.Size(1097, 284);
            this.grdMaster.TabIndex = 2;
            this.grdMaster.UseAutoBestFitColumns = false;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 294);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdDetail);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(1097, 348);
            this.smartSpliterContainer1.SplitterPosition = 1000;
            this.smartSpliterContainer1.TabIndex = 3;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdDetail
            // 
            this.grdDetail.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDetail.IsUsePaging = false;
            this.grdDetail.LanguageKey = "QCMINSPECTIONRESULTDETAIL";
            this.grdDetail.Location = new System.Drawing.Point(0, 0);
            this.grdDetail.Margin = new System.Windows.Forms.Padding(0);
            this.grdDetail.Name = "grdDetail";
            this.grdDetail.ShowBorder = true;
            this.grdDetail.Size = new System.Drawing.Size(1000, 348);
            this.grdDetail.TabIndex = 4;
            this.grdDetail.UseAutoBestFitColumns = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grdFile, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.picBox, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(92, 348);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // grdFile
            // 
            this.grdFile.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFile.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdFile.IsUsePaging = false;
            this.grdFile.LanguageKey = null;
            this.grdFile.Location = new System.Drawing.Point(0, 0);
            this.grdFile.Margin = new System.Windows.Forms.Padding(0);
            this.grdFile.Name = "grdFile";
            this.grdFile.ShowBorder = true;
            this.grdFile.ShowButtonBar = false;
            this.grdFile.Size = new System.Drawing.Size(92, 104);
            this.grdFile.TabIndex = 0;
            this.grdFile.UseAutoBestFitColumns = false;
            // 
            // picBox
            // 
            this.picBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBox.Location = new System.Drawing.Point(0, 104);
            this.picBox.Margin = new System.Windows.Forms.Padding(0);
            this.picBox.Name = "picBox";
            this.picBox.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picBox.Size = new System.Drawing.Size(92, 244);
            this.picBox.TabIndex = 1;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl1.Location = new System.Drawing.Point(0, 284);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(1097, 5);
            this.smartSpliterControl1.TabIndex = 4;
            this.smartSpliterControl1.TabStop = false;
            // 
            // IndependentInspectionInputOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1422, 691);
            this.Name = "IndependentInspectionInputOutput";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        public Framework.SmartControls.SmartBandedGrid grdMaster;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        public Framework.SmartControls.SmartBandedGrid grdDetail;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartBandedGrid grdFile;
        private Framework.SmartControls.SmartPictureEdit picBox;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
    }
}