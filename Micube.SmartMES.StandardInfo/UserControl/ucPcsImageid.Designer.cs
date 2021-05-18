namespace Micube.SmartMES.StandardInfo
{
    partial class ucPcsImageid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPcsImageid));
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.NAME = new Micube.Framework.SmartControls.SmartTextBox();
            this.CODE = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CODE.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 2;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.btnSearch, 1, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel1, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 1;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(166, 33);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.ImageOptions.Image")));
            this.btnSearch.ImageOptions.ImageToTextIndent = 0;
            this.btnSearch.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSearch.IsBusy = false;
            this.btnSearch.Location = new System.Drawing.Point(126, 2);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btnSearch.Size = new System.Drawing.Size(36, 28);
            this.btnSearch.TabIndex = 20;
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.NAME);
            this.smartPanel1.Controls.Add(this.CODE);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(3, 3);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(118, 27);
            this.smartPanel1.TabIndex = 21;
            // 
            // NAME
            // 
            this.NAME.LabelText = null;
            this.NAME.LanguageKey = null;
            this.NAME.Location = new System.Drawing.Point(0, 0);
            this.NAME.Name = "NAME";
            this.NAME.Size = new System.Drawing.Size(118, 24);
            this.NAME.TabIndex = 1;
            this.NAME.Tag = "PCSIMAGEID";
            // 
            // CODE
            // 
            this.CODE.LabelText = null;
            this.CODE.LanguageKey = null;
            this.CODE.Location = new System.Drawing.Point(0, 0);
            this.CODE.Name = "CODE";
            this.CODE.Size = new System.Drawing.Size(118, 24);
            this.CODE.TabIndex = 0;
            this.CODE.Tag = "PCSIMAGEID";
            // 
            // ucPcsImageid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(166, 33);
            this.MinimumSize = new System.Drawing.Size(140, 33);
            this.Name = "ucPcsImageid";
            this.Size = new System.Drawing.Size(166, 33);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CODE.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartPanel smartPanel1;
        public Framework.SmartControls.SmartTextBox CODE;
        public Framework.SmartControls.SmartTextBox NAME;
    }
}
