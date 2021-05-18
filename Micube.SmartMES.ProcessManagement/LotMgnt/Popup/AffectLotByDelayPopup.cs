using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.Framework.SmartControls;
using Micube.Framework.Net;
using Micube.Framework;

namespace Micube.SmartMES.ProcessManagement
{
	/// <summary>
	/// 프 로 그 램 명  : 공정관리 > 재공관리 > 체공 LOT 조회 > 체공사유 팝업
	/// 업  무  설  명  : 체공사유 입력 후 이상발생 등록
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-10-25
	/// 수  정  이  력  : TODO :: 체공 사유 코드, 체공 이상발생 등록 
	/// 
	/// 
	/// </summary>
	public partial class AffectLotByDelayPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
		public DataRow CurrentDataRow { get; set; }
        public int page { get; set; }
		#region 생성자
		public AffectLotByDelayPopup()
		{
			InitializeComponent();
		}

		public AffectLotByDelayPopup(DataRow dr, int page)
		{
			InitializeComponent();

			InitializeEvent();
            CurrentDataRow = dr;
            this.page = page;

            txtLotId.Text = Format.GetFullTrimString(CurrentDataRow["LOTID"]);

        }
		#endregion

		#region 이벤트
		private void InitializeEvent()
		{
			this.Load += AffectLotByDelayPopup_Load;
			btnSave.Click += BtnSave_Click;
			btnCancel.Click += BtnCancel_Click;
		}

		

		/// <summary>
		/// 닫기
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			Close();
		}

		/// <summary>
		/// 체공 이상발생 등록
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSave_Click(object sender, EventArgs e)
		{
            DataTable tbl = new DataTable();

            if (page == 1)
            { 
         
            tbl.Columns.Add("PROCESSDEFID", typeof(string));
            tbl.Columns.Add("PROCESSDEFVERSION", typeof(string));
            tbl.Columns.Add("PROCESSSEGMENTID", typeof(string));
            tbl.Columns.Add("PROCESSSEGMENTVERSION", typeof(string));
            tbl.Columns.Add("PROCESSSTATE", typeof(string));
            tbl.Columns.Add("PRODUCTDEFID", typeof(string));
            tbl.Columns.Add("PRODUCTDEFVERSION", typeof(string));
            tbl.Columns.Add("WORKCOUNT", typeof(string));
            DataRow dr = tbl.NewRow();

            dr["PROCESSDEFID"] = Format.GetTrimString(CurrentDataRow["PROCESSDEFID"]);
            dr["PROCESSDEFVERSION"] = Format.GetTrimString(CurrentDataRow["PROCESSDEFVERSION"]);
            dr["PROCESSSEGMENTID"] = Format.GetTrimString(CurrentDataRow["PROCESSSEGMENTID"]);
            dr["PROCESSSEGMENTVERSION"] = Format.GetTrimString(CurrentDataRow["PROCESSSEGMENTVERSION"]);
            dr["PROCESSSTATE"] = Format.GetTrimString(CurrentDataRow["PROCESSSTATE"]);
            dr["PRODUCTDEFID"] = Format.GetTrimString(CurrentDataRow["PRODUCTDEFID"]);
            dr["PRODUCTDEFVERSION"] = Format.GetTrimString(CurrentDataRow["PRODUCTDEFVERSION"]);
            dr["WORKCOUNT"] = Format.GetTrimString(CurrentDataRow["WORKCOUNT"]);
            tbl.Rows.Add(dr);
            }
            SaveValidation();
			MessageWorker worker = new MessageWorker("AbnormalOccurenceByDelayLot");
            worker.SetBody(new MessageBody(){
                { "enterpriseId", Framework.UserInfo.Current.Enterprise },
                { "plantId", Framework.UserInfo.Current.Plant },
                { "userId", Framework.UserInfo.Current.Id },
                { "lotId", txtLotId.Text },
                { "delayCode", cboDelayCode.GetDataValue() },
                { "delayComment", txtComment.EditValue },
                { "checkTime", Format.GetTrimString(CurrentDataRow["CHECKTIME"])},
                { "list2", tbl },
                { "page", page }

            });

			worker.Execute();

			this.DialogResult = DialogResult.OK;


			ShowMessage("SuccedSave");
		}

		/// <summary>
		/// Load 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AffectLotByDelayPopup_Load(object sender, EventArgs e)
		{
			InitalizeDelayCombo();
		}
		#endregion

		#region 초기화

		/// <summary>
		/// 체공 사유 combo 초기화
		/// </summary>
		private void InitalizeDelayCombo()
		{
			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("REASONCODECLASSID", "QCLockInWaitingTime");
			param.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            param.Add("VALIDSTATE", "Valid");
            cboDelayCode.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
			cboDelayCode.ShowHeader = false;
			cboDelayCode.DisplayMember = "REASONCODENAME";
			cboDelayCode.ValueMember = "REASONCODEID";
			cboDelayCode.DataSource = SqlExecuter.Query("GetReasonCodeByProcess", "10001", param);

			if ((cboDelayCode.DataSource as DataTable).Rows.Count > 0)
				cboDelayCode.ItemIndex = 0;
		}

		/// <summary>
		/// 보류 중분류 사유 combo 초기화
		/// </summary>
		private void InitalizeHoldCombo()
		{
			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("P_PARENTREASONCODECLASSID", "HoldCode");
			param.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

			
		}

	
		#endregion

		#region 유효성 검사

		/// <summary>
		/// 
		/// </summary>
		private void SaveValidation()
		{

			if (cboDelayCode.GetDataValue() == null || string.IsNullOrEmpty(Format.GetString(cboDelayCode.GetDataValue())))
			{
				//체공사유 선택은 필수입니다.
				throw MessageException.Create("NoExistDelayCode");
			}
		}

		#endregion
	}
}
