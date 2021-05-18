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
	/// 프 로 그 램 명  : 공정관리 > 공정작업 > 입고검사등록
	/// 업  무  설  명  : Lot 조회 조회조건 팝업
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-06-13
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class LotConditionPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
		public DataRow CurrentDataRow { get; set; }

		#region Local Variables

		// TODO : 화면에서 사용할 내부 변수 추가
		public event SelectPopupApplyEventHandler Selected;


		/// <summary>
		/// 입고 검사를 진행 할 Lot List 전달
		/// </summary>
		/// <param name="dt">LotList</param>
		public delegate void ResultDataSourceHandler(DataTable dt);
		public event ResultDataSourceHandler ResultDataSource;

		private string _isHold = "";
		private string _isLocking = "";
		private string _lotState = "";
		#endregion

		#region 생성자

		public LotConditionPopup()
		{
			InitializeComponent();
			
			if(!this.IsDesignMode())
			{
				InitializeEvent();
				InitializeGrid();
				InitializeCondition();
			}			
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
			grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

			//대공정
			grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 70)
				.SetIsReadOnly()
				.SetLabel("TOPPROCESSSEGMENTCLASS");
			//주요공정
			grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 70)
				.SetLabel("MIDDLEPROCESSSEGMENTCLASS"); 
			//작업장
			grdLotList.View.AddTextBoxColumn("AREANAME", 80)
				.SetIsReadOnly();
			//품목코드
			grdLotList.View.AddTextBoxColumn("PRODUCTDEFID", 130)
				.SetIsReadOnly();
			//품목코드명
			grdLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 230)
				.SetIsReadOnly();
			//LOT ID
			grdLotList.View.AddTextBoxColumn("LOTID", 230)
				.SetIsReadOnly();

			grdLotList.View.PopulateColumns();


			//GRID APPLY LOT LIST 초기화
			grdApplyLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

			//대공정
			grdApplyLotList.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 70)
				.SetIsReadOnly()
				.SetLabel("TOPPROCESSSEGMENTCLASS");
			//주요공정
			grdApplyLotList.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 70)
				.SetLabel("MIDDLEPROCESSSEGMENTCLASS");
			//작업장
			grdApplyLotList.View.AddTextBoxColumn("AREANAME", 80)
				.SetIsReadOnly();
			//품목코드
			grdApplyLotList.View.AddTextBoxColumn("PRODUCTDEFID", 130)
				.SetIsReadOnly();
			//품목코드명
			grdApplyLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 230)
				.SetIsReadOnly();
			//LOT ID
			grdApplyLotList.View.AddTextBoxColumn("LOTID", 230);

			grdApplyLotList.View.PopulateColumns();

		}

		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			// TODO : 화면에서 사용할 이벤트 추가

			btnMinus.Click += BtnMinus_Click;
			btnPlus.Click += BtnPlus_Click;
			btnSearch.Click += BtnSearch_Click;
			btnApply.Click += BtnApply_Click;
			btnCancel.Click += BtnCancel_Click;			
		}

		/// <summary>
		/// 왼쪽 이동 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnMinus_Click(object sender, EventArgs e)
		{
			DataTable dtChecked = grdApplyLotList.View.GetCheckedRows();
			DataTable dtMinusLotList = grdApplyLotList.DataSource as DataTable;
			DataTable dtApplyLotList = grdLotList.DataSource as DataTable;

			for (int i = dtMinusLotList.Rows.Count - 1; i >= 0; i --)
			{
				DataRow row = dtMinusLotList.Rows[i];
				string grdLotId = row["LOTID"].ToString();
				for(int k = dtChecked.Rows.Count -1; k>=0; k--)
				{
					DataRow chkRow = dtChecked.Rows[k];
					string chkLotId = chkRow["LOTID"].ToString();
					if(grdLotId.Equals(chkLotId))
					{
						dtApplyLotList.ImportRow(chkRow);
						row.Delete();
					}
				}				
			}

			dtMinusLotList.AcceptChanges();
			grdLotList.DataSource = dtApplyLotList;

		}


		/// <summary>
		/// 오른쪽 이동 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnPlus_Click(object sender, EventArgs e)
		{
			DataTable dtChecked = grdLotList.View.GetCheckedRows();
			DataTable dtPlusLotList = grdLotList.DataSource as DataTable;
			DataTable dtApplyLotList = grdApplyLotList.DataSource as DataTable;

			for (int i = dtPlusLotList.Rows.Count - 1; i >= 0; i --)
			{
				DataRow row = dtPlusLotList.Rows[i];
				string grdLotId = row["LOTID"].ToString();
				for(int k = dtChecked.Rows.Count -1; k>=0; k--)
				{
					DataRow chkRow = dtChecked.Rows[k];
					string chkLotId = chkRow["LOTID"].ToString();
					if(grdLotId.Equals(chkLotId))
					{
						dtApplyLotList.ImportRow(chkRow);
						row.Delete();
					}
				}				
			}

			dtPlusLotList.AcceptChanges();
			grdLotList.DataSource = dtPlusLotList;

		}

		/// <summary>
		/// 검색 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSearch_Click(object sender, EventArgs e)
		{
			//Apply Lot List 초기화
			grdApplyLotList.View.ClearDatas();

			Dictionary<string, object> param = new Dictionary<string, object>();

			string areaId = cboArea.GetDataValue() as string;
			string middleProcessSegClassId = cboProcessSeg.GetDataValue() as string;
			string productionType = cboProductionType.GetDataValue() as string;

			param.Add("areaId", areaId);
			param.Add("processSegmentClassId_Middle", middleProcessSegClassId);
			param.Add("LotType", productionType);
			param.Add("LotId", txtLotId.Text);
			//param.Add("LanguageType", UserInfo.Current.LanguageType);
			param.Add("isHold", _isHold);
			param.Add("isLocking", _isLocking);
			param.Add("lotState", _lotState);

			DataTable dtLotList = SqlExecuter.Query("GetLotList", "10001", param);
			if (dtLotList.Rows.Count < 1)
			{
				// 조회할 데이터가 없습니다.
				ShowMessage("NoSelectData");
			}

			grdLotList.DataSource = dtLotList;
		}

		/// <summary>
		/// 적용 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnApply_Click(object sender, EventArgs e)
		{
			DataTable dtResultLotList = grdApplyLotList.DataSource as DataTable;
			this.Selected(this, new SelectPopupApplyEventArgs() { Selections = dtResultLotList.Rows.Cast<DataRow>() });

			this.Close();
		}


		/// <summary>
		/// 취소 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion

		#region 조회조건
		private void InitializeCondition()
		{
			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("AreaType", "Area");
			param.Add("LanguageType", UserInfo.Current.LanguageType);
			param.Add("PlantId", UserInfo.Current.Plant);
			param.Add("ProcessSegmentClassType", "Middle");
			param.Add("CodeClassId", "ProductionType");

			//작업장
			DataTable dtAreaList = SqlExecuter.Query("GetAreaList", "10003", param);
			cboArea.DataSource = dtAreaList;
			cboArea.DisplayMember = "AREANAME";
			cboArea.ValueMember = "AREAID";
			cboArea.ShowHeader = false;

			//주요공정
			DataTable dtMiddleProcessSegClassList = SqlExecuter.Query("GetProcessSegmentClassByType", "10001", param);
			cboProcessSeg.DataSource = dtMiddleProcessSegClassList;
			cboProcessSeg.DisplayMember = "PROCESSSEGMENTCLASSNAME";
			cboProcessSeg.ValueMember = "PROCESSSEGMENTCLASSID";
			cboProcessSeg.ShowHeader = false;

			//양산구분(LotType)
			DataTable dtProductionTypeList = SqlExecuter.Query("GetTypeList", "10001", param);
			cboProductionType.DataSource = dtProductionTypeList;
			cboProductionType.DisplayMember = "CODENAME";
			cboProductionType.ValueMember = "CODEID";
			cboProductionType.ShowHeader = false;

		}
		#endregion

		#region Private Function

		// TODO : 화면에서 사용할 내부 함수 추가
		/// <summary>
		/// 마스터 정보를 조회한다.
		/// </summary>
		public void SetValue(string isHold, string isLocking, string lotState)
		{
			_isHold = isHold;
			_isLocking = isLocking;
			_lotState = lotState;
		}

		#endregion
	}
}