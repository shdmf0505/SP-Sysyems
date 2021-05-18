namespace Micube.SmartMES.Commons.Controls
{ 
    partial class ReliabilityApprovalRegistPopup
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
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd3 = new Micube.Framework.SmartControls.SmartButton();
            this.btnMinus3 = new Micube.Framework.SmartControls.SmartButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd1 = new Micube.Framework.SmartControls.SmartButton();
            this.btnMinus1 = new Micube.Framework.SmartControls.SmartButton();
            this.grdQCApproval3 = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdQCApproval2 = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdQCApproval1 = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel3 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartLabel2 = new Micube.Framework.SmartControls.SmartLabel();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.txtID = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtNAME = new Micube.Framework.SmartControls.SmartTextBox();
            this.btnGetUserApproval = new Micube.Framework.SmartControls.SmartButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd2 = new Micube.Framework.SmartControls.SmartButton();
            this.btnMinus2 = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.smartSplitTableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNAME.Properties)).BeginInit();
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
            this.smartSplitTableLayoutPanel2.SetRowSpan(this.grdUser, 3);
            this.grdUser.ShowBorder = false;
            this.grdUser.ShowStatusBar = false;
            this.grdUser.Size = new System.Drawing.Size(510, 585);
            this.grdUser.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel2.SetColumnSpan(this.flowLayoutPanel1, 3);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 625);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1143, 46);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.btnClose);
            this.flowLayoutPanel2.Controls.Add(this.btnSave);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(979, 13);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 13, 0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(164, 25);
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
            this.btnClose.Location = new System.Drawing.Point(89, 0);
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
            this.btnSave.Size = new System.Drawing.Size(80, 25);
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
            this.smartSplitTableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 3);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdQCApproval3, 2, 3);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdQCApproval2, 2, 2);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdQCApproval1, 2, 1);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdUser, 0, 1);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 4);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.smartSplitTableLayoutPanel3, 0, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 1, 2);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 5;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(1143, 671);
            this.smartSplitTableLayoutPanel2.TabIndex = 1;
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
            this.tableLayoutPanel3.Location = new System.Drawing.Point(515, 435);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(53, 185);
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
            this.btnMinus3.Location = new System.Drawing.Point(4, 92);
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(53, 185);
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
            this.btnMinus1.Location = new System.Drawing.Point(4, 92);
            this.btnMinus1.Margin = new System.Windows.Forms.Padding(4, 0, 3, 0);
            this.btnMinus1.Name = "btnMinus1";
            this.btnMinus1.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnMinus1.Size = new System.Drawing.Size(44, 35);
            this.btnMinus1.TabIndex = 1;
            this.btnMinus1.Text = "<";
            this.btnMinus1.TooltipLanguageKey = "";
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
            this.grdQCApproval3.Location = new System.Drawing.Point(573, 430);
            this.grdQCApproval3.Margin = new System.Windows.Forms.Padding(0);
            this.grdQCApproval3.Name = "grdQCApproval3";
            this.grdQCApproval3.ShowBorder = false;
            this.grdQCApproval3.ShowStatusBar = false;
            this.grdQCApproval3.Size = new System.Drawing.Size(570, 195);
            this.grdQCApproval3.TabIndex = 4;
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
            this.grdQCApproval2.Location = new System.Drawing.Point(573, 235);
            this.grdQCApproval2.Margin = new System.Windows.Forms.Padding(0);
            this.grdQCApproval2.Name = "grdQCApproval2";
            this.grdQCApproval2.ShowBorder = false;
            this.grdQCApproval2.ShowStatusBar = false;
            this.grdQCApproval2.Size = new System.Drawing.Size(570, 195);
            this.grdQCApproval2.TabIndex = 3;
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
            this.grdQCApproval1.ShowBorder = false;
            this.grdQCApproval1.ShowStatusBar = false;
            this.grdQCApproval1.Size = new System.Drawing.Size(570, 195);
            this.grdQCApproval1.TabIndex = 2;
            // 
            // smartSplitTableLayoutPanel3
            // 
            this.smartSplitTableLayoutPanel3.ColumnCount = 5;
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 695F));
            this.smartSplitTableLayoutPanel3.Controls.Add(this.smartLabel2, 2, 0);
            this.smartSplitTableLayoutPanel3.Controls.Add(this.smartLabel1, 0, 0);
            this.smartSplitTableLayoutPanel3.Controls.Add(this.txtID, 1, 0);
            this.smartSplitTableLayoutPanel3.Controls.Add(this.txtNAME, 3, 0);
            this.smartSplitTableLayoutPanel3.Controls.Add(this.btnGetUserApproval, 4, 0);
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
            this.smartLabel2.LanguageKey = "NAME";
            this.smartLabel2.Location = new System.Drawing.Point(153, 3);
            this.smartLabel2.Name = "smartLabel2";
            this.smartLabel2.Size = new System.Drawing.Size(48, 34);
            this.smartLabel2.TabIndex = 6;
            this.smartLabel2.Text = "이름";
            // 
            // smartLabel1
            // 
            this.smartLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLabel1.LanguageKey = "ID";
            this.smartLabel1.Location = new System.Drawing.Point(3, 0);
            this.smartLabel1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 5);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(27, 35);
            this.smartLabel1.TabIndex = 3;
            this.smartLabel1.Text = "ID";
            // 
            // txtID
            // 
            this.txtID.LabelText = null;
            this.txtID.LanguageKey = null;
            this.txtID.Location = new System.Drawing.Point(40, 10);
            this.txtID.Margin = new System.Windows.Forms.Padding(10);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(100, 20);
            this.txtID.TabIndex = 8;
            // 
            // txtNAME
            // 
            this.txtNAME.LabelText = null;
            this.txtNAME.LanguageKey = null;
            this.txtNAME.Location = new System.Drawing.Point(214, 10);
            this.txtNAME.Margin = new System.Windows.Forms.Padding(10);
            this.txtNAME.Name = "txtNAME";
            this.txtNAME.Size = new System.Drawing.Size(100, 20);
            this.txtNAME.TabIndex = 9;
            // 
            // btnGetUserApproval
            // 
            this.btnGetUserApproval.AllowFocus = false;
            this.btnGetUserApproval.IsBusy = false;
            this.btnGetUserApproval.IsWrite = false;
            this.btnGetUserApproval.LanguageKey = "SEARCHAPPROVALUSER";
            this.btnGetUserApproval.Location = new System.Drawing.Point(424, 7);
            this.btnGetUserApproval.Margin = new System.Windows.Forms.Padding(100, 7, 10, 0);
            this.btnGetUserApproval.Name = "btnGetUserApproval";
            this.btnGetUserApproval.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnGetUserApproval.Size = new System.Drawing.Size(80, 25);
            this.btnGetUserApproval.TabIndex = 10;
            this.btnGetUserApproval.Text = "smartButton1";
            this.btnGetUserApproval.TooltipLanguageKey = "";
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(515, 240);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(53, 185);
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
            this.btnMinus2.Location = new System.Drawing.Point(4, 92);
            this.btnMinus2.Margin = new System.Windows.Forms.Padding(4, 0, 3, 0);
            this.btnMinus2.Name = "btnMinus2";
            this.btnMinus2.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnMinus2.Size = new System.Drawing.Size(44, 35);
            this.btnMinus2.TabIndex = 1;
            this.btnMinus2.Text = "<";
            this.btnMinus2.TooltipLanguageKey = "";
            // 
            // ReliabilityApprovalRegistPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 691);
            this.Name = "ReliabilityApprovalRegistPopup";
            this.Text = "결재정보";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.smartSplitTableLayoutPanel3.ResumeLayout(false);
            this.smartSplitTableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNAME.Properties)).EndInit();
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
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartTextBox txtID;
        private Framework.SmartControls.SmartTextBox txtNAME;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartButton btnAdd2;
        private Framework.SmartControls.SmartButton btnMinus2;
        private Framework.SmartControls.SmartButton btnGetUserApproval;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Framework.SmartControls.SmartButton btnAdd3;
        private Framework.SmartControls.SmartButton btnMinus3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Framework.SmartControls.SmartButton btnAdd1;
        private Framework.SmartControls.SmartButton btnMinus1;
    }
}