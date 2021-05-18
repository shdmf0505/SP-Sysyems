namespace Micube.SmartMES.QualityAnalysis
{
    partial class ProcessImportInspectionResult
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grdProcessInspectionResult = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.txtLotId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.btnPopupFlag = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnPopupFlag);
            this.pnlToolbar.Controls.SetChildIndex(this.btnPopupFlag, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tableLayoutPanel1);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.6F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.4F));
            this.tableLayoutPanel1.Controls.Add(this.grdProcessInspectionResult, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtLotId, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(475, 401);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // grdProcessInspectionResult
            // 
            this.grdProcessInspectionResult.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.tableLayoutPanel1.SetColumnSpan(this.grdProcessInspectionResult, 2);
            this.grdProcessInspectionResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProcessInspectionResult.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdProcessInspectionResult.IsUsePaging = false;
            this.grdProcessInspectionResult.LanguageKey = "OSPIMPORTINSPRESULTLIST";
            this.grdProcessInspectionResult.Location = new System.Drawing.Point(0, 30);
            this.grdProcessInspectionResult.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.grdProcessInspectionResult.Name = "grdProcessInspectionResult";
            this.grdProcessInspectionResult.ShowBorder = true;
            this.grdProcessInspectionResult.Size = new System.Drawing.Size(475, 371);
            this.grdProcessInspectionResult.TabIndex = 1;
            this.grdProcessInspectionResult.UseAutoBestFitColumns = false;
            // 
            // txtLotId
            // 
            this.txtLotId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLotId.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txtLotId.LanguageKey = "LOTID";
            this.txtLotId.Location = new System.Drawing.Point(0, 0);
            this.txtLotId.Margin = new System.Windows.Forms.Padding(0);
            this.txtLotId.Name = "txtLotId";
            this.txtLotId.Properties.AutoHeight = false;
            this.txtLotId.Size = new System.Drawing.Size(112, 20);
            this.txtLotId.TabIndex = 0;
            // 
            // btnPopupFlag
            // 
            this.btnPopupFlag.AllowFocus = false;
            this.btnPopupFlag.IsBusy = false;
            this.btnPopupFlag.IsWrite = true;
            this.btnPopupFlag.Location = new System.Drawing.Point(395, 0);
            this.btnPopupFlag.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPopupFlag.Name = "btnPopupFlag";
            this.btnPopupFlag.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPopupFlag.Size = new System.Drawing.Size(80, 25);
            this.btnPopupFlag.TabIndex = 8;
            this.btnPopupFlag.Text = "smartButton1";
            this.btnPopupFlag.TooltipLanguageKey = "";
            this.btnPopupFlag.Visible = false;
            // 
            // ProcessImportInspectionResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "ProcessImportInspectionResult";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartLabelTextBox txtLotId;
        private Framework.SmartControls.SmartBandedGrid grdProcessInspectionResult;
        private Framework.SmartControls.SmartButton btnPopupFlag;
    }
}