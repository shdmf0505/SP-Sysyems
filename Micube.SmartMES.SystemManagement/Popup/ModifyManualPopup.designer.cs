namespace Micube.SmartMES.SystemManagement
{
	partial class ModifyManualPopup
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
            this.tplModifiyImage = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.flpButtonBar = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnApply = new Micube.Framework.SmartControls.SmartButton();
            this.grdFileList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnDownload = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tplModifiyImage.SuspendLayout();
            this.flpButtonBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grdFileList);
            this.pnlMain.Controls.Add(this.tplModifiyImage);
            this.pnlMain.Size = new System.Drawing.Size(564, 341);
            // 
            // tplModifiyImage
            // 
            this.tplModifiyImage.ColumnCount = 1;
            this.tplModifiyImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplModifiyImage.Controls.Add(this.flpButtonBar, 0, 1);
            this.tplModifiyImage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tplModifiyImage.Location = new System.Drawing.Point(0, 307);
            this.tplModifiyImage.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tplModifiyImage.Name = "tplModifiyImage";
            this.tplModifiyImage.RowCount = 2;
            this.tplModifiyImage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tplModifiyImage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplModifiyImage.Size = new System.Drawing.Size(564, 34);
            this.tplModifiyImage.TabIndex = 0;
            // 
            // flpButtonBar
            // 
            this.flpButtonBar.Controls.Add(this.btnClose);
            this.flpButtonBar.Controls.Add(this.btnApply);
            this.flpButtonBar.Controls.Add(this.btnDownload);
            this.flpButtonBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpButtonBar.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpButtonBar.Location = new System.Drawing.Point(0, 10);
            this.flpButtonBar.Margin = new System.Windows.Forms.Padding(0);
            this.flpButtonBar.Name = "flpButtonBar";
            this.flpButtonBar.Size = new System.Drawing.Size(564, 24);
            this.flpButtonBar.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(486, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnApply
            // 
            this.btnApply.AllowFocus = false;
            this.btnApply.IsBusy = false;
            this.btnApply.IsWrite = false;
            this.btnApply.LanguageKey = "APPLY";
            this.btnApply.Location = new System.Drawing.Point(405, 0);
            this.btnApply.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "적용";
            this.btnApply.TooltipLanguageKey = "";
            // 
            // grdFileList
            // 
            this.grdFileList.Caption = "그리드제목( LanguageKey를 입력하세요)";
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
            this.grdFileList.Size = new System.Drawing.Size(564, 307);
            this.grdFileList.TabIndex = 1;
            this.grdFileList.UseAutoBestFitColumns = false;
            // 
            // btnDownload
            // 
            this.btnDownload.AllowFocus = false;
            this.btnDownload.IsBusy = false;
            this.btnDownload.IsWrite = false;
            this.btnDownload.LanguageKey = "FILEDOWNLOAD";
            this.btnDownload.Location = new System.Drawing.Point(324, 0);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 1;
            this.btnDownload.Text = "다운로드";
            this.btnDownload.TooltipLanguageKey = "";
            // 
            // ModifyManualPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Name = "ModifyManualPopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tplModifiyImage.ResumeLayout(false);
            this.flpButtonBar.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private Framework.SmartControls.SmartSplitTableLayoutPanel tplModifiyImage;
		private Framework.SmartControls.SmartBandedGrid grdFileList;
        private System.Windows.Forms.FlowLayoutPanel flpButtonBar;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnApply;
        private Framework.SmartControls.SmartButton btnDownload;
    }
}
