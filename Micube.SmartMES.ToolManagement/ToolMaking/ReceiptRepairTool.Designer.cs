namespace Micube.SmartMES.ToolManagement
{
    partial class ReceiptRepairTool
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
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup1 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceiptRepairTool));
            Micube.Framework.SmartControls.ConditionCollection conditionCollection1 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection2 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdToolRepairReceipt = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.grdInputToolRepairReceipt = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel4 = new Micube.Framework.SmartControls.SmartPanel();
            this.popEditVendor = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.lblRepairVendor = new Micube.Framework.SmartControls.SmartLabel();
            this.txtReceiptUser = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtReceiptSequence = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblSendDate = new Micube.Framework.SmartControls.SmartLabel();
            this.deReceiptDate = new Micube.Framework.SmartControls.SmartDateEdit();
            this.lblSendUser = new Micube.Framework.SmartControls.SmartLabel();
            this.btnSearchTool = new Micube.Framework.SmartControls.SmartButton();
            this.lblInputTitle = new Micube.Framework.SmartControls.SmartLabel();
            this.btnInitialize = new Micube.Framework.SmartControls.SmartButton();
            this.btnErase = new Micube.Framework.SmartControls.SmartButton();
            this.btnModify = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel4)).BeginInit();
            this.smartPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popEditVendor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiptUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiptSequence.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deReceiptDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deReceiptDate.Properties)).BeginInit();
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
            this.pnlContent.Controls.Add(this.smartSpliterContainer1);
            this.pnlContent.Size = new System.Drawing.Size(1134, 729);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1439, 758);
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Horizontal = false;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdToolRepairReceipt);
            this.smartSpliterContainer1.Panel1.Controls.Add(this.smartPanel1);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdInputToolRepairReceipt);
            this.smartSpliterContainer1.Panel2.Controls.Add(this.smartPanel4);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(1134, 729);
            this.smartSpliterContainer1.SplitterPosition = 347;
            this.smartSpliterContainer1.TabIndex = 3;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdToolRepairReceipt
            // 
            this.grdToolRepairReceipt.Caption = "치공구 수리입고목록:";
            this.grdToolRepairReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdToolRepairReceipt.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdToolRepairReceipt.IsUsePaging = false;
            this.grdToolRepairReceipt.LanguageKey = "TOOLREPAIRRECEIPTLIST";
            this.grdToolRepairReceipt.Location = new System.Drawing.Point(0, 36);
            this.grdToolRepairReceipt.Margin = new System.Windows.Forms.Padding(0);
            this.grdToolRepairReceipt.Name = "grdToolRepairReceipt";
            this.grdToolRepairReceipt.ShowBorder = true;
            this.grdToolRepairReceipt.Size = new System.Drawing.Size(1134, 311);
            this.grdToolRepairReceipt.TabIndex = 103;
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.smartLabel1);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(1134, 36);
            this.smartPanel1.TabIndex = 102;
            // 
            // smartLabel1
            // 
            this.smartLabel1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.smartLabel1.Appearance.Options.UseFont = true;
            this.smartLabel1.LanguageKey = "TOOLREPAIRRECEIPT";
            this.smartLabel1.Location = new System.Drawing.Point(5, 5);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(109, 19);
            this.smartLabel1.TabIndex = 113;
            this.smartLabel1.Text = "치공구 수리입고:";
            // 
            // grdInputToolRepairReceipt
            // 
            this.grdInputToolRepairReceipt.Caption = "수리입고:";
            this.grdInputToolRepairReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInputToolRepairReceipt.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInputToolRepairReceipt.IsUsePaging = false;
            this.grdInputToolRepairReceipt.LanguageKey = "TOOLREPAIRRECEIPT";
            this.grdInputToolRepairReceipt.Location = new System.Drawing.Point(0, 37);
            this.grdInputToolRepairReceipt.Margin = new System.Windows.Forms.Padding(0);
            this.grdInputToolRepairReceipt.Name = "grdInputToolRepairReceipt";
            this.grdInputToolRepairReceipt.ShowBorder = true;
            this.grdInputToolRepairReceipt.Size = new System.Drawing.Size(1134, 340);
            this.grdInputToolRepairReceipt.TabIndex = 102;
            // 
            // smartPanel4
            // 
            this.smartPanel4.Controls.Add(this.popEditVendor);
            this.smartPanel4.Controls.Add(this.lblRepairVendor);
            this.smartPanel4.Controls.Add(this.txtReceiptUser);
            this.smartPanel4.Controls.Add(this.txtReceiptSequence);
            this.smartPanel4.Controls.Add(this.lblSendDate);
            this.smartPanel4.Controls.Add(this.deReceiptDate);
            this.smartPanel4.Controls.Add(this.lblSendUser);
            this.smartPanel4.Controls.Add(this.btnSearchTool);
            this.smartPanel4.Controls.Add(this.lblInputTitle);
            this.smartPanel4.Controls.Add(this.btnInitialize);
            this.smartPanel4.Controls.Add(this.btnErase);
            this.smartPanel4.Controls.Add(this.btnModify);
            this.smartPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel4.Location = new System.Drawing.Point(0, 0);
            this.smartPanel4.Name = "smartPanel4";
            this.smartPanel4.Size = new System.Drawing.Size(1134, 37);
            this.smartPanel4.TabIndex = 2;
            // 
            // popEditVendor
            // 
            this.popEditVendor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.popEditVendor.LabelText = null;
            this.popEditVendor.LanguageKey = null;
            this.popEditVendor.Location = new System.Drawing.Point(322, 8);
            this.popEditVendor.Name = "popEditVendor";
            conditionItemSelectPopup1.ApplySelection = null;
            conditionItemSelectPopup1.AutoFillColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup1.AutoFillColumnNames")));
            conditionItemSelectPopup1.CanOkNoSelection = true;
            conditionItemSelectPopup1.ClearButtonRealOnly = false;
            conditionItemSelectPopup1.ClearButtonVisible = true;
            conditionItemSelectPopup1.ConditionDefaultId = null;
            conditionItemSelectPopup1.ConditionLayoutType = DevExpress.XtraLayout.Utils.LayoutType.Horizontal;
            conditionItemSelectPopup1.ConditionRequireId = "";
            conditionItemSelectPopup1.Conditions = conditionCollection1;
            conditionItemSelectPopup1.CustomPopup = null;
            conditionItemSelectPopup1.CustomValidate = null;
            conditionItemSelectPopup1.DefaultDisplayValue = null;
            conditionItemSelectPopup1.DefaultValue = null;
            conditionItemSelectPopup1.DisplayFieldName = "";
            conditionItemSelectPopup1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            conditionItemSelectPopup1.GreatThenEqual = false;
            conditionItemSelectPopup1.GreatThenId = "";
            conditionItemSelectPopup1.GridColumns = conditionCollection2;
            conditionItemSelectPopup1.Id = null;
            conditionItemSelectPopup1.InitAction = null;
            conditionItemSelectPopup1.IsEnabled = true;
            conditionItemSelectPopup1.IsHidden = false;
            conditionItemSelectPopup1.IsImmediatlyUpdate = true;
            conditionItemSelectPopup1.IsKeyColumn = false;
            conditionItemSelectPopup1.IsMultiGrid = false;
            conditionItemSelectPopup1.IsReadOnly = false;
            conditionItemSelectPopup1.IsRequired = false;
            conditionItemSelectPopup1.IsSearchOnLoading = true;
            conditionItemSelectPopup1.IsUseRowCheckByMouseDrag = false;
            conditionItemSelectPopup1.LabelText = null;
            conditionItemSelectPopup1.LanguageKey = null;
            conditionItemSelectPopup1.LessThenEqual = false;
            conditionItemSelectPopup1.LessThenId = "";
            conditionItemSelectPopup1.NoSelectionMessageId = "";
            conditionItemSelectPopup1.PopupButtonStyle = Micube.Framework.SmartControls.PopupButtonStyles.Ok_Cancel;
            conditionItemSelectPopup1.PopupCustomValidation = null;
            conditionItemSelectPopup1.Position = 0D;
            conditionItemSelectPopup1.QueryPopup = null;
            conditionItemSelectPopup1.RelationIds = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup1.RelationIds")));
            conditionItemSelectPopup1.ResultAction = null;
            conditionItemSelectPopup1.ResultCount = 1;
            conditionItemSelectPopup1.SearchButtonReadOnly = false;
            conditionItemSelectPopup1.SearchQuery = null;
            conditionItemSelectPopup1.SearchText = null;
            conditionItemSelectPopup1.SearchTextControlId = null;
            conditionItemSelectPopup1.SelectionQuery = null;
            conditionItemSelectPopup1.ShowSearchButton = true;
            conditionItemSelectPopup1.TextAlignment = Micube.Framework.SmartControls.TextAlignment.Default;
            conditionItemSelectPopup1.Title = null;
            conditionItemSelectPopup1.ToolTip = null;
            conditionItemSelectPopup1.ToolTipLanguageKey = null;
            conditionItemSelectPopup1.ValueFieldName = "";
            conditionItemSelectPopup1.WindowSize = new System.Drawing.Size(800, 500);
            this.popEditVendor.SelectPopupCondition = conditionItemSelectPopup1;
            this.popEditVendor.Size = new System.Drawing.Size(32, 20);
            this.popEditVendor.TabIndex = 121;
            this.popEditVendor.Visible = false;
            // 
            // lblRepairVendor
            // 
            this.lblRepairVendor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRepairVendor.Appearance.Options.UseTextOptions = true;
            this.lblRepairVendor.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblRepairVendor.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblRepairVendor.LanguageKey = "MAKEVENDOR";
            this.lblRepairVendor.Location = new System.Drawing.Point(264, 6);
            this.lblRepairVendor.Name = "lblRepairVendor";
            this.lblRepairVendor.Size = new System.Drawing.Size(52, 24);
            this.lblRepairVendor.TabIndex = 119;
            this.lblRepairVendor.Text = "수리업체:";
            this.lblRepairVendor.Visible = false;
            // 
            // txtReceiptUser
            // 
            this.txtReceiptUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceiptUser.LabelText = null;
            this.txtReceiptUser.LanguageKey = null;
            this.txtReceiptUser.Location = new System.Drawing.Point(241, 6);
            this.txtReceiptUser.Name = "txtReceiptUser";
            this.txtReceiptUser.Size = new System.Drawing.Size(17, 20);
            this.txtReceiptUser.TabIndex = 118;
            this.txtReceiptUser.Visible = false;
            // 
            // txtReceiptSequence
            // 
            this.txtReceiptSequence.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceiptSequence.LabelText = null;
            this.txtReceiptSequence.LanguageKey = null;
            this.txtReceiptSequence.Location = new System.Drawing.Point(157, 8);
            this.txtReceiptSequence.Name = "txtReceiptSequence";
            this.txtReceiptSequence.Size = new System.Drawing.Size(29, 20);
            this.txtReceiptSequence.TabIndex = 117;
            this.txtReceiptSequence.Visible = false;
            // 
            // lblSendDate
            // 
            this.lblSendDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSendDate.Appearance.Options.UseTextOptions = true;
            this.lblSendDate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblSendDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSendDate.LanguageKey = "RECEIPTDATE";
            this.lblSendDate.Location = new System.Drawing.Point(73, 4);
            this.lblSendDate.Name = "lblSendDate";
            this.lblSendDate.Size = new System.Drawing.Size(46, 24);
            this.lblSendDate.TabIndex = 115;
            this.lblSendDate.Text = "입고일자:";
            this.lblSendDate.Visible = false;
            // 
            // deReceiptDate
            // 
            this.deReceiptDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deReceiptDate.EditValue = null;
            this.deReceiptDate.LabelText = null;
            this.deReceiptDate.LanguageKey = null;
            this.deReceiptDate.Location = new System.Drawing.Point(125, 8);
            this.deReceiptDate.Name = "deReceiptDate";
            this.deReceiptDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deReceiptDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deReceiptDate.Size = new System.Drawing.Size(26, 20);
            this.deReceiptDate.TabIndex = 116;
            this.deReceiptDate.Visible = false;
            // 
            // lblSendUser
            // 
            this.lblSendUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSendUser.Appearance.Options.UseTextOptions = true;
            this.lblSendUser.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblSendUser.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSendUser.LanguageKey = "RECEIPTUSER";
            this.lblSendUser.Location = new System.Drawing.Point(192, 6);
            this.lblSendUser.Name = "lblSendUser";
            this.lblSendUser.Size = new System.Drawing.Size(43, 24);
            this.lblSendUser.TabIndex = 115;
            this.lblSendUser.Text = "입고자:";
            this.lblSendUser.Visible = false;
            // 
            // btnSearchTool
            // 
            this.btnSearchTool.AllowFocus = false;
            this.btnSearchTool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchTool.IsBusy = false;
            this.btnSearchTool.IsWrite = false;
            this.btnSearchTool.LanguageKey = "CHOICETOOL";
            this.btnSearchTool.Location = new System.Drawing.Point(958, 5);
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
            this.lblInputTitle.LanguageKey = "TOOLREPAIRRECEIPT";
            this.lblInputTitle.Location = new System.Drawing.Point(5, 5);
            this.lblInputTitle.Name = "lblInputTitle";
            this.lblInputTitle.Size = new System.Drawing.Size(62, 19);
            this.lblInputTitle.TabIndex = 113;
            this.lblInputTitle.Text = "수리입고:";
            // 
            // btnInitialize
            // 
            this.btnInitialize.AllowFocus = false;
            this.btnInitialize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInitialize.IsBusy = false;
            this.btnInitialize.IsWrite = false;
            this.btnInitialize.LanguageKey = "CLEAR";
            this.btnInitialize.Location = new System.Drawing.Point(786, 5);
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
            this.btnErase.Location = new System.Drawing.Point(1044, 5);
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
            this.btnModify.Location = new System.Drawing.Point(872, 5);
            this.btnModify.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnModify.Name = "btnModify";
            this.btnModify.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnModify.Size = new System.Drawing.Size(80, 25);
            this.btnModify.TabIndex = 111;
            this.btnModify.Text = "저장:";
            this.btnModify.TooltipLanguageKey = "";
            this.btnModify.Visible = false;
            // 
            // ReceiptRepairTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1459, 778);
            this.Name = "ReceiptRepairTool";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel4)).EndInit();
            this.smartPanel4.ResumeLayout(false);
            this.smartPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popEditVendor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiptUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiptSequence.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deReceiptDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deReceiptDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdToolRepairReceipt;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartBandedGrid grdInputToolRepairReceipt;
        private Framework.SmartControls.SmartPanel smartPanel4;
        private Framework.SmartControls.SmartSelectPopupEdit popEditVendor;
        private Framework.SmartControls.SmartLabel lblRepairVendor;
        private Framework.SmartControls.SmartTextBox txtReceiptUser;
        private Framework.SmartControls.SmartTextBox txtReceiptSequence;
        private Framework.SmartControls.SmartLabel lblSendDate;
        private Framework.SmartControls.SmartDateEdit deReceiptDate;
        private Framework.SmartControls.SmartLabel lblSendUser;
        private Framework.SmartControls.SmartButton btnSearchTool;
        private Framework.SmartControls.SmartLabel lblInputTitle;
        private Framework.SmartControls.SmartButton btnInitialize;
        private Framework.SmartControls.SmartButton btnErase;
        private Framework.SmartControls.SmartButton btnModify;
    }
}