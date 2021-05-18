using DevExpress.Images;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.Utils.Design;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Events;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using SmartMES.Repositories;
using SmartMES.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Controls;
using System.Windows.Forms;

namespace SmartMES
{
    public partial class MainForm : XtraForm
    {
        //private DataTable _menuTable;
        private IMenuRepository _MenuRepository;
        private RecentMenuSettingRepository _RecentMenuSettingRepository = new RecentMenuSettingRepository();
        private Dictionary<string, IMenuRepository> _MenuList = new Dictionary<string, IMenuRepository>();
        private Dictionary<string, int> _ServiceMenuCount = new Dictionary<string, int>();

        public int _ServiceCount;
        public Dictionary<string, IConvertible> _ServiceList;

        public MainForm(Micube.Framework.SmartControls.Interface.IOpenMenu menuRepository)
        {
            InitializeComponent();

            this.Width = 1000;
            this.Height = 800;

            this.CenterToScreen();
            // 2019.09.30 hykang - 프로그램 명
            //this.Text = "SmartMES";
            this.Text = Process.GetCurrentProcess().ProcessName;

            _MenuRepository = menuRepository as IMenuRepository;
            _RecentMenuSettingRepository.Get(UserInfo.Current.Id);


            //Micube.Framework.SmartControls.Services.Jots.StateTrackerService.ApplyConfig(this);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                if (this.DesignMode) return base.CreateParams;

                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        #region 로드할 폼 선택

        private bool Login()
        {
            LoginForm login = new LoginForm();
            login.Owner = this;
            login.ShowDialog();

            return UserInfo.Current.IsAuthenticated;
        }

        private void ShowError(Exception ex)
        {
            Micube.Framework.SmartControls.Helpers.UIHelper.ShowError(ex);
        }

        private void LoadFirstForm()
        {
#if DEBUG
            try
            {
                if (this.CallMenu != null)
                {
                    string defaultLogin = ApplicationConfig.Current.GetDefaultLogin();
                    string[] debugLogin = null;
                    if (!string.IsNullOrWhiteSpace(defaultLogin))
                    {
                        debugLogin = defaultLogin.Split(new char[] { '/' });
                    }
                    else
                    {
                        debugLogin = new string[] { "admin", "admin" };
                    }

                    NetworkSettings.SetServiceUrl(UserInfo.Current.DefaultService);

                    (new LoginForm()).LoginCore(debugLogin[0], debugLogin[1]);
                    //InitializeMenu();
                    _MenuRepository.OpenMenu(this.CallMenu, null);
                }
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
#endif
        }

        #endregion

        /// <summary>
        /// 디버그용 띄울 창
        /// </summary>
        public MenuInfo CallMenu { get; set; }

        //private bool isFirstOpen = true;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 2019.09.30 hykang - 메뉴 리스트 초기화 로직 변경
            //_ServiceList.ForEach(value =>
            //{
            //    NetworkSettings.SetServiceUrl(value.Key);

            //    _MenuRepository = new MenuRepository();

            //    _MenuRepository.InitMenu(this);

            //    _ServiceMenuCount.Add(value.Key, _MenuRepository.GetMenuTable().Rows.Count);

            //    _MenuList.Add(value.Key, _MenuRepository);
            //});

            NetworkSettings.SetServiceUrl(UserInfo.Current.Uiid);
            _MenuRepository.InitMenu(this, _ServiceList);


            LoadFirstForm();


            InitializeServiceBar();


            InitializeEvent();
            InitializeMenuBar();

            InitializeFavorite();
            InitializeRecent();

            //InitializeSearch();

            InitializeLanguageLabel();

            SetUserInformationLabel();


            Skin skin = DockingSkins.GetSkin(UserLookAndFeel.Default);
            skin.Properties["DocumentGroupRootMargin"] = 0;
            LookAndFeelHelper.ForceDefaultLookAndFeelChanged();

            //layoutSiteMapSubMenu.Width = panelSiteMapMenu.Width;

            //SetAccordianControlWidth();
            pnlMenu.Visible = false;

            if (barService.Groups.Count > 0)
                barService.SelectedItem = barService.Groups[0].Items[0];
        }

        private void SetUserInformationLabel()
        {
            btnUserInfo.Text = UserInfo.Current.Name;

            lblLoginUserId.Text = UserInfo.Current.Id;
            lblLoginUserName.Text = UserInfo.Current.Name;
            lblLoginEmailAddress.Text = UserInfo.Current.EmailAddress;
            lblLoginCellPhoneNumber.Text = UserInfo.Current.CellPhoneNumber;
            lblLoginLoginTime.Text = UserInfo.Current.LoginTime.ToString("yyyy-MM-dd tt h:mm:ss");
        }

        private void InitializeSearch()
        {
            //Text = "";
            //ControlBox = false;
            //FormBorderStyle = FormBorderStyle.SizableToolWindow;

            TextEdit txtSearch = new TextEdit();
            txtSearch.Location = new Point(Width - 200, -10);
            txtSearch.Show();

            Controls.Add(txtSearch);

            txtSearch.BringToFront();
        }


        #region 프로그램 상세 메뉴 초기화

        //private Dictionary<string, IHasMenuInfo> _elements;
        private List<AccordionControl> _accordionControls = new List<AccordionControl>();
        private List<SearchControl> _searchControls = new List<SearchControl>();



        private void InitializeFavorite()
        {
            UserInfo.Current.ServiceId = UserInfo.Current.Uiid;

            NetworkSettings.SetServiceUrl(UserInfo.Current.Uiid);

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dicParam.Add("USERID", UserInfo.Current.Id);

            DataTable FavoriteMenuList = SqlExecuter.Query("GetFavoriteMenuList", "00001", dicParam);

            groupFavoriteMenu.Elements.Clear();

            if (FavoriteMenuList.Rows.Count < 1)
                return;

            foreach (DataRow row in FavoriteMenuList.Rows)
            {
                MenuInfo menuInfo = _MenuRepository.ToMenuInfo(row["MENUID"].ToString());

                if (menuInfo == null)
                    continue;

                DataRow[] rowMenu = _MenuRepository.GetMenuTable(UserInfo.Current.Uiid).Select("MENUID = '" + menuInfo.MenuId + "'");

                if (rowMenu.Length > 0)
                {
                    DataRow menu = rowMenu.First();

                    AccordionControlElementEx element = new AccordionControlElementEx() { MenuInfo = menuInfo };
                    element.Name = "elementFavorite" + menu["MENUID"].ToString();

                    element.Style = ElementStyle.Item;
                    element.Text = menu["MENUNAME"].ToString();

                    element.Appearance.Normal.Font = new Font("Malgun Gothic", 10, FontStyle.Bold);
                    element.Appearance.Normal.TextOptions.Trimming = Trimming.EllipsisWord;
                    element.Appearance.Normal.TextOptions.WordWrap = WordWrap.NoWrap;

                    element.Appearance.Hovered.Font = new Font("Malgun Gothic", 10, FontStyle.Bold);
                    element.Appearance.Hovered.TextOptions.Trimming = Trimming.EllipsisWord;
                    element.Appearance.Hovered.TextOptions.WordWrap = WordWrap.NoWrap;

                    element.Appearance.Pressed.Font = new Font("Malgun Gothic", 10, FontStyle.Bold);
                    element.Appearance.Pressed.TextOptions.Trimming = Trimming.EllipsisWord;
                    element.Appearance.Pressed.TextOptions.WordWrap = WordWrap.NoWrap;

                    SuperToolTip toolTip = new SuperToolTip();
                    ToolTipItem toolTipItem = new ToolTipItem();
                    if (UserInfo.Current.IsUseToolTipLanguage)
                        toolTipItem.Text = Language.GetToolTipDictionary("Menu_" + menu["MENUID"].ToString(), UserInfo.Current.ToolTipLanguageType).Name;
                    else
                        toolTipItem.Text = menu["MENUNAME"].ToString();
                    toolTip.Items.Add(toolTipItem);

                    element.SuperTip = toolTip;


                    groupFavoriteMenu.Elements.Add(element);
                }
            }

            UserInfo.Current.ServiceId = UserInfo.Current.DefaultService;
        }

        private void InitializeRecent()
        {
            groupRecentMenu.Elements.Clear();

            if (SettingConfig.Current.RecentMenu.Count < 1)
                return;

            List<MenuInfo> listRecentMenu = SettingConfig.Current.RecentMenu;

            if (listRecentMenu.Count > 0)
            {
                foreach (MenuInfo menuInfo in listRecentMenu)
                {
                    DataRow[] rowMenu = _MenuRepository.GetMenuTable(UserInfo.Current.Uiid).Select("MENUID = '" + menuInfo.MenuId + "'");

                    if (rowMenu.Length > 0)
                    {
                        DataRow row = rowMenu.First();

                        AccordionControlElementEx element = new AccordionControlElementEx() { MenuInfo = menuInfo };
                        element.Name = "elementRecent" + row["MENUID"].ToString();

                        element.Style = ElementStyle.Item;
                        element.Text = row["MENUNAME"].ToString();

                        element.Appearance.Normal.Font = new Font("Malgun Gothic", 10, FontStyle.Bold);
                        element.Appearance.Normal.TextOptions.Trimming = Trimming.EllipsisWord;
                        element.Appearance.Normal.TextOptions.WordWrap = WordWrap.NoWrap;

                        element.Appearance.Hovered.Font = new Font("Malgun Gothic", 10, FontStyle.Bold);
                        element.Appearance.Hovered.TextOptions.Trimming = Trimming.EllipsisWord;
                        element.Appearance.Hovered.TextOptions.WordWrap = WordWrap.NoWrap;

                        element.Appearance.Pressed.Font = new Font("Malgun Gothic", 10, FontStyle.Bold);
                        element.Appearance.Pressed.TextOptions.Trimming = Trimming.EllipsisWord;
                        element.Appearance.Pressed.TextOptions.WordWrap = WordWrap.NoWrap;

                        SuperToolTip toolTip = new SuperToolTip();
                        ToolTipItem toolTipItem = new ToolTipItem();
                        if (UserInfo.Current.IsUseToolTipLanguage)
                            toolTipItem.Text = Language.GetToolTipDictionary("Menu_" + row["MENUID"].ToString(), UserInfo.Current.ToolTipLanguageType).Name;
                        else
                            toolTipItem.Text = row["MENUNAME"].ToString();
                        toolTip.Items.Add(toolTipItem);

                        element.SuperTip = toolTip;


                        groupRecentMenu.Elements.Add(element);
                    }
                }
            }
        }

        private void InitializeOptions()
        {
            NetworkSettings.SetServiceUrl(UserInfo.Current.DefaultService);

            DataTable languageType = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>()
            {
                { "CODECLASSID", "LanguageType" },
                { "LanguageType", UserInfo.Current.LanguageType }
            });

            DXPopupMenu menuLanguageType = new DXPopupMenu();

            foreach (DataRow row in languageType.Rows)
            {
                DXMenuItem item = new DXMenuItem(row["CODENAME"].ToString());
                item.Tag = row["CODEID"].ToString();

                item.Click += (o, e) =>
                {
                    DXMenuItem selectItem = o as DXMenuItem;

                    ChangeLanguage(selectItem.Tag.ToString());
                };

                menuLanguageType.Items.Add(item);
            }

            btnSelectLanguageType.DropDownControl = menuLanguageType;


            //DataTable plantList = SqlExecuter.Query("GetPlantList", "10001", new Dictionary<string, object>()
            //{
            //    { "LanguageType", UserInfo.Current.LanguageType }
            //});

            DataTable plantList = SqlExecuter.Query("GetUserAuthorityPlantList", "00001", new Dictionary<string, object>()
            {
                { "P_ENTERPRISEID", UserInfo.Current.Enterprise },
                { "P_USERID", UserInfo.Current.Id }
            });

            DXPopupMenu menuPlantList = new DXPopupMenu();

            foreach (DataRow row in plantList.Rows)
            {
                DXMenuItem item = new DXMenuItem(row["PLANTNAME"].ToString());
                item.Tag = row["PLANTID"].ToString() + "," + row["PLANTNAME"].ToString() + "," + row["STARTBUSINESSHOUR"].ToString();

                item.Click += (o, e) =>
                {
                    DXMenuItem selectItem = o as DXMenuItem;

                    string plantId = "";
                    string plantName = "";
                    string startBusinessHour = "";

                    string[] strTag = selectItem.Tag.ToString().Split(',');

                    if (strTag.Length == 3)
                    {
                        plantId = strTag[0];
                        plantName = strTag[1];
                        startBusinessHour = strTag[2];
                    }

                    ChangePlant(plantId, plantName, startBusinessHour);
                };

                menuPlantList.Items.Add(item);
            }

            btnSelectPlant.DropDownControl = menuPlantList;


            //smjang - 프린터 목록 추가
            DXPopupMenu menuPrinterList = new DXPopupMenu();
            PrinterSettings printerSetting = new PrinterSettings();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {

                DXMenuItem item = new DXMenuItem(printer.ToString());
                item.Tag = printer.ToString();

                item.Click += (o, e) =>
                {
                    DXMenuItem selectItem = o as DXMenuItem;

                    // 기본 프린터로 변경
                    this.lblSelectPrinterDescriptionTitle.Text = this.lblSelectPrinterDescriptionTitle.Text.Replace(UserInfo.Current.Printer, selectItem.Tag.ToString());
                    UserInfo.Current.Printer = selectItem.Tag.ToString();
                    UserInfo.Current.PrinterType = "NAME";

                    System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["DefaultPrinter"].Value = UserInfo.Current.Printer;
                    config.AppSettings.Settings["PrinterType"].Value = UserInfo.Current.PrinterType;
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                };

                menuPrinterList.Items.Add(item);

                // 기본 프린터 찾기
                if (string.IsNullOrEmpty(UserInfo.Current.Printer))
                {
                    printerSetting.PrinterName = printer;
                    if (printerSetting.IsDefaultPrinter)
                    {
                        UserInfo.Current.Printer = printer;

                        // Language  변경이 먼저 진행되기 때문에 다시 설정 해준다.
                        Language.LanguageMessageItem msgChangePrinter = Language.GetMessage("CHANGEPRINTER");
                        lblSelectPrinterDescriptionTitle.Text = msgChangePrinter.Title ?? msgChangePrinter.Message;
                        lblSelectPrinterDescriptionTitle.Text = lblSelectPrinterDescriptionTitle.Text + " - " + UserInfo.Current.Printer;
                    }
                }
            }
            
            // ip/Port 설정
            DXMenuItem menuitem = new DXMenuItem("IP / PORT");
            menuitem.Tag = "IP / PORT";

            menuitem.Click += (o, e) =>
            {
                DXMenuItem selectItem = o as DXMenuItem;

                // 기본 프린터로 변경
                PrinterIPPortSetting printer = new PrinterIPPortSetting();
                if (printer.ShowDialog() == DialogResult.OK)
                {
                    // Language  변경이 먼저 진행되기 때문에 다시 설정 해준다.
                    Language.LanguageMessageItem msgChangePrinter = Language.GetMessage("CHANGEPRINTER");
                    lblSelectPrinterDescriptionTitle.Text = msgChangePrinter.Title ?? msgChangePrinter.Message;
                    lblSelectPrinterDescriptionTitle.Text = lblSelectPrinterDescriptionTitle.Text + " - " + UserInfo.Current.Printer;
                }
            };
            menuPrinterList.Items.Add(menuitem);
                       
            btnSelectPrinter.DropDownControl = menuPrinterList;
        }

        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(string name);


        private void InitializeInformation()
        {

        }

        #endregion

        #region ServiceBar 서비스 목록 추가

        private void InitializeServiceBar()
        {
            barService.Groups.Clear();

            int nServiceAuthority = 0;

            _MenuRepository.GetServiceCount().ForEach(value =>
            {
                if (value.Value > 0)
                {
                    TileBarGroup group = new TileBarGroup();
                    group.Name = "group" + value.Key;

                    TileBarItem item = new TileBarItem();
                    item.Name = "item" + value.Key;
                    item.AllowAnimation = false;
                    item.BorderVisibility = TileItemBorderVisibility.Never;
                    item.ItemSize = TileBarItemSize.Wide;
                    item.ShowDropDownButton = DefaultBoolean.False;
                    item.ShowItemShadow = DefaultBoolean.False;
                    item.TextAlignment = TileItemContentAlignment.MiddleCenter;
                    item.Text = value.Key;

                    SuperToolTip toolTip = new SuperToolTip();
                    ToolTipItem toolTipItem = new ToolTipItem();
                    toolTipItem.Text = value.Key;
                    toolTip.Items.Add(toolTipItem);

                    item.SuperTip = toolTip;

                    item.AppearanceItem.Normal.BackColor = Color.FromArgb(71, 71, 71);
                    item.AppearanceItem.Normal.Font = new Font("Malgun Gothic", 11, FontStyle.Bold);

                    item.AppearanceItem.Hovered.BackColor = Color.FromArgb(117, 117, 117);

                    item.AppearanceItem.Selected.BackColor = Color.FromArgb(117, 117, 117);


                    item.ItemClick += (o, s) =>
                    {
                        if (item.Text != UserInfo.Current.Uiid)
                        {
                            HideMenu();

                            SetRecentMenu();

                            UserInfo.Current.Uiid = item.Text;
                            //NetworkSettings.SetServiceUrl(item.Text);

                            //_MenuRepository = _MenuList[item.Text];

                            _IsInitStartMenu = false;

                            ChangeService();
                        }
                    };

                    group.Items.Add(item);

                    barService.Groups.Add(group);

                    nServiceAuthority++;
                }
            });


            if (nServiceAuthority < 1)
            {
                string uiid = AppConfiguration.GetString("Application.Service.Default");

                UserInfo.Current.Uiid = uiid;
                NetworkSettings.SetServiceUrl(uiid);
                //_MenuRepository = _MenuList[uiid];

                MSGBox.Show(MessageBoxType.Information, "사용 권한이 있는 메뉴가 없습니다. 관리자에게 문의하시기 바랍니다.");

                pnlService.Visible = false;

                return;
            }
            else
            {
                string uiid = barService.Groups[0].Items[0].Text;

                UserInfo.Current.Uiid = uiid;
                NetworkSettings.SetServiceUrl(uiid);
                //_MenuRepository = _MenuList[uiid];


                if (nServiceAuthority == 1)
                    pnlService.Visible = false;
            }
        }

        private void ChangeService()
        {
            try
            {
                DialogManager.ShowWaitDialog();

                UserInfo.Current.ServiceId = UserInfo.Current.Uiid;

                _RecentMenuSettingRepository.Get(UserInfo.Current.Id);

                //InitStartMenu();
                InitializeMenuBar();
                //InitializeSiteMap();
                //InitializeFavorite();
                InitializeRecent();
                //InitializeOptions();

                //InitializeInformation();

                ChangeMenuItemSize();
            }
            catch (Exception ex)
            {
                MSGBox.Show(ex);
            }
            finally
            {
                DialogManager.Close();

                UserInfo.Current.ServiceId = UserInfo.Current.DefaultService;
            }
        }

        #endregion

        #region MenuBar 메뉴 목록 추가

        private void InitializeMenuBar()
        {
            for (int i = barMenu.Groups.Count - 1; i >= 0; i--)
            {
                if (barMenu.Groups[i].Name.StartsWith("group"))
                    barMenu.Groups.RemoveAt(i);
            }

            DataRow[] menuRow = _MenuRepository.GetMenuTable(UserInfo.Current.Uiid).Select("PARENTMENUID = '' AND MENUTYPE = 'Folder' AND VALIDSTATE = 'Valid'");

            //foreach (DataRow row in menuRow)
            //{
            //    TileGroup group = barMenu.GetTileGroupByName("group" + row["MENUID"].ToString());

            //    if (group != null)
            //        barMenu.Groups.Remove(group);
            //}

            for (int i = 0; i < menuRow.Length; i++)
            {
                DataRow row = menuRow[i];

                AddItems(row["MENUID"].ToString(), row["MENUNAME"].ToString());
            }
        }

        private void AddItems(string menuId, string menuName)
        {
            TileBarGroup group = new TileBarGroup();
            group.Name = "group" + menuId;

            TileBarItem item = new TileBarItem();
            item.Name = "item" + menuId;
            item.AllowAnimation = false;
            item.BorderVisibility = TileItemBorderVisibility.Never;
            item.ItemSize = TileBarItemSize.Wide;
            item.ShowDropDownButton = DefaultBoolean.False;
            item.ShowItemShadow = DefaultBoolean.False;
            item.TextAlignment = TileItemContentAlignment.MiddleCenter;
            item.Text = menuName;

            SuperToolTip toolTip = new SuperToolTip();
            ToolTipItem toolTipItem = new ToolTipItem();
            if (UserInfo.Current.IsUseToolTipLanguage)
                toolTipItem.Text = Language.GetToolTipDictionary("Menu_" + menuId, UserInfo.Current.ToolTipLanguageType).Name;
            else
                toolTipItem.Text = menuName;
            toolTip.Items.Add(toolTipItem);
            
            item.SuperTip = toolTip;

            item.AppearanceItem.Normal.BackColor = Color.FromArgb(71, 71, 71);
            //smjang - 폰트 사이즈 변경
            //item.AppearanceItem.Normal.Font = new Font("Malgun Gothic", 9, FontStyle.Bold);
            item.AppearanceItem.Normal.Font = new Font("Malgun Gothic", 12, FontStyle.Bold);

            item.AppearanceItem.Hovered.BackColor = Color.FromArgb(117, 117, 117);

            item.AppearanceItem.Selected.BackColor = Color.FromArgb(117, 117, 117);


            TileBarDropDownContainer container = InitializeMenuList(menuId, menuName);

            item.DropDownControl = container;
            item.DropDownOptions.Height = container.Height;

            //smjang - 메뉴바 Size 변경 위해서 event 생성ㅅㄱ
            container.ParentChanged += (s, e) =>
            {
                TileBarDropDownContainer contain = s as TileBarDropDownContainer;
                TileBarWindow window = contain.Parent as TileBarWindow;
                if (window == null) return;

                window.VisibleChanged += (sender, args) =>
                {
                    TileBarWindow wind = sender as TileBarWindow;
                    if(wind.Visible)
                    {

                        //메뉴 깜빡임 현상 수정 -- 박윤신
                        wind.Size = new Size(barMenu.Width + btnUserInfo.Width, contain.Height);

                        wind.MinimumSize = new Size(barMenu.Width + btnUserInfo.Width, contain.Height);
                        wind.MaximumSize = new Size(barMenu.Width + btnUserInfo.Width, contain.Height);

                    }

                };

                //window.SizeChanged += (se, ag) =>
                //{
                //    TileBarWindow wind = se as TileBarWindow;
                //    if (wind.Width < barMenu.Width + btnUserInfo.Width)
                //        wind.Width = barMenu.Width + btnUserInfo.Width;
                //};
            };




            item.ItemClick += (o, s) =>
            {
                HideMenu();

                item.ShowDropDown();
            };

            group.Items.Add(item);

            barMenu.Groups.Add(group);
        }



        private void AddSeparator()
        {

        }

        private TileBarDropDownContainer InitializeMenuList(string menuId, string menuName)
        {
            int iMaxCount = 0;
            //int iHeight = 0;

            TileBarDropDownContainer container = new TileBarDropDownContainer();
            container.Name = "container" + menuId;
            container.BackColor = Color.FromArgb(71, 71, 71);

            DataRow[] dataRows = _MenuRepository.GetMenuTable(UserInfo.Current.Uiid).Select("PARENTMENUID = '" + menuId + "' AND VALIDSTATE = 'Valid'");

            if (dataRows.Length > 0)
            {
                PanelControl panelTopLine = new PanelControl();
                panelTopLine.Name = "panelTopLine";
                panelTopLine.Dock = DockStyle.Top;
                panelTopLine.Size = new Size(0, 1);
                panelTopLine.BorderStyle = BorderStyles.NoBorder;

                panelTopLine.Appearance.BackColor = Color.FromArgb(23, 107, 209);
                panelTopLine.Appearance.Options.UseBackColor = true;

                container.Controls.Add(panelTopLine);

                TableLayoutPanel layout = new TableLayoutPanel();
                layout.Name = "layout" + menuId;
                layout.Location = new Point(barMenu.ItemSize, 20);
                layout.Size = new Size(barMenu.WideTileWidth * dataRows.Length, 30 + layout.Margin.Top + layout.Margin.Bottom);
                layout.Dock = DockStyle.None;

                layout.RowStyles.Clear();
                layout.ColumnStyles.Clear();

                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
                layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                layout.RowCount = 2;
                layout.ColumnCount = dataRows.Length;

                for (int i = 0; i < dataRows.Length; i++)
                {
                    layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

                    PanelControl panel = new PanelControl();
                    panel.Name = "panel" + dataRows[i]["MENUID"].ToString();
                    panel.BorderStyle = BorderStyles.NoBorder;
                    panel.Margin = new Padding(0);
                    panel.Padding = new Padding(0, 0, 5, 0);
                    panel.Dock = DockStyle.Fill;

                    LabelControl label = new LabelControl();
                    label.Name = "lbl" + dataRows[i]["MENUID"].ToString();
                    label.Text = dataRows[i]["MENUNAME"].ToString();
                    label.AutoSizeMode = LabelAutoSizeMode.None;
                    label.Dock = DockStyle.Fill;

                    if (UserInfo.Current.IsUseToolTipLanguage)
                        label.ToolTip = Language.GetToolTipDictionary("Menu_" + dataRows[i]["MENUID"].ToString(), UserInfo.Current.ToolTipLanguageType).Name;

                    label.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
                    label.Appearance.TextOptions.VAlignment = VertAlignment.Center;
                    label.Appearance.TextOptions.Trimming = Trimming.EllipsisWord;

                    //smjang - 폰트 사이즈 변경
                    //label.Font = new Font("Malgun Gothic", 9, FontStyle.Bold);
                    label.Font = new Font("Malgun Gothic", 11, FontStyle.Bold);
                    label.ForeColor = Color.White;

                    panel.Controls.Add(label);

                    layout.Controls.Add(panel, i, 0);


                    FlowLayoutPanel flowLayout = new FlowLayoutPanel();
                    flowLayout.Name = "flowLayout" + dataRows[i]["MENUID"].ToString();
                    flowLayout.FlowDirection = FlowDirection.TopDown;
                    flowLayout.Dock = DockStyle.Fill;

                    DataRow[] childRows = _MenuRepository.GetMenuTable(UserInfo.Current.Uiid).Select("PARENTMENUID = '" + dataRows[i]["MENUID"].ToString() + "' AND VALIDSTATE = 'Valid'");

                    if (childRows.Length > 0)
                    {
                        for (int j = 0; j < childRows.Length; j++)
                        {
                            LabelControl labelChild = new LabelControl();
                            labelChild.Name = "lbl" + childRows[j]["MENUID"].ToString();
                            labelChild.Text = "  " + childRows[j]["MENUNAME"].ToString();
                            labelChild.Margin = new Padding(0);
                            labelChild.AutoSizeMode = LabelAutoSizeMode.None;
                            //labelChild.Size = new Size(barMenu.WideTileWidth - labelChild.Margin.Left - labelChild.Margin.Right, 25);
                            labelChild.Size = new Size(flowLayout.Width - labelChild.Margin.Left - labelChild.Margin.Right, 25);
                            labelChild.Location = new Point(3, 25 * j);

                            //smjang - 폰트 사이즈 변경
                            //labelChild.Font = new Font("Malgun Gothic", 8);
                            labelChild.Font = new Font("Malgun Gothic", 10);
                            labelChild.ForeColor = Color.White;

                            //labelChild.Cursor = Cursors.Hand;

                            if (UserInfo.Current.IsUseToolTipLanguage)
                                labelChild.ToolTip = Language.GetToolTipDictionary("Menu_" + childRows[j]["MENUID"].ToString(), UserInfo.Current.ToolTipLanguageType).Name;
                            else
                                labelChild.ToolTip = childRows[j]["MENUNAME"].ToString();

                            labelChild.Appearance.TextOptions.Trimming = Trimming.EllipsisWord;
                            labelChild.Appearance.Options.UseTextOptions = true;
                            labelChild.AppearanceHovered.TextOptions.Trimming = Trimming.EllipsisWord;
                            labelChild.AppearancePressed.TextOptions.Trimming = Trimming.EllipsisWord;

                            labelChild.AppearanceHovered.ForeColor = Color.White;
                            labelChild.AppearanceHovered.BackColor = Color.FromArgb(23, 107, 209);

                            MenuType menuType = MenuType.Folder;
                            if (!Enum.TryParse(childRows[j]["MENUTYPE"].ToString(), true, out menuType)) menuType = MenuType.Folder;

                            //labelChild.Tag = menuName + " > " + dataRows[i]["MENUNAME"].ToString() + " > " + childRows[j]["MENUNAME"].ToString();

                            labelChild.Click += MenuItem_Click;

                            //labelChild.Click += async (o, s) =>
                            //{
                            //    MenuType menuType = MenuType.Folder;
                            //    if (!Enum.TryParse<MenuType>(childRows[j]["MENUTYPE"].ToString(), true, out menuType)) menuType = MenuType.Folder;

                            //    DialogManager.ShowWaitDialog();

                            //    await ShowForm(new MenuInfo()
                            //    {
                            //        MenuId = childRows[j]["MENUID"].ToString(),
                            //        MenuType = menuType,
                            //        Caption = childRows[j]["MENUNAME"].ToString(),
                            //        ProgramId = childRows[j]["PROGRAMID"].ToString()
                            //    });

                            //    HideMenu();
                            //};

                            flowLayout.Controls.Add(labelChild);


                            if (iMaxCount == j)
                                iMaxCount++;
                        }
                    }

                    layout.Controls.Add(flowLayout, i, 1);
                }

                layout.Height = panelTopLine.Height + layout.Location.Y + layout.Height + iMaxCount * 25;

                container.Height = layout.Height + layout.Margin.Top + layout.Margin.Bottom;

                //smjang
                container.Width = barMenu.Width + btnUserInfo.Width;

                container.Controls.Add(layout);
            }


            return container;
        }

        #endregion

        private void InitializeEvent()
        {
            Shown += (o, e) =>
            {
                ChangeMenuItemSize();
            };

            //this.menuNavigatorContainer1.MenuClick += OpenMenu;
            menuItem.ItemClick += (o, e) => ToggleMenu();
            //this.btnSetting.ItemClick += (o, e) => new SettingForm().ShowDialog();
            this.tabbedView1.DocumentClosed += (o, e) =>
            {
                if (this.tabbedView1.Documents.Count == 0)
                {
                    this.ShowMenu();
                    //this.txtMenuPath.Caption = "";
                }
            };

            SizeChanged += (o, e) =>
            {
                ChangeMenuItemSize();
            };


            smartDocumentManager1.DocumentActivate += (o, e) =>
            {
                if (e.Document != null)
                {
                    // 현재 오픈 된 메뉴의 메뉴 리스트 정보 전환
                    //_MenuRepository = _MenuList[e.Document.Form.Tag.ToString()];

                    // 서비스 Id 전달 파라미터 추가
                    lblMenuPath.Text = GetMenuPath(e.Document.Form.Tag.ToString(), e.Document.Form.Name);

                    if (e.Document.Form is SmartConditionBaseForm smartConditionBaseForm)
                    {
                        smartConditionBaseForm.LoadCondition();
                        smartConditionBaseForm.InitializeSaveConditionList();
                    }

                    //if (!e.Document.Form.Tag.Equals(UserInfo.Current.Uiid))
                    //{
                    NetworkSettings.SetServiceUrl(e.Document.Form.Tag.ToString());
                    //}

                    // 현재 서비스의 메뉴 리스트 정보로 원복
                    //_MenuRepository = _MenuList[UserInfo.Current.Uiid];
                }
                else
                    lblMenuPath.Text = "";
            };

            barMenu.DropDownHidden += (o, e) =>
            {
                barMenu.SelectedItem = null;
            };

            barMenu.DropDownShown += (o, e) =>
            {
                barMenu.SelectedItem = e.Item;
            };

            txtSearchSiteMap.EditValueChanging += (o, e) =>
            {
                foreach (SearchControl search in _searchControls)
                {
                    search.Text = e.NewValue.ToString();
                }

                smartScrollableControl1.VerticalScroll.Value = 0;

                //foreach (AccordionControl accordion in _accordionControls)
                //{
                //    if (e.NewValue.Equals(""))
                //        accordion.Height = 1500;

                //    accordion.Height = GetSubMenuHeight(accordion);
                //}

                //foreach (AccordionControl accordion in _accordionControls)
                //{
                //    accordion.BeginUpdate();

                //    foreach (AccordionControlElement group in accordion.Elements)
                //    {
                //        if (group.Style == ElementStyle.Group)
                //        {
                //            foreach (AccordionControlElement item in group.Elements)
                //            {
                //                if (item is AccordionControlElementEx element && element.MenuInfo.MenuType == MenuType.Screen)
                //                {
                //                    if (item.Text.IndexOf(e.NewValue.ToString()) < 0)
                //                        item.Visible = false;
                //                    else
                //                        item.Visible = true;
                //                }
                //            }
                //        }
                //        else if (group.Style == ElementStyle.Item)
                //        {
                //            if (group is AccordionControlElementEx element && element.MenuInfo.MenuType == MenuType.Screen)
                //            {
                //                if (group.Text.IndexOf(e.NewValue.ToString()) < 0)
                //                    group.Visible = false;
                //                else
                //                    group.Visible = true;
                //            }
                //        }
                //    }

                //    accordion.EndUpdate();
                //}
            };

            //txtSearchSiteMap.EditValueChanged += (o, e) =>
            //{
            //    foreach (AccordionControl accordion in _accordionControls)
            //    {
            //        accordion.BeginUpdate();

            //        InnerHeight(accordion);

            //        accordion.EndUpdate();
            //    }
            //};

            linkFavoriteSetting.Click += (o, e) =>
            {
                FavoriteSetting favoriteSettingForm = new FavoriteSetting();
                favoriteSettingForm.ShowDialog(this);

                if (favoriteSettingForm.DialogResult == DialogResult.OK)
                    InitializeFavorite();
            };

            accFavoriteMenu.ElementClick += AccordionControl_ElementClick;
            accRecentMenu.ElementClick += AccordionControl_ElementClick;


            btnHomepage.Click += (o, e) =>
            {
                System.Diagnostics.Process.Start("IExplore.exe", "http://www.micube.co.kr");
            };

            btnCheckUpdate.Click += (o, e) =>
            {

            };

            itemQuit.ItemClick += (o, e) =>
            {
                Close();
            };

            linkChangeUserInfo.Click += (o, e) =>
            {
                btnUserInfo.HideDropDown();

                ChangeUserInformation changeUserInfoForm = new ChangeUserInformation();
                changeUserInfoForm.ShowDialog(this);

                if (changeUserInfoForm.DialogResult == DialogResult.OK)
                    SetUserInformationLabel();
            };

            linkChangePassword.Click += (o, e) =>
            {
                btnUserInfo.HideDropDown();

                ChangePassword changePasswordForm = new ChangePassword();
                changePasswordForm.ShowDialog(this);
            };

            linkLogout.Click += LinkLogout_Click;
        }



        private void AddRecentMenu(MenuInfo menuInfo)
        {
            UserInfo.Current.ServiceId = UserInfo.Current.Uiid;

            AccordionControlElementEx accElement = null;

            foreach (AccordionControlElementEx item in groupRecentMenu.Elements)
            {
                if (item.MenuInfo.MenuId == menuInfo.MenuId)
                {
                    accElement = item;
                    groupRecentMenu.Elements.Remove(item);

                    //if (SettingConfig.Current.RecentMenu.Contains(item.MenuInfo))
                    //    SettingConfig.Current.RecentMenu.Remove(item.MenuInfo);

                    break;
                }
            }

            if (accElement == null)
            {
                accElement = new AccordionControlElementEx() { MenuInfo = menuInfo };
                accElement.Name = "accElementRecent" + menuInfo.MenuId;

                accElement.Style = ElementStyle.Item;
                accElement.Text = menuInfo.Caption;

                accElement.Appearance.Normal.Font = new Font("Malgun Gothic", 10, FontStyle.Bold);
                accElement.Appearance.Normal.TextOptions.Trimming = Trimming.EllipsisWord;
                accElement.Appearance.Normal.TextOptions.WordWrap = WordWrap.NoWrap;

                accElement.Appearance.Hovered.Font = new Font("Malgun Gothic", 10, FontStyle.Bold);
                accElement.Appearance.Hovered.TextOptions.Trimming = Trimming.EllipsisWord;
                accElement.Appearance.Hovered.TextOptions.WordWrap = WordWrap.NoWrap;

                accElement.Appearance.Pressed.Font = new Font("Malgun Gothic", 10, FontStyle.Bold);
                accElement.Appearance.Pressed.TextOptions.Trimming = Trimming.EllipsisWord;
                accElement.Appearance.Pressed.TextOptions.WordWrap = WordWrap.NoWrap;

                SuperToolTip toolTip = new SuperToolTip();
                ToolTipItem toolTipItem = new ToolTipItem();
                if (UserInfo.Current.IsUseToolTipLanguage)
                    toolTipItem.Text = Language.GetToolTipDictionary("Menu_" + menuInfo.MenuId, UserInfo.Current.ToolTipLanguageType).Name;
                else
                    toolTipItem.Text = menuInfo.Caption;
                toolTip.Items.Add(toolTipItem);

                accElement.SuperTip = toolTip;
            }

            groupRecentMenu.Elements.Insert(0, accElement);


            int count = ApplicationConfig.Current.GetRecentMenuCount();

            if (groupRecentMenu.Elements.Count > count)
            {
                for (int i = groupRecentMenu.Elements.Count - 1; i >= count; i--)
                {
                    groupRecentMenu.Elements.RemoveAt(i);
                }
            }

            UserInfo.Current.ServiceId = UserInfo.Current.DefaultService;

            //SettingConfig.Current.RecentMenu.Add(accElement.MenuInfo);
        }


        private void InitializeLanguageLabel()
        {
            UserInfo.Current.ServiceId = UserInfo.Current.DefaultService;

            NetworkSettings.SetServiceUrl(UserInfo.Current.DefaultService);

            lblUserId.Text = Language.Get(lblUserId.LanguageKey);
            lblUserName.Text = Language.Get(lblUserName.LanguageKey);
            lblEmailAddress.Text = Language.Get(lblEmailAddress.LanguageKey);
            lblCellPhoneNumber.Text = Language.Get(lblCellPhoneNumber.LanguageKey);
            lblLoginTime.Text = Language.Get(lblLoginTime.LanguageKey);

            string strSiteMap = Language.Get("SITEMAP");
            string strFavorite = Language.Get("FAVORITE");
            string strFavoriteSetting = Language.Get("SETTING");
            string strRecent = Language.Get("RECENT");
            string strOptions = Language.Get("PROGRAMOPTIONS");
            string strInformation = Language.Get("PROGRAMINFORMATION");
            string strQuit = Language.Get("QUIT");

            itemSiteMap.Caption = strSiteMap;
            itemFavorite.Caption = strFavorite;
            itemRecent.Caption = strRecent;
            itemOptions.Caption = strOptions;
            itemInformation.Caption = strInformation;
            itemQuit.Caption = strQuit;

            lblSiteMapTitle.Text = strSiteMap;
            lblFavoriteTitle.Text = strFavorite;
            lblRecentTitle.Text = strRecent;
            lblOptionsTitle.Text = strOptions;
            lblInformationTitle.Text = strInformation;

            linkFavoriteSetting.Text = strFavoriteSetting;


            DataTable dtLanguageTypeName = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>()
            {
                { "CODECLASSID", "LanguageType" },
                { "LanguageType", UserInfo.Current.LanguageType }
            });

            string strLanguageName = dtLanguageTypeName.Select("CODEID = '" + UserInfo.Current.LanguageType + "'").First()["CODENAME"].ToString();

            Language.LanguageMessageItem msgChangeLanguage = Language.GetMessage("CHANGELANGUAGE");
            btnSelectLanguageType.Text = msgChangeLanguage.Title ?? msgChangeLanguage.Message;
            if (UserInfo.Current.IsUseToolTipLanguage)
            {
                Language.LanguageMessageItem msgToolTip = Language.GetToolTipMessage("CHANGELANGUAGE", UserInfo.Current.ToolTipLanguageType);
                btnSelectLanguageType.ToolTip = msgToolTip.Title ?? msgToolTip.Message;
            }
            else
                btnSelectLanguageType.ToolTip = msgChangeLanguage.Title ?? msgChangeLanguage.Message;
            lblSelectLanguageTypeDescriptionTitle.Text = msgChangeLanguage.Title ?? msgChangeLanguage.Message;
            lblSelectLanguageTypeDescriptionTitle.Text = lblSelectLanguageTypeDescriptionTitle.Text + " - " + strLanguageName;
            lblSelectLanguageTypeDescription.Text = msgChangeLanguage.Message;


            DataTable dtPlantName = SqlExecuter.Query("GetPlantList", "10001", new Dictionary<string, object>()
            {
                { "LanguageType", UserInfo.Current.LanguageType }
            });

            string strPlantName = "";
            if (dtPlantName.Select("PLANTID = '" + UserInfo.Current.Plant + "'").Count() > 0)
                strPlantName = dtPlantName.Select("PLANTID = '" + UserInfo.Current.Plant + "'").First()["PLANTNAME"].ToString();

            Language.LanguageMessageItem msgChangePlant = Language.GetMessage("CHANGEPLANT");
            btnSelectPlant.Text = msgChangePlant.Title ?? msgChangePlant.Message;
            if (UserInfo.Current.IsUseToolTipLanguage)
            {
                Language.LanguageMessageItem msgToolTip = Language.GetToolTipMessage("CHANGEPLANT", UserInfo.Current.ToolTipLanguageType);
                btnSelectPlant.ToolTip = msgToolTip.Title ?? msgToolTip.Message;
            }
            else
                btnSelectPlant.ToolTip = msgChangePlant.Title ?? msgChangePlant.Message;
            lblSelectPlantDescriptionTitle.Text = msgChangePlant.Title ?? msgChangePlant.Message;
            lblSelectPlantDescriptionTitle.Text = lblSelectPlantDescriptionTitle.Text + " - " + strPlantName;
            lblSelectPlantDescription.Text = msgChangePlant.Message;

            //smjang - 프린터 목록 추가 
            Language.LanguageMessageItem msgChangePrinter = Language.GetMessage("CHANGEPRINTER");
            btnSelectPrinter.Text = msgChangePrinter.Title ?? msgChangePrinter.Message;
            if (UserInfo.Current.IsUseToolTipLanguage)
            {
                Language.LanguageMessageItem msgToolTip = Language.GetToolTipMessage("CHANGEPRINTER", UserInfo.Current.ToolTipLanguageType);
                btnSelectPrinter.ToolTip = msgToolTip.Title ?? msgToolTip.Message;
            }
            else
                btnSelectPrinter.ToolTip = msgChangePrinter.Title ?? msgChangePrinter.Message;
            lblSelectPrinterDescriptionTitle.Text = msgChangePrinter.Title ?? msgChangePrinter.Message;
            lblSelectPrinterDescriptionTitle.Text = lblSelectPrinterDescriptionTitle.Text + " - " + UserInfo.Current.Printer;
            lblSelectPrinterDescription.Text = msgChangePrinter.Message;


            Language.LanguageMessageItem msgVersionCopyright = Language.GetMessage("PROGRAMINFORMATION");
            lblProgramVersion.Text = (msgVersionCopyright.Title ?? msgVersionCopyright.Message) + " 1.0.0";
            lblCopyright.Text = msgVersionCopyright.Message;


            btnHomepage.Text = Language.Get("HOMEPAGE");
            if (UserInfo.Current.IsUseToolTipLanguage)
                btnHomepage.ToolTip = Language.GetToolTipDictionary("HOMEPAGE", UserInfo.Current.ToolTipLanguageType).Name;
            else
                btnHomepage.ToolTip = Language.Get("HOMEPAGE");

            Language.LanguageMessageItem msgHomepage = Language.GetMessage("HOMEPAGE");
            lblHomepageDescriptionTitle.Text = msgHomepage.Title ?? msgHomepage.Message;
            lblHomepageDescription.Text = msgHomepage.Message;


            Language.LanguageMessageItem msgCheckUpdate = Language.GetMessage("CHECKUPDATE");
            btnCheckUpdate.Text = msgCheckUpdate.Title ?? msgCheckUpdate.Message;
            if (UserInfo.Current.IsUseToolTipLanguage)
            {
                Language.LanguageMessageItem msgToolTip = Language.GetToolTipMessage("CHECKUPDATE", UserInfo.Current.ToolTipLanguageType);
                btnCheckUpdate.ToolTip = msgToolTip.Title ?? msgToolTip.Message;
            }
            else
                btnCheckUpdate.ToolTip = msgCheckUpdate.Title ?? msgCheckUpdate.Message;
            lblCheckUpdateDescriptionTitle.Text = msgCheckUpdate.Title ?? msgCheckUpdate.Message;
            lblCheckUpdateDescription.Text = msgCheckUpdate.Message;


            linkChangeUserInfo.Text = Language.GetMessage("CHANGEUSERINFORMATION").Message;
            linkChangePassword.Text = Language.GetMessage("CHANGEPASSWORD").Message;
            linkLogout.Text = Language.GetMessage("LOGOUT").Message;

            //smjang - 프린터 추가

        }

        private void ShowMenu()
        {
            this.pnlMenu.Visible = pnlMenu.Controls[0].Visible = true;

            if (pnlMenu.Visible)
            {
                if (!_IsInitStartMenu)
                    InitStartMenu();

                InitializeFavorite();
            }
        }

        private void HideMenu()
        {
            this.pnlMenu.Visible = pnlMenu.Controls[0].Visible = false;
        }

        private void ToggleMenu()
        {

            this.pnlMenu.Visible = pnlMenu.Controls[0].Visible = !this.pnlMenu.Visible;

            if (pnlMenu.Visible)
            {
                if (!_IsInitStartMenu)
                    InitStartMenu();

                InitializeFavorite();
            }
        }

        private bool _IsInitStartMenu = false;
        private void InitStartMenu()
        {
            if (_IsInitStartMenu) return;
            
            //viewMain.SuspendLayout();
            viewMain.BeginUpdate();

            BackstageViewTabItem beforeItem = viewMain.SelectedTab;

            viewMain.SelectedTab = itemSiteMap;

            InitializeSiteMap();

            viewMain.SelectedTab = beforeItem;

            //InitializeRecent();
            InitializeOptions();
            InitializeInformation();

            viewMain.EndUpdate();
            //viewMain.ResumeLayout();

            _IsInitStartMenu = true;
        }

        private void SetRecentMenu()
        {
            List<MenuInfo> listRecentMenu = new List<MenuInfo>();

            foreach (AccordionControlElementEx item in groupRecentMenu.Elements)
            {
                listRecentMenu.Add(item.MenuInfo);
            }

            SettingConfig.Current.RecentMenu = listRecentMenu;

            _RecentMenuSettingRepository.Save(UserInfo.Current.Id, SettingConfig.Current);
        }

        private string GetMenuName(string menuId)
        {
            string result = "";

            DataRow[] rowMenu = _MenuRepository.GetMenuTable(UserInfo.Current.Uiid).Select("MENUID = '" + menuId + "'");

            if (rowMenu.Length > 0)
                result = rowMenu.First()["MENUNAME"].ToString();
            else
                result = menuId;


            return result;
        }

        private string GetMenuPath(string serviceId, string menuId)
        {
            bool bHasParent = true;

            DataRow rowCurrent = _MenuRepository.GetMenuTable(serviceId).Select("MENUID = '" + menuId + "'").First();
            string result = rowCurrent["MENUNAME"].ToString();
            string parentMenuId = rowCurrent["PARENTMENUID"].ToString();

            while (bHasParent)
            {
                DataRow[] rowParent = _MenuRepository.GetMenuTable(serviceId).Select("MENUID = '" + parentMenuId + "'");

                if (rowParent.Length > 0)
                {
                    result = rowParent[0]["MENUNAME"].ToString() + " > " + result;
                    parentMenuId = rowParent[0]["PARENTMENUID"].ToString();
                }
                else
                    bHasParent = false;
            }

            result = serviceId + " > " + result;

            return result;
        }

        private string GetMenuPath(string menuId)
        {
            return GetMenuPath(UserInfo.Current.Uiid, menuId);
        }

        private void ChangeLanguage(string languageType)
        {
            UserInfo.Current.LanguageType = languageType;

            Language.LanguageTypes.Clear();

            try
            {
                DialogManager.ShowWaitDialog();

                FrameworkSettings.Initialize(_ServiceList);

                // 2019.09.30 hykang - 메뉴 리스트 초기화 로직 변경
                //_MenuList.Clear();

                //_ServiceList.ForEach(value =>
                //{
                //    NetworkSettings.SetServiceUrl(value.Key);

                //    _MenuRepository = new MenuRepository();

                //    _MenuRepository.InitMenu(this);

                //    _MenuList.Add(value.Key, _MenuRepository);
                //});

                NetworkSettings.SetServiceUrl(UserInfo.Current.Uiid);
                _MenuRepository.InitMenu(this, _ServiceList);

                //_MenuRepository = _MenuList[UserInfo.Current.Uiid];

                InitializeLanguageLabel();

                //InitializeMenu();

                foreach (Control panel in layoutSiteMapTitle.Controls)
                {
                    if (panel is PanelControl)
                    {
                        foreach (Control label in panel.Controls)
                        {
                            if (label is LabelControl labelControl)
                            {
                                labelControl.Text = GetMenuName(label.Name.Replace("lbl", ""));
                            }
                        }
                    }
                }

                _accordionControls.ForEach(acc =>
                {
                    acc.Elements.ForEach(group =>
                    {
                        group.Text = GetMenuName(group.Name.Replace("element", ""));
                        (group.SuperTip.Items[0] as ToolTipItem).Text = GetMenuName(group.Name.Replace("element", ""));

                        group.Elements.ForEach(element =>
                        {
                            element.Text = "  " + GetMenuName(element.Name.Replace("element", ""));
                            (element.SuperTip.Items[0] as ToolTipItem).Text = GetMenuName(element.Name.Replace("element", ""));
                        });
                    });
                });

                groupFavoriteMenu.Elements.ForEach(element =>
                {
                    element.Text = GetMenuName(element.Name.Replace("elementFavorite", ""));
                    (element.SuperTip.Items[0] as ToolTipItem).Text = GetMenuName(element.Name.Replace("elementFavorite", ""));
                });

                groupRecentMenu.Elements.ForEach(element =>
                {
                    element.Text = GetMenuName(element.Name.Replace("elementRecent", ""));
                    (element.SuperTip.Items[0] as ToolTipItem).Text = GetMenuName(element.Name.Replace("elementRecent", ""));
                });

                InitializeOptions();
                InitializeInformation();

                barMenu.Groups.ForEach(group =>
                {
                    if (group.Name.StartsWith("group"))
                    {
                        group.Items.ForEach(item =>
                        {
                            TileBarItem barItem = item as TileBarItem;

                            barItem.Text = GetMenuName(barItem.Name.Replace("item", ""));

                            barItem.DropDownControl.Controls.Find<LabelControl>(true).ForEach(label =>
                            {
                                label.Text = (label.Text.StartsWith("  ") ? "  " : "") + GetMenuName(label.Name.Replace("lbl", ""));
                                label.ToolTip = GetMenuName(label.Name.Replace("lbl", ""));
                            });
                        });
                    }
                });

                if (!string.IsNullOrEmpty(lblMenuPath.Text))
                    lblMenuPath.Text = GetMenuPath(smartDocumentManager1.View.ActiveDocument.Form.Name);

                //smartDocumentManager1.View.Documents.ForEach(document =>
                //{
                //    document.Caption = GetMenuName(document.Form.Name);

                //    document.Form.Text = GetMenuName(document.Form.Name);

                //    //SmartConditionBaseForm mdiForm = document.Form as SmartConditionBaseForm;

                //    //mdiForm.ChangeLanguage();
                //});

                EventAggregator.Current.Publish(new LanguageChangedEventArgs());

                //InitializeMenuBar();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                DialogManager.Close();
            }
        }

        private void ChangePlant(string plantId, string plantName, string startBusinessHour)
        {
            Language.LanguageMessageItem msgChangePlant = Language.GetMessage("CHANGEPLANT");
            lblSelectPlantDescriptionTitle.Text = (msgChangePlant.Title ?? msgChangePlant.Message) + " - " + plantName;

            UserInfo.Current.LoginPlant = plantId;

            this.Text = System.Diagnostics.Process.GetCurrentProcess().ProcessName + " - " + UserInfo.Current.LoginPlant;
            //UserInfo.Current.PlantStartBusinessHour = startBusinessHour;
        }

        private void ChangeMenuItemSize()
        {
            barMenu.SuspendLayout();

            int iMaxWidth = 130;
            int iWideTileWidth = (barMenu.Width - barMenu.ItemSize - btnUserInfo.Width) / 12;

            barMenu.WideTileWidth = Math.Min(iWideTileWidth, iMaxWidth);


            iMaxWidth = 170;

            foreach (TileBarGroup group in barMenu.Groups)
            {
                foreach (TileBarItem item in group.Items)
                {
                    if (item.DropDownControl != null)
                    {
                        foreach (Control ctrl in item.DropDownControl.Controls)
                        {
                            if (ctrl is TableLayoutPanel layout)
                            {
                                int iWidth = 0;
                                int iDropDownWidth = (barMenu.Width + btnUserInfo.Width) / 11;

                                iWidth = Math.Min(iDropDownWidth, iMaxWidth);
                                //iWidth = barMenu.WideTileWidth + 10;

                                layout.Width = iWidth * layout.ColumnCount;
                            }
                        }
                    }
                }
            }

            barMenu.ResumeLayout();
        }


        #region AccordionControl Class

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
            MenuInfo MenuInfo { get; }
        }

        #endregion

        #region 메뉴 오픈

        private void MenuItem_Click(object sender, EventArgs e)
        {

            barMenu.HideDropDownWindow();
            /*
            if (UserInfo.Current.Enterprise == "YOUNGPOONG" && smartDocumentManager1.View.Documents.Count >= 10)
            {
                // 메뉴는 10개 까지만 오픈 할 수 있습니다.
                MSGBox.Show(MessageBoxType.Information, "OPENMENUCOUNTLIMIT");
                return;
            }
            */
            LabelControl label = sender as LabelControl;

            string menuId = label.Name.Replace("lbl", "");

            try
            {
                DialogManager.ShowWaitDialog();

                NetworkSettings.SetServiceUrl(UserInfo.Current.Uiid);

                AddRecentMenu(_MenuRepository.ToMenuInfo(menuId));

                _MenuRepository.OpenMenu(menuId, null);

                HideMenu();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                DialogManager.Close();
            }
        }

        private void AccordionControl_ElementClick(object sender, ElementClickEventArgs e)
        {
            if (e.Element is AccordionControlElementEx element/* && element.Style == ElementStyle.Item && element.MenuInfo.MenuType == MenuType.Screen*/)
            {
                if (element.Style == ElementStyle.Item)
                {/*
                    if (UserInfo.Current.Enterprise == "YOUNGPOONG" && smartDocumentManager1.View.Documents.Count >= 10)
                    {
                        // 메뉴는 10개 까지만 오픈 할 수 있습니다.
                        MSGBox.Show(MessageBoxType.Information, "OPENMENUCOUNTLIMIT");
                        return;
                    }
                    */
                    try
                    {
                        DialogManager.ShowWaitDialog();

                        NetworkSettings.SetServiceUrl(UserInfo.Current.Uiid);

                        AddRecentMenu(element.MenuInfo);
                        _MenuRepository.OpenMenu(element.MenuInfo, null);

                        HideMenu();
                    }
                    catch (Exception ex)
                    {
                        ShowError(ex);
                    }
                    finally
                    {
                        DialogManager.Close();
                    }
                }
                //else if (element.Style == ElementStyle.Group)
                //{
                //    element.Expanded = !element.Expanded;
                //}
            }
        }

        #endregion

        #region 로그아웃

        private void LinkLogout_Click(object sender, EventArgs e)
        {
            btnUserInfo.HideDropDown();

            //if (MessageBox.Show("로그아웃 하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            if (MSGBox.Show(MessageBoxType.Question, "LOGOUTCONFIRM", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DialogResult = DialogResult.OK;

                SetRecentMenu();

                SetLogoutTime();

                Owner.Show();
                Owner.Activate();
                Owner.Refresh();

                Dispose();
                Close();
            }
        }

        #endregion

        #region 프로그램 종료

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (DialogResult != DialogResult.OK)
            {
                //if (MessageBox.Show("프로그램을 종료하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                if (MSGBox.Show(MessageBoxType.Question, "QUITCONFIRM", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SetRecentMenu();

                    LoginSettingRepository repository = new LoginSettingRepository();
                    repository.Save("", SettingConfig.Current);

                    SetLogoutTime();

                    Application.Exit();
                    Process.GetCurrentProcess().Kill();
                }
                else
                    e.Cancel = true;
            }
        }

        #endregion

        #region 접속 이력 Logout 시간 업데이트

        private void SetLogoutTime()
        {
            NetworkSettings.SetServiceUrl(UserInfo.Current.DefaultService);

            MessageWorker worker = new MessageWorker("SaveConnectionHistory");

            worker.SetBody(new MessageBody()
            {
                { "TxnHistKey", UserInfo.Current.ConnectionKey },
                { "UserId", UserInfo.Current.Id },
                { "ConnectionType", "Logout" }
            });

            worker.Execute();
        }
        #endregion
    }
}