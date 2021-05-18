namespace Micube.SmartMES.QualityAnalysis
{ 
    partial class ucProcessAreaGrid
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
            this.pnlMain = new Micube.Framework.SmartControls.SmartPanel();
            this.pnlGrid = new Micube.Framework.SmartControls.SmartPanel();
            this.splitContiainerGrid = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdProcess = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdAreaEquipment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.pnlTopInfo = new Micube.Framework.SmartControls.SmartPanel();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.chkEditMainProcess = new Micube.Framework.SmartControls.SmartCheckEdit();
            this.periodSmartLabelTextBox = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.itemSmartLabelTextBox = new Micube.Framework.SmartControls.SmartLabelTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGrid)).BeginInit();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContiainerGrid)).BeginInit();
            this.splitContiainerGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopInfo)).BeginInit();
            this.pnlTopInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkEditMainProcess.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodSmartLabelTextBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemSmartLabelTextBox.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlGrid);
            this.pnlMain.Controls.Add(this.pnlTopInfo);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(946, 360);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.splitContiainerGrid);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(2, 49);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(942, 309);
            this.pnlGrid.TabIndex = 1;
            // 
            // splitContiainerGrid
            // 
            this.splitContiainerGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContiainerGrid.Location = new System.Drawing.Point(2, 2);
            this.splitContiainerGrid.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.splitContiainerGrid.Name = "splitContiainerGrid";
            this.splitContiainerGrid.Panel1.Controls.Add(this.grdProcess);
            this.splitContiainerGrid.Panel1.Text = "Panel1";
            this.splitContiainerGrid.Panel2.Controls.Add(this.grdAreaEquipment);
            this.splitContiainerGrid.Panel2.Text = "Panel2";
            this.splitContiainerGrid.Size = new System.Drawing.Size(938, 305);
            this.splitContiainerGrid.SplitterPosition = 250;
            this.splitContiainerGrid.TabIndex = 0;
            // 
            // grdProcess
            // 
            this.grdProcess.Caption = "";
            this.grdProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProcess.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.None;
            this.grdProcess.IsUsePaging = false;
            this.grdProcess.LanguageKey = null;
            this.grdProcess.Location = new System.Drawing.Point(0, 0);
            this.grdProcess.Margin = new System.Windows.Forms.Padding(0);
            this.grdProcess.Name = "grdProcess";
            this.grdProcess.ShowBorder = true;
            this.grdProcess.ShowStatusBar = false;
            this.grdProcess.Size = new System.Drawing.Size(250, 305);
            this.grdProcess.TabIndex = 1;
            // 
            // grdAreaEquipment
            // 
            this.grdAreaEquipment.Caption = "";
            this.grdAreaEquipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAreaEquipment.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.None;
            this.grdAreaEquipment.IsUsePaging = false;
            this.grdAreaEquipment.LanguageKey = null;
            this.grdAreaEquipment.Location = new System.Drawing.Point(0, 0);
            this.grdAreaEquipment.Margin = new System.Windows.Forms.Padding(0);
            this.grdAreaEquipment.Name = "grdAreaEquipment";
            this.grdAreaEquipment.ShowBorder = true;
            this.grdAreaEquipment.ShowStatusBar = false;
            this.grdAreaEquipment.Size = new System.Drawing.Size(683, 305);
            this.grdAreaEquipment.TabIndex = 2;
            // 
            // pnlTopInfo
            // 
            this.pnlTopInfo.Controls.Add(this.btnSearch);
            this.pnlTopInfo.Controls.Add(this.chkEditMainProcess);
            this.pnlTopInfo.Controls.Add(this.periodSmartLabelTextBox);
            this.pnlTopInfo.Controls.Add(this.itemSmartLabelTextBox);
            this.pnlTopInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopInfo.Location = new System.Drawing.Point(2, 2);
            this.pnlTopInfo.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTopInfo.Name = "pnlTopInfo";
            this.pnlTopInfo.Size = new System.Drawing.Size(942, 47);
            this.pnlTopInfo.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.IsBusy = false;
            this.btnSearch.IsWrite = false;
            this.btnSearch.LanguageKey = "SEARCHAPPROVALUSER";
            this.btnSearch.Location = new System.Drawing.Point(781, 9);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(80, 25);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "조회";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // chkEditMainProcess
            // 
            this.chkEditMainProcess.LabelText = null;
            this.chkEditMainProcess.LanguageKey = "MAINPROCESSSEGMENT";
            this.chkEditMainProcess.Location = new System.Drawing.Point(669, 12);
            this.chkEditMainProcess.Name = "chkEditMainProcess";
            this.chkEditMainProcess.Properties.Caption = "주요공정";
            this.chkEditMainProcess.Size = new System.Drawing.Size(73, 19);
            this.chkEditMainProcess.TabIndex = 8;
            // 
            // periodSmartLabelTextBox
            // 
            this.periodSmartLabelTextBox.EditorWidth = "200%";
            this.periodSmartLabelTextBox.LabelText = "기간";
            this.periodSmartLabelTextBox.LanguageKey = "PERIOD";
            this.periodSmartLabelTextBox.Location = new System.Drawing.Point(339, 13);
            this.periodSmartLabelTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.periodSmartLabelTextBox.Name = "periodSmartLabelTextBox";
            this.periodSmartLabelTextBox.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.periodSmartLabelTextBox.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.periodSmartLabelTextBox.Properties.ReadOnly = true;
            this.periodSmartLabelTextBox.Size = new System.Drawing.Size(292, 20);
            this.periodSmartLabelTextBox.TabIndex = 7;
            // 
            // itemSmartLabelTextBox
            // 
            this.itemSmartLabelTextBox.EditorWidth = "200%";
            this.itemSmartLabelTextBox.LabelText = "품목명";
            this.itemSmartLabelTextBox.LanguageKey = "PRODUCTDEFNAME";
            this.itemSmartLabelTextBox.Location = new System.Drawing.Point(15, 13);
            this.itemSmartLabelTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.itemSmartLabelTextBox.Name = "itemSmartLabelTextBox";
            this.itemSmartLabelTextBox.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.itemSmartLabelTextBox.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.itemSmartLabelTextBox.Properties.ReadOnly = true;
            this.itemSmartLabelTextBox.Size = new System.Drawing.Size(292, 20);
            this.itemSmartLabelTextBox.TabIndex = 6;
            // 
            // ucProcessAreaGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ucProcessAreaGrid";
            this.Size = new System.Drawing.Size(946, 360);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGrid)).EndInit();
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContiainerGrid)).EndInit();
            this.splitContiainerGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopInfo)).EndInit();
            this.pnlTopInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkEditMainProcess.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodSmartLabelTextBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemSmartLabelTextBox.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartPanel pnlMain;
        private Framework.SmartControls.SmartPanel pnlTopInfo;
        private Framework.SmartControls.SmartLabelTextBox periodSmartLabelTextBox;
        private Framework.SmartControls.SmartLabelTextBox itemSmartLabelTextBox;
        private Framework.SmartControls.SmartPanel pnlGrid;
        private Framework.SmartControls.SmartSpliterContainer splitContiainerGrid;
        private Framework.SmartControls.SmartCheckEdit chkEditMainProcess;
        private Framework.SmartControls.SmartBandedGrid grdProcess;
        private Framework.SmartControls.SmartBandedGrid grdAreaEquipment;
        private Framework.SmartControls.SmartButton btnSearch;
    }
}
