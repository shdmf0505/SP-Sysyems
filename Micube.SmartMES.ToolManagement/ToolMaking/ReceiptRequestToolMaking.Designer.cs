namespace Micube.SmartMES.ToolManagement
{
    partial class ReceiptRequestToolMaking
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
            this.grdToolRequestReceipt = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.grdInputToolRequestReceipt = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel4 = new Micube.Framework.SmartControls.SmartPanel();
            this.lblInboundDate = new Micube.Framework.SmartControls.SmartLabel();
            this.deInboundDate = new Micube.Framework.SmartControls.SmartDateEdit();
            this.btnSearchTool = new Micube.Framework.SmartControls.SmartButton();
            this.lblInputTitle = new Micube.Framework.SmartControls.SmartLabel();
            this.btnInitialize = new Micube.Framework.SmartControls.SmartButton();
            this.btnErase = new Micube.Framework.SmartControls.SmartButton();
            this.btnModify = new Micube.Framework.SmartControls.SmartButton();
            this.tabMakeReceipt = new Micube.Framework.SmartControls.SmartTabControl();
            this.tabPageMakeReceipt = new DevExpress.XtraTab.XtraTabPage();
            this.tabPageMakeReceiptList = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel4)).BeginInit();
            this.smartPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deInboundDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deInboundDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMakeReceipt)).BeginInit();
            this.tabMakeReceipt.SuspendLayout();
            this.tabPageMakeReceipt.SuspendLayout();
            this.tabPageMakeReceiptList.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 725);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1134, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabMakeReceipt);
            this.pnlContent.Size = new System.Drawing.Size(1134, 729);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1439, 758);
            // 
            // grdToolRequestReceipt
            // 
            this.grdToolRequestReceipt.Caption = "치공구 제작 입고 목록:";
            this.grdToolRequestReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdToolRequestReceipt.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdToolRequestReceipt.IsUsePaging = false;
            this.grdToolRequestReceipt.LanguageKey = "TOOLRECEIPTLIST";
            this.grdToolRequestReceipt.Location = new System.Drawing.Point(0, 36);
            this.grdToolRequestReceipt.Margin = new System.Windows.Forms.Padding(0);
            this.grdToolRequestReceipt.Name = "grdToolRequestReceipt";
            this.grdToolRequestReceipt.ShowBorder = true;
            this.grdToolRequestReceipt.ShowStatusBar = false;
            this.grdToolRequestReceipt.Size = new System.Drawing.Size(469, 336);
            this.grdToolRequestReceipt.TabIndex = 103;
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.smartLabel1);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(469, 36);
            this.smartPanel1.TabIndex = 102;
            // 
            // smartLabel1
            // 
            this.smartLabel1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.smartLabel1.Appearance.Options.UseFont = true;
            this.smartLabel1.LanguageKey = "TOOLMAKERECEIPTLIST";
            this.smartLabel1.Location = new System.Drawing.Point(5, 5);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(114, 19);
            this.smartLabel1.TabIndex = 113;
            this.smartLabel1.Text = "치공구 제작 입고:";
            // 
            // grdInputToolRequestReceipt
            // 
            this.grdInputToolRequestReceipt.Caption = "제작입고:";
            this.grdInputToolRequestReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInputToolRequestReceipt.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInputToolRequestReceipt.IsUsePaging = false;
            this.grdInputToolRequestReceipt.LanguageKey = "MAKERECEIPT";
            this.grdInputToolRequestReceipt.Location = new System.Drawing.Point(0, 36);
            this.grdInputToolRequestReceipt.Margin = new System.Windows.Forms.Padding(0);
            this.grdInputToolRequestReceipt.Name = "grdInputToolRequestReceipt";
            this.grdInputToolRequestReceipt.ShowBorder = true;
            this.grdInputToolRequestReceipt.ShowStatusBar = false;
            this.grdInputToolRequestReceipt.Size = new System.Drawing.Size(1128, 664);
            this.grdInputToolRequestReceipt.TabIndex = 102;
            // 
            // smartPanel4
            // 
            this.smartPanel4.Controls.Add(this.lblInboundDate);
            this.smartPanel4.Controls.Add(this.deInboundDate);
            this.smartPanel4.Controls.Add(this.btnSearchTool);
            this.smartPanel4.Controls.Add(this.lblInputTitle);
            this.smartPanel4.Controls.Add(this.btnInitialize);
            this.smartPanel4.Controls.Add(this.btnErase);
            this.smartPanel4.Controls.Add(this.btnModify);
            this.smartPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel4.Location = new System.Drawing.Point(0, 0);
            this.smartPanel4.Name = "smartPanel4";
            this.smartPanel4.Size = new System.Drawing.Size(1128, 36);
            this.smartPanel4.TabIndex = 2;
            // 
            // lblInboundDate
            // 
            this.lblInboundDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInboundDate.Appearance.Options.UseTextOptions = true;
            this.lblInboundDate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblInboundDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblInboundDate.LanguageKey = "INBOUNDDATE";
            this.lblInboundDate.Location = new System.Drawing.Point(589, 4);
            this.lblInboundDate.Name = "lblInboundDate";
            this.lblInboundDate.Size = new System.Drawing.Size(84, 24);
            this.lblInboundDate.TabIndex = 5;
            this.lblInboundDate.Text = "입고일자:";
            this.lblInboundDate.Visible = false;
            // 
            // deInboundDate
            // 
            this.deInboundDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deInboundDate.EditValue = null;
            this.deInboundDate.LabelText = null;
            this.deInboundDate.LanguageKey = null;
            this.deInboundDate.Location = new System.Drawing.Point(679, 6);
            this.deInboundDate.Name = "deInboundDate";
            this.deInboundDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deInboundDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.deInboundDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deInboundDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.deInboundDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deInboundDate.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.deInboundDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deInboundDate.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            this.deInboundDate.Size = new System.Drawing.Size(96, 20);
            this.deInboundDate.TabIndex = 114;
            this.deInboundDate.Visible = false;
            // 
            // btnSearchTool
            // 
            this.btnSearchTool.AllowFocus = false;
            this.btnSearchTool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchTool.IsBusy = false;
            this.btnSearchTool.IsWrite = false;
            this.btnSearchTool.LanguageKey = "CHOICETOOL";
            this.btnSearchTool.Location = new System.Drawing.Point(957, 5);
            this.btnSearchTool.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSearchTool.Name = "btnSearchTool";
            this.btnSearchTool.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearchTool.Size = new System.Drawing.Size(80, 25);
            this.btnSearchTool.TabIndex = 111;
            this.btnSearchTool.Text = "Tool선택:";
            this.btnSearchTool.TooltipLanguageKey = "";
            this.btnSearchTool.Visible = false;
            // 
            // lblInputTitle
            // 
            this.lblInputTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblInputTitle.Appearance.Options.UseFont = true;
            this.lblInputTitle.LanguageKey = "MAKERECEIPT";
            this.lblInputTitle.Location = new System.Drawing.Point(5, 5);
            this.lblInputTitle.Name = "lblInputTitle";
            this.lblInputTitle.Size = new System.Drawing.Size(67, 19);
            this.lblInputTitle.TabIndex = 113;
            this.lblInputTitle.Text = "제작 입고:";
            // 
            // btnInitialize
            // 
            this.btnInitialize.AllowFocus = false;
            this.btnInitialize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInitialize.IsBusy = false;
            this.btnInitialize.IsWrite = false;
            this.btnInitialize.LanguageKey = "CLEAR";
            this.btnInitialize.Location = new System.Drawing.Point(785, 5);
            this.btnInitialize.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnInitialize.Size = new System.Drawing.Size(80, 25);
            this.btnInitialize.TabIndex = 112;
            this.btnInitialize.Text = "초기화:";
            this.btnInitialize.TooltipLanguageKey = "";
            this.btnInitialize.Visible = false;
            // 
            // btnErase
            // 
            this.btnErase.AllowFocus = false;
            this.btnErase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnErase.IsBusy = false;
            this.btnErase.IsWrite = false;
            this.btnErase.LanguageKey = "ERASE";
            this.btnErase.Location = new System.Drawing.Point(1043, 5);
            this.btnErase.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnErase.Name = "btnErase";
            this.btnErase.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnErase.Size = new System.Drawing.Size(80, 25);
            this.btnErase.TabIndex = 110;
            this.btnErase.Text = "삭제:";
            this.btnErase.TooltipLanguageKey = "";
            this.btnErase.Visible = false;
            // 
            // btnModify
            // 
            this.btnModify.AllowFocus = false;
            this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModify.IsBusy = false;
            this.btnModify.IsWrite = false;
            this.btnModify.LanguageKey = "SAVE";
            this.btnModify.Location = new System.Drawing.Point(871, 5);
            this.btnModify.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnModify.Name = "btnModify";
            this.btnModify.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnModify.Size = new System.Drawing.Size(80, 25);
            this.btnModify.TabIndex = 111;
            this.btnModify.Text = "저장:";
            this.btnModify.TooltipLanguageKey = "";
            this.btnModify.Visible = false;
            // 
            // tabMakeReceipt
            // 
            this.tabMakeReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMakeReceipt.Location = new System.Drawing.Point(0, 0);
            this.tabMakeReceipt.Name = "tabMakeReceipt";
            this.tabMakeReceipt.SelectedTabPage = this.tabPageMakeReceipt;
            this.tabMakeReceipt.Size = new System.Drawing.Size(1134, 729);
            this.tabMakeReceipt.TabIndex = 2;
            this.tabMakeReceipt.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageMakeReceipt,
            this.tabPageMakeReceiptList});
            // 
            // tabPageMakeReceipt
            // 
            this.tabPageMakeReceipt.Controls.Add(this.grdInputToolRequestReceipt);
            this.tabPageMakeReceipt.Controls.Add(this.smartPanel4);
            this.tabMakeReceipt.SetLanguageKey(this.tabPageMakeReceipt, "MAKERECEIPT");
            this.tabPageMakeReceipt.Name = "tabPageMakeReceipt";
            this.tabPageMakeReceipt.Size = new System.Drawing.Size(1128, 700);
            this.tabPageMakeReceipt.Text = "제작입고:";
            // 
            // tabPageMakeReceiptList
            // 
            this.tabPageMakeReceiptList.Controls.Add(this.grdToolRequestReceipt);
            this.tabPageMakeReceiptList.Controls.Add(this.smartPanel1);
            this.tabMakeReceipt.SetLanguageKey(this.tabPageMakeReceiptList, "TOOLMAKERECEIPTLIST");
            this.tabPageMakeReceiptList.Name = "tabPageMakeReceiptList";
            this.tabPageMakeReceiptList.Size = new System.Drawing.Size(469, 372);
            this.tabPageMakeReceiptList.Text = "제작입고현황:";
            // 
            // ReceiptRequestToolMaking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1459, 778);
            this.Name = "ReceiptRequestToolMaking";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel4)).EndInit();
            this.smartPanel4.ResumeLayout(false);
            this.smartPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deInboundDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deInboundDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMakeReceipt)).EndInit();
            this.tabMakeReceipt.ResumeLayout(false);
            this.tabPageMakeReceipt.ResumeLayout(false);
            this.tabPageMakeReceiptList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartBandedGrid grdToolRequestReceipt;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartBandedGrid grdInputToolRequestReceipt;
        private Framework.SmartControls.SmartPanel smartPanel4;
        private Framework.SmartControls.SmartButton btnSearchTool;
        private Framework.SmartControls.SmartLabel lblInputTitle;
        private Framework.SmartControls.SmartButton btnInitialize;
        private Framework.SmartControls.SmartButton btnErase;
        private Framework.SmartControls.SmartButton btnModify;
        private Framework.SmartControls.SmartDateEdit deInboundDate;
        private Framework.SmartControls.SmartLabel lblInboundDate;
        private Framework.SmartControls.SmartTabControl tabMakeReceipt;
        private DevExpress.XtraTab.XtraTabPage tabPageMakeReceipt;
        private DevExpress.XtraTab.XtraTabPage tabPageMakeReceiptList;
    }
}