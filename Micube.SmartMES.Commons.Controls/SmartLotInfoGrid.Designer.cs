namespace Micube.SmartMES.Commons.Controls
{
    partial class SmartLotInfoGrid
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
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.gvwMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwMain)).BeginInit();
            this.SuspendLayout();
            // 
            // grdMain
            // 
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.Location = new System.Drawing.Point(0, 0);
            this.grdMain.MainView = this.gvwMain;
            this.grdMain.Margin = new System.Windows.Forms.Padding(0);
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(300, 200);
            this.grdMain.TabIndex = 0;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwMain});
            // 
            // gvwMain
            // 
            this.gvwMain.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gvwMain.GridControl = this.grdMain;
            this.gvwMain.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            this.gvwMain.Name = "gvwMain";
            this.gvwMain.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvwMain.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvwMain.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.False;
            this.gvwMain.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.False;
            this.gvwMain.OptionsBehavior.AllowPartialGroups = DevExpress.Utils.DefaultBoolean.False;
            this.gvwMain.OptionsBehavior.AllowPartialRedrawOnScrolling = false;
            this.gvwMain.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.False;
            this.gvwMain.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.False;
            this.gvwMain.OptionsBehavior.AllowValidationErrors = false;
            this.gvwMain.OptionsBehavior.ReadOnly = true;
            this.gvwMain.OptionsCustomization.AllowColumnMoving = false;
            this.gvwMain.OptionsCustomization.AllowColumnResizing = false;
            this.gvwMain.OptionsCustomization.AllowFilter = false;
            this.gvwMain.OptionsCustomization.AllowGroup = false;
            this.gvwMain.OptionsCustomization.AllowSort = false;
            this.gvwMain.OptionsMenu.EnableColumnMenu = false;
            this.gvwMain.OptionsMenu.EnableFooterMenu = false;
            this.gvwMain.OptionsMenu.EnableGroupPanelMenu = false;
            this.gvwMain.OptionsNavigation.AutoMoveRowFocus = false;
            this.gvwMain.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvwMain.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvwMain.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gvwMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvwMain.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.gvwMain.OptionsView.ShowColumnHeaders = false;
            this.gvwMain.OptionsView.ShowGroupPanel = false;
            this.gvwMain.OptionsView.ShowIndicator = false;
            this.gvwMain.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None;
            this.gvwMain.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            // 
            // SmartLotInfoGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdMain);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SmartLotInfoGrid";
            this.Size = new System.Drawing.Size(300, 200);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwMain;
    }
}
