namespace Micube.SmartMES.StandardInfo.Popup
{
    partial class SpecDetailPopUp
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
            this.grdSpecDetail = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
            this.smartPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartPanel1);
            this.pnlMain.Size = new System.Drawing.Size(1020, 436);
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.tableLayoutPanel1);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(1020, 436);
            this.smartPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grdSpecDetail, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.smartPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1020, 436);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // grdSpecDetail
            // 
            this.grdSpecDetail.Caption = "";
            this.grdSpecDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSpecDetail.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdSpecDetail.IsUsePaging = false;
            this.grdSpecDetail.LanguageKey = "SPECDEFINITIONLIST";
            this.grdSpecDetail.Location = new System.Drawing.Point(0, 0);
            this.grdSpecDetail.Margin = new System.Windows.Forms.Padding(0);
            this.grdSpecDetail.Name = "grdSpecDetail";
            this.grdSpecDetail.ShowBorder = true;
            this.grdSpecDetail.ShowStatusBar = false;
            this.grdSpecDetail.Size = new System.Drawing.Size(1020, 396);
            this.grdSpecDetail.TabIndex = 112;
            // 
            // smartPanel2
            // 
            this.smartPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel2.Controls.Add(this.btnCancel);
            this.smartPanel2.Controls.Add(this.btnSave);
            this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel2.Location = new System.Drawing.Point(3, 399);
            this.smartPanel2.Name = "smartPanel2";
            this.smartPanel2.Size = new System.Drawing.Size(1014, 34);
            this.smartPanel2.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.IsBusy = false;
            this.btnCancel.IsWrite = false;
            this.btnCancel.LanguageKey = "CLOSE";
            this.btnCancel.Location = new System.Drawing.Point(929, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "smartButton1";
            this.btnCancel.TooltipLanguageKey = "";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(848, 3);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "smartButton2";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // SpecDetailPopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 456);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.LanguageKey = "SPECDETAILPOPUP";
            this.Name = "SpecDetailPopUp";
            this.Text = "SpecDetailPopUp";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
            this.smartPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartPanel smartPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartPanel smartPanel2;
        private Framework.SmartControls.SmartButton btnCancel;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartBandedGrid grdSpecDetail;
    }
}