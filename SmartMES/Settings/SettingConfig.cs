using Micube.Framework.Log;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Micube.Framework.SmartControls;

namespace SmartMES
{
    public class SettingConfig
    {
        //private static string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Micube\\SmartEES\\Setting");
        //private static string fileFullName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Micube\\SmartEES\\Setting\\") + "LoginSetting.json";

        public static SettingConfig Current { get; set; } = new SettingConfig();

        #region Property
        
        public bool IsSaveLoginId { get; set; } = false;

        public string SaveLoginId { get; set; } = "";

        public string LanguageType { get; set; } = "";

        public string PlantId { get; set; } = "";

        [Newtonsoft.Json.JsonIgnore()]
        public List<MenuInfo> RecentMenu { get; set; } = new List<MenuInfo>();

        [Newtonsoft.Json.JsonIgnore()]
        public List<MenuInfo> FavoriteMenu { get; set; } = new List<MenuInfo>();

        [Newtonsoft.Json.JsonIgnore()]
        public Dictionary<DateTime, Dictionary<string, object>> Condition { get; set; } = new Dictionary<DateTime, Dictionary<string, object>>();


        #endregion

        //public static void SaveJson()
        //{
        //    string jsonString = JsonConvert.SerializeObject(Current);

        //    if (!Directory.Exists(filePath))
        //        Directory.CreateDirectory(filePath);


        //    File.WriteAllText(fileFullName, jsonString);
        //}

        //public static void LoadJson()
        //{
        //    if (File.Exists(fileFullName))
        //    {
        //        try
        //        {
        //            var jsonString = File.ReadAllText(fileFullName);
        //            Current = JsonConvert.DeserializeObject<SettingConfig>(jsonString);
        //        }
        //        catch (Exception ex)
        //        {
        //            //MessageBox.Show("로그인 설정을 불러오는데 실패하였습니다. 로그인 설정을 초기화합니다.");
        //            MSGBox.Show(MessageBoxType.Information, "SmartEES", "로그인 설정을 불러오는데 실패하였습니다. 로그인 설정을 초기화합니다.");

        //            Logger.Error(ex.ToString());

        //            Current = new SettingConfig();
        //        }
        //    }
        //    else
        //        Current = new SettingConfig();
        //}
    }
}
