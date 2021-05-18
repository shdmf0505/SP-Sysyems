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
    public partial class RollLotConsumableLotListPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        private string _areaId;
        private string _consumableDefId;
        private string _consumableDefVersion;

        public RollLotConsumableLotListPopup()
        {
            InitializeComponent();
        }

        public RollLotConsumableLotListPopup(string areaId, string consumableDefId, string consumableDefVersion)
        {
            InitializeComponent();

            _areaId = areaId;
            _consumableDefId = consumableDefId;
            _consumableDefVersion = consumableDefVersion;

            InitializeControls();
            InitializeEvent();
        }

        private void InitializeControls()
        {
            grdConsumableLotList.GridButtonItem = GridButtonItem.None;

            grdConsumableLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdConsumableLotList.View.SetIsReadOnly();

            // 자재 Lot
            grdConsumableLotList.View.AddTextBoxColumn("CONSUMABLELOTID", 170);
            // 입고일자
            grdConsumableLotList.View.AddTextBoxColumn("STOCKDATE", 120)
                .SetLabel("INBOUNDTIME")
                .SetTextAlignment(TextAlignment.Center);
            // 자재 ID
            grdConsumableLotList.View.AddTextBoxColumn("CONSUMABLEDEFID", 100);
            // 자재버전
            grdConsumableLotList.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 70);
            // 자재명
            grdConsumableLotList.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
            // 자재수량
            grdConsumableLotList.View.AddSpinEditColumn("CONSUMABLELOTQTY", 90)
                .SetDisplayFormat("#,##0.##");
            // 보류여부
            grdConsumableLotList.View.AddTextBoxColumn("ISHOLD", 80)
                .SetTextAlignment(TextAlignment.Center);


            grdConsumableLotList.View.PopulateColumns();
        }

        private void InitializeEvent()
        {
            Shown += RollLotConsumableLotListPopup_Shown;

            btnClose.Click += BtnClose_Click;
        }

        private void RollLotConsumableLotListPopup_Shown(object sender, EventArgs e)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("AREAID", _areaId);
            param.Add("CONSUMABLEDEFID", _consumableDefId);
            param.Add("CONSUMABLEDEFVERSION", _consumableDefVersion);

            grdConsumableLotList.DataSource = SqlExecuter.Query("GetConsumableLotListForRollLot", "10001", param);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public DataRow CurrentDataRow { get; set; }
    }
}
