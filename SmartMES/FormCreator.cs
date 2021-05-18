using DevExpress.XtraEditors;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Newtonsoft.Json;
using SmartMES.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMES
{
    public class FormCreator
    {
        public static Form CreateForm(string menuId, string menuName, string programId, Dictionary<string,object> parameters)
        {
            return CreateFormCore(menuId, menuName, programId, parameters, UserInfo.Current.Uiid, null, "N", "N");
        }

        // 2019.09.30 hykang - Service Id를 파라미터로 넘겨받는 경우 처리
        // 2019.10.09 hykang - Parent Menu Id 파라미터 추가
        // 2019.11.11 hykang - Site 사용자 권한 사용 여부 파라미터 추가
        // 2019.12.10 hykang - Site 단일 사용 여부 파라미터 추가
        public static Form CreateForm(string menuId, string menuName, string programId, Dictionary<string, object> parameters, string serviceId, string parentMenuId, string isUsePlantAuthority, string isUsePlantSingle)
        {
            return CreateFormCore(menuId, menuName, programId, parameters, serviceId, parentMenuId, isUsePlantAuthority, isUsePlantSingle);
        }

        // 2019.09.30 hykang - Service Id 파라미터 추가
        // 2019.10.09 hykang - Parent Menu Id 파라미터 추가
        // 2019.11.11 hykang - Site 사용자 권한 사용 여부 파라미터 추가
        // 2019.12.10 hykang - Site 단일 사용 여부 파라미터 추가
        private static Form CreateFormCore(string menuId, string menuName, string programId, Dictionary<string, object> parameters, string serviceId, string parentMenuId, string isUsePlantAuthority, string isUsePlantSingle)
        {
            var formType = GetFormType(programId);
            
            Form form = null;

            form = Activator.CreateInstance(formType) as Form;
            form.Tag = serviceId;

            if (form is SmartBaseForm smartBaseForm)
            {
                NetworkSettings.SetServiceUrl(UserInfo.Current.DefaultService);

                MessageWorker worker = new MessageWorker("SaveConnectionHistory");

                worker.SetBody(new MessageBody()
                {
                    { "UserId", UserInfo.Current.Id },
                    { "ConnectionType", "Menu_Open" },
                    { "Uiid", UserInfo.Current.Uiid },
                    { "MenuId", menuId }
                });

                var saveResult = worker.Execute<DataTable>();
                DataTable resultData = saveResult.GetResultSet();

                NetworkSettings.SetServiceUrl(serviceId);

                smartBaseForm.MenuId = menuId;
                smartBaseForm.ParentMenuId = parentMenuId;
                smartBaseForm.IsUsePlantAuthority = isUsePlantAuthority;
                smartBaseForm.IsUsePlantSingle = isUsePlantSingle;
                smartBaseForm.LanguageKey = "Menu_" + menuId;

                smartBaseForm.ConnectionKey = Format.GetString(resultData.Rows[0]["TXNHISTKEY"]);
            }

            if (form is SmartConditionBaseForm smartConditionBaseForm)
            {
                var toolbarData = GetToolbarInfo(UserInfo.Current.Id, menuId);

                smartConditionBaseForm.Toolbars = toolbarData.AsEnumerable()
                    .OrderBy(row => row.ToTypeNullToEmpty<decimal>("DISPLAYSEQUENCE"))
                    .Select(row =>
                {
                    int toolbarWidth = 75;

                    dynamic options = JsonConvert.DeserializeObject(row["OPTIONS"].ToString());
                    //if (options == null) return null;
                    if (options != null && options.Width != null)
                    {
                        toolbarWidth = Format.GetInteger(options.Width);
                    }

                    return new ToolbarItem()
                    {
                        Id = row["TOOLBARID"].ToString(),
                        TextLanguageKey = "Toolbar_" + row.ToStringNullToEmpty("TOOLBARID"),
                        //Image = ResourceCollector.GetImage(options.Image.ToString()),
                        ToolTipLanguageKey = row.ToStringNullToEmpty("MESSAGEID"),

                        //smjang - ToolBar에 쓰기권한 부여 
                        IsWrite = row.ToStringNullToEmpty("ISWRITE").Equals("Y"),
                        // hykang - Toolbar Width 변경
                        Width = toolbarWidth
                    };
                })
                .Where(e => e != null).ToList();

                smartConditionBaseForm.LoadForm(parameters);
            }

            form.Name = menuId;
            form.Text = menuName;

            return form;
        }

        private static Type GetFormType(string programId)
        {
            if (string.IsNullOrWhiteSpace(programId))
                throw new Exception($"Invalid Program Id : {programId}");

            // ex) Micube.Test.ManagementTool.TestForm -> Micube.Test.ManagementTool.dll
            var lastDotIndex = programId.LastIndexOf(".");
            var dllDirectory = string.IsNullOrEmpty(ConfigurationManager.AppSettings["dllDirectory"]) ? string.Empty : "\\" + ConfigurationManager.AppSettings["dllDirectory"];
            var dllFileName = programId.Substring(0, lastDotIndex) + ".dll";
            var dllFileFullName = Path.Combine(Application.StartupPath + dllDirectory, dllFileName);
            if (!File.Exists(dllFileFullName))
                throw new Exception($"File Not Found : {dllFileFullName}");

            var asm = Assembly.LoadFrom(dllFileFullName);
            var formType = asm.GetType(programId);
            if (formType == null)
                throw new Exception($"Menu Not Found : {programId}");

            return formType;
        }

        private static DataTable GetToolbarInfo(string userId, string menuId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "USERID", userId },
                { "MENUID", menuId },
                { "VALIDSTATE", "Valid" },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "UIID", UserInfo.Current.Uiid }
            };

            //smjang - 쿼리 추가 변경 
            //return SqlExecuter.Query("GetMenuObjectList", "00001", param);
            return SqlExecuter.Query("GetMenuObjectList", "00002", param);
        }
    }
}
