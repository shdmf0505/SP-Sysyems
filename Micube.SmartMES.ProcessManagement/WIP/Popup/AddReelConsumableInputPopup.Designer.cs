namespace Micube.SmartMES.ProcessManagement
{
	partial class AddReelConsumableInputPopup
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.btnApply = new Micube.Framework.SmartControls.SmartButton();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.grdReelConsumable = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.txtReelConsumable = new Micube.Framework.SmartControls.SmartTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReelConsumable.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(515, 423);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnApply);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 397);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(512, 23);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.IsBusy = false;
            this.btnCancel.IsWrite = false;
            this.btnCancel.LanguageKey = "CANCEL";
            this.btnCancel.Location = new System.Drawing.Point(437, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TooltipLanguageKey = "";
            // 
            // btnApply
            // 
            this.btnApply.AllowFocus = false;
            this.btnApply.IsBusy = false;
            this.btnApply.IsWrite = false;
            this.btnApply.LanguageKey = "APPLY";
            this.btnApply.Location = new System.Drawing.Point(356, 0);
            this.btnApply.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply";
            this.btnApply.TooltipLanguageKey = "";
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(515, 423);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.smartLabel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grdReelConsumable, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtReelConsumable, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(515, 394);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // smartLabel1
            // 
            this.smartLabel1.Appearance.BackColor = System.Drawing.Color.Yellow;
            this.smartLabel1.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smartLabel1.Appearance.Options.UseBackColor = true;
            this.smartLabel1.Appearance.Options.UseFont = true;
            this.smartLabel1.Appearance.Options.UseTextOptions = true;
            this.smartLabel1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.smartLabel1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.smartLabel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tableLayoutPanel1.SetColumnSpan(this.smartLabel1, 2);
            this.smartLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLabel1.Location = new System.Drawing.Point(3, 3);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(509, 39);
            this.smartLabel1.TabIndex = 0;
            this.smartLabel1.Text = "Reel 자재 입력";
            // 
            // grdReelConsumable
            // 
            this.grdReelConsumable.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.tableLayoutPanel1.SetColumnSpan(this.grdReelConsumable, 2);
            this.grdReelConsumable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReelConsumable.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdReelConsumable.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdReelConsumable.IsUsePaging = false;
            this.grdReelConsumable.LanguageKey = null;
            this.grdReelConsumable.Location = new System.Drawing.Point(0, 90);
            this.grdReelConsumable.Margin = new System.Windows.Forms.Padding(0);
            this.grdReelConsumable.Name = "grdReelConsumable";
            this.grdReelConsumable.ShowBorder = true;
            this.grdReelConsumable.ShowButtonBar = false;
            this.grdReelConsumable.ShowStatusBar = false;
            this.grdReelConsumable.Size = new System.Drawing.Size(515, 304);
            this.grdReelConsumable.TabIndex = 1;
            this.grdReelConsumable.UseAutoBestFitColumns = false;
            // 
            // txtReelConsumable
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtReelConsumable, 2);
            this.txtReelConsumable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReelConsumable.LabelText = null;
            this.txtReelConsumable.LanguageKey = null;
            this.txtReelConsumable.Location = new System.Drawing.Point(0, 55);
            this.txtReelConsumable.Margin = new System.Windows.Forms.Padding(0);
            this.txtReelConsumable.Name = "txtReelConsumable";
            this.txtReelConsumable.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReelConsumable.Properties.Appearance.Options.UseFont = true;
            this.txtReelConsumable.Properties.AutoHeight = false;
            this.txtReelConsumable.Size = new System.Drawing.Size(515, 30);
            this.txtReelConsumable.TabIndex = 2;
            // 
            // AddReelConsumableInputPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 443);
            this.LanguageKey = "ADDREELCONSUMABLE";
            this.Name = "AddReelConsumableInputPopup";
            this.Text = "Reel 자재 입력";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReelConsumable.Properties)).EndInit();
            this.ResumeLayout(false);

		}

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnCancel;
        private Framework.SmartControls.SmartButton btnApply;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartBandedGrid grdReelConsumable;
        private Framework.SmartControls.SmartTextBox txtReelConsumable;
    }
}