namespace Micube.SmartMES.Commons.Controls
{
    partial class ucReworkRouting
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.gbxReworkRouting = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtReworkRouting = new Micube.Framework.SmartControls.SmartLabelSelectPopupEdit();
            this.txtRoutingVersion = new Micube.Framework.SmartControls.SmartTextBox();
            this.chkProductRouting = new Micube.Framework.SmartControls.SmartCheckBox();
            this.grdReworkRouting = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartGroupBox2 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.cboResource = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.grdCurrentRouting = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartGroupBox3 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.cboResourceReturn = new Micube.Framework.SmartControls.SmartLabelComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbxReworkRouting)).BeginInit();
            this.gbxReworkRouting.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReworkRouting.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoutingVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkProductRouting.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox2)).BeginInit();
            this.smartGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboResource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox3)).BeginInit();
            this.smartGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboResourceReturn.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.gbxReworkRouting);
            this.smartSpliterContainer1.Panel1.Controls.Add(this.smartGroupBox2);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdCurrentRouting);
            this.smartSpliterContainer1.Panel2.Controls.Add(this.smartGroupBox3);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(1060, 389);
            this.smartSpliterContainer1.SplitterPosition = 609;
            this.smartSpliterContainer1.TabIndex = 6;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // gbxReworkRouting
            // 
            this.gbxReworkRouting.Controls.Add(this.tableLayoutPanel1);
            this.gbxReworkRouting.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gbxReworkRouting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxReworkRouting.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gbxReworkRouting.LanguageKey = "ReworkRouting";
            this.gbxReworkRouting.Location = new System.Drawing.Point(0, 34);
            this.gbxReworkRouting.Name = "gbxReworkRouting";
            this.gbxReworkRouting.ShowBorder = true;
            this.gbxReworkRouting.Size = new System.Drawing.Size(609, 355);
            this.gbxReworkRouting.TabIndex = 1;
            this.gbxReworkRouting.Text = "재작업 라우팅";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grdReworkRouting, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 31);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(605, 322);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel2, 3);
            this.flowLayoutPanel2.Controls.Add(this.txtReworkRouting);
            this.flowLayoutPanel2.Controls.Add(this.txtRoutingVersion);
            this.flowLayoutPanel2.Controls.Add(this.chkProductRouting);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(605, 30);
            this.flowLayoutPanel2.TabIndex = 0;
            this.flowLayoutPanel2.WrapContents = false;
            // 
            // txtReworkRouting
            // 
            this.txtReworkRouting.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.txtReworkRouting.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtReworkRouting.Appearance.Options.UseBackColor = true;
            this.txtReworkRouting.Appearance.Options.UseForeColor = true;
            this.txtReworkRouting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReworkRouting.LanguageKey = "REWORKROUTING";
            this.txtReworkRouting.Location = new System.Drawing.Point(3, 3);
            this.txtReworkRouting.Name = "txtReworkRouting";
            this.txtReworkRouting.Size = new System.Drawing.Size(300, 20);
            this.txtReworkRouting.TabIndex = 10;
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
            this.chkProductRouting.Size = new System.Drawing.Size(148, 19);
            this.chkProductRouting.TabIndex = 9;
            // 
            // grdReworkRouting
            // 
            this.grdReworkRouting.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.tableLayoutPanel1.SetColumnSpan(this.grdReworkRouting, 3);
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
            this.grdReworkRouting.Size = new System.Drawing.Size(605, 292);
            this.grdReworkRouting.TabIndex = 1;
            this.grdReworkRouting.UseAutoBestFitColumns = false;
            // 
            // smartGroupBox2
            // 
            this.smartGroupBox2.Controls.Add(this.cboResource);
            this.smartGroupBox2.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartGroupBox2.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.smartGroupBox2.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.smartGroupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.smartGroupBox2.Name = "smartGroupBox2";
            this.smartGroupBox2.Padding = new System.Windows.Forms.Padding(3);
            this.smartGroupBox2.ShowBorder = true;
            this.smartGroupBox2.ShowCaption = false;
            this.smartGroupBox2.Size = new System.Drawing.Size(609, 34);
            this.smartGroupBox2.TabIndex = 8;
            this.smartGroupBox2.Text = "smartGroupBox2";
            // 
            // cboResource
            // 
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
            this.cboResource.Size = new System.Drawing.Size(339, 20);
            this.cboResource.TabIndex = 6;
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
            this.grdCurrentRouting.Location = new System.Drawing.Point(0, 34);
            this.grdCurrentRouting.Margin = new System.Windows.Forms.Padding(0);
            this.grdCurrentRouting.Name = "grdCurrentRouting";
            this.grdCurrentRouting.ShowBorder = true;
            this.grdCurrentRouting.Size = new System.Drawing.Size(446, 355);
            this.grdCurrentRouting.TabIndex = 5;
            this.grdCurrentRouting.UseAutoBestFitColumns = false;
            // 
            // smartGroupBox3
            // 
            this.smartGroupBox3.Controls.Add(this.cboResourceReturn);
            this.smartGroupBox3.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartGroupBox3.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.smartGroupBox3.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.smartGroupBox3.Margin = new System.Windows.Forms.Padding(0);
            this.smartGroupBox3.Name = "smartGroupBox3";
            this.smartGroupBox3.Padding = new System.Windows.Forms.Padding(3);
            this.smartGroupBox3.ShowBorder = true;
            this.smartGroupBox3.ShowCaption = false;
            this.smartGroupBox3.Size = new System.Drawing.Size(446, 34);
            this.smartGroupBox3.TabIndex = 9;
            this.smartGroupBox3.Text = "smartGroupBox3";
            // 
            // cboResourceReturn
            // 
            this.cboResourceReturn.Dock = System.Windows.Forms.DockStyle.Left;
            this.cboResourceReturn.EditorWidth = "80%";
            this.cboResourceReturn.LabelText = "대상 자원";
            this.cboResourceReturn.LabelWidth = "20%";
            this.cboResourceReturn.LanguageKey = "TARGETRESOURCE";
            this.cboResourceReturn.Location = new System.Drawing.Point(5, 5);
            this.cboResourceReturn.Name = "cboResourceReturn";
            this.cboResourceReturn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboResourceReturn.Properties.NullText = "";
            this.cboResourceReturn.Size = new System.Drawing.Size(339, 20);
            this.cboResourceReturn.TabIndex = 6;
            // 
            // ucReworkRouting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.smartSpliterContainer1);
            this.Name = "ucReworkRouting";
            this.Size = new System.Drawing.Size(1060, 389);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbxReworkRouting)).EndInit();
            this.gbxReworkRouting.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtReworkRouting.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoutingVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkProductRouting.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox2)).EndInit();
            this.smartGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboResource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox3)).EndInit();
            this.smartGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboResourceReturn.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartGroupBox gbxReworkRouting;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Framework.SmartControls.SmartLabelSelectPopupEdit txtReworkRouting;
        private Framework.SmartControls.SmartTextBox txtRoutingVersion;
        private Framework.SmartControls.SmartCheckBox chkProductRouting;
        private Framework.SmartControls.SmartBandedGrid grdReworkRouting;
        private Framework.SmartControls.SmartGroupBox smartGroupBox2;
        private Framework.SmartControls.SmartLabelComboBox cboResource;
        private Framework.SmartControls.SmartBandedGrid grdCurrentRouting;
        private Framework.SmartControls.SmartGroupBox smartGroupBox3;
        private Framework.SmartControls.SmartLabelComboBox cboResourceReturn;
    }
}
