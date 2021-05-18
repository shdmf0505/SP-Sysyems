namespace Micube.SmartMES.Commons
{
    partial class SendInspectionMailPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendInspectionMailPopup));
            this.grdUserList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnDelete = new Micube.Framework.SmartControls.SmartButton();
            this.smartLayoutControl1 = new Micube.Framework.SmartControls.SmartLayoutControl();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnSend = new Micube.Framework.SmartControls.SmartButton();
            this.btnAdd = new Micube.Framework.SmartControls.SmartButton();
            this.grpContents = new Micube.Framework.SmartControls.SmartGroupBox();
            this.smartLayoutControl2 = new Micube.Framework.SmartControls.SmartLayoutControl();
            this.memoCustom = new Micube.Framework.SmartControls.SmartMemoEdit();
            this.memoInfo = new Micube.Framework.SmartControls.SmartMemoEdit();
            this.txtTitle = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartLayoutControlGroup2 = new Micube.Framework.SmartControls.SmartLayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ucFileInfo = new Micube.SmartMES.Commons.Controls.SmartFileProcessingControl();
            this.smartLayoutControlGroup1 = new Micube.Framework.SmartControls.SmartLayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl1)).BeginInit();
            this.smartLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpContents)).BeginInit();
            this.grpContents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl2)).BeginInit();
            this.smartLayoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoCustom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoInfo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartLayoutControl1);
            this.pnlMain.Location = new System.Drawing.Point(3, 3);
            this.pnlMain.Size = new System.Drawing.Size(978, 555);
            // 
            // grdUserList
            // 
            this.grdUserList.Caption = "";
            this.grdUserList.IsUsePaging = false;
            this.grdUserList.LanguageKey = "RECEIVERLIST";
            this.grdUserList.Location = new System.Drawing.Point(2, 28);
            this.grdUserList.Margin = new System.Windows.Forms.Padding(0);
            this.grdUserList.Name = "grdUserList";
            this.grdUserList.ShowBorder = true;
            this.grdUserList.Size = new System.Drawing.Size(401, 223);
            this.grdUserList.TabIndex = 0;
            this.grdUserList.UseAutoBestFitColumns = false;
            // 
            // btnDelete
            // 
            this.btnDelete.AllowFocus = false;
            this.btnDelete.IsBusy = false;
            this.btnDelete.IsWrite = false;
            this.btnDelete.Location = new System.Drawing.Point(314, 2);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDelete.Size = new System.Drawing.Size(89, 22);
            this.btnDelete.StyleController = this.smartLayoutControl1;
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "삭제";
            this.btnDelete.TooltipLanguageKey = "";
            // 
            // smartLayoutControl1
            // 
            this.smartLayoutControl1.Controls.Add(this.btnClose);
            this.smartLayoutControl1.Controls.Add(this.grdUserList);
            this.smartLayoutControl1.Controls.Add(this.btnSend);
            this.smartLayoutControl1.Controls.Add(this.btnDelete);
            this.smartLayoutControl1.Controls.Add(this.btnAdd);
            this.smartLayoutControl1.Controls.Add(this.grpContents);
            this.smartLayoutControl1.Controls.Add(this.ucFileInfo);
            this.smartLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.smartLayoutControl1.Name = "smartLayoutControl1";
            this.smartLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(732, 301, 650, 400);
            this.smartLayoutControl1.Root = this.smartLayoutControlGroup1;
            this.smartLayoutControl1.Size = new System.Drawing.Size(978, 555);
            this.smartLayoutControl1.TabIndex = 1;
            this.smartLayoutControl1.Text = "smartLayoutControl1";
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.Location = new System.Drawing.Point(887, 531);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(89, 22);
            this.btnClose.StyleController = this.smartLayoutControl1;
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnSend
            // 
            this.btnSend.AllowFocus = false;
            this.btnSend.IsBusy = false;
            this.btnSend.IsWrite = false;
            this.btnSend.Location = new System.Drawing.Point(794, 531);
            this.btnSend.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.btnSend.Name = "btnSend";
            this.btnSend.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSend.Size = new System.Drawing.Size(89, 22);
            this.btnSend.StyleController = this.smartLayoutControl1;
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "보내기";
            this.btnSend.TooltipLanguageKey = "";
            // 
            // btnAdd
            // 
            this.btnAdd.AllowFocus = false;
            this.btnAdd.IsBusy = false;
            this.btnAdd.IsWrite = false;
            this.btnAdd.Location = new System.Drawing.Point(221, 2);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnAdd.Size = new System.Drawing.Size(89, 22);
            this.btnAdd.StyleController = this.smartLayoutControl1;
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "추가";
            this.btnAdd.TooltipLanguageKey = "";
            // 
            // grpContents
            // 
            this.grpContents.Controls.Add(this.smartLayoutControl2);
            this.grpContents.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grpContents.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grpContents.LanguageKey = "COMMENTS";
            this.grpContents.Location = new System.Drawing.Point(407, 2);
            this.grpContents.Margin = new System.Windows.Forms.Padding(0);
            this.grpContents.Name = "grpContents";
            this.grpContents.ShowBorder = true;
            this.grpContents.Size = new System.Drawing.Size(569, 525);
            this.grpContents.TabIndex = 6;
            this.grpContents.Text = "smartGroupBox1";
            // 
            // smartLayoutControl2
            // 
            this.smartLayoutControl2.Controls.Add(this.memoCustom);
            this.smartLayoutControl2.Controls.Add(this.memoInfo);
            this.smartLayoutControl2.Controls.Add(this.txtTitle);
            this.smartLayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLayoutControl2.Location = new System.Drawing.Point(2, 31);
            this.smartLayoutControl2.Name = "smartLayoutControl2";
            this.smartLayoutControl2.Root = this.smartLayoutControlGroup2;
            this.smartLayoutControl2.Size = new System.Drawing.Size(565, 492);
            this.smartLayoutControl2.TabIndex = 8;
            this.smartLayoutControl2.Text = "smartLayoutControl2";
            // 
            // memoCustom
            // 
            this.memoCustom.Location = new System.Drawing.Point(5, 383);
            this.memoCustom.Name = "memoCustom";
            this.memoCustom.Size = new System.Drawing.Size(555, 104);
            this.memoCustom.StyleController = this.smartLayoutControl2;
            this.memoCustom.TabIndex = 6;
            // 
            // memoInfo
            // 
            this.memoInfo.Location = new System.Drawing.Point(5, 46);
            this.memoInfo.Name = "memoInfo";
            this.memoInfo.Size = new System.Drawing.Size(555, 316);
            this.memoInfo.StyleController = this.smartLayoutControl2;
            this.memoInfo.TabIndex = 5;
            // 
            // txtTitle
            // 
            this.txtTitle.LabelText = null;
            this.txtTitle.LanguageKey = null;
            this.txtTitle.Location = new System.Drawing.Point(120, 5);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(440, 20);
            this.txtTitle.StyleController = this.smartLayoutControl2;
            this.txtTitle.TabIndex = 4;
            // 
            // smartLayoutControlGroup2
            // 
            this.smartLayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.smartLayoutControlGroup2.GroupBordersVisible = false;
            this.smartLayoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10});
            this.smartLayoutControlGroup2.Name = "smartLayoutControlGroup2";
            this.smartLayoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.smartLayoutControlGroup2.Size = new System.Drawing.Size(565, 492);
            this.smartLayoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.txtTitle;
            this.smartLayoutControl2.SetLanguageKey(this.layoutControlItem8, "TITLE");
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(559, 24);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(112, 14);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.memoInfo;
            this.smartLayoutControl2.SetLanguageKey(this.layoutControlItem9, "ABNORMALINFO");
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(559, 337);
            this.layoutControlItem9.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(112, 14);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.memoCustom;
            this.smartLayoutControl2.SetLanguageKey(this.layoutControlItem10, "REMARK");
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 361);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(559, 125);
            this.layoutControlItem10.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem10.TextSize = new System.Drawing.Size(112, 14);
            // 
            // ucFileInfo
            // 
            this.ucFileInfo.countRows = false;
            this.ucFileInfo.executeFileAfterDown = false;
            this.ucFileInfo.LanguageKey = "ATTACHEDFILE";
            this.ucFileInfo.Location = new System.Drawing.Point(2, 255);
            this.ucFileInfo.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.ucFileInfo.Name = "ucFileInfo";
            this.ucFileInfo.showImage = false;
            this.ucFileInfo.Size = new System.Drawing.Size(401, 272);
            this.ucFileInfo.TabIndex = 7;
            this.ucFileInfo.UploadPath = "";
            this.ucFileInfo.UseCommentsColumn = true;
            // 
            // smartLayoutControlGroup1
            // 
            this.smartLayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.smartLayoutControlGroup1.GroupBordersVisible = false;
            this.smartLayoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.emptySpaceItem2});
            this.smartLayoutControlGroup1.Name = "Root";
            this.smartLayoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.smartLayoutControlGroup1.Size = new System.Drawing.Size(978, 555);
            this.smartLayoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnAdd;
            this.layoutControlItem1.Location = new System.Drawing.Point(219, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(93, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(93, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnDelete;
            this.layoutControlItem2.Location = new System.Drawing.Point(312, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(93, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(93, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.grdUserList;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(405, 227);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ucFileInfo;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 253);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(405, 276);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(219, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.grpContents;
            this.layoutControlItem5.Location = new System.Drawing.Point(405, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(573, 529);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnSend;
            this.layoutControlItem6.Location = new System.Drawing.Point(792, 529);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(93, 26);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(93, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnClose;
            this.layoutControlItem7.Location = new System.Drawing.Point(885, 529);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(93, 26);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(93, 26);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(93, 26);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 529);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(792, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // SendInspectionMailPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.LanguageKey = "EMAIL";
            this.Name = "SendInspectionMailPopup";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "MailPopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl1)).EndInit();
            this.smartLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpContents)).EndInit();
            this.grpContents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl2)).EndInit();
            this.smartLayoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoCustom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoInfo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Framework.SmartControls.SmartBandedGrid grdUserList;
        private Framework.SmartControls.SmartButton btnDelete;
        private Framework.SmartControls.SmartButton btnAdd;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnSend;
        private Framework.SmartControls.SmartGroupBox grpContents;
        private Controls.SmartFileProcessingControl ucFileInfo;
        private Framework.SmartControls.SmartLayoutControl smartLayoutControl1;
        private Framework.SmartControls.SmartLayoutControlGroup smartLayoutControlGroup1;
        private Framework.SmartControls.SmartLayoutControl smartLayoutControl2;
        private Framework.SmartControls.SmartMemoEdit memoCustom;
        private Framework.SmartControls.SmartMemoEdit memoInfo;
        private Framework.SmartControls.SmartTextBox txtTitle;
        private Framework.SmartControls.SmartLayoutControlGroup smartLayoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}