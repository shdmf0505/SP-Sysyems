using Micube.Framework;
using Micube.Framework.SmartControls.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Settings
{
    public class ConditionSettingRepository : ISettingRepository, IConditionRepository
    {
        private readonly string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Micube\\SmartMES\\Setting");
        private readonly string fileName = "ConditionSetting";
        private readonly string fileExtension = ".json";
        private string fileFullName = "";
        
        public Dictionary<DateTime, Dictionary<string, object>> LoadConditionList(string userId, string menuId)
        {
            fileFullName = filePath + "\\" + fileName + "_" + UserInfo.Current.Uiid + "_" + userId + "_" + menuId + fileExtension;

            if (File.Exists(fileFullName))
            {
                var jsonString = File.ReadAllText(fileFullName);
                SettingConfig.Current.Condition = JsonConvert.DeserializeObject<Dictionary<DateTime, Dictionary<string, object>>>(jsonString);
            }
            else
                SettingConfig.Current.Condition = new Dictionary<DateTime, Dictionary<string, object>>();


            return SettingConfig.Current.Condition;
        }

        public void SaveCondition(string userId, string menuId, Dictionary<string, object> condition)
        {
            DateTime ConditionKey = DateTime.Now;

            fileFullName = filePath + "\\" + fileName + "_" + UserInfo.Current.Uiid + "_" + userId + "_" + menuId + fileExtension;

            //Dictionary<string, Dictionary<string, Dictionary<string, object>>> dicMenuList = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>();
            //Dictionary<string, Dictionary<string, object>> dicConditionKeyList = new Dictionary<string, Dictionary<string, object>>();
            //Dictionary<string, object> dicConditionList = new Dictionary<string, object>();

            Dictionary<DateTime, Dictionary<string, object>> conditionList = SettingConfig.Current.Condition;

            if (conditionList.ContainsKey(ConditionKey))
            {
                conditionList[ConditionKey] = condition;
            }
            else
            {
                conditionList.Add(ConditionKey, condition);

                int count = ApplicationConfig.Current.GetSaveConditionCount();

                if (conditionList.Count > count)
                {
                    conditionList.Remove(conditionList.Keys.Min());
                }
            }

            conditionList.OrderBy(key => key);

            SettingConfig.Current.Condition = conditionList;

            string jsonString = JsonConvert.SerializeObject(conditionList);

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);


            File.WriteAllText(fileFullName, jsonString);

            //if (SettingConfig.Current.SearchCondition.ContainsKey(userId))
            //{
            //    dicMenuList = SettingConfig.Current.SearchCondition[userId];

            //    if (dicMenuList.ContainsKey(menuId))
            //    {
            //        dicConditionKeyList = dicMenuList[menuId];

            //        if (dicConditionKeyList.ContainsKey(ConditionKey))
            //        {
            //            dicConditionKeyList[ConditionKey] = condition;
            //        }
            //        else
            //        {
            //            dicConditionList = condition;

            //            if (dicConditionKeyList.Count > 10)
            //            {
            //                dicConditionKeyList.Remove(dicConditionKeyList.First().Key);
            //            }

            //            dicConditionKeyList.Add(ConditionKey, dicConditionList);
            //        }
            //    }
            //    else
            //    {
            //        dicConditionList = condition;
            //        dicConditionKeyList.Add(ConditionKey, dicConditionList);
            //        dicMenuList.Add(menuId, dicConditionKeyList);
            //    }
            //}
            //else
            //{
            //    dicConditionList = condition;
            //    dicConditionKeyList.Add(ConditionKey, dicConditionList);
            //    dicMenuList.Add(menuId, dicConditionKeyList);
            //    SettingConfig.Current.SearchCondition.Add(userId, dicMenuList);
            //}
        }

        public SettingConfig Get(string userId)
        {
            throw new NotImplementedException();
        }

        public void Save(string userId, SettingConfig setting)
        {
            throw new NotImplementedException();
        }
    }
}
