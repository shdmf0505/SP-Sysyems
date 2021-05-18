namespace Micube.SmartMES.StandardInfo.Popup
{
    partial class LabelMasterOpenPopup
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
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.btnLoad = new Micube.Framework.SmartControls.SmartButton();
            this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
            this.documentViewer1 = new DevExpress.XtraPrinting.Preview.DocumentViewer();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.grdLabelDefList = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
            this.smartPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartPanel1);
            this.pnlMain.Size = new System.Drawing.Size(864, 491);
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.tableLayoutPanel1);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(864, 491);
            this.smartPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.smartPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(864, 491);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnLoad);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 468);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(864, 23);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.IsBusy = false;
            this.btnCancel.LanguageKey = "CLOSE";
            this.btnCancel.Location = new System.Drawing.Point(789, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "smartButton1";
            this.btnCancel.TooltipLanguageKey = "";
            // 
            // btnLoad
            // 
            this.btnLoad.AllowFocus = false;
            this.btnLoad.IsBusy = false;
            this.btnLoad.LanguageKey = "LOAD";
            this.btnLoad.Location = new System.Drawing.Point(708, 0);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "smartButton2";
            this.btnLoad.TooltipLanguageKey = "";
            // 
            // smartPanel2
            // 
            this.smartPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel2.Controls.Add(this.documentViewer1);
            this.smartPanel2.Controls.Add(this.smartSpliterControl1);
            this.smartPanel2.Controls.Add(this.grdLabelDefList);
            this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel2.Location = new System.Drawing.Point(3, 3);
            this.smartPanel2.Name = "smartPanel2";
            this.smartPanel2.Size = new System.Drawing.Size(858, 452);
            this.smartPanel2.TabIndex = 2;
            // 
            // documentViewer1
            // 
            this.documentViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentViewer1.IsMetric = true;
            this.documentViewer1.Location = new System.Drawing.Point(321, 0);
            this.documentViewer1.Name = "documentViewer1";
            this.documentViewer1.Size = new System.Drawing.Size(537, 452);
            this.documentViewer1.TabIndex = 6;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Location = new System.Drawing.Point(316, 0);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(5, 452);
            this.smartSpliterControl1.TabIndex = 5;
            this.smartSpliterControl1.TabStop = false;
            // 
            // grdLabelDefList
            // 
            this.grdLabelDefList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLabelDefList.Dock = System.Windows.Forms.DockStyle.Left;
            this.grdLabelDefList.IsUsePaging = false;
            this.grdLabelDefList.LanguageKey = "LABELDEFINITIONLIST";
            this.grdLabelDefList.Location = new System.Drawing.Point(0, 0);
            this.grdLabelDefList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLabelDefList.Name = "grdLabelDefList";
            this.grdLabelDefList.ShowBorder = true;
            this.grdLabelDefList.ShowStatusBar = false;
            this.grdLabelDefList.Size = new System.Drawing.Size(316, 452);
            this.grdLabelDefList.TabIndex = 4;
            // 
            // LabelMasterOpenPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 511);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.LanguageKey = "LABELMASTEROPENPOPUP";
            this.Name = "LabelMasterOpenPopup";
            this.Text = "LABELMASTEROPENPOPUP";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
            this.smartPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartPanel smartPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnCancel;
        private Framework.SmartControls.SmartButton btnLoad;
        private Framework.SmartControls.SmartPanel smartPanel2;
        private DevExpress.XtraPrinting.Preview.DocumentViewer documentViewer1;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartBandedGrid grdLabelDefList;
    }
}