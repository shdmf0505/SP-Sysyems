using Micube.Framework;
using Micube.Framework.Log;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        // 다중 서비스 사용을 위한 Menu List
        private Dictionary<string, DataTable> _menuList = new Dictionary<string, DataTable>();
        private DataTable _menuTable;
        private System.Windows.Forms.Form _mdiParent;

        public DataTable GetMenuTable()
        {
            //return _menuTable;
            return GetMenuTable(UserInfo.Current.Uiid);
        }

        // 2019.09.30 hykang - Service Id에 따라 Menu Table 정보 반환
        public DataTable GetMenuTable(string serviceId)
        {
            return _menuList[serviceId];
        }

        // 2019.09.30 hykang - 로그인 한 사용자에게 메뉴 권한이 있는 서비스 개수 반환
        public Dictionary<string, int> GetServiceCount()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            _menuList.ForEach(value =>
            {
                result.Add(value.Key, value.Value.Rows.Count);
            });

            return result;
        }

        public void InitMenu(System.Windows.Forms.Form mdiParent)
        {
            _menuTable = SqlExecuter.Query("GetMenuList", "00001");
            _mdiParent = mdiParent;
        }

        // 2019.09.30 hykang - 메뉴 초기화 시 Service List 별 Menu Table 생성
        public void InitMenu(System.Windows.Forms.Form mdiParent, Dictionary<string, IConvertible> serviceList)
        {
            _menuList.Clear();

            serviceList.ForEach(value =>
            {
                NetworkSettings.SetServiceUrl(value.Key);
                
                //2019-10-31 장선미 - Audit 권한 추가
                //_menuList.Add(value.Key, SqlExecuter.Query("GetMenuList", "00001"));
                _menuList.Add(value.Key, SqlExecuter.Query("GetMenuList", "00002"));
            });

            _mdiParent = mdiParent;
        }

        #region 메뉴 오픈

        public MenuInfo ToMenuInfo(string menuId)
        {
            //DataRow menuRow = _menuList[UserInfo.Current.Uiid].Select("MENUID = '" + menuId + "'").FirstOrDefault();
            //if (menuRow == null)
            //{
            //    throw MessageException.Create($"MENUID = {menuId}는 찾을수 없습니다");
            //    //MSGBox.Show(MessageBoxType.Error, "SmartEES", $"MENUID = {menuId}는 찾을 수 없습니다.");
            //    //return null;
            //}

            //MenuType menuType = MenuType.Folder;
            //if (!Enum.TryParse(menuRow["MENUTYPE"].ToString(), true, out menuType)) menuType = MenuType.Folder;

            //return new MenuInfo()
            //{
            //    MenuId = menuRow["MENUID"].ToString(),
            //    MenuType = menuType,
            //    Caption = menuRow["MENUNAME"].ToString(),
            //    ProgramId = menuRow["PROGRAMID"].ToString()
            //};

            return ToMenuInfo(menuId, UserInfo.Current.Uiid);
        }

        /// <summary>
        /// 2019.09.30 hykang - Menu Id와 Service Id를 기반으로 메뉴 정보 반환
        /// </summary>
        /// <param name="menuId">Menu Id</param>
        /// <param name="serviceId">Service Id</param>
        /// <returns>Menu Info</returns>
        public MenuInfo ToMenuInfo(string menuId, string serviceId)
        {
            DataRow menuRow = _menuList[serviceId].Select("MENUID = '" + menuId + "'").FirstOrDefault();

            if (menuRow == null)
            {
                Logger.Error($"MENUID = {menuId}는 찾을수 없습니다");
                return null;
                //throw MessageException.Create($"MENUID = {menuId}는 찾을수 없습니다");
            }

            MenuType menuType = MenuType.Folder;

            if (!Enum.TryParse(menuRow["MENUTYPE"].ToString(), true, out menuType))
                menuType = MenuType.Folder;

            return new MenuInfo()
            {
                MenuId = menuRow["MENUID"].ToString(),
                MenuType = menuType,
                Caption = menuRow["MENUNAME"].ToString(),
                ProgramId = menuRow["PROGRAMID"].ToString(),
                // 2019.11.11 hykang - Site 사용자 권한 사용 여부 추가
                IsUsePlantAuthority = menuRow["ISUSEPLANTAUTHORITY"].ToString(),
                IsUsePlantSingle = menuRow["ISUSEPLANTSINGLE"].ToString()
            };
        }
   
        public void OpenMenu(string menuId, string parameterKey, object parameter)
        {
            this.OpenMenu(menuId, new Dictionary<string, object>() { { parameterKey, parameter } });
        }

        public void OpenMenu(string menuId, Dictionary<string, object> parameters)
        {
            this.OpenMenu(this.ToMenuInfo(menuId, UserInfo.Current.Uiid), parameters, UserInfo.Current.Uiid);
        }

        // 2019.09.30 hykang - Service Id에 따라 OpenMenu 함수 호출
        // 2019.10.09 hykang - Parent Menu Id 파라미터 추가
        public void OpenMenu(string menuId, Dictionary<string, object> parameters, string serviceId, string parentMenuId)
        {
            this.OpenMenu(this.ToMenuInfo(menuId, serviceId), parameters, serviceId, parentMenuId);
        }

        public event OpenedMenuHandler OpenedMenuEvent;

        public void OpenMenu(MenuInfo menu, Dictionary<string, object> parameters)
        {
            OpenMenu(menu, parameters, UserInfo.Current.Uiid);

            //if (menu.MenuType != MenuType.Screen) throw new Exception("MenuType.Folder는 열수 없는 형식입니다.");

            //OpenedMenuEvent?.Invoke(menu);

            //var form = FormCreator.CreateForm(menu.MenuId, menu.Caption, menu.ProgramId, parameters);
            //form.MdiParent = _mdiParent;
            //form.Show();
        }

        /// <summary>
        /// 2019.09.30 hykang - Menu Info, Parameters, Service Id, Parent Menu Id를 기반으로 메뉴에 등록된 화면 오픈
        /// </summary>
        /// <param name="menu">Menu Info</param>
        /// <param name="parameters">화면에 전달하는 파라미터</param>
        /// <param name="serviceId">Service Id</param>
        /// <param name="parentMenuId">화면에서 다른 화면을 Open 할 경우 부모 화면의 Menu Id</param>
        public void OpenMenu(MenuInfo menu, Dictionary<string, object> parameters, string serviceId, string parentMenuId = null)
        {
            if (menu.MenuType != MenuType.Screen)
                throw new Exception("MenuType.Folder는 열수 없는 형식입니다.");

            OpenedMenuEvent?.Invoke(menu);

            // 2019.11.11 hykang - Site 사용자 권한 사용 여부 추가
            // 2019.12.10 hykang - Site 단일 사용 여부 파라미터 추가
            var form = FormCreator.CreateForm(menu.MenuId, menu.Caption, menu.ProgramId, parameters, serviceId, parentMenuId, menu.IsUsePlantAuthority, menu.IsUsePlantSingle);
            form.MdiParent = _mdiParent;
            form.Show();
        }

        #endregion

    }

    public delegate void OpenedMenuHandler(MenuInfo menu);
        

}
