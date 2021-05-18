namespace Micube.SmartMES.SPC
{
    partial class TestSpcBaseChart03XBar01
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
            this.spcPanel02Sub = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.btnDirectRuleCheck = new Micube.Framework.SmartControls.SmartButton();
            this.btnXBarDirect = new Micube.Framework.SmartControls.SmartButton();
            this.txtView = new System.Windows.Forms.TextBox();
            this.gridNavigator = new System.Windows.Forms.DataGridView();
            this.grdViewPPI = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grdViewResult = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxSigmaUse = new System.Windows.Forms.TextBox();
            this.textBoxSigmaMode = new System.Windows.Forms.TextBox();
            this.labelSigmaUse = new System.Windows.Forms.Label();
            this.labelSigmaMode = new System.Windows.Forms.Label();
            this.splMain = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdTrend = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.ucXBarDirect = new Micube.SmartMES.SPC.UserControl.ucXBar();
            this.ucXBarGrid1 = new Micube.SmartMES.SPC.UserControl.ucXBarGrid();
            this.txtRuleNo = new Micube.Framework.SmartControls.SmartTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.spcPanel02Sub)).BeginInit();
            this.spcPanel02Sub.Panel1.SuspendLayout();
            this.spcPanel02Sub.Panel2.SuspendLayout();
            this.spcPanel02Sub.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridNavigator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewPPI)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewResult)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).BeginInit();
            this.splMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRuleNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // spcPanel02Sub
            // 
            this.spcPanel02Sub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spcPanel02Sub.Location = new System.Drawing.Point(696, 3);
            this.spcPanel02Sub.Name = "spcPanel02Sub";
            // 
            // spcPanel02Sub.Panel1
            // 
            this.spcPanel02Sub.Panel1.Controls.Add(this.splitContainer3);
            // 
            // spcPanel02Sub.Panel2
            // 
            this.spcPanel02Sub.Panel2.Controls.Add(this.panel2);
            this.spcPanel02Sub.Panel2.Controls.Add(this.panel1);
            this.spcPanel02Sub.Size = new System.Drawing.Size(415, 595);
            this.spcPanel02Sub.SplitterDistance = 328;
            this.spcPanel02Sub.SplitterWidth = 6;
            this.spcPanel02Sub.TabIndex = 1;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.txtRuleNo);
            this.splitContainer3.Panel1.Controls.Add(this.btnDirectRuleCheck);
            this.splitContainer3.Panel1.Controls.Add(this.btnXBarDirect);
            this.splitContainer3.Panel1.Controls.Add(this.txtView);
            this.splitContainer3.Panel1.Controls.Add(this.gridNavigator);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.grdViewPPI);
            this.splitContainer3.Size = new System.Drawing.Size(328, 595);
            this.splitContainer3.SplitterDistance = 331;
            this.splitContainer3.TabIndex = 0;
            // 
            // btnDirectRuleCheck
            // 
            this.btnDirectRuleCheck.AllowFocus = false;
            this.btnDirectRuleCheck.IsBusy = false;
            this.btnDirectRuleCheck.IsWrite = false;
            this.btnDirectRuleCheck.Location = new System.Drawing.Point(114, 110);
            this.btnDirectRuleCheck.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnDirectRuleCheck.Name = "btnDirectRuleCheck";
            this.btnDirectRuleCheck.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDirectRuleCheck.Size = new System.Drawing.Size(107, 25);
            this.btnDirectRuleCheck.TabIndex = 6;
            this.btnDirectRuleCheck.Text = "Rule Check";
            this.btnDirectRuleCheck.TooltipLanguageKey = "";
            this.btnDirectRuleCheck.Click += new System.EventHandler(this.btnDirectRuleCheck_Click);
            // 
            // btnXBarDirect
            // 
            this.btnXBarDirect.AllowFocus = false;
            this.btnXBarDirect.IsBusy = false;
            this.btnXBarDirect.IsWrite = false;
            this.btnXBarDirect.Location = new System.Drawing.Point(204, 36);
            this.btnXBarDirect.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnXBarDirect.Name = "btnXBarDirect";
            this.btnXBarDirect.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnXBarDirect.Size = new System.Drawing.Size(107, 25);
            this.btnXBarDirect.TabIndex = 5;
            this.btnXBarDirect.Text = "XBAR 직접조회";
            this.btnXBarDirect.TooltipLanguageKey = "";
            this.btnXBarDirect.Click += new System.EventHandler(this.btnXBarDirect_Click);
            // 
            // txtView
            // 
            this.txtView.Location = new System.Drawing.Point(203, 8);
            this.txtView.Name = "txtView";
            this.txtView.Size = new System.Drawing.Size(100, 21);
            this.txtView.TabIndex = 3;
            this.txtView.TextChanged += new System.EventHandler(this.txtView_TextChanged);
            // 
            // gridNavigator
            // 
            this.gridNavigator.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridNavigator.Location = new System.Drawing.Point(8, 8);
            this.gridNavigator.Name = "gridNavigator";
            this.gridNavigator.RowTemplate.Height = 23;
            this.gridNavigator.Size = new System.Drawing.Size(189, 93);
            this.gridNavigator.TabIndex = 1;
            // 
            // grdViewPPI
            // 
            this.grdViewPPI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdViewPPI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdViewPPI.Location = new System.Drawing.Point(0, 0);
            this.grdViewPPI.Name = "grdViewPPI";
            this.grdViewPPI.RowTemplate.Height = 23;
            this.grdViewPPI.Size = new System.Drawing.Size(328, 260);
            this.grdViewPPI.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.grdViewResult);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 181);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(81, 414);
            this.panel2.TabIndex = 3;
            // 
            // grdViewResult
            // 
            this.grdViewResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdViewResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdViewResult.Location = new System.Drawing.Point(0, 0);
            this.grdViewResult.Name = "grdViewResult";
            this.grdViewResult.RowTemplate.Height = 23;
            this.grdViewResult.Size = new System.Drawing.Size(81, 414);
            this.grdViewResult.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBoxSigmaUse);
            this.panel1.Controls.Add(this.textBoxSigmaMode);
            this.panel1.Controls.Add(this.labelSigmaUse);
            this.panel1.Controls.Add(this.labelSigmaMode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(81, 181);
            this.panel1.TabIndex = 2;
            // 
            // textBoxSigmaUse
            // 
            this.textBoxSigmaUse.Location = new System.Drawing.Point(121, 43);
            this.textBoxSigmaUse.Name = "textBoxSigmaUse";
            this.textBoxSigmaUse.Size = new System.Drawing.Size(100, 21);
            this.textBoxSigmaUse.TabIndex = 3;
            // 
            // textBoxSigmaMode
            // 
            this.textBoxSigmaMode.Location = new System.Drawing.Point(121, 16);
            this.textBoxSigmaMode.Name = "textBoxSigmaMode";
            this.textBoxSigmaMode.Size = new System.Drawing.Size(100, 21);
            this.textBoxSigmaMode.TabIndex = 2;
            // 
            // labelSigmaUse
            // 
            this.labelSigmaUse.AutoSize = true;
            this.labelSigmaUse.Location = new System.Drawing.Point(14, 46);
            this.labelSigmaUse.Name = "labelSigmaUse";
            this.labelSigmaUse.Size = new System.Drawing.Size(101, 12);
            this.labelSigmaUse.TabIndex = 1;
            this.labelSigmaUse.Text = "추정치 사용여부 :";
            // 
            // labelSigmaMode
            // 
            this.labelSigmaMode.AutoSize = true;
            this.labelSigmaMode.Location = new System.Drawing.Point(14, 19);
            this.labelSigmaMode.Name = "labelSigmaMode";
            this.labelSigmaMode.Size = new System.Drawing.Size(65, 12);
            this.labelSigmaMode.TabIndex = 0;
            this.labelSigmaMode.Text = "추정방법 : ";
            // 
            // splMain
            // 
            this.splMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splMain.Horizontal = false;
            this.splMain.Location = new System.Drawing.Point(9, 11);
            this.splMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.splMain.Name = "splMain";
            this.splMain.Panel1.Controls.Add(this.ucXBarDirect);
            this.splMain.Panel1.Controls.Add(this.ucXBarGrid1);
            this.splMain.Panel1.Text = "Panel1";
            this.splMain.Panel2.Controls.Add(this.grdTrend);
            this.splMain.Panel2.Text = "Panel2";
            this.splMain.Size = new System.Drawing.Size(665, 587);
            this.splMain.SplitterPosition = 480;
            this.splMain.TabIndex = 2;
            this.splMain.Text = "smartSpliterContainer1";
            // 
            // grdTrend
            // 
            this.grdTrend.Caption = "";
            this.grdTrend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTrend.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdTrend.IsUsePaging = false;
            this.grdTrend.LanguageKey = null;
            this.grdTrend.Location = new System.Drawing.Point(0, 0);
            this.grdTrend.Margin = new System.Windows.Forms.Padding(0);
            this.grdTrend.Name = "grdTrend";
            this.grdTrend.ShowBorder = true;
            this.grdTrend.ShowStatusBar = false;
            this.grdTrend.Size = new System.Drawing.Size(665, 102);
            this.grdTrend.TabIndex = 7;
            // 
            // ucXBarDirect
            // 
            this.ucXBarDirect.Appearance.BackColor = System.Drawing.Color.White;
            this.ucXBarDirect.Appearance.Options.UseBackColor = true;
            this.ucXBarDirect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucXBarDirect.Location = new System.Drawing.Point(0, 0);
            this.ucXBarDirect.Name = "ucXBarDirect";
            this.ucXBarDirect.Size = new System.Drawing.Size(665, 480);
            this.ucXBarDirect.TabIndex = 4;
            // 
            // ucXBarGrid1
            // 
            this.ucXBarGrid1.Appearance.BackColor = System.Drawing.Color.White;
            this.ucXBarGrid1.Appearance.Options.UseBackColor = true;
            this.ucXBarGrid1.Location = new System.Drawing.Point(0, 0);
            this.ucXBarGrid1.Name = "ucXBarGrid1";
            this.ucXBarGrid1.Size = new System.Drawing.Size(195, 145);
            this.ucXBarGrid1.TabIndex = 0;
            // 
            // txtRuleNo
            // 
            this.txtRuleNo.LabelText = null;
            this.txtRuleNo.LanguageKey = null;
            this.txtRuleNo.Location = new System.Drawing.Point(8, 112);
            this.txtRuleNo.Name = "txtRuleNo";
            this.txtRuleNo.Size = new System.Drawing.Size(100, 20);
            this.txtRuleNo.TabIndex = 7;
            // 
            // TestSpcBaseChart03XBar01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 600);
            this.Controls.Add(this.splMain);
            this.Controls.Add(this.spcPanel02Sub);
            this.Name = "TestSpcBaseChart03XBar01";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPC  User Control - XBAR Chart Test - 01번";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TestSpcBaseChart03_Load);
            this.Resize += new System.EventHandler(this.TestSpcBaseChart03_Resize);
            this.spcPanel02Sub.Panel1.ResumeLayout(false);
            this.spcPanel02Sub.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcPanel02Sub)).EndInit();
            this.spcPanel02Sub.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridNavigator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewPPI)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdViewResult)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).EndInit();
            this.splMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRuleNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.ucXBarGrid ucXBarGrid1;
        private System.Windows.Forms.SplitContainer spcPanel02Sub;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView grdViewPPI;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView grdViewResult;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxSigmaUse;
        private System.Windows.Forms.TextBox textBoxSigmaMode;
        private System.Windows.Forms.Label labelSigmaUse;
        private System.Windows.Forms.Label labelSigmaMode;
        private System.Windows.Forms.DataGridView gridNavigator;
        private System.Windows.Forms.TextBox txtView;
        private Framework.SmartControls.SmartSpliterContainer splMain;
        private Framework.SmartControls.SmartButton btnXBarDirect;
        private UserControl.ucXBar ucXBarDirect;
        private Framework.SmartControls.SmartBandedGrid grdTrend;
        private Framework.SmartControls.SmartButton btnDirectRuleCheck;
        private Framework.SmartControls.SmartTextBox txtRuleNo;
    }
}