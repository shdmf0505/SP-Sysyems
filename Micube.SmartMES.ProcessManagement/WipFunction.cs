#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using Micube.Framework;
using Micube.Framework.Net;
using System.Windows.Forms;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
	/// 프 로 그 램 명  : WipCommonFunction
	/// 업  무  설  명  : 공정에서 쓰는 공통 함수 모음
	/// 생    성    자  : 박정훈
	/// 생    성    일  : 2019-10-29
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
    public static class WipFunction
    {
        #region - ShowLotMessagePopup :: Lot Message Popup 보여주기 |
        /// <summary>
        /// Lot Message Popup 보여주기
        /// </summary>
        /// <param name="lotInfo"></param>
        /// <param name="lotId"></param>
        /// <param name="productDefId"></param>
        /// <param name="productDefVersion"></param>
        /// <param name="processSegmentId"></param>
        public static void ShowLotMessagePopup(DataTable lotInfo, string lotId, string productDefId, string productDefVersion, string processSegmentId)
        {
            DataRow row = lotInfo.Rows.Cast<DataRow>().FirstOrDefault();

            DataTable lotMessageInfo = new DataTable();
            lotMessageInfo.Columns.Add("LOTID", typeof(string));
            lotMessageInfo.Columns.Add("PRODUCTDEFID", typeof(string));
            lotMessageInfo.Columns.Add("PRODUCTDEFVERSION", typeof(string));
            lotMessageInfo.Columns.Add("PRODUCTDEFNAME", typeof(string));
            lotMessageInfo.Columns.Add("PROCESSSEGMENTID", typeof(string));
            lotMessageInfo.Columns.Add("PROCESSSEGMENTVERSION", typeof(string));
            lotMessageInfo.Columns.Add("PROCESSSEGMENTNAME", typeof(string));
            lotMessageInfo.Columns.Add("USERSEQUENCE", typeof(string));

            DataRow newRow = lotMessageInfo.NewRow();

            foreach (DataColumn column in lotMessageInfo.Columns)
            {
                newRow[column.ColumnName] = row[column.ColumnName];
            }

            lotMessageInfo.Rows.Add(newRow);

            lotMessageInfo.AcceptChanges();


            //메시지 정보 조회

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LOTID", lotId);
            param.Add("PRODUCTDEFID", productDefId);
            param.Add("PRODUCTDEFVERSION", productDefVersion);
            param.Add("PROCESSSEGMENTID", processSegmentId);
            param.Add("ISREAD", "N");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("SHOWTYPE", "Y");

            DataTable messageList = SqlExecuter.Query("GetLotMessageList", "10002", param);
            if (messageList.Rows.Count > 0)
            {
                LotMessagePopupView popup = new LotMessagePopupView(lotMessageInfo);
                popup.MessageDataSource = messageList;
                popup.ShowDialog();

                DataTable readList = popup.GetChangedRows();

                MessageWorker worker = new MessageWorker("SaveLotMessageRead");
                worker.SetBody(new MessageBody()
                            {
                                { "LotId", lotId },
                                { "ReadMessageList", readList }
                            });

                worker.Execute();
            }
        }

        /// <summary>
        /// Window Time 이 도래한 해당 작업장의 Lot을 팝업에 표시
        /// </summary>
        /// <param name="areaId">작업장</param>
        public static DialogResult ShowWindowTimeArrivedPopup(string areaId)
        {
            DataTable wtimeLots = new SqlQuery("SelectArrivedWindowTimeLotListOfArea", "10001"
                , $"AREAID={areaId}"
                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                ).Execute();
            if(wtimeLots.Rows.Count > 0)
            {
                WindowTimeArrivedPopup popup = new WindowTimeArrivedPopup(wtimeLots);
                return popup.ShowDialog();
            }
            else
            {
                return DialogResult.Cancel;
            }
        }
        #endregion
    }
}
