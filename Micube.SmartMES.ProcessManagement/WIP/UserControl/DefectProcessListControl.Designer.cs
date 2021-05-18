namespace Micube.SmartMES.ProcessManagement
{
	partial class DefectProcessListControl
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
            this.pnlPhoto = new Micube.Framework.SmartControls.SmartPanel();
            this.grdFileList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl2 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.picDefectPhoto = new Micube.Framework.SmartControls.SmartPictureEdit();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.grdInspectionList = new Micube.SmartMES.ProcessManagement.usDefectGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPhoto)).BeginInit();
            this.pnlPhoto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDefectPhoto.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlPhoto
            // 
            this.pnlPhoto.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlPhoto.Controls.Add(this.grdFileList);
            this.pnlPhoto.Controls.Add(this.smartSpliterControl2);
            this.pnlPhoto.Controls.Add(this.picDefectPhoto);
            this.pnlPhoto.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlPhoto.Location = new System.Drawing.Point(466, 0);
            this.pnlPhoto.Name = "pnlPhoto";
            this.pnlPhoto.Size = new System.Drawing.Size(350, 380);
            this.pnlPhoto.TabIndex = 2;
            // 
            // grdFileList
            // 
            this.grdFileList.AutoScroll = true;
            this.grdFileList.Caption = "";
            this.grdFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFileList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdFileList.IsUsePaging = false;
            this.grdFileList.LanguageKey = null;
            this.grdFileList.Location = new System.Drawing.Point(0, 0);
            this.grdFileList.Margin = new System.Windows.Forms.Padding(0);
            this.grdFileList.Name = "grdFileList";
            this.grdFileList.ShowBorder = true;
            this.grdFileList.Size = new System.Drawing.Size(350, 140);
            this.grdFileList.TabIndex = 0;
            this.grdFileList.UseAutoBestFitColumns = false;
            // 
            // smartSpliterControl2
            // 
            this.smartSpliterControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.smartSpliterControl2.Location = new System.Drawing.Point(0, 140);
            this.smartSpliterControl2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl2.Name = "smartSpliterControl2";
            this.smartSpliterControl2.Size = new System.Drawing.Size(350, 5);
            this.smartSpliterControl2.TabIndex = 2;
            this.smartSpliterControl2.TabStop = false;
            // 
            // picDefectPhoto
            // 
            this.picDefectPhoto.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.picDefectPhoto.Location = new System.Drawing.Point(0, 145);
            this.picDefectPhoto.Name = "picDefectPhoto";
            this.picDefectPhoto.Properties.AllowScrollOnMouseWheel = DevExpress.Utils.DefaultBoolean.True;
            this.picDefectPhoto.Properties.AllowScrollViaMouseDrag = true;
            this.picDefectPhoto.Properties.AllowZoomOnMouseWheel = DevExpress.Utils.DefaultBoolean.True;
            this.picDefectPhoto.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picDefectPhoto.Properties.ShowScrollBars = true;
            this.picDefectPhoto.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.True;
            this.picDefectPhoto.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.picDefectPhoto.Properties.ZoomingOperationMode = DevExpress.XtraEditors.Repository.ZoomingOperationMode.MouseWheel;
            this.picDefectPhoto.Size = new System.Drawing.Size(350, 235);
            this.picDefectPhoto.TabIndex = 1;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.smartSpliterControl1.Location = new System.Drawing.Point(461, 0);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(5, 380);
            this.smartSpliterControl1.TabIndex = 3;
            this.smartSpliterControl1.TabStop = false;
            // 
            // grdInspectionList
            // 
            this.grdInspectionList.DataSource = null;
            this.grdInspectionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspectionList.Location = new System.Drawing.Point(0, 0);
            this.grdInspectionList.LotID = null;
            this.grdInspectionList.Name = "grdInspectionList";
            this.grdInspectionList.Size = new System.Drawing.Size(461, 380);
            this.grdInspectionList.TabIndex = 4;
            this.grdInspectionList.VisibleTopDefectCode = false;
            // 
            // DefectProcessListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdInspectionList);
            this.Controls.Add(this.smartSpliterControl1);
            this.Controls.Add(this.pnlPhoto);
            this.Name = "DefectProcessListControl";
            this.Size = new System.Drawing.Size(816, 380);
            ((System.ComponentModel.ISupportInitialize)(this.pnlPhoto)).EndInit();
            this.pnlPhoto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDefectPhoto.Properties)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		private Framework.SmartControls.SmartSelectPopupEdit popProcessSegMiddle;
		private Framework.SmartControls.SmartSelectPopupEdit popDefectCode;
		private Framework.SmartControls.SmartPanel pnlPhoto;
		private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
		private Framework.SmartControls.SmartBandedGrid grdFileList;
		private Framework.SmartControls.SmartPictureEdit picDefectPhoto;
		private Framework.SmartControls.SmartSpliterControl smartSpliterControl2;
        private usDefectGrid grdInspectionList;
    }
}
