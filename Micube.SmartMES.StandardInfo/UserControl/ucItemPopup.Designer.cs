namespace Micube.SmartMES.StandardInfo
{
    partial class ucItemPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucItemPopup));
            this.CODE = new Micube.Framework.SmartControls.SmartTextBox();
            this.VERSION = new Micube.Framework.SmartControls.SmartTextBox();
            this.NAME = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.CODE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VERSION.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NAME.Properties)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CODE
            // 
            this.CODE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CODE.LabelText = null;
            this.CODE.LanguageKey = null;
            this.CODE.Location = new System.Drawing.Point(3, 2);
            this.CODE.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CODE.Name = "CODE";
            this.CODE.Size = new System.Drawing.Size(124, 24);
            this.CODE.TabIndex = 17;
            // 
            // VERSION
            // 
            this.VERSION.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VERSION.LabelText = null;
            this.VERSION.LanguageKey = null;
            this.VERSION.Location = new System.Drawing.Point(133, 2);
            this.VERSION.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.VERSION.Name = "VERSION";
            this.VERSION.Properties.ReadOnly = true;
            this.VERSION.Size = new System.Drawing.Size(44, 24);
            this.VERSION.TabIndex = 19;
            // 
            // NAME
            // 
            this.NAME.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NAME.LabelText = null;
            this.NAME.LanguageKey = null;
            this.NAME.Location = new System.Drawing.Point(231, 2);
            this.NAME.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NAME.Name = "NAME";
            this.NAME.Properties.ReadOnly = true;
            this.NAME.Size = new System.Drawing.Size(126, 24);
            this.NAME.TabIndex = 18;
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 4;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.NAME, 3, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.VERSION, 1, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.CODE, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.btnSearch, 2, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 1;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(360, 28);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.ImageOptions.Image")));
            this.btnSearch.ImageOptions.ImageToTextIndent = 0;
            this.btnSearch.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSearch.IsBusy = false;
            this.btnSearch.Location = new System.Drawing.Point(180, 0);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btnSearch.Size = new System.Drawing.Size(48, 26);
            this.btnSearch.TabIndex = 20;
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // ucItemPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(900, 28);
            this.MinimumSize = new System.Drawing.Size(229, 28);
            this.Name = "ucItemPopup";
            this.Size = new System.Drawing.Size(360, 28);
            ((System.ComponentModel.ISupportInitialize)(this.CODE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VERSION.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NAME.Properties)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public Framework.SmartControls.SmartTextBox CODE;
        public Framework.SmartControls.SmartTextBox VERSION;
        public Framework.SmartControls.SmartTextBox NAME;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartButton btnSearch;
    }
}
