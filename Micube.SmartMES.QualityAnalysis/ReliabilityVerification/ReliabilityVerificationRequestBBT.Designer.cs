namespace Micube.SmartMES.QualityAnalysis
{
    partial class ReliabilityVerificationRequestBBT
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.grdReliabiVerifiReqRgistRegular = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtLotId2 = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRecieve1 = new Micube.Framework.SmartControls.SmartButton();
            this.btnFlag = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId2.Properties)).BeginInit();
            this.flowLayoutPanel3.SuspendLayout();
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
            this.tableLayoutPanel2.Controls.Add(this.grdReliabiVerifiReqRgistRegular, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(475, 401);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // grdReliabiVerifiReqRgistRegular
            // 
            this.grdReliabiVerifiReqRgistRegular.Caption = "BBT 신뢰성 의뢰접수 현황";
            this.grdReliabiVerifiReqRgistRegular.CausesValidation = false;
            this.tableLayoutPanel2.SetColumnSpan(this.grdReliabiVerifiReqRgistRegular, 2);
            this.grdReliabiVerifiReqRgistRegular.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReliabiVerifiReqRgistRegular.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdReliabiVerifiReqRgistRegular.IsUsePaging = false;
            this.grdReliabiVerifiReqRgistRegular.LanguageKey = "RELIABVERIFIREREQBBTSTATUS";
            this.grdReliabiVerifiReqRgistRegular.Location = new System.Drawing.Point(0, 35);
            this.grdReliabiVerifiReqRgistRegular.Margin = new System.Windows.Forms.Padding(0);
            this.grdReliabiVerifiReqRgistRegular.Name = "grdReliabiVerifiReqRgistRegular";
            this.grdReliabiVerifiReqRgistRegular.ShowBorder = true;
            this.grdReliabiVerifiReqRgistRegular.ShowStatusBar = false;
            this.grdReliabiVerifiReqRgistRegular.Size = new System.Drawing.Size(475, 366);
            this.grdReliabiVerifiReqRgistRegular.TabIndex = 2;
            this.grdReliabiVerifiReqRgistRegular.UseAutoBestFitColumns = false;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.txtLotId2);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(231, 29);
            this.flowLayoutPanel4.TabIndex = 0;
            // 
            // txtLotId2
            // 
            this.txtLotId2.EditorWidth = "80%";
            this.txtLotId2.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txtLotId2.LabelText = "Lot No";
            this.txtLotId2.LabelWidth = "30%";
            this.txtLotId2.LanguageKey = "LOTID";
            this.txtLotId2.Location = new System.Drawing.Point(3, 3);
            this.txtLotId2.Name = "txtLotId2";
            this.txtLotId2.Size = new System.Drawing.Size(220, 20);
            this.txtLotId2.TabIndex = 2;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.btnRecieve1);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(240, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(232, 29);
            this.flowLayoutPanel3.TabIndex = 3;
            // 
            // btnRecieve1
            // 
            this.btnRecieve1.AllowFocus = false;
            this.btnRecieve1.IsBusy = false;
            this.btnRecieve1.IsWrite = false;
            this.btnRecieve1.LanguageKey = "SAVE";
            this.btnRecieve1.Location = new System.Drawing.Point(149, 0);
            this.btnRecieve1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnRecieve1.Name = "btnRecieve1";
            this.btnRecieve1.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnRecieve1.Size = new System.Drawing.Size(80, 25);
            this.btnRecieve1.TabIndex = 6;
            this.btnRecieve1.Text = "저장";
            this.btnRecieve1.TooltipLanguageKey = "";
            this.btnRecieve1.Visible = false;
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
            // ReliabilityVerificationRequestBBT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "ReliabilityVerificationRequestBBT";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId2.Properties)).EndInit();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Framework.SmartControls.SmartBandedGrid grdReliabiVerifiReqRgistRegular;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private Framework.SmartControls.SmartButton btnRecieve1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private Framework.SmartControls.SmartLabelTextBox txtLotId2;
        private Framework.SmartControls.SmartButton btnFlag;
    }
}