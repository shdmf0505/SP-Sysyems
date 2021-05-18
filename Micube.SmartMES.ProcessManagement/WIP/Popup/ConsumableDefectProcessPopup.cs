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
    /// 프 로 그 램 명  : 공정 관리 > 공정작업 > 작업 완료 > 자재 불량 처리
    /// 업  무  설  명  : 작업 완료에서 자재 불량 처리하는 팝업
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-08-07
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ConsumableDefectProcessPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region ISmartCustomPopup

        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region Local Variables

        private string _consumableLotId;    // 자재 LOT
        private decimal _currentQty;            // 현재 수량
        private DataTable _consumableDefectList;    // 불량 내역
        private decimal _defectQty;             // 불량 수량

        #endregion

        #region 생성자

        public ConsumableDefectProcessPopup()
        {
            InitializeComponent();
        }

        public ConsumableDefectProcessPopup(string consumableLotId, decimal currentQty, DataTable dataSource)
        {
            InitializeComponent();

            InitializeEvent();

            _consumableLotId = consumableLotId;
            _currentQty = currentQty;
            _consumableDefectList = dataSource;
        }

        #endregion

        #region 컨텐츠 영역 초기화

        private void InitializeControls()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("REASONCODECLASSID", "ConsumableScrapCode");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            cboReasonCode.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboReasonCode.ShowHeader = false;
            cboReasonCode.DisplayMember = "REASONCODENAME";
            cboReasonCode.ValueMember = "REASONCODEID";
            cboReasonCode.UseEmptyItem = true;
            cboReasonCode.EmptyItemValue = "";
            cboReasonCode.EmptyItemCaption = "";
            cboReasonCode.DataSource = SqlExecuter.Query("GetReasonCodeByProcess", "10001", param);
        }

        #endregion

        #region Event

        private void InitializeEvent()
        {
            Shown += ConsumableDefectProcessPopup_Shown;

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void ConsumableDefectProcessPopup_Shown(object sender, EventArgs e)
        {
            InitializeControls();

            txtConsumableLotId.Text = _consumableLotId;
            numCurrentQty.Value = _currentQty;

            DataRow[] rows = _consumableDefectList.Select("CONSUMABLELOTID = '" + txtConsumableLotId.Text + "'");

            if (rows.Count() > 0)
            {
                DataRow row = rows[0];

                numDefectQty.Value = Format.GetDecimal(row["DEFECTQTY"]);
                cboReasonCode.EditValue = Format.GetString(row["REASONCODE"]);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            DataRow[] rows = _consumableDefectList.Select("CONSUMABLELOTID = '" + txtConsumableLotId.Text + "'");

            if (rows.Count() > 0)
            {
                DataRow row = rows[0];

                row["DEFECTQTY"] = numDefectQty.Value;
                row["REASONCODE"] = cboReasonCode.EditValue;
            }
            else
            {
                DataRow row = _consumableDefectList.NewRow();

                row["CONSUMABLELOTID"] = txtConsumableLotId.Text;
                row["DEFECTQTY"] = numDefectQty.Value;
                row["REASONCODE"] = cboReasonCode.EditValue;

                _consumableDefectList.Rows.Add(row);
            }

            _consumableDefectList.AcceptChanges();

            _defectQty = Format.GetDecimal(numDefectQty.Value);

            DialogResult = DialogResult.OK;

            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Public Function

        public DataTable GetConsumableDefectList()
        {
            return _consumableDefectList;
        }

        public decimal GetDefectQty()
        {
            return _defectQty;
        }

        #endregion
    }
}