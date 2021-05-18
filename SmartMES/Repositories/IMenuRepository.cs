using Micube.Framework.SmartControls.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Repositories
{
    public interface IMenuRepository : IOpenMenu
    {
        void OpenMenu(MenuInfo menuId, Dictionary<string, object> parameters);

        void InitMenu(System.Windows.Forms.Form mdiParent);

        // 2019.09.30 hykang - Service List를 받는 Overload 함수 추가
        void InitMenu(System.Windows.Forms.Form mdiParent, Dictionary<string, IConvertible> serviceList);

        DataTable GetMenuTable();

        // 2019.09.30 hykang - Service Id에 따른 Menu Table 정보를 가져오는 함수 추가
        DataTable GetMenuTable(string serviceId);

        // 2019.09.30 hykang - 로그인 한 사용자에게 메뉴 권한이 있는 서비스 개수 조회 함수
        Dictionary<string, int> GetServiceCount();

        MenuInfo ToMenuInfo(string menuId);
    }
}
