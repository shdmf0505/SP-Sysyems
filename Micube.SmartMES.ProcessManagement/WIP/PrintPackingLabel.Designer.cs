namespace Micube.SmartMES.ProcessManagement
{
    partial class PrintPackingLabel
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
            this.grdPackingList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.spcSpliter = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.btnPrintLabel = new Micube.Framework.SmartControls.SmartButton();
            this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabInfo = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgLotInfo = new DevExpress.XtraTab.XtraTabPage();
            this.tpgXoutCase = new DevExpress.XtraTab.XtraTabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grdXOUT = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnPrintXoutInnerLabel = new Micube.Framework.SmartControls.SmartButton();
            this.btnPrintXoutOuterLabel = new Micube.Framework.SmartControls.SmartButton();
            this.smartSpliterControl5 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.panel5 = new System.Windows.Forms.Panel();
            this.grdCase = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPrintLabelCase = new Micube.Framework.SmartControls.SmartButton();
            this.btnPrintTrayLabel = new Micube.Framework.SmartControls.SmartButton();
            this.btnPrintLabel2 = new Micube.Framework.SmartControls.SmartButton();
            this.btnChangeCustomer = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabInfo)).BeginInit();
            this.tabInfo.SuspendLayout();
            this.tpgLotInfo.SuspendLayout();
            this.tpgXoutCase.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.OptionsView.UseDefaultDragAndDropRendering = false;
            this.pnlCondition.Size = new System.Drawing.Size(296, 467);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnChangeCustomer);
            this.pnlToolbar.Controls.Add(this.btnPrintLabel2);
            this.pnlToolbar.Controls.Add(this.btnPrintTrayLabel);
            this.pnlToolbar.Controls.Add(this.btnPrintLabel);
            this.pnlToolbar.Size = new System.Drawing.Size(738, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.btnPrintLabel, 0);
            this.pnlToolbar.Controls.SetChildIndex(this.btnPrintTrayLabel, 0);
            this.pnlToolbar.Controls.SetChildIndex(this.btnPrintLabel2, 0);
            this.pnlToolbar.Controls.SetChildIndex(this.btnChangeCustomer, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.grdPackingList);
            this.pnlContent.Controls.Add(this.spcSpliter);
            this.pnlContent.Controls.Add(this.tabInfo);
            this.pnlContent.Size = new System.Drawing.Size(738, 471);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1043, 500);
            // 
            // grdPackingList
            // 
            this.grdPackingList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdPackingList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPackingList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdPackingList.IsUsePaging = false;
            this.grdPackingList.LanguageKey = "WIPLIST";
            this.grdPackingList.Location = new System.Drawing.Point(0, 0);
            this.grdPackingList.Margin = new System.Windows.Forms.Padding(0);
            this.grdPackingList.Name = "grdPackingList";
            this.grdPackingList.ShowBorder = true;
            this.grdPackingList.Size = new System.Drawing.Size(738, 166);
            this.grdPackingList.TabIndex = 1;
            this.grdPackingList.UseAutoBestFitColumns = false;
            // 
            // spcSpliter
            // 
            this.spcSpliter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.spcSpliter.Location = new System.Drawing.Point(0, 166);
            this.spcSpliter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.spcSpliter.Name = "spcSpliter";
            this.spcSpliter.Size = new System.Drawing.Size(738, 5);
            this.spcSpliter.TabIndex = 5;
            this.spcSpliter.TabStop = false;
            // 
            // btnPrintLabel
            // 
            this.btnPrintLabel.AllowFocus = false;
            this.btnPrintLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintLabel.IsBusy = false;
            this.btnPrintLabel.IsWrite = false;
            this.btnPrintLabel.LanguageKey = "PRINTBOXLABEL";
            this.btnPrintLabel.Location = new System.Drawing.Point(72, 0);
            this.btnPrintLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPrintLabel.Name = "btnPrintLabel";
            this.btnPrintLabel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPrintLabel.Size = new System.Drawing.Size(123, 24);
            this.btnPrintLabel.TabIndex = 5;
            this.btnPrintLabel.Text = "Box 라벨출력";
            this.btnPrintLabel.TooltipLanguageKey = "";
            this.btnPrintLabel.Visible = false;
            // 
            // grdLotList
            // 
            this.grdLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLotList.IsUsePaging = false;
            this.grdLotList.LanguageKey = "LOTINFO";
            this.grdLotList.Location = new System.Drawing.Point(3, 3);
            this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotList.Name = "grdLotList";
            this.grdLotList.ShowBorder = true;
            this.grdLotList.ShowStatusBar = false;
            this.grdLotList.Size = new System.Drawing.Size(726, 265);
            this.grdLotList.TabIndex = 6;
            this.grdLotList.UseAutoBestFitColumns = false;
            // 
            // tabInfo
            // 
            this.tabInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabInfo.Location = new System.Drawing.Point(0, 171);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.SelectedTabPage = this.tpgLotInfo;
            this.tabInfo.Size = new System.Drawing.Size(738, 300);
            this.tabInfo.TabIndex = 7;
            this.tabInfo.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgLotInfo,
            this.tpgXoutCase});
            // 
            // tpgLotInfo
            // 
            this.tpgLotInfo.Controls.Add(this.grdLotList);
            this.tabInfo.SetLanguageKey(this.tpgLotInfo, "LOTINFO");
            this.tpgLotInfo.Name = "tpgLotInfo";
            this.tpgLotInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpgLotInfo.Size = new System.Drawing.Size(732, 271);
            this.tpgLotInfo.Text = "Lot 정보";
            // 
            // tpgXoutCase
            // 
            this.tpgXoutCase.Controls.Add(this.panel2);
            this.tpgXoutCase.Controls.Add(this.smartSpliterControl5);
            this.tpgXoutCase.Controls.Add(this.panel5);
            this.tpgXoutCase.Name = "tpgXoutCase";
            this.tpgXoutCase.Padding = new System.Windows.Forms.Padding(3);
            this.tpgXoutCase.Size = new System.Drawing.Size(732, 271);
            this.tpgXoutCase.Text = "X-OUT / Case";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.grdXOUT);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(409, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(320, 265);
            this.panel2.TabIndex = 26;
            // 
            // grdXOUT
            // 
            this.grdXOUT.Caption = "X-OUT";
            this.grdXOUT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdXOUT.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdXOUT.IsUsePaging = false;
            this.grdXOUT.LanguageKey = "";
            this.grdXOUT.Location = new System.Drawing.Point(0, 40);
            this.grdXOUT.Margin = new System.Windows.Forms.Padding(0);
            this.grdXOUT.Name = "grdXOUT";
            this.grdXOUT.ShowBorder = true;
            this.grdXOUT.ShowStatusBar = false;
            this.grdXOUT.Size = new System.Drawing.Size(320, 225);
            this.grdXOUT.TabIndex = 22;
            this.grdXOUT.UseAutoBestFitColumns = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnPrintXoutInnerLabel);
            this.panel3.Controls.Add(this.btnPrintXoutOuterLabel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(320, 40);
            this.panel3.TabIndex = 25;
            // 
            // btnPrintXoutInnerLabel
            // 
            this.btnPrintXoutInnerLabel.AllowFocus = false;
            this.btnPrintXoutInnerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintXoutInnerLabel.IsBusy = false;
            this.btnPrintXoutInnerLabel.IsWrite = false;
            this.btnPrintXoutInnerLabel.LanguageKey = "PRINTXOUTINNERLABEL";
            this.btnPrintXoutInnerLabel.Location = new System.Drawing.Point(57, 6);
            this.btnPrintXoutInnerLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPrintXoutInnerLabel.Name = "btnPrintXoutInnerLabel";
            this.btnPrintXoutInnerLabel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPrintXoutInnerLabel.Size = new System.Drawing.Size(128, 28);
            this.btnPrintXoutInnerLabel.TabIndex = 1;
            this.btnPrintXoutInnerLabel.Text = "내부라벨 출력";
            this.btnPrintXoutInnerLabel.TooltipLanguageKey = "";
            this.btnPrintXoutInnerLabel.Visible = false;
            // 
            // btnPrintXoutOuterLabel
            // 
            this.btnPrintXoutOuterLabel.AllowFocus = false;
            this.btnPrintXoutOuterLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintXoutOuterLabel.IsBusy = false;
            this.btnPrintXoutOuterLabel.IsWrite = false;
            this.btnPrintXoutOuterLabel.LanguageKey = "PRINTXOUTOUTERLABEL";
            this.btnPrintXoutOuterLabel.Location = new System.Drawing.Point(191, 6);
            this.btnPrintXoutOuterLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPrintXoutOuterLabel.Name = "btnPrintXoutOuterLabel";
            this.btnPrintXoutOuterLabel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPrintXoutOuterLabel.Size = new System.Drawing.Size(128, 28);
            this.btnPrintXoutOuterLabel.TabIndex = 0;
            this.btnPrintXoutOuterLabel.Text = "외부라벨 출력";
            this.btnPrintXoutOuterLabel.TooltipLanguageKey = "";
            this.btnPrintXoutOuterLabel.Visible = false;
            // 
            // smartSpliterControl5
            // 
            this.smartSpliterControl5.Location = new System.Drawing.Point(404, 3);
            this.smartSpliterControl5.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl5.Name = "smartSpliterControl5";
            this.smartSpliterControl5.Size = new System.Drawing.Size(5, 265);
            this.smartSpliterControl5.TabIndex = 24;
            this.smartSpliterControl5.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.grdCase);
            this.panel5.Controls.Add(this.panel1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(401, 265);
            this.panel5.TabIndex = 25;
            // 
            // grdCase
            // 
            this.grdCase.Caption = "Case";
            this.grdCase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCase.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdCase.IsUsePaging = false;
            this.grdCase.LanguageKey = "";
            this.grdCase.Location = new System.Drawing.Point(0, 40);
            this.grdCase.Margin = new System.Windows.Forms.Padding(0);
            this.grdCase.Name = "grdCase";
            this.grdCase.ShowBorder = true;
            this.grdCase.ShowStatusBar = false;
            this.grdCase.Size = new System.Drawing.Size(401, 225);
            this.grdCase.TabIndex = 23;
            this.grdCase.UseAutoBestFitColumns = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPrintLabelCase);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(401, 40);
            this.panel1.TabIndex = 24;
            // 
            // btnPrintLabelCase
            // 
            this.btnPrintLabelCase.AllowFocus = false;
            this.btnPrintLabelCase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintLabelCase.IsBusy = false;
            this.btnPrintLabelCase.IsWrite = false;
            this.btnPrintLabelCase.LanguageKey = "PRINTCASELABEL";
            this.btnPrintLabelCase.Location = new System.Drawing.Point(283, 6);
            this.btnPrintLabelCase.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPrintLabelCase.Name = "btnPrintLabelCase";
            this.btnPrintLabelCase.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPrintLabelCase.Size = new System.Drawing.Size(117, 28);
            this.btnPrintLabelCase.TabIndex = 0;
            this.btnPrintLabelCase.Text = "Case 라벨 출력";
            this.btnPrintLabelCase.TooltipLanguageKey = "";
            this.btnPrintLabelCase.Visible = false;
            // 
            // btnPrintTrayLabel
            // 
            this.btnPrintTrayLabel.AllowFocus = false;
            this.btnPrintTrayLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintTrayLabel.IsBusy = false;
            this.btnPrintTrayLabel.IsWrite = false;
            this.btnPrintTrayLabel.LanguageKey = "PRINTTRAYLABEL";
            this.btnPrintTrayLabel.Location = new System.Drawing.Point(11, 0);
            this.btnPrintTrayLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPrintTrayLabel.Name = "btnPrintTrayLabel";
            this.btnPrintTrayLabel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPrintTrayLabel.Size = new System.Drawing.Size(123, 24);
            this.btnPrintTrayLabel.TabIndex = 5;
            this.btnPrintTrayLabel.Text = "Tray 라벨출력";
            this.btnPrintTrayLabel.TooltipLanguageKey = "";
            this.btnPrintTrayLabel.Visible = false;
            // 
            // btnPrintLabel2
            // 
            this.btnPrintLabel2.AllowFocus = false;
            this.btnPrintLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintLabel2.IsBusy = false;
            this.btnPrintLabel2.IsWrite = false;
            this.btnPrintLabel2.LanguageKey = "PRINTLABEL";
            this.btnPrintLabel2.Location = new System.Drawing.Point(589, 0);
            this.btnPrintLabel2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPrintLabel2.Name = "btnPrintLabel2";
            this.btnPrintLabel2.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPrintLabel2.Size = new System.Drawing.Size(123, 24);
            this.btnPrintLabel2.TabIndex = 5;
            this.btnPrintLabel2.Text = "라벨출력";
            this.btnPrintLabel2.TooltipLanguageKey = "";
            // 
            // btnChangeCustomer
            // 
            this.btnChangeCustomer.AllowFocus = false;
            this.btnChangeCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeCustomer.IsBusy = false;
            this.btnChangeCustomer.IsWrite = false;
            this.btnChangeCustomer.Location = new System.Drawing.Point(201, 0);
            this.btnChangeCustomer.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnChangeCustomer.Name = "btnChangeCustomer";
            this.btnChangeCustomer.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnChangeCustomer.Size = new System.Drawing.Size(123, 24);
            this.btnChangeCustomer.TabIndex = 5;
            this.btnChangeCustomer.Text = "고객사변경";
            this.btnChangeCustomer.TooltipLanguageKey = "";
            // 
            // PrintPackingLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 538);
            this.LanguageKey = "PRINTLABEL";
            this.Name = "PrintPackingLabel";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "포장라벨 출력";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabInfo)).EndInit();
            this.tabInfo.ResumeLayout(false);
            this.tpgLotInfo.ResumeLayout(false);
            this.tpgXoutCase.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdPackingList;
        private Framework.SmartControls.SmartSpliterControl spcSpliter;
        private Framework.SmartControls.SmartButton btnPrintLabel;
        private Framework.SmartControls.SmartBandedGrid grdLotList;
        private Framework.SmartControls.SmartTabControl tabInfo;
        private DevExpress.XtraTab.XtraTabPage tpgLotInfo;
        private DevExpress.XtraTab.XtraTabPage tpgXoutCase;
        private Framework.SmartControls.SmartBandedGrid grdXOUT;
        private Framework.SmartControls.SmartBandedGrid grdCase;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private Framework.SmartControls.SmartButton btnPrintXoutOuterLabel;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel1;
        private Framework.SmartControls.SmartButton btnPrintLabelCase;
        private Framework.SmartControls.SmartButton btnPrintXoutInnerLabel;
        private Framework.SmartControls.SmartButton btnPrintTrayLabel;
        private Framework.SmartControls.SmartButton btnPrintLabel2;
        private Framework.SmartControls.SmartButton btnChangeCustomer;
    }
}