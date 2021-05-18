namespace Micube.SmartMES.ProcessManagement
{
    partial class ucEquipment
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.hideContainerRight = new DevExpress.XtraBars.Docking.AutoHideContainer();
            this.smartDockManager1 = new Micube.Framework.SmartControls.SmartDockManager();
            this.dkEquipment = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.grdEquipmentRecipe = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.tlpEquipment = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdEquipment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.pnlEquipment = new Micube.Framework.SmartControls.SmartPanel();
            this.txtEquipmentId = new Micube.Framework.SmartControls.SmartLabelTextBox();
            this.txtEquipment = new Micube.Framework.SmartControls.SmartLabelSelectPopupEdit();
            this.autoHideContainer1 = new DevExpress.XtraBars.Docking.AutoHideContainer();
            ((System.ComponentModel.ISupportInitialize)(this.smartDockManager1)).BeginInit();
            this.dkEquipment.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            this.tlpEquipment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlEquipment)).BeginInit();
            this.pnlEquipment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEquipmentId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEquipment.Properties)).BeginInit();
            this.autoHideContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hideContainerRight
            // 
            this.hideContainerRight.BackColor = System.Drawing.SystemColors.Control;
            this.hideContainerRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.hideContainerRight.Location = new System.Drawing.Point(542, 0);
            this.hideContainerRight.Name = "hideContainerRight";
            this.hideContainerRight.Size = new System.Drawing.Size(20, 300);
            // 
            // smartDockManager1
            // 
            this.smartDockManager1.AutoHideContainers.AddRange(new DevExpress.XtraBars.Docking.AutoHideContainer[] {
            this.autoHideContainer1});
            this.smartDockManager1.Form = this;
            this.smartDockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl",
            "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl"});
            // 
            // dkEquipment
            // 
            this.dkEquipment.Controls.Add(this.dockPanel1_Container);
            this.dkEquipment.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dkEquipment.ID = new System.Guid("e9a386c6-f8e5-43f9-bc5e-d7a2ea13bb94");
            this.dkEquipment.Location = new System.Drawing.Point(0, 0);
            this.dkEquipment.Name = "dkEquipment";
            this.dkEquipment.Options.AllowDockLeft = false;
            this.dkEquipment.Options.AllowDockTop = false;
            this.dkEquipment.Options.AllowFloating = false;
            this.dkEquipment.Options.ShowCloseButton = false;
            this.dkEquipment.OriginalSize = new System.Drawing.Size(200, 200);
            this.dkEquipment.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dkEquipment.SavedIndex = 0;
            this.dkEquipment.SavedSizeFactor = 1D;
            this.dkEquipment.Size = new System.Drawing.Size(200, 252);
            this.dkEquipment.Text = "설비별 Recipe";
            this.dkEquipment.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.grdEquipmentRecipe);
            this.dockPanel1_Container.Location = new System.Drawing.Point(5, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(191, 225);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // grdEquipmentRecipe
            // 
            this.grdEquipmentRecipe.Caption = "";
            this.grdEquipmentRecipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEquipmentRecipe.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdEquipmentRecipe.IsUsePaging = false;
            this.grdEquipmentRecipe.LanguageKey = null;
            this.grdEquipmentRecipe.Location = new System.Drawing.Point(0, 0);
            this.grdEquipmentRecipe.Margin = new System.Windows.Forms.Padding(0);
            this.grdEquipmentRecipe.Name = "grdEquipmentRecipe";
            this.grdEquipmentRecipe.ShowBorder = true;
            this.grdEquipmentRecipe.ShowStatusBar = false;
            this.grdEquipmentRecipe.Size = new System.Drawing.Size(191, 225);
            this.grdEquipmentRecipe.TabIndex = 2;
            this.grdEquipmentRecipe.UseAutoBestFitColumns = false;
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.tlpEquipment);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(739, 252);
            this.smartPanel1.TabIndex = 0;
            // 
            // tlpEquipment
            // 
            this.tlpEquipment.ColumnCount = 1;
            this.tlpEquipment.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpEquipment.Controls.Add(this.grdEquipment, 0, 1);
            this.tlpEquipment.Controls.Add(this.pnlEquipment, 0, 0);
            this.tlpEquipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpEquipment.Location = new System.Drawing.Point(2, 2);
            this.tlpEquipment.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpEquipment.Name = "tlpEquipment";
            this.tlpEquipment.RowCount = 2;
            this.tlpEquipment.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tlpEquipment.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpEquipment.Size = new System.Drawing.Size(735, 248);
            this.tlpEquipment.TabIndex = 1;
            // 
            // grdEquipment
            // 
            this.grdEquipment.Caption = "";
            this.grdEquipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEquipment.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdEquipment.IsUsePaging = false;
            this.grdEquipment.LanguageKey = null;
            this.grdEquipment.Location = new System.Drawing.Point(0, 1);
            this.grdEquipment.Margin = new System.Windows.Forms.Padding(0);
            this.grdEquipment.Name = "grdEquipment";
            this.grdEquipment.ShowBorder = true;
            this.grdEquipment.ShowStatusBar = false;
            this.grdEquipment.Size = new System.Drawing.Size(735, 247);
            this.grdEquipment.TabIndex = 1;
            this.grdEquipment.UseAutoBestFitColumns = false;
            // 
            // pnlEquipment
            // 
            this.pnlEquipment.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlEquipment.Controls.Add(this.txtEquipmentId);
            this.pnlEquipment.Controls.Add(this.txtEquipment);
            this.pnlEquipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEquipment.Location = new System.Drawing.Point(0, 0);
            this.pnlEquipment.Margin = new System.Windows.Forms.Padding(0);
            this.pnlEquipment.Name = "pnlEquipment";
            this.pnlEquipment.Size = new System.Drawing.Size(735, 1);
            this.pnlEquipment.TabIndex = 2;
            // 
            // txtEquipmentId
            // 
            this.txtEquipmentId.EditorWidth = "70%";
            this.txtEquipmentId.LabelText = "설비";
            this.txtEquipmentId.LabelWidth = "30%";
            this.txtEquipmentId.LanguageKey = "EQUIPMENT";
            this.txtEquipmentId.Location = new System.Drawing.Point(10, 5);
            this.txtEquipmentId.Name = "txtEquipmentId";
            this.txtEquipmentId.Size = new System.Drawing.Size(200, 20);
            this.txtEquipmentId.TabIndex = 1;
            // 
            // txtEquipment
            // 
            this.txtEquipment.LabelText = "설비";
            this.txtEquipment.LabelWidth = "20%";
            this.txtEquipment.LanguageKey = "EQUIPMENT";
            this.txtEquipment.Location = new System.Drawing.Point(320, 5);
            this.txtEquipment.Margin = new System.Windows.Forms.Padding(0);
            this.txtEquipment.Name = "txtEquipment";
            this.txtEquipment.Size = new System.Drawing.Size(300, 20);
            this.txtEquipment.TabIndex = 0;
            this.txtEquipment.Visible = false;
            // 
            // autoHideContainer1
            // 
            this.autoHideContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.autoHideContainer1.Controls.Add(this.dkEquipment);
            this.autoHideContainer1.Dock = System.Windows.Forms.DockStyle.Right;
            this.autoHideContainer1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.autoHideContainer1.Location = new System.Drawing.Point(739, 0);
            this.autoHideContainer1.Name = "autoHideContainer1";
            this.autoHideContainer1.Size = new System.Drawing.Size(20, 252);
            // 
            // ucEquipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.smartPanel1);
            this.Controls.Add(this.autoHideContainer1);
            this.Name = "ucEquipment";
            this.Size = new System.Drawing.Size(759, 252);
            ((System.ComponentModel.ISupportInitialize)(this.smartDockManager1)).EndInit();
            this.dkEquipment.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.tlpEquipment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlEquipment)).EndInit();
            this.pnlEquipment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtEquipmentId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEquipment.Properties)).EndInit();
            this.autoHideContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.Docking.AutoHideContainer hideContainerRight;
        private Framework.SmartControls.SmartDockManager smartDockManager1;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private DevExpress.XtraBars.Docking.DockPanel dkEquipment;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpEquipment;
        private Framework.SmartControls.SmartBandedGrid grdEquipment;
        private Framework.SmartControls.SmartPanel pnlEquipment;
        private Framework.SmartControls.SmartLabelTextBox txtEquipmentId;
        private Framework.SmartControls.SmartLabelSelectPopupEdit txtEquipment;
        private Framework.SmartControls.SmartBandedGrid grdEquipmentRecipe;
        private DevExpress.XtraBars.Docking.AutoHideContainer autoHideContainer1;
    }
}
