namespace Micube.SmartMES.ProcessManagement
{
    partial class ucProcessFourStepInfo
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
            this.pnlProcessFourStepInfo = new Micube.Framework.SmartControls.SmartPanel();
            this.tlpProcessFourStepInfo = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.tlpComment = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.txtComment = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblComment = new Micube.Framework.SmartControls.SmartLabel();
            this.tlpDefectQty = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.tlpDefectQtyContent = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.numDefectQty = new Micube.Framework.SmartControls.SmartSpinEdit();
            this.numDefectPnlQty = new Micube.Framework.SmartControls.SmartSpinEdit();
            this.lblDefectQtyUom = new Micube.Framework.SmartControls.SmartLabel();
            this.lblDefectQty = new Micube.Framework.SmartControls.SmartLabel();
            this.cboUnitOfMaterial = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.tlpGoodQty = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.tlpGoodQtyContent = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.numGoodQty = new Micube.Framework.SmartControls.SmartSpinEdit();
            this.numGoodPnlQty = new Micube.Framework.SmartControls.SmartSpinEdit();
            this.lblGoodQtyUOM = new Micube.Framework.SmartControls.SmartLabel();
            this.lblGoodQty = new Micube.Framework.SmartControls.SmartLabel();
            this.cboEquipment = new Micube.Framework.SmartControls.SmartLabelCheckedComboBox();
            this.cboEquipmentClass = new Micube.Framework.SmartControls.SmartLabelComboBox();
            this.txtPrintWeek = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtWorker = new Micube.Framework.SmartControls.SmartLabelSelectPopupEdit();
            this.tlpTransitArea = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.lblTransitArea = new Micube.Framework.SmartControls.SmartLabel();
            this.cboTransitArea = new Micube.Framework.SmartControls.SmartComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlProcessFourStepInfo)).BeginInit();
            this.pnlProcessFourStepInfo.SuspendLayout();
            this.tlpProcessFourStepInfo.SuspendLayout();
            this.tlpComment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).BeginInit();
            this.tlpDefectQty.SuspendLayout();
            this.tlpDefectQtyContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDefectQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDefectPnlQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUnitOfMaterial.Properties)).BeginInit();
            this.tlpGoodQty.SuspendLayout();
            this.tlpGoodQtyContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGoodQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGoodPnlQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEquipment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEquipmentClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrintWeek.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorker.Properties)).BeginInit();
            this.tlpTransitArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboTransitArea.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlProcessFourStepInfo
            // 
            this.pnlProcessFourStepInfo.Controls.Add(this.tlpProcessFourStepInfo);
            this.pnlProcessFourStepInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProcessFourStepInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlProcessFourStepInfo.Margin = new System.Windows.Forms.Padding(0);
            this.pnlProcessFourStepInfo.Name = "pnlProcessFourStepInfo";
            this.pnlProcessFourStepInfo.Size = new System.Drawing.Size(780, 97);
            this.pnlProcessFourStepInfo.TabIndex = 1;
            // 
            // tlpProcessFourStepInfo
            // 
            this.tlpProcessFourStepInfo.ColumnCount = 9;
            this.tlpProcessFourStepInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpProcessFourStepInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpProcessFourStepInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpProcessFourStepInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpProcessFourStepInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpProcessFourStepInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpProcessFourStepInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpProcessFourStepInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpProcessFourStepInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tlpProcessFourStepInfo.Controls.Add(this.tlpComment, 1, 5);
            this.tlpProcessFourStepInfo.Controls.Add(this.tlpDefectQty, 5, 3);
            this.tlpProcessFourStepInfo.Controls.Add(this.cboUnitOfMaterial, 1, 3);
            this.tlpProcessFourStepInfo.Controls.Add(this.tlpGoodQty, 3, 3);
            this.tlpProcessFourStepInfo.Controls.Add(this.cboEquipment, 8, 1);
            this.tlpProcessFourStepInfo.Controls.Add(this.cboEquipmentClass, 8, 3);
            this.tlpProcessFourStepInfo.Controls.Add(this.txtPrintWeek, 7, 3);
            this.tlpProcessFourStepInfo.Controls.Add(this.txtWorker, 1, 1);
            this.tlpProcessFourStepInfo.Controls.Add(this.tlpTransitArea, 3, 1);
            this.tlpProcessFourStepInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpProcessFourStepInfo.Location = new System.Drawing.Point(2, 2);
            this.tlpProcessFourStepInfo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpProcessFourStepInfo.Name = "tlpProcessFourStepInfo";
            this.tlpProcessFourStepInfo.RowCount = 7;
            this.tlpProcessFourStepInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tlpProcessFourStepInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpProcessFourStepInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tlpProcessFourStepInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpProcessFourStepInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tlpProcessFourStepInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpProcessFourStepInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.tlpProcessFourStepInfo.Size = new System.Drawing.Size(776, 93);
            this.tlpProcessFourStepInfo.TabIndex = 0;
            // 
            // tlpComment
            // 
            this.tlpComment.ColumnCount = 3;
            this.tlpProcessFourStepInfo.SetColumnSpan(this.tlpComment, 7);
            this.tlpComment.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tlpComment.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tlpComment.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpComment.Controls.Add(this.txtComment, 2, 0);
            this.tlpComment.Controls.Add(this.lblComment, 0, 0);
            this.tlpComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpComment.Location = new System.Drawing.Point(13, 65);
            this.tlpComment.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tlpComment.Name = "tlpComment";
            this.tlpComment.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.tlpComment.RowCount = 1;
            this.tlpComment.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpComment.Size = new System.Drawing.Size(751, 25);
            this.tlpComment.TabIndex = 8;
            // 
            // txtComment
            // 
            this.txtComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComment.LabelText = null;
            this.txtComment.LanguageKey = null;
            this.txtComment.Location = new System.Drawing.Point(75, 0);
            this.txtComment.Margin = new System.Windows.Forms.Padding(0);
            this.txtComment.Name = "txtComment";
            this.txtComment.Properties.AutoHeight = false;
            this.txtComment.Size = new System.Drawing.Size(676, 23);
            this.txtComment.TabIndex = 0;
            // 
            // lblComment
            // 
            this.lblComment.Appearance.Options.UseTextOptions = true;
            this.lblComment.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblComment.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblComment.LanguageKey = "COMMENT";
            this.lblComment.Location = new System.Drawing.Point(0, 0);
            this.lblComment.Margin = new System.Windows.Forms.Padding(0);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(69, 23);
            this.lblComment.TabIndex = 4;
            this.lblComment.Text = "특이사항";
            // 
            // tlpDefectQty
            // 
            this.tlpDefectQty.ColumnCount = 2;
            this.tlpDefectQty.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpDefectQty.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpDefectQty.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpDefectQty.Controls.Add(this.tlpDefectQtyContent, 3, 0);
            this.tlpDefectQty.Controls.Add(this.lblDefectQty, 0, 0);
            this.tlpDefectQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDefectQty.Location = new System.Drawing.Point(392, 35);
            this.tlpDefectQty.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpDefectQty.Name = "tlpDefectQty";
            this.tlpDefectQty.RowCount = 1;
            this.tlpDefectQty.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDefectQty.Size = new System.Drawing.Size(181, 25);
            this.tlpDefectQty.TabIndex = 6;
            // 
            // tlpDefectQtyContent
            // 
            this.tlpDefectQtyContent.ColumnCount = 4;
            this.tlpDefectQtyContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDefectQtyContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tlpDefectQtyContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDefectQtyContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpDefectQtyContent.Controls.Add(this.numDefectQty, 0, 0);
            this.tlpDefectQtyContent.Controls.Add(this.numDefectPnlQty, 2, 0);
            this.tlpDefectQtyContent.Controls.Add(this.lblDefectQtyUom, 3, 0);
            this.tlpDefectQtyContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDefectQtyContent.Location = new System.Drawing.Point(78, 0);
            this.tlpDefectQtyContent.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.tlpDefectQtyContent.Name = "tlpDefectQtyContent";
            this.tlpDefectQtyContent.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.tlpDefectQtyContent.RowCount = 1;
            this.tlpDefectQtyContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDefectQtyContent.Size = new System.Drawing.Size(103, 25);
            this.tlpDefectQtyContent.TabIndex = 0;
            // 
            // numDefectQty
            // 
            this.numDefectQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numDefectQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numDefectQty.LabelText = null;
            this.numDefectQty.LanguageKey = null;
            this.numDefectQty.Location = new System.Drawing.Point(0, 0);
            this.numDefectQty.Margin = new System.Windows.Forms.Padding(0);
            this.numDefectQty.Name = "numDefectQty";
            this.numDefectQty.Properties.AutoHeight = false;
            this.numDefectQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numDefectQty.Properties.Mask.EditMask = "n0";
            this.numDefectQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.numDefectQty.Size = new System.Drawing.Size(38, 23);
            this.numDefectQty.TabIndex = 0;
            // 
            // numDefectPnlQty
            // 
            this.numDefectPnlQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numDefectPnlQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numDefectPnlQty.LabelText = null;
            this.numDefectPnlQty.LanguageKey = null;
            this.numDefectPnlQty.Location = new System.Drawing.Point(40, 0);
            this.numDefectPnlQty.Margin = new System.Windows.Forms.Padding(0);
            this.numDefectPnlQty.Name = "numDefectPnlQty";
            this.numDefectPnlQty.Properties.AutoHeight = false;
            this.numDefectPnlQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numDefectPnlQty.Properties.Mask.EditMask = "n2";
            this.numDefectPnlQty.Size = new System.Drawing.Size(38, 23);
            this.numDefectPnlQty.TabIndex = 1;
            // 
            // lblDefectQtyUom
            // 
            this.lblDefectQtyUom.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDefectQtyUom.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDefectQtyUom.Location = new System.Drawing.Point(78, 0);
            this.lblDefectQtyUom.Margin = new System.Windows.Forms.Padding(0);
            this.lblDefectQtyUom.Name = "lblDefectQtyUom";
            this.lblDefectQtyUom.Padding = new System.Windows.Forms.Padding(1, 3, 0, 2);
            this.lblDefectQtyUom.Size = new System.Drawing.Size(25, 20);
            this.lblDefectQtyUom.TabIndex = 2;
            this.lblDefectQtyUom.Text = "PNL";
            // 
            // lblDefectQty
            // 
            this.lblDefectQty.Appearance.Options.UseTextOptions = true;
            this.lblDefectQty.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblDefectQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDefectQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDefectQty.LanguageKey = "DEFECTQTY";
            this.lblDefectQty.Location = new System.Drawing.Point(3, 0);
            this.lblDefectQty.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblDefectQty.Name = "lblDefectQty";
            this.lblDefectQty.Size = new System.Drawing.Size(69, 25);
            this.lblDefectQty.TabIndex = 1;
            this.lblDefectQty.Text = "불량수량";
            // 
            // cboUnitOfMaterial
            // 
            this.cboUnitOfMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboUnitOfMaterial.LabelHAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.cboUnitOfMaterial.LabelText = "UOM";
            this.cboUnitOfMaterial.LanguageKey = "UOM";
            this.cboUnitOfMaterial.Location = new System.Drawing.Point(10, 35);
            this.cboUnitOfMaterial.Margin = new System.Windows.Forms.Padding(0);
            this.cboUnitOfMaterial.Name = "cboUnitOfMaterial";
            this.cboUnitOfMaterial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUnitOfMaterial.Properties.NullText = "";
            this.cboUnitOfMaterial.Size = new System.Drawing.Size(181, 20);
            this.cboUnitOfMaterial.TabIndex = 4;
            // 
            // tlpGoodQty
            // 
            this.tlpGoodQty.ColumnCount = 2;
            this.tlpGoodQty.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpGoodQty.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpGoodQty.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpGoodQty.Controls.Add(this.tlpGoodQtyContent, 3, 0);
            this.tlpGoodQty.Controls.Add(this.lblGoodQty, 0, 0);
            this.tlpGoodQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGoodQty.Location = new System.Drawing.Point(201, 35);
            this.tlpGoodQty.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpGoodQty.Name = "tlpGoodQty";
            this.tlpGoodQty.RowCount = 1;
            this.tlpGoodQty.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGoodQty.Size = new System.Drawing.Size(181, 25);
            this.tlpGoodQty.TabIndex = 5;
            // 
            // tlpGoodQtyContent
            // 
            this.tlpGoodQtyContent.ColumnCount = 4;
            this.tlpGoodQtyContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpGoodQtyContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tlpGoodQtyContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpGoodQtyContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpGoodQtyContent.Controls.Add(this.numGoodQty, 0, 0);
            this.tlpGoodQtyContent.Controls.Add(this.numGoodPnlQty, 2, 0);
            this.tlpGoodQtyContent.Controls.Add(this.lblGoodQtyUOM, 3, 0);
            this.tlpGoodQtyContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGoodQtyContent.Location = new System.Drawing.Point(78, 0);
            this.tlpGoodQtyContent.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.tlpGoodQtyContent.Name = "tlpGoodQtyContent";
            this.tlpGoodQtyContent.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.tlpGoodQtyContent.RowCount = 1;
            this.tlpGoodQtyContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGoodQtyContent.Size = new System.Drawing.Size(103, 25);
            this.tlpGoodQtyContent.TabIndex = 0;
            // 
            // numGoodQty
            // 
            this.numGoodQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numGoodQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numGoodQty.LabelText = null;
            this.numGoodQty.LanguageKey = null;
            this.numGoodQty.Location = new System.Drawing.Point(0, 0);
            this.numGoodQty.Margin = new System.Windows.Forms.Padding(0);
            this.numGoodQty.Name = "numGoodQty";
            this.numGoodQty.Properties.AutoHeight = false;
            this.numGoodQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numGoodQty.Properties.Mask.EditMask = "n0";
            this.numGoodQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.numGoodQty.Size = new System.Drawing.Size(38, 23);
            this.numGoodQty.TabIndex = 0;
            // 
            // numGoodPnlQty
            // 
            this.numGoodPnlQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numGoodPnlQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numGoodPnlQty.LabelText = null;
            this.numGoodPnlQty.LanguageKey = null;
            this.numGoodPnlQty.Location = new System.Drawing.Point(40, 0);
            this.numGoodPnlQty.Margin = new System.Windows.Forms.Padding(0);
            this.numGoodPnlQty.Name = "numGoodPnlQty";
            this.numGoodPnlQty.Properties.AutoHeight = false;
            this.numGoodPnlQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numGoodPnlQty.Properties.Mask.EditMask = "n0";
            this.numGoodPnlQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.numGoodPnlQty.Size = new System.Drawing.Size(38, 23);
            this.numGoodPnlQty.TabIndex = 1;
            // 
            // lblGoodQtyUOM
            // 
            this.lblGoodQtyUOM.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblGoodQtyUOM.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGoodQtyUOM.Location = new System.Drawing.Point(78, 0);
            this.lblGoodQtyUOM.Margin = new System.Windows.Forms.Padding(0);
            this.lblGoodQtyUOM.Name = "lblGoodQtyUOM";
            this.lblGoodQtyUOM.Padding = new System.Windows.Forms.Padding(1, 3, 0, 2);
            this.lblGoodQtyUOM.Size = new System.Drawing.Size(25, 20);
            this.lblGoodQtyUOM.TabIndex = 2;
            this.lblGoodQtyUOM.Text = "PNL";
            // 
            // lblGoodQty
            // 
            this.lblGoodQty.Appearance.Options.UseTextOptions = true;
            this.lblGoodQty.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblGoodQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblGoodQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGoodQty.LanguageKey = "GOODQTY";
            this.lblGoodQty.Location = new System.Drawing.Point(3, 0);
            this.lblGoodQty.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblGoodQty.Name = "lblGoodQty";
            this.lblGoodQty.Size = new System.Drawing.Size(69, 25);
            this.lblGoodQty.TabIndex = 1;
            this.lblGoodQty.Text = "양품수량";
            // 
            // cboEquipment
            // 
            this.cboEquipment.LabelHAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.cboEquipment.LabelText = "설비";
            this.cboEquipment.LanguageKey = "EQUIPMENT";
            this.cboEquipment.Location = new System.Drawing.Point(764, 5);
            this.cboEquipment.Margin = new System.Windows.Forms.Padding(0);
            this.cboEquipment.Name = "cboEquipment";
            this.cboEquipment.Properties.AutoHeight = false;
            this.cboEquipment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboEquipment.Properties.DisplayMember = null;
            this.cboEquipment.Properties.PopupWidth = 400;
            this.cboEquipment.Properties.ShowHeader = true;
            this.cboEquipment.Properties.ValueMember = null;
            this.cboEquipment.Properties.VisibleColumns = null;
            this.cboEquipment.Properties.VisibleColumnsWidth = null;
            this.cboEquipment.Size = new System.Drawing.Size(12, 20);
            this.cboEquipment.TabIndex = 2;
            // 
            // cboEquipmentClass
            // 
            this.cboEquipmentClass.LabelHAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.cboEquipmentClass.LabelText = "설비그룹";
            this.cboEquipmentClass.LanguageKey = "EQUIPMENTCLASS";
            this.cboEquipmentClass.Location = new System.Drawing.Point(764, 35);
            this.cboEquipmentClass.Margin = new System.Windows.Forms.Padding(0);
            this.cboEquipmentClass.Name = "cboEquipmentClass";
            this.cboEquipmentClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboEquipmentClass.Properties.NullText = "";
            this.cboEquipmentClass.Size = new System.Drawing.Size(12, 20);
            this.cboEquipmentClass.TabIndex = 1;
            // 
            // txtPrintWeek
            // 
            this.txtPrintWeek.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPrintWeek.LabelHAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtPrintWeek.LabelText = "인쇄주차";
            this.txtPrintWeek.LanguageKey = "PRINTWEEK";
            this.txtPrintWeek.Location = new System.Drawing.Point(583, 35);
            this.txtPrintWeek.Margin = new System.Windows.Forms.Padding(0);
            this.txtPrintWeek.Name = "txtPrintWeek";
            this.txtPrintWeek.Size = new System.Drawing.Size(181, 20);
            this.txtPrintWeek.TabIndex = 3;
            // 
            // txtWorker
            // 
            this.txtWorker.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtWorker.Appearance.Options.UseForeColor = true;
            this.txtWorker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWorker.LabelHAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtWorker.LabelText = "작업자";
            this.txtWorker.LanguageKey = "WORKER";
            this.txtWorker.Location = new System.Drawing.Point(10, 5);
            this.txtWorker.Margin = new System.Windows.Forms.Padding(0);
            this.txtWorker.Name = "txtWorker";
            this.txtWorker.Size = new System.Drawing.Size(181, 20);
            this.txtWorker.TabIndex = 9;
            // 
            // tlpTransitArea
            // 
            this.tlpTransitArea.ColumnCount = 3;
            this.tlpProcessFourStepInfo.SetColumnSpan(this.tlpTransitArea, 3);
            this.tlpTransitArea.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tlpTransitArea.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tlpTransitArea.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTransitArea.Controls.Add(this.lblTransitArea, 0, 0);
            this.tlpTransitArea.Controls.Add(this.cboTransitArea, 2, 0);
            this.tlpTransitArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTransitArea.Location = new System.Drawing.Point(204, 5);
            this.tlpTransitArea.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tlpTransitArea.Name = "tlpTransitArea";
            this.tlpTransitArea.RowCount = 1;
            this.tlpTransitArea.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTransitArea.Size = new System.Drawing.Size(369, 25);
            this.tlpTransitArea.TabIndex = 10;
            // 
            // lblTransitArea
            // 
            this.lblTransitArea.Appearance.Options.UseTextOptions = true;
            this.lblTransitArea.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblTransitArea.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTransitArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTransitArea.LanguageKey = "TRANSITAREA";
            this.lblTransitArea.Location = new System.Drawing.Point(0, 0);
            this.lblTransitArea.Margin = new System.Windows.Forms.Padding(0);
            this.lblTransitArea.Name = "lblTransitArea";
            this.lblTransitArea.Size = new System.Drawing.Size(69, 25);
            this.lblTransitArea.TabIndex = 0;
            this.lblTransitArea.Text = "인계작업장";
            // 
            // cboTransitArea
            // 
            this.cboTransitArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboTransitArea.LabelText = null;
            this.cboTransitArea.LanguageKey = null;
            this.cboTransitArea.Location = new System.Drawing.Point(75, 0);
            this.cboTransitArea.Margin = new System.Windows.Forms.Padding(0);
            this.cboTransitArea.Name = "cboTransitArea";
            this.cboTransitArea.PopupWidth = 0;
            this.cboTransitArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTransitArea.Properties.NullText = "";
            this.cboTransitArea.ShowHeader = true;
            this.cboTransitArea.Size = new System.Drawing.Size(294, 20);
            this.cboTransitArea.TabIndex = 1;
            this.cboTransitArea.VisibleColumns = null;
            this.cboTransitArea.VisibleColumnsWidth = null;
            // 
            // ucProcessFourStepInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.pnlProcessFourStepInfo);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucProcessFourStepInfo";
            this.Size = new System.Drawing.Size(780, 97);
            ((System.ComponentModel.ISupportInitialize)(this.pnlProcessFourStepInfo)).EndInit();
            this.pnlProcessFourStepInfo.ResumeLayout(false);
            this.tlpProcessFourStepInfo.ResumeLayout(false);
            this.tlpComment.ResumeLayout(false);
            this.tlpComment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment.Properties)).EndInit();
            this.tlpDefectQty.ResumeLayout(false);
            this.tlpDefectQty.PerformLayout();
            this.tlpDefectQtyContent.ResumeLayout(false);
            this.tlpDefectQtyContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDefectQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDefectPnlQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUnitOfMaterial.Properties)).EndInit();
            this.tlpGoodQty.ResumeLayout(false);
            this.tlpGoodQty.PerformLayout();
            this.tlpGoodQtyContent.ResumeLayout(false);
            this.tlpGoodQtyContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGoodQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGoodPnlQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEquipment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEquipmentClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrintWeek.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorker.Properties)).EndInit();
            this.tlpTransitArea.ResumeLayout(false);
            this.tlpTransitArea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboTransitArea.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpProcessFourStepInfo;
        private Framework.SmartControls.SmartLabelComboBox cboEquipmentClass;
        private Framework.SmartControls.SmartLabelComboBox cboUnitOfMaterial;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpGoodQty;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpGoodQtyContent;
        private Framework.SmartControls.SmartLabel lblGoodQty;
        private Framework.SmartControls.SmartSpinEdit numGoodQty;
        private Framework.SmartControls.SmartSpinEdit numGoodPnlQty;
        private Framework.SmartControls.SmartLabel lblGoodQtyUOM;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpDefectQty;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpDefectQtyContent;
        private Framework.SmartControls.SmartSpinEdit numDefectQty;
        private Framework.SmartControls.SmartSpinEdit numDefectPnlQty;
        private Framework.SmartControls.SmartLabel lblDefectQtyUom;
        private Framework.SmartControls.SmartLabel lblDefectQty;
        private Framework.SmartControls.SmartLabelTextBox txtPrintWeek;
        private Framework.SmartControls.SmartPanel pnlProcessFourStepInfo;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpComment;
        private Framework.SmartControls.SmartTextBox txtComment;
        private Framework.SmartControls.SmartLabel lblComment;
        private Framework.SmartControls.SmartLabelCheckedComboBox cboEquipment;
        private Framework.SmartControls.SmartLabelSelectPopupEdit txtWorker;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpTransitArea;
        private Framework.SmartControls.SmartLabel lblTransitArea;
        private Framework.SmartControls.SmartComboBox cboTransitArea;
    }
}
