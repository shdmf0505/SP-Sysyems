namespace Micube.SmartMES.Commons
{
    partial class ShipmentInspectionMailPopup
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShipmentInspectionMailPopup));
            this.smartLayoutControl1 = new Micube.Framework.SmartControls.SmartLayoutControl();
            this.grdMain = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.cboMailType = new Micube.Framework.SmartControls.SmartComboBox();
            this.memoCustom = new Micube.Framework.SmartControls.SmartMemoEdit();
            this.memoInfo = new Micube.Framework.SmartControls.SmartMemoEdit();
            this.txtTitle = new Micube.Framework.SmartControls.SmartTextBox();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnSend = new Micube.Framework.SmartControls.SmartButton();
            this.ucFileInfo = new Micube.SmartMES.Commons.Controls.SmartFileProcessingControl();
            this.grdUser = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartLayoutControlGroup1 = new Micube.Framework.SmartControls.SmartLayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl1)).BeginInit();
            this.smartLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboMailType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoCustom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoInfo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartLayoutControl1);
            this.pnlMain.Location = new System.Drawing.Point(3, 3);
            this.pnlMain.Size = new System.Drawing.Size(1178, 755);
            // 
            // smartLayoutControl1
            // 
            this.smartLayoutControl1.Controls.Add(this.grdMain);
            this.smartLayoutControl1.Controls.Add(this.cboMailType);
            this.smartLayoutControl1.Controls.Add(this.memoCustom);
            this.smartLayoutControl1.Controls.Add(this.memoInfo);
            this.smartLayoutControl1.Controls.Add(this.txtTitle);
            this.smartLayoutControl1.Controls.Add(this.btnClose);
            this.smartLayoutControl1.Controls.Add(this.btnSend);
            this.smartLayoutControl1.Controls.Add(this.ucFileInfo);
            this.smartLayoutControl1.Controls.Add(this.grdUser);
            this.smartLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.smartLayoutControl1.Name = "smartLayoutControl1";
            this.smartLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(636, 510, 650, 400);
            this.smartLayoutControl1.Root = this.smartLayoutControlGroup1;
            this.smartLayoutControl1.Size = new System.Drawing.Size(1178, 755);
            this.smartLayoutControl1.TabIndex = 0;
            this.smartLayoutControl1.Text = "smartLayoutControl1";
            // 
            // grdMain
            // 
            this.grdMain.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMain.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMain.IsUsePaging = false;
            this.grdMain.LanguageKey = null;
            this.grdMain.Location = new System.Drawing.Point(526, 5);
            this.grdMain.Margin = new System.Windows.Forms.Padding(0);
            this.grdMain.Name = "grdMain";
            this.grdMain.ShowBorder = true;
            this.grdMain.Size = new System.Drawing.Size(647, 719);
            this.grdMain.TabIndex = 12;
            this.grdMain.UseAutoBestFitColumns = false;
            // 
            // cboMailType
            // 
            this.cboMailType.LabelText = null;
            this.cboMailType.LanguageKey = null;
            this.cboMailType.Location = new System.Drawing.Point(472, 259);
            this.cboMailType.Name = "cboMailType";
            this.cboMailType.PopupWidth = 0;
            this.cboMailType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboMailType.Properties.NullText = "";
            this.cboMailType.ShowHeader = true;
            this.cboMailType.Size = new System.Drawing.Size(50, 20);
            this.cboMailType.StyleController = this.smartLayoutControl1;
            this.cboMailType.TabIndex = 11;
            this.cboMailType.VisibleColumns = null;
            this.cboMailType.VisibleColumnsWidth = null;
            // 
            // memoCustom
            // 
            this.memoCustom.Location = new System.Drawing.Point(113, 414);
            this.memoCustom.Name = "memoCustom";
            this.memoCustom.Size = new System.Drawing.Size(409, 60);
            this.memoCustom.StyleController = this.smartLayoutControl1;
            this.memoCustom.TabIndex = 10;
            // 
            // memoInfo
            // 
            this.memoInfo.Location = new System.Drawing.Point(113, 283);
            this.memoInfo.Name = "memoInfo";
            this.memoInfo.Size = new System.Drawing.Size(409, 127);
            this.memoInfo.StyleController = this.smartLayoutControl1;
            this.memoInfo.TabIndex = 9;
            // 
            // txtTitle
            // 
            this.txtTitle.LabelText = null;
            this.txtTitle.LanguageKey = null;
            this.txtTitle.Location = new System.Drawing.Point(113, 259);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(247, 20);
            this.txtTitle.StyleController = this.smartLayoutControl1;
            this.txtTitle.TabIndex = 8;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.Location = new System.Drawing.Point(1075, 728);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(98, 22);
            this.btnClose.StyleController = this.smartLayoutControl1;
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "smartButton2";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnSend
            // 
            this.btnSend.AllowFocus = false;
            this.btnSend.IsBusy = false;
            this.btnSend.IsWrite = false;
            this.btnSend.Location = new System.Drawing.Point(973, 728);
            this.btnSend.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSend.Name = "btnSend";
            this.btnSend.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSend.Size = new System.Drawing.Size(98, 22);
            this.btnSend.StyleController = this.smartLayoutControl1;
            this.btnSend.TabIndex = 6;
            this.btnSend.Text = "smartButton1";
            this.btnSend.TooltipLanguageKey = "";
            // 
            // ucFileInfo
            // 
            this.ucFileInfo.countRows = false;
            this.ucFileInfo.executeFileAfterDown = false;
            this.ucFileInfo.LanguageKey = "FILELIST";
            this.ucFileInfo.Location = new System.Drawing.Point(5, 478);
            this.ucFileInfo.Margin = new System.Windows.Forms.Padding(0);
            this.ucFileInfo.Name = "ucFileInfo";
            this.ucFileInfo.showImage = false;
            this.ucFileInfo.Size = new System.Drawing.Size(517, 246);
            this.ucFileInfo.TabIndex = 5;
            this.ucFileInfo.UploadPath = "";
            this.ucFileInfo.UseCommentsColumn = true;
            // 
            // grdUser
            // 
            this.grdUser.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdUser.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdUser.IsUsePaging = false;
            this.grdUser.LanguageKey = null;
            this.grdUser.Location = new System.Drawing.Point(5, 5);
            this.grdUser.Margin = new System.Windows.Forms.Padding(0);
            this.grdUser.Name = "grdUser";
            this.grdUser.ShowBorder = true;
            this.grdUser.Size = new System.Drawing.Size(517, 250);
            this.grdUser.TabIndex = 4;
            this.grdUser.UseAutoBestFitColumns = false;
            // 
            // smartLayoutControlGroup1
            // 
            this.smartLayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.smartLayoutControlGroup1.GroupBordersVisible = false;
            this.smartLayoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.emptySpaceItem1});
            this.smartLayoutControlGroup1.Name = "Root";
            this.smartLayoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.smartLayoutControlGroup1.Size = new System.Drawing.Size(1178, 755);
            this.smartLayoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdUser;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(521, 254);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ucFileInfo;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 473);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(521, 250);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnSend;
            this.layoutControlItem3.Location = new System.Drawing.Point(968, 723);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(102, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnClose;
            this.layoutControlItem4.Location = new System.Drawing.Point(1070, 723);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(102, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 723);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(968, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtTitle;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 254);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(359, 24);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(105, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.memoInfo;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 278);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(521, 131);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(105, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.memoCustom;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 409);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(521, 64);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(105, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.cboMailType;
            this.layoutControlItem8.Location = new System.Drawing.Point(359, 254);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(162, 24);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(105, 14);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.grdMain;
            this.layoutControlItem9.Location = new System.Drawing.Point(521, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(651, 723);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // ShipmentInspectionMailPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Name = "ShipmentInspectionMailPopup";
            this.Padding = new System.Windows.Forms.Padding(3);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControl1)).EndInit();
            this.smartLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboMailType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoCustom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoInfo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartLayoutControl smartLayoutControl1;
        private Framework.SmartControls.SmartLayoutControlGroup smartLayoutControlGroup1;
        private Framework.SmartControls.SmartBandedGrid grdUser;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private Controls.SmartFileProcessingControl ucFileInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnSend;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private Framework.SmartControls.SmartTextBox txtTitle;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private Framework.SmartControls.SmartMemoEdit memoCustom;
        private Framework.SmartControls.SmartMemoEdit memoInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private Framework.SmartControls.SmartComboBox cboMailType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private Framework.SmartControls.SmartBandedGrid grdMain;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
    }
}
