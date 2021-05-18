namespace Micube.SmartMES.ProcessManagement
{
    partial class LotCreateReturn
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
            this.grdLotsToCreate = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdProcessPath = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cboReason = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.chkPrintLotCard = new Micube.Framework.SmartControls.SmartCheckBox();
            this.txtQty = new Micube.Framework.SmartControls.SmartTextBox();
            this.cboArea = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.lblQty = new Micube.Framework.SmartControls.SmartLabel();
            this.cboRouting = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.txtSpecialNote = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblSpecialNote = new Micube.Framework.SmartControls.SmartLabel();
            this.txtCSManager = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.grdReturnLots = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.smartSpliterControl2 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.ucDataUpDownBtnCtrl = new Micube.SmartMES.Commons.Controls.ucDataUpDownBtn();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboReason.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPrintLotCard.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboRouting.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpecialNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCSManager.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 589);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(950, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdReturnLots);
            this.pnlContent.Controls.Add(this.ucDataUpDownBtnCtrl);
            this.pnlContent.Controls.Add(this.smartSpliterControl2);
            this.pnlContent.Controls.Add(this.grdLotsToCreate);
            this.pnlContent.Controls.Add(this.smartGroupBox1);
            this.pnlContent.Controls.Add(this.smartSpliterControl1);
            this.pnlContent.Controls.Add(this.grdProcessPath);
            this.pnlContent.Size = new System.Drawing.Size(950, 593);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1255, 622);
            // 
            // grdLotsToCreate
            // 
            this.grdLotsToCreate.Caption = "생성 대상 LOT";
            this.grdLotsToCreate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdLotsToCreate.IsUsePaging = false;
            this.grdLotsToCreate.LanguageKey = "LOTSTOCREATE";
            this.grdLotsToCreate.Location = new System.Drawing.Point(0, 154);
            this.grdLotsToCreate.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotsToCreate.Name = "grdLotsToCreate";
            this.grdLotsToCreate.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.grdLotsToCreate.ShowBorder = true;
            this.grdLotsToCreate.Size = new System.Drawing.Size(950, 162);
            this.grdLotsToCreate.TabIndex = 4;
            this.grdLotsToCreate.UseAutoBestFitColumns = false;
            // 
            // grdProcessPath
            // 
            this.grdProcessPath.Caption = "작업공정";
            this.grdProcessPath.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdProcessPath.IsUsePaging = false;
            this.grdProcessPath.LanguageKey = "WORKPROCESSSEGMENT";
            this.grdProcessPath.Location = new System.Drawing.Point(0, 385);
            this.grdProcessPath.Margin = new System.Windows.Forms.Padding(0);
            this.grdProcessPath.Name = "grdProcessPath";
            this.grdProcessPath.ShowBorder = true;
            this.grdProcessPath.Size = new System.Drawing.Size(950, 208);
            this.grdProcessPath.TabIndex = 5;
            this.grdProcessPath.UseAutoBestFitColumns = false;
            // 
            // smartGroupBox1
            // 
            this.smartGroupBox1.Controls.Add(this.tableLayoutPanel1);
            this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox1.Location = new System.Drawing.Point(0, 316);
            this.smartGroupBox1.Name = "smartGroupBox1";
            this.smartGroupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.smartGroupBox1.ShowBorder = true;
            this.smartGroupBox1.ShowCaption = false;
            this.smartGroupBox1.Size = new System.Drawing.Size(950, 64);
            this.smartGroupBox1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.cboReason, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.chkPrintLotCard, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtQty, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cboArea, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblQty, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cboRouting, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtSpecialNote, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblSpecialNote, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtCSManager, 5, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(942, 56);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // cboReason
            // 
            this.cboReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboReason.EditorWidth = "70%";
            this.cboReason.LabelHAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.cboReason.LabelText = "반품사유";
            this.cboReason.LabelWidth = "30%";
            this.cboReason.LanguageKey = "RETURNREASON";
            this.cboReason.Location = new System.Drawing.Point(526, 31);
            this.cboReason.Name = "cboReason";
            this.cboReason.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboReason.Properties.NullText = "";
            this.cboReason.Size = new System.Drawing.Size(203, 20);
            this.cboReason.TabIndex = 18;
            // 
            // chkPrintLotCard
            // 
            this.chkPrintLotCard.EditValue = true;
            this.chkPrintLotCard.Location = new System.Drawing.Point(762, 3);
            this.chkPrintLotCard.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.chkPrintLotCard.Name = "chkPrintLotCard";
            this.chkPrintLotCard.Properties.Caption = "LOT Card 출력";
            this.chkPrintLotCard.Size = new System.Drawing.Size(124, 19);
            this.chkPrintLotCard.TabIndex = 7;
            // 
            // txtQty
            // 
            this.txtQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQty.LabelText = null;
            this.txtQty.LanguageKey = null;
            this.txtQty.Location = new System.Drawing.Point(108, 3);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(203, 20);
            this.txtQty.TabIndex = 17;
            // 
            // cboArea
            // 
            this.cboArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboArea.EditorWidth = "70%";
            this.cboArea.LabelHAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.cboArea.LabelText = "자원";
            this.cboArea.LabelWidth = "30%";
            this.cboArea.LanguageKey = "RESOURCE";
            this.cboArea.Location = new System.Drawing.Point(526, 3);
            this.cboArea.Name = "cboArea";
            this.cboArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboArea.Properties.NullText = "";
            this.cboArea.Size = new System.Drawing.Size(203, 20);
            this.cboArea.TabIndex = 5;
            // 
            // lblQty
            // 
            this.lblQty.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblQty.LanguageKey = "QTYLABEL";
            this.lblQty.Location = new System.Drawing.Point(77, 3);
            this.lblQty.Name = "lblQty";
            this.lblQty.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.lblQty.Size = new System.Drawing.Size(25, 14);
            this.lblQty.TabIndex = 16;
            this.lblQty.Text = "수량";
            // 
            // cboRouting
            // 
            this.cboRouting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboRouting.EditorWidth = "70%";
            this.cboRouting.LabelHAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.cboRouting.LabelText = "라우팅";
            this.cboRouting.LabelWidth = "30%";
            this.cboRouting.LanguageKey = "ROUTE";
            this.cboRouting.Location = new System.Drawing.Point(317, 3);
            this.cboRouting.Name = "cboRouting";
            this.cboRouting.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboRouting.Properties.NullText = "";
            this.cboRouting.Size = new System.Drawing.Size(203, 20);
            this.cboRouting.TabIndex = 5;
            // 
            // txtSpecialNote
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtSpecialNote, 2);
            this.txtSpecialNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSpecialNote.LabelText = null;
            this.txtSpecialNote.LanguageKey = null;
            this.txtSpecialNote.Location = new System.Drawing.Point(108, 31);
            this.txtSpecialNote.Name = "txtSpecialNote";
            this.txtSpecialNote.Size = new System.Drawing.Size(412, 20);
            this.txtSpecialNote.TabIndex = 15;
            // 
            // lblSpecialNote
            // 
            this.lblSpecialNote.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblSpecialNote.LanguageKey = "SPECIALNOTE";
            this.lblSpecialNote.Location = new System.Drawing.Point(57, 31);
            this.lblSpecialNote.Name = "lblSpecialNote";
            this.lblSpecialNote.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.lblSpecialNote.Size = new System.Drawing.Size(45, 14);
            this.lblSpecialNote.TabIndex = 0;
            this.lblSpecialNote.Text = "특이사항";
            // 
            // txtCSManager
            // 
            this.txtCSManager.LabelHAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtCSManager.LabelText = "CS 담당자";
            this.txtCSManager.LanguageKey = "CSMANAGER";
            this.txtCSManager.Location = new System.Drawing.Point(735, 31);
            this.txtCSManager.Name = "txtCSManager";
            this.txtCSManager.Size = new System.Drawing.Size(204, 20);
            this.txtCSManager.TabIndex = 19;
            // 
            // grdReturnLots
            // 
            this.grdReturnLots.Caption = "반품 LOT";
            this.grdReturnLots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReturnLots.IsUsePaging = false;
            this.grdReturnLots.LanguageKey = "RETURNLOTS";
            this.grdReturnLots.Location = new System.Drawing.Point(0, 0);
            this.grdReturnLots.Margin = new System.Windows.Forms.Padding(0);
            this.grdReturnLots.Name = "grdReturnLots";
            this.grdReturnLots.ShowBorder = true;
            this.grdReturnLots.Size = new System.Drawing.Size(950, 97);
            this.grdReturnLots.TabIndex = 6;
            this.grdReturnLots.UseAutoBestFitColumns = false;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.smartSpliterControl1.Location = new System.Drawing.Point(0, 380);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(950, 5);
            this.smartSpliterControl1.TabIndex = 13;
            this.smartSpliterControl1.TabStop = false;
            // 
            // smartSpliterControl2
            // 
            this.smartSpliterControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.smartSpliterControl2.Location = new System.Drawing.Point(0, 149);
            this.smartSpliterControl2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl2.Name = "smartSpliterControl2";
            this.smartSpliterControl2.Size = new System.Drawing.Size(950, 5);
            this.smartSpliterControl2.TabIndex = 14;
            this.smartSpliterControl2.TabStop = false;
            // 
            // ucDataUpDownBtnCtrl
            // 
            this.ucDataUpDownBtnCtrl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucDataUpDownBtnCtrl.Location = new System.Drawing.Point(0, 97);
            this.ucDataUpDownBtnCtrl.Name = "ucDataUpDownBtnCtrl";
            this.ucDataUpDownBtnCtrl.Size = new System.Drawing.Size(950, 52);
            this.ucDataUpDownBtnCtrl.SourceGrid = null;
            this.ucDataUpDownBtnCtrl.TabIndex = 1;
            this.ucDataUpDownBtnCtrl.TargetGrid = null;
            // 
            // LotCreateReturn
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1275, 642);
            this.Name = "LotCreateReturn";
            this.Text = "CreateReturnLot";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboReason.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPrintLotCard.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboRouting.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpecialNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCSManager.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Micube.SmartMES.Commons.Controls.ucDataUpDownBtn ucDataUpDownBtnCtrl;
        private Framework.SmartControls.SmartBandedGrid grdLotsToCreate;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private Framework.SmartControls.SmartLabelComboBox cboRouting;
        private Framework.SmartControls.SmartLabelComboBox cboArea;
        private Framework.SmartControls.SmartCheckBox chkPrintLotCard;
        private Framework.SmartControls.SmartBandedGrid grdProcessPath;
        private Framework.SmartControls.SmartBandedGrid grdReturnLots;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartTextBox txtQty;
        private Framework.SmartControls.SmartLabel lblQty;
        private Framework.SmartControls.SmartTextBox txtSpecialNote;
        private Framework.SmartControls.SmartLabel lblSpecialNote;
        private Framework.SmartControls.SmartLabelComboBox cboReason;
        private Framework.SmartControls.SmartLabelTextBox txtCSManager;
    }
}
