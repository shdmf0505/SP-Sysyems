using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Settings
{
    public class FavoriteSettingRepository : ISettingRepository, IFavoriteRepository
    {
        public void AddFavorite(string userId, string menuId)
        {
            DataTable saveData = new DataTable();
            saveData.Columns.Add("USERID", typeof(string));
            saveData.Columns.Add("MENUID", typeof(string));
            saveData.Columns.Add("REGTYPE", typeof(string));
            saveData.Columns.Add("TXNHISTKEY", typeof(string));
            saveData.Columns.Add("DISPLAYSEQUENCE", typeof(int));
            saveData.Columns.Add("_STATE_", typeof(string));

            saveData.Rows.Add(userId, menuId, "Favorite", "1", 0, "added");

            MessageWorker worker = new MessageWorker("SaveFavoriteMenu");
            worker.SetBody(new MessageBody()
            {
                { "list", saveData }
            });

            worker.Execute();

            MSGBox.Show(MessageBoxType.Information, "SuccessAddFavorite");
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
