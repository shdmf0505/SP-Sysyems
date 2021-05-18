namespace Micube.SmartMES.StandardInfo
{
    partial class InspectionAlarmActionManagement
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
            this.spcMain = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdQCInterlockGrade = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdQCInterLockAction = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.grdQcInterlock = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spcMain)).BeginInit();
            this.spcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 496);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(869, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.spcMain);
            this.pnlContent.Size = new System.Drawing.Size(869, 500);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1174, 529);
            // 
            // spcMain
            // 
            this.spcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcMain.Location = new System.Drawing.Point(0, 0);
            this.spcMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcMain.Name = "spcMain";
            this.spcMain.Panel1.Controls.Add(this.smartSpliterContainer1);
            this.spcMain.Panel1.Text = "Panel1";
            this.spcMain.Panel2.Controls.Add(this.tableLayoutPanel3);
            this.spcMain.Panel2.Text = "Panel2";
            this.spcMain.Size = new System.Drawing.Size(869, 500);
            this.spcMain.SplitterPosition = 649;
            this.spcMain.TabIndex = 1;
            this.spcMain.Text = "smartSpliterContainer2";
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdQCInterlockGrade);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdQCInterLockAction);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(649, 500);
            this.smartSpliterContainer1.SplitterPosition = 212;
            this.smartSpliterContainer1.TabIndex = 3;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdQCInterlockGrade
            // 
            this.grdQCInterlockGrade.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdQCInterlockGrade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdQCInterlockGrade.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdQCInterlockGrade.IsUsePaging = false;
            this.grdQCInterlockGrade.LanguageKey = "QCGRADECAPTION";
            this.grdQCInterlockGrade.Location = new System.Drawing.Point(0, 0);
            this.grdQCInterlockGrade.Margin = new System.Windows.Forms.Padding(0);
            this.grdQCInterlockGrade.Name = "grdQCInterlockGrade";
            this.grdQCInterlockGrade.ShowBorder = true;
            this.grdQCInterlockGrade.ShowStatusBar = false;
            this.grdQCInterlockGrade.Size = new System.Drawing.Size(212, 500);
            this.grdQCInterlockGrade.TabIndex = 2;
            // 
            // grdQCInterLockAction
            // 
            this.grdQCInterLockAction.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdQCInterLockAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdQCInterLockAction.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdQCInterLockAction.IsUsePaging = false;
            this.grdQCInterLockAction.LanguageKey = "QCACTIONCAPTION";
            this.grdQCInterLockAction.Location = new System.Drawing.Point(0, 0);
            this.grdQCInterLockAction.Margin = new System.Windows.Forms.Padding(0);
            this.grdQCInterLockAction.Name = "grdQCInterLockAction";
            this.grdQCInterLockAction.ShowBorder = true;
            this.grdQCInterLockAction.ShowStatusBar = false;
            this.grdQCInterLockAction.Size = new System.Drawing.Size(432, 500);
            this.grdQCInterLockAction.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.grdQcInterlock, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 500F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 500F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(215, 500);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // grdQcInterlock
            // 
            this.grdQcInterlock.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdQcInterlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdQcInterlock.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdQcInterlock.IsUsePaging = false;
            this.grdQcInterlock.LanguageKey = "QCGRADENGCAPTION";
            this.grdQcInterlock.Location = new System.Drawing.Point(0, 0);
            this.grdQcInterlock.Margin = new System.Windows.Forms.Padding(0);
            this.grdQcInterlock.Name = "grdQcInterlock";
            this.grdQcInterlock.ShowBorder = true;
            this.grdQcInterlock.ShowStatusBar = false;
            this.grdQcInterlock.Size = new System.Drawing.Size(215, 500);
            this.grdQcInterlock.TabIndex = 3;
            // 
            // InspectionAlarmActionManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 549);
            this.Name = "InspectionAlarmActionManagement";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spcMain)).EndInit();
            this.spcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.SmartControls.SmartSpliterContainer spcMain;
        private Framework.SmartControls.SmartBandedGrid grdQcInterlock;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Framework.SmartControls.SmartBandedGrid grdQCInterlockGrade;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdQCInterLockAction;
    }
}