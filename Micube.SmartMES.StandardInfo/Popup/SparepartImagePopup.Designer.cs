namespace Micube.SmartMES.StandardInfo
{
    partial class SparepartImagePopup
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtSparepartID = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtSparepartName = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.btnFileRegister = new Micube.Framework.SmartControls.SmartButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.btnOK = new Micube.Framework.SmartControls.SmartButton();
            this.picbox = new Micube.Framework.SmartControls.SmartPictureEdit();
            this.btnFileDel = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSparepartID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSparepartName.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbox.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(736, 430);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.picbox, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(736, 430);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.btnFileDel, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtSparepartID, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtSparepartName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFileRegister, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(736, 28);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // txtSparepartID
            // 
            this.txtSparepartID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSparepartID.LanguageKey = "SPAREPARTID";
            this.txtSparepartID.Location = new System.Drawing.Point(0, 0);
            this.txtSparepartID.Margin = new System.Windows.Forms.Padding(0);
            this.txtSparepartID.Name = "txtSparepartID";
            this.txtSparepartID.Size = new System.Drawing.Size(256, 20);
            this.txtSparepartID.TabIndex = 0;
            // 
            // txtSparepartName
            // 
            this.txtSparepartName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSparepartName.LanguageKey = "SPAREPARTNAME";
            this.txtSparepartName.Location = new System.Drawing.Point(256, 0);
            this.txtSparepartName.Margin = new System.Windows.Forms.Padding(0);
            this.txtSparepartName.Name = "txtSparepartName";
            this.txtSparepartName.Size = new System.Drawing.Size(256, 20);
            this.txtSparepartName.TabIndex = 1;
            // 
            // btnFileRegister
            // 
            this.btnFileRegister.AllowFocus = false;
            this.btnFileRegister.IsBusy = false;
            this.btnFileRegister.IsWrite = false;
            this.btnFileRegister.LanguageKey = "REGISTATTACHMENT";
            this.btnFileRegister.Location = new System.Drawing.Point(569, 0);
            this.btnFileRegister.Margin = new System.Windows.Forms.Padding(0);
            this.btnFileRegister.Name = "btnFileRegister";
            this.btnFileRegister.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnFileRegister.Size = new System.Drawing.Size(80, 25);
            this.btnFileRegister.TabIndex = 2;
            this.btnFileRegister.Text = "smartButton1";
            this.btnFileRegister.TooltipLanguageKey = "";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnOK);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 405);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(736, 25);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.IsBusy = false;
            this.btnCancel.IsWrite = false;
            this.btnCancel.LanguageKey = "CANCEL";
            this.btnCancel.Location = new System.Drawing.Point(653, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "smartButton3";
            this.btnCancel.TooltipLanguageKey = "";
            // 
            // btnOK
            // 
            this.btnOK.AllowFocus = false;
            this.btnOK.IsBusy = false;
            this.btnOK.IsWrite = false;
            this.btnOK.LanguageKey = "OK";
            this.btnOK.Location = new System.Drawing.Point(570, 0);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "smartButton2";
            this.btnOK.TooltipLanguageKey = "";
            // 
            // picbox
            // 
            this.picbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picbox.Location = new System.Drawing.Point(3, 41);
            this.picbox.Name = "picbox";
            this.picbox.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picbox.Size = new System.Drawing.Size(730, 351);
            this.picbox.TabIndex = 3;
            // 
            // btnFileDel
            // 
            this.btnFileDel.AllowFocus = false;
            this.btnFileDel.IsBusy = false;
            this.btnFileDel.IsWrite = false;
            this.btnFileDel.LanguageKey = "FILEDELETE";
            this.btnFileDel.Location = new System.Drawing.Point(654, 0);
            this.btnFileDel.Margin = new System.Windows.Forms.Padding(0);
            this.btnFileDel.Name = "btnFileDel";
            this.btnFileDel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnFileDel.Size = new System.Drawing.Size(80, 25);
            this.btnFileDel.TabIndex = 3;
            this.btnFileDel.Text = "smartButton1";
            this.btnFileDel.TooltipLanguageKey = "";
            // 
            // SparepartImagePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 450);
            this.Name = "SparepartImagePopup";
            this.Text = "SparepartImagePopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSparepartID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSparepartName.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picbox.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Framework.SmartControls.SmartLabelTextBox txtSparepartID;
        private Framework.SmartControls.SmartLabelTextBox txtSparepartName;
        private Framework.SmartControls.SmartButton btnFileRegister;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnOK;
        private Framework.SmartControls.SmartButton btnCancel;
        private Framework.SmartControls.SmartPictureEdit picbox;
        private Framework.SmartControls.SmartButton btnFileDel;
    }
}