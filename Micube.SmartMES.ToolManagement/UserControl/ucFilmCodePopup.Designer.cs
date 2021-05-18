namespace Micube.SmartMES.ToolManagement
{
    partial class ucFilmCodePopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucFilmCodePopup));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtCode = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtVersion = new Micube.Framework.SmartControls.SmartTextBox();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVersion.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.txtCode);
            this.flowLayoutPanel1.Controls.Add(this.txtVersion);
            this.flowLayoutPanel1.Controls.Add(this.btnSearch);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 1);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(298, 33);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // txtCode
            // 
            this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCode.LabelText = null;
            this.txtCode.LanguageKey = null;
            this.txtCode.Location = new System.Drawing.Point(1, 1);
            this.txtCode.Margin = new System.Windows.Forms.Padding(1);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(144, 20);
            this.txtCode.TabIndex = 0;
            // 
            // txtVersion
            // 
            this.txtVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVersion.LabelText = null;
            this.txtVersion.LanguageKey = null;
            this.txtVersion.Location = new System.Drawing.Point(147, 1);
            this.txtVersion.Margin = new System.Windows.Forms.Padding(1);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(50, 20);
            this.txtVersion.TabIndex = 4;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.ImageOptions.Image")));
            this.btnSearch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btnSearch.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnSearch.IsBusy = false;
            this.btnSearch.Location = new System.Drawing.Point(201, 2);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.btnSearch.Size = new System.Drawing.Size(24, 18);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // ucFilmCodePopup
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "ucFilmCodePopup";
            this.Size = new System.Drawing.Size(230, 24);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVersion.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public Framework.SmartControls.SmartTextBox txtCode;
        public Framework.SmartControls.SmartTextBox txtVersion;
        public Framework.SmartControls.SmartButton btnSearch;
    }
}
