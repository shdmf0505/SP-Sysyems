namespace Micube.SmartMES.OutsideOrderMgnt
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
            this.txtProdcutCode = new Micube.Framework.SmartControls.SmartTextBox();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.CODE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VERSION.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NAME.Properties)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdcutCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // CODE
            // 
            this.CODE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CODE.LabelText = null;
            this.CODE.LanguageKey = null;
            this.CODE.Location = new System.Drawing.Point(3, 0);
            this.CODE.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.CODE.Name = "CODE";
            this.CODE.Size = new System.Drawing.Size(162, 24);
            this.CODE.TabIndex = 0;
            // 
            // VERSION
            // 
            this.VERSION.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VERSION.LabelText = null;
            this.VERSION.LanguageKey = null;
            this.VERSION.Location = new System.Drawing.Point(168, 0);
            this.VERSION.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.VERSION.Name = "VERSION";
            this.VERSION.Properties.ReadOnly = true;
            this.VERSION.Size = new System.Drawing.Size(44, 24);
            this.VERSION.TabIndex = 1;
            // 
            // NAME
            // 
            this.NAME.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NAME.LabelText = null;
            this.NAME.LanguageKey = null;
            this.NAME.Location = new System.Drawing.Point(248, 0);
            this.NAME.Margin = new System.Windows.Forms.Padding(0);
            this.NAME.Name = "NAME";
            this.NAME.Properties.ReadOnly = true;
            this.NAME.Size = new System.Drawing.Size(115, 24);
            this.NAME.TabIndex = 3;
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 5;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 165F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.txtProdcutCode, 4, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.btnSearch, 2, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.NAME, 3, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.VERSION, 1, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.CODE, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 1;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(364, 27);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // txtProdcutCode
            // 
            this.txtProdcutCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProdcutCode.LabelText = null;
            this.txtProdcutCode.LanguageKey = null;
            this.txtProdcutCode.Location = new System.Drawing.Point(366, 0);
            this.txtProdcutCode.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.txtProdcutCode.Name = "txtProdcutCode";
            this.txtProdcutCode.Properties.ReadOnly = true;
            this.txtProdcutCode.Size = new System.Drawing.Size(1, 24);
            this.txtProdcutCode.TabIndex = 4;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.ImageOptions.Image")));
            this.btnSearch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btnSearch.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnSearch.IsBusy = false;
            this.btnSearch.IsWrite = false;
            this.btnSearch.Location = new System.Drawing.Point(218, 0);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btnSearch.Size = new System.Drawing.Size(27, 24);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // ucItemPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.Name = "ucItemPopup";
            this.Size = new System.Drawing.Size(364, 27);
            ((System.ComponentModel.ISupportInitialize)(this.CODE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VERSION.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NAME.Properties)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtProdcutCode.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Framework.SmartControls.SmartTextBox CODE;
        public Framework.SmartControls.SmartTextBox VERSION;
        public Framework.SmartControls.SmartTextBox NAME;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        public Framework.SmartControls.SmartButton btnSearch;
        public Framework.SmartControls.SmartTextBox txtProdcutCode;
    }
}
