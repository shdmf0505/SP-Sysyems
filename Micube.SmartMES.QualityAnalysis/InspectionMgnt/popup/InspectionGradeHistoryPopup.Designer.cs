namespace Micube.SmartMES.QualityAnalysis
{
    partial class InspectionGradeHistoryPopup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.smartSplitTableLayoutPanel3 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdInspectionGrade = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtCapacityType = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtScore = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtGrade = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            this.smartSplitTableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCapacityType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtScore.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGrade.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.pnlMain.Size = new System.Drawing.Size(758, 338);
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 1;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.smartGroupBox1, 0, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 2;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(758, 338);
            this.smartSplitTableLayoutPanel2.TabIndex = 2;
            // 
            // smartGroupBox1
            // 
            this.smartGroupBox1.Controls.Add(this.smartSplitTableLayoutPanel3);
            this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartGroupBox1.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox1.LanguageKey = "INSPECTIONGRADEHISTORY";
            this.smartGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.smartGroupBox1.Name = "smartGroupBox1";
            this.smartGroupBox1.ShowBorder = true;
            this.smartGroupBox1.Size = new System.Drawing.Size(752, 302);
            this.smartGroupBox1.TabIndex = 1;
            this.smartGroupBox1.Text = "검사원 등급이력";
            // 
            // smartSplitTableLayoutPanel3
            // 
            this.smartSplitTableLayoutPanel3.ColumnCount = 1;
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel3.Controls.Add(this.grdInspectionGrade, 0, 1);
            this.smartSplitTableLayoutPanel3.Controls.Add(this.flowLayoutPanel4, 0, 0);
            this.smartSplitTableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel3.Location = new System.Drawing.Point(2, 31);
            this.smartSplitTableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel3.Name = "smartSplitTableLayoutPanel3";
            this.smartSplitTableLayoutPanel3.RowCount = 2;
            this.smartSplitTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel3.Size = new System.Drawing.Size(748, 269);
            this.smartSplitTableLayoutPanel3.TabIndex = 0;
            // 
            // grdInspectionGrade
            // 
            this.grdInspectionGrade.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspectionGrade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspectionGrade.IsUsePaging = false;
            this.grdInspectionGrade.LanguageKey = null;
            this.grdInspectionGrade.Location = new System.Drawing.Point(0, 30);
            this.grdInspectionGrade.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspectionGrade.Name = "grdInspectionGrade";
            this.grdInspectionGrade.ShowBorder = false;
            this.grdInspectionGrade.ShowButtonBar = false;
            this.grdInspectionGrade.Size = new System.Drawing.Size(748, 239);
            this.grdInspectionGrade.TabIndex = 0;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.txtCapacityType);
            this.flowLayoutPanel4.Controls.Add(this.txtScore);
            this.flowLayoutPanel4.Controls.Add(this.txtGrade);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(748, 30);
            this.flowLayoutPanel4.TabIndex = 2;
            // 
            // txtCapacityType
            // 
            this.txtCapacityType.AutoHeight = false;
            this.txtCapacityType.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtCapacityType.LabelText = "자격구분";
            this.txtCapacityType.LabelVAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtCapacityType.LabelWidth = "30%";
            this.txtCapacityType.LanguageKey = "CAPACITYTYPE";
            this.txtCapacityType.Location = new System.Drawing.Point(3, 3);
            this.txtCapacityType.Name = "txtCapacityType";
            this.txtCapacityType.Properties.AutoHeight = false;
            this.txtCapacityType.Properties.ReadOnly = true;
            this.txtCapacityType.Size = new System.Drawing.Size(264, 20);
            this.txtCapacityType.TabIndex = 0;
            // 
            // txtScore
            // 
            this.txtScore.AutoHeight = false;
            this.txtScore.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtScore.LabelText = "점수";
            this.txtScore.LabelVAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtScore.LabelWidth = "30%";
            this.txtScore.LanguageKey = "SCORE";
            this.txtScore.Location = new System.Drawing.Point(273, 3);
            this.txtScore.Name = "txtScore";
            this.txtScore.Properties.AutoHeight = false;
            this.txtScore.Properties.ReadOnly = true;
            this.txtScore.Size = new System.Drawing.Size(280, 20);
            this.txtScore.TabIndex = 1;
            // 
            // txtGrade
            // 
            this.txtGrade.AutoHeight = false;
            this.txtGrade.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtGrade.LabelText = "등급";
            this.txtGrade.LabelVAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtGrade.LabelWidth = "30%";
            this.txtGrade.LanguageKey = "GRADE";
            this.txtGrade.Location = new System.Drawing.Point(559, 3);
            this.txtGrade.Name = "txtGrade";
            this.txtGrade.Properties.AutoHeight = false;
            this.txtGrade.Properties.ReadOnly = true;
            this.txtGrade.Size = new System.Drawing.Size(183, 20);
            this.txtGrade.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 308);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(758, 30);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.IsBusy = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(675, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // InspectionGradeHistoryPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 358);
            this.Name = "InspectionGradeHistoryPopup";
            this.Text = "검사원 등급이력";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCapacityType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtScore.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGrade.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel3;
        private Framework.SmartControls.SmartBandedGrid grdInspectionGrade;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private Framework.SmartControls.SmartLabelTextBox txtCapacityType;
        private Framework.SmartControls.SmartLabelTextBox txtScore;
        private Framework.SmartControls.SmartLabelTextBox txtGrade;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnClose;
    }
}