namespace Micube.SmartMES.QualityAnalysis
{
    partial class ucShipmentInspDefect
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdChildLot = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.txtChildLotId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.btnAddImageMeasure = new Micube.Framework.SmartControls.SmartButton();
            this.btnTempSave = new Micube.Framework.SmartControls.SmartButton();
            this.smartSplitTableLayoutPanel5 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdInspectionList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.picBox = new Micube.Framework.SmartControls.SmartPictureEdit();
            this.picBox2 = new Micube.Framework.SmartControls.SmartPictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtChildLotId.Properties)).BeginInit();
            this.smartSplitTableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdChildLot);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.tableLayoutPanel5);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(995, 380);
            this.smartSpliterContainer1.SplitterPosition = 324;
            this.smartSpliterContainer1.TabIndex = 9;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdChildLot
            // 
            this.grdChildLot.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdChildLot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChildLot.IsUsePaging = false;
            this.grdChildLot.LanguageKey = null;
            this.grdChildLot.Location = new System.Drawing.Point(0, 0);
            this.grdChildLot.Margin = new System.Windows.Forms.Padding(0);
            this.grdChildLot.Name = "grdChildLot";
            this.grdChildLot.ShowBorder = true;
            this.grdChildLot.ShowButtonBar = false;
            this.grdChildLot.ShowStatusBar = false;
            this.grdChildLot.Size = new System.Drawing.Size(324, 380);
            this.grdChildLot.TabIndex = 4;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 5;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.Controls.Add(this.txtChildLotId, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.btnAddImageMeasure, 4, 2);
            this.tableLayoutPanel5.Controls.Add(this.btnTempSave, 4, 0);
            this.tableLayoutPanel5.Controls.Add(this.smartSplitTableLayoutPanel5, 0, 4);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 5;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(666, 380);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // txtChildLotId
            // 
            this.txtChildLotId.AutoHeight = false;
            this.tableLayoutPanel5.SetColumnSpan(this.txtChildLotId, 2);
            this.txtChildLotId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChildLotId.LabelWidth = "14.5%";
            this.txtChildLotId.LanguageKey = "CHILDLOTID";
            this.txtChildLotId.Location = new System.Drawing.Point(0, 29);
            this.txtChildLotId.Margin = new System.Windows.Forms.Padding(0);
            this.txtChildLotId.Name = "txtChildLotId";
            this.txtChildLotId.Properties.AutoHeight = false;
            this.txtChildLotId.Properties.ReadOnly = true;
            this.txtChildLotId.Properties.UseReadOnlyAppearance = false;
            this.txtChildLotId.Size = new System.Drawing.Size(266, 24);
            this.txtChildLotId.TabIndex = 7;
            // 
            // btnAddImageMeasure
            // 
            this.btnAddImageMeasure.AllowFocus = false;
            this.btnAddImageMeasure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddImageMeasure.IsBusy = false;
            this.btnAddImageMeasure.LanguageKey = "ADDPICTURE";
            this.btnAddImageMeasure.Location = new System.Drawing.Point(580, 29);
            this.btnAddImageMeasure.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddImageMeasure.Name = "btnAddImageMeasure";
            this.btnAddImageMeasure.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnAddImageMeasure.Size = new System.Drawing.Size(86, 24);
            this.btnAddImageMeasure.TabIndex = 6;
            this.btnAddImageMeasure.Text = "smartButton3";
            this.btnAddImageMeasure.TooltipLanguageKey = "";
            // 
            // btnTempSave
            // 
            this.btnTempSave.AllowFocus = false;
            this.btnTempSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTempSave.IsBusy = false;
            this.btnTempSave.LanguageKey = "TEMPSAVE";
            this.btnTempSave.Location = new System.Drawing.Point(591, 0);
            this.btnTempSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnTempSave.Name = "btnTempSave";
            this.btnTempSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnTempSave.Size = new System.Drawing.Size(75, 24);
            this.btnTempSave.TabIndex = 1;
            this.btnTempSave.Text = "smartButton3";
            this.btnTempSave.TooltipLanguageKey = "";
            // 
            // smartSplitTableLayoutPanel5
            // 
            this.smartSplitTableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.SetColumnSpan(this.smartSplitTableLayoutPanel5, 5);
            this.smartSplitTableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel5.Controls.Add(this.grdInspectionList, 0, 0);
            this.smartSplitTableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.smartSplitTableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel5.Location = new System.Drawing.Point(0, 58);
            this.smartSplitTableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel5.Name = "smartSplitTableLayoutPanel5";
            this.smartSplitTableLayoutPanel5.RowCount = 1;
            this.smartSplitTableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel5.Size = new System.Drawing.Size(666, 322);
            this.smartSplitTableLayoutPanel5.TabIndex = 5;
            // 
            // grdInspectionList
            // 
            this.grdInspectionList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspectionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspectionList.IsUsePaging = false;
            this.grdInspectionList.LanguageKey = "DEFECTINFO";
            this.grdInspectionList.Location = new System.Drawing.Point(0, 0);
            this.grdInspectionList.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspectionList.Name = "grdInspectionList";
            this.grdInspectionList.ShowBorder = true;
            this.grdInspectionList.ShowStatusBar = false;
            this.grdInspectionList.Size = new System.Drawing.Size(333, 322);
            this.grdInspectionList.TabIndex = 0;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Controls.Add(this.picBox, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.picBox2, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(333, 0);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(333, 322);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // picBox
            // 
            this.picBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBox.Location = new System.Drawing.Point(3, 3);
            this.picBox.Name = "picBox";
            this.picBox.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picBox.Size = new System.Drawing.Size(160, 316);
            this.picBox.TabIndex = 0;
            // 
            // picBox2
            // 
            this.picBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBox2.Location = new System.Drawing.Point(169, 3);
            this.picBox2.Name = "picBox2";
            this.picBox2.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picBox2.Size = new System.Drawing.Size(161, 316);
            this.picBox2.TabIndex = 1;
            // 
            // ucShipmentInspDefect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.smartSpliterContainer1);
            this.Name = "ucShipmentInspDefect";
            this.Size = new System.Drawing.Size(995, 380);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtChildLotId.Properties)).EndInit();
            this.smartSplitTableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Framework.SmartControls.SmartSelectPopupEdit popProcessSegMiddle;
        private Framework.SmartControls.SmartSelectPopupEdit popDefectCode;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdChildLot;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private Framework.SmartControls.SmartButton btnAddImageMeasure;
        private Framework.SmartControls.SmartButton btnTempSave;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel5;
        private Framework.SmartControls.SmartBandedGrid grdInspectionList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private Framework.SmartControls.SmartPictureEdit picBox;
        private Framework.SmartControls.SmartPictureEdit picBox2;
        private Framework.SmartControls.SmartLabelTextBox txtChildLotId;
    }
}
