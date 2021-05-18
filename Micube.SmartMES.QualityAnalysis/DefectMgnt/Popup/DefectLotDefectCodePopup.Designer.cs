namespace Micube.SmartMES.QualityAnalysis
{
    partial class DefectLotDefectCodePopup
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gbxDefectCode = new Micube.Framework.SmartControls.SmartGroupBox();
            this.grdDefectCode = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbxDefectCode)).BeginInit();
            this.gbxDefectCode.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(977, 424);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.gbxDefectCode, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(977, 424);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gbxDefectCode
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gbxDefectCode, 2);
            this.gbxDefectCode.Controls.Add(this.grdDefectCode);
            this.gbxDefectCode.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbxDefectCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxDefectCode.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbxDefectCode.Location = new System.Drawing.Point(0, 0);
            this.gbxDefectCode.Margin = new System.Windows.Forms.Padding(0);
            this.gbxDefectCode.Name = "gbxDefectCode";
            this.gbxDefectCode.ShowBorder = true;
            this.gbxDefectCode.Size = new System.Drawing.Size(977, 394);
            this.gbxDefectCode.TabIndex = 0;
            this.gbxDefectCode.Text = "불량 내역";
            // 
            // grdDefectCode
            // 
            this.grdDefectCode.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdDefectCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDefectCode.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdDefectCode.IsUsePaging = false;
            this.grdDefectCode.LanguageKey = null;
            this.grdDefectCode.Location = new System.Drawing.Point(2, 31);
            this.grdDefectCode.Margin = new System.Windows.Forms.Padding(0);
            this.grdDefectCode.Name = "grdDefectCode";
            this.grdDefectCode.ShowBorder = false;
            this.grdDefectCode.ShowButtonBar = false;
            this.grdDefectCode.ShowStatusBar = false;
            this.grdDefectCode.Size = new System.Drawing.Size(973, 361);
            this.grdDefectCode.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 394);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(977, 30);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.Location = new System.Drawing.Point(897, 5);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 24);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // DefectLotDefectCodePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 444);
            this.Name = "DefectLotDefectCodePopup";
            this.Text = "LOT별 불량";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbxDefectCode)).EndInit();
            this.gbxDefectCode.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartGroupBox gbxDefectCode;
        private Framework.SmartControls.SmartBandedGrid grdDefectCode;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnClose;
    }
}