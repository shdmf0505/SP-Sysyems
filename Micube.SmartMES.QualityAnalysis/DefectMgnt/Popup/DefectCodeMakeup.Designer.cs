namespace Micube.SmartMES.QualityAnalysis
{
    partial class DefectCodeMakeup
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
            this.gbxDefectCodeTakeOut = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.grdDefectCode = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdDefectCodeMakeup = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbxDefectCodeTakeOut)).BeginInit();
            this.gbxDefectCodeTakeOut.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(1272, 432);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.gbxDefectCodeTakeOut, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.53732F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.462687F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1272, 432);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gbxDefectCodeTakeOut
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gbxDefectCodeTakeOut, 2);
            this.gbxDefectCodeTakeOut.Controls.Add(this.tableLayoutPanel2);
            this.gbxDefectCodeTakeOut.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbxDefectCodeTakeOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxDefectCodeTakeOut.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbxDefectCodeTakeOut.Location = new System.Drawing.Point(0, 0);
            this.gbxDefectCodeTakeOut.Margin = new System.Windows.Forms.Padding(0);
            this.gbxDefectCodeTakeOut.Name = "gbxDefectCodeTakeOut";
            this.gbxDefectCodeTakeOut.ShowBorder = true;
            this.gbxDefectCodeTakeOut.Size = new System.Drawing.Size(1272, 399);
            this.gbxDefectCodeTakeOut.TabIndex = 0;
            this.gbxDefectCodeTakeOut.Text = "불량정보";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.grdDefectCode, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.grdDefectCodeMakeup, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 31);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1268, 366);
            this.tableLayoutPanel2.TabIndex = 0;
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
            this.grdDefectCode.Location = new System.Drawing.Point(0, 0);
            this.grdDefectCode.Margin = new System.Windows.Forms.Padding(0);
            this.grdDefectCode.Name = "grdDefectCode";
            this.grdDefectCode.ShowBorder = false;
            this.grdDefectCode.ShowButtonBar = false;
            this.grdDefectCode.ShowStatusBar = false;
            this.grdDefectCode.Size = new System.Drawing.Size(1268, 103);
            this.grdDefectCode.TabIndex = 0;
            this.grdDefectCode.UseAutoBestFitColumns = false;
            // 
            // grdDefectCodeMakeup
            // 
            this.grdDefectCodeMakeup.Caption = "불량내역";
            this.grdDefectCodeMakeup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDefectCodeMakeup.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)(((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete)));
            this.grdDefectCodeMakeup.IsUsePaging = false;
            this.grdDefectCodeMakeup.LanguageKey = null;
            this.grdDefectCodeMakeup.Location = new System.Drawing.Point(0, 113);
            this.grdDefectCodeMakeup.Margin = new System.Windows.Forms.Padding(0);
            this.grdDefectCodeMakeup.Name = "grdDefectCodeMakeup";
            this.grdDefectCodeMakeup.ShowBorder = false;
            this.grdDefectCodeMakeup.ShowStatusBar = false;
            this.grdDefectCodeMakeup.Size = new System.Drawing.Size(1268, 242);
            this.grdDefectCodeMakeup.TabIndex = 1;
            this.grdDefectCodeMakeup.UseAutoBestFitColumns = false;
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 399);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1272, 33);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(1192, 6);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 6, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(1106, 6);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "저장";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // DefectCodeMakeup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1292, 452);
            this.Name = "DefectCodeMakeup";
            this.Text = "불량내역조정";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbxDefectCodeTakeOut)).EndInit();
            this.gbxDefectCodeTakeOut.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartGroupBox gbxDefectCodeTakeOut;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Framework.SmartControls.SmartBandedGrid grdDefectCode;
        private Framework.SmartControls.SmartBandedGrid grdDefectCodeMakeup;
        private Framework.SmartControls.SmartButton btnSave;
    }
}