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
	public partial class DetailLogicStatePopup : SmartPopupBaseForm, ISmartCustomPopup
	{
		public DataRow CurrentDataRow { get; set; }


        #region 생성자
		public DetailLogicStatePopup()
		{
            InitializeComponent();

        }
        public DetailLogicStatePopup(string productdefid,string productdefversion,string plantid)
        {
            InitializeComponent();
            InitializeGrid();
            btnClose.Click += BtnClose_Click;
            SearchGrid(productdefid, productdefversion, plantid);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void InitializeGrid()
        {
            #region - 재공 Grid 설정 
            grdLogicState.GridButtonItem = GridButtonItem.None;

            grdLogicState.View.SetIsReadOnly();
            grdLogicState.SetIsUseContextMenu(false);
            // CheckBox 설정


            grdLogicState.View.AddTextBoxColumn("PLANTID", 50).SetTextAlignment(TextAlignment.Center);
            grdLogicState.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 100).SetTextAlignment(TextAlignment.Left);
            //grdLogicState.View.AddTextBoxColumn("CLASS", 70).SetTextAlignment(TextAlignment.Center);
            grdLogicState.View.AddComboBoxColumn("CLASS", 70, new SqlQuery("GetCodeList", "00001", "CODECLASSID=LogisticMoveState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                            .SetTextAlignment(TextAlignment.Center);
                            
            grdLogicState.View.AddTextBoxColumn("STATE", 70).SetTextAlignment(TextAlignment.Center);
            grdLogicState.View.AddTextBoxColumn("PCS", 50).SetTextAlignment(TextAlignment.Right);
            grdLogicState.View.AddTextBoxColumn("PNL", 50).SetTextAlignment(TextAlignment.Right);
            grdLogicState.View.AddTextBoxColumn("MM", 50).SetTextAlignment(TextAlignment.Right);
            grdLogicState.View.AddTextBoxColumn("CURRENCY", 70).SetTextAlignment(TextAlignment.Center);
            grdLogicState.View.AddTextBoxColumn("WIPPRICE", 100).SetTextAlignment(TextAlignment.Right);

            grdLogicState.View.PopulateColumns();

            #endregion

        }

        private void SearchGrid(string productdefid,string productdefversion,string plantid)
        {

            //Apply Lot List 초기화
            grdLogicState.View.ClearDatas();

            Dictionary<string, object> param = new Dictionary<string, object>();


            param.Add("P_PRODUCTDEFID", productdefid);
            param.Add("P_PRODUCTDEFVERSION", productdefversion);
            param.Add("P_PLANTID", plantid);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            
            DataTable dtLotList = SqlExecuter.Query("selectLotStatusDetailLogicState", "10001", param);
            if (dtLotList.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
            }

            grdLogicState.DataSource = dtLotList;

        }

        #region Event
        private void InitializeEvent()
		{
		
		}

		#endregion
	}
}
