#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.StandardInfo
{
	/// <summary>
	/// 프 로 그 램 명  : 기준정보 > Setup > 환율 Import 및 조회
	/// 업  무  설  명  : ERP에서 인터페이스 된 환율 정보를 조회
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-05-23
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class ExchangeRateImport : SmartConditionManualBaseForm
	{
		#region Local Variables

		// TODO : 화면에서 사용할 내부 변수 추가

		#endregion

		#region 생성자

		public ExchangeRateImport()
		{
			InitializeComponent();
		}

		#endregion

		#region 컨텐츠 영역 초기화
		/// <summary>
		/// 컨텐츠 영역 초기화 시작
		/// </summary>
		protected override void InitializeContent()
		{
			base.InitializeContent();

			InitializeEvent();

			// TODO : 컨트롤 초기화 로직 구성
			InitializeGrid();
		}

		/// <summary>        
		/// 그리드 초기화
		/// </summary>
		private void InitializeGrid()
		{
			// TODO : 그리드 초기화 로직 추가
			grdExchangeList.GridButtonItem = GridButtonItem.None;
			grdExchangeList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

			//grdExchangeList.View.AddTextBoxColumn("ENTERPRISEID")
			//	.SetIsHidden()
			//	.SetDefault(UserInfo.Current.Enterprise);
			grdExchangeList.View.AddTextBoxColumn("PLANTID", 150)
				.SetIsReadOnly()
				.SetLabel("SITE");
			grdExchangeList.View.AddTextBoxColumn("EXCHANGETYPE", 150)
				.SetIsReadOnly();
			grdExchangeList.View.AddTextBoxColumn("EXCHANGEDATE", 150)
				.SetIsReadOnly()
				.SetLabel("DATE");

			grdExchangeList.View.AddTextBoxColumn("BASEUNIT", 150)
				.SetIsReadOnly();
			grdExchangeList.View.AddTextBoxColumn("CURRENCYUNIT", 150)
				.SetIsReadOnly();
			grdExchangeList.View.AddTextBoxColumn("EXCHANGEVALUE", 150)
				.SetIsReadOnly();

			//유효상태, 생성자, 수정자...
			grdExchangeList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetDefault("Valid")
				//.SetValidationIsRequired()
				.SetTextAlignment(TextAlignment.Center);
			grdExchangeList.View.AddTextBoxColumn("CREATOR", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdExchangeList.View.AddTextBoxColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdExchangeList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdExchangeList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);

			grdExchangeList.View.PopulateColumns();
		}

		#endregion

		#region Event

		/// <summary>        
		/// 이벤트 초기화
		/// </summary>
		public void InitializeEvent()
		{
			// TODO : 화면에서 사용할 이벤트 추가
			btnImport.Click += BtnImport_Click;
		}

		/// <summary>
		/// 환율 정보 Import (인터페이스)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnImport_Click(object sender, EventArgs e)
		{
			//인터페이스
			Console.WriteLine("환율 정보 Import");
		}

		#endregion

		#region 툴바

		/// <summary>
		/// 저장버튼 클릭
		/// </summary>
		protected override void OnToolbarSaveClick()
		{
			base.OnToolbarSaveClick();

			// TODO : 저장 Rule 변경
		}

		#endregion

		#region 검색

		/// <summary>
		/// 비동기 override 모델
		/// </summary>
		protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

			// TODO : 조회 SP 변경
			var values = Conditions.GetValues();
			DateTime exhangeDate = Convert.ToDateTime(values["P_EXCHANGEDATE"].ToString());

			values["P_EXCHANGEDATE"] = string.Format("{0:yyyy-MM-dd}", exhangeDate);

			/*
			switch(values["P_EXCHANGETYPE"].ToString())
			{
				case "Month":
				break;
				case "Day":
				break;
			}
			*/

			DataTable dtExchangeRate = await QueryAsync("SelectExchangeRateList", "10001", values);//ProcedureAsync("usp_com_selectexchangerate", values);

			if (dtExchangeRate.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
			{
				ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
			}

			grdExchangeList.DataSource = dtExchangeRate;
		}

		/// <summary>
		/// 조회조건 추가 구성
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

			// TODO : 조회조건 추가 구성이 필요한 경우 사용
		}

		/// <summary>
		/// 조회조건 컨트롤 기능 추가
		/// </summary>
		protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            Conditions.GetControl<SmartComboBox>("P_EXCHANGETYPE").EditValueChanged += ExchangeType_EditValueChanged;
            Conditions.GetControl<SmartDateEdit>("P_EXCHANGEDATE").EditValueChanging += ExchangeDate_EditValueChanging; ;

        }

        private void ExchangeDate_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (Conditions.GetControl<SmartComboBox>("P_EXCHANGETYPE").EditValue.Equals("Month") == true)
            {
                DateTime currentDate = (DateTime)(e.NewValue);
                if (currentDate.Day != 1)
                    e.Cancel = true;
            }
        }

        private void ExchangeType_EditValueChanged(object sender, EventArgs e)
        {
            if (Conditions.GetControl<SmartComboBox>("P_EXCHANGETYPE").EditValue.Equals("Month")  == true )
            {
                DateTime currentDate = (DateTime)(Conditions.GetControl<SmartDateEdit>("P_EXCHANGEDATE").EditValue);

                DateTime firstDate = new DateTime(currentDate.Year, currentDate.Month, 1);
                Conditions.GetControl<SmartDateEdit>("P_EXCHANGEDATE").EditValue = firstDate;
            }
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
		{
			base.OnValidateContent();

			// TODO : 유효성 로직 변경
		}

		#endregion

		#region Private Function

		// TODO : 화면에서 사용할 내부 함수 추가

		#endregion
	}
}
