namespace Micube.SmartMES.Commons.Controls
{ 
    partial class ApprovalRegistPopup
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
            this.grdUser = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd4 = new Micube.Framework.SmartControls.SmartButton();
            this.btnMinus4 = new Micube.Framework.SmartControls.SmartButton();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd3 = new Micube.Framework.SmartControls.SmartButton();
            this.btnMinus3 = new Micube.Framework.SmartControls.SmartButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd1 = new Micube.Framework.SmartControls.SmartButton();
            this.btnMinus1 = new Micube.Framework.SmartControls.SmartButton();
            this.grdQCApproval4 = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdQCApproval3 = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdQCApproval2 = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdQCApproval1 = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel3 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartLabel2 = new Micube.Framework.SmartControls.SmartLabel();
            this.txtUserIdName = new Micube.Framework.SmartControls.SmartTextBox();
            this.btnGetUserApproval = new Micube.Framework.SmartControls.SmartButton();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.cboPlant = new Micube.Framework.SmartControls.SmartComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd2 = new Micube.Framework.SmartControls.SmartButton();
            this.btnMinus2 = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.smartSplitTableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserIdName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlant.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.pnlMain.Size = new System.Drawing.Size(1143, 671);
            // 
            // grdUser
            // 
            this.grdUser.Caption = "";
            this.grdUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUser.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdUser.IsUsePaging = false;
            this.grdUser.LanguageKey = null;
            this.grdUser.Location = new System.Drawing.Point(0, 40);
            this.grdUser.Margin = new System.Windows.Forms.Padding(0);
            this.grdUser.Name = "grdUser";
            this.smartSplitTableLayoutPanel2.SetRowSpan(this.grdUser, 4);
            this.grdUser.ShowBorder = true;
            this.grdUser.ShowStatusBar = false;
            this.grdUser.Size = new System.Drawing.Size(510, 584);
            this.grdUser.TabIndex = 0;
            this.grdUser.UseAutoBestFitColumns = false;
            // 
            // flowLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel2.SetColumnSpan(this.flowLayoutPanel1, 3);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 624);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1143, 47);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.btnClose);
            this.flowLayoutPanel2.Controls.Add(this.btnSave);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(984, 13);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 13, 0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(159, 23);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(84, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "btnClose";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(3, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "btnSave";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 3;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.68493F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.531507F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.78356F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 1, 4);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 3);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdQCApproval4, 2, 4);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdQCApproval3, 2, 3);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdQCApproval2, 2, 2);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdQCApproval1, 2, 1);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdUser, 0, 1);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 5);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.smartSplitTableLayoutPanel3, 0, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 1, 2);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 6;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(1143, 671);
            this.smartSplitTableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.btnAdd4, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnMinus4, 0, 1);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(515, 483);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(53, 136);
            this.tableLayoutPanel4.TabIndex = 15;
            // 
            // btnAdd4
            // 
            this.btnAdd4.AllowFocus = false;
            this.btnAdd4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd4.Appearance.Options.UseFont = true;
            this.btnAdd4.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.btnAdd4.IsBusy = false;
            this.btnAdd4.IsWrite = false;
            this.btnAdd4.Location = new System.Drawing.Point(4, 30);
            this.btnAdd4.Margin = new System.Windows.Forms.Padding(4, 30, 3, 0);
            this.btnAdd4.Name = "btnAdd4";
            this.btnAdd4.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnAdd4.Size = new System.Drawing.Size(44, 35);
            this.btnAdd4.TabIndex = 2;
            this.btnAdd4.Text = ">";
            this.btnAdd4.TooltipLanguageKey = "";
            // 
            // btnMinus4
            // 
            this.btnMinus4.AllowFocus = false;
            this.btnMinus4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnMinus4.Appearance.Options.UseFont = true;
            this.btnMinus4.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.btnMinus4.IsBusy = false;
            this.btnMinus4.IsWrite = false;
            this.btnMinus4.Location = new System.Drawing.Point(4, 68);
            this.btnMinus4.Margin = new System.Windows.Forms.Padding(4, 0, 3, 0);
            this.btnMinus4.Name = "btnMinus4";
            this.btnMinus4.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnMinus4.Size = new System.Drawing.Size(44, 35);
            this.btnMinus4.TabIndex = 1;
            this.btnMinus4.Text = "<";
            this.btnMinus4.TooltipLanguageKey = "";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.btnAdd3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnMinus3, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(515, 337);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(53, 136);
            this.tableLayoutPanel3.TabIndex = 14;
            // 
            // btnAdd3
            // 
            this.btnAdd3.AllowFocus = false;
            this.btnAdd3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd3.Appearance.Options.UseFont = true;
            this.btnAdd3.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.btnAdd3.IsBusy = false;
            this.btnAdd3.IsWrite = false;
            this.btnAdd3.Location = new System.Drawing.Point(4, 30);
            this.btnAdd3.Margin = new System.Windows.Forms.Padding(4, 30, 3, 0);
            this.btnAdd3.Name = "btnAdd3";
            this.btnAdd3.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnAdd3.Size = new System.Drawing.Size(44, 35);
            this.btnAdd3.TabIndex = 2;
            this.btnAdd3.Text = ">";
            this.btnAdd3.TooltipLanguageKey = "";
            // 
            // btnMinus3
            // 
            this.btnMinus3.AllowFocus = false;
            this.btnMinus3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnMinus3.Appearance.Options.UseFont = true;
            this.btnMinus3.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.btnMinus3.IsBusy = false;
            this.btnMinus3.IsWrite = false;
            this.btnMinus3.Location = new System.Drawing.Point(4, 68);
            this.btnMinus3.Margin = new System.Windows.Forms.Padding(4, 0, 3, 0);
            this.btnMinus3.Name = "btnMinus3";
            this.btnMinus3.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnMinus3.Size = new System.Drawing.Size(44, 35);
            this.btnMinus3.TabIndex = 1;
            this.btnMinus3.Text = "<";
            this.btnMinus3.TooltipLanguageKey = "";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btnAdd1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnMinus1, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(515, 45);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(53, 136);
            this.tableLayoutPanel2.TabIndex = 13;
            // 
            // btnAdd1
            // 
            this.btnAdd1.AllowFocus = false;
            this.btnAdd1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd1.Appearance.Options.UseFont = true;
            this.btnAdd1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.btnAdd1.IsBusy = false;
            this.btnAdd1.IsWrite = false;
            this.btnAdd1.Location = new System.Drawing.Point(4, 30);
            this.btnAdd1.Margin = new System.Windows.Forms.Padding(4, 30, 3, 0);
            this.btnAdd1.Name = "btnAdd1";
            this.btnAdd1.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnAdd1.Size = new System.Drawing.Size(44, 35);
            this.btnAdd1.TabIndex = 2;
            this.btnAdd1.Text = ">";
            this.btnAdd1.TooltipLanguageKey = "";
            // 
            // btnMinus1
            // 
            this.btnMinus1.AllowFocus = false;
            this.btnMinus1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnMinus1.Appearance.Options.UseFont = true;
            this.btnMinus1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.btnMinus1.IsBusy = false;
            this.btnMinus1.IsWrite = false;
            this.btnMinus1.Location = new System.Drawing.Point(4, 68);
            this.btnMinus1.Margin = new System.Windows.Forms.Padding(4, 0, 3, 0);
            this.btnMinus1.Name = "btnMinus1";
            this.btnMinus1.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnMinus1.Size = new System.Drawing.Size(44, 35);
            this.btnMinus1.TabIndex = 1;
            this.btnMinus1.Text = "<";
            this.btnMinus1.TooltipLanguageKey = "";
            // 
            // grdQCApproval4
            // 
            this.grdQCApproval4.Caption = "신규/장기미거래 등록 정보";
            this.grdQCApproval4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdQCApproval4.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdQCApproval4.IsUsePaging = false;
            this.grdQCApproval4.LanguageKey = "RECEPTION";
            this.grdQCApproval4.Location = new System.Drawing.Point(573, 478);
            this.grdQCApproval4.Margin = new System.Windows.Forms.Padding(0);
            this.grdQCApproval4.Name = "grdQCApproval4";
            this.grdQCApproval4.ShowBorder = true;
            this.grdQCApproval4.ShowStatusBar = false;
            this.grdQCApproval4.Size = new System.Drawing.Size(570, 146);
            this.grdQCApproval4.TabIndex = 12;
            this.grdQCApproval4.UseAutoBestFitColumns = false;
            // 
            // grdQCApproval3
            // 
            this.grdQCApproval3.Caption = "신규/장기미거래 등록 정보";
            this.grdQCApproval3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdQCApproval3.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdQCApproval3.IsUsePaging = false;
            this.grdQCApproval3.LanguageKey = "Approbal";
            this.grdQCApproval3.Location = new System.Drawing.Point(573, 332);
            this.grdQCApproval3.Margin = new System.Windows.Forms.Padding(0);
            this.grdQCApproval3.Name = "grdQCApproval3";
            this.grdQCApproval3.ShowBorder = true;
            this.grdQCApproval3.ShowStatusBar = false;
            this.grdQCApproval3.Size = new System.Drawing.Size(570, 146);
            this.grdQCApproval3.TabIndex = 4;
            this.grdQCApproval3.UseAutoBestFitColumns = false;
            // 
            // grdQCApproval2
            // 
            this.grdQCApproval2.Caption = "신규/장기미거래 등록 정보";
            this.grdQCApproval2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdQCApproval2.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdQCApproval2.IsUsePaging = false;
            this.grdQCApproval2.LanguageKey = "AGREEMENT";
            this.grdQCApproval2.Location = new System.Drawing.Point(573, 186);
            this.grdQCApproval2.Margin = new System.Windows.Forms.Padding(0);
            this.grdQCApproval2.Name = "grdQCApproval2";
            this.grdQCApproval2.ShowBorder = true;
            this.grdQCApproval2.ShowStatusBar = false;
            this.grdQCApproval2.Size = new System.Drawing.Size(570, 146);
            this.grdQCApproval2.TabIndex = 3;
            this.grdQCApproval2.UseAutoBestFitColumns = false;
            // 
            // grdQCApproval1
            // 
            this.grdQCApproval1.Caption = "신규/장기미거래 등록 정보";
            this.grdQCApproval1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdQCApproval1.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdQCApproval1.IsUsePaging = false;
            this.grdQCApproval1.LanguageKey = "REQUESTRELIABILITYVERIFICATION";
            this.grdQCApproval1.Location = new System.Drawing.Point(573, 40);
            this.grdQCApproval1.Margin = new System.Windows.Forms.Padding(0);
            this.grdQCApproval1.Name = "grdQCApproval1";
            this.grdQCApproval1.ShowBorder = true;
            this.grdQCApproval1.ShowStatusBar = false;
            this.grdQCApproval1.Size = new System.Drawing.Size(570, 146);
            this.grdQCApproval1.TabIndex = 2;
            this.grdQCApproval1.UseAutoBestFitColumns = false;
            // 
            // smartSplitTableLayoutPanel3
            // 
            this.smartSplitTableLayoutPanel3.ColumnCount = 5;
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 626F));
            this.smartSplitTableLayoutPanel3.Controls.Add(this.smartLabel2, 2, 0);
            this.smartSplitTableLayoutPanel3.Controls.Add(this.txtUserIdName, 3, 0);
            this.smartSplitTableLayoutPanel3.Controls.Add(this.btnGetUserApproval, 4, 0);
            this.smartSplitTableLayoutPanel3.Controls.Add(this.smartLabel1, 0, 0);
            this.smartSplitTableLayoutPanel3.Controls.Add(this.cboPlant, 1, 0);
            this.smartSplitTableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel3.Name = "smartSplitTableLayoutPanel3";
            this.smartSplitTableLayoutPanel3.RowCount = 1;
            this.smartSplitTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel3.Size = new System.Drawing.Size(510, 40);
            this.smartSplitTableLayoutPanel3.TabIndex = 9;
            // 
            // smartLabel2
            // 
            this.smartLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLabel2.LanguageKey = "IDNAME";
            this.smartLabel2.Location = new System.Drawing.Point(173, 3);
            this.smartLabel2.Name = "smartLabel2";
            this.smartLabel2.Size = new System.Drawing.Size(120, 34);
            this.smartLabel2.TabIndex = 6;
            this.smartLabel2.Text = "사용자ID / 사용자명";
            // 
            // txtUserIdName
            // 
            this.txtUserIdName.LabelText = null;
            this.txtUserIdName.LanguageKey = "IDNAME";
            this.txtUserIdName.Location = new System.Drawing.Point(306, 10);
            this.txtUserIdName.Margin = new System.Windows.Forms.Padding(10);
            this.txtUserIdName.Name = "txtUserIdName";
            this.txtUserIdName.Size = new System.Drawing.Size(97, 20);
            this.txtUserIdName.TabIndex = 9;
            // 
            // btnGetUserApproval
            // 
            this.btnGetUserApproval.AllowFocus = false;
            this.btnGetUserApproval.IsBusy = false;
            this.btnGetUserApproval.IsWrite = false;
            this.btnGetUserApproval.LanguageKey = "SEARCHAPPROVALUSER";
            this.btnGetUserApproval.Location = new System.Drawing.Point(423, 7);
            this.btnGetUserApproval.Margin = new System.Windows.Forms.Padding(10, 7, 10, 0);
            this.btnGetUserApproval.Name = "btnGetUserApproval";
            this.btnGetUserApproval.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnGetUserApproval.Size = new System.Drawing.Size(80, 25);
            this.btnGetUserApproval.TabIndex = 10;
            this.btnGetUserApproval.Text = "조회";
            this.btnGetUserApproval.TooltipLanguageKey = "";
            // 
            // smartLabel1
            // 
            this.smartLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLabel1.LanguageKey = "PLANT";
            this.smartLabel1.Location = new System.Drawing.Point(3, 3);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(44, 34);
            this.smartLabel1.TabIndex = 11;
            this.smartLabel1.Text = "Site";
            // 
            // cboPlant
            // 
            this.cboPlant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboPlant.LabelText = null;
            this.cboPlant.LanguageKey = null;
            this.cboPlant.Location = new System.Drawing.Point(60, 10);
            this.cboPlant.Margin = new System.Windows.Forms.Padding(10);
            this.cboPlant.Name = "cboPlant";
            this.cboPlant.PopupWidth = 0;
            this.cboPlant.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPlant.Properties.NullText = "";
            this.cboPlant.ShowHeader = true;
            this.cboPlant.Size = new System.Drawing.Size(100, 20);
            this.cboPlant.TabIndex = 12;
            this.cboPlant.VisibleColumns = null;
            this.cboPlant.VisibleColumnsWidth = null;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btnAdd2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnMinus2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(515, 191);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(53, 136);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // btnAdd2
            // 
            this.btnAdd2.AllowFocus = false;
            this.btnAdd2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd2.Appearance.Options.UseFont = true;
            this.btnAdd2.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.btnAdd2.IsBusy = false;
            this.btnAdd2.IsWrite = false;
            this.btnAdd2.Location = new System.Drawing.Point(4, 30);
            this.btnAdd2.Margin = new System.Windows.Forms.Padding(4, 30, 3, 0);
            this.btnAdd2.Name = "btnAdd2";
            this.btnAdd2.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnAdd2.Size = new System.Drawing.Size(44, 35);
            this.btnAdd2.TabIndex = 2;
            this.btnAdd2.Text = ">";
            this.btnAdd2.TooltipLanguageKey = "";
            // 
            // btnMinus2
            // 
            this.btnMinus2.AllowFocus = false;
            this.btnMinus2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnMinus2.Appearance.Options.UseFont = true;
            this.btnMinus2.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.btnMinus2.IsBusy = false;
            this.btnMinus2.IsWrite = false;
            this.btnMinus2.Location = new System.Drawing.Point(4, 68);
            this.btnMinus2.Margin = new System.Windows.Forms.Padding(4, 0, 3, 0);
            this.btnMinus2.Name = "btnMinus2";
            this.btnMinus2.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnMinus2.Size = new System.Drawing.Size(44, 35);
            this.btnMinus2.TabIndex = 1;
            this.btnMinus2.Text = "<";
            this.btnMinus2.TooltipLanguageKey = "";
            // 
            // ApprovalRegistPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 691);
            this.Name = "ApprovalRegistPopup";
            this.Text = "결재정보";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.smartSplitTableLayoutPanel3.ResumeLayout(false);
            this.smartSplitTableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserIdName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlant.Properties)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartBandedGrid grdUser;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartBandedGrid grdQCApproval3;
        private Framework.SmartControls.SmartBandedGrid grdQCApproval2;
        private Framework.SmartControls.SmartBandedGrid grdQCApproval1;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel3;
        private Framework.SmartControls.SmartLabel smartLabel2;
        private Framework.SmartControls.SmartTextBox txtUserIdName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartButton btnAdd2;
        private Framework.SmartControls.SmartButton btnMinus2;
        private Framework.SmartControls.SmartBandedGrid grdQCApproval4;
        private Framework.SmartControls.SmartButton btnGetUserApproval;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private Framework.SmartControls.SmartButton btnAdd4;
        private Framework.SmartControls.SmartButton btnMinus4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Framework.SmartControls.SmartButton btnAdd3;
        private Framework.SmartControls.SmartButton btnMinus3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Framework.SmartControls.SmartButton btnAdd1;
        private Framework.SmartControls.SmartButton btnMinus1;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartComboBox cboPlant;
    }
}