namespace Micube.SmartMES.QualityAnalysis
{
    partial class ucLotInfo
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
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.tblLotInfo = new System.Windows.Forms.TableLayoutPanel();
            this.txtMM = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtPcsQty = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtArrayQty = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtPnlQty = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtUom = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtCustomer = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtAreaName = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtIsLocking = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtLotProductType = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtProductType = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtDueDate = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtSalesOrderId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtInputDate = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtProductDefName = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtProductDefId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtUserSequence = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtNextProcessSegment = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtProcessSegment = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtPreProcessSegement = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtLotId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            this.tblLotInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPcsQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtArrayQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPnlQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAreaName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIsLocking.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotProductType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDueDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalesOrderId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInputDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductDefName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductDefId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserSequence.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNextProcessSegment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProcessSegment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPreProcessSegement.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.tblLotInfo);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(1046, 104);
            this.smartPanel1.TabIndex = 0;
            // 
            // tblLotInfo
            // 
            this.tblLotInfo.ColumnCount = 5;
            this.tblLotInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblLotInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblLotInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblLotInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblLotInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblLotInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLotInfo.Controls.Add(this.txtMM, 4, 3);
            this.tblLotInfo.Controls.Add(this.txtPcsQty, 3, 3);
            this.tblLotInfo.Controls.Add(this.txtArrayQty, 2, 3);
            this.tblLotInfo.Controls.Add(this.txtPnlQty, 1, 3);
            this.tblLotInfo.Controls.Add(this.txtUom, 0, 3);
            this.tblLotInfo.Controls.Add(this.txtCustomer, 4, 2);
            this.tblLotInfo.Controls.Add(this.txtAreaName, 3, 2);
            this.tblLotInfo.Controls.Add(this.txtIsLocking, 2, 2);
            this.tblLotInfo.Controls.Add(this.txtLotProductType, 1, 2);
            this.tblLotInfo.Controls.Add(this.txtProductType, 0, 2);
            this.tblLotInfo.Controls.Add(this.txtDueDate, 4, 1);
            this.tblLotInfo.Controls.Add(this.txtSalesOrderId, 3, 1);
            this.tblLotInfo.Controls.Add(this.txtInputDate, 2, 1);
            this.tblLotInfo.Controls.Add(this.txtProductDefName, 1, 1);
            this.tblLotInfo.Controls.Add(this.txtProductDefId, 0, 1);
            this.tblLotInfo.Controls.Add(this.txtUserSequence, 4, 0);
            this.tblLotInfo.Controls.Add(this.txtNextProcessSegment, 3, 0);
            this.tblLotInfo.Controls.Add(this.txtProcessSegment, 2, 0);
            this.tblLotInfo.Controls.Add(this.txtPreProcessSegement, 1, 0);
            this.tblLotInfo.Controls.Add(this.txtLotId, 0, 0);
            this.tblLotInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLotInfo.Location = new System.Drawing.Point(2, 2);
            this.tblLotInfo.Name = "tblLotInfo";
            this.tblLotInfo.RowCount = 4;
            this.tblLotInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLotInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLotInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLotInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLotInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLotInfo.Size = new System.Drawing.Size(1042, 100);
            this.tblLotInfo.TabIndex = 0;
            // 
            // txtMM
            // 
            this.txtMM.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.txtMM.Appearance.Options.UseBackColor = true;
            this.txtMM.AutoHeight = false;
            this.txtMM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMM.LanguageKey = "MM";
            this.txtMM.Location = new System.Drawing.Point(832, 75);
            this.txtMM.Margin = new System.Windows.Forms.Padding(0);
            this.txtMM.Name = "txtMM";
            this.txtMM.Properties.AutoHeight = false;
            this.txtMM.Properties.ReadOnly = true;
            this.txtMM.Properties.UseReadOnlyAppearance = false;
            this.txtMM.Size = new System.Drawing.Size(210, 25);
            this.txtMM.TabIndex = 19;
            // 
            // txtPcsQty
            // 
            this.txtPcsQty.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.txtPcsQty.Appearance.Options.UseBackColor = true;
            this.txtPcsQty.AutoHeight = false;
            this.txtPcsQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPcsQty.LanguageKey = "PCSQTY";
            this.txtPcsQty.Location = new System.Drawing.Point(624, 75);
            this.txtPcsQty.Margin = new System.Windows.Forms.Padding(0);
            this.txtPcsQty.Name = "txtPcsQty";
            this.txtPcsQty.Properties.AutoHeight = false;
            this.txtPcsQty.Properties.ReadOnly = true;
            this.txtPcsQty.Properties.UseReadOnlyAppearance = false;
            this.txtPcsQty.Size = new System.Drawing.Size(208, 25);
            this.txtPcsQty.TabIndex = 18;
            // 
            // txtArrayQty
            // 
            this.txtArrayQty.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.txtArrayQty.Appearance.Options.UseBackColor = true;
            this.txtArrayQty.AutoHeight = false;
            this.txtArrayQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtArrayQty.LanguageKey = "ARRAYQTY";
            this.txtArrayQty.Location = new System.Drawing.Point(416, 75);
            this.txtArrayQty.Margin = new System.Windows.Forms.Padding(0);
            this.txtArrayQty.Name = "txtArrayQty";
            this.txtArrayQty.Properties.AutoHeight = false;
            this.txtArrayQty.Properties.ReadOnly = true;
            this.txtArrayQty.Properties.UseReadOnlyAppearance = false;
            this.txtArrayQty.Size = new System.Drawing.Size(208, 25);
            this.txtArrayQty.TabIndex = 17;
            // 
            // txtPnlQty
            // 
            this.txtPnlQty.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.txtPnlQty.Appearance.Options.UseBackColor = true;
            this.txtPnlQty.AutoHeight = false;
            this.txtPnlQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPnlQty.LanguageKey = "PANELQTY";
            this.txtPnlQty.Location = new System.Drawing.Point(208, 75);
            this.txtPnlQty.Margin = new System.Windows.Forms.Padding(0);
            this.txtPnlQty.Name = "txtPnlQty";
            this.txtPnlQty.Properties.AutoHeight = false;
            this.txtPnlQty.Properties.ReadOnly = true;
            this.txtPnlQty.Properties.UseReadOnlyAppearance = false;
            this.txtPnlQty.Size = new System.Drawing.Size(208, 25);
            this.txtPnlQty.TabIndex = 16;
            // 
            // txtUom
            // 
            this.txtUom.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.txtUom.Appearance.Options.UseBackColor = true;
            this.txtUom.AutoHeight = false;
            this.txtUom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUom.LanguageKey = "UOM";
            this.txtUom.Location = new System.Drawing.Point(0, 75);
            this.txtUom.Margin = new System.Windows.Forms.Padding(0);
            this.txtUom.Name = "txtUom";
            this.txtUom.Properties.AutoHeight = false;
            this.txtUom.Properties.ReadOnly = true;
            this.txtUom.Properties.UseReadOnlyAppearance = false;
            this.txtUom.Size = new System.Drawing.Size(208, 25);
            this.txtUom.TabIndex = 15;
            // 
            // txtCustomer
            // 
            this.txtCustomer.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.txtCustomer.Appearance.Options.UseBackColor = true;
            this.txtCustomer.AutoHeight = false;
            this.txtCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCustomer.LanguageKey = "COMPANYCLIENT";
            this.txtCustomer.Location = new System.Drawing.Point(832, 50);
            this.txtCustomer.Margin = new System.Windows.Forms.Padding(0);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Properties.AutoHeight = false;
            this.txtCustomer.Properties.ReadOnly = true;
            this.txtCustomer.Properties.UseReadOnlyAppearance = false;
            this.txtCustomer.Size = new System.Drawing.Size(210, 25);
            this.txtCustomer.TabIndex = 14;
            // 
            // txtAreaName
            // 
            this.txtAreaName.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.txtAreaName.Appearance.Options.UseBackColor = true;
            this.txtAreaName.AutoHeight = false;
            this.txtAreaName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAreaName.LanguageKey = "AREANAME";
            this.txtAreaName.Location = new System.Drawing.Point(624, 50);
            this.txtAreaName.Margin = new System.Windows.Forms.Padding(0);
            this.txtAreaName.Name = "txtAreaName";
            this.txtAreaName.Properties.AutoHeight = false;
            this.txtAreaName.Properties.ReadOnly = true;
            this.txtAreaName.Properties.UseReadOnlyAppearance = false;
            this.txtAreaName.Size = new System.Drawing.Size(208, 25);
            this.txtAreaName.TabIndex = 13;
            // 
            // txtIsLocking
            // 
            this.txtIsLocking.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.txtIsLocking.Appearance.Options.UseBackColor = true;
            this.txtIsLocking.AutoHeight = false;
            this.txtIsLocking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtIsLocking.LanguageKey = "ISLOCKING";
            this.txtIsLocking.Location = new System.Drawing.Point(416, 50);
            this.txtIsLocking.Margin = new System.Windows.Forms.Padding(0);
            this.txtIsLocking.Name = "txtIsLocking";
            this.txtIsLocking.Properties.AutoHeight = false;
            this.txtIsLocking.Properties.ReadOnly = true;
            this.txtIsLocking.Properties.UseReadOnlyAppearance = false;
            this.txtIsLocking.Size = new System.Drawing.Size(208, 25);
            this.txtIsLocking.TabIndex = 12;
            // 
            // txtLotProductType
            // 
            this.txtLotProductType.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.txtLotProductType.Appearance.Options.UseBackColor = true;
            this.txtLotProductType.AutoHeight = false;
            this.txtLotProductType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLotProductType.LanguageKey = "LOTPRODUCTTYPE";
            this.txtLotProductType.Location = new System.Drawing.Point(208, 50);
            this.txtLotProductType.Margin = new System.Windows.Forms.Padding(0);
            this.txtLotProductType.Name = "txtLotProductType";
            this.txtLotProductType.Properties.AutoHeight = false;
            this.txtLotProductType.Properties.ReadOnly = true;
            this.txtLotProductType.Properties.UseReadOnlyAppearance = false;
            this.txtLotProductType.Size = new System.Drawing.Size(208, 25);
            this.txtLotProductType.TabIndex = 11;
            // 
            // txtProductType
            // 
            this.txtProductType.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.txtProductType.Appearance.Options.UseBackColor = true;
            this.txtProductType.AutoHeight = false;
            this.txtProductType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductType.LanguageKey = "PRODUCTDEFTYPE";
            this.txtProductType.Location = new System.Drawing.Point(0, 50);
            this.txtProductType.Margin = new System.Windows.Forms.Padding(0);
            this.txtProductType.Name = "txtProductType";
            this.txtProductType.Properties.AutoHeight = false;
            this.txtProductType.Properties.ReadOnly = true;
            this.txtProductType.Properties.UseReadOnlyAppearance = false;
            this.txtProductType.Size = new System.Drawing.Size(208, 25);
            this.txtProductType.TabIndex = 10;
            // 
            // txtDueDate
            // 
            this.txtDueDate.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.txtDueDate.Appearance.Options.UseBackColor = true;
            this.txtDueDate.AutoHeight = false;
            this.txtDueDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDueDate.LanguageKey = "DUEDATE";
            this.txtDueDate.Location = new System.Drawing.Point(832, 25);
            this.txtDueDate.Margin = new System.Windows.Forms.Padding(0);
            this.txtDueDate.Name = "txtDueDate";
            this.txtDueDate.Properties.AutoHeight = false;
            this.txtDueDate.Properties.ReadOnly = true;
            this.txtDueDate.Properties.UseReadOnlyAppearance = false;
            this.txtDueDate.Size = new System.Drawing.Size(210, 25);
            this.txtDueDate.TabIndex = 9;
            // 
            // txtSalesOrderId
            // 
            this.txtSalesOrderId.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.txtSalesOrderId.Appearance.Options.UseBackColor = true;
            this.txtSalesOrderId.AutoHeight = false;
            this.txtSalesOrderId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSalesOrderId.LanguageKey = "SALESORDERID";
            this.txtSalesOrderId.Location = new System.Drawing.Point(624, 25);
            this.txtSalesOrderId.Margin = new System.Windows.Forms.Padding(0);
            this.txtSalesOrderId.Name = "txtSalesOrderId";
            this.txtSalesOrderId.Properties.AutoHeight = false;
            this.txtSalesOrderId.Properties.ReadOnly = true;
            this.txtSalesOrderId.Properties.UseReadOnlyAppearance = false;
            this.txtSalesOrderId.Size = new System.Drawing.Size(208, 25);
            this.txtSalesOrderId.TabIndex = 8;
            // 
            // txtInputDate
            // 
            this.txtInputDate.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.txtInputDate.Appearance.Options.UseBackColor = true;
            this.txtInputDate.AutoHeight = false;
            this.txtInputDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInputDate.LanguageKey = "INPUTDATE";
            this.txtInputDate.Location = new System.Drawing.Point(416, 25);
            this.txtInputDate.Margin = new System.Windows.Forms.Padding(0);
            this.txtInputDate.Name = "txtInputDate";
            this.txtInputDate.Properties.AutoHeight = false;
            this.txtInputDate.Properties.ReadOnly = true;
            this.txtInputDate.Properties.UseReadOnlyAppearance = false;
            this.txtInputDate.Size = new System.Drawing.Size(208, 25);
            this.txtInputDate.TabIndex = 7;
            // 
            // txtProductDefName
            // 
            this.txtProductDefName.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.txtProductDefName.Appearance.Options.UseBackColor = true;
            this.txtProductDefName.AutoHeight = false;
            this.txtProductDefName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductDefName.LanguageKey = "PRODUCTDEFNAME";
            this.txtProductDefName.Location = new System.Drawing.Point(208, 25);
            this.txtProductDefName.Margin = new System.Windows.Forms.Padding(0);
            this.txtProductDefName.Name = "txtProductDefName";
            this.txtProductDefName.Properties.AutoHeight = false;
            this.txtProductDefName.Properties.ReadOnly = true;
            this.txtProductDefName.Properties.UseReadOnlyAppearance = false;
            this.txtProductDefName.Size = new System.Drawing.Size(208, 25);
            this.txtProductDefName.TabIndex = 6;
            // 
            // txtProductDefId
            // 
            this.txtProductDefId.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.txtProductDefId.Appearance.Options.UseBackColor = true;
            this.txtProductDefId.AutoHeight = false;
            this.txtProductDefId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductDefId.LanguageKey = "PRODUCTDEFID";
            this.txtProductDefId.Location = new System.Drawing.Point(0, 25);
            this.txtProductDefId.Margin = new System.Windows.Forms.Padding(0);
            this.txtProductDefId.Name = "txtProductDefId";
            this.txtProductDefId.Properties.AutoHeight = false;
            this.txtProductDefId.Properties.ReadOnly = true;
            this.txtProductDefId.Properties.UseReadOnlyAppearance = false;
            this.txtProductDefId.Size = new System.Drawing.Size(208, 25);
            this.txtProductDefId.TabIndex = 5;
            // 
            // txtUserSequence
            // 
            this.txtUserSequence.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.txtUserSequence.Appearance.Options.UseBackColor = true;
            this.txtUserSequence.AutoHeight = false;
            this.txtUserSequence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserSequence.LanguageKey = "USERSEQUENCE";
            this.txtUserSequence.Location = new System.Drawing.Point(832, 0);
            this.txtUserSequence.Margin = new System.Windows.Forms.Padding(0);
            this.txtUserSequence.Name = "txtUserSequence";
            this.txtUserSequence.Properties.AutoHeight = false;
            this.txtUserSequence.Properties.ReadOnly = true;
            this.txtUserSequence.Properties.UseReadOnlyAppearance = false;
            this.txtUserSequence.Size = new System.Drawing.Size(210, 25);
            this.txtUserSequence.TabIndex = 4;
            // 
            // txtNextProcessSegment
            // 
            this.txtNextProcessSegment.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.txtNextProcessSegment.Appearance.Options.UseBackColor = true;
            this.txtNextProcessSegment.AutoHeight = false;
            this.txtNextProcessSegment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNextProcessSegment.LanguageKey = "NEXTPROCESSSEGMENTNAME";
            this.txtNextProcessSegment.Location = new System.Drawing.Point(624, 0);
            this.txtNextProcessSegment.Margin = new System.Windows.Forms.Padding(0);
            this.txtNextProcessSegment.Name = "txtNextProcessSegment";
            this.txtNextProcessSegment.Properties.AutoHeight = false;
            this.txtNextProcessSegment.Properties.ReadOnly = true;
            this.txtNextProcessSegment.Properties.UseReadOnlyAppearance = false;
            this.txtNextProcessSegment.Size = new System.Drawing.Size(208, 25);
            this.txtNextProcessSegment.TabIndex = 3;
            // 
            // txtProcessSegment
            // 
            this.txtProcessSegment.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.txtProcessSegment.Appearance.Options.UseBackColor = true;
            this.txtProcessSegment.AutoHeight = false;
            this.txtProcessSegment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProcessSegment.LanguageKey = "PROCESSSEGMENTNAME";
            this.txtProcessSegment.Location = new System.Drawing.Point(416, 0);
            this.txtProcessSegment.Margin = new System.Windows.Forms.Padding(0);
            this.txtProcessSegment.Name = "txtProcessSegment";
            this.txtProcessSegment.Properties.AutoHeight = false;
            this.txtProcessSegment.Properties.ReadOnly = true;
            this.txtProcessSegment.Properties.UseReadOnlyAppearance = false;
            this.txtProcessSegment.Size = new System.Drawing.Size(208, 25);
            this.txtProcessSegment.TabIndex = 2;
            // 
            // txtPreProcessSegement
            // 
            this.txtPreProcessSegement.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.txtPreProcessSegement.Appearance.Options.UseBackColor = true;
            this.txtPreProcessSegement.AutoHeight = false;
            this.txtPreProcessSegement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPreProcessSegement.LanguageKey = "PREVPROCESSSEGMENTNAME";
            this.txtPreProcessSegement.Location = new System.Drawing.Point(208, 0);
            this.txtPreProcessSegement.Margin = new System.Windows.Forms.Padding(0);
            this.txtPreProcessSegement.Name = "txtPreProcessSegement";
            this.txtPreProcessSegement.Properties.AutoHeight = false;
            this.txtPreProcessSegement.Properties.ReadOnly = true;
            this.txtPreProcessSegement.Properties.UseReadOnlyAppearance = false;
            this.txtPreProcessSegement.Size = new System.Drawing.Size(208, 25);
            this.txtPreProcessSegement.TabIndex = 1;
            // 
            // txtLotId
            // 
            this.txtLotId.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(238)))), ((int)(((byte)(243)))));
            this.txtLotId.Appearance.Options.UseBackColor = true;
            this.txtLotId.AutoHeight = false;
            this.txtLotId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLotId.LanguageKey = "LOTID";
            this.txtLotId.Location = new System.Drawing.Point(0, 0);
            this.txtLotId.Margin = new System.Windows.Forms.Padding(0);
            this.txtLotId.Name = "txtLotId";
            this.txtLotId.Properties.AutoHeight = false;
            this.txtLotId.Properties.ReadOnly = true;
            this.txtLotId.Properties.UseReadOnlyAppearance = false;
            this.txtLotId.Size = new System.Drawing.Size(208, 25);
            this.txtLotId.TabIndex = 0;
            // 
            // ucLotInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.smartPanel1);
            this.Name = "ucLotInfo";
            this.Size = new System.Drawing.Size(1046, 104);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.tblLotInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtMM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPcsQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtArrayQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPnlQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAreaName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIsLocking.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotProductType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDueDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalesOrderId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInputDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductDefName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductDefId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserSequence.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNextProcessSegment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProcessSegment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPreProcessSegement.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartPanel smartPanel1;
        private System.Windows.Forms.TableLayoutPanel tblLotInfo;
        private Framework.SmartControls.SmartLabelTextBox txtMM;
        private Framework.SmartControls.SmartLabelTextBox txtPcsQty;
        private Framework.SmartControls.SmartLabelTextBox txtArrayQty;
        private Framework.SmartControls.SmartLabelTextBox txtPnlQty;
        private Framework.SmartControls.SmartLabelTextBox txtUom;
        private Framework.SmartControls.SmartLabelTextBox txtCustomer;
        private Framework.SmartControls.SmartLabelTextBox txtAreaName;
        private Framework.SmartControls.SmartLabelTextBox txtIsLocking;
        private Framework.SmartControls.SmartLabelTextBox txtLotProductType;
        private Framework.SmartControls.SmartLabelTextBox txtProductType;
        private Framework.SmartControls.SmartLabelTextBox txtDueDate;
        private Framework.SmartControls.SmartLabelTextBox txtSalesOrderId;
        private Framework.SmartControls.SmartLabelTextBox txtInputDate;
        private Framework.SmartControls.SmartLabelTextBox txtProductDefName;
        private Framework.SmartControls.SmartLabelTextBox txtProductDefId;
        private Framework.SmartControls.SmartLabelTextBox txtUserSequence;
        private Framework.SmartControls.SmartLabelTextBox txtNextProcessSegment;
        private Framework.SmartControls.SmartLabelTextBox txtProcessSegment;
        private Framework.SmartControls.SmartLabelTextBox txtPreProcessSegement;
        private Framework.SmartControls.SmartLabelTextBox txtLotId;
    }
}
