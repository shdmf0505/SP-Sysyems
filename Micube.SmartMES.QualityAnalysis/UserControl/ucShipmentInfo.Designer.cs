namespace Micube.SmartMES.QualityAnalysis
{
    partial class ucShipmentInfo
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
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup1 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucShipmentInfo));
            Micube.Framework.SmartControls.ConditionCollection conditionCollection1 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection2 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.tblGroup = new System.Windows.Forms.TableLayoutPanel();
            this.cboToArea = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.txtDefectQty = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.cboUOM = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.txtGoodQty = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.lblInspector = new Micube.Framework.SmartControls.SmartLabel();
            this.popInspector = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            this.tblGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboToArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefectQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUOM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGoodQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popInspector.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.tblGroup);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(868, 67);
            this.smartPanel1.TabIndex = 0;
            // 
            // tblGroup
            // 
            this.tblGroup.ColumnCount = 11;
            this.tblGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.tblGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tblGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tblGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tblGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tblGroup.Controls.Add(this.cboToArea, 6, 3);
            this.tblGroup.Controls.Add(this.txtDefectQty, 6, 1);
            this.tblGroup.Controls.Add(this.cboUOM, 1, 1);
            this.tblGroup.Controls.Add(this.txtGoodQty, 4, 1);
            this.tblGroup.Controls.Add(this.lblInspector, 8, 1);
            this.tblGroup.Controls.Add(this.popInspector, 9, 1);
            this.tblGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblGroup.Location = new System.Drawing.Point(2, 2);
            this.tblGroup.Margin = new System.Windows.Forms.Padding(0);
            this.tblGroup.Name = "tblGroup";
            this.tblGroup.RowCount = 5;
            this.tblGroup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.tblGroup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblGroup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tblGroup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblGroup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.tblGroup.Size = new System.Drawing.Size(864, 63);
            this.tblGroup.TabIndex = 0;
            // 
            // cboToArea
            // 
            this.cboToArea.AutoHeight = false;
            this.cboToArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboToArea.LanguageKey = "TRANSITAREA";
            this.cboToArea.Location = new System.Drawing.Point(433, 34);
            this.cboToArea.Margin = new System.Windows.Forms.Padding(0);
            this.cboToArea.Name = "cboToArea";
            this.cboToArea.Properties.AutoHeight = false;
            this.cboToArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboToArea.Properties.NullText = "";
            this.cboToArea.Size = new System.Drawing.Size(210, 26);
            this.cboToArea.TabIndex = 5;
            // 
            // txtDefectQty
            // 
            this.txtDefectQty.AutoHeight = false;
            this.txtDefectQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDefectQty.LanguageKey = "DEFECTQTY";
            this.txtDefectQty.Location = new System.Drawing.Point(433, 3);
            this.txtDefectQty.Margin = new System.Windows.Forms.Padding(0);
            this.txtDefectQty.Name = "txtDefectQty";
            this.txtDefectQty.Properties.AutoHeight = false;
            this.txtDefectQty.Properties.ReadOnly = true;
            this.txtDefectQty.Size = new System.Drawing.Size(210, 26);
            this.txtDefectQty.TabIndex = 2;
            // 
            // cboUOM
            // 
            this.cboUOM.AutoHeight = false;
            this.tblGroup.SetColumnSpan(this.cboUOM, 2);
            this.cboUOM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboUOM.LanguageKey = "UOM";
            this.cboUOM.Location = new System.Drawing.Point(3, 3);
            this.cboUOM.Margin = new System.Windows.Forms.Padding(0);
            this.cboUOM.Name = "cboUOM";
            this.cboUOM.Properties.AutoHeight = false;
            this.cboUOM.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUOM.Properties.NullText = "";
            this.cboUOM.Size = new System.Drawing.Size(210, 26);
            this.cboUOM.TabIndex = 0;
            // 
            // txtGoodQty
            // 
            this.txtGoodQty.AutoHeight = false;
            this.txtGoodQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGoodQty.LanguageKey = "GOODQTY";
            this.txtGoodQty.Location = new System.Drawing.Point(218, 3);
            this.txtGoodQty.Margin = new System.Windows.Forms.Padding(0);
            this.txtGoodQty.Name = "txtGoodQty";
            this.txtGoodQty.Properties.AutoHeight = false;
            this.txtGoodQty.Properties.ReadOnly = true;
            this.txtGoodQty.Size = new System.Drawing.Size(210, 26);
            this.txtGoodQty.TabIndex = 1;
            // 
            // lblInspector
            // 
            this.lblInspector.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblInspector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInspector.LanguageKey = "INSPECTIONUSER";
            this.lblInspector.Location = new System.Drawing.Point(648, 3);
            this.lblInspector.Margin = new System.Windows.Forms.Padding(0);
            this.lblInspector.Name = "lblInspector";
            this.lblInspector.Size = new System.Drawing.Size(105, 26);
            this.lblInspector.TabIndex = 3;
            this.lblInspector.Text = "smartLabel1";
            // 
            // popInspector
            // 
            this.popInspector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.popInspector.LabelText = null;
            this.popInspector.LanguageKey = null;
            this.popInspector.Location = new System.Drawing.Point(753, 3);
            this.popInspector.Margin = new System.Windows.Forms.Padding(0);
            this.popInspector.Name = "popInspector";
            this.popInspector.Properties.AutoHeight = false;
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
            this.popInspector.SelectPopupCondition = conditionItemSelectPopup1;
            this.popInspector.Size = new System.Drawing.Size(105, 26);
            this.popInspector.TabIndex = 4;
            // 
            // ucShipmentInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.smartPanel1);
            this.Name = "ucShipmentInfo";
            this.Size = new System.Drawing.Size(868, 67);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.tblGroup.ResumeLayout(false);
            this.tblGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboToArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefectQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUOM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGoodQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popInspector.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartPanel smartPanel1;
        private System.Windows.Forms.TableLayoutPanel tblGroup;
        private Framework.SmartControls.SmartLabelComboBox cboUOM;
        private Framework.SmartControls.SmartLabelTextBox txtGoodQty;
        private Framework.SmartControls.SmartLabelTextBox txtDefectQty;
        private Framework.SmartControls.SmartLabel lblInspector;
        private Framework.SmartControls.SmartSelectPopupEdit popInspector;
        private Framework.SmartControls.SmartLabelComboBox cboToArea;
    }
}
