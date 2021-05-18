#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
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
	/// 프 로 그 램 명  : 공정관리 > 자재관리 > 자재반납
	/// 업  무  설  명  : 작업장별 재고를 원창에 반납한다.
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-09-02
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class ReturnConsumable : SmartConditionManualBaseForm
	{
		#region Local Variables

		#endregion

		#region 생성자

		public ReturnConsumable()
		{
			InitializeComponent();
		}

		#endregion

		#region 컨텐츠 영역 초기화

		/// <summary>
		/// 화면의 컨텐츠 영역을 초기화한다.
		/// </summary>
		protected override void InitializeContent()
		{
			base.InitializeContent();

			InitializeEvent();

			// TODO : 컨트롤 초기화 로직 구성
			InitializeConsumableListGrid();
			InitializeReturnConsumListGrid();
		}

		/// <summary>        
		/// 자재 리스트 그리드를 초기화
		/// </summary>
		private void InitializeConsumableListGrid()
		{
			
		}

		/// <summary>
		/// 반품 대상 자재 리스트 초기화
		/// </summary>
		private void InitializeReturnConsumListGrid()
		{ }
		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			
		}

		#endregion

		#region 툴바

		/// <summary>
		/// 저장 버튼을 클릭하면 호출한다.
		/// </summary>
		protected override void OnToolbarSaveClick()
		{
			base.OnToolbarSaveClick();

			// TODO : 저장 Rule 변경
			//DataTable changed = grdList.GetChangedRows();

			//ExecuteRule("SaveCodeClass", changed);
		}

		#endregion

		#region 검색

		/// <summary>
		/// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
		/// </summary>
		protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

			// TODO : 조회 SP 변경
			//var values = Conditions.GetValues();

			//DataTable dtCodeClass = await ProcedureAsync("usp_com_selectCodeClass", values);

			//if (dtCodeClass.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
			//{
			//	ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
			//}

			//grdList.DataSource = dtCodeClass;
		}

		/// <summary>
		/// 조회조건 항목을 추가한다.
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

			// TODO : 조회조건 추가 구성이 필요한 경우 사용
		}

		/// <summary>
		/// 조회조건의 컨트롤을 추가한다.
		/// </summary>
		protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();

			// TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
		}

		#endregion

		#region 유효성 검사

		/// <summary>
		/// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
		/// </summary>
		protected override void OnValidateContent()
		{
			base.OnValidateContent();

			// TODO : 유효성 로직 변경
			//grdList.View.CheckValidation();

			//DataTable changed = grdList.GetChangedRows();

			//if (changed.Rows.Count == 0)
			//{
			//	// 저장할 데이터가 존재하지 않습니다.
			//	throw MessageException.Create("NoSaveData");
			//}
		}

		#endregion

		#region Private Function

		#endregion
	}
}