using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace SmartMES.Modules
{
    public class InjectModule : NinjectModule
    {
        public InjectModule()
        {
        }

        public override void Load()
        {
            //this.Bind<Repositories.IMenuRepository>().To<Repositories.MenuRepository>().InSingletonScope();            
            this.Bind<Micube.Framework.SmartControls.Interface.IOpenMenu>().To<Repositories.MenuRepository>().InSingletonScope();
            this.Bind<SmartMES.Settings.ISettingRepository>().To<SmartMES.Settings.LoginSettingRepository>();
            this.Bind<SmartMES.Settings.ISettingRepository>().To<SmartMES.Settings.ConditionSettingRepository>();
            this.Bind<Micube.Framework.SmartControls.Interface.IConditionRepository>().To<SmartMES.Settings.ConditionSettingRepository>();
            this.Bind<Micube.Framework.SmartControls.Interface.IFavoriteRepository>().To<SmartMES.Settings.FavoriteSettingRepository>();

            if (ConfigurationManager.AppSettings["Database"] == "PostgreSql")
            {
                this.Bind<Micube.Framework.SmartControls.Paging.IPaging>().To<Micube.Framework.SmartControls.Paging.PostgreSqlPaging>();
            }
            else if (ConfigurationManager.AppSettings["Database"] == "SqlServer")
            {
                this.Bind<Micube.Framework.SmartControls.Paging.IPaging>().To<Micube.Framework.SmartControls.Paging.SqlServerPaging>();
            }
            else
            {
                throw new Exception("AppSettings.Database 설정은 'PostgreSql', 'SqlServer' 만 지원합니다");
            }
        }
    }
}