namespace Micube.SmartMES.ProcessManagement
{
    partial class FinalInspectionAccept
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
            this.pnlTopCondition = new Micube.Framework.SmartControls.SmartPanel();
            this.tlpTopCondition = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.txtArea = new Micube.Framework.SmartControls.SmartLabelSelectPopupEdit();
            this.txtLotId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.grdLotInfo = new Micube.SmartMES.Commons.Controls.SmartLotInfoGrid();
            this.tabMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgSplit = new DevExpress.XtraTab.XtraTabPage();
            this.grdSplit = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSplitQty = new Micube.Framework.SmartControls.SmartLabel();
            this.txtSplitQty = new Micube.Framework.SmartControls.SmartSpinEdit();
            this.txtParentLotQty = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.cboUOM = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.btnSplit = new Micube.Framework.SmartControls.SmartButton();
            this.tpgMessage = new DevExpress.XtraTab.XtraTabPage();
            this.ucMessage = new Micube.SmartMES.ProcessManagement.usLotMessage();
            this.tpgComment = new DevExpress.XtraTab.XtraTabPage();
            this.grdComment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgProcessSpec = new DevExpress.XtraTab.XtraTabPage();
            this.grdProcessSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.btnInit = new Micube.Framework.SmartControls.SmartButton();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopCondition)).BeginInit();
            this.pnlTopCondition.SuspendLayout();
            this.tlpTopCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tpgSplit.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSplitQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParentLotQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUOM.Properties)).BeginInit();
            this.tpgMessage.SuspendLayout();
            this.tpgComment.SuspendLayout();
            this.tpgProcessSpec.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(0, 30);
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(0, 0);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.pnlToolbar.Size = new System.Drawing.Size(952, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.smartSplitTableLayoutPanel2, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabMain);
            this.pnlContent.Controls.Add(this.smartSpliterControl1);
            this.pnlContent.Controls.Add(this.grdLotInfo);
            this.pnlContent.Controls.Add(this.pnlTopCondition);
            this.pnlContent.Size = new System.Drawing.Size(952, 594);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(952, 623);
            // 
            // pnlTopCondition
            // 
            this.pnlTopCondition.Controls.Add(this.tlpTopCondition);
            this.pnlTopCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopCondition.Location = new System.Drawing.Point(0, 0);
            this.pnlTopCondition.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTopCondition.Name = "pnlTopCondition";
            this.pnlTopCondition.Size = new System.Drawing.Size(952, 34);
            this.pnlTopCondition.TabIndex = 0;
            // 
            // tlpTopCondition
            // 
            this.tlpTopCondition.ColumnCount = 9;
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTopCondition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpTopCondition.Controls.Add(this.txtArea, 1, 1);
            this.tlpTopCondition.Controls.Add(this.txtLotId, 3, 1);
            this.tlpTopCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTopCondition.Location = new System.Drawing.Point(2, 2);
            this.tlpTopCondition.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpTopCondition.Name = "tlpTopCondition";
            this.tlpTopCondition.RowCount = 3;
            this.tlpTopCondition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4F));
            this.tlpTopCondition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTopCondition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tlpTopCondition.Size = new System.Drawing.Size(948, 30);
            this.tlpTopCondition.TabIndex = 0;
            // 
            // txtArea
            // 
            this.txtArea.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtArea.Appearance.Options.UseForeColor = true;
            this.txtArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtArea.LabelText = "작업장";
            this.txtArea.LanguageKey = "AREA";
            this.txtArea.Location = new System.Drawing.Point(10, 4);
            this.txtArea.Margin = new System.Windows.Forms.Padding(0);
            this.txtArea.Name = "txtArea";
            this.txtArea.Size = new System.Drawing.Size(219, 20);
            this.txtArea.TabIndex = 0;
            // 
            // txtLotId
            // 
            this.txtLotId.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtLotId.Appearance.Options.UseForeColor = true;
            this.txtLotId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLotId.LabelText = "LOT NO";
            this.txtLotId.LanguageKey = "LOTID";
            this.txtLotId.Location = new System.Drawing.Point(239, 4);
            this.txtLotId.Margin = new System.Windows.Forms.Padding(0);
            this.txtLotId.Name = "txtLotId";
            this.txtLotId.Size = new System.Drawing.Size(219, 20);
            this.txtLotId.TabIndex = 1;
            // 
            // grdLotInfo
            // 
            this.grdLotInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdLotInfo.Location = new System.Drawing.Point(0, 34);
            this.grdLotInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotInfo.Name = "grdLotInfo";
            this.grdLotInfo.Size = new System.Drawing.Size(952, 148);
            this.grdLotInfo.TabIndex = 1;
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 187);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tpgSplit;
            this.tabMain.Size = new System.Drawing.Size(952, 407);
            this.tabMain.TabIndex = 3;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgSplit,
            this.tpgMessage,
            this.tpgComment,
            this.tpgProcessSpec});
            // 
            // tpgSplit
            // 
            this.tpgSplit.Controls.Add(this.grdSplit);
            this.tpgSplit.Controls.Add(this.panel1);
            this.tabMain.SetLanguageKey(this.tpgSplit, "LOTSPLIT");
            this.tpgSplit.Name = "tpgSplit";
            this.tpgSplit.Padding = new System.Windows.Forms.Padding(3);
            this.tpgSplit.Size = new System.Drawing.Size(946, 378);
            this.tpgSplit.Text = "Lot 분할";
            // 
            // grdSplit
            // 
            this.grdSplit.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSplit.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdSplit.IsUsePaging = false;
            this.grdSplit.LanguageKey = "TARGETSPLIT";
            this.grdSplit.Location = new System.Drawing.Point(3, 39);
            this.grdSplit.Margin = new System.Windows.Forms.Padding(0);
            this.grdSplit.Name = "grdSplit";
            this.grdSplit.ShowBorder = true;
            this.grdSplit.ShowStatusBar = false;
            this.grdSplit.Size = new System.Drawing.Size(940, 336);
            this.grdSplit.TabIndex = 3;
            this.grdSplit.UseAutoBestFitColumns = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSplitQty);
            this.panel1.Controls.Add(this.txtSplitQty);
            this.panel1.Controls.Add(this.txtParentLotQty);
            this.panel1.Controls.Add(this.cboUOM);
            this.panel1.Controls.Add(this.btnSplit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(940, 36);
            this.panel1.TabIndex = 0;
            // 
            // lblSplitQty
            // 
            this.lblSplitQty.LanguageKey = "SPLITQTY";
            this.lblSplitQty.Location = new System.Drawing.Point(206, 11);
            this.lblSplitQty.Name = "lblSplitQty";
            this.lblSplitQty.Size = new System.Drawing.Size(44, 14);
            this.lblSplitQty.TabIndex = 5;
            this.lblSplitQty.Text = "분할 수량";
            // 
            // txtSplitQty
            // 
            this.txtSplitQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSplitQty.LabelText = null;
            this.txtSplitQty.LanguageKey = null;
            this.txtSplitQty.Location = new System.Drawing.Point(312, 9);
            this.txtSplitQty.Name = "txtSplitQty";
            this.txtSplitQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSplitQty.Size = new System.Drawing.Size(100, 20);
            this.txtSplitQty.TabIndex = 4;
            // 
            // txtParentLotQty
            // 
            this.txtParentLotQty.LabelText = "모 LOT 수량";
            this.txtParentLotQty.LanguageKey = null;
            this.txtParentLotQty.Location = new System.Drawing.Point(522, 9);
            this.txtParentLotQty.Name = "txtParentLotQty";
            this.txtParentLotQty.Properties.ReadOnly = true;
            this.txtParentLotQty.Size = new System.Drawing.Size(220, 20);
            this.txtParentLotQty.TabIndex = 3;
            this.txtParentLotQty.Visible = false;
            // 
            // cboUOM
            // 
            this.cboUOM.LabelText = "UOM";
            this.cboUOM.LabelWidth = "20%";
            this.cboUOM.LanguageKey = null;
            this.cboUOM.Location = new System.Drawing.Point(5, 9);
            this.cboUOM.Name = "cboUOM";
            this.cboUOM.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUOM.Properties.NullText = "";
            this.cboUOM.Size = new System.Drawing.Size(177, 20);
            this.cboUOM.TabIndex = 2;
            // 
            // btnSplit
            // 
            this.btnSplit.AllowFocus = false;
            this.btnSplit.IsBusy = false;
            this.btnSplit.IsWrite = false;
            this.btnSplit.LanguageKey = "SPLIT";
            this.btnSplit.Location = new System.Drawing.Point(418, 6);
            this.btnSplit.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSplit.Size = new System.Drawing.Size(83, 25);
            this.btnSplit.TabIndex = 1;
            this.btnSplit.Text = "분할";
            this.btnSplit.TooltipLanguageKey = "";
            // 
            // tpgMessage
            // 
            this.tpgMessage.Controls.Add(this.ucMessage);
            this.tabMain.SetLanguageKey(this.tpgMessage, "MESSAGE");
            this.tpgMessage.Name = "tpgMessage";
            this.tpgMessage.Padding = new System.Windows.Forms.Padding(3);
            this.tpgMessage.Size = new System.Drawing.Size(750, 273);
            this.tpgMessage.Text = "Message";
            // 
            // ucMessage
            // 
            this.ucMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMessage.Location = new System.Drawing.Point(3, 3);
            this.ucMessage.MessageDataSource = null;
            this.ucMessage.Name = "ucMessage";
            this.ucMessage.Size = new System.Drawing.Size(744, 267);
            this.ucMessage.TabIndex = 0;
            // 
            // tpgComment
            // 
            this.tpgComment.Controls.Add(this.grdComment);
            this.tabMain.SetLanguageKey(this.tpgComment, "REMARKS");
            this.tpgComment.Name = "tpgComment";
            this.tpgComment.Padding = new System.Windows.Forms.Padding(3);
            this.tpgComment.Size = new System.Drawing.Size(750, 273);
            this.tpgComment.Text = "특기사항";
            // 
            // grdComment
            // 
            this.grdComment.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdComment.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdComment.IsUsePaging = false;
            this.grdComment.LanguageKey = null;
            this.grdComment.Location = new System.Drawing.Point(3, 3);
            this.grdComment.Margin = new System.Windows.Forms.Padding(0);
            this.grdComment.Name = "grdComment";
            this.grdComment.ShowBorder = true;
            this.grdComment.ShowStatusBar = false;
            this.grdComment.Size = new System.Drawing.Size(744, 267);
            this.grdComment.TabIndex = 1;
            this.grdComment.UseAutoBestFitColumns = false;
            // 
            // tpgProcessSpec
            // 
            this.tpgProcessSpec.Controls.Add(this.grdProcessSpec);
            this.tabMain.SetLanguageKey(this.tpgProcessSpec, "PROCESSSPEC");
            this.tpgProcessSpec.Name = "tpgProcessSpec";
            this.tpgProcessSpec.Padding = new System.Windows.Forms.Padding(3);
            this.tpgProcessSpec.Size = new System.Drawing.Size(750, 273);
            this.tpgProcessSpec.Text = "공정 SPEC";
            // 
            // grdProcessSpec
            // 
            this.grdProcessSpec.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdProcessSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProcessSpec.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdProcessSpec.IsUsePaging = false;
            this.grdProcessSpec.LanguageKey = null;
            this.grdProcessSpec.Location = new System.Drawing.Point(3, 3);
            this.grdProcessSpec.Margin = new System.Windows.Forms.Padding(0);
            this.grdProcessSpec.Name = "grdProcessSpec";
            this.grdProcessSpec.ShowBorder = true;
            this.grdProcessSpec.ShowStatusBar = false;
            this.grdProcessSpec.Size = new System.Drawing.Size(744, 267);
            this.grdProcessSpec.TabIndex = 1;
            this.grdProcessSpec.UseAutoBestFitColumns = false;
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 2;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.btnInit, 3, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(47, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 1;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(905, 24);
            this.smartSplitTableLayoutPanel2.TabIndex = 6;
            // 
            // btnInit
            // 
            this.btnInit.AllowFocus = false;
            this.btnInit.IsBusy = false;
            this.btnInit.IsWrite = false;
            this.btnInit.LanguageKey = "INITIALIZE";
            this.btnInit.Location = new System.Drawing.Point(823, 0);
            this.btnInit.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnInit.Name = "btnInit";
            this.btnInit.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnInit.Size = new System.Drawing.Size(75, 23);
            this.btnInit.TabIndex = 0;
            this.btnInit.Text = "초기화";
            this.btnInit.TooltipLanguageKey = "";
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl1.Location = new System.Drawing.Point(0, 182);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(952, 5);
            this.smartSpliterControl1.TabIndex = 13;
            this.smartSpliterControl1.TabStop = false;
            // 
            // FinalInspectionAccept
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 643);
            this.ConditionsVisible = false;
            this.Name = "FinalInspectionAccept";
            this.ShowSaveCompleteMessage = false;
            this.Text = "최종검사 인수등록";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopCondition)).EndInit();
            this.pnlTopCondition.ResumeLayout(false);
            this.tlpTopCondition.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tpgSplit.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSplitQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParentLotQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUOM.Properties)).EndInit();
            this.tpgMessage.ResumeLayout(false);
            this.tpgComment.ResumeLayout(false);
            this.tpgProcessSpec.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartButton btnInit;
        private Framework.SmartControls.SmartPanel pnlTopCondition;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpTopCondition;
        private Framework.SmartControls.SmartLabelSelectPopupEdit txtArea;
        private Framework.SmartControls.SmartLabelTextBox txtLotId;
        private Commons.Controls.SmartLotInfoGrid grdLotInfo;
        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tpgSplit;
        private System.Windows.Forms.Panel panel1;
        private Framework.SmartControls.SmartButton btnSplit;
        private Framework.SmartControls.SmartLabelComboBox cboUOM;
        private Framework.SmartControls.SmartBandedGrid grdSplit;
        private Framework.SmartControls.SmartLabelTextBox txtParentLotQty;
        private DevExpress.XtraTab.XtraTabPage tpgMessage;
        private usLotMessage ucMessage;
        private DevExpress.XtraTab.XtraTabPage tpgComment;
        private DevExpress.XtraTab.XtraTabPage tpgProcessSpec;
        private Framework.SmartControls.SmartBandedGrid grdComment;
        private Framework.SmartControls.SmartBandedGrid grdProcessSpec;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartLabel lblSplitQty;
        private Framework.SmartControls.SmartSpinEdit txtSplitQty;
    }
}