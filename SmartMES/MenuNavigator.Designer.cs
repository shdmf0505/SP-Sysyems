namespace SmartMES
{
    partial class MenuNavigator
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
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.smartSearchLookUpEdit1 = new Micube.Framework.SmartControls.SmartSearchLookupEdit();
            this.smartSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.acc4 = new Micube.Framework.SmartControls.SmartAccordionControl();
            this.acc3 = new Micube.Framework.SmartControls.SmartAccordionControl();
            this.acc2 = new Micube.Framework.SmartControls.SmartAccordionControl();
            this.accFavorite = new Micube.Framework.SmartControls.SmartAccordionControl();
            this.favoriteMenuGroup = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlSeparator1 = new DevExpress.XtraBars.Navigation.AccordionControlSeparator();
            this.accRecentMenu = new Micube.Framework.SmartControls.SmartAccordionControl();
            this.accordionControlElement2 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acc1 = new Micube.Framework.SmartControls.SmartAccordionControl();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSearchLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSearchLookUpEdit1View)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.acc4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.acc3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.acc2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accFavorite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accRecentMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.acc1)).BeginInit();
            this.SuspendLayout();
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.smartSearchLookUpEdit1);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Padding = new System.Windows.Forms.Padding(14);
            this.smartPanel1.Size = new System.Drawing.Size(985, 65);
            this.smartPanel1.TabIndex = 0;
            // 
            // smartSearchLookUpEdit1
            // 
            this.smartSearchLookUpEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSearchLookUpEdit1.EditValue = "";
            this.smartSearchLookUpEdit1.Location = new System.Drawing.Point(16, 16);
            this.smartSearchLookUpEdit1.Name = "smartSearchLookUpEdit1";
            this.smartSearchLookUpEdit1.Properties.AutoHeight = false;
            this.smartSearchLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.smartSearchLookUpEdit1.Properties.NullText = "...";
            this.smartSearchLookUpEdit1.Properties.PopupFormSize = new System.Drawing.Size(640, 480);
            this.smartSearchLookUpEdit1.Properties.PopupView = this.smartSearchLookUpEdit1View;
            this.smartSearchLookUpEdit1.Size = new System.Drawing.Size(953, 33);
            this.smartSearchLookUpEdit1.TabIndex = 1;
            // 
            // smartSearchLookUpEdit1View
            // 
            this.smartSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.smartSearchLookUpEdit1View.Name = "smartSearchLookUpEdit1View";
            this.smartSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.smartSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.acc4, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.acc3, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.acc2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.accFavorite, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.accRecentMenu, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.acc1, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 65);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(985, 279);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // acc4
            // 
            this.acc4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acc4.Location = new System.Drawing.Point(823, 3);
            this.acc4.Name = "acc4";
            this.acc4.Size = new System.Drawing.Size(159, 273);
            this.acc4.TabIndex = 16;
            this.acc4.Text = "smartAccordionControl5";
            // 
            // acc3
            // 
            this.acc3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acc3.Location = new System.Drawing.Point(659, 3);
            this.acc3.Name = "acc3";
            this.acc3.Size = new System.Drawing.Size(158, 273);
            this.acc3.TabIndex = 15;
            this.acc3.Text = "smartAccordionControl4";
            // 
            // acc2
            // 
            this.acc2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acc2.Location = new System.Drawing.Point(495, 3);
            this.acc2.Name = "acc2";
            this.acc2.Size = new System.Drawing.Size(158, 273);
            this.acc2.TabIndex = 14;
            this.acc2.Text = "smartAccordionControl3";
            // 
            // accFavorite
            // 
            this.accFavorite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accFavorite.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.favoriteMenuGroup});
            this.accFavorite.Location = new System.Drawing.Point(3, 2);
            this.accFavorite.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.accFavorite.Name = "accFavorite";
            this.accFavorite.Size = new System.Drawing.Size(158, 275);
            this.accFavorite.TabIndex = 12;
            this.accFavorite.Text = "smartAccordionControl1";
            // 
            // favoriteMenuGroup
            // 
            this.favoriteMenuGroup.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlSeparator1});
            this.favoriteMenuGroup.ImageOptions.Image = global::SmartMES.Properties.Resources.star;
            this.favoriteMenuGroup.Name = "favoriteMenuGroup";
            this.favoriteMenuGroup.Text = "Favorite Menu";
            // 
            // accordionControlSeparator1
            // 
            this.accordionControlSeparator1.Name = "accordionControlSeparator1";
            // 
            // accRecentMenu
            // 
            this.accRecentMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accRecentMenu.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement2});
            this.accRecentMenu.Location = new System.Drawing.Point(167, 3);
            this.accRecentMenu.Name = "accRecentMenu";
            this.accRecentMenu.Size = new System.Drawing.Size(158, 273);
            this.accRecentMenu.TabIndex = 1;
            this.accRecentMenu.Text = "smartAccordionControl2";
            // 
            // accordionControlElement2
            // 
            this.accordionControlElement2.Expanded = true;
            this.accordionControlElement2.Name = "accordionControlElement2";
            this.accordionControlElement2.Text = "Recent Menu";
            // 
            // acc1
            // 
            this.acc1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acc1.Location = new System.Drawing.Point(331, 3);
            this.acc1.Name = "acc1";
            this.acc1.Size = new System.Drawing.Size(158, 273);
            this.acc1.TabIndex = 13;
            this.acc1.Text = "smartAccordionControl1";
            // 
            // MenuNavigator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.smartPanel1);
            this.Name = "MenuNavigator";
            this.Size = new System.Drawing.Size(985, 344);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSearchLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSearchLookUpEdit1View)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.acc4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.acc3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.acc2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accFavorite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accRecentMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.acc1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Micube.Framework.SmartControls.SmartPanel smartPanel1;
        private Micube.Framework.SmartControls.SmartSearchLookupEdit smartSearchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView smartSearchLookUpEdit1View;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Micube.Framework.SmartControls.SmartAccordionControl accRecentMenu;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement2;
        private Micube.Framework.SmartControls.SmartAccordionControl accFavorite;
        private DevExpress.XtraBars.Navigation.AccordionControlElement favoriteMenuGroup;
        private Micube.Framework.SmartControls.SmartAccordionControl acc1;
        private Micube.Framework.SmartControls.SmartAccordionControl acc4;
        private Micube.Framework.SmartControls.SmartAccordionControl acc3;
        private Micube.Framework.SmartControls.SmartAccordionControl acc2;
        private DevExpress.XtraBars.Navigation.AccordionControlSeparator accordionControlSeparator1;
    }
}
