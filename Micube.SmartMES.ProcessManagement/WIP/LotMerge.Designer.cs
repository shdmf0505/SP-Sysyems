namespace Micube.SmartMES.ProcessManagement
{
    partial class LotMerge
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
            this.grdWIP = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.smartGroupBox2 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.grdSource = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.ucDataUpDownBtnCtrl2 = new Micube.SmartMES.Commons.Controls.ucDataUpDownBtn();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.grdTarget = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.ucDataUpDownBtnCtrl = new Micube.SmartMES.Commons.Controls.ucDataUpDownBtn();
            this.spcSpliter = new Micube.Framework.SmartControls.SmartSpliterControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox2)).BeginInit();
            this.smartGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(371, 618);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(917, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.panel1);
            this.pnlContent.Controls.Add(this.spcSpliter);
            this.pnlContent.Controls.Add(this.grdWIP);
            this.pnlContent.Size = new System.Drawing.Size(917, 621);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1298, 657);
            // 
            // grdWIP
            // 
            this.grdWIP.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdWIP.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdWIP.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdWIP.IsUsePaging = false;
            this.grdWIP.LanguageKey = "WIPLIST";
            this.grdWIP.Location = new System.Drawing.Point(0, 0);
            this.grdWIP.Margin = new System.Windows.Forms.Padding(0);
            this.grdWIP.Name = "grdWIP";
            this.grdWIP.ShowBorder = true;
            this.grdWIP.Size = new System.Drawing.Size(917, 174);
            this.grdWIP.TabIndex = 1;
            this.grdWIP.UseAutoBestFitColumns = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.smartGroupBox2);
            this.panel1.Controls.Add(this.smartSpliterControl1);
            this.panel1.Controls.Add(this.smartGroupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 180);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(917, 441);
            this.panel1.TabIndex = 4;
            // 
            // smartGroupBox2
            // 
            this.smartGroupBox2.Controls.Add(this.grdSource);
            this.smartGroupBox2.Controls.Add(this.ucDataUpDownBtnCtrl2);
            this.smartGroupBox2.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartGroupBox2.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox2.LanguageKey = "TARGETMERGE";
            this.smartGroupBox2.Location = new System.Drawing.Point(676, 0);
            this.smartGroupBox2.Name = "smartGroupBox2";
            this.smartGroupBox2.ShowBorder = true;
            this.smartGroupBox2.Size = new System.Drawing.Size(241, 441);
            this.smartGroupBox2.TabIndex = 9;
            this.smartGroupBox2.Text = "병합대상";
            // 
            // grdSource
            // 
            this.grdSource.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSource.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdSource.IsUsePaging = false;
            this.grdSource.LanguageKey = "TARGETMERGE";
            this.grdSource.Location = new System.Drawing.Point(2, 89);
            this.grdSource.Margin = new System.Windows.Forms.Padding(0);
            this.grdSource.Name = "grdSource";
            this.grdSource.ShowBorder = true;
            this.grdSource.ShowStatusBar = false;
            this.grdSource.Size = new System.Drawing.Size(237, 350);
            this.grdSource.TabIndex = 7;
            this.grdSource.UseAutoBestFitColumns = false;
            // 
            // ucDataUpDownBtnCtrl2
            // 
            this.ucDataUpDownBtnCtrl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucDataUpDownBtnCtrl2.Location = new System.Drawing.Point(2, 37);
            this.ucDataUpDownBtnCtrl2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ucDataUpDownBtnCtrl2.Name = "ucDataUpDownBtnCtrl2";
            this.ucDataUpDownBtnCtrl2.Size = new System.Drawing.Size(237, 52);
            this.ucDataUpDownBtnCtrl2.SourceGrid = null;
            this.ucDataUpDownBtnCtrl2.TabIndex = 6;
            this.ucDataUpDownBtnCtrl2.TargetGrid = null;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Location = new System.Drawing.Point(670, 0);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(6, 441);
            this.smartSpliterControl1.TabIndex = 8;
            this.smartSpliterControl1.TabStop = false;
            // 
            // smartGroupBox1
            // 
            this.smartGroupBox1.Controls.Add(this.grdTarget);
            this.smartGroupBox1.Controls.Add(this.ucDataUpDownBtnCtrl);
            this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox1.LanguageKey = "TARGETWIP";
            this.smartGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.smartGroupBox1.Name = "smartGroupBox1";
            this.smartGroupBox1.ShowBorder = true;
            this.smartGroupBox1.Size = new System.Drawing.Size(670, 441);
            this.smartGroupBox1.TabIndex = 7;
            this.smartGroupBox1.Text = "기준LOT";
            // 
            // grdTarget
            // 
            this.grdTarget.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTarget.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdTarget.IsUsePaging = false;
            this.grdTarget.LanguageKey = "TARGETWIP";
            this.grdTarget.Location = new System.Drawing.Point(2, 89);
            this.grdTarget.Margin = new System.Windows.Forms.Padding(0);
            this.grdTarget.Name = "grdTarget";
            this.grdTarget.ShowBorder = true;
            this.grdTarget.ShowStatusBar = false;
            this.grdTarget.Size = new System.Drawing.Size(666, 350);
            this.grdTarget.TabIndex = 2;
            this.grdTarget.UseAutoBestFitColumns = false;
            // 
            // ucDataUpDownBtnCtrl
            // 
            this.ucDataUpDownBtnCtrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucDataUpDownBtnCtrl.Location = new System.Drawing.Point(2, 37);
            this.ucDataUpDownBtnCtrl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucDataUpDownBtnCtrl.Name = "ucDataUpDownBtnCtrl";
            this.ucDataUpDownBtnCtrl.Size = new System.Drawing.Size(666, 52);
            this.ucDataUpDownBtnCtrl.SourceGrid = null;
            this.ucDataUpDownBtnCtrl.TabIndex = 5;
            this.ucDataUpDownBtnCtrl.TargetGrid = null;
            // 
            // spcSpliter
            // 
            this.spcSpliter.Dock = System.Windows.Forms.DockStyle.Top;
            this.spcSpliter.Location = new System.Drawing.Point(0, 174);
            this.spcSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcSpliter.Name = "spcSpliter";
            this.spcSpliter.Size = new System.Drawing.Size(917, 6);
            this.spcSpliter.TabIndex = 7;
            this.spcSpliter.TabStop = false;
            // 
            // LotMerge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1328, 687);
            this.Name = "LotMerge";
            this.Text = "Lot Merge";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox2)).EndInit();
            this.smartGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdWIP;
        private System.Windows.Forms.Panel panel1;
        private Framework.SmartControls.SmartBandedGrid grdTarget;
        private Framework.SmartControls.SmartSpliterControl spcSpliter;
        private Micube.SmartMES.Commons.Controls.ucDataUpDownBtn ucDataUpDownBtnCtrl;
        private Framework.SmartControls.SmartGroupBox smartGroupBox2;
        private Framework.SmartControls.SmartBandedGrid grdSource;
        private Commons.Controls.ucDataUpDownBtn ucDataUpDownBtnCtrl2;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
    }
}