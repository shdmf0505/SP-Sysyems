using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Ribbon;

namespace SmartMES
{
    [ToolboxItem(true)]
    public partial class MenuNavigatorContainer : XtraUserControl
    {
        public MenuNavigatorContainer()
        {
            InitializeComponent();
        }

        public event EventHandler<MenuClickEventArgs> MenuClick;

        protected override void OnFirstLoad()
        {
            base.OnFirstLoad();
            SelectFirstTab();
        }

        public void AddMenu(string categoryName, IEnumerable<MenuInfo> menus)
        {
            var container = this.AddTab(categoryName);
            this.CreateMenu(container, menus.ToArray());
        }

        private void CreateMenu(ContainerControl container, MenuInfo[] menus)
        {
            var navigator = new MenuNavigator();
            navigator.Dock = DockStyle.Fill;
            navigator.SetMenu(menus);
            navigator.MenuClick += (o, e) => MenuClick?.Invoke(this, e);
            container.Controls.Add(navigator);
        }

        private ContainerControl AddTab(string tabName)
        {
            var tab = new BackstageViewTabItem();
            tab.Caption = tabName;
            tab.ContentControl = new BackstageViewClientControl();
            this.smartBackstageViewControl1.Items.Add(tab);
            return tab.ContentControl;
        }

        private void SelectFirstTab()
        {
            var tab = this.smartBackstageViewControl1
                .Items.FirstOrDefault() as BackstageViewTabItem;
            if (tab != null)
                tab.Selected = true;
        }
    }
}
