using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Micube.Framework.SmartControls;
using DevExpress.XtraBars.Navigation;

namespace SmartMES
{
    [ToolboxItem(false)]
    public partial class MenuNavigator : XtraUserControl
    {
        private SmartAccordionControl[] _accordionControls;

        private Dictionary<string, IHasMenuInfo> _elements;

        public MenuNavigator()
        {
            InitializeComponent();

            this._accordionControls = new SmartAccordionControl[]
            {
                acc1, acc2, acc3, acc4
            };

            foreach (var item in this._accordionControls)
                item.ElementClick += (o, e) => OnMenuClick(e.Element);

            this.smartSearchLookUpEdit1.EditValueChanged += (o, e) =>
            {
                if(this.smartSearchLookUpEdit1.EditValue != null)
                {
                    var info = this.smartSearchLookUpEdit1.EditValue as MenuInfo;
                    OnMenuClick(info);
                }
            };

            this.accRecentMenu.ElementClick += (o, e) => OnMenuClick(e.Element);
        }

        public event EventHandler<MenuClickEventArgs> MenuClick;

        public void SetMenu(MenuInfo[] menus)
        {
            this.smartSearchLookUpEdit1View.Columns.AddVisible(nameof(MenuInfo.MenuId));
            this.smartSearchLookUpEdit1View.Columns.AddVisible(nameof(MenuInfo.Caption));
            this.smartSearchLookUpEdit1.Properties.DataSource = menus
                .Where(m => m.MenuType == MenuType.Screen)
                .ToList();

            IEnumerable<SmartAccordionControl> GetControls()
            {
                var e = this._accordionControls
                    .AsEnumerable()
                    .GetEnumerator();

                while(true)
                {
                    if (e.MoveNext())
                        yield return e.Current;
                    else
                        e.Reset();
                }
            }

            this._elements = menus.ToDictionary(
                menu => menu.MenuId,
                menu => CreateElement(menu)
            );

            var controls = GetControls().GetEnumerator();
            controls.MoveNext();

            foreach (var menu in this._elements.Values)
            {
                AccordionControlElement element = menu as AccordionControlElement;

                if (string.IsNullOrWhiteSpace(menu.MenuInfo.ParentMenuId))
                {
                    
                    if (element != null)
                    {
                        element.Expanded = true;
                        controls.Current.Elements.Add(element);
                        controls.MoveNext();
                    }
                    
                }
                else if (this._elements.TryGetValue(menu.MenuInfo.ParentMenuId, out var parentElement))
                {
                    (parentElement as AccordionControlElement).Elements.Add(element);
                }
            }
        }

        private IHasMenuInfo CreateElement(MenuInfo menu)
        {
            if (menu.MenuType == MenuType.Seperator)
            {
                return new AccordionControlSeparatorEx()
                {
                    MenuInfo = menu
                };
            }
            
            return new AccordionControlElementEx()
            {
                MenuInfo = menu,
                Text = menu.Caption,
                Style = menu.MenuType == MenuType.Screen ? ElementStyle.Item : ElementStyle.Group
            };
        }

        private void OnMenuClick(AccordionControlElement element)
        {
            if (element is AccordionControlElementEx ele &&
                ele.MenuInfo.MenuType == MenuType.Screen)
            {
                OnMenuClick(ele.MenuInfo);
            }
        }

        private void OnMenuClick(MenuInfo info)
        {
            string path = "";

            if(this._elements.TryGetValue(info.MenuId, out var element))
            {
                AccordionControlElement current = element as AccordionControlElement;
                while (current != null)
                {
                    path = current.Text + " > " + path;
                    current = current.OwnerElement;
                }
                path = path.Substring(0, path.Length - 2);
            }

            MenuClick?.Invoke(this, new MenuClickEventArgs(info, path));
            this.smartSearchLookUpEdit1.EditValue = null;
            AddRecentMenu(info);
        }

        private void AddRecentMenu(MenuInfo menu)
        {
            var elements = this.accRecentMenu.Elements[0].Elements;

            var first = elements.FirstOrDefault();
            if (first is AccordionControlElementEx element)
            {
                if (element.MenuInfo.MenuId == menu.MenuId)
                    return;
            }

            elements.Insert(0, CreateElement(menu) as AccordionControlElement);
        }

        class AccordionControlElementEx : AccordionControlElement, IHasMenuInfo
        {
            
            public MenuInfo MenuInfo { get; set; }
        }

        class AccordionControlSeparatorEx : AccordionControlSeparator, IHasMenuInfo
        {

            public MenuInfo MenuInfo { get; set; }
        }

        public interface IHasMenuInfo
        {
            MenuInfo MenuInfo { get;  }
        }
    }
}
