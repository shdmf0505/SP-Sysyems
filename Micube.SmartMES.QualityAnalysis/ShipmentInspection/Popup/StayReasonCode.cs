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

namespace Micube.SmartMES.QualityAnalysis
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
	public partial class StayReasonCode : SmartPopupBaseForm, ISmartCustomPopup
	{
		public DataRow CurrentDataRow { get; set; }
        public DataTable dtStaying;
		#region 생성자
		public StayReasonCode()
		{
			InitializeComponent();
            InitializeGrid();

        }

		public StayReasonCode(DataTable dt)
		{
			InitializeComponent();

			InitializeEvent();
            InitializeGrid();
            dtStaying = dt;
            grdStaying.DataSource = dtStaying;

        }
        #endregion
        #region 컨텐츠 영역 초기화

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가

            //GRID LOT LIST 초기화
            grdStaying.GridButtonItem = GridButtonItem.None;
            grdStaying.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //체공기준 시간
            grdStaying.View.AddTextBoxColumn("STAYINGLOCKSTD", 70)
                .SetIsReadOnly()
                .SetLabel("STDSTAYINGTIME")
                ;
            //현상태 체공
            grdStaying.View.AddTextBoxColumn("CURRENTSTATESTAYINGTIMECNV", 120)
                .SetLabel("STATEDELAYTIME")
                .SetIsReadOnly();
            //이전스텝 완료시간
            grdStaying.View.AddTextBoxColumn("CHECKTIME", 150)
                .SetLabel("PRVSTEPCOMPTIME")
                .SetIsReadOnly();
            //현재시간
            grdStaying.View.AddTextBoxColumn("CUR_TIME", 150)
                .SetIsReadOnly()
                .SetLabel("NOWTIME");
            grdStaying.View.PopulateColumns();

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
			SaveValidation();

			MessageWorker worker = new MessageWorker("AbnormalOccurenceByDelayLot");
            worker.SetBody(new MessageBody(){
                { "enterpriseId", Framework.UserInfo.Current.Enterprise },
                { "plantId", Framework.UserInfo.Current.Plant },
                { "userId", Framework.UserInfo.Current.Id },
                { "lotId", Format.GetTrimString(dtStaying.Rows[0]["LOTID"]) },
                { "delayCode", cboDelayCode.GetDataValue() },
                { "delayComment", txtComment.EditValue },
                { "checkTime", Format.GetTrimString(dtStaying.Rows[0]["CHECKTIME"]) }
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
