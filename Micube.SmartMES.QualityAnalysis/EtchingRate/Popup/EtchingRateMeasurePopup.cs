#region using
using Micube.Framework;
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

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질 > 레칭레이트 > 에칭레이트 측정값 등록
    /// 업  무  설  명  : 에칭레이트 측정값 등록 화면의 그리드를 더블 클릭하여 측정값을 관리한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-07-17
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class EtchingRateMeasurePopup : SmartPopupBaseForm, ISmartCustomPopup
    {

        #region Interface

        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region Local Variables
        string _result = "";
        #endregion

        #region 생성자
        public EtchingRateMeasurePopup()
        {
            InitializeComponent();
            InitializeGrid();
            InitializeEvent();
        }
        #endregion

        #region 컨텐츠 영역 초기화
        private void InitializeGrid()
        {
            grdSpec.GridButtonItem = GridButtonItem.None;
            grdSpec.View.SetIsReadOnly();

            grdSpec.View.AddTextBoxColumn("MEASUREDATE", 150);

            grdSpec.View.AddTextBoxColumn("PLANTID", 150)
                .SetLabel("PLANT");

            grdSpec.View.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();

            grdSpec.View.AddTextBoxColumn("AREANAME", 150)
               .SetIsHidden();

            grdSpec.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdSpec.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 150)
                .SetLabel("TOPPROCESSSEGMENTCLASS");

            grdSpec.View.AddTextBoxColumn("EQUIPMENTNAME", 150)
                .SetLabel("EQUIPMENT");

            grdSpec.View.AddTextBoxColumn("CHILDEQUIPMENTNAME", 150)
                .SetLabel("CHILDEQUIPMENT");

            grdSpec.View.AddTextBoxColumn("WORKTYPE", 150)
                .SetLabel("TYPECONDITION");

            grdSpec.View.AddTextBoxColumn("SPECRANGE", 150);

            grdSpec.View.AddTextBoxColumn("CONTROLRANGE", 150);

            grdSpec.View.PopulateColumns();

        }
        #endregion

        #region Event
        private void InitializeEvent()
        {

            Load += (s, e) =>
            {
                numAfter.Properties.MinValue = 0;
                numBefore.Properties.MinValue = 0;
                numEqpVelocity.Properties.MinValue = 0;

                numAfter.Properties.MaxValue = decimal.MaxValue;
                numBefore.Properties.MaxValue = decimal.MaxValue;
                numEqpVelocity.Properties.MaxValue = decimal.MaxValue;

                numAfter.Properties.EditMask = "#,###,###.##";
                numBefore.Properties.EditMask = "#,###,###.##";
                numEqpVelocity.Properties.EditMask = "#,###,###.#";
                txtArea.Enabled = false;
            };

            btnCancel.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            //OK버튼 클릭 이벤트
            btnOK.Click += BtnOK_Click;
  
            //에칭 전, 후 값이 바뀔 때 이벤트
            numBefore.EditValueChanged += Num_EditValueChanged;
            numAfter.EditValueChanged += Num_EditValueChanged;


        }

        /// <summary>
        /// OK버튼 클릭 이벤트 
        /// _result 값이 ""인지 확인 
        /// 입력값 CurrentRow에 담기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_result))
            {
                ShowMessage("NeedToInputEtchingValue");//측정값을 입력해야합니다.
                return;
            }

            this.DialogResult = DialogResult.OK;

            DataRow row = grdSpec.View.GetFocusedDataRow();
            CurrentDataRow["MEASUREDATE"] = row["MEASUREDATE"];
            CurrentDataRow["ENTERPRISEID"] = row["ENTERPRISEID"];
            CurrentDataRow["PLANTID"] = row["PLANTID"];
            CurrentDataRow["EQPTSPEED"] = numEqpVelocity.EditValue;
            CurrentDataRow["MEASURER"] = txtMeasurer.EditValue;
            CurrentDataRow["ETCHRATEVALUE"] = txtValue.EditValue;
            CurrentDataRow["ETCHRATEBEFOREVALUE"] = numBefore.EditValue;
            CurrentDataRow["ETCHRATEAFTERVALUE"] = numAfter.EditValue;
            CurrentDataRow["RESULT"] = _result;
        }

        /// <summary>
        /// 에칭후 값이 바뀔때 에칭레이트 값과 판정결과를 판정해주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Num_EditValueChanged(object sender, EventArgs e)
        {
            //에칭전, 에칭 후 값이 0일 때 return
            if (numBefore.EditValue.ToString().Equals("0") || numAfter.EditValue.ToString().Equals("0"))
                return;

            Double value = ((numBefore.EditValue.ToSafeDoubleNaN() - numAfter.EditValue.ToSafeDoubleNaN()) * 10000 / (10 * 10 * 2 * 8.93).ToSafeDoubleNaN()).ToSafeDoubleNaN();

            //확인후 수정 필요
            if (CurrentDataRow["LSL"].ToSafeDoubleNaN() <= value && value <= CurrentDataRow["USL"].ToSafeDoubleNaN())
            {
                _result = "OK";
            }
            else
            {
                _result = "NG";
            }

            txtValue.EditValue = value;
        }

        #endregion

        #region Private Function
        #endregion

        #region Public Function
        /// <summary>
        /// 팝업을 열때 팝업에(상단 그리드) 데이터를 세팅 해주는 함수
        /// </summary>
        public void AddNewRow()
        {
            DataTable dt = grdSpec.DataSource as DataTable;
            DataRow row = dt.NewRow();

            row["MEASUREDATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            row["PLANTID"] = Framework.UserInfo.Current.Plant;
            row["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
            row["PROCESSSEGMENTCLASSNAME"] = CurrentDataRow["PROCESSSEGMENTCLASSNAME"];
            row["EQUIPMENTNAME"] = CurrentDataRow["EQUIPMENTNAME"];
            row["CHILDEQUIPMENTNAME"] = CurrentDataRow["CHILDEQUIPMENTNAME"];
            row["WORKTYPE"] = CurrentDataRow["WORKTYPE"];
            row["SPECRANGE"] = CurrentDataRow["SPECRANGE"];
            row["CONTROLRANGE"] = CurrentDataRow["CONTROLRANGE"];
            row["AREAID"] = CurrentDataRow["AREAID"];
            row["AREANAME"] = CurrentDataRow["AREANAME"];
            txtArea.EditValue = CurrentDataRow["AREANAME"];
            dt.Rows.Add(row);
        }

        /// <summary>
        /// 컨트롤에 데이터를 바인딩 해주는 함수
        /// 파라미터로 컨트롤 Enable 관리
        /// 저장된 row 수정불가
        /// </summary>
        /// <param name="isEnable"></param>
        public void UpdateMeasure(Boolean isEnable)
        {
            DataTable dt = grdSpec.DataSource as DataTable;
            DataRow row = dt.NewRow();

            row["MEASUREDATE"] = CurrentDataRow["MEASUREDATE"];
            row["PLANTID"] = CurrentDataRow["PLANTID"];
            row["ENTERPRISEID"] = CurrentDataRow["ENTERPRISEID"];
            row["PROCESSSEGMENTCLASSNAME"] = CurrentDataRow["PROCESSSEGMENTCLASSNAME"];
            row["EQUIPMENTNAME"] = CurrentDataRow["EQUIPMENTNAME"];
            row["CHILDEQUIPMENTNAME"] = CurrentDataRow["CHILDEQUIPMENTNAME"];
            row["WORKTYPE"] = CurrentDataRow["WORKTYPE"];
            row["SPECRANGE"] = CurrentDataRow["SPECRANGE"];
            row["CONTROLRANGE"] = CurrentDataRow["CONTROLRANGE"];
            row["AREAID"] = CurrentDataRow["AREAID"];
            row["AREANAME"] = CurrentDataRow["AREANAME"];
            dt.Rows.Add(row);

            txtCycle.EditValue = CurrentDataRow["MEASUREDEGREE"];
            numEqpVelocity.EditValue = CurrentDataRow["EQPTSPEED"];
            txtMeasurer.EditValue = CurrentDataRow["MEASURER"];
            numBefore.EditValue = CurrentDataRow["ETCHRATEBEFOREVALUE"];
            numAfter.EditValue = CurrentDataRow["ETCHRATEAFTERVALUE"];
            txtValue.EditValue = CurrentDataRow["ETCHRATEVALUE"];
            txtArea.EditValue = CurrentDataRow["AREANAME"];

            if (isEnable == false)
            {
                txtCycle.Enabled = false;
                numEqpVelocity.Enabled = false;
                txtMeasurer.Enabled = false;
                numBefore.Enabled = false;
                numAfter.Enabled = false;
                txtValue.Enabled = false;               
            }
        }
        #endregion


    }
}
