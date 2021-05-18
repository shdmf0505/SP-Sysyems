using Micube.Framework;
using Micube.Framework.Log;
using Micube.Framework.SmartControls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Settings
{
    public class RecentMenuSettingRepository : ISettingRepository
    {
        private readonly string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Micube\\SmartMES\\Setting");
        private readonly string fileName = "RecentMenuSetting";
        private readonly string fileExtension = ".json";
        private string fileFullName = "";


        public SettingConfig Get(string userId)
        {
            fileFullName = filePath + "\\" + fileName + "_" + UserInfo.Current.Uiid + "_" + userId + fileExtension;

            if (File.Exists(fileFullName))
            {
                try
                {
                    var jsonString = File.ReadAllText(fileFullName);
                    SettingConfig.Current.RecentMenu = JsonConvert.DeserializeObject<List<MenuInfo>>(jsonString);
                }
                catch (Exception ex)
                {
                    MSGBox.Show(MessageBoxType.Information, "G-MES", "최근 사용한 메뉴 리스트를 불러오는데 실패하였습니다. 최근 사용한 메뉴 리스트를 초기화합니다.");

                    Logger.Error(ex.ToString());

                    SettingConfig.Current.RecentMenu = new List<MenuInfo>();
                }
            }
            else
                SettingConfig.Current.RecentMenu = new List<MenuInfo>();

            return SettingConfig.Current;
        }

        public void Save(string userId, SettingConfig setting)
        {
            fileFullName = filePath + "\\" + fileName + "_" + UserInfo.Current.Uiid + "_" + userId + fileExtension;

            string jsonString = JsonConvert.SerializeObject(setting.RecentMenu);

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);


            File.WriteAllText(fileFullName, jsonString);
        }
    }
}
