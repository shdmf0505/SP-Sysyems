namespace Micube.SmartMES.SPC.UserControl
{
    partial class ucXBarFrame
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
            this.splMain = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.palLeftMain = new Micube.Framework.SmartControls.SmartPanel();
            this.btnTestDirectSpec = new Micube.Framework.SmartControls.SmartButton();
            this.btnReExecutionTest = new Micube.Framework.SmartControls.SmartButton();
            this.smartLabel4 = new Micube.Framework.SmartControls.SmartLabel();
            this.smartLabel3 = new Micube.Framework.SmartControls.SmartLabel();
            this.txtWholeMinP2 = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtWholeMaxP2 = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtWholeMinP1 = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartLabel2 = new Micube.Framework.SmartControls.SmartLabel();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.txtWholeMaxP1 = new Micube.Framework.SmartControls.SmartTextBox();
            this.chkLeftEstimate = new Micube.Framework.SmartControls.SmartCheckBox();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.lblAllLsl = new Micube.Framework.SmartControls.SmartLabel();
            this.txtAllLslValue = new Micube.Framework.SmartControls.SmartTextBox();
            this.btnReExecution = new Micube.Framework.SmartControls.SmartButton();
            this.lblAllRLcl = new Micube.Framework.SmartControls.SmartLabel();
            this.txtAllRLclValue = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblAllCsl = new Micube.Framework.SmartControls.SmartLabel();
            this.txtAllCslValue = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblAllRUcl = new Micube.Framework.SmartControls.SmartLabel();
            this.txtAllRUclValue = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblAllLcl = new Micube.Framework.SmartControls.SmartLabel();
            this.txtAllLclValue = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblAllUsl = new Micube.Framework.SmartControls.SmartLabel();
            this.txtAllUslValue = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblAllUcl = new Micube.Framework.SmartControls.SmartLabel();
            this.txtAllUclValue = new Micube.Framework.SmartControls.SmartTextBox();
            this.rdoLeftGroup = new Micube.Framework.SmartControls.SmartRadioGroup();
            this.cboLeftChartType = new Micube.Framework.SmartControls.SmartComboBox();
            this.splMainSub = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.ucXBarGrid01 = new Micube.SmartMES.SPC.UserControl.ucXBarGrid();
            this.grdSelectChartRawData = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).BeginInit();
            this.splMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.palLeftMain)).BeginInit();
            this.palLeftMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtWholeMinP2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWholeMaxP2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWholeMinP1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWholeMaxP1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLeftEstimate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllLslValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllRLclValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllCslValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllRUclValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllLclValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllUslValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllUclValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoLeftGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLeftChartType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splMainSub)).BeginInit();
            this.splMainSub.SuspendLayout();
            this.SuspendLayout();
            // 
            // splMain
            // 
            this.splMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splMain.Location = new System.Drawing.Point(0, 0);
            this.splMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.splMain.Name = "splMain";
            this.splMain.Panel1.Controls.Add(this.palLeftMain);
            this.splMain.Panel1.Text = "Panel1";
            this.splMain.Panel2.Controls.Add(this.splMainSub);
            this.splMain.Panel2.Text = "Panel2";
            this.splMain.Size = new System.Drawing.Size(1005, 606);
            this.splMain.SplitterPosition = 150;
            this.splMain.TabIndex = 1;
            this.splMain.Text = "splMain";
            // 
            // palLeftMain
            // 
            this.palLeftMain.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.palLeftMain.Appearance.Options.UseBackColor = true;
            this.palLeftMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.palLeftMain.Controls.Add(this.btnTestDirectSpec);
            this.palLeftMain.Controls.Add(this.btnReExecutionTest);
            this.palLeftMain.Controls.Add(this.smartLabel4);
            this.palLeftMain.Controls.Add(this.smartLabel3);
            this.palLeftMain.Controls.Add(this.txtWholeMinP2);
            this.palLeftMain.Controls.Add(this.txtWholeMaxP2);
            this.palLeftMain.Controls.Add(this.txtWholeMinP1);
            this.palLeftMain.Controls.Add(this.smartLabel2);
            this.palLeftMain.Controls.Add(this.smartLabel1);
            this.palLeftMain.Controls.Add(this.txtWholeMaxP1);
            this.palLeftMain.Controls.Add(this.chkLeftEstimate);
            this.palLeftMain.Controls.Add(this.smartGroupBox1);
            this.palLeftMain.Controls.Add(this.cboLeftChartType);
            this.palLeftMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palLeftMain.Location = new System.Drawing.Point(0, 0);
            this.palLeftMain.Name = "palLeftMain";
            this.palLeftMain.Size = new System.Drawing.Size(150, 606);
            this.palLeftMain.TabIndex = 0;
            // 
            // btnTestDirectSpec
            // 
            this.btnTestDirectSpec.AllowFocus = false;
            this.btnTestDirectSpec.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestDirectSpec.Appearance.Options.UseFont = true;
            this.btnTestDirectSpec.IsBusy = false;
            this.btnTestDirectSpec.Location = new System.Drawing.Point(14, 561);
            this.btnTestDirectSpec.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnTestDirectSpec.Name = "btnTestDirectSpec";
            this.btnTestDirectSpec.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnTestDirectSpec.Size = new System.Drawing.Size(28, 25);
            this.btnTestDirectSpec.TabIndex = 19;
            this.btnTestDirectSpec.Text = "D";
            this.btnTestDirectSpec.TooltipLanguageKey = "";
            this.btnTestDirectSpec.Visible = false;
            this.btnTestDirectSpec.Click += new System.EventHandler(this.btnTestDirectSpec_Click);
            // 
            // btnReExecutionTest
            // 
            this.btnReExecutionTest.AllowFocus = false;
            this.btnReExecutionTest.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReExecutionTest.Appearance.Options.UseFont = true;
            this.btnReExecutionTest.IsBusy = false;
            this.btnReExecutionTest.Location = new System.Drawing.Point(14, 536);
            this.btnReExecutionTest.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnReExecutionTest.Name = "btnReExecutionTest";
            this.btnReExecutionTest.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnReExecutionTest.Size = new System.Drawing.Size(123, 25);
            this.btnReExecutionTest.TabIndex = 18;
            this.btnReExecutionTest.Text = "Test 실행";
            this.btnReExecutionTest.TooltipLanguageKey = "";
            this.btnReExecutionTest.Visible = false;
            this.btnReExecutionTest.Click += new System.EventHandler(this.btnReExecutionTest_Click);
            // 
            // smartLabel4
            // 
            this.smartLabel4.Location = new System.Drawing.Point(104, 50);
            this.smartLabel4.Name = "smartLabel4";
            this.smartLabel4.Size = new System.Drawing.Size(18, 14);
            this.smartLabel4.TabIndex = 15;
            this.smartLabel4.Text = "Min";
            // 
            // smartLabel3
            // 
            this.smartLabel3.Location = new System.Drawing.Point(51, 50);
            this.smartLabel3.Name = "smartLabel3";
            this.smartLabel3.Size = new System.Drawing.Size(21, 14);
            this.smartLabel3.TabIndex = 14;
            this.smartLabel3.Text = "Max";
            // 
            // txtWholeMinP2
            // 
            this.txtWholeMinP2.EditValue = 123.013D;
            this.txtWholeMinP2.LabelText = null;
            this.txtWholeMinP2.LanguageKey = null;
            this.txtWholeMinP2.Location = new System.Drawing.Point(88, 89);
            this.txtWholeMinP2.Name = "txtWholeMinP2";
            this.txtWholeMinP2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtWholeMinP2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtWholeMinP2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtWholeMinP2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtWholeMinP2.Size = new System.Drawing.Size(55, 20);
            this.txtWholeMinP2.TabIndex = 4;
            this.txtWholeMinP2.Leave += new System.EventHandler(this.txtWholeMinP2_Leave);
            // 
            // txtWholeMaxP2
            // 
            this.txtWholeMaxP2.EditValue = 123.013D;
            this.txtWholeMaxP2.LabelText = null;
            this.txtWholeMaxP2.LanguageKey = null;
            this.txtWholeMaxP2.Location = new System.Drawing.Point(33, 89);
            this.txtWholeMaxP2.Name = "txtWholeMaxP2";
            this.txtWholeMaxP2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtWholeMaxP2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtWholeMaxP2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtWholeMaxP2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtWholeMaxP2.Size = new System.Drawing.Size(54, 20);
            this.txtWholeMaxP2.TabIndex = 3;
            this.txtWholeMaxP2.Leave += new System.EventHandler(this.txtWholeMaxP2_Leave);
            // 
            // txtWholeMinP1
            // 
            this.txtWholeMinP1.EditValue = 123.013D;
            this.txtWholeMinP1.LabelText = null;
            this.txtWholeMinP1.LanguageKey = null;
            this.txtWholeMinP1.Location = new System.Drawing.Point(88, 66);
            this.txtWholeMinP1.Name = "txtWholeMinP1";
            this.txtWholeMinP1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtWholeMinP1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtWholeMinP1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtWholeMinP1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtWholeMinP1.Size = new System.Drawing.Size(55, 20);
            this.txtWholeMinP1.TabIndex = 2;
            this.txtWholeMinP1.Leave += new System.EventHandler(this.txtWholeMinP1_Leave);
            // 
            // smartLabel2
            // 
            this.smartLabel2.Location = new System.Drawing.Point(10, 92);
            this.smartLabel2.Name = "smartLabel2";
            this.smartLabel2.Size = new System.Drawing.Size(19, 14);
            this.smartLabel2.TabIndex = 10;
            this.smartLabel2.Text = "W2";
            // 
            // smartLabel1
            // 
            this.smartLabel1.Location = new System.Drawing.Point(10, 69);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(19, 14);
            this.smartLabel1.TabIndex = 8;
            this.smartLabel1.Text = "W1";
            // 
            // txtWholeMaxP1
            // 
            this.txtWholeMaxP1.EditValue = "123.066";
            this.txtWholeMaxP1.LabelText = null;
            this.txtWholeMaxP1.LanguageKey = null;
            this.txtWholeMaxP1.Location = new System.Drawing.Point(33, 66);
            this.txtWholeMaxP1.Name = "txtWholeMaxP1";
            this.txtWholeMaxP1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtWholeMaxP1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtWholeMaxP1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtWholeMaxP1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtWholeMaxP1.Size = new System.Drawing.Size(54, 20);
            this.txtWholeMaxP1.TabIndex = 1;
            this.txtWholeMaxP1.EditValueChanged += new System.EventHandler(this.txtWholeMaxP1_EditValueChanged);
            this.txtWholeMaxP1.Leave += new System.EventHandler(this.txtWholeMaxP1_Leave);
            // 
            // chkLeftEstimate
            // 
            this.chkLeftEstimate.EditValue = true;
            this.chkLeftEstimate.Location = new System.Drawing.Point(8, 484);
            this.chkLeftEstimate.Name = "chkLeftEstimate";
            this.chkLeftEstimate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLeftEstimate.Properties.Appearance.Options.UseFont = true;
            this.chkLeftEstimate.Properties.Caption = "추정치";
            this.chkLeftEstimate.Size = new System.Drawing.Size(117, 23);
            this.chkLeftEstimate.TabIndex = 6;
            // 
            // smartGroupBox1
            // 
            this.smartGroupBox1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.smartGroupBox1.Appearance.Options.UseBackColor = true;
            this.smartGroupBox1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartGroupBox1.CaptionLocation = DevExpress.Utils.Locations.Top;
            this.smartGroupBox1.Controls.Add(this.lblAllLsl);
            this.smartGroupBox1.Controls.Add(this.txtAllLslValue);
            this.smartGroupBox1.Controls.Add(this.btnReExecution);
            this.smartGroupBox1.Controls.Add(this.lblAllRLcl);
            this.smartGroupBox1.Controls.Add(this.txtAllRLclValue);
            this.smartGroupBox1.Controls.Add(this.lblAllCsl);
            this.smartGroupBox1.Controls.Add(this.txtAllCslValue);
            this.smartGroupBox1.Controls.Add(this.lblAllRUcl);
            this.smartGroupBox1.Controls.Add(this.txtAllRUclValue);
            this.smartGroupBox1.Controls.Add(this.lblAllLcl);
            this.smartGroupBox1.Controls.Add(this.txtAllLclValue);
            this.smartGroupBox1.Controls.Add(this.lblAllUsl);
            this.smartGroupBox1.Controls.Add(this.txtAllUslValue);
            this.smartGroupBox1.Controls.Add(this.lblAllUcl);
            this.smartGroupBox1.Controls.Add(this.txtAllUclValue);
            this.smartGroupBox1.Controls.Add(this.rdoLeftGroup);
            this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox1.Location = new System.Drawing.Point(8, 122);
            this.smartGroupBox1.Name = "smartGroupBox1";
            this.smartGroupBox1.ShowBorder = true;
            this.smartGroupBox1.Size = new System.Drawing.Size(135, 345);
            this.smartGroupBox1.TabIndex = 5;
            this.smartGroupBox1.Text = "Control Limit Option";
            // 
            // lblAllLsl
            // 
            this.lblAllLsl.Location = new System.Drawing.Point(6, 174);
            this.lblAllLsl.Name = "lblAllLsl";
            this.lblAllLsl.Size = new System.Drawing.Size(19, 14);
            this.lblAllLsl.TabIndex = 10;
            this.lblAllLsl.Text = "LSL";
            // 
            // txtAllLslValue
            // 
            this.txtAllLslValue.EditValue = "999,999.0123";
            this.txtAllLslValue.LabelText = null;
            this.txtAllLslValue.LanguageKey = null;
            this.txtAllLslValue.Location = new System.Drawing.Point(39, 171);
            this.txtAllLslValue.Name = "txtAllLslValue";
            this.txtAllLslValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtAllLslValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllLslValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllLslValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtAllLslValue.Size = new System.Drawing.Size(90, 20);
            this.txtAllLslValue.TabIndex = 2;
            // 
            // btnReExecution
            // 
            this.btnReExecution.AllowFocus = false;
            this.btnReExecution.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReExecution.Appearance.Options.UseFont = true;
            this.btnReExecution.IsBusy = false;
            this.btnReExecution.Location = new System.Drawing.Point(6, 311);
            this.btnReExecution.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnReExecution.Name = "btnReExecution";
            this.btnReExecution.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnReExecution.Size = new System.Drawing.Size(123, 25);
            this.btnReExecution.TabIndex = 17;
            this.btnReExecution.Text = "재분석";
            this.btnReExecution.TooltipLanguageKey = "";
            this.btnReExecution.Click += new System.EventHandler(this.btnReExecution_Click);
            // 
            // lblAllRLcl
            // 
            this.lblAllRLcl.Location = new System.Drawing.Point(6, 276);
            this.lblAllRLcl.Name = "lblAllRLcl";
            this.lblAllRLcl.Size = new System.Drawing.Size(26, 14);
            this.lblAllRLcl.TabIndex = 14;
            this.lblAllRLcl.Text = "RLCL";
            // 
            // txtAllRLclValue
            // 
            this.txtAllRLclValue.EditValue = "999,999.0123";
            this.txtAllRLclValue.LabelText = null;
            this.txtAllRLclValue.LanguageKey = null;
            this.txtAllRLclValue.Location = new System.Drawing.Point(39, 273);
            this.txtAllRLclValue.Name = "txtAllRLclValue";
            this.txtAllRLclValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtAllRLclValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllRLclValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllRLclValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtAllRLclValue.Size = new System.Drawing.Size(90, 20);
            this.txtAllRLclValue.TabIndex = 6;
            // 
            // lblAllCsl
            // 
            this.lblAllCsl.Location = new System.Drawing.Point(6, 151);
            this.lblAllCsl.Name = "lblAllCsl";
            this.lblAllCsl.Size = new System.Drawing.Size(20, 14);
            this.lblAllCsl.TabIndex = 9;
            this.lblAllCsl.Text = "CSL";
            // 
            // txtAllCslValue
            // 
            this.txtAllCslValue.EditValue = "999,999.0123";
            this.txtAllCslValue.LabelText = null;
            this.txtAllCslValue.LanguageKey = null;
            this.txtAllCslValue.Location = new System.Drawing.Point(39, 148);
            this.txtAllCslValue.Name = "txtAllCslValue";
            this.txtAllCslValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtAllCslValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllCslValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllCslValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtAllCslValue.Size = new System.Drawing.Size(90, 20);
            this.txtAllCslValue.TabIndex = 1;
            // 
            // lblAllRUcl
            // 
            this.lblAllRUcl.Location = new System.Drawing.Point(6, 253);
            this.lblAllRUcl.Name = "lblAllRUcl";
            this.lblAllRUcl.Size = new System.Drawing.Size(28, 14);
            this.lblAllRUcl.TabIndex = 13;
            this.lblAllRUcl.Text = "RUCL";
            // 
            // txtAllRUclValue
            // 
            this.txtAllRUclValue.EditValue = "999,999.0123";
            this.txtAllRUclValue.LabelText = null;
            this.txtAllRUclValue.LanguageKey = null;
            this.txtAllRUclValue.Location = new System.Drawing.Point(39, 250);
            this.txtAllRUclValue.Name = "txtAllRUclValue";
            this.txtAllRUclValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtAllRUclValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllRUclValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllRUclValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtAllRUclValue.Size = new System.Drawing.Size(90, 20);
            this.txtAllRUclValue.TabIndex = 5;
            // 
            // lblAllLcl
            // 
            this.lblAllLcl.Location = new System.Drawing.Point(6, 225);
            this.lblAllLcl.Name = "lblAllLcl";
            this.lblAllLcl.Size = new System.Drawing.Size(19, 14);
            this.lblAllLcl.TabIndex = 12;
            this.lblAllLcl.Text = "LCL";
            // 
            // txtAllLclValue
            // 
            this.txtAllLclValue.EditValue = "999,999.0123";
            this.txtAllLclValue.LabelText = null;
            this.txtAllLclValue.LanguageKey = null;
            this.txtAllLclValue.Location = new System.Drawing.Point(39, 222);
            this.txtAllLclValue.Name = "txtAllLclValue";
            this.txtAllLclValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtAllLclValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllLclValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllLclValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtAllLclValue.Size = new System.Drawing.Size(90, 20);
            this.txtAllLclValue.TabIndex = 4;
            // 
            // lblAllUsl
            // 
            this.lblAllUsl.Location = new System.Drawing.Point(6, 128);
            this.lblAllUsl.Name = "lblAllUsl";
            this.lblAllUsl.Size = new System.Drawing.Size(21, 14);
            this.lblAllUsl.TabIndex = 8;
            this.lblAllUsl.Text = "USL";
            // 
            // txtAllUslValue
            // 
            this.txtAllUslValue.EditValue = "999,999.0123";
            this.txtAllUslValue.LabelText = null;
            this.txtAllUslValue.LanguageKey = null;
            this.txtAllUslValue.Location = new System.Drawing.Point(39, 125);
            this.txtAllUslValue.Name = "txtAllUslValue";
            this.txtAllUslValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtAllUslValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllUslValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllUslValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtAllUslValue.Size = new System.Drawing.Size(90, 20);
            this.txtAllUslValue.TabIndex = 0;
            // 
            // lblAllUcl
            // 
            this.lblAllUcl.Location = new System.Drawing.Point(6, 202);
            this.lblAllUcl.Name = "lblAllUcl";
            this.lblAllUcl.Size = new System.Drawing.Size(21, 14);
            this.lblAllUcl.TabIndex = 11;
            this.lblAllUcl.Text = "UCL";
            // 
            // txtAllUclValue
            // 
            this.txtAllUclValue.EditValue = "999,999.0123";
            this.txtAllUclValue.LabelText = null;
            this.txtAllUclValue.LanguageKey = null;
            this.txtAllUclValue.Location = new System.Drawing.Point(39, 199);
            this.txtAllUclValue.Name = "txtAllUclValue";
            this.txtAllUclValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtAllUclValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllUclValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAllUclValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtAllUclValue.Size = new System.Drawing.Size(90, 20);
            this.txtAllUclValue.TabIndex = 3;
            // 
            // rdoLeftGroup
            // 
            this.rdoLeftGroup.EditValue = 2;
            this.rdoLeftGroup.Location = new System.Drawing.Point(6, 19);
            this.rdoLeftGroup.Name = "rdoLeftGroup";
            this.rdoLeftGroup.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoLeftGroup.Properties.Appearance.Options.UseFont = true;
            this.rdoLeftGroup.Properties.Columns = 1;
            this.rdoLeftGroup.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.rdoLeftGroup.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "해석용"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "관리용"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "직접입력")});
            this.rdoLeftGroup.Size = new System.Drawing.Size(123, 91);
            this.rdoLeftGroup.TabIndex = 7;
            // 
            // cboLeftChartType
            // 
            this.cboLeftChartType.LabelText = null;
            this.cboLeftChartType.LanguageKey = null;
            this.cboLeftChartType.Location = new System.Drawing.Point(14, 24);
            this.cboLeftChartType.Name = "cboLeftChartType";
            this.cboLeftChartType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboLeftChartType.Properties.NullText = "";
            this.cboLeftChartType.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
            this.cboLeftChartType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cboLeftChartType.ShowHeader = true;
            this.cboLeftChartType.Size = new System.Drawing.Size(123, 20);
            this.cboLeftChartType.TabIndex = 0;
            this.cboLeftChartType.TabIndexChanged += new System.EventHandler(this.cboLeftChartType_TabIndexChanged);
            // 
            // splMainSub
            // 
            this.splMainSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splMainSub.Horizontal = false;
            this.splMainSub.Location = new System.Drawing.Point(0, 0);
            this.splMainSub.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.splMainSub.Name = "splMainSub";
            this.splMainSub.Panel1.Controls.Add(this.ucXBarGrid01);
            this.splMainSub.Panel1.Text = "Panel1";
            this.splMainSub.Panel2.Controls.Add(this.grdSelectChartRawData);
            this.splMainSub.Panel2.Text = "Panel2";
            this.splMainSub.Size = new System.Drawing.Size(850, 606);
            this.splMainSub.SplitterPosition = 537;
            this.splMainSub.TabIndex = 0;
            this.splMainSub.Text = "smartSpliterContainer2";
            // 
            // ucXBarGrid01
            // 
            this.ucXBarGrid01.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucXBarGrid01.Appearance.BackColor = System.Drawing.Color.White;
            this.ucXBarGrid01.Appearance.Options.UseBackColor = true;
            this.ucXBarGrid01.Location = new System.Drawing.Point(0, 0);
            this.ucXBarGrid01.Name = "ucXBarGrid01";
            this.ucXBarGrid01.Size = new System.Drawing.Size(850, 537);
            this.ucXBarGrid01.TabIndex = 0;
            // 
            // grdSelectChartRawData
            // 
            this.grdSelectChartRawData.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdSelectChartRawData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSelectChartRawData.IsUsePaging = false;
            this.grdSelectChartRawData.LanguageKey = "SPCCHARTRAWDATA";
            this.grdSelectChartRawData.Location = new System.Drawing.Point(0, 0);
            this.grdSelectChartRawData.Margin = new System.Windows.Forms.Padding(0);
            this.grdSelectChartRawData.Name = "grdSelectChartRawData";
            this.grdSelectChartRawData.ShowBorder = true;
            this.grdSelectChartRawData.ShowStatusBar = false;
            this.grdSelectChartRawData.Size = new System.Drawing.Size(850, 64);
            this.grdSelectChartRawData.TabIndex = 1;
            // 
            // ucXBarFrame
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splMain);
            this.Name = "ucXBarFrame";
            this.Size = new System.Drawing.Size(1005, 606);
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).EndInit();
            this.splMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.palLeftMain)).EndInit();
            this.palLeftMain.ResumeLayout(false);
            this.palLeftMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtWholeMinP2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWholeMaxP2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWholeMinP1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWholeMaxP1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLeftEstimate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            this.smartGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllLslValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllRLclValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllCslValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllRUclValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllLclValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllUslValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllUclValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoLeftGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLeftChartType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splMainSub)).EndInit();
            this.splMainSub.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Framework.SmartControls.SmartPanel palLeftMain;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private Framework.SmartControls.SmartRadioGroup rdoLeftGroup;
        private Framework.SmartControls.SmartLabel lblAllRLcl;
        private Framework.SmartControls.SmartTextBox txtAllRLclValue;
        private Framework.SmartControls.SmartLabel lblAllCsl;
        private Framework.SmartControls.SmartTextBox txtAllCslValue;
        private Framework.SmartControls.SmartLabel lblAllRUcl;
        private Framework.SmartControls.SmartTextBox txtAllRUclValue;
        private Framework.SmartControls.SmartLabel lblAllLcl;
        private Framework.SmartControls.SmartTextBox txtAllLclValue;
        private Framework.SmartControls.SmartLabel lblAllUsl;
        private Framework.SmartControls.SmartTextBox txtAllUslValue;
        private Framework.SmartControls.SmartLabel lblAllUcl;
        private Framework.SmartControls.SmartTextBox txtAllUclValue;
        private Framework.SmartControls.SmartButton btnReExecution;
        private Framework.SmartControls.SmartBandedGrid grdSelectChartRawData;
        public Framework.SmartControls.SmartSpliterContainer splMain;
        public Framework.SmartControls.SmartSpliterContainer splMainSub;
        public ucXBarGrid ucXBarGrid01;
        public Framework.SmartControls.SmartComboBox cboLeftChartType;
        public Framework.SmartControls.SmartCheckBox chkLeftEstimate;
        private Framework.SmartControls.SmartLabel smartLabel4;
        private Framework.SmartControls.SmartLabel smartLabel3;
        private Framework.SmartControls.SmartTextBox txtWholeMinP2;
        private Framework.SmartControls.SmartTextBox txtWholeMaxP2;
        private Framework.SmartControls.SmartTextBox txtWholeMinP1;
        private Framework.SmartControls.SmartLabel smartLabel2;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartTextBox txtWholeMaxP1;
        private Framework.SmartControls.SmartLabel lblAllLsl;
        private Framework.SmartControls.SmartTextBox txtAllLslValue;
        public Framework.SmartControls.SmartButton btnReExecutionTest;
        public Framework.SmartControls.SmartButton btnTestDirectSpec;
    }
}
