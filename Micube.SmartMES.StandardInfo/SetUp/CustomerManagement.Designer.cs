namespace Micube.SmartMES.StandardInfo
{
	partial class CustomerManagement
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
            this.components = new System.ComponentModel.Container();
            this.grdCustomer = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnImport = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 373);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnImport);
            this.pnlToolbar.Size = new System.Drawing.Size(381, 30);
            this.pnlToolbar.Controls.SetChildIndex(this.btnImport, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdCustomer);
            this.pnlContent.Size = new System.Drawing.Size(381, 376);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(762, 412);
            // 
            // grdCustomer
            // 
            this.grdCustomer.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCustomer.IsUsePaging = false;
            this.grdCustomer.LanguageKey = "CUSTOMERLIST";
            this.grdCustomer.Location = new System.Drawing.Point(0, 0);
            this.grdCustomer.Margin = new System.Windows.Forms.Padding(0);
            this.grdCustomer.Name = "grdCustomer";
            this.grdCustomer.ShowBorder = true;
            this.grdCustomer.Size = new System.Drawing.Size(381, 376);
            this.grdCustomer.TabIndex = 0;
            // 
            // btnImport
            // 
            this.btnImport.AllowFocus = false;
            this.btnImport.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnImport.IsBusy = false;
            this.btnImport.LanguageKey = "IMPORT";
            this.btnImport.Location = new System.Drawing.Point(301, 0);
            this.btnImport.Margin = new System.Windows.Forms.Padding(0);
            this.btnImport.Name = "btnImport";
            this.btnImport.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnImport.Size = new System.Drawing.Size(80, 30);
            this.btnImport.TabIndex = 7;
            this.btnImport.Text = "smartButton1";
            this.btnImport.TooltipLanguageKey = "";
            this.btnImport.Visible = false;
            // 
            // CustomerManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "CustomerManagement";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "CustomerManagement";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion
        private Framework.SmartControls.SmartBandedGrid grdCustomer;
        private Framework.SmartControls.SmartButton btnImport;
    }
}