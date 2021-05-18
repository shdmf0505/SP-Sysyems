namespace Micube.SmartMES.QualityAnalysis
{
    partial class NCRProgressControl
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
            this.gbxNCRProgress = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblHandling = new Micube.Framework.SmartControls.SmartLabel();
            this.cboHandling = new Micube.Framework.SmartControls.SmartComboBox();
            this.btnApply = new Micube.Framework.SmartControls.SmartButton();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDelete = new Micube.Framework.SmartControls.SmartButton();
            this.btnAddAffectLot = new Micube.Framework.SmartControls.SmartButton();
            this.btnLockingReset = new Micube.Framework.SmartControls.SmartButton();
            this.btnLockingApply = new Micube.Framework.SmartControls.SmartButton();
            this.grdNCRProgress = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.gbxNCRProgress)).BeginInit();
            this.gbxNCRProgress.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboHandling.Properties)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxNCRProgress
            // 
            this.gbxNCRProgress.Controls.Add(this.tableLayoutPanel1);
            this.gbxNCRProgress.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbxNCRProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxNCRProgress.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbxNCRProgress.Location = new System.Drawing.Point(0, 0);
            this.gbxNCRProgress.Name = "gbxNCRProgress";
            this.gbxNCRProgress.ShowBorder = true;
            this.gbxNCRProgress.Size = new System.Drawing.Size(800, 301);
            this.gbxNCRProgress.TabIndex = 0;
            this.gbxNCRProgress.Text = "NCR 진행현황";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.31492F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.68508F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.grdNCRProgress, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 31);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(796, 268);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lblHandling);
            this.flowLayoutPanel1.Controls.Add(this.cboHandling);
            this.flowLayoutPanel1.Controls.Add(this.btnApply);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 5);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(344, 23);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // lblHandling
            // 
            this.lblHandling.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHandling.Location = new System.Drawing.Point(0, 0);
            this.lblHandling.Margin = new System.Windows.Forms.Padding(0);
            this.lblHandling.Name = "lblHandling";
            this.lblHandling.Size = new System.Drawing.Size(40, 23);
            this.lblHandling.TabIndex = 0;
            this.lblHandling.Text = "처리여부";
            // 
            // cboHandling
            // 
            this.cboHandling.LabelText = null;
            this.cboHandling.LanguageKey = null;
            this.cboHandling.Location = new System.Drawing.Point(43, 0);
            this.cboHandling.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cboHandling.Name = "cboHandling";
            this.cboHandling.PopupWidth = 0;
            this.cboHandling.Properties.AutoHeight = false;
            this.cboHandling.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboHandling.Properties.NullText = "";
            this.cboHandling.ShowHeader = true;
            this.cboHandling.Size = new System.Drawing.Size(154, 23);
            this.cboHandling.TabIndex = 1;
            this.cboHandling.VisibleColumns = null;
            this.cboHandling.VisibleColumnsWidth = null;
            // 
            // btnApply
            // 
            this.btnApply.AllowFocus = false;
            this.btnApply.IsBusy = false;
            this.btnApply.IsWrite = false;
            this.btnApply.Location = new System.Drawing.Point(203, 0);
            this.btnApply.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnApply.Size = new System.Drawing.Size(80, 23);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "적용";
            this.btnApply.TooltipLanguageKey = "";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnDelete);
            this.flowLayoutPanel2.Controls.Add(this.btnAddAffectLot);
            this.flowLayoutPanel2.Controls.Add(this.btnLockingReset);
            this.flowLayoutPanel2.Controls.Add(this.btnLockingApply);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(344, 5);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(452, 23);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // btnDelete
            // 
            this.btnDelete.AllowFocus = false;
            this.btnDelete.IsBusy = false;
            this.btnDelete.IsWrite = false;
            this.btnDelete.Location = new System.Drawing.Point(372, 0);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDelete.Size = new System.Drawing.Size(80, 23);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "삭제";
            this.btnDelete.TooltipLanguageKey = "";
            // 
            // btnAddAffectLot
            // 
            this.btnAddAffectLot.AllowFocus = false;
            this.btnAddAffectLot.IsBusy = false;
            this.btnAddAffectLot.IsWrite = false;
            this.btnAddAffectLot.Location = new System.Drawing.Point(247, 0);
            this.btnAddAffectLot.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnAddAffectLot.Name = "btnAddAffectLot";
            this.btnAddAffectLot.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnAddAffectLot.Size = new System.Drawing.Size(119, 23);
            this.btnAddAffectLot.TabIndex = 1;
            this.btnAddAffectLot.Text = "Affect Lot 추가 ";
            this.btnAddAffectLot.TooltipLanguageKey = "";
            // 
            // btnLockingReset
            // 
            this.btnLockingReset.AllowFocus = false;
            this.btnLockingReset.IsBusy = false;
            this.btnLockingReset.IsWrite = false;
            this.btnLockingReset.Location = new System.Drawing.Point(142, 0);
            this.btnLockingReset.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnLockingReset.Name = "btnLockingReset";
            this.btnLockingReset.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnLockingReset.Size = new System.Drawing.Size(99, 23);
            this.btnLockingReset.TabIndex = 0;
            this.btnLockingReset.Text = "Locking 초기화";
            this.btnLockingReset.TooltipLanguageKey = "";
            // 
            // btnLockingApply
            // 
            this.btnLockingApply.AllowFocus = false;
            this.btnLockingApply.IsBusy = false;
            this.btnLockingApply.IsWrite = false;
            this.btnLockingApply.Location = new System.Drawing.Point(36, 0);
            this.btnLockingApply.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnLockingApply.Name = "btnLockingApply";
            this.btnLockingApply.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnLockingApply.Size = new System.Drawing.Size(100, 23);
            this.btnLockingApply.TabIndex = 1;
            this.btnLockingApply.Text = "Locking 적용";
            this.btnLockingApply.TooltipLanguageKey = "";
            // 
            // grdNCRProgress
            // 
            this.grdNCRProgress.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.tableLayoutPanel1.SetColumnSpan(this.grdNCRProgress, 2);
            this.grdNCRProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdNCRProgress.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdNCRProgress.IsUsePaging = false;
            this.grdNCRProgress.LanguageKey = null;
            this.grdNCRProgress.Location = new System.Drawing.Point(0, 33);
            this.grdNCRProgress.Margin = new System.Windows.Forms.Padding(0);
            this.grdNCRProgress.Name = "grdNCRProgress";
            this.grdNCRProgress.ShowBorder = false;
            this.grdNCRProgress.ShowButtonBar = false;
            this.grdNCRProgress.Size = new System.Drawing.Size(796, 235);
            this.grdNCRProgress.TabIndex = 0;
            // 
            // NCRProgressControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxNCRProgress);
            this.Name = "NCRProgressControl";
            this.Size = new System.Drawing.Size(800, 301);
            ((System.ComponentModel.ISupportInitialize)(this.gbxNCRProgress)).EndInit();
            this.gbxNCRProgress.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboHandling.Properties)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartGroupBox gbxNCRProgress;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartBandedGrid grdNCRProgress;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Framework.SmartControls.SmartButton btnDelete;
        private Framework.SmartControls.SmartButton btnAddAffectLot;
        private Framework.SmartControls.SmartLabel lblHandling;
        private Framework.SmartControls.SmartComboBox cboHandling;
        private Framework.SmartControls.SmartButton btnApply;
        private Framework.SmartControls.SmartButton btnLockingReset;
        private Framework.SmartControls.SmartButton btnLockingApply;
    }
}
