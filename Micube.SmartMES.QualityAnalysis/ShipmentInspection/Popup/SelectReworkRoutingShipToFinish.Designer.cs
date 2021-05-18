namespace Micube.SmartMES.QualityAnalysis.ShipmentInspection
{
    partial class SelectReworkRoutingShipToFinish
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
            this.lblInfo = new Micube.Framework.SmartControls.SmartLabel();
            this.cboReworkRouting = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.grdReworkPath = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdReworkResource = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboReworkRouting.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(630, 447);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblInfo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cboReworkRouting, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.grdReworkPath, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.grdReworkResource, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 2, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.90909F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(630, 447);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblInfo
            // 
            this.lblInfo.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Appearance.Options.UseFont = true;
            this.lblInfo.Appearance.Options.UseForeColor = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblInfo, 3);
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInfo.Location = new System.Drawing.Point(0, 0);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(630, 34);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "smartLabel1";
            // 
            // cboReworkRouting
            // 
            this.cboReworkRouting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboReworkRouting.LanguageKey = "REWORKROUTING";
            this.cboReworkRouting.Location = new System.Drawing.Point(0, 34);
            this.cboReworkRouting.Margin = new System.Windows.Forms.Padding(0);
            this.cboReworkRouting.Name = "cboReworkRouting";
            this.cboReworkRouting.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboReworkRouting.Properties.NullText = "";
            this.cboReworkRouting.Size = new System.Drawing.Size(310, 20);
            this.cboReworkRouting.TabIndex = 1;
            // 
            // grdReworkPath
            // 
            this.grdReworkPath.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdReworkPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReworkPath.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdReworkPath.IsUsePaging = false;
            this.grdReworkPath.LanguageKey = null;
            this.grdReworkPath.Location = new System.Drawing.Point(0, 64);
            this.grdReworkPath.Margin = new System.Windows.Forms.Padding(0);
            this.grdReworkPath.Name = "grdReworkPath";
            this.grdReworkPath.ShowBorder = true;
            this.grdReworkPath.ShowButtonBar = false;
            this.grdReworkPath.Size = new System.Drawing.Size(310, 347);
            this.grdReworkPath.TabIndex = 2;
            this.grdReworkPath.UseAutoBestFitColumns = false;
            // 
            // grdReworkResource
            // 
            this.grdReworkResource.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdReworkResource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReworkResource.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdReworkResource.IsUsePaging = false;
            this.grdReworkResource.LanguageKey = null;
            this.grdReworkResource.Location = new System.Drawing.Point(320, 64);
            this.grdReworkResource.Margin = new System.Windows.Forms.Padding(0);
            this.grdReworkResource.Name = "grdReworkResource";
            this.grdReworkResource.ShowBorder = true;
            this.grdReworkResource.ShowButtonBar = false;
            this.grdReworkResource.Size = new System.Drawing.Size(310, 347);
            this.grdReworkResource.TabIndex = 3;
            this.grdReworkResource.UseAutoBestFitColumns = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(320, 421);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(310, 26);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.IsBusy = false;
            this.btnCancel.IsWrite = false;
            this.btnCancel.LanguageKey = "CANCEL";
            this.btnCancel.Location = new System.Drawing.Point(230, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "smartButton1";
            this.btnCancel.TooltipLanguageKey = "";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(144, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "smartButton2";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // SelectReworkRoutingShipToFinish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 467);
            this.Name = "SelectReworkRoutingShipToFinish";
            this.Text = "SelectReworkRoutingShipToFinish";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboReworkRouting.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartLabel lblInfo;
        private Framework.SmartControls.SmartLabelComboBox cboReworkRouting;
        private Framework.SmartControls.SmartBandedGrid grdReworkPath;
        private Framework.SmartControls.SmartBandedGrid grdReworkResource;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnCancel;
        private Framework.SmartControls.SmartButton btnSave;
    }
}