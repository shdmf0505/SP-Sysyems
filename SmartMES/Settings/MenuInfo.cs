using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES
{
    public class MenuInfo : ICloneable
    {
        private string caption;

        public string MenuId { get; set; }
        public string Caption
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(caption))
                    return caption;
                else
                    return MenuId;
            }
            set { caption = value; }
        }
        public MenuType MenuType { get; set; }
        public string ParentMenuId { get; set; }
        public string ProgramId { get; set; }
        // 2019.11.11 hykang - Site 사용자 권한 사용 여부 추가
        public string IsUsePlantAuthority { get; set; }
        // 2019.12.10 hykang - Site 단일 사용 여부 추가
        public string IsUsePlantSingle { get; set; }

        public decimal DisplaySequence { get; set; }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        public MenuInfo Clone()
        {
            return ((ICloneable)this).Clone() as MenuInfo;
        }
    }

    public enum MenuType
    {
        Screen,
        Folder,
        Seperator
    }

    public class MenuClickEventArgs : EventArgs
    {
        public MenuClickEventArgs(MenuInfo info, string menuPath)
        {
            this.MenuInfo = info.Clone();
            this.MenuPath = menuPath;
        }

        public MenuInfo MenuInfo { get; private set; }

        public string MenuPath { get; set; }
    }
}
