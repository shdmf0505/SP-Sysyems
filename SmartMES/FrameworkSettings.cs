using Micube.Framework;
using Micube.Framework.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Micube.Framework.SmartControls;

namespace SmartMES
{
    public static class FrameworkSettings
    {
        private static Dictionary<string, IConvertible> ServiceList;

        public static void Initialize(Dictionary<string, IConvertible> serviceList)
        {
            ServiceList = serviceList;

            InitializeMessage();
            InitializeResource();
            InitializeLanguage();
            //ExceptionMonitor.Initialize();
        }

        private static void InitializeMessage()
        {
            NetworkSettings.Default.MessageSettings += (msg) =>
            {
                msg.Transaction.LanguageType = UserInfo.Current.LanguageType;
                msg.Transaction.User = UserInfo.Current.Id;
                msg.Transaction.Uiid = UserInfo.Current.Uiid;
                msg.Transaction.ClientTimeZoneID = string.IsNullOrEmpty(UserInfo.Current.TimeZone) ? "Asia/Seoul" : UserInfo.Current.TimeZone;
            };
        }

        private static void InitializeResource()
        {
            ResourceCollector.CollectResource();
        }

        private static void InitializeLanguage()
        {
            Language.ChangeLanguage(UserInfo.Current.LanguageType);

            DataTable language = new DataTable();
            DataTable message = new DataTable();

            foreach (string serviceId in ServiceList.Keys)
            {
                NetworkSettings.SetServiceUrl(serviceId);

                language.Merge(SqlExecuter.Query("GetDictionaryList", "00001", new Dictionary<string, object>()
                {
                    { "LANGUAGETYPE", Language.LanguageType },
                    { "VALIDSTATE", "Valid" }
                }));

                message.Merge(SqlExecuter.Query("GetMessageList", "00001", new Dictionary<string, object>()
                {
                    { "LANGUAGETYPE", Language.LanguageType },
                    { "VALIDSTATE", "Valid" }
                }));

                if (UserInfo.Current.IsUseToolTipLanguage && Language.LanguageType != UserInfo.Current.ToolTipLanguageType)
                {
                    language.Merge(SqlExecuter.Query("GetDictionaryList", "00001", new Dictionary<string, object>()
                    {
                        { "LANGUAGETYPE", UserInfo.Current.ToolTipLanguageType },
                        { "VALIDSTATE", "Valid" }
                    }));

                    message.Merge(SqlExecuter.Query("GetMessageList", "00001", new Dictionary<string, object>()
                    {
                        { "LANGUAGETYPE", UserInfo.Current.ToolTipLanguageType },
                        { "VALIDSTATE", "Valid" }
                    }));
                }
            }

            Language.Dictionary.InitializeMultiService(language.AsEnumerable()
                .Select(r => new Language.LanguageItem
                {
                    ServiceId = r["SERVICEID"].ToString().ToUpper(),
                    ItemId = r["DICTIONARYID"].ToString().ToUpper(),
                    Name = r["DICTIONARYNAME"].ToString(),
                    LanguageType = r["LANGUAGETYPE"].ToString(),
                    Description = r.ToStringNullToEmpty("DESCRIPTION"),
                })
                .ToList());

            Language.Message.InitializeMultiService(message.AsEnumerable()
                .Select(r => new Language.LanguageMessageItem
                {
                    ServiceId = r["SERVICEID"].ToString().ToUpper(),
                    ItemId = r["MESSAGEID"].ToString().ToUpper(),
                    Message = r["MESSAGE"].ToString(),
                    Title = r["TITLE"].ToString(),
                    LanguageType = r["LANGUAGETYPE"].ToString(),
                })
                .ToList());

            NetworkSettings.SetServiceUrl(UserInfo.Current.DefaultService);

            var languageType = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>()
            {
                { "CODECLASSID", "LanguageType" },
                { "LANGUAGETYPE", Language.LanguageType }
            });

            Language.LanguageTypes.AddRange(
                languageType.AsEnumerable()
                .Select((r, index) => new LanguageType
                {
                    DictionaryId = r["CODEID"].ToString().ToUpper(),
                    Sequence = index + 1
                })
                .OrderBy(e => e.Sequence)
                .ToList());
        }
    }
}
