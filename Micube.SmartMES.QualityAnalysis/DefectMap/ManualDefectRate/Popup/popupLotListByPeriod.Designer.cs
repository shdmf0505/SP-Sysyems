namespace Micube.SmartMES.QualityAnalysis
{
    partial class popupLotListByPeriod
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
            this.components = new System.ComponentModel.Container();
            this.layoutMain = new Micube.Framework.SmartControls.SmartLayoutControl();
            this.smartLayoutControlGroup1 = new Micube.Framework.SmartControls.SmartLayoutControlGroup();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnOK = new Micube.Framework.SmartControls.SmartButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.grdMain = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dtpFrom = new Micube.Framework.SmartControls.SmartDateEdit();
            this.layoutFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.dtpTo = new Micube.Framework.SmartControls.SmartDateEdit();
            this.layoutTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtLot = new Micube.Framework.SmartControls.SmartTextBox();
            this.layoutLot = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtProduct = new Micube.Framework.SmartControls.SmartTextBox();
            this.layoutProduct = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutMain)).BeginInit();
            this.layoutMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLot.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutLot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProduct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.layoutMain);
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Size = new System.Drawing.Size(693, 450);
            // 
            // smartLayoutControl1
            // 
            this.layoutMain.Controls.Add(this.btnSearch);
            this.layoutMain.Controls.Add(this.txtProduct);
            this.layoutMain.Controls.Add(this.txtLot);
            this.layoutMain.Controls.Add(this.dtpTo);
            this.layoutMain.Controls.Add(this.dtpFrom);
            this.layoutMain.Controls.Add(this.grdMain);
            this.layoutMain.Controls.Add(this.btnOK);
            this.layoutMain.Controls.Add(this.btnCancel);
            this.layoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutMain.Location = new System.Drawing.Point(0, 0);
            this.layoutMain.Name = "smartLayoutControl1";
            this.layoutMain.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1232, 191, 650, 400);
            this.layoutMain.Root = this.smartLayoutControlGroup1;
            this.layoutMain.Size = new System.Drawing.Size(693, 450);
            this.layoutMain.TabIndex = 0;
            this.layoutMain.Text = "smartLayoutControl1";
            // 
            // smartLayoutControlGroup1
            // 
            this.smartLayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.smartLayoutControlGroup1.GroupBordersVisible = false;
            this.smartLayoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.layoutControlItem3,
            this.layoutFrom,
            this.layoutTo,
            this.layoutLot,
            this.layoutProduct,
            this.layoutControlItem8,
            this.emptySpaceItem1});
            this.smartLayoutControlGroup1.Name = "Root";
            this.smartLayoutControlGroup1.Size = new System.Drawing.Size(693, 450);
            this.smartLayoutControlGroup1.TextVisible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.IsBusy = false;
            this.btnCancel.Location = new System.Drawing.Point(596, 416);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(85, 22);
            this.btnCancel.StyleController = this.layoutMain;
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "smartButton1";
            this.btnCancel.TooltipLanguageKey = "";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnCancel;
            this.layoutControlItem1.Location = new System.Drawing.Point(584, 404);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(89, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(89, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(89, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // btnOK
            // 
            this.btnOK.AllowFocus = false;
            this.btnOK.IsBusy = false;
            this.btnOK.Location = new System.Drawing.Point(507, 416);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnOK.Size = new System.Drawing.Size(85, 22);
            this.btnOK.StyleController = this.layoutMain;
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "smartButton2";
            this.btnOK.TooltipLanguageKey = "";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnOK;
            this.layoutControlItem2.Location = new System.Drawing.Point(495, 404);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(89, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(89, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(89, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 404);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(495, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // grdMain
            // 
            this.grdMain.Caption = "";
            this.grdMain.IsUsePaging = false;
            this.grdMain.LanguageKey = null;
            this.grdMain.Location = new System.Drawing.Point(12, 62);
            this.grdMain.Margin = new System.Windows.Forms.Padding(0);
            this.grdMain.Name = "grdMain";
            this.grdMain.ShowBorder = true;
            this.grdMain.Size = new System.Drawing.Size(669, 350);
            this.grdMain.TabIndex = 6;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.grdMain;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(673, 354);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // dtpFrom
            // 
            this.dtpFrom.EditValue = null;
            this.dtpFrom.LabelText = null;
            this.dtpFrom.LanguageKey = null;
            this.dtpFrom.Location = new System.Drawing.Point(92, 12);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFrom.Size = new System.Drawing.Size(208, 20);
            this.dtpFrom.StyleController = this.layoutMain;
            this.dtpFrom.TabIndex = 7;
            // 
            // layoutFrom
            // 
            this.layoutFrom.Control = this.dtpFrom;
            this.layoutFrom.Location = new System.Drawing.Point(0, 0);
            this.layoutFrom.MaxSize = new System.Drawing.Size(292, 24);
            this.layoutFrom.MinSize = new System.Drawing.Size(292, 24);
            this.layoutFrom.Name = "layoutFrom";
            this.layoutFrom.Size = new System.Drawing.Size(292, 26);
            this.layoutFrom.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutFrom.TextSize = new System.Drawing.Size(76, 14);
            // 
            // dtpTo
            // 
            this.dtpTo.EditValue = null;
            this.dtpTo.LabelText = null;
            this.dtpTo.LanguageKey = null;
            this.dtpTo.Location = new System.Drawing.Point(384, 12);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTo.Size = new System.Drawing.Size(208, 20);
            this.dtpTo.StyleController = this.layoutMain;
            this.dtpTo.TabIndex = 8;
            // 
            // layoutTo
            // 
            this.layoutTo.Control = this.dtpTo;
            this.layoutTo.Location = new System.Drawing.Point(292, 0);
            this.layoutTo.Name = "layoutTo";
            this.layoutTo.Size = new System.Drawing.Size(292, 26);
            this.layoutTo.TextSize = new System.Drawing.Size(76, 14);
            // 
            // txtLot
            // 
            this.txtLot.LabelText = null;
            this.txtLot.LanguageKey = null;
            this.txtLot.Location = new System.Drawing.Point(92, 38);
            this.txtLot.Name = "txtLot";
            this.txtLot.Size = new System.Drawing.Size(208, 20);
            this.txtLot.StyleController = this.layoutMain;
            this.txtLot.TabIndex = 9;
            // 
            // layoutLot
            // 
            this.layoutLot.Control = this.txtLot;
            this.layoutLot.Location = new System.Drawing.Point(0, 26);
            this.layoutLot.MaxSize = new System.Drawing.Size(292, 24);
            this.layoutLot.MinSize = new System.Drawing.Size(292, 24);
            this.layoutLot.Name = "layoutLot";
            this.layoutLot.Size = new System.Drawing.Size(292, 24);
            this.layoutLot.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutLot.TextSize = new System.Drawing.Size(76, 14);
            // 
            // txtProduct
            // 
            this.txtProduct.LabelText = null;
            this.txtProduct.LanguageKey = null;
            this.txtProduct.Location = new System.Drawing.Point(384, 38);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(208, 20);
            this.txtProduct.StyleController = this.layoutMain;
            this.txtProduct.TabIndex = 10;
            // 
            // layoutProduct
            // 
            this.layoutProduct.Control = this.txtProduct;
            this.layoutProduct.Location = new System.Drawing.Point(292, 26);
            this.layoutProduct.MaxSize = new System.Drawing.Size(292, 24);
            this.layoutProduct.MinSize = new System.Drawing.Size(292, 24);
            this.layoutProduct.Name = "layoutProduct";
            this.layoutProduct.Size = new System.Drawing.Size(292, 24);
            this.layoutProduct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutProduct.TextSize = new System.Drawing.Size(76, 14);
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.IsBusy = false;
            this.btnSearch.Location = new System.Drawing.Point(596, 12);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(85, 22);
            this.btnSearch.StyleController = this.layoutMain;
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "smartButton1";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnSearch;
            this.layoutControlItem8.Location = new System.Drawing.Point(584, 0);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(89, 26);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(89, 26);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(89, 26);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(584, 26);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(89, 24);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // popupLotListByPeriod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 450);
            this.Name = "popupLotListByPeriod";
            this.Padding = new System.Windows.Forms.Padding(0);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutMain)).EndInit();
            this.layoutMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartLayoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLot.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutLot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProduct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartLayoutControl layoutMain;
        private Framework.SmartControls.SmartLayoutControlGroup smartLayoutControlGroup1;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartTextBox txtProduct;
        private Framework.SmartControls.SmartTextBox txtLot;
        private Framework.SmartControls.SmartDateEdit dtpTo;
        private Framework.SmartControls.SmartDateEdit dtpFrom;
        private Framework.SmartControls.SmartBandedGrid grdMain;
        private Framework.SmartControls.SmartButton btnOK;
        private Framework.SmartControls.SmartButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutFrom;
        private DevExpress.XtraLayout.LayoutControlItem layoutTo;
        private DevExpress.XtraLayout.LayoutControlItem layoutLot;
        private DevExpress.XtraLayout.LayoutControlItem layoutProduct;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}
