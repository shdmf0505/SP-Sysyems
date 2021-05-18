namespace Micube.SmartMES.EquipmentManagement
{
    partial class ucDurableCodePopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDurableCodePopup));
            this.txtCode = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.txtVersion = new Micube.Framework.SmartControls.SmartTextBox();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtVersion.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCode
            // 
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCode.LabelText = null;
            this.txtCode.LanguageKey = null;
            this.txtCode.Location = new System.Drawing.Point(3, 0);
            this.txtCode.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtCode.Properties.Appearance.Options.UseFont = true;
            this.txtCode.Size = new System.Drawing.Size(147, 24);
            this.txtCode.TabIndex = 0;
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 4;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.txtVersion, 1, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.txtCode, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.btnSearch, 2, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 1;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(242, 28);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // txtVersion
            // 
            this.txtVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtVersion.LabelText = null;
            this.txtVersion.LanguageKey = null;
            this.txtVersion.Location = new System.Drawing.Point(153, 0);
            this.txtVersion.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtVersion.Properties.Appearance.Options.UseFont = true;
            this.txtVersion.Size = new System.Drawing.Size(37, 24);
            this.txtVersion.TabIndex = 4;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.ImageOptions.Image")));
            this.btnSearch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btnSearch.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnSearch.IsBusy = false;
            this.btnSearch.IsWrite = false;
            this.btnSearch.Location = new System.Drawing.Point(193, 2);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.btnSearch.Size = new System.Drawing.Size(30, 22);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // ucDurableCodePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ucDurableCodePopup";
            this.Size = new System.Drawing.Size(242, 28);
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtVersion.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Framework.SmartControls.SmartTextBox txtCode;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        public Framework.SmartControls.SmartButton btnSearch;
        public Framework.SmartControls.SmartTextBox txtVersion;
    }
}
