namespace Micube.SmartMES.Commons.SPCLibrary
{
    partial class TestSPCStatisticsData
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.buttonControlXBarR = new System.Windows.Forms.Button();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.comboBoxMode = new System.Windows.Forms.ComboBox();
            this.textBoxDate = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonPpCdTest = new System.Windows.Forms.Button();
            this.buttonCDFTest = new System.Windows.Forms.Button();
            this.buttonPpIMRTest = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonPpmUSL = new System.Windows.Forms.Button();
            this.buttonPpmLSL = new System.Windows.Forms.Button();
            this.spcPanel02Sub = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewPPI = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridViewResult = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxSigmaUse = new System.Windows.Forms.TextBox();
            this.textBoxSigmaMode = new System.Windows.Forms.TextBox();
            this.labelSigmaUse = new System.Windows.Forms.Label();
            this.labelSigmaMode = new System.Windows.Forms.Label();
            this.btnA2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spcPanel02Sub)).BeginInit();
            this.spcPanel02Sub.Panel1.SuspendLayout();
            this.spcPanel02Sub.Panel2.SuspendLayout();
            this.spcPanel02Sub.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPPI)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnA2);
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel1.Controls.Add(this.buttonControlXBarR);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxType);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxMode);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxDate);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.buttonPpCdTest);
            this.splitContainer1.Panel1.Controls.Add(this.buttonCDFTest);
            this.splitContainer1.Panel1.Controls.Add(this.buttonPpIMRTest);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.buttonPpmUSL);
            this.splitContainer1.Panel1.Controls.Add(this.buttonPpmLSL);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.spcPanel02Sub);
            this.splitContainer1.Size = new System.Drawing.Size(1159, 604);
            this.splitContainer1.SplitterDistance = 166;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView1.Location = new System.Drawing.Point(523, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(212, 115);
            this.dataGridView1.TabIndex = 11;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // buttonControlXBarR
            // 
            this.buttonControlXBarR.Location = new System.Drawing.Point(298, 92);
            this.buttonControlXBarR.Name = "buttonControlXBarR";
            this.buttonControlXBarR.Size = new System.Drawing.Size(111, 35);
            this.buttonControlXBarR.TabIndex = 10;
            this.buttonControlXBarR.Text = "XBar-R 관리도";
            this.buttonControlXBarR.UseVisualStyleBackColor = true;
            this.buttonControlXBarR.Click += new System.EventHandler(this.buttonControlXBarR_Click_1);
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "0-사용",
            "1-미사용"});
            this.comboBoxType.Location = new System.Drawing.Point(130, 100);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(121, 20);
            this.comboBoxType.TabIndex = 9;
            // 
            // comboBoxMode
            // 
            this.comboBoxMode.FormattingEnabled = true;
            this.comboBoxMode.Items.AddRange(new object[] {
            "R-XBAR-R",
            "S-XBAR-S",
            "P-합동",
            "M-I-MR"});
            this.comboBoxMode.Location = new System.Drawing.Point(3, 100);
            this.comboBoxMode.Name = "comboBoxMode";
            this.comboBoxMode.Size = new System.Drawing.Size(121, 20);
            this.comboBoxMode.TabIndex = 8;
            // 
            // textBoxDate
            // 
            this.textBoxDate.Location = new System.Drawing.Point(904, 66);
            this.textBoxDate.Name = "textBoxDate";
            this.textBoxDate.Size = new System.Drawing.Size(219, 21);
            this.textBoxDate.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(213, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 34);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonPpCdTest
            // 
            this.buttonPpCdTest.Location = new System.Drawing.Point(3, 49);
            this.buttonPpCdTest.Name = "buttonPpCdTest";
            this.buttonPpCdTest.Size = new System.Drawing.Size(203, 35);
            this.buttonPpCdTest.TabIndex = 5;
            this.buttonPpCdTest.Text = "공정능력 - 합동STD Test";
            this.buttonPpCdTest.UseVisualStyleBackColor = true;
            this.buttonPpCdTest.Click += new System.EventHandler(this.buttonPpCdTest_Click);
            // 
            // buttonCDFTest
            // 
            this.buttonCDFTest.Location = new System.Drawing.Point(385, 8);
            this.buttonCDFTest.Name = "buttonCDFTest";
            this.buttonCDFTest.Size = new System.Drawing.Size(89, 35);
            this.buttonCDFTest.TabIndex = 4;
            this.buttonCDFTest.Text = "CDF Test";
            this.buttonCDFTest.UseVisualStyleBackColor = true;
            this.buttonCDFTest.Click += new System.EventHandler(this.buttonCDFTest_Click_1);
            // 
            // buttonPpIMRTest
            // 
            this.buttonPpIMRTest.Location = new System.Drawing.Point(3, 8);
            this.buttonPpIMRTest.Name = "buttonPpIMRTest";
            this.buttonPpIMRTest.Size = new System.Drawing.Size(203, 35);
            this.buttonPpIMRTest.TabIndex = 0;
            this.buttonPpIMRTest.Text = "공정능력 - I-MR Test";
            this.buttonPpIMRTest.UseVisualStyleBackColor = true;
            this.buttonPpIMRTest.Click += new System.EventHandler(this.buttonPpIMRTest_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(904, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(215, 52);
            this.button2.TabIndex = 1;
            this.button2.Text = "Test ParallelForEach ";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // buttonPpmUSL
            // 
            this.buttonPpmUSL.Location = new System.Drawing.Point(298, 8);
            this.buttonPpmUSL.Name = "buttonPpmUSL";
            this.buttonPpmUSL.Size = new System.Drawing.Size(81, 35);
            this.buttonPpmUSL.TabIndex = 3;
            this.buttonPpmUSL.Text = "PPM USL";
            this.buttonPpmUSL.UseVisualStyleBackColor = true;
            // 
            // buttonPpmLSL
            // 
            this.buttonPpmLSL.Location = new System.Drawing.Point(212, 8);
            this.buttonPpmLSL.Name = "buttonPpmLSL";
            this.buttonPpmLSL.Size = new System.Drawing.Size(80, 35);
            this.buttonPpmLSL.TabIndex = 2;
            this.buttonPpmLSL.Text = "PPM LSL";
            this.buttonPpmLSL.UseVisualStyleBackColor = true;
            // 
            // spcPanel02Sub
            // 
            this.spcPanel02Sub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcPanel02Sub.Location = new System.Drawing.Point(0, 0);
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
            this.spcPanel02Sub.Size = new System.Drawing.Size(1159, 432);
            this.spcPanel02Sub.SplitterDistance = 609;
            this.spcPanel02Sub.SplitterWidth = 6;
            this.spcPanel02Sub.TabIndex = 0;
            this.spcPanel02Sub.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.spcPanel02Sub_SplitterMoved);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dataGridViewPPI);
            this.splitContainer3.Size = new System.Drawing.Size(609, 432);
            this.splitContainer3.SplitterDistance = 212;
            this.splitContainer3.TabIndex = 0;
            // 
            // dataGridViewPPI
            // 
            this.dataGridViewPPI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPPI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPPI.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewPPI.Name = "dataGridViewPPI";
            this.dataGridViewPPI.RowTemplate.Height = 23;
            this.dataGridViewPPI.Size = new System.Drawing.Size(609, 216);
            this.dataGridViewPPI.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridViewResult);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(544, 383);
            this.panel2.TabIndex = 3;
            // 
            // dataGridViewResult
            // 
            this.dataGridViewResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewResult.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewResult.Name = "dataGridViewResult";
            this.dataGridViewResult.RowTemplate.Height = 23;
            this.dataGridViewResult.Size = new System.Drawing.Size(544, 383);
            this.dataGridViewResult.TabIndex = 1;
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
            this.panel1.Size = new System.Drawing.Size(544, 49);
            this.panel1.TabIndex = 2;
            // 
            // textBoxSigmaUse
            // 
            this.textBoxSigmaUse.Location = new System.Drawing.Point(303, 16);
            this.textBoxSigmaUse.Name = "textBoxSigmaUse";
            this.textBoxSigmaUse.Size = new System.Drawing.Size(100, 21);
            this.textBoxSigmaUse.TabIndex = 3;
            // 
            // textBoxSigmaMode
            // 
            this.textBoxSigmaMode.Location = new System.Drawing.Point(77, 15);
            this.textBoxSigmaMode.Name = "textBoxSigmaMode";
            this.textBoxSigmaMode.Size = new System.Drawing.Size(100, 21);
            this.textBoxSigmaMode.TabIndex = 2;
            // 
            // labelSigmaUse
            // 
            this.labelSigmaUse.AutoSize = true;
            this.labelSigmaUse.Location = new System.Drawing.Point(196, 19);
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
            // btnA2
            // 
            this.btnA2.Location = new System.Drawing.Point(298, 49);
            this.btnA2.Name = "btnA2";
            this.btnA2.Size = new System.Drawing.Size(79, 34);
            this.btnA2.TabIndex = 12;
            this.btnA2.Text = "A2";
            this.btnA2.UseVisualStyleBackColor = true;
            this.btnA2.Click += new System.EventHandler(this.btnA2_Click);
            // 
            // TestSPCStatisticsData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 604);
            this.Controls.Add(this.splitContainer1);
            this.Name = "TestSPCStatisticsData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "관리도 & 공정능력 분석 Test ";
            this.Load += new System.EventHandler(this.TestSPCStatistics_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.spcPanel02Sub.Panel1.ResumeLayout(false);
            this.spcPanel02Sub.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcPanel02Sub)).EndInit();
            this.spcPanel02Sub.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPPI)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResult)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.Button buttonControlXBarR;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.ComboBox comboBoxMode;
        private System.Windows.Forms.TextBox textBoxDate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonPpCdTest;
        private System.Windows.Forms.Button buttonCDFTest;
        private System.Windows.Forms.Button buttonPpIMRTest;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonPpmUSL;
        private System.Windows.Forms.Button buttonPpmLSL;
        private System.Windows.Forms.SplitContainer spcPanel02Sub;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView dataGridViewPPI;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridViewResult;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxSigmaUse;
        private System.Windows.Forms.TextBox textBoxSigmaMode;
        private System.Windows.Forms.Label labelSigmaUse;
        private System.Windows.Forms.Label labelSigmaMode;
        private System.Windows.Forms.Button btnA2;
    }
}