namespace Micube.SmartMES.ToolManagement
{
    partial class UpdateToolRepairSend
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
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdToolUpdateSend = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.btnModify = new Micube.Framework.SmartControls.SmartButton();
            this.grdInputToolUpdateSend = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel4 = new Micube.Framework.SmartControls.SmartPanel();
            this.txtSendor = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtSendSequence = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblSendDate = new Micube.Framework.SmartControls.SmartLabel();
            this.deSendDate = new Micube.Framework.SmartControls.SmartDateEdit();
            this.lblSendUser = new Micube.Framework.SmartControls.SmartLabel();
            this.btnSearchTool = new Micube.Framework.SmartControls.SmartButton();
            this.lblInputTitle = new Micube.Framework.SmartControls.SmartLabel();
            this.btnInitialize = new Micube.Framework.SmartControls.SmartButton();
            this.btnErase = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel4)).BeginInit();
            this.smartPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendSequence.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deSendDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deSendDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
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
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdToolUpdateSend);
            this.smartSpliterContainer1.Panel1.Controls.Add(this.smartPanel1);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdInputToolUpdateSend);
            this.smartSpliterContainer1.Panel2.Controls.Add(this.smartPanel4);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(1134, 729);
            this.smartSpliterContainer1.SplitterPosition = 347;
            this.smartSpliterContainer1.TabIndex = 2;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdToolUpdateSend
            // 
            this.grdToolUpdateSend.Caption = "치공구 수정출고목록:";
            this.grdToolUpdateSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdToolUpdateSend.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdToolUpdateSend.IsUsePaging = false;
            this.grdToolUpdateSend.LanguageKey = "TOOLUPDATESENDLIST";
            this.grdToolUpdateSend.Location = new System.Drawing.Point(0, 36);
            this.grdToolUpdateSend.Margin = new System.Windows.Forms.Padding(0);
            this.grdToolUpdateSend.Name = "grdToolUpdateSend";
            this.grdToolUpdateSend.ShowBorder = true;
            this.grdToolUpdateSend.Size = new System.Drawing.Size(1134, 311);
            this.grdToolUpdateSend.TabIndex = 103;
            this.grdToolUpdateSend.UseAutoBestFitColumns = false;
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.smartLabel1);
            this.smartPanel1.Controls.Add(this.btnModify);
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
            this.smartLabel1.LanguageKey = "TOOLUPDATESEND";
            this.smartLabel1.Location = new System.Drawing.Point(5, 5);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(109, 19);
            this.smartLabel1.TabIndex = 113;
            this.smartLabel1.Text = "치공구 수정출고:";
            // 
            // btnModify
            // 
            this.btnModify.AllowFocus = false;
            this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModify.IsBusy = false;
            this.btnModify.IsWrite = false;
            this.btnModify.LanguageKey = "SAVE";
            this.btnModify.Location = new System.Drawing.Point(1048, 5);
            this.btnModify.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnModify.Name = "btnModify";
            this.btnModify.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnModify.Size = new System.Drawing.Size(80, 25);
            this.btnModify.TabIndex = 111;
            this.btnModify.Text = "저장:";
            this.btnModify.TooltipLanguageKey = "";
            this.btnModify.Visible = false;
            // 
            // grdInputToolUpdateSend
            // 
            this.grdInputToolUpdateSend.Caption = "수정출고:";
            this.grdInputToolUpdateSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInputToolUpdateSend.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInputToolUpdateSend.IsUsePaging = false;
            this.grdInputToolUpdateSend.LanguageKey = "UPDATESENDTOOLLIST";
            this.grdInputToolUpdateSend.Location = new System.Drawing.Point(0, 36);
            this.grdInputToolUpdateSend.Margin = new System.Windows.Forms.Padding(0);
            this.grdInputToolUpdateSend.Name = "grdInputToolUpdateSend";
            this.grdInputToolUpdateSend.ShowBorder = true;
            this.grdInputToolUpdateSend.Size = new System.Drawing.Size(1134, 341);
            this.grdInputToolUpdateSend.TabIndex = 102;
            this.grdInputToolUpdateSend.UseAutoBestFitColumns = false;
            // 
            // smartPanel4
            // 
            this.smartPanel4.Controls.Add(this.txtSendor);
            this.smartPanel4.Controls.Add(this.txtSendSequence);
            this.smartPanel4.Controls.Add(this.lblSendDate);
            this.smartPanel4.Controls.Add(this.deSendDate);
            this.smartPanel4.Controls.Add(this.lblSendUser);
            this.smartPanel4.Controls.Add(this.btnSearchTool);
            this.smartPanel4.Controls.Add(this.lblInputTitle);
            this.smartPanel4.Controls.Add(this.btnInitialize);
            this.smartPanel4.Controls.Add(this.btnErase);
            this.smartPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel4.Location = new System.Drawing.Point(0, 0);
            this.smartPanel4.Name = "smartPanel4";
            this.smartPanel4.Size = new System.Drawing.Size(1134, 36);
            this.smartPanel4.TabIndex = 2;
            // 
            // txtSendor
            // 
            this.txtSendor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSendor.LabelText = null;
            this.txtSendor.LanguageKey = null;
            this.txtSendor.Location = new System.Drawing.Point(679, 8);
            this.txtSendor.Name = "txtSendor";
            this.txtSendor.Size = new System.Drawing.Size(99, 20);
            this.txtSendor.TabIndex = 123;
            this.txtSendor.Visible = false;
            // 
            // txtSendSequence
            // 
            this.txtSendSequence.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSendSequence.LabelText = null;
            this.txtSendSequence.LanguageKey = null;
            this.txtSendSequence.Location = new System.Drawing.Point(566, 8);
            this.txtSendSequence.Name = "txtSendSequence";
            this.txtSendSequence.Size = new System.Drawing.Size(32, 20);
            this.txtSendSequence.TabIndex = 122;
            this.txtSendSequence.Visible = false;
            // 
            // lblSendDate
            // 
            this.lblSendDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSendDate.Appearance.Options.UseTextOptions = true;
            this.lblSendDate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblSendDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSendDate.LanguageKey = "SENDDATE";
            this.lblSendDate.Location = new System.Drawing.Point(372, 6);
            this.lblSendDate.Name = "lblSendDate";
            this.lblSendDate.Size = new System.Drawing.Size(84, 24);
            this.lblSendDate.TabIndex = 119;
            this.lblSendDate.Text = "출고일자:";
            this.lblSendDate.Visible = false;
            // 
            // deSendDate
            // 
            this.deSendDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deSendDate.EditValue = null;
            this.deSendDate.LabelText = null;
            this.deSendDate.LanguageKey = null;
            this.deSendDate.Location = new System.Drawing.Point(462, 8);
            this.deSendDate.Name = "deSendDate";
            this.deSendDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deSendDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deSendDate.Size = new System.Drawing.Size(98, 20);
            this.deSendDate.TabIndex = 121;
            this.deSendDate.Visible = false;
            // 
            // lblSendUser
            // 
            this.lblSendUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSendUser.Appearance.Options.UseTextOptions = true;
            this.lblSendUser.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblSendUser.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSendUser.LanguageKey = "SENDOR";
            this.lblSendUser.Location = new System.Drawing.Point(609, 5);
            this.lblSendUser.Name = "lblSendUser";
            this.lblSendUser.Size = new System.Drawing.Size(64, 24);
            this.lblSendUser.TabIndex = 120;
            this.lblSendUser.Text = "출고자:";
            this.lblSendUser.Visible = false;
            // 
            // btnSearchTool
            // 
            this.btnSearchTool.AllowFocus = false;
            this.btnSearchTool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchTool.IsBusy = false;
            this.btnSearchTool.IsWrite = false;
            this.btnSearchTool.LanguageKey = "CHOICETOOL";
            this.btnSearchTool.Location = new System.Drawing.Point(963, 5);
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
            this.lblInputTitle.LanguageKey = "UPDATESENDTOOLLIST";
            this.lblInputTitle.Location = new System.Drawing.Point(5, 5);
            this.lblInputTitle.Name = "lblInputTitle";
            this.lblInputTitle.Size = new System.Drawing.Size(62, 19);
            this.lblInputTitle.TabIndex = 113;
            this.lblInputTitle.Text = "수정출고:";
            // 
            // btnInitialize
            // 
            this.btnInitialize.AllowFocus = false;
            this.btnInitialize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInitialize.IsBusy = false;
            this.btnInitialize.IsWrite = false;
            this.btnInitialize.LanguageKey = "CLEAR";
            this.btnInitialize.Location = new System.Drawing.Point(877, 5);
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
            this.btnErase.Location = new System.Drawing.Point(1049, 5);
            this.btnErase.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnErase.Name = "btnErase";
            this.btnErase.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnErase.Size = new System.Drawing.Size(80, 25);
            this.btnErase.TabIndex = 110;
            this.btnErase.Text = "삭제:";
            this.btnErase.TooltipLanguageKey = "";
            this.btnErase.Visible = false;
            // 
            // UpdateToolRepairSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1459, 778);
            this.Name = "UpdateToolRepairSend";
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
            ((System.ComponentModel.ISupportInitialize)(this.txtSendor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendSequence.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deSendDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deSendDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdToolUpdateSend;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartBandedGrid grdInputToolUpdateSend;
        private Framework.SmartControls.SmartPanel smartPanel4;
        private Framework.SmartControls.SmartButton btnSearchTool;
        private Framework.SmartControls.SmartLabel lblInputTitle;
        private Framework.SmartControls.SmartButton btnInitialize;
        private Framework.SmartControls.SmartButton btnErase;
        private Framework.SmartControls.SmartButton btnModify;
        private Framework.SmartControls.SmartTextBox txtSendor;
        private Framework.SmartControls.SmartTextBox txtSendSequence;
        private Framework.SmartControls.SmartLabel lblSendDate;
        private Framework.SmartControls.SmartDateEdit deSendDate;
        private Framework.SmartControls.SmartLabel lblSendUser;
    }
}