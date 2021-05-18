namespace Micube.SmartMES.QualityAnalysis
{
    partial class ReliaVerifiResultNonRegular
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.grdQCReliabilityLot = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdReliabiVerifiReqRgistRegular = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnFlag = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnFlag);
            this.pnlToolbar.Controls.SetChildIndex(this.btnFlag, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tableLayoutPanel2);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.grdQCReliabilityLot, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.grdReliabiVerifiReqRgistRegular, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(475, 401);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // grdQCReliabilityLot
            // 
            this.grdQCReliabilityLot.Caption = "품목 리스트";
            this.grdQCReliabilityLot.CausesValidation = false;
            this.tableLayoutPanel2.SetColumnSpan(this.grdQCReliabilityLot, 2);
            this.grdQCReliabilityLot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdQCReliabilityLot.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdQCReliabilityLot.IsUsePaging = false;
            this.grdQCReliabilityLot.LanguageKey = "GRIDPRODUCTLIST";
            this.grdQCReliabilityLot.Location = new System.Drawing.Point(0, 200);
            this.grdQCReliabilityLot.Margin = new System.Windows.Forms.Padding(0);
            this.grdQCReliabilityLot.Name = "grdQCReliabilityLot";
            this.grdQCReliabilityLot.ShowBorder = true;
            this.grdQCReliabilityLot.ShowStatusBar = false;
            this.grdQCReliabilityLot.Size = new System.Drawing.Size(475, 201);
            this.grdQCReliabilityLot.TabIndex = 4;
            // 
            // grdReliabiVerifiReqRgistRegular
            // 
            this.grdReliabiVerifiReqRgistRegular.Caption = "신뢰성 검증 의뢰접수 현황";
            this.grdReliabiVerifiReqRgistRegular.CausesValidation = false;
            this.tableLayoutPanel2.SetColumnSpan(this.grdReliabiVerifiReqRgistRegular, 2);
            this.grdReliabiVerifiReqRgistRegular.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReliabiVerifiReqRgistRegular.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdReliabiVerifiReqRgistRegular.IsUsePaging = false;
            this.grdReliabiVerifiReqRgistRegular.LanguageKey = "RELIABVERIFIREREQSTATUS";
            this.grdReliabiVerifiReqRgistRegular.Location = new System.Drawing.Point(0, 0);
            this.grdReliabiVerifiReqRgistRegular.Margin = new System.Windows.Forms.Padding(0);
            this.grdReliabiVerifiReqRgistRegular.Name = "grdReliabiVerifiReqRgistRegular";
            this.grdReliabiVerifiReqRgistRegular.ShowBorder = true;
            this.grdReliabiVerifiReqRgistRegular.ShowStatusBar = false;
            this.grdReliabiVerifiReqRgistRegular.Size = new System.Drawing.Size(475, 200);
            this.grdReliabiVerifiReqRgistRegular.TabIndex = 2;
            // 
            // btnFlag
            // 
            this.btnFlag.AllowFocus = false;
            this.btnFlag.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFlag.IsBusy = false;
            this.btnFlag.IsWrite = true;
            this.btnFlag.Location = new System.Drawing.Point(47, 0);
            this.btnFlag.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnFlag.Name = "btnFlag";
            this.btnFlag.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnFlag.Size = new System.Drawing.Size(428, 24);
            this.btnFlag.TabIndex = 10;
            this.btnFlag.Text = "Flag";
            this.btnFlag.TooltipLanguageKey = "";
            this.btnFlag.Visible = false;
            // 
            // ReliaVerifiResultNonRegular
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "ReliaVerifiResultNonRegular";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Framework.SmartControls.SmartBandedGrid grdReliabiVerifiReqRgistRegular;
        private Framework.SmartControls.SmartBandedGrid grdQCReliabilityLot;
        public Framework.SmartControls.SmartButton btnFlag;
    }
}