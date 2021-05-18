namespace Micube.SmartMES.ProcessManagement
{
    partial class ConsumeSNMapping
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
            this.grdWip = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.spcSpliter = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdTarget = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.lblComment = new Micube.Framework.SmartControls.SmartLabel();
            this.txtComment = new DevExpress.XtraEditors.MemoEdit();
            this.txtSalseOrderNo = new Micube.Framework.SmartControls.SmartLabelSelectPopupEdit();
            this.txtProduct = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.cboProductRevision = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ucUpDown = new Micube.SmartMES.Commons.Controls.ucDataUpDownBtn();
            this.chkPrintLotCard = new Micube.Framework.SmartControls.SmartCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalseOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProduct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProductRevision.Properties)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkPrintLotCard.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 561);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(814, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdWip);
            this.pnlContent.Controls.Add(this.spcSpliter);
            this.pnlContent.Controls.Add(this.panel1);
            this.pnlContent.Size = new System.Drawing.Size(814, 565);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1119, 594);
            // 
            // grdWip
            // 
            this.grdWip.Caption = "";
            this.grdWip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdWip.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.grdWip.IsUsePaging = false;
            this.grdWip.LanguageKey = "";
            this.grdWip.Location = new System.Drawing.Point(0, 0);
            this.grdWip.Margin = new System.Windows.Forms.Padding(0);
            this.grdWip.Name = "grdWip";
            this.grdWip.ShowBorder = true;
            this.grdWip.Size = new System.Drawing.Size(814, 232);
            this.grdWip.TabIndex = 6;
            // 
            // spcSpliter
            // 
            this.spcSpliter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.spcSpliter.Location = new System.Drawing.Point(0, 232);
            this.spcSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcSpliter.Name = "spcSpliter";
            this.spcSpliter.Size = new System.Drawing.Size(814, 5);
            this.spcSpliter.TabIndex = 8;
            this.spcSpliter.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdTarget);
            this.panel1.Controls.Add(this.smartSpliterControl1);
            this.panel1.Controls.Add(this.smartGroupBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 237);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(814, 328);
            this.panel1.TabIndex = 7;
            // 
            // grdTarget
            // 
            this.grdTarget.Caption = "";
            this.grdTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTarget.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdTarget.IsUsePaging = false;
            this.grdTarget.LanguageKey = "";
            this.grdTarget.Location = new System.Drawing.Point(0, 45);
            this.grdTarget.Margin = new System.Windows.Forms.Padding(0);
            this.grdTarget.Name = "grdTarget";
            this.grdTarget.ShowBorder = true;
            this.grdTarget.Size = new System.Drawing.Size(364, 283);
            this.grdTarget.TabIndex = 2;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.smartSpliterControl1.Location = new System.Drawing.Point(364, 45);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(5, 283);
            this.smartSpliterControl1.TabIndex = 6;
            this.smartSpliterControl1.TabStop = false;
            // 
            // smartGroupBox1
            // 
            this.smartGroupBox1.Controls.Add(this.chkPrintLotCard);
            this.smartGroupBox1.Controls.Add(this.lblComment);
            this.smartGroupBox1.Controls.Add(this.txtComment);
            this.smartGroupBox1.Controls.Add(this.txtSalseOrderNo);
            this.smartGroupBox1.Controls.Add(this.txtProduct);
            this.smartGroupBox1.Controls.Add(this.cboProductRevision);
            this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox1.LanguageKey = "CHANGESEGMENT";
            this.smartGroupBox1.Location = new System.Drawing.Point(369, 45);
            this.smartGroupBox1.Name = "smartGroupBox1";
            this.smartGroupBox1.ShowBorder = true;
            this.smartGroupBox1.Size = new System.Drawing.Size(445, 283);
            this.smartGroupBox1.TabIndex = 0;
            this.smartGroupBox1.Text = "공정변경";
            // 
            // lblComment
            // 
            this.lblComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblComment.LanguageKey = "SPECIALNOTE";
            this.lblComment.Location = new System.Drawing.Point(7, 92);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(40, 14);
            this.lblComment.TabIndex = 10;
            this.lblComment.Text = "특이사항";
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.Location = new System.Drawing.Point(5, 112);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(434, 166);
            this.txtComment.TabIndex = 11;
            // 
            // txtSalseOrderNo
            // 
            this.txtSalseOrderNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSalseOrderNo.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtSalseOrderNo.Appearance.Options.UseForeColor = true;
            this.txtSalseOrderNo.LabelWidth = "20%";
            this.txtSalseOrderNo.LanguageKey = "SALSEORDERNO";
            this.txtSalseOrderNo.Location = new System.Drawing.Point(5, 62);
            this.txtSalseOrderNo.Name = "txtSalseOrderNo";
            this.txtSalseOrderNo.Size = new System.Drawing.Size(435, 20);
            this.txtSalseOrderNo.TabIndex = 9;
            // 
            // txtProduct
            // 
            this.txtProduct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProduct.LabelWidth = "20%";
            this.txtProduct.LanguageKey = "PRODUCTDEFNAME";
            this.txtProduct.Location = new System.Drawing.Point(5, 36);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Properties.ReadOnly = true;
            this.txtProduct.Size = new System.Drawing.Size(435, 20);
            this.txtProduct.TabIndex = 8;
            // 
            // cboProductRevision
            // 
            this.cboProductRevision.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboProductRevision.Appearance.ForeColor = System.Drawing.Color.Red;
            this.cboProductRevision.Appearance.Options.UseForeColor = true;
            this.cboProductRevision.LabelText = "";
            this.cboProductRevision.LabelWidth = "20%";
            this.cboProductRevision.LanguageKey = "PRODUCTDEFVERSION";
            this.cboProductRevision.Location = new System.Drawing.Point(293, 88);
            this.cboProductRevision.Name = "cboProductRevision";
            this.cboProductRevision.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProductRevision.Properties.NullText = "";
            this.cboProductRevision.Size = new System.Drawing.Size(146, 20);
            this.cboProductRevision.TabIndex = 7;
            this.cboProductRevision.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ucUpDown);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(814, 45);
            this.panel2.TabIndex = 0;
            // 
            // ucUpDown
            // 
            this.ucUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucUpDown.Location = new System.Drawing.Point(0, 0);
            this.ucUpDown.Name = "ucUpDown";
            this.ucUpDown.Size = new System.Drawing.Size(814, 45);
            this.ucUpDown.SourceGrid = null;
            this.ucUpDown.TabIndex = 0;
            this.ucUpDown.TargetGrid = null;
            // 
            // chkPrintLotCard
            // 
            this.chkPrintLotCard.EditValue = true;
            this.chkPrintLotCard.Location = new System.Drawing.Point(121, 88);
            this.chkPrintLotCard.Name = "chkPrintLotCard";
            this.chkPrintLotCard.Properties.Caption = "LOT CARD 출력";
            this.chkPrintLotCard.Size = new System.Drawing.Size(136, 19);
            this.chkPrintLotCard.TabIndex = 12;
            // 
            // ConsumeSNMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 614);
            this.Name = "ConsumeSNMapping";
            this.Text = "LotCreateSheet";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            this.smartGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalseOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProduct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProductRevision.Properties)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkPrintLotCard.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdWip;
        private Framework.SmartControls.SmartSpliterControl spcSpliter;
        private System.Windows.Forms.Panel panel1;
        private Framework.SmartControls.SmartBandedGrid grdTarget;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private Framework.SmartControls.SmartLabelComboBox cboProductRevision;
        private System.Windows.Forms.Panel panel2;
        private Commons.Controls.ucDataUpDownBtn ucUpDown;
        private Framework.SmartControls.SmartLabelTextBox txtProduct;
        private Framework.SmartControls.SmartLabelSelectPopupEdit txtSalseOrderNo;
        private DevExpress.XtraEditors.MemoEdit txtComment;
        private Framework.SmartControls.SmartLabel lblComment;
        private Framework.SmartControls.SmartCheckBox chkPrintLotCard;
    }
}