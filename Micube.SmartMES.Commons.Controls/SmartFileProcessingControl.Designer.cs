namespace Micube.SmartMES.Commons.Controls
{
    partial class SmartFileProcessingControl
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
            this.pnlFileUploadButton = new Micube.Framework.SmartControls.SmartPanel();
            this.btnFileDownload = new Micube.Framework.SmartControls.SmartButton();
            this.btnFileDelete = new Micube.Framework.SmartControls.SmartButton();
            this.btnFileAdd = new Micube.Framework.SmartControls.SmartButton();
            this.grdFileList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartLayoutControl1 = new Micube.Framework.SmartControls.SmartLayoutControl();
            this.smartLayoutControlGroup1 = new Micube.Framework.SmartControls.SmartLayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.smartLayoutControl2 = new Micube.Framework.SmartControls.SmartLayoutControl();
            this.smartLayoutControlGroup2 = new Micube.Framework.SmartControls.SmartLayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFileUploadButton)).BeginInit();
            this.pnlFileUploadButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl1)).BeginInit();
            this.smartLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl2)).BeginInit();
            this.smartLayoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFileUploadButton
            // 
            this.pnlFileUploadButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFileUploadButton.Controls.Add(this.smartLayoutControl1);
            this.pnlFileUploadButton.Location = new System.Drawing.Point(0, 0);
            this.pnlFileUploadButton.Margin = new System.Windows.Forms.Padding(0);
            this.pnlFileUploadButton.Name = "pnlFileUploadButton";
            this.pnlFileUploadButton.Size = new System.Drawing.Size(400, 26);
            this.pnlFileUploadButton.TabIndex = 0;
            // 
            // btnFileDownload
            // 
            this.btnFileDownload.AllowFocus = false;
            this.btnFileDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFileDownload.IsBusy = false;
            this.btnFileDownload.IsWrite = false;
            this.btnFileDownload.LanguageKey = "FILEDOWNLOAD";
            this.btnFileDownload.Location = new System.Drawing.Point(302, 2);
            this.btnFileDownload.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnFileDownload.Name = "btnFileDownload";
            this.btnFileDownload.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnFileDownload.Size = new System.Drawing.Size(96, 22);
            this.btnFileDownload.StyleController = this.smartLayoutControl1;
            this.btnFileDownload.TabIndex = 2;
            this.btnFileDownload.Text = "다운로드";
            this.btnFileDownload.TooltipLanguageKey = "";
            // 
            // btnFileDelete
            // 
            this.btnFileDelete.AllowFocus = false;
            this.btnFileDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFileDelete.IsBusy = false;
            this.btnFileDelete.IsWrite = false;
            this.btnFileDelete.LanguageKey = "FILEDELETE";
            this.btnFileDelete.Location = new System.Drawing.Point(202, 2);
            this.btnFileDelete.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnFileDelete.Name = "btnFileDelete";
            this.btnFileDelete.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnFileDelete.Size = new System.Drawing.Size(96, 22);
            this.btnFileDelete.StyleController = this.smartLayoutControl1;
            this.btnFileDelete.TabIndex = 1;
            this.btnFileDelete.Text = "파일삭제";
            this.btnFileDelete.TooltipLanguageKey = "";
            // 
            // btnFileAdd
            // 
            this.btnFileAdd.AllowFocus = false;
            this.btnFileAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFileAdd.IsBusy = false;
            this.btnFileAdd.IsWrite = false;
            this.btnFileAdd.LanguageKey = "FILEADD";
            this.btnFileAdd.Location = new System.Drawing.Point(102, 2);
            this.btnFileAdd.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnFileAdd.Name = "btnFileAdd";
            this.btnFileAdd.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnFileAdd.Size = new System.Drawing.Size(96, 22);
            this.btnFileAdd.StyleController = this.smartLayoutControl1;
            this.btnFileAdd.TabIndex = 0;
            this.btnFileAdd.Text = "파일추가";
            this.btnFileAdd.TooltipLanguageKey = "";
            // 
            // grdFileList
            // 
            this.grdFileList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdFileList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdFileList.IsUsePaging = false;
            this.grdFileList.LanguageKey = "FILELIST";
            this.grdFileList.Location = new System.Drawing.Point(0, 28);
            this.grdFileList.Margin = new System.Windows.Forms.Padding(0);
            this.grdFileList.Name = "grdFileList";
            this.grdFileList.ShowBorder = true;
            this.grdFileList.Size = new System.Drawing.Size(400, 272);
            this.grdFileList.TabIndex = 1;
            this.grdFileList.UseAutoBestFitColumns = false;
            // 
            // smartLayoutControl1
            // 
            this.smartLayoutControl1.Controls.Add(this.btnFileDownload);
            this.smartLayoutControl1.Controls.Add(this.btnFileDelete);
            this.smartLayoutControl1.Controls.Add(this.btnFileAdd);
            this.smartLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.smartLayoutControl1.Margin = new System.Windows.Forms.Padding(0);
            this.smartLayoutControl1.Name = "smartLayoutControl1";
            this.smartLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(447, 0, 650, 400);
            this.smartLayoutControl1.Root = this.smartLayoutControlGroup1;
            this.smartLayoutControl1.Size = new System.Drawing.Size(400, 26);
            this.smartLayoutControl1.TabIndex = 3;
            this.smartLayoutControl1.Text = "smartLayoutControl1";
            // 
            // smartLayoutControlGroup1
            // 
            this.smartLayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.smartLayoutControlGroup1.GroupBordersVisible = false;
            this.smartLayoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1});
            this.smartLayoutControlGroup1.Name = "Root";
            this.smartLayoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.smartLayoutControlGroup1.Size = new System.Drawing.Size(400, 26);
            this.smartLayoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnFileAdd;
            this.layoutControlItem1.Location = new System.Drawing.Point(100, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnFileDelete;
            this.layoutControlItem2.Location = new System.Drawing.Point(200, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnFileDownload;
            this.layoutControlItem3.Location = new System.Drawing.Point(300, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(100, 27);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // smartLayoutControl2
            // 
            this.smartLayoutControl2.Controls.Add(this.pnlFileUploadButton);
            this.smartLayoutControl2.Controls.Add(this.grdFileList);
            this.smartLayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLayoutControl2.Location = new System.Drawing.Point(0, 0);
            this.smartLayoutControl2.Margin = new System.Windows.Forms.Padding(0);
            this.smartLayoutControl2.Name = "smartLayoutControl2";
            this.smartLayoutControl2.Root = this.smartLayoutControlGroup2;
            this.smartLayoutControl2.Size = new System.Drawing.Size(400, 300);
            this.smartLayoutControl2.TabIndex = 2;
            this.smartLayoutControl2.Text = "smartLayoutControl2";
            // 
            // smartLayoutControlGroup2
            // 
            this.smartLayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.smartLayoutControlGroup2.GroupBordersVisible = false;
            this.smartLayoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.smartLayoutControlGroup2.Name = "smartLayoutControlGroup2";
            this.smartLayoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.smartLayoutControlGroup2.Size = new System.Drawing.Size(400, 300);
            this.smartLayoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.grdFileList;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 2, 0);
            this.layoutControlItem4.Size = new System.Drawing.Size(400, 274);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.pnlFileUploadButton;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem5.Size = new System.Drawing.Size(400, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // SmartFileProcessingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.smartLayoutControl2);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SmartFileProcessingControl";
            this.Size = new System.Drawing.Size(400, 300);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFileUploadButton)).EndInit();
            this.pnlFileUploadButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl1)).EndInit();
            this.smartLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl2)).EndInit();
            this.smartLayoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Framework.SmartControls.SmartPanel pnlFileUploadButton;
        public Framework.SmartControls.SmartButton btnFileDelete;
        public Framework.SmartControls.SmartButton btnFileAdd;
        public Framework.SmartControls.SmartBandedGrid grdFileList;
        public Framework.SmartControls.SmartButton btnFileDownload;
        private Framework.SmartControls.SmartLayoutControl smartLayoutControl1;
        private Framework.SmartControls.SmartLayoutControlGroup smartLayoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private Framework.SmartControls.SmartLayoutControl smartLayoutControl2;
        private Framework.SmartControls.SmartLayoutControlGroup smartLayoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}
