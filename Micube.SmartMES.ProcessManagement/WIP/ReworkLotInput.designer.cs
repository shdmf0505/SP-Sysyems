namespace Micube.SmartMES.ProcessManagement
{
    partial class ReworkLotInput
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
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.txtLotId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.grdLotInfo = new Micube.SmartMES.Commons.Controls.SmartLotInfoGrid();
            this.ucReworkRouting = new Micube.SmartMES.Commons.Controls.ucReworkRouting();
            this.smartSplitTableLayoutPanel3 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.btnInit = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).BeginInit();
            this.smartSplitTableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(0, 30);
            this.pnlCondition.Size = new System.Drawing.Size(0, 0);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.smartSplitTableLayoutPanel3);
            this.pnlToolbar.Size = new System.Drawing.Size(987, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.smartSplitTableLayoutPanel3, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlContent.Size = new System.Drawing.Size(987, 565);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(987, 594);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartGroupBox1, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdLotInfo, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.ucReworkRouting, 0, 4);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 5;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(987, 565);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // smartGroupBox1
            // 
            this.smartGroupBox1.Controls.Add(this.txtLotId);
            this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartGroupBox1.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.smartGroupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.smartGroupBox1.Name = "smartGroupBox1";
            this.smartGroupBox1.Padding = new System.Windows.Forms.Padding(3);
            this.smartGroupBox1.ShowBorder = true;
            this.smartGroupBox1.ShowCaption = false;
            this.smartGroupBox1.Size = new System.Drawing.Size(987, 34);
            this.smartGroupBox1.TabIndex = 6;
            this.smartGroupBox1.Text = "smartGroupBox1";
            // 
            // txtLotId
            // 
            this.txtLotId.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtLotId.Appearance.Options.UseForeColor = true;
            this.txtLotId.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtLotId.EditorWidth = "80%";
            this.txtLotId.LabelText = "LOT NO";
            this.txtLotId.LabelWidth = "20%";
            this.txtLotId.LanguageKey = "LOTID";
            this.txtLotId.Location = new System.Drawing.Point(5, 5);
            this.txtLotId.Margin = new System.Windows.Forms.Padding(0);
            this.txtLotId.Name = "txtLotId";
            this.txtLotId.Size = new System.Drawing.Size(339, 20);
            this.txtLotId.TabIndex = 4;
            // 
            // grdLotInfo
            // 
            this.grdLotInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotInfo.Location = new System.Drawing.Point(0, 44);
            this.grdLotInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotInfo.Name = "grdLotInfo";
            this.grdLotInfo.Size = new System.Drawing.Size(987, 105);
            this.grdLotInfo.TabIndex = 2;
            // 
            // ucReworkRouting
            // 
            this.ucReworkRouting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucReworkRouting.Location = new System.Drawing.Point(3, 162);
            this.ucReworkRouting.Name = "ucReworkRouting";
            this.ucReworkRouting.ReworkTypeVisible = true;
            this.ucReworkRouting.Size = new System.Drawing.Size(981, 400);
            this.ucReworkRouting.SplitterPosition = 609;
            this.ucReworkRouting.TabIndex = 7;
            // 
            // smartSplitTableLayoutPanel3
            // 
            this.smartSplitTableLayoutPanel3.ColumnCount = 2;
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel3.Controls.Add(this.btnInit, 1, 0);
            this.smartSplitTableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel3.Location = new System.Drawing.Point(47, 0);
            this.smartSplitTableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel3.Name = "smartSplitTableLayoutPanel3";
            this.smartSplitTableLayoutPanel3.RowCount = 1;
            this.smartSplitTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel3.Size = new System.Drawing.Size(940, 24);
            this.smartSplitTableLayoutPanel3.TabIndex = 5;
            // 
            // btnInit
            // 
            this.btnInit.AllowFocus = false;
            this.btnInit.IsBusy = false;
            this.btnInit.IsWrite = false;
            this.btnInit.LanguageKey = "INITIALIZE";
            this.btnInit.Location = new System.Drawing.Point(858, 0);
            this.btnInit.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnInit.Name = "btnInit";
            this.btnInit.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnInit.Size = new System.Drawing.Size(75, 23);
            this.btnInit.TabIndex = 1;
            this.btnInit.Text = "초기화";
            this.btnInit.TooltipLanguageKey = "";
            // 
            // ReworkLotInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 632);
            this.ConditionsVisible = false;
            this.Name = "ReworkLotInput";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "ReworkLotInput";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).EndInit();
            this.smartSplitTableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Commons.Controls.SmartLotInfoGrid grdLotInfo;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel3;
        private Framework.SmartControls.SmartButton btnInit;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private Framework.SmartControls.SmartLabelTextBox txtLotId;
        private Commons.Controls.ucReworkRouting ucReworkRouting;
    }
}
