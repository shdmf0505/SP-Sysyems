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

namespace Micube.SmartMES.ProductManagement
{
	/// <summary>
	/// 프 로 그 램 명  : 생산관리 > 우선순위관리 > 우선 순위 적용 기준 등록 > 시뮬레이션 팝업
	/// 업  무  설  명  : 
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-08-19
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class SimulationPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
		public DataRow CurrentDataRow { get; set; }

		#region Local Variables

		#endregion

		#region 생성자

		public SimulationPopup()
		{
			InitializeComponent();

			if(!this.IsDesignMode())
			{
				InitializeEvent();
				InitializeGrid();
				InitializePopup();
			}
		}

		#endregion

		#region 컨텐츠 영역 초기화

		/// <summary>
		/// 팝업 초기화
		/// </summary>
		private void InitializePopup()
		{
			ConditionItemSelectPopup areaCondition = new ConditionItemSelectPopup();
			areaCondition.Id = "AREA";
			areaCondition.SearchQuery = new SqlQuery("GetAreaList", "10003", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", "AREATYPE=Area", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
			areaCondition.ValueFieldName = "AREAID";
			areaCondition.DisplayFieldName = "AREANAME";
			areaCondition.SetPopupLayout("SELECTAREA", PopupButtonStyles.Ok_Cancel, true, true);
			areaCondition.SetPopupResultCount(1);
			areaCondition.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
			areaCondition.SetPopupAutoFillColumns("AREANAME");

			areaCondition.Conditions.AddTextBox("AREA");

			areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150);
			areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);

			popArea.Editor.SelectPopupCondition = areaCondition;
		}

		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitializeGrid()
		{
			grdSimulationList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
			grdSimulationList.View.SetIsReadOnly();

			//품목순위
			grdSimulationList.View.AddTextBoxColumn("PRODUCTPRIORITY", 70);
			//잔여납기일
			grdSimulationList.View.AddTextBoxColumn("LEFTDATE", 70);
			//잔여공정
			grdSimulationList.View.AddTextBoxColumn("REMAINSEGMENT", 70);
			//대공정
			grdSimulationList.View.AddTextBoxColumn("TOPSEGMENTCLASSNAME", 70)
				.SetLabel("TOPPROCESSSEGMENTCLASS");
			//중공정
			grdSimulationList.View.AddTextBoxColumn("MIDDLESEGMENTCLASSNAME", 80)
				.SetLabel("MIDDLEPROCESSSEGMENTCLASS");
			//작업장
			grdSimulationList.View.AddTextBoxColumn("AREANAME", 70);
			//품목코드
			grdSimulationList.View.AddTextBoxColumn("PRODUCTDEFID", 100);
			//품목명
			grdSimulationList.View.AddTextBoxColumn("PRODUCTDEFNAME", 300);
			//LOT
			grdSimulationList.View.AddTextBoxColumn("LOTID", 250);
			//작업공정
			grdSimulationList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 80);
			//공정수순
			grdSimulationList.View.AddTextBoxColumn("USERSEQUENCE", 60);
			//UOM
			grdSimulationList.View.AddTextBoxColumn("UOM", 60);
			//PNL
			grdSimulationList.View.AddSpinEditColumn("PNL", 60);
			//PCS
			grdSimulationList.View.AddSpinEditColumn("PCS", 60);
			//MM
			grdSimulationList.View.AddSpinEditColumn("MM", 100);

			grdSimulationList.View.PopulateColumns();
		}

		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			btnRunSimulation.Click += BtnRunSimulation_Click;
			btnClose.Click += BtnClose_Click;
		}

		/// <summary>
		/// 시뮬레이션 시작
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnRunSimulation_Click(object sender, EventArgs e)
		{
			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("LanguageType", UserInfo.Current.LanguageType);
			param.Add("AreaId", popArea.Editor.GetValue());

			grdSimulationList.DataSource = SqlExecuter.Query("SelectLotListSetDispatchingItem", "10001", param);
		}

		/// <summary>
		/// 닫기
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}


		#endregion

		#region Private Function

		// TODO : 화면에서 사용할 내부 함수 추가
		/// <summary>
		/// 마스터 정보를 조회한다.
		/// </summary>
		private void LoadData()
		{

		}

		#endregion
	}
}