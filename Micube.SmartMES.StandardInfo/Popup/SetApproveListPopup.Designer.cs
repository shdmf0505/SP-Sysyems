namespace Micube.SmartMES.StandardInfo
{
    partial class SetApproveListPopup
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
			this.grdApproveList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.pnlButton = new System.Windows.Forms.FlowLayoutPanel();
			this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
			this.btnOK = new Micube.Framework.SmartControls.SmartButton();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.pnlMain.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.pnlButton.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.tableLayoutPanel1);
			this.pnlMain.Size = new System.Drawing.Size(956, 430);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.grdApproveList, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.pnlButton, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(956, 430);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// grdApproveList
			// 
			this.grdApproveList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdApproveList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdApproveList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
			this.grdApproveList.IsUsePaging = false;
			this.grdApproveList.LanguageKey = "APPROVALLINEASSIGNMENT";
			this.grdApproveList.Location = new System.Drawing.Point(0, 0);
			this.grdApproveList.Margin = new System.Windows.Forms.Padding(0);
			this.grdApproveList.Name = "grdApproveList";
			this.grdApproveList.ShowBorder = true;
			this.grdApproveList.Size = new System.Drawing.Size(956, 397);
			this.grdApproveList.TabIndex = 1;
			// 
			// pnlButton
			// 
			this.pnlButton.AutoSize = true;
			this.pnlButton.Controls.Add(this.btnCancel);
			this.pnlButton.Controls.Add(this.btnOK);
			this.pnlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlButton.Location = new System.Drawing.Point(0, 407);
			this.pnlButton.Margin = new System.Windows.Forms.Padding(0);
			this.pnlButton.Name = "pnlButton";
			this.pnlButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.pnlButton.Size = new System.Drawing.Size(956, 23);
			this.pnlButton.TabIndex = 2;
			// 
			// btnCancel
			// 
			this.btnCancel.AllowFocus = false;
			this.btnCancel.IsBusy = false;
			this.btnCancel.IsWrite = false;
			this.btnCancel.LanguageKey = "CANCEL";
			this.btnCancel.Location = new System.Drawing.Point(878, 0);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "취소";
			this.btnCancel.TooltipLanguageKey = "";
			// 
			// btnOK
			// 
			this.btnOK.AllowFocus = false;
			this.btnOK.IsBusy = false;
			this.btnOK.IsWrite = false;
			this.btnOK.LanguageKey = "OK";
			this.btnOK.Location = new System.Drawing.Point(800, 0);
			this.btnOK.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.btnOK.Name = "btnOK";
			this.btnOK.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "결재요청";
			this.btnOK.TooltipLanguageKey = "";
			// 
			// SetApproveListPopup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(976, 450);
			this.LanguageKey = "APPROVALLINEASSIGNMENT";
			this.Name = "SetApproveListPopup";
			this.Text = "";
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.pnlMain.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.pnlButton.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartBandedGrid grdApproveList;
        private System.Windows.Forms.FlowLayoutPanel pnlButton;
        private Framework.SmartControls.SmartButton btnOK;
        private Framework.SmartControls.SmartButton btnCancel;
    }
}