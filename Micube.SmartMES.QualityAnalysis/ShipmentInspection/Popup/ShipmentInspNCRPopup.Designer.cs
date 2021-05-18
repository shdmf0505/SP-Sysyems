namespace Micube.SmartMES.QualityAnalysis
{
    partial class ShipmentInspNCRPopup
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
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.accRoot = new Micube.Framework.SmartControls.SmartAccordionControl();
            this.accordionContentContainer1 = new DevExpress.XtraBars.Navigation.AccordionContentContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnShipmentIssue = new Micube.Framework.SmartControls.SmartButton();
            this.accordionContentContainer2 = new DevExpress.XtraBars.Navigation.AccordionContentContainer();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.grdCauseProcessSegmentIssue = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnCauseIssue = new Micube.Framework.SmartControls.SmartButton();
            this.accGroup = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accShipment = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accCause = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.grdFinishIssue = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accRoot)).BeginInit();
            this.accRoot.SuspendLayout();
            this.accordionContentContainer1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.accordionContentContainer2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(1094, 727);
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.Name = "accordionControlElement1";
            this.accordionControlElement1.Text = "Element1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnClose, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.accRoot, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1094, 727);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(1014, 702);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "smartButton1";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // accRoot
            // 
            this.accRoot.Controls.Add(this.accordionContentContainer1);
            this.accRoot.Controls.Add(this.accordionContentContainer2);
            this.accRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accRoot.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accGroup});
            this.accRoot.Location = new System.Drawing.Point(3, 3);
            this.accRoot.Name = "accRoot";
            this.accRoot.Size = new System.Drawing.Size(1088, 691);
            this.accRoot.TabIndex = 1;
            this.accRoot.Text = "smartAccordionControl1";
            // 
            // accordionContentContainer1
            // 
            this.accordionContentContainer1.Controls.Add(this.tableLayoutPanel2);
            this.accordionContentContainer1.Name = "accordionContentContainer1";
            this.accordionContentContainer1.Size = new System.Drawing.Size(1071, 573);
            this.accordionContentContainer1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btnShipmentIssue, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.grdFinishIssue, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1071, 573);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // btnShipmentIssue
            // 
            this.btnShipmentIssue.AllowFocus = false;
            this.btnShipmentIssue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnShipmentIssue.IsBusy = false;
            this.btnShipmentIssue.IsWrite = false;
            this.btnShipmentIssue.LanguageKey = "ISSUE";
            this.btnShipmentIssue.Location = new System.Drawing.Point(988, 0);
            this.btnShipmentIssue.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnShipmentIssue.Name = "btnShipmentIssue";
            this.btnShipmentIssue.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnShipmentIssue.Size = new System.Drawing.Size(80, 25);
            this.btnShipmentIssue.TabIndex = 1;
            this.btnShipmentIssue.Text = "smartButton2";
            this.btnShipmentIssue.TooltipLanguageKey = "";
            // 
            // accordionContentContainer2
            // 
            this.accordionContentContainer2.Controls.Add(this.tableLayoutPanel3);
            this.accordionContentContainer2.Name = "accordionContentContainer2";
            this.accordionContentContainer2.Size = new System.Drawing.Size(1071, 550);
            this.accordionContentContainer2.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.grdCauseProcessSegmentIssue, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.btnCauseIssue, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1071, 550);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // grdCauseProcessSegmentIssue
            // 
            this.grdCauseProcessSegmentIssue.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdCauseProcessSegmentIssue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCauseProcessSegmentIssue.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdCauseProcessSegmentIssue.IsUsePaging = false;
            this.grdCauseProcessSegmentIssue.LanguageKey = null;
            this.grdCauseProcessSegmentIssue.Location = new System.Drawing.Point(0, 30);
            this.grdCauseProcessSegmentIssue.Margin = new System.Windows.Forms.Padding(0);
            this.grdCauseProcessSegmentIssue.Name = "grdCauseProcessSegmentIssue";
            this.grdCauseProcessSegmentIssue.ShowBorder = true;
            this.grdCauseProcessSegmentIssue.ShowButtonBar = false;
            this.grdCauseProcessSegmentIssue.Size = new System.Drawing.Size(1071, 520);
            this.grdCauseProcessSegmentIssue.TabIndex = 0;
            this.grdCauseProcessSegmentIssue.UseAutoBestFitColumns = false;
            // 
            // btnCauseIssue
            // 
            this.btnCauseIssue.AllowFocus = false;
            this.btnCauseIssue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCauseIssue.IsBusy = false;
            this.btnCauseIssue.IsWrite = false;
            this.btnCauseIssue.LanguageKey = "ISSUE";
            this.btnCauseIssue.Location = new System.Drawing.Point(988, 0);
            this.btnCauseIssue.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCauseIssue.Name = "btnCauseIssue";
            this.btnCauseIssue.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCauseIssue.Size = new System.Drawing.Size(80, 25);
            this.btnCauseIssue.TabIndex = 1;
            this.btnCauseIssue.Text = "smartButton3";
            this.btnCauseIssue.TooltipLanguageKey = "";
            // 
            // accGroup
            // 
            this.accGroup.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accShipment,
            this.accCause});
            this.accGroup.Expanded = true;
            this.accGroup.Name = "accGroup";
            this.accGroup.Text = "ELEMENT";
            // 
            // accShipment
            // 
            this.accShipment.ContentContainer = this.accordionContentContainer1;
            this.accShipment.Expanded = true;
            this.accShipment.Name = "accShipment";
            this.accShipment.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accShipment.Text = "Element3";
            // 
            // accCause
            // 
            this.accCause.ContentContainer = this.accordionContentContainer2;
            this.accCause.Expanded = true;
            this.accCause.Name = "accCause";
            this.accCause.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accCause.Text = "Element4";
            // 
            // grdFinishIssue
            // 
            this.grdFinishIssue.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdFinishIssue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFinishIssue.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdFinishIssue.IsUsePaging = false;
            this.grdFinishIssue.LanguageKey = null;
            this.grdFinishIssue.Location = new System.Drawing.Point(0, 30);
            this.grdFinishIssue.Margin = new System.Windows.Forms.Padding(0);
            this.grdFinishIssue.Name = "grdFinishIssue";
            this.grdFinishIssue.ShowBorder = true;
            this.grdFinishIssue.ShowButtonBar = false;
            this.grdFinishIssue.Size = new System.Drawing.Size(1071, 543);
            this.grdFinishIssue.TabIndex = 2;
            this.grdFinishIssue.UseAutoBestFitColumns = false;
            // 
            // ShipmentInspNCRPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 747);
            this.LanguageKey = "NCRISSUEPOPUP";
            this.Name = "ShipmentInspNCRPopup";
            this.Text = "ShipmentInspNCRPopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.accRoot)).EndInit();
            this.accRoot.ResumeLayout(false);
            this.accordionContentContainer1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.accordionContentContainer2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartAccordionControl accRoot;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accGroup;
        private DevExpress.XtraBars.Navigation.AccordionContentContainer accordionContentContainer1;
        private DevExpress.XtraBars.Navigation.AccordionContentContainer accordionContentContainer2;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accShipment;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accCause;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Framework.SmartControls.SmartButton btnShipmentIssue;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Framework.SmartControls.SmartBandedGrid grdCauseProcessSegmentIssue;
        private Framework.SmartControls.SmartButton btnCauseIssue;
        private Framework.SmartControls.SmartBandedGrid grdFinishIssue;
    }
}