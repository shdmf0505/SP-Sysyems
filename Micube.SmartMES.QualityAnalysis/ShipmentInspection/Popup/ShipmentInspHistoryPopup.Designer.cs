namespace Micube.SmartMES.QualityAnalysis
{
    partial class ShipmentInspHistoryPopup
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
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.tabHistory = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgHistory = new DevExpress.XtraTab.XtraTabPage();
            this.grpInspInfo = new Micube.Framework.SmartControls.SmartGroupBox();
            this.smartSplitTableLayoutPanel5 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.picBox1 = new Micube.Framework.SmartControls.SmartPictureEdit();
            this.picBox2 = new Micube.Framework.SmartControls.SmartPictureEdit();
            this.picBox3 = new Micube.Framework.SmartControls.SmartPictureEdit();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.grdHistory = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabResult = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgDefectEx = new DevExpress.XtraTab.XtraTabPage();
            this.grdHistoryDetail = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgDefectDisposal = new DevExpress.XtraTab.XtraTabPage();
            this.grdDefectDisposal = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdLotComInfo = new Micube.SmartMES.Commons.Controls.SmartLotInfoGrid();
            this.tpgMessage = new DevExpress.XtraTab.XtraTabPage();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.grdCreatorList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdProcessSegmentList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.txtTitle = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.memoBox = new Micube.Framework.SmartControls.SmartRichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabHistory)).BeginInit();
            this.tabHistory.SuspendLayout();
            this.tpgHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpInspInfo)).BeginInit();
            this.grpInspInfo.SuspendLayout();
            this.smartSplitTableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox3.Properties)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabResult)).BeginInit();
            this.tabResult.SuspendLayout();
            this.tpgDefectEx.SuspendLayout();
            this.tpgDefectDisposal.SuspendLayout();
            this.tpgMessage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(1388, 457);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnClose, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tabHistory, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1388, 457);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(1308, 432);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "smartButton1";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // tabHistory
            // 
            this.tabHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabHistory.Location = new System.Drawing.Point(0, 0);
            this.tabHistory.Margin = new System.Windows.Forms.Padding(0);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.SelectedTabPage = this.tpgHistory;
            this.tabHistory.Size = new System.Drawing.Size(1388, 422);
            this.tabHistory.TabIndex = 8;
            this.tabHistory.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgHistory,
            this.tpgMessage});
            // 
            // tpgHistory
            // 
            this.tpgHistory.Controls.Add(this.grpInspInfo);
            this.tabHistory.SetLanguageKey(this.tpgHistory, "SHIPMENTINSPHISTORY");
            this.tpgHistory.Name = "tpgHistory";
            this.tpgHistory.Size = new System.Drawing.Size(1382, 393);
            this.tpgHistory.Text = "xtraTabPage1";
            // 
            // grpInspInfo
            // 
            this.grpInspInfo.Controls.Add(this.smartSplitTableLayoutPanel5);
            this.grpInspInfo.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpInspInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInspInfo.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpInspInfo.LanguageKey = "SHIPMENTINSPHISTORY";
            this.grpInspInfo.Location = new System.Drawing.Point(0, 0);
            this.grpInspInfo.Name = "grpInspInfo";
            this.grpInspInfo.ShowBorder = true;
            this.grpInspInfo.Size = new System.Drawing.Size(1382, 393);
            this.grpInspInfo.TabIndex = 0;
            this.grpInspInfo.Text = "smartGroupBox1";
            // 
            // smartSplitTableLayoutPanel5
            // 
            this.smartSplitTableLayoutPanel5.ColumnCount = 2;
            this.smartSplitTableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 1, 1);
            this.smartSplitTableLayoutPanel5.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.smartSplitTableLayoutPanel5.Controls.Add(this.grdLotComInfo, 0, 0);
            this.smartSplitTableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel5.Location = new System.Drawing.Point(2, 31);
            this.smartSplitTableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel5.Name = "smartSplitTableLayoutPanel5";
            this.smartSplitTableLayoutPanel5.RowCount = 2;
            this.smartSplitTableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.81818F));
            this.smartSplitTableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 68.18182F));
            this.smartSplitTableLayoutPanel5.Size = new System.Drawing.Size(1378, 360);
            this.smartSplitTableLayoutPanel5.TabIndex = 8;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel6.Controls.Add(this.picBox1, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.picBox2, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.picBox3, 2, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(689, 114);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(689, 246);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // picBox1
            // 
            this.picBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBox1.Location = new System.Drawing.Point(3, 3);
            this.picBox1.Name = "picBox1";
            this.picBox1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picBox1.Size = new System.Drawing.Size(223, 240);
            this.picBox1.TabIndex = 0;
            // 
            // picBox2
            // 
            this.picBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBox2.Location = new System.Drawing.Point(232, 3);
            this.picBox2.Name = "picBox2";
            this.picBox2.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picBox2.Size = new System.Drawing.Size(223, 240);
            this.picBox2.TabIndex = 1;
            // 
            // picBox3
            // 
            this.picBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBox3.Location = new System.Drawing.Point(461, 3);
            this.picBox3.Name = "picBox3";
            this.picBox3.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picBox3.Size = new System.Drawing.Size(225, 240);
            this.picBox3.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.grdHistory, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tabResult, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 114);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(689, 246);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // grdHistory
            // 
            this.grdHistory.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHistory.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdHistory.IsUsePaging = false;
            this.grdHistory.LanguageKey = null;
            this.grdHistory.Location = new System.Drawing.Point(0, 0);
            this.grdHistory.Margin = new System.Windows.Forms.Padding(0);
            this.grdHistory.Name = "grdHistory";
            this.grdHistory.ShowBorder = true;
            this.grdHistory.ShowButtonBar = false;
            this.grdHistory.ShowStatusBar = false;
            this.grdHistory.Size = new System.Drawing.Size(689, 94);
            this.grdHistory.TabIndex = 1;
            // 
            // tabResult
            // 
            this.tabResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabResult.Location = new System.Drawing.Point(0, 104);
            this.tabResult.Margin = new System.Windows.Forms.Padding(0);
            this.tabResult.Name = "tabResult";
            this.tabResult.SelectedTabPage = this.tpgDefectEx;
            this.tabResult.Size = new System.Drawing.Size(689, 142);
            this.tabResult.TabIndex = 2;
            this.tabResult.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgDefectEx,
            this.tpgDefectDisposal});
            // 
            // tpgDefectEx
            // 
            this.tpgDefectEx.Controls.Add(this.grdHistoryDetail);
            this.tabResult.SetLanguageKey(this.tpgDefectEx, "DEFECTINSPECTION");
            this.tpgDefectEx.Name = "tpgDefectEx";
            this.tpgDefectEx.Size = new System.Drawing.Size(683, 113);
            this.tpgDefectEx.Text = "xtraTabPage1";
            // 
            // grdHistoryDetail
            // 
            this.grdHistoryDetail.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdHistoryDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHistoryDetail.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdHistoryDetail.IsUsePaging = false;
            this.grdHistoryDetail.LanguageKey = null;
            this.grdHistoryDetail.Location = new System.Drawing.Point(0, 0);
            this.grdHistoryDetail.Margin = new System.Windows.Forms.Padding(0);
            this.grdHistoryDetail.Name = "grdHistoryDetail";
            this.grdHistoryDetail.ShowBorder = true;
            this.grdHistoryDetail.ShowButtonBar = false;
            this.grdHistoryDetail.ShowStatusBar = false;
            this.grdHistoryDetail.Size = new System.Drawing.Size(683, 113);
            this.grdHistoryDetail.TabIndex = 1;
            // 
            // tpgDefectDisposal
            // 
            this.tpgDefectDisposal.Controls.Add(this.grdDefectDisposal);
            this.tabResult.SetLanguageKey(this.tpgDefectDisposal, "DEFECTDISPOSAL");
            this.tpgDefectDisposal.Name = "tpgDefectDisposal";
            this.tpgDefectDisposal.Size = new System.Drawing.Size(379, 102);
            this.tpgDefectDisposal.Text = "xtraTabPage2";
            // 
            // grdDefectDisposal
            // 
            this.grdDefectDisposal.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdDefectDisposal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDefectDisposal.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdDefectDisposal.IsUsePaging = false;
            this.grdDefectDisposal.LanguageKey = null;
            this.grdDefectDisposal.Location = new System.Drawing.Point(0, 0);
            this.grdDefectDisposal.Margin = new System.Windows.Forms.Padding(0);
            this.grdDefectDisposal.Name = "grdDefectDisposal";
            this.grdDefectDisposal.ShowBorder = true;
            this.grdDefectDisposal.ShowButtonBar = false;
            this.grdDefectDisposal.ShowStatusBar = false;
            this.grdDefectDisposal.Size = new System.Drawing.Size(379, 102);
            this.grdDefectDisposal.TabIndex = 1;
            // 
            // grdLotComInfo
            // 
            this.smartSplitTableLayoutPanel5.SetColumnSpan(this.grdLotComInfo, 2);
            this.grdLotComInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotComInfo.Location = new System.Drawing.Point(0, 0);
            this.grdLotComInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotComInfo.Name = "grdLotComInfo";
            this.grdLotComInfo.Size = new System.Drawing.Size(1378, 114);
            this.grdLotComInfo.TabIndex = 3;
            // 
            // tpgMessage
            // 
            this.tpgMessage.Controls.Add(this.smartGroupBox1);
            this.tabHistory.SetLanguageKey(this.tpgMessage, "MESSAGE");
            this.tpgMessage.Name = "tpgMessage";
            this.tpgMessage.Size = new System.Drawing.Size(774, 366);
            this.tpgMessage.Text = "xtraTabPage2";
            // 
            // smartGroupBox1
            // 
            this.smartGroupBox1.Controls.Add(this.tableLayoutPanel3);
            this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox1.LanguageKey = "MESSAGE";
            this.smartGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.smartGroupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.smartGroupBox1.Name = "smartGroupBox1";
            this.smartGroupBox1.ShowBorder = true;
            this.smartGroupBox1.Size = new System.Drawing.Size(774, 366);
            this.smartGroupBox1.TabIndex = 0;
            this.smartGroupBox1.Text = "smartGroupBox1";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.77778F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.grdCreatorList, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.grdProcessSegmentList, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 4, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(2, 31);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(770, 333);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // grdCreatorList
            // 
            this.grdCreatorList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdCreatorList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCreatorList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdCreatorList.IsUsePaging = false;
            this.grdCreatorList.LanguageKey = null;
            this.grdCreatorList.Location = new System.Drawing.Point(89, 0);
            this.grdCreatorList.Margin = new System.Windows.Forms.Padding(0);
            this.grdCreatorList.Name = "grdCreatorList";
            this.grdCreatorList.ShowBorder = true;
            this.grdCreatorList.ShowButtonBar = false;
            this.grdCreatorList.ShowStatusBar = false;
            this.grdCreatorList.Size = new System.Drawing.Size(84, 333);
            this.grdCreatorList.TabIndex = 2;
            // 
            // grdProcessSegmentList
            // 
            this.grdProcessSegmentList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdProcessSegmentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProcessSegmentList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdProcessSegmentList.IsUsePaging = false;
            this.grdProcessSegmentList.LanguageKey = null;
            this.grdProcessSegmentList.Location = new System.Drawing.Point(0, 0);
            this.grdProcessSegmentList.Margin = new System.Windows.Forms.Padding(0);
            this.grdProcessSegmentList.Name = "grdProcessSegmentList";
            this.grdProcessSegmentList.ShowBorder = true;
            this.grdProcessSegmentList.ShowButtonBar = false;
            this.grdProcessSegmentList.ShowStatusBar = false;
            this.grdProcessSegmentList.Size = new System.Drawing.Size(84, 333);
            this.grdProcessSegmentList.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.memoBox, 0, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(178, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(592, 333);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.Controls.Add(this.txtTitle, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(592, 30);
            this.tableLayoutPanel7.TabIndex = 2;
            // 
            // txtTitle
            // 
            this.txtTitle.AutoHeight = false;
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTitle.LabelWidth = "10%";
            this.txtTitle.LanguageKey = "TITLE";
            this.txtTitle.Location = new System.Drawing.Point(0, 0);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(0);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Properties.AutoHeight = false;
            this.txtTitle.Properties.ReadOnly = true;
            this.txtTitle.Properties.UseReadOnlyAppearance = false;
            this.txtTitle.Size = new System.Drawing.Size(296, 30);
            this.txtTitle.TabIndex = 1;
            // 
            // memoBox
            // 
            this.memoBox.AlignCenterVisible = false;
            this.memoBox.AlignLeftVisible = false;
            this.memoBox.AlignRightVisible = false;
            this.memoBox.BoldVisible = false;
            this.memoBox.BulletsVisible = false;
            this.memoBox.ChooseFontVisible = false;
            this.memoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoBox.FontColorVisible = false;
            this.memoBox.FontFamilyVisible = false;
            this.memoBox.FontSizeVisible = false;
            this.memoBox.INDENT = 10;
            this.memoBox.ItalicVisible = false;
            this.memoBox.Location = new System.Drawing.Point(0, 35);
            this.memoBox.Margin = new System.Windows.Forms.Padding(0);
            this.memoBox.Name = "memoBox";
            this.memoBox.ReadOnly = true;
            this.memoBox.Rtf = "{\\rtf1\\ansi\\ansicpg949\\deff0\\deflang1033\\deflangfe1042{\\fonttbl{\\f0\\fnil\\fcharset" +
    "204 Microsoft Sans Serif;}}\r\n\\viewkind4\\uc1\\pard\\lang1042\\f0\\fs18\\par\r\n}\r\n";
            this.memoBox.SeparatorAlignVisible = false;
            this.memoBox.SeparatorBoldUnderlineItalicVisible = false;
            this.memoBox.SeparatorFontColorVisible = false;
            this.memoBox.SeparatorFontVisible = false;
            this.memoBox.Size = new System.Drawing.Size(592, 298);
            this.memoBox.TabIndex = 3;
            this.memoBox.ToolStripVisible = false;
            this.memoBox.UnderlineVisible = false;
            this.memoBox.WordWrapVisible = false;
            // 
            // ShipmentInspHistoryPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1408, 477);
            this.Name = "ShipmentInspHistoryPopup";
            this.Text = "ShipmentInspHistoryPopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabHistory)).EndInit();
            this.tabHistory.ResumeLayout(false);
            this.tpgHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpInspInfo)).EndInit();
            this.grpInspInfo.ResumeLayout(false);
            this.smartSplitTableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox3.Properties)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabResult)).EndInit();
            this.tabResult.ResumeLayout(false);
            this.tpgDefectEx.ResumeLayout(false);
            this.tpgDefectDisposal.ResumeLayout(false);
            this.tpgMessage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartTabControl tabHistory;
        private DevExpress.XtraTab.XtraTabPage tpgHistory;
        private Framework.SmartControls.SmartGroupBox grpInspInfo;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private Framework.SmartControls.SmartPictureEdit picBox1;
        private Framework.SmartControls.SmartPictureEdit picBox2;
        private Framework.SmartControls.SmartPictureEdit picBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Framework.SmartControls.SmartBandedGrid grdHistory;
        private Commons.Controls.SmartLotInfoGrid grdLotComInfo;
        private DevExpress.XtraTab.XtraTabPage tpgMessage;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Framework.SmartControls.SmartBandedGrid grdCreatorList;
        private Framework.SmartControls.SmartBandedGrid grdProcessSegmentList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private Framework.SmartControls.SmartLabelTextBox txtTitle;
        private Framework.SmartControls.SmartRichTextBox memoBox;
        private Framework.SmartControls.SmartTabControl tabResult;
        private DevExpress.XtraTab.XtraTabPage tpgDefectEx;
        private Framework.SmartControls.SmartBandedGrid grdHistoryDetail;
        private DevExpress.XtraTab.XtraTabPage tpgDefectDisposal;
        private Framework.SmartControls.SmartBandedGrid grdDefectDisposal;
    }
}