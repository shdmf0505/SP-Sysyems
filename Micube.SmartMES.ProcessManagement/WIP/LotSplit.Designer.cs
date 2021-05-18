namespace Micube.SmartMES.ProcessManagement
{
    partial class LotSplit
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
            this.grdWIP = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdTarget = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cboUOM = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.grdLotInfo = new Micube.SmartMES.Commons.Controls.SmartLotInfoGrid();
            this.spcSpliter = new Micube.Framework.SmartControls.SmartSpliterControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboUOM.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 546);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(756, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.panel1);
            this.pnlContent.Controls.Add(this.spcSpliter);
            this.pnlContent.Controls.Add(this.grdWIP);
            this.pnlContent.Size = new System.Drawing.Size(756, 550);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1061, 579);
            // 
            // grdWIP
            // 
            this.grdWIP.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdWIP.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdWIP.IsUsePaging = false;
            this.grdWIP.LanguageKey = "WIPLIST";
            this.grdWIP.Location = new System.Drawing.Point(0, 0);
            this.grdWIP.Margin = new System.Windows.Forms.Padding(0);
            this.grdWIP.Name = "grdWIP";
            this.grdWIP.ShowBorder = true;
            this.grdWIP.Size = new System.Drawing.Size(756, 216);
            this.grdWIP.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdTarget);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.grdLotInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 221);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(756, 329);
            this.panel1.TabIndex = 4;
            // 
            // grdTarget
            // 
            this.grdTarget.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTarget.IsUsePaging = false;
            this.grdTarget.LanguageKey = "TARGETSPLIT";
            this.grdTarget.Location = new System.Drawing.Point(0, 181);
            this.grdTarget.Margin = new System.Windows.Forms.Padding(0);
            this.grdTarget.Name = "grdTarget";
            this.grdTarget.ShowBorder = true;
            this.grdTarget.ShowStatusBar = false;
            this.grdTarget.Size = new System.Drawing.Size(756, 148);
            this.grdTarget.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cboUOM);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 151);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(756, 30);
            this.panel2.TabIndex = 4;
            // 
            // cboUOM
            // 
            this.cboUOM.LabelText = "UOM";
            this.cboUOM.LanguageKey = null;
            this.cboUOM.Location = new System.Drawing.Point(5, 5);
            this.cboUOM.Name = "cboUOM";
            this.cboUOM.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUOM.Properties.NullText = "";
            this.cboUOM.Size = new System.Drawing.Size(220, 20);
            this.cboUOM.TabIndex = 0;
            // 
            // grdLotInfo
            // 
            this.grdLotInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdLotInfo.Location = new System.Drawing.Point(0, 0);
            this.grdLotInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotInfo.Name = "grdLotInfo";
            this.grdLotInfo.Size = new System.Drawing.Size(756, 151);
            this.grdLotInfo.TabIndex = 3;
            // 
            // spcSpliter
            // 
            this.spcSpliter.Dock = System.Windows.Forms.DockStyle.Top;
            this.spcSpliter.Location = new System.Drawing.Point(0, 216);
            this.spcSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcSpliter.Name = "spcSpliter";
            this.spcSpliter.Size = new System.Drawing.Size(756, 5);
            this.spcSpliter.TabIndex = 7;
            this.spcSpliter.TabStop = false;
            // 
            // LotSplit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 599);
            this.LanguageKey = "";
            this.Name = "LotSplit";
            this.Text = "Lot Split";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboUOM.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdWIP;
        private System.Windows.Forms.Panel panel1;
        private Framework.SmartControls.SmartBandedGrid grdTarget;
        private Commons.Controls.SmartLotInfoGrid grdLotInfo;
        private Framework.SmartControls.SmartSpliterControl spcSpliter;
        private System.Windows.Forms.Panel panel2;
        private Framework.SmartControls.SmartLabelComboBox cboUOM;
    }
}