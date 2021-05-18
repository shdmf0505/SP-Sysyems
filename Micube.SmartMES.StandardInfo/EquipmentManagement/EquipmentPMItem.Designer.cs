namespace Micube.SmartMES.StandardInfo
{
    partial class EquipmentPMItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EquipmentPMItem));
            this.tlpSplitTable = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.pnlPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.grdEquipment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.spcSpliter = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.tlpPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPMList = new Micube.Framework.SmartControls.SmartLabel();
            this.cboPMList = new Micube.Framework.SmartControls.SmartComboBox();
            this.spcSmartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.pnlPanel2 = new Micube.Framework.SmartControls.SmartPanel();
            this.grdPMItemNotMapping = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.grdPMItemMapping = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSelectedItemDelete = new Micube.Framework.SmartControls.SmartButton();
            this.btnSelectItemAdd = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tlpSplitTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPanel1)).BeginInit();
            this.pnlPanel1.SuspendLayout();
            this.tlpPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPMList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spcSmartSpliterContainer1)).BeginInit();
            this.spcSmartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPanel2)).BeginInit();
            this.pnlPanel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 470);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(809, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tlpSplitTable);
            this.pnlContent.Size = new System.Drawing.Size(809, 474);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1114, 503);
            // 
            // tlpSplitTable
            // 
            this.tlpSplitTable.ColumnCount = 1;
            this.tlpSplitTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSplitTable.Controls.Add(this.pnlPanel1, 0, 0);
            this.tlpSplitTable.Controls.Add(this.spcSpliter, 0, 1);
            this.tlpSplitTable.Controls.Add(this.tlpPanel1, 0, 2);
            this.tlpSplitTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSplitTable.Location = new System.Drawing.Point(0, 0);
            this.tlpSplitTable.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpSplitTable.Name = "tlpSplitTable";
            this.tlpSplitTable.RowCount = 3;
            this.tlpSplitTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSplitTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpSplitTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSplitTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpSplitTable.Size = new System.Drawing.Size(809, 474);
            this.tlpSplitTable.TabIndex = 2;
            // 
            // pnlPanel1
            // 
            this.pnlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlPanel1.Controls.Add(this.grdEquipment);
            this.pnlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPanel1.Location = new System.Drawing.Point(0, 0);
            this.pnlPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPanel1.Name = "pnlPanel1";
            this.pnlPanel1.Size = new System.Drawing.Size(809, 232);
            this.pnlPanel1.TabIndex = 4;
            // 
            // grdEquipment
            // 
            this.grdEquipment.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdEquipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEquipment.IsUsePaging = false;
            this.grdEquipment.LanguageKey = "MAINEQUIPMENTLIST";
            this.grdEquipment.Location = new System.Drawing.Point(0, 0);
            this.grdEquipment.Margin = new System.Windows.Forms.Padding(0);
            this.grdEquipment.Name = "grdEquipment";
            this.grdEquipment.ShowBorder = true;
            this.grdEquipment.Size = new System.Drawing.Size(809, 232);
            this.grdEquipment.TabIndex = 0;
            // 
            // spcSpliter
            // 
            this.spcSpliter.Dock = System.Windows.Forms.DockStyle.Top;
            this.spcSpliter.Location = new System.Drawing.Point(0, 232);
            this.spcSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcSpliter.Name = "spcSpliter";
            this.spcSpliter.Size = new System.Drawing.Size(809, 5);
            this.spcSpliter.TabIndex = 0;
            this.spcSpliter.TabStop = false;
            // 
            // tlpPanel1
            // 
            this.tlpPanel1.ColumnCount = 1;
            this.tlpPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPanel1.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.tlpPanel1.Controls.Add(this.spcSmartSpliterContainer1, 0, 1);
            this.tlpPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPanel1.Location = new System.Drawing.Point(0, 242);
            this.tlpPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpPanel1.Name = "tlpPanel1";
            this.tlpPanel1.RowCount = 2;
            this.tlpPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPanel1.Size = new System.Drawing.Size(809, 232);
            this.tlpPanel1.TabIndex = 5;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.lblPMList);
            this.flowLayoutPanel2.Controls.Add(this.cboPMList);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(181, 25);
            this.flowLayoutPanel2.TabIndex = 7;
            // 
            // lblPMList
            // 
            this.lblPMList.LanguageKey = "MAINTITEMCLASSID";
            this.lblPMList.Location = new System.Drawing.Point(0, 0);
            this.lblPMList.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.lblPMList.Name = "lblPMList";
            this.lblPMList.Size = new System.Drawing.Size(16, 14);
            this.lblPMList.TabIndex = 0;
            this.lblPMList.Text = "PM";
            // 
            // cboPMList
            // 
            this.cboPMList.LabelText = null;
            this.cboPMList.LanguageKey = null;
            this.cboPMList.Location = new System.Drawing.Point(22, 0);
            this.cboPMList.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.cboPMList.Name = "cboPMList";
            this.cboPMList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPMList.Properties.NullText = "";
            this.cboPMList.ShowHeader = true;
            this.cboPMList.Size = new System.Drawing.Size(159, 20);
            this.cboPMList.TabIndex = 1;
            // 
            // spcSmartSpliterContainer1
            // 
            this.spcSmartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcSmartSpliterContainer1.Location = new System.Drawing.Point(0, 25);
            this.spcSmartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcSmartSpliterContainer1.Name = "spcSmartSpliterContainer1";
            this.spcSmartSpliterContainer1.Panel1.Controls.Add(this.pnlPanel2);
            this.spcSmartSpliterContainer1.Panel1.Text = "Panel1";
            this.spcSmartSpliterContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.spcSmartSpliterContainer1.Panel2.Text = "Panel2";
            this.spcSmartSpliterContainer1.Size = new System.Drawing.Size(809, 207);
            this.spcSmartSpliterContainer1.SplitterPosition = 621;
            this.spcSmartSpliterContainer1.TabIndex = 6;
            this.spcSmartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // pnlPanel2
            // 
            this.pnlPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlPanel2.Controls.Add(this.grdPMItemNotMapping);
            this.pnlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPanel2.Location = new System.Drawing.Point(0, 0);
            this.pnlPanel2.Name = "pnlPanel2";
            this.pnlPanel2.Size = new System.Drawing.Size(621, 207);
            this.pnlPanel2.TabIndex = 0;
            // 
            // grdPMItemNotMapping
            // 
            this.grdPMItemNotMapping.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdPMItemNotMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPMItemNotMapping.IsUsePaging = false;
            this.grdPMItemNotMapping.LanguageKey = "EQUIPMENTPMITEMLIST";
            this.grdPMItemNotMapping.Location = new System.Drawing.Point(0, 0);
            this.grdPMItemNotMapping.Margin = new System.Windows.Forms.Padding(0);
            this.grdPMItemNotMapping.Name = "grdPMItemNotMapping";
            this.grdPMItemNotMapping.ShowBorder = true;
            this.grdPMItemNotMapping.Size = new System.Drawing.Size(621, 207);
            this.grdPMItemNotMapping.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.grdPMItemMapping, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(183, 207);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // grdPMItemMapping
            // 
            this.grdPMItemMapping.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdPMItemMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPMItemMapping.IsUsePaging = false;
            this.grdPMItemMapping.LanguageKey = "EQUIPMENTPMITEMLISTMAPPING";
            this.grdPMItemMapping.Location = new System.Drawing.Point(40, 0);
            this.grdPMItemMapping.Margin = new System.Windows.Forms.Padding(0);
            this.grdPMItemMapping.Name = "grdPMItemMapping";
            this.grdPMItemMapping.ShowBorder = true;
            this.grdPMItemMapping.Size = new System.Drawing.Size(143, 207);
            this.grdPMItemMapping.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.btnSelectedItemDelete, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnSelectItemAdd, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 33);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(40, 140);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // btnSelectedItemDelete
            // 
            this.btnSelectedItemDelete.AllowFocus = false;
            this.btnSelectedItemDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectedItemDelete.ImageOptions.Image")));
            this.btnSelectedItemDelete.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSelectedItemDelete.IsBusy = false;
            this.btnSelectedItemDelete.Location = new System.Drawing.Point(0, 80);
            this.btnSelectedItemDelete.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSelectedItemDelete.Name = "btnSelectedItemDelete";
            this.btnSelectedItemDelete.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSelectedItemDelete.Size = new System.Drawing.Size(30, 30);
            this.btnSelectedItemDelete.TabIndex = 2;
            this.btnSelectedItemDelete.TooltipLanguageKey = "";
            // 
            // btnSelectItemAdd
            // 
            this.btnSelectItemAdd.AllowFocus = false;
            this.btnSelectItemAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectItemAdd.ImageOptions.Image")));
            this.btnSelectItemAdd.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSelectItemAdd.IsBusy = false;
            this.btnSelectItemAdd.Location = new System.Drawing.Point(0, 30);
            this.btnSelectItemAdd.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSelectItemAdd.Name = "btnSelectItemAdd";
            this.btnSelectItemAdd.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSelectItemAdd.Size = new System.Drawing.Size(30, 30);
            this.btnSelectItemAdd.TabIndex = 1;
            this.btnSelectItemAdd.TooltipLanguageKey = "";
            // 
            // EquipmentPMItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 523);
            this.Name = "EquipmentPMItem";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tlpSplitTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlPanel1)).EndInit();
            this.pnlPanel1.ResumeLayout(false);
            this.tlpPanel1.ResumeLayout(false);
            this.tlpPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPMList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spcSmartSpliterContainer1)).EndInit();
            this.spcSmartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlPanel2)).EndInit();
            this.pnlPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpSplitTable;
        private Framework.SmartControls.SmartSpliterControl spcSpliter;
        private Framework.SmartControls.SmartPanel pnlPanel1;
        private Framework.SmartControls.SmartBandedGrid grdEquipment;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpPanel1;
        private Framework.SmartControls.SmartSpliterContainer spcSmartSpliterContainer1;
        private Framework.SmartControls.SmartPanel pnlPanel2;
        private Framework.SmartControls.SmartBandedGrid grdPMItemNotMapping;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Framework.SmartControls.SmartBandedGrid grdPMItemMapping;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartButton btnSelectedItemDelete;
        private Framework.SmartControls.SmartButton btnSelectItemAdd;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Framework.SmartControls.SmartLabel lblPMList;
        private Framework.SmartControls.SmartComboBox cboPMList;
    }
}