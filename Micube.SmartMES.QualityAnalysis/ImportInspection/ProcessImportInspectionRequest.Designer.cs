namespace Micube.SmartMES.QualityAnalysis
{
    partial class ProcessImportInspectionRequest
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
            this.txtLotId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.grdInspectionRequest = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnCancelRequest = new Micube.Framework.SmartControls.SmartButton();
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
            this.pnlCondition.Size = new System.Drawing.Size(296, 443);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnCancelRequest);
            this.pnlToolbar.Size = new System.Drawing.Size(591, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.btnCancelRequest, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tableLayoutPanel1);
            this.pnlContent.Size = new System.Drawing.Size(591, 447);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(896, 476);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.59551F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.4045F));
            this.tableLayoutPanel1.Controls.Add(this.txtLotId, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grdInspectionRequest, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(591, 447);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtLotId
            // 
            this.txtLotId.AutoHeight = false;
            this.txtLotId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLotId.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txtLotId.LanguageKey = "LOTID";
            this.txtLotId.Location = new System.Drawing.Point(5, 0);
            this.txtLotId.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.txtLotId.Name = "txtLotId";
            this.txtLotId.Properties.AutoHeight = false;
            this.txtLotId.Size = new System.Drawing.Size(134, 20);
            this.txtLotId.TabIndex = 0;
            // 
            // grdInspectionRequest
            // 
            this.grdInspectionRequest.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.tableLayoutPanel1.SetColumnSpan(this.grdInspectionRequest, 2);
            this.grdInspectionRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspectionRequest.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInspectionRequest.IsUsePaging = false;
            this.grdInspectionRequest.LanguageKey = "REQUESTLIST";
            this.grdInspectionRequest.Location = new System.Drawing.Point(0, 30);
            this.grdInspectionRequest.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspectionRequest.Name = "grdInspectionRequest";
            this.grdInspectionRequest.ShowBorder = true;
            this.grdInspectionRequest.Size = new System.Drawing.Size(591, 417);
            this.grdInspectionRequest.TabIndex = 2;
            this.grdInspectionRequest.UseAutoBestFitColumns = false;
            // 
            // btnCancelRequest
            // 
            this.btnCancelRequest.AllowFocus = false;
            this.btnCancelRequest.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancelRequest.IsBusy = false;
            this.btnCancelRequest.IsWrite = false;
            this.btnCancelRequest.LanguageKey = "CANCELINSPREQUEST";
            this.btnCancelRequest.Location = new System.Drawing.Point(511, 0);
            this.btnCancelRequest.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCancelRequest.Name = "btnCancelRequest";
            this.btnCancelRequest.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancelRequest.Size = new System.Drawing.Size(80, 24);
            this.btnCancelRequest.TabIndex = 5;
            this.btnCancelRequest.Text = "smartButton1";
            this.btnCancelRequest.TooltipLanguageKey = "";
            this.btnCancelRequest.Visible = false;
            // 
            // ProcessImportInspectionRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 496);
            this.Name = "ProcessImportInspectionRequest";
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
        private Framework.SmartControls.SmartBandedGrid grdInspectionRequest;
        private Framework.SmartControls.SmartButton btnCancelRequest;
    }
}