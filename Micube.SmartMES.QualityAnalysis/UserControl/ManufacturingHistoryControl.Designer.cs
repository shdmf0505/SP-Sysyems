namespace Micube.SmartMES.QualityAnalysis
{
    partial class ManufacturingHistoryControl
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
            this.spcManufacturingHistoryControl = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDelete = new Micube.Framework.SmartControls.SmartButton();
            this.btnManufacturingHistory = new Micube.Framework.SmartControls.SmartButton();
            this.grdManufacturingHistory = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.spcManufacturingHistoryControl.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // spcManufacturingHistoryControl
            // 
            this.spcManufacturingHistoryControl.ColumnCount = 2;
            this.spcManufacturingHistoryControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.spcManufacturingHistoryControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.spcManufacturingHistoryControl.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.spcManufacturingHistoryControl.Controls.Add(this.grdManufacturingHistory, 0, 1);
            this.spcManufacturingHistoryControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcManufacturingHistoryControl.Location = new System.Drawing.Point(0, 0);
            this.spcManufacturingHistoryControl.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcManufacturingHistoryControl.Name = "spcManufacturingHistoryControl";
            this.spcManufacturingHistoryControl.RowCount = 2;
            this.spcManufacturingHistoryControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.spcManufacturingHistoryControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.spcManufacturingHistoryControl.Size = new System.Drawing.Size(800, 301);
            this.spcManufacturingHistoryControl.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.spcManufacturingHistoryControl.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.btnDelete);
            this.flowLayoutPanel1.Controls.Add(this.btnManufacturingHistory);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(794, 29);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.AllowFocus = false;
            this.btnDelete.IsBusy = false;
            this.btnDelete.LanguageKey = "ERASE";
            this.btnDelete.Location = new System.Drawing.Point(711, 0);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDelete.Size = new System.Drawing.Size(80, 25);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "삭제";
            this.btnDelete.TooltipLanguageKey = "";
            // 
            // btnManufacturingHistory
            // 
            this.btnManufacturingHistory.AllowFocus = false;
            this.btnManufacturingHistory.IsBusy = false;
            this.btnManufacturingHistory.LanguageKey = "MANUFACTURINGHISTORYCHECK";
            this.btnManufacturingHistory.Location = new System.Drawing.Point(585, 0);
            this.btnManufacturingHistory.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnManufacturingHistory.Name = "btnManufacturingHistory";
            this.btnManufacturingHistory.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnManufacturingHistory.Size = new System.Drawing.Size(120, 25);
            this.btnManufacturingHistory.TabIndex = 1;
            this.btnManufacturingHistory.Text = "재조이력조회";
            this.btnManufacturingHistory.TooltipLanguageKey = "";
            // 
            // grdManufacturingHistory
            // 
            this.grdManufacturingHistory.AccessibleName = "";
            this.grdManufacturingHistory.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.spcManufacturingHistoryControl.SetColumnSpan(this.grdManufacturingHistory, 2);
            this.grdManufacturingHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdManufacturingHistory.IsUsePaging = false;
            this.grdManufacturingHistory.LanguageKey = "MANUFACTURINGHISTORY";
            this.grdManufacturingHistory.Location = new System.Drawing.Point(0, 35);
            this.grdManufacturingHistory.Margin = new System.Windows.Forms.Padding(0);
            this.grdManufacturingHistory.Name = "grdManufacturingHistory";
            this.grdManufacturingHistory.ShowBorder = true;
            this.grdManufacturingHistory.Size = new System.Drawing.Size(800, 266);
            this.grdManufacturingHistory.TabIndex = 1;
            // 
            // ManufacturingHistoryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spcManufacturingHistoryControl);
            this.Name = "ManufacturingHistoryControl";
            this.Size = new System.Drawing.Size(800, 301);
            this.spcManufacturingHistoryControl.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel spcManufacturingHistoryControl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnDelete;
        private Framework.SmartControls.SmartButton btnManufacturingHistory;
        private Framework.SmartControls.SmartBandedGrid grdManufacturingHistory;
    }
}
