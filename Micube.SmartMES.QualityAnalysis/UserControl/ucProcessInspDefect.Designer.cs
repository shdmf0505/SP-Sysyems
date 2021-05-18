namespace Micube.SmartMES.QualityAnalysis
{
	partial class ucProcessInspDefect
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
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddImageMeasure = new Micube.Framework.SmartControls.SmartButton();
            this.picBox = new Micube.Framework.SmartControls.SmartPictureEdit();
            this.picBox2 = new Micube.Framework.SmartControls.SmartPictureEdit();
            this.grdInspectionList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(816, 380);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.Controls.Add(this.grdInspectionList, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnAddImageMeasure, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.picBox, 2, 2);
            this.tableLayoutPanel5.Controls.Add(this.picBox2, 3, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(816, 380);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // btnAddImageMeasure
            // 
            this.btnAddImageMeasure.AllowFocus = false;
            this.btnAddImageMeasure.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddImageMeasure.IsBusy = false;
            this.btnAddImageMeasure.LanguageKey = "ADDPICTURE";
            this.btnAddImageMeasure.Location = new System.Drawing.Point(736, 0);
            this.btnAddImageMeasure.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddImageMeasure.Name = "btnAddImageMeasure";
            this.btnAddImageMeasure.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnAddImageMeasure.Size = new System.Drawing.Size(80, 23);
            this.btnAddImageMeasure.TabIndex = 1;
            this.btnAddImageMeasure.Text = "smartButton1";
            this.btnAddImageMeasure.TooltipLanguageKey = "";
            // 
            // picBox
            // 
            this.picBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBox.Location = new System.Drawing.Point(493, 28);
            this.picBox.Margin = new System.Windows.Forms.Padding(0);
            this.picBox.Name = "picBox";
            this.picBox.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picBox.Size = new System.Drawing.Size(161, 352);
            this.picBox.TabIndex = 2;
            // 
            // picBox2
            // 
            this.picBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBox2.Location = new System.Drawing.Point(654, 28);
            this.picBox2.Margin = new System.Windows.Forms.Padding(0);
            this.picBox2.Name = "picBox2";
            this.picBox2.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picBox2.Size = new System.Drawing.Size(162, 352);
            this.picBox2.TabIndex = 3;
            // 
            // grdInspectionList
            // 
            this.grdInspectionList.Caption = "";
            this.grdInspectionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspectionList.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.None;
            this.grdInspectionList.IsUsePaging = false;
            this.grdInspectionList.LanguageKey = null;
            this.grdInspectionList.Location = new System.Drawing.Point(0, 0);
            this.grdInspectionList.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspectionList.Name = "grdInspectionList";
            this.tableLayoutPanel5.SetRowSpan(this.grdInspectionList, 3);
            this.grdInspectionList.ShowBorder = true;
            this.grdInspectionList.Size = new System.Drawing.Size(483, 380);
            this.grdInspectionList.TabIndex = 4;
            // 
            // ucProcessInspDefect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.Name = "ucProcessInspDefect";
            this.Size = new System.Drawing.Size(816, 380);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox2.Properties)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
		private Framework.SmartControls.SmartSelectPopupEdit popProcessSegMiddle;
		private Framework.SmartControls.SmartSelectPopupEdit popDefectCode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private Framework.SmartControls.SmartButton btnAddImageMeasure;
        private Framework.SmartControls.SmartPictureEdit picBox;
        private Framework.SmartControls.SmartPictureEdit picBox2;
        private Framework.SmartControls.SmartBandedGrid grdInspectionList;
    }
}
