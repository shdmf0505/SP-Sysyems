namespace Micube.SmartMES.QualityAnalysis
{
    partial class ucSelfImageControl
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
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.picMeasurePrinted = new Micube.Framework.SmartControls.SmartPictureEdit();
            this.smartSplitTableLayoutPanel3 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.chkPicture = new Micube.Framework.SmartControls.SmartCheckBox();
            this.lblValue = new Micube.Framework.SmartControls.SmartLabel();
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
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.smartSplitTableLayoutPanel2.ColumnCount = 1;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.picMeasurePrinted, 0, 1);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.smartSplitTableLayoutPanel3, 0, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 2;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(204, 156);
            this.smartSplitTableLayoutPanel2.TabIndex = 1;
            // 
            // picMeasurePrinted
            // 
            this.picMeasurePrinted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMeasurePrinted.Location = new System.Drawing.Point(4, 25);
            this.picMeasurePrinted.Name = "picMeasurePrinted";
            this.picMeasurePrinted.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picMeasurePrinted.Size = new System.Drawing.Size(196, 127);
            this.picMeasurePrinted.TabIndex = 2;
            // 
            // smartSplitTableLayoutPanel3
            // 
            this.smartSplitTableLayoutPanel3.ColumnCount = 4;
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.71429F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.76191F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.76191F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.76191F));
            this.smartSplitTableLayoutPanel3.Controls.Add(this.chkPicture, 0, 0);
            this.smartSplitTableLayoutPanel3.Controls.Add(this.lblValue, 1, 0);
            this.smartSplitTableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel3.Location = new System.Drawing.Point(1, 1);
            this.smartSplitTableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel3.Name = "smartSplitTableLayoutPanel3";
            this.smartSplitTableLayoutPanel3.RowCount = 1;
            this.smartSplitTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel3.Size = new System.Drawing.Size(202, 20);
            this.smartSplitTableLayoutPanel3.TabIndex = 3;
            // 
            // chkPicture
            // 
            this.chkPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkPicture.Location = new System.Drawing.Point(3, 3);
            this.chkPicture.Name = "chkPicture";
            this.chkPicture.Properties.Caption = "";
            this.chkPicture.Size = new System.Drawing.Size(15, 14);
            this.chkPicture.TabIndex = 3;
            // 
            // lblValue
            // 
            this.smartSplitTableLayoutPanel3.SetColumnSpan(this.lblValue, 3);
            this.lblValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValue.Location = new System.Drawing.Point(24, 3);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(175, 14);
            this.lblValue.TabIndex = 4;
            // 
            // ucSelfImageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.Name = "ucSelfImageControl";
            this.Size = new System.Drawing.Size(204, 156);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMeasurePrinted.Properties)).EndInit();
            this.smartSplitTableLayoutPanel3.ResumeLayout(false);
            this.smartSplitTableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkPicture.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel3;
        public Framework.SmartControls.SmartCheckBox chkPicture;
        public Framework.SmartControls.SmartPictureEdit picMeasurePrinted;
        private Framework.SmartControls.SmartLabel lblValue;
    }
}
