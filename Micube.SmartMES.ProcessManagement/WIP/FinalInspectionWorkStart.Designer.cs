namespace Micube.SmartMES.ProcessManagement
{
    partial class FinalInspectionWorkStart
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
            this.pnlTopCondition = new Micube.Framework.SmartControls.SmartPanel();
            this.tlpTopCondition = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.txtArea = new Micube.Framework.SmartControls.SmartLabelSelectPopupEdit();
            this.txtLotId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.grdLotInfo = new Micube.SmartMES.Commons.Controls.SmartLotInfoGrid();
            this.tabInfo = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgLotInfo = new DevExpress.XtraTab.XtraTabPage();
            this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgEquipment = new DevExpress.XtraTab.XtraTabPage();
            this.grdEquipment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgSelfInspection = new DevExpress.XtraTab.XtraTabPage();
            this.usInspectionResult = new Micube.SmartMES.ProcessManagement.usInspectionResult();
            this.tpgMessage = new DevExpress.XtraTab.XtraTabPage();
            this.ucMessage = new Micube.SmartMES.ProcessManagement.usLotMessage();
            this.tpgComment = new DevExpress.XtraTab.XtraTabPage();
            this.grdComment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgProcessSpec = new DevExpress.XtraTab.XtraTabPage();
            this.grdProcessSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.pfsInfo = new Micube.Framework.SmartControls.SmartPanel();
            this.lblComment = new Micube.Framework.SmartControls.SmartLabel();
            this.txtComment = new Micube.Framework.SmartControls.SmartTextBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.tabInfo)).BeginInit();
            this.tabInfo.SuspendLayout();
            this.tpgLotInfo.SuspendLayout();
            this.tpgEquipment.SuspendLayout();
            this.tpgSelfInspection.SuspendLayout();
            this.tpgMessage.SuspendLayout();
            this.tpgComment.SuspendLayout();
            this.tpgProcessSpec.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pfsInfo)).BeginInit();
            this.pfsInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).BeginInit();
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
            this.pnlToolbar.Size = new System.Drawing.Size(820, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.smartSplitTableLayoutPanel2, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabInfo);
            this.pnlContent.Controls.Add(this.pfsInfo);
            this.pnlContent.Controls.Add(this.smartSpliterControl1);
            this.pnlContent.Controls.Add(this.grdLotInfo);
            this.pnlContent.Controls.Add(this.pnlTopCondition);
            this.pnlContent.Size = new System.Drawing.Size(820, 571);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(820, 600);
            // 
            // pnlTopCondition
            // 
            this.pnlTopCondition.Controls.Add(this.tlpTopCondition);
            this.pnlTopCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopCondition.Location = new System.Drawing.Point(0, 0);
            this.pnlTopCondition.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTopCondition.Name = "pnlTopCondition";
            this.pnlTopCondition.Size = new System.Drawing.Size(820, 34);
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
            this.tlpTopCondition.Size = new System.Drawing.Size(816, 30);
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
            this.txtArea.Size = new System.Drawing.Size(186, 20);
            this.txtArea.TabIndex = 0;
            // 
            // txtLotId
            // 
            this.txtLotId.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtLotId.Appearance.Options.UseForeColor = true;
            this.txtLotId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLotId.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txtLotId.LabelText = "LOT NO";
            this.txtLotId.LanguageKey = "LOTID";
            this.txtLotId.Location = new System.Drawing.Point(206, 4);
            this.txtLotId.Margin = new System.Windows.Forms.Padding(0);
            this.txtLotId.Name = "txtLotId";
            this.txtLotId.Size = new System.Drawing.Size(186, 20);
            this.txtLotId.TabIndex = 1;
            // 
            // grdLotInfo
            // 
            this.grdLotInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdLotInfo.Location = new System.Drawing.Point(0, 34);
            this.grdLotInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotInfo.Name = "grdLotInfo";
            this.grdLotInfo.Size = new System.Drawing.Size(820, 124);
            this.grdLotInfo.TabIndex = 1;
            // 
            // tabInfo
            // 
            this.tabInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInfo.Location = new System.Drawing.Point(0, 197);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.SelectedTabPage = this.tpgLotInfo;
            this.tabInfo.Size = new System.Drawing.Size(820, 374);
            this.tabInfo.TabIndex = 3;
            this.tabInfo.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgEquipment,
            this.tpgLotInfo,
            this.tpgSelfInspection,
            this.tpgMessage,
            this.tpgComment,
            this.tpgProcessSpec});
            // 
            // tpgLotInfo
            // 
            this.tpgLotInfo.Controls.Add(this.grdLotList);
            this.tabInfo.SetLanguageKey(this.tpgLotInfo, "LOTINFO");
            this.tpgLotInfo.Name = "tpgLotInfo";
            this.tpgLotInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpgLotInfo.Size = new System.Drawing.Size(814, 345);
            this.tpgLotInfo.Text = "Lot 정보";
            // 
            // grdLotList
            // 
            this.grdLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLotList.IsUsePaging = false;
            this.grdLotList.LanguageKey = "LOTINFO";
            this.grdLotList.Location = new System.Drawing.Point(3, 3);
            this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotList.Name = "grdLotList";
            this.grdLotList.ShowBorder = true;
            this.grdLotList.ShowStatusBar = false;
            this.grdLotList.Size = new System.Drawing.Size(808, 339);
            this.grdLotList.TabIndex = 3;
            this.grdLotList.UseAutoBestFitColumns = false;
            // 
            // tpgEquipment
            // 
            this.tpgEquipment.Controls.Add(this.grdEquipment);
            this.tabInfo.SetLanguageKey(this.tpgEquipment, "EQUIPMENT");
            this.tpgEquipment.Name = "tpgEquipment";
            this.tpgEquipment.Padding = new System.Windows.Forms.Padding(3);
            this.tpgEquipment.Size = new System.Drawing.Size(750, 132);
            this.tpgEquipment.Text = "설비";
            // 
            // grdEquipment
            // 
            this.grdEquipment.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdEquipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEquipment.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdEquipment.IsUsePaging = false;
            this.grdEquipment.LanguageKey = null;
            this.grdEquipment.Location = new System.Drawing.Point(3, 3);
            this.grdEquipment.Margin = new System.Windows.Forms.Padding(0);
            this.grdEquipment.Name = "grdEquipment";
            this.grdEquipment.ShowBorder = true;
            this.grdEquipment.ShowStatusBar = false;
            this.grdEquipment.Size = new System.Drawing.Size(744, 126);
            this.grdEquipment.TabIndex = 3;
            this.grdEquipment.UseAutoBestFitColumns = false;
            // 
            // tpgSelfInspection
            // 
            this.tpgSelfInspection.Controls.Add(this.usInspectionResult);
            this.tabInfo.SetLanguageKey(this.tpgSelfInspection, "QCMINSPECTIONRESULTLIST");
            this.tpgSelfInspection.Name = "tpgSelfInspection";
            this.tpgSelfInspection.Padding = new System.Windows.Forms.Padding(3);
            this.tpgSelfInspection.Size = new System.Drawing.Size(750, 132);
            this.tpgSelfInspection.Text = "자주검사 이력";
            // 
            // usInspectionResult
            // 
            this.usInspectionResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usInspectionResult.InspectionData = null;
            this.usInspectionResult.Location = new System.Drawing.Point(3, 3);
            this.usInspectionResult.LotID = null;
            this.usInspectionResult.Name = "usInspectionResult";
            this.usInspectionResult.Size = new System.Drawing.Size(744, 126);
            this.usInspectionResult.TabIndex = 1;
            // 
            // tpgMessage
            // 
            this.tpgMessage.Controls.Add(this.ucMessage);
            this.tabInfo.SetLanguageKey(this.tpgMessage, "MESSAGE");
            this.tpgMessage.Name = "tpgMessage";
            this.tpgMessage.Padding = new System.Windows.Forms.Padding(3);
            this.tpgMessage.Size = new System.Drawing.Size(750, 132);
            this.tpgMessage.Text = "Message";
            // 
            // ucMessage
            // 
            this.ucMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMessage.Location = new System.Drawing.Point(3, 3);
            this.ucMessage.MessageDataSource = null;
            this.ucMessage.Name = "ucMessage";
            this.ucMessage.Size = new System.Drawing.Size(744, 126);
            this.ucMessage.TabIndex = 1;
            // 
            // tpgComment
            // 
            this.tpgComment.Controls.Add(this.grdComment);
            this.tabInfo.SetLanguageKey(this.tpgComment, "REMARKS");
            this.tpgComment.Name = "tpgComment";
            this.tpgComment.Padding = new System.Windows.Forms.Padding(3);
            this.tpgComment.Size = new System.Drawing.Size(750, 132);
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
            this.grdComment.Size = new System.Drawing.Size(744, 126);
            this.grdComment.TabIndex = 2;
            this.grdComment.UseAutoBestFitColumns = false;
            // 
            // tpgProcessSpec
            // 
            this.tpgProcessSpec.Controls.Add(this.grdProcessSpec);
            this.tabInfo.SetLanguageKey(this.tpgProcessSpec, "PROCESSSPEC");
            this.tpgProcessSpec.Name = "tpgProcessSpec";
            this.tpgProcessSpec.Padding = new System.Windows.Forms.Padding(3);
            this.tpgProcessSpec.Size = new System.Drawing.Size(750, 132);
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
            this.grdProcessSpec.Size = new System.Drawing.Size(744, 126);
            this.grdProcessSpec.TabIndex = 2;
            this.grdProcessSpec.UseAutoBestFitColumns = false;
            // 
            // pfsInfo
            // 
            this.pfsInfo.Controls.Add(this.lblComment);
            this.pfsInfo.Controls.Add(this.txtComment);
            this.pfsInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pfsInfo.Location = new System.Drawing.Point(0, 163);
            this.pfsInfo.Name = "pfsInfo";
            this.pfsInfo.Size = new System.Drawing.Size(820, 34);
            this.pfsInfo.TabIndex = 5;
            // 
            // lblComment
            // 
            this.lblComment.LanguageKey = "COMMENT";
            this.lblComment.Location = new System.Drawing.Point(8, 10);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(40, 14);
            this.lblComment.TabIndex = 7;
            this.lblComment.Text = "특이사항";
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.LabelText = null;
            this.txtComment.LanguageKey = null;
            this.txtComment.Location = new System.Drawing.Point(84, 7);
            this.txtComment.Margin = new System.Windows.Forms.Padding(0);
            this.txtComment.Name = "txtComment";
            this.txtComment.Properties.AutoHeight = false;
            this.txtComment.Size = new System.Drawing.Size(728, 20);
            this.txtComment.TabIndex = 6;
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
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(773, 24);
            this.smartSplitTableLayoutPanel2.TabIndex = 6;
            // 
            // btnInit
            // 
            this.btnInit.AllowFocus = false;
            this.btnInit.IsBusy = false;
            this.btnInit.IsWrite = false;
            this.btnInit.LanguageKey = "INITIALIZE";
            this.btnInit.Location = new System.Drawing.Point(691, 0);
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
            this.smartSpliterControl1.Location = new System.Drawing.Point(0, 158);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(820, 5);
            this.smartSpliterControl1.TabIndex = 14;
            this.smartSpliterControl1.TabStop = false;
            // 
            // FinalInspectionWorkStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 620);
            this.ConditionsVisible = false;
            this.LanguageKey = "SEG0560";
            this.Name = "FinalInspectionWorkStart";
            this.ShowSaveCompleteMessage = false;
            this.Text = "최종검사 작업 시작";
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
            ((System.ComponentModel.ISupportInitialize)(this.tabInfo)).EndInit();
            this.tabInfo.ResumeLayout(false);
            this.tpgLotInfo.ResumeLayout(false);
            this.tpgEquipment.ResumeLayout(false);
            this.tpgSelfInspection.ResumeLayout(false);
            this.tpgMessage.ResumeLayout(false);
            this.tpgComment.ResumeLayout(false);
            this.tpgProcessSpec.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pfsInfo)).EndInit();
            this.pfsInfo.ResumeLayout(false);
            this.pfsInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).EndInit();
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
        private Framework.SmartControls.SmartTabControl tabInfo;
        private DevExpress.XtraTab.XtraTabPage tpgLotInfo;
        private Framework.SmartControls.SmartPanel pfsInfo;
        private Framework.SmartControls.SmartLabel lblComment;
        private Framework.SmartControls.SmartTextBox txtComment;
        private Framework.SmartControls.SmartBandedGrid grdLotList;
        private DevExpress.XtraTab.XtraTabPage tpgMessage;
        private usLotMessage ucMessage;
        private DevExpress.XtraTab.XtraTabPage tpgComment;
        private DevExpress.XtraTab.XtraTabPage tpgProcessSpec;
        private Framework.SmartControls.SmartBandedGrid grdComment;
        private Framework.SmartControls.SmartBandedGrid grdProcessSpec;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private DevExpress.XtraTab.XtraTabPage tpgSelfInspection;
        private DevExpress.XtraTab.XtraTabPage tpgEquipment;
        private Framework.SmartControls.SmartBandedGrid grdEquipment;
        private usInspectionResult usInspectionResult;
    }
}