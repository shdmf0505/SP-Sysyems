using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Settings
{
    public interface ISettingRepository
    {
        SettingConfig Get(string userId);

        void Save(string userId, SettingConfig setting);
    }
}
