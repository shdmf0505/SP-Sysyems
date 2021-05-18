namespace Micube.SmartMES.QualityAnalysis
{
    partial class MultiVerificationResultControl
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
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.txtTitle = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.picMeasurePrinted = new Micube.Framework.SmartControls.SmartPictureEdit();
            this.smartSplitTableLayoutPanel3 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.btnAdd = new Micube.Framework.SmartControls.SmartButton();
            this.btnDelete = new Micube.Framework.SmartControls.SmartButton();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.chkPicture = new Micube.Framework.SmartControls.SmartCheckBox();
            this.grdMeasureValue = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMeasurePrinted.Properties)).BeginInit();
            this.smartSplitTableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkPicture.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 2;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(173, 147);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // txtTitle
            // 
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTitle.LabelText = null;
            this.txtTitle.LanguageKey = null;
            this.txtTitle.Location = new System.Drawing.Point(4, 25);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(240, 20);
            this.txtTitle.TabIndex = 0;
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.smartSplitTableLayoutPanel2.ColumnCount = 1;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.txtTitle, 0, 1);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.picMeasurePrinted, 0, 2);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.smartSplitTableLayoutPanel3, 0, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdMeasureValue, 0, 3);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 4;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.38047F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.61953F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(248, 251);
            this.smartSplitTableLayoutPanel2.TabIndex = 1;
            // 
            // picMeasurePrinted
            // 
            this.picMeasurePrinted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMeasurePrinted.Location = new System.Drawing.Point(4, 51);
            this.picMeasurePrinted.Name = "picMeasurePrinted";
            this.picMeasurePrinted.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picMeasurePrinted.Size = new System.Drawing.Size(240, 87);
            this.picMeasurePrinted.TabIndex = 2;
            // 
            // smartSplitTableLayoutPanel3
            // 
            this.smartSplitTableLayoutPanel3.ColumnCount = 4;
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.71429F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.76191F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.76191F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.76191F));
            this.smartSplitTableLayoutPanel3.Controls.Add(this.btnAdd, 1, 0);
            this.smartSplitTableLayoutPanel3.Controls.Add(this.btnDelete, 2, 0);
            this.smartSplitTableLayoutPanel3.Controls.Add(this.btnClose, 3, 0);
            this.smartSplitTableLayoutPanel3.Controls.Add(this.chkPicture, 0, 0);
            this.smartSplitTableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel3.Location = new System.Drawing.Point(1, 1);
            this.smartSplitTableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel3.Name = "smartSplitTableLayoutPanel3";
            this.smartSplitTableLayoutPanel3.RowCount = 1;
            this.smartSplitTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel3.Size = new System.Drawing.Size(246, 20);
            this.smartSplitTableLayoutPanel3.TabIndex = 3;
            // 
            // btnAdd
            // 
            this.btnAdd.AllowFocus = false;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.IsBusy = false;
            this.btnAdd.IsWrite = false;
            this.btnAdd.LanguageKey = "ADD";
            this.btnAdd.Location = new System.Drawing.Point(26, 0);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnAdd.Size = new System.Drawing.Size(73, 20);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "추가";
            this.btnAdd.TooltipLanguageKey = "";
            // 
            // btnDelete
            // 
            this.btnDelete.AllowFocus = false;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.IsBusy = false;
            this.btnDelete.IsWrite = false;
            this.btnDelete.LanguageKey = "DELETE";
            this.btnDelete.Location = new System.Drawing.Point(99, 0);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDelete.Size = new System.Drawing.Size(73, 20);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "삭제";
            this.btnDelete.TooltipLanguageKey = "";
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(172, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(74, 20);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // chkPicture
            // 
            this.chkPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkPicture.Location = new System.Drawing.Point(3, 3);
            this.chkPicture.Name = "chkPicture";
            this.chkPicture.Properties.Caption = "";
            this.chkPicture.Size = new System.Drawing.Size(20, 14);
            this.chkPicture.TabIndex = 3;
            // 
            // grdMeasureValue
            // 
            this.grdMeasureValue.Caption = "";
            this.grdMeasureValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMeasureValue.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMeasureValue.IsUsePaging = false;
            this.grdMeasureValue.LanguageKey = null;
            this.grdMeasureValue.Location = new System.Drawing.Point(1, 142);
            this.grdMeasureValue.Margin = new System.Windows.Forms.Padding(0);
            this.grdMeasureValue.Name = "grdMeasureValue";
            this.grdMeasureValue.ShowBorder = true;
            this.grdMeasureValue.Size = new System.Drawing.Size(246, 108);
            this.grdMeasureValue.TabIndex = 4;
            this.grdMeasureValue.UseAutoBestFitColumns = false;
            // 
            // MultiVerificationResultControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.Name = "MultiVerificationResultControl";
            this.Size = new System.Drawing.Size(248, 251);
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMeasurePrinted.Properties)).EndInit();
            this.smartSplitTableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkPicture.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel3;
        public Framework.SmartControls.SmartTextBox txtTitle;
        public Framework.SmartControls.SmartCheckBox chkPicture;
        public Framework.SmartControls.SmartPictureEdit picMeasurePrinted;
        public Framework.SmartControls.SmartButton btnAdd;
        public Framework.SmartControls.SmartButton btnDelete;
        public Framework.SmartControls.SmartButton btnClose;
        public Framework.SmartControls.SmartBandedGrid grdMeasureValue;
    }
}
