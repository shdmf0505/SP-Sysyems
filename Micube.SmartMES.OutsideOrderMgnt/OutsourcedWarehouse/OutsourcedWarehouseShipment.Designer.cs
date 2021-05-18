namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutsourcedWarehouseShipment
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
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup1 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            Micube.Framework.SmartControls.ConditionCollection conditionCollection1 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection2 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.tblWorkGubun = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdOutsourcedWarehouseShipment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.popupOspAreaid = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.lblOspAreaid = new Micube.Framework.SmartControls.SmartLabel();
            this.btnLotnoSearch = new Micube.Framework.SmartControls.SmartButton();
            this.smartLabel2 = new Micube.Framework.SmartControls.SmartLabel();
            this.lblOspsender = new Micube.Framework.SmartControls.SmartLabel();
            this.lblPlantid = new Micube.Framework.SmartControls.SmartLabel();
            this.cboPlantid = new Micube.Framework.SmartControls.SmartComboBox();
            this.txtOspsenderName = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtLotid = new Micube.Framework.SmartControls.SmartTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tblWorkGubun.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupOspAreaid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOspsenderName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotid.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(0, 36);
            this.pnlCondition.Size = new System.Drawing.Size(0, 0);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1226, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tblWorkGubun);
            this.pnlContent.Size = new System.Drawing.Size(1226, 911);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1226, 947);
            // 
            // tblWorkGubun
            // 
            this.tblWorkGubun.ColumnCount = 1;
            this.tblWorkGubun.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblWorkGubun.Controls.Add(this.grdOutsourcedWarehouseShipment, 0, 1);
            this.tblWorkGubun.Controls.Add(this.smartSplitTableLayoutPanel1, 0, 0);
            this.tblWorkGubun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblWorkGubun.Location = new System.Drawing.Point(0, 0);
            this.tblWorkGubun.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tblWorkGubun.Name = "tblWorkGubun";
            this.tblWorkGubun.RowCount = 2;
            this.tblWorkGubun.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblWorkGubun.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblWorkGubun.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblWorkGubun.Size = new System.Drawing.Size(1226, 911);
            this.tblWorkGubun.TabIndex = 0;
            // 
            // grdOutsourcedWarehouseShipment
            // 
            this.grdOutsourcedWarehouseShipment.Caption = "외주창고 출고 목록";
            this.grdOutsourcedWarehouseShipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOutsourcedWarehouseShipment.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOutsourcedWarehouseShipment.IsUsePaging = false;
            this.grdOutsourcedWarehouseShipment.LanguageKey = "OUTSOURCEDWAREHOUSESHIPMENTLIST";
            this.grdOutsourcedWarehouseShipment.Location = new System.Drawing.Point(0, 60);
            this.grdOutsourcedWarehouseShipment.Margin = new System.Windows.Forms.Padding(0);
            this.grdOutsourcedWarehouseShipment.Name = "grdOutsourcedWarehouseShipment";
            this.grdOutsourcedWarehouseShipment.ShowBorder = true;
            this.grdOutsourcedWarehouseShipment.Size = new System.Drawing.Size(1226, 851);
            this.grdOutsourcedWarehouseShipment.TabIndex = 7;
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 7;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.popupOspAreaid, 3, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblOspAreaid, 2, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.btnLotnoSearch, 6, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartLabel2, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblOspsender, 4, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblPlantid, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.cboPlantid, 1, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.txtOspsenderName, 5, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.txtLotid, 1, 1);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(1226, 60);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // popupOspAreaid
            // 
            this.popupOspAreaid.LabelText = null;
            this.popupOspAreaid.LanguageKey = null;
            this.popupOspAreaid.Location = new System.Drawing.Point(393, 3);
            this.popupOspAreaid.Name = "popupOspAreaid";
            conditionItemSelectPopup1.ApplySelection = null;
            conditionItemSelectPopup1.AutoFillColumnNames = null;
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
            conditionItemSelectPopup1.RelationIds = null;
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
            this.popupOspAreaid.SelectPopupCondition = conditionItemSelectPopup1;
            this.popupOspAreaid.Size = new System.Drawing.Size(194, 24);
            this.popupOspAreaid.TabIndex = 11;
            // 
            // lblOspAreaid
            // 
            this.lblOspAreaid.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblOspAreaid.Appearance.Options.UseForeColor = true;
            this.lblOspAreaid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOspAreaid.LanguageKey = "AREANAME";
            this.lblOspAreaid.Location = new System.Drawing.Point(273, 3);
            this.lblOspAreaid.Name = "lblOspAreaid";
            this.lblOspAreaid.Size = new System.Drawing.Size(114, 24);
            this.lblOspAreaid.TabIndex = 10;
            this.lblOspAreaid.Text = "작업장";
            // 
            // btnLotnoSearch
            // 
            this.btnLotnoSearch.AllowFocus = false;
            this.btnLotnoSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLotnoSearch.IsBusy = false;
            this.btnLotnoSearch.IsWrite = false;
            this.btnLotnoSearch.LanguageKey = "LOTNOSEARCH";
            this.btnLotnoSearch.Location = new System.Drawing.Point(1089, 30);
            this.btnLotnoSearch.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnLotnoSearch.Name = "btnLotnoSearch";
            this.btnLotnoSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnLotnoSearch.Size = new System.Drawing.Size(134, 25);
            this.btnLotnoSearch.TabIndex = 9;
            this.btnLotnoSearch.Text = "Lot No Search";
            this.btnLotnoSearch.TooltipLanguageKey = "";
            this.btnLotnoSearch.Visible = false;
            // 
            // smartLabel2
            // 
            this.smartLabel2.Location = new System.Drawing.Point(3, 33);
            this.smartLabel2.Name = "smartLabel2";
            this.smartLabel2.Size = new System.Drawing.Size(54, 18);
            this.smartLabel2.TabIndex = 0;
            this.smartLabel2.Text = "LOT NO";
            // 
            // lblOspsender
            // 
            this.lblOspsender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOspsender.LanguageKey = "OSPSENDUSER";
            this.lblOspsender.Location = new System.Drawing.Point(593, 3);
            this.lblOspsender.Name = "lblOspsender";
            this.lblOspsender.Size = new System.Drawing.Size(14, 24);
            this.lblOspsender.TabIndex = 0;
            this.lblOspsender.Text = "출고자";
            this.lblOspsender.Visible = false;
            // 
            // lblPlantid
            // 
            this.lblPlantid.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblPlantid.Appearance.Options.UseForeColor = true;
            this.lblPlantid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPlantid.LanguageKey = "PLANTID";
            this.lblPlantid.Location = new System.Drawing.Point(3, 3);
            this.lblPlantid.Name = "lblPlantid";
            this.lblPlantid.Size = new System.Drawing.Size(114, 24);
            this.lblPlantid.TabIndex = 0;
            this.lblPlantid.Text = "SITE";
            // 
            // cboPlantid
            // 
            this.cboPlantid.LabelText = null;
            this.cboPlantid.LanguageKey = null;
            this.cboPlantid.Location = new System.Drawing.Point(123, 3);
            this.cboPlantid.Name = "cboPlantid";
            this.cboPlantid.PopupWidth = 0;
            this.cboPlantid.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPlantid.Properties.NullText = "";
            this.cboPlantid.ShowHeader = true;
            this.cboPlantid.Size = new System.Drawing.Size(144, 24);
            this.cboPlantid.TabIndex = 1;
            this.cboPlantid.VisibleColumns = null;
            this.cboPlantid.VisibleColumnsWidth = null;
            // 
            // txtOspsenderName
            // 
            this.txtOspsenderName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOspsenderName.Enabled = false;
            this.txtOspsenderName.LabelText = null;
            this.txtOspsenderName.LanguageKey = null;
            this.txtOspsenderName.Location = new System.Drawing.Point(613, 3);
            this.txtOspsenderName.Name = "txtOspsenderName";
            this.txtOspsenderName.Size = new System.Drawing.Size(14, 24);
            this.txtOspsenderName.TabIndex = 2;
            this.txtOspsenderName.Visible = false;
            // 
            // txtLotid
            // 
            this.smartSplitTableLayoutPanel1.SetColumnSpan(this.txtLotid, 3);
            this.txtLotid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLotid.LabelText = null;
            this.txtLotid.LanguageKey = null;
            this.txtLotid.Location = new System.Drawing.Point(123, 33);
            this.txtLotid.Name = "txtLotid";
            this.txtLotid.Size = new System.Drawing.Size(464, 24);
            this.txtLotid.TabIndex = 3;
            // 
            // OutsourcedWarehouseShipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 985);
            this.ConditionsVisible = false;
            this.Name = "OutsourcedWarehouseShipment";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tblWorkGubun.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupOspAreaid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOspsenderName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotid.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tblWorkGubun;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartBandedGrid grdOutsourcedWarehouseShipment;
        private Framework.SmartControls.SmartLabel smartLabel2;
        private Framework.SmartControls.SmartLabel lblOspsender;
        private Framework.SmartControls.SmartLabel lblPlantid;
        private Framework.SmartControls.SmartComboBox cboPlantid;
        private Framework.SmartControls.SmartTextBox txtOspsenderName;
        private Framework.SmartControls.SmartTextBox txtLotid;
        private Framework.SmartControls.SmartButton btnLotnoSearch;
        private Framework.SmartControls.SmartLabel lblOspAreaid;
        private Framework.SmartControls.SmartSelectPopupEdit popupOspAreaid;
    }
}