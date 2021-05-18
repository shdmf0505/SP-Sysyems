#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
	/// <summary>
	/// 프 로 그 램 명  : 공정관리 > 재공조회 > 모델별 재공 조회
	/// 업  무  설  명  : 
	/// 생    성    자  : 배선용
	/// 생    성    일  : 2019-08-19
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class LotLeadTimePopup : SmartPopupBaseForm, ISmartCustomPopup
	{
		public DataRow CurrentDataRow { get; set; }


        #region 생성자
		public LotLeadTimePopup(string processegment, string field, Dictionary<string, object> values)
        {
            InitializeComponent();
            InitializeGrid(processegment, field, values);

        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void InitializeGrid(string processegment, string field, Dictionary<string, object> values)
        {
            #region - 재공 Grid 설정 

            grdproduct.View.ClearDatas();

            Dictionary<string, object> param = new Dictionary<string, object>();

            values.Add("PROCESSSEGMENTNAME", processegment);
            values.Add("MONTH", field);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            grdproduct.GridButtonItem = GridButtonItem.None;
            grdproduct.GridButtonItem = GridButtonItem.Export;
            grdproduct.View.SetIsReadOnly();

            grdproduct.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            grdproduct.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            grdproduct.View.AddTextBoxColumn("LEADTIME", 100).SetTextAlignment(TextAlignment.Center);
            grdproduct.View.AddTextBoxColumn("DELAYREASON", 200).SetTextAlignment(TextAlignment.Left);

            grdproduct.View.PopulateColumns();


            DataTable dtProductList = SqlExecuter.Query("GetProductLeadTime", "10001", values);
            if (dtProductList.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
            }

            grdproduct.DataSource = dtProductList;

            #endregion

        }



        #region Event
        private void InitializeEvent()
		{
		
		}

		#endregion
	}
}
