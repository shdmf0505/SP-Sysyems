namespace Micube.SmartMES.ProcessManagement
{
    partial class MultiLotSend
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
            this.spcSpliter = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.cboTransitArea = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.txtArea = new Micube.Framework.SmartControls.SmartLabelSelectPopupEdit();
            this.txtLotId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.grdLotInfo = new Micube.SmartMES.Commons.Controls.SmartLotInfoGrid();
            this.tabMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgMessage = new DevExpress.XtraTab.XtraTabPage();
            this.ucMessage = new Micube.SmartMES.ProcessManagement.usLotMessage();
            this.tpgComment = new DevExpress.XtraTab.XtraTabPage();
            this.grdComment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgProcessSpec = new DevExpress.XtraTab.XtraTabPage();
            this.grdProcessSpec = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTransitArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tpgMessage.SuspendLayout();
            this.tpgComment.SuspendLayout();
            this.tpgProcessSpec.SuspendLayout();
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
            this.pnlToolbar.Size = new System.Drawing.Size(1061, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabMain);
            this.pnlContent.Controls.Add(this.grdLotInfo);
            this.pnlContent.Controls.Add(this.smartSpliterControl1);
            this.pnlContent.Controls.Add(this.grdLotList);
            this.pnlContent.Controls.Add(this.smartPanel1);
            this.pnlContent.Controls.Add(this.spcSpliter);
            this.pnlContent.Size = new System.Drawing.Size(1061, 806);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1061, 835);
            // 
            // spcSpliter
            // 
            this.spcSpliter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.spcSpliter.Location = new System.Drawing.Point(0, 801);
            this.spcSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcSpliter.Name = "spcSpliter";
            this.spcSpliter.Size = new System.Drawing.Size(1061, 5);
            this.spcSpliter.TabIndex = 8;
            this.spcSpliter.TabStop = false;
            // 
            // cboTransitArea
            // 
            this.cboTransitArea.LabelText = "인계작업장";
            this.cboTransitArea.LabelWidth = "24%";
            this.cboTransitArea.LanguageKey = "TRANSITAREA";
            this.cboTransitArea.Location = new System.Drawing.Point(641, 9);
            this.cboTransitArea.Margin = new System.Windows.Forms.Padding(0);
            this.cboTransitArea.Name = "cboTransitArea";
            this.cboTransitArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTransitArea.Properties.NullText = "";
            this.cboTransitArea.Size = new System.Drawing.Size(257, 20);
            this.cboTransitArea.TabIndex = 10;
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.cboTransitArea);
            this.smartPanel1.Controls.Add(this.txtArea);
            this.smartPanel1.Controls.Add(this.txtLotId);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.smartPanel1.Size = new System.Drawing.Size(1061, 37);
            this.smartPanel1.TabIndex = 19;
            // 
            // txtArea
            // 
            this.txtArea.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtArea.Appearance.Options.UseForeColor = true;
            this.txtArea.EditorWidth = "70%";
            this.txtArea.LabelText = "작업장";
            this.txtArea.LabelWidth = "30%";
            this.txtArea.LanguageKey = "AREA";
            this.txtArea.Location = new System.Drawing.Point(7, 9);
            this.txtArea.Margin = new System.Windows.Forms.Padding(0);
            this.txtArea.Name = "txtArea";
            this.txtArea.Size = new System.Drawing.Size(269, 20);
            this.txtArea.TabIndex = 1;
            // 
            // txtLotId
            // 
            this.txtLotId.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtLotId.Appearance.Options.UseForeColor = true;
            this.txtLotId.EditorWidth = "80%";
            this.txtLotId.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txtLotId.LabelText = "LOT NO";
            this.txtLotId.LabelWidth = "20%";
            this.txtLotId.LanguageKey = "LOTID";
            this.txtLotId.Location = new System.Drawing.Point(292, 9);
            this.txtLotId.Margin = new System.Windows.Forms.Padding(0);
            this.txtLotId.Name = "txtLotId";
            this.txtLotId.Size = new System.Drawing.Size(337, 20);
            this.txtLotId.TabIndex = 2;
            // 
            // grdLotList
            // 
            this.grdLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLotList.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdLotList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLotList.IsUsePaging = false;
            this.grdLotList.LanguageKey = "WIPLIST";
            this.grdLotList.Location = new System.Drawing.Point(0, 37);
            this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotList.Name = "grdLotList";
            this.grdLotList.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.grdLotList.ShowBorder = true;
            this.grdLotList.Size = new System.Drawing.Size(1061, 355);
            this.grdLotList.TabIndex = 20;
            this.grdLotList.UseAutoBestFitColumns = false;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl1.Location = new System.Drawing.Point(0, 392);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(1061, 5);
            this.smartSpliterControl1.TabIndex = 21;
            this.smartSpliterControl1.TabStop = false;
            // 
            // grdLotInfo
            // 
            this.grdLotInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdLotInfo.Location = new System.Drawing.Point(0, 397);
            this.grdLotInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotInfo.Name = "grdLotInfo";
            this.grdLotInfo.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.grdLotInfo.Size = new System.Drawing.Size(1061, 90);
            this.grdLotInfo.TabIndex = 22;
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 487);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.tabMain.SelectedTabPage = this.tpgMessage;
            this.tabMain.Size = new System.Drawing.Size(1061, 314);
            this.tabMain.TabIndex = 23;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgMessage,
            this.tpgComment,
            this.tpgProcessSpec});
            // 
            // tpgMessage
            // 
            this.tpgMessage.Controls.Add(this.ucMessage);
            this.tabMain.SetLanguageKey(this.tpgMessage, "MESSAGE");
            this.tpgMessage.Name = "tpgMessage";
            this.tpgMessage.Padding = new System.Windows.Forms.Padding(3);
            this.tpgMessage.Size = new System.Drawing.Size(1055, 285);
            this.tpgMessage.Text = "Message";
            // 
            // ucMessage
            // 
            this.ucMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMessage.Location = new System.Drawing.Point(3, 3);
            this.ucMessage.MessageDataSource = null;
            this.ucMessage.Name = "ucMessage";
            this.ucMessage.Size = new System.Drawing.Size(1049, 279);
            this.ucMessage.TabIndex = 0;
            // 
            // tpgComment
            // 
            this.tpgComment.Controls.Add(this.grdComment);
            this.tabMain.SetLanguageKey(this.tpgComment, "REMARKS");
            this.tpgComment.Name = "tpgComment";
            this.tpgComment.Padding = new System.Windows.Forms.Padding(3);
            this.tpgComment.Size = new System.Drawing.Size(750, 0);
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
            this.grdComment.Size = new System.Drawing.Size(744, 0);
            this.grdComment.TabIndex = 1;
            this.grdComment.UseAutoBestFitColumns = false;
            // 
            // tpgProcessSpec
            // 
            this.tpgProcessSpec.Controls.Add(this.grdProcessSpec);
            this.tabMain.SetLanguageKey(this.tpgProcessSpec, "PROCESSSPEC");
            this.tpgProcessSpec.Name = "tpgProcessSpec";
            this.tpgProcessSpec.Padding = new System.Windows.Forms.Padding(3);
            this.tpgProcessSpec.Size = new System.Drawing.Size(750, 0);
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
            this.grdProcessSpec.Size = new System.Drawing.Size(744, 0);
            this.grdProcessSpec.TabIndex = 1;
            this.grdProcessSpec.UseAutoBestFitColumns = false;
            // 
            // MultiLotSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 855);
            this.ConditionsVisible = false;
            this.Name = "MultiLotSend";
            this.ShowSaveCompleteMessage = false;
            this.Text = "Multi Lot Send";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTransitArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tpgMessage.ResumeLayout(false);
            this.tpgComment.ResumeLayout(false);
            this.tpgProcessSpec.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartSpliterControl spcSpliter;
        private Framework.SmartControls.SmartLabelComboBox cboTransitArea;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartLabelSelectPopupEdit txtArea;
        private Framework.SmartControls.SmartLabelTextBox txtLotId;
        private Framework.SmartControls.SmartBandedGrid grdLotList;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Commons.Controls.SmartLotInfoGrid grdLotInfo;
        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tpgMessage;
        private usLotMessage ucMessage;
        private DevExpress.XtraTab.XtraTabPage tpgComment;
        private Framework.SmartControls.SmartBandedGrid grdComment;
        private DevExpress.XtraTab.XtraTabPage tpgProcessSpec;
        private Framework.SmartControls.SmartBandedGrid grdProcessSpec;
    }
}