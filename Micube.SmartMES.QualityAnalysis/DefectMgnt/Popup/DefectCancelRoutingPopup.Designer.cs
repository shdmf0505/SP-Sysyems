namespace Micube.SmartMES.QualityAnalysis
{
    partial class DefectCancelRoutingPopup
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
            this.grdCurrentRouting = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.gbxReworkRouting = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.popupReworkRouting = new Micube.Framework.SmartControls.SmartLabelSelectPopupEdit();
            this.txtRoutingVersion = new Micube.Framework.SmartControls.SmartTextBox();
            this.chkProductRouting = new Micube.Framework.SmartControls.SmartCheckBox();
            this.grdReworkRouting = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.cboReworkAfterArea = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.smartGroupBox2 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.cboResource = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnApply = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbxReworkRouting)).BeginInit();
            this.gbxReworkRouting.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupReworkRouting.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoutingVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkProductRouting.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboReworkAfterArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox2)).BeginInit();
            this.smartGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboResource.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(1033, 545);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.grdCurrentRouting, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.gbxReworkRouting, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.smartGroupBox1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.smartGroupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 2, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1033, 545);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // grdCurrentRouting
            // 
            this.grdCurrentRouting.Caption = "재작업 후 공정";
            this.grdCurrentRouting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCurrentRouting.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdCurrentRouting.IsUsePaging = false;
            this.grdCurrentRouting.LanguageKey = "ProcessAfterRework";
            this.grdCurrentRouting.Location = new System.Drawing.Point(521, 50);
            this.grdCurrentRouting.Margin = new System.Windows.Forms.Padding(0);
            this.grdCurrentRouting.Name = "grdCurrentRouting";
            this.grdCurrentRouting.ShowBorder = true;
            this.grdCurrentRouting.Size = new System.Drawing.Size(512, 460);
            this.grdCurrentRouting.TabIndex = 11;
            this.grdCurrentRouting.UseAutoBestFitColumns = false;
            // 
            // gbxReworkRouting
            // 
            this.gbxReworkRouting.Controls.Add(this.tableLayoutPanel2);
            this.gbxReworkRouting.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbxReworkRouting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxReworkRouting.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbxReworkRouting.LanguageKey = "ReworkRouting";
            this.gbxReworkRouting.Location = new System.Drawing.Point(0, 50);
            this.gbxReworkRouting.Margin = new System.Windows.Forms.Padding(0);
            this.gbxReworkRouting.Name = "gbxReworkRouting";
            this.gbxReworkRouting.ShowBorder = true;
            this.gbxReworkRouting.Size = new System.Drawing.Size(511, 460);
            this.gbxReworkRouting.TabIndex = 10;
            this.gbxReworkRouting.Text = "재작업 라우팅";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.grdReworkRouting, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 31);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(507, 427);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.flowLayoutPanel2, 3);
            this.flowLayoutPanel2.Controls.Add(this.popupReworkRouting);
            this.flowLayoutPanel2.Controls.Add(this.txtRoutingVersion);
            this.flowLayoutPanel2.Controls.Add(this.chkProductRouting);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(507, 30);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // popupReworkRouting
            // 
            this.popupReworkRouting.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.popupReworkRouting.Appearance.ForeColor = System.Drawing.Color.Red;
            this.popupReworkRouting.Appearance.Options.UseBackColor = true;
            this.popupReworkRouting.Appearance.Options.UseForeColor = true;
            this.popupReworkRouting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.popupReworkRouting.LabelText = "재작업 라우팅";
            this.popupReworkRouting.LanguageKey = "REWORKROUTING";
            this.popupReworkRouting.Location = new System.Drawing.Point(3, 3);
            this.popupReworkRouting.Name = "popupReworkRouting";
            this.popupReworkRouting.Size = new System.Drawing.Size(300, 20);
            this.popupReworkRouting.TabIndex = 10;
            // 
            // txtRoutingVersion
            // 
            this.txtRoutingVersion.LabelText = null;
            this.txtRoutingVersion.LanguageKey = null;
            this.txtRoutingVersion.Location = new System.Drawing.Point(309, 3);
            this.txtRoutingVersion.Name = "txtRoutingVersion";
            this.txtRoutingVersion.Properties.ReadOnly = true;
            this.txtRoutingVersion.Size = new System.Drawing.Size(48, 20);
            this.txtRoutingVersion.TabIndex = 8;
            // 
            // chkProductRouting
            // 
            this.chkProductRouting.LanguageKey = "PRODUCTROUTING";
            this.chkProductRouting.Location = new System.Drawing.Point(363, 3);
            this.chkProductRouting.Name = "chkProductRouting";
            this.chkProductRouting.Properties.Caption = "품목 라우팅";
            this.chkProductRouting.Size = new System.Drawing.Size(79, 19);
            this.chkProductRouting.TabIndex = 9;
            // 
            // grdReworkRouting
            // 
            this.grdReworkRouting.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.tableLayoutPanel2.SetColumnSpan(this.grdReworkRouting, 3);
            this.grdReworkRouting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReworkRouting.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdReworkRouting.IsUsePaging = false;
            this.grdReworkRouting.LanguageKey = null;
            this.grdReworkRouting.Location = new System.Drawing.Point(0, 30);
            this.grdReworkRouting.Margin = new System.Windows.Forms.Padding(0);
            this.grdReworkRouting.Name = "grdReworkRouting";
            this.grdReworkRouting.ShowBorder = false;
            this.grdReworkRouting.ShowButtonBar = false;
            this.grdReworkRouting.Size = new System.Drawing.Size(507, 397);
            this.grdReworkRouting.TabIndex = 1;
            this.grdReworkRouting.UseAutoBestFitColumns = false;
            // 
            // smartGroupBox1
            // 
            this.smartGroupBox1.Controls.Add(this.cboReworkAfterArea);
            this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartGroupBox1.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox1.Location = new System.Drawing.Point(521, 0);
            this.smartGroupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.smartGroupBox1.Name = "smartGroupBox1";
            this.smartGroupBox1.Padding = new System.Windows.Forms.Padding(3);
            this.smartGroupBox1.ShowBorder = true;
            this.smartGroupBox1.ShowCaption = false;
            this.smartGroupBox1.Size = new System.Drawing.Size(512, 40);
            this.smartGroupBox1.TabIndex = 9;
            this.smartGroupBox1.Text = "smartGroupBox1";
            // 
            // cboReworkAfterArea
            // 
            this.cboReworkAfterArea.Appearance.ForeColor = System.Drawing.Color.Red;
            this.cboReworkAfterArea.Appearance.Options.UseForeColor = true;
            this.cboReworkAfterArea.Dock = System.Windows.Forms.DockStyle.Left;
            this.cboReworkAfterArea.EditorWidth = "80%";
            this.cboReworkAfterArea.LabelText = "대상 자원";
            this.cboReworkAfterArea.LabelWidth = "20%";
            this.cboReworkAfterArea.LanguageKey = "TARGETRESOURCE";
            this.cboReworkAfterArea.Location = new System.Drawing.Point(5, 5);
            this.cboReworkAfterArea.Name = "cboReworkAfterArea";
            this.cboReworkAfterArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboReworkAfterArea.Properties.NullText = "";
            this.cboReworkAfterArea.Size = new System.Drawing.Size(330, 20);
            this.cboReworkAfterArea.TabIndex = 6;
            // 
            // smartGroupBox2
            // 
            this.smartGroupBox2.Controls.Add(this.cboResource);
            this.smartGroupBox2.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartGroupBox2.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.smartGroupBox2.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.smartGroupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.smartGroupBox2.Name = "smartGroupBox2";
            this.smartGroupBox2.Padding = new System.Windows.Forms.Padding(3);
            this.smartGroupBox2.ShowBorder = true;
            this.smartGroupBox2.ShowCaption = false;
            this.smartGroupBox2.Size = new System.Drawing.Size(511, 40);
            this.smartGroupBox2.TabIndex = 8;
            this.smartGroupBox2.Text = "smartGroupBox2";
            // 
            // cboResource
            // 
            this.cboResource.Appearance.ForeColor = System.Drawing.Color.Red;
            this.cboResource.Appearance.Options.UseForeColor = true;
            this.cboResource.Dock = System.Windows.Forms.DockStyle.Left;
            this.cboResource.EditorWidth = "80%";
            this.cboResource.LabelText = "대상 자원";
            this.cboResource.LabelWidth = "20%";
            this.cboResource.LanguageKey = "TARGETRESOURCE";
            this.cboResource.Location = new System.Drawing.Point(5, 5);
            this.cboResource.Name = "cboResource";
            this.cboResource.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboResource.Properties.NullText = "";
            this.cboResource.Size = new System.Drawing.Size(330, 20);
            this.cboResource.TabIndex = 6;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Controls.Add(this.btnApply);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(521, 510);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(512, 35);
            this.flowLayoutPanel1.TabIndex = 12;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(432, 7);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 7, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnApply
            // 
            this.btnApply.AllowFocus = false;
            this.btnApply.IsBusy = false;
            this.btnApply.IsWrite = false;
            this.btnApply.LanguageKey = "APPLY";
            this.btnApply.Location = new System.Drawing.Point(346, 7);
            this.btnApply.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnApply.Size = new System.Drawing.Size(80, 25);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "적용";
            this.btnApply.TooltipLanguageKey = "";
            // 
            // DefectCancelRoutingPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 565);
            this.Name = "DefectCancelRoutingPopup";
            this.Text = "라우팅 선택";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbxReworkRouting)).EndInit();
            this.gbxReworkRouting.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupReworkRouting.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoutingVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkProductRouting.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboReworkAfterArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox2)).EndInit();
            this.smartGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboResource.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartGroupBox smartGroupBox2;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private Framework.SmartControls.SmartGroupBox gbxReworkRouting;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Framework.SmartControls.SmartLabelSelectPopupEdit popupReworkRouting;
        private Framework.SmartControls.SmartTextBox txtRoutingVersion;
        private Framework.SmartControls.SmartCheckBox chkProductRouting;
        private Framework.SmartControls.SmartBandedGrid grdReworkRouting;
        private Framework.SmartControls.SmartBandedGrid grdCurrentRouting;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnApply;
        public Framework.SmartControls.SmartLabelComboBox cboResource;
        public Framework.SmartControls.SmartLabelComboBox cboReworkAfterArea;
    }
}