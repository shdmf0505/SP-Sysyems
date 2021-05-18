using DevExpress.Utils;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMES
{
    public partial class MainForm
    {
        #region 사이트맵

        private void DisposeSiteMap()
        {
            //이벤트 해지필요
            _accordionControls.Clear();

            foreach (SearchControl search in _searchControls)
            {
                pnlSiteMapTitle.Controls.RemoveByKey(search.Name);
            }

            _searchControls.Clear();
        }

        private void InitializeSiteMapMainTitle(DataRow[] menuFirstDepth)
        {
            layoutSiteMapTitle.Controls.Clear();

            layoutSiteMapTitle.RowStyles.Clear();
            layoutSiteMapTitle.ColumnStyles.Clear();

            layoutSiteMapTitle.RowCount = 1;
            layoutSiteMapTitle.ColumnCount = menuFirstDepth.Length;

            layoutSiteMapTitle.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            for (int i = 0; i < menuFirstDepth.Length; i++)
            {
                layoutSiteMapTitle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

                DataRow row = menuFirstDepth[i];

                PanelControl panelMenuTitle = new PanelControl();
                panelMenuTitle.Name = "panel" + row["MENUID"].ToString();
                panelMenuTitle.Dock = DockStyle.Fill;
                panelMenuTitle.Margin = new Padding(0);
                panelMenuTitle.Padding = new Padding(0, 0, 0, 10);
                panelMenuTitle.BorderStyle = BorderStyles.NoBorder;

                LabelControl labelMenuTitle = new LabelControl();
                labelMenuTitle.Name = "label" + row["MENUID"].ToString();
                labelMenuTitle.AutoSizeMode = LabelAutoSizeMode.None;
                labelMenuTitle.Dock = DockStyle.Fill;
                labelMenuTitle.Margin = new Padding(0);
                labelMenuTitle.BorderStyle = BorderStyles.NoBorder;
                labelMenuTitle.Text = row["MENUNAME"].ToString();
                labelMenuTitle.Appearance.TextOptions.Trimming = Trimming.EllipsisWord;
                labelMenuTitle.Appearance.TextOptions.WordWrap = WordWrap.NoWrap;

                labelMenuTitle.Font = new Font("Malgun Gothic", 14, FontStyle.Bold);

                panelMenuTitle.Controls.Add(labelMenuTitle);

                layoutSiteMapTitle.Controls.Add(panelMenuTitle, i, 0);
            }
        }

        private void ClearSiteMapContainer()
        {
            SiteMapContainer.Controls.Clear();

            SiteMapContainer.RowStyles.Clear();
            SiteMapContainer.ColumnStyles.Clear();

        }

        private void CreateSizeMapContainerStyle(int columnCount)
        {
            SiteMapContainer.RowCount = 1;
            SiteMapContainer.ColumnCount = columnCount;

            SiteMapContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            for (int col = 0; col < columnCount; col++) SiteMapContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        }

        private AccordionControl CreateSiteMapSubMenu(string menuId)
        {
            AccordionControl result = new AccordionControl();
            result.Name = "acc" + menuId;
            result.Dock = DockStyle.Top;
            result.Margin = new Padding(0);
            result.GroupHeight = 40;
            result.ShowGroupExpandButtons = false;
                        
            result.ScrollBarMode = ScrollBarMode.Hidden;
            result.AnimationType = AnimationType.None;
            result.Appearance.AccordionControl.BackColor = Color.Transparent;
            result.Appearance.Group.Normal.BackColor = Color.Transparent;
            result.Appearance.Group.Hovered.BackColor = Color.Transparent;
            result.Appearance.Group.Pressed.BackColor = Color.Transparent;
            result.MouseDown += SiteMapSubMenu_MouseDown;
            result.ExpandStateChanged += Result_ExpandStateChanged;
            return result;
        }

        private void Result_ExpandStateChanged(object sender, ExpandStateChangedEventArgs e)
        {
            AccordionControl control = sender as AccordionControl;

            control.Height = GetSubMenuHeight(control);
        }

        private void SiteMapSubMenu_MouseDown(object sender, MouseEventArgs e)
        {
            var hit = (sender as AccordionControl).CalcHitInfo(e.Location);
            if (hit.ItemInfo == null)
                return;
            var text = hit.ItemInfo.Text;

            // 스크롤 위치 초기화 현상 방지
            smartScrollableControl1.Focus();

            if (hit.ItemInfo.Element is AccordionControlElementEx elementEx)
            {
                this.AccordionControl_ElementClick(hit.ItemInfo.Element, new ElementClickEventArgs(hit.ItemInfo.Element));
                
                (sender as AccordionControl).SelectedElement = null;
            }
        }

        private AccordionControlElement CreateSiteMapSubMenuElement(string menuId, string menuName)
        {
            AccordionControlElement result = this.CreateSiteMapSubMenuElementCore("element" + menuId, menuName, 9, ElementStyle.Group);

            return result;
        }

        private AccordionControlElementEx CreateSiteMapSubMenuElementCore(string name, string menuName, int fontSize, ElementStyle elementStyle)
        {
            AccordionControlElementEx result = new AccordionControlElementEx();
            result.Name = name;
            result.Style = elementStyle;
            result.Text = menuName;

            result.Appearance.Normal.Font = new Font("Malgun Gothic", fontSize, FontStyle.Bold);
            result.Appearance.Normal.TextOptions.Trimming = Trimming.EllipsisWord;
            result.Appearance.Normal.TextOptions.WordWrap = WordWrap.NoWrap;

            result.Appearance.Hovered.Font = new Font("Malgun Gothic", fontSize, FontStyle.Bold);
            result.Appearance.Hovered.TextOptions.Trimming = Trimming.EllipsisWord;
            result.Appearance.Hovered.TextOptions.WordWrap = WordWrap.NoWrap;

            result.Appearance.Pressed.Font = new Font("Malgun Gothic", fontSize, FontStyle.Bold);
            result.Appearance.Pressed.TextOptions.Trimming = Trimming.EllipsisWord;
            result.Appearance.Pressed.TextOptions.WordWrap = WordWrap.NoWrap;

            if (elementStyle == ElementStyle.Item)
            {
                result.Appearance.Pressed.BackColor = Color.Transparent;
                result.Appearance.Pressed.ForeColor = Color.FromArgb(80, 80, 80);
            }
            
            SuperToolTip groupToolTip = new SuperToolTip();
            ToolTipItem groupToolTipItem = new ToolTipItem();
            if (UserInfo.Current.IsUseToolTipLanguage)
                groupToolTipItem.Text = Language.GetToolTipDictionary("Menu_" + name.Replace("element", ""), UserInfo.Current.ToolTipLanguageType).Name;
            else
                groupToolTipItem.Text = menuName;
            groupToolTip.Items.Add(groupToolTipItem);

            result.SuperTip = groupToolTip;
            result.Expanded = true;


            return result;
        }


        private AccordionControlElementEx CreateSiteMapSubMenuElement(MenuInfo menu)
        {
            AccordionControlElementEx result = this.CreateSiteMapSubMenuElementCore("element" + menu.MenuId, "  " + menu.Caption, 8, ElementStyle.Item);
            result.MenuInfo = menu;
                        

            return result;
        }

        private AccordionControlSeparatorEx CreateSiteMapSubMenuElementSeparator(MenuInfo menu)
        {
            AccordionControlSeparatorEx separator = new AccordionControlSeparatorEx() { MenuInfo = menu };
            separator.Name = "separator" + menu.MenuId;

            return separator;
        }

        private void AddToSiteMapContainer(AccordionControl subMenu, int colIndex)
        {
            SiteMapContainer.Controls.Add(subMenu, colIndex, 0);
            _accordionControls.Add(subMenu);

            //subMenu.ElementClick += AccordionControl_ElementClick;
            //subMenu.ExpandStateChanged += (s, e) =>
            //{
            //    AccordionControl accordion = s as AccordionControl;

            //    InnerHeight(s);
            //};
        }

        private int GetSubMenuHeight(AccordionControl control)
        {
            AccordionControlViewInfo info = control.GetViewInfo() as AccordionControlViewInfo;
            int result = 0;

            foreach (var einfo in info.ElementsInfo)
            {
                result += GetAccodionGroupExpandHeight(einfo);
                //result += control.GroupHeight;                
            }

            return result;
        }

        private int GetAccodionGroupExpandHeight(AccordionGroupViewInfo ginfo)
        {
            int height = ginfo.HeaderBounds.Height;

            if (ginfo.Element.Expanded & ginfo.ElementsInfo != null && ginfo.ElementsInfo.Count > 0)
            {
                List<AccordionGroupViewInfo> groups = ginfo.ElementsInfo.OfType<AccordionGroupViewInfo>().ToList();
                List<AccordionItemViewInfo> items = ginfo.ElementsInfo.OfType<AccordionItemViewInfo>().ToList();

                foreach (AccordionGroupViewInfo group in groups)
                {
                    if (group.Element.Visible)
                    height += GetAccodionGroupExpandHeight(group);
                }

                foreach (AccordionItemViewInfo item in items)
                {
                    if (item.Element.Visible)
                    height += item.HeaderBounds.Height;
                }
            }

            return height + 20;
        }

        private void InitializeSiteMap()
        {
            this.DisposeSiteMap();

            DataTable menuTable = _MenuRepository.GetMenuTable(UserInfo.Current.Uiid);
            DataRow[] menuFirstDepth = menuTable.Select("PARENTMENUID = '' AND VALIDSTATE = 'Valid'");

            if (menuFirstDepth.Length == 0) return;

            InitializeSiteMapMainTitle(menuFirstDepth);

            this.ClearSiteMapContainer();
            this.CreateSizeMapContainerStyle(menuFirstDepth.Length);

            //SiteMapContainer.Height = 3000;
            //SiteMapContainer.AutoSize = false;
            
            //SiteMapContainer.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            int subMenuIndex = 0;
            foreach (var rowFirstDepth in menuFirstDepth)
            {
                DataRow[] menuSecondDepth = menuTable.Select("PARENTMENUID = '" + rowFirstDepth["MENUID"].ToString() + "' AND VALIDSTATE = 'Valid'");
                AccordionControl subMenu = CreateSiteMapSubMenu(rowFirstDepth["MENUID"].ToString());

                subMenu.BeginUpdate();

                foreach (var rowSecondDepth in menuSecondDepth)
                {
                    DataRow[] menuThirdDepth = menuTable.Select("PARENTMENUID = '" + rowSecondDepth["MENUID"].ToString() + "' AND VALIDSTATE = 'Valid'");
                    AccordionControlElement elementSecondDepth = CreateSiteMapSubMenuElement(rowSecondDepth["MENUID"].ToString(), rowSecondDepth["MENUNAME"].ToString());

                    subMenu.Elements.Add(elementSecondDepth);

                    foreach (var rowThirdDepth in menuThirdDepth)
                    {
                        MenuInfo menu = _MenuRepository.ToMenuInfo(rowThirdDepth["MENUID"].ToString());

                        if (menu.MenuType == MenuType.Screen)
                        {
                            AccordionControlElementEx elementThirdDepth = CreateSiteMapSubMenuElement(menu);
                            elementSecondDepth.Elements.Add(elementThirdDepth);
                        }
                        else if (menu.MenuType == MenuType.Seperator)
                        {
                            AccordionControlSeparatorEx separator = this.CreateSiteMapSubMenuElementSeparator(menu);
                            elementSecondDepth.Elements.Add(separator);
                        }
                    }
                }

                subMenu.EndUpdate();

                AddToSiteMapContainer(subMenu, subMenuIndex);
                SearchControl search = CreateSearchControlForSiteMapSubMenu(rowFirstDepth["MENUID"].ToString(), subMenu);

                pnlSiteMapTitle.Controls.Add(search);
                _searchControls.Add(search);

                search.SendToBack();

                subMenuIndex++;
            }

            int maxHeight = 0;
            _accordionControls.ForEach(a =>
            {
                //a.ExpandAll();  
                //a.Refresh();

                a.Height = GetSubMenuHeight(a);
                maxHeight = Math.Max(maxHeight, a.Height);
                
            });

            SiteMapContainer.Height = maxHeight;
        }

        private SearchControl CreateSearchControlForSiteMapSubMenu(string menuId, AccordionControl subMenu)
        {
            SearchControl search = new SearchControl();
            search.Name = "search" + menuId;
            search.Location = new Point(30, 30);
            search.Client = subMenu;
            search.Properties.Client = subMenu;
            search.Properties.FindDelay = 100;
            search.Properties.AllowAutoApply = true;
            search.Properties.Buttons.AddRange(new EditorButton[]
            {
                    new ClearButton(),
                    new SearchButton()
            });

            return search;
        }

        #endregion
    }
}
