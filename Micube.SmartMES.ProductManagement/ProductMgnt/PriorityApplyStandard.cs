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

namespace Micube.SmartMES.ProductManagement
{
	/// <summary>
	/// 프 로 그 램 명  : 생산관리 > 우선 순위 관리 > 우선순위 적용 기준 등록
	/// 업  무  설  명  : 디스패칭 항목별 우선순위 기준을 등록하는 화면
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-08-16
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class PriorityApplyStandard : SmartConditionManualBaseForm
	{
		#region Local Variables

		#endregion

		#region 생성자

		public PriorityApplyStandard()
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
			InitializeGridDispatchingList();
			InitializeGridSetPriorityList();

			new Commons.SetGridDeleteButonVisibleSimple(grdSetPriorityList);
			(grdSetPriorityList.View.Columns["TOVALUE"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItem).EditValueChanging += PriorityApplyStandard_EditValueChanging1;
			(grdSetPriorityList.View.Columns["FROMVALUE"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItem).EditValueChanging += PriorityApplyStandard_EditValueChanging;
		}

		/// <summary>        
		/// 디스패치 항목별 리스트 그리드 초기화
		/// </summary>
		private void InitializeGridDispatchingList()
		{
			grdDispachingList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

			//디스패칭 항목
			grdDispachingList.View.AddComboBoxColumn("DISPATCHINGITEM", 130, new SqlQuery("GetTypeList", "10001", "CODECLASSID=DispatchingItem", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetValidationIsRequired()
				.SetValidationKeyColumn();
			//설명
			grdDispachingList.View.AddTextBoxColumn("DESCRIPTION", 200);
			//우선순위
			grdDispachingList.View.AddSpinEditColumn("PRIOTY")
				.SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
				.SetLabel("PRIORITY");
			//회사
			grdDispachingList.View.AddTextBoxColumn("ENTERPRISEID")
				.SetIsHidden()
				.SetDefault(UserInfo.Current.Enterprise);
			//SITE
			grdDispachingList.View.AddTextBoxColumn("PLANTID")
				.SetIsHidden()
				.SetDefault(UserInfo.Current.Plant);

			//유효상태, 생성자, 수정자
			grdDispachingList.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetDefault("Valid")
				.SetValidationIsRequired()
				.SetTextAlignment(TextAlignment.Center);
			grdDispachingList.View.AddTextBoxColumn("CREATOR", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdDispachingList.View.AddTextBoxColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdDispachingList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdDispachingList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);

			grdDispachingList.View.PopulateColumns();
		}

		/// <summary>
		/// 우선순위 설정 그리드 초기화
		/// </summary>
		private void InitializeGridSetPriorityList()
		{
			grdSetPriorityList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
			grdSetPriorityList.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;

			//디스패칭 항목
			grdSetPriorityList.View.AddTextBoxColumn("DISPATCHINGITEM")
				.SetIsHidden();
			//순위
			grdSetPriorityList.View.AddSpinEditColumn("ITEMPRIOTY", 60)
				.SetIsReadOnly()
				.SetLabel("PRIORITY")
				.SetValidationKeyColumn();
			//From
			grdSetPriorityList.View.AddSpinEditColumn("FROMVALUE", 100)
				.SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
			//To
			grdSetPriorityList.View.AddSpinEditColumn("TOVALUE", 100)
				.SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
			//회사
			grdSetPriorityList.View.AddTextBoxColumn("ENTERPRISEID")
				.SetIsHidden()
				.SetDefault(UserInfo.Current.Enterprise);
			//SITE
			grdSetPriorityList.View.AddTextBoxColumn("PLANTID")
				.SetIsHidden()
				.SetDefault(UserInfo.Current.Plant);

			//유효상태, 생성자, 수정자
			grdSetPriorityList.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetDefault("Valid")
				.SetValidationIsRequired()
				.SetTextAlignment(TextAlignment.Center);
			grdSetPriorityList.View.AddTextBoxColumn("CREATOR", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdSetPriorityList.View.AddTextBoxColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdSetPriorityList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdSetPriorityList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);

			grdSetPriorityList.View.PopulateColumns();
		}
		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			btnSimulation.Click += BtnSimulation_Click;
			grdSetPriorityList.View.CellValueChanged += View_CellValueChanged;
			grdSetPriorityList.View.AddingNewRow += View_AddingNewRow1;
			grdDispachingList.View.FocusedRowChanged += View_FocusedRowChanged;
			grdDispachingList.View.AddingNewRow += View_AddingNewRow;
		}

		/// <summary>
		/// To Value에 빈값 입력 시 Cancel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PriorityApplyStandard_EditValueChanging1(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			string toValue = Format.GetString(e.NewValue);
			if (string.IsNullOrEmpty(toValue))
			{
				e.Cancel = true;
			}
			
		}

		/// <summary>
		/// From Value에 빈값 입력 시 Cancel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PriorityApplyStandard_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			string fromValue = Format.GetString(e.NewValue);
			if(string.IsNullOrEmpty(fromValue))
			{
				e.Cancel = true;
			}
			
		}

		/// <summary>
		/// 디스패칭 항목 선택 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			FocusedRowChange();
		}

		/// <summary>
		/// 시뮬레이션 팝업 오픈
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSimulation_Click(object sender, EventArgs e)
		{
			SimulationPopup simulationPopup = new SimulationPopup();
			simulationPopup.ShowDialog();
		}

		/// <summary>
		/// 우선순위 설정 그리드 셀 변경 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			int currentIndex = grdSetPriorityList.View.FocusedRowHandle;

			if (currentIndex < 0) return;


			DataRow currentRow = grdSetPriorityList.View.GetFocusedDataRow();
			decimal currentFromValue = Format.GetDecimal(currentRow["FROMVALUE"], 0);
			decimal currentToValue = Format.GetDecimal(currentRow["TOVALUE"], 0);

			int nextIndex = currentIndex + 1;
			DataRow nextRow = grdSetPriorityList.View.GetDataRow(nextIndex);
			if(nextRow != null)
			{
				decimal nextToValue = Format.GetDecimal(nextRow["TOVALUE"], 0);
				if (currentToValue >= nextToValue)
				{
					//현재 To 값은 다음 To 값보다 클 수 없습니다.
					ShowMessage("NotOverNextToValue");

					grdSetPriorityList.View.CellValueChanged -= View_CellValueChanged;
					grdSetPriorityList.View.SetRowCellValue(currentIndex, e.Column, "");
					grdSetPriorityList.View.CellValueChanged += View_CellValueChanged;

					return;
				}
			}


			if (currentIndex == 0)
			{
				if (currentFromValue > currentToValue)
				{
					//From 값은 To 값보다 클 수 없습니다.
					ShowMessage("NotOverToValue");

					grdSetPriorityList.View.CellValueChanged -= View_CellValueChanged;
					grdSetPriorityList.View.SetRowCellValue(currentIndex, e.Column, "");
					grdSetPriorityList.View.CellValueChanged += View_CellValueChanged;

					return;
				}
			}
			else if(currentIndex > 0)
			{
				DataRow prevRow = grdSetPriorityList.View.GetDataRow(currentIndex -1);
				decimal prevToValue = Format.GetDecimal(prevRow["TOVALUE"], 0);

				if (currentFromValue < prevToValue)
				{
					//이전 범위보다 작을수 없습니다.
					ShowMessage("LessPrveRange");

					grdSetPriorityList.View.CellValueChanged -= View_CellValueChanged;
					grdSetPriorityList.View.SetRowCellValue(currentIndex, e.Column, prevToValue + 1);
					grdSetPriorityList.View.CellValueChanged += View_CellValueChanged;

					return;
				}
				else if(!currentFromValue.Equals(prevToValue + 1))
				{
					//From 값은 {0} 이어야 합니다.
					ShowMessage("MustBeFromValue", string.Format("{0:D}", (int)(prevToValue + 1) ));

					grdSetPriorityList.View.CellValueChanged -= View_CellValueChanged;
					grdSetPriorityList.View.SetRowCellValue(currentIndex, e.Column, prevToValue + 1);
					grdSetPriorityList.View.CellValueChanged += View_CellValueChanged;

					return;
				}
				else if(currentFromValue > currentToValue)
				{
					//From 값은 To 값보다 클 수 없습니다.
					ShowMessage("NotOverToValue");

					grdSetPriorityList.View.CellValueChanged -= View_CellValueChanged;
					grdSetPriorityList.View.SetRowCellValue(currentIndex, e.Column, "");
					grdSetPriorityList.View.CellValueChanged += View_CellValueChanged;

					return;
				}
			}

			if(!(e.Column.FieldName.Equals("FROMVALUE") && currentIndex.Equals(0)) && nextRow != null)
			{
				//다음 row From 값 지정
				if (nextRow != null)
				{
					grdSetPriorityList.View.CellValueChanged -= View_CellValueChanged;
					grdSetPriorityList.View.SetRowCellValue(nextIndex, "FROMVALUE", currentToValue + 1);
					grdSetPriorityList.View.CellValueChanged += View_CellValueChanged;
				}
			}
			

		}

		/// <summary>
		/// 우선순위 설정 그리드 ROW  추가 시 이벤트 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void View_AddingNewRow1(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
		{
			DataTable dtSetPriorityList = grdSetPriorityList.DataSource as DataTable;
			int index = dtSetPriorityList.Rows.Count;

			args.NewRow["ITEMPRIOTY"] = index;

			DataRow drSelectedDispatchingItem = grdDispachingList.View.GetFocusedDataRow();
			if(drSelectedDispatchingItem != null)
			{
				string dispatchingItem = Format.GetString(drSelectedDispatchingItem["DISPATCHINGITEM"], "");

				args.NewRow["DISPATCHINGITEM"] = dispatchingItem;

			}
			int currentIndex = grdSetPriorityList.View.FocusedRowHandle;
			if (currentIndex > 0)
			{
				DataRow prevRow = grdSetPriorityList.View.GetDataRow(currentIndex - 1);
				decimal prevToValue = Format.GetDecimal(prevRow["TOVALUE"], 0);

				args.NewRow["FROMVALUE"] = prevToValue + 1;
			}
			else
			{
				args.NewRow["FROMVALUE"] = (decimal)1;
			}
			
			
		}

		/// <summary>
		/// 디스패칭 항목 리스트 ROW 추가 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param vname="args"></param>
		private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
		{
			DataTable dtDispatchingList = grdDispachingList.DataSource as DataTable;
			int index = dtDispatchingList.Rows.Count;
			
			args.NewRow["PRIOTY"] = index;
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

			string dispatchingItem = "";

			DataTable changedDispatchingItem = grdDispachingList.GetChangedRows();
			if(changedDispatchingItem.Rows.Count == 0)
			{
				DataRow drDispatchingItem = grdDispachingList.View.GetFocusedDataRow();
				dispatchingItem = drDispatchingItem["DISPATCHINGITEM"].ToString();
			}
			else
			{
				dispatchingItem = changedDispatchingItem.Rows[0]["DISPATCHINGITEM"].ToString();
			}

			//오른쪽 먼저 add하고 왼쪽 add했을 경우 방지
			DataTable changedPriority = grdSetPriorityList.GetChangedRows();
			foreach(DataRow row in changedPriority.Rows)
			{
				row["DISPATCHINGITEM"] = dispatchingItem;
			}

			MessageWorker worker = new MessageWorker("SaveDispatchingRule");
			worker.SetBody(new MessageBody()
			{
				{ "dispatchingItem", changedDispatchingItem },
				{ "priorityOfItem", changedPriority }
			});

			worker.Execute();
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
			int prevIndex = grdDispachingList.View.FocusedRowHandle;

			var values = Conditions.GetValues();
			values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
			values.Add("PLANTID", UserInfo.Current.Plant);

			DataTable dtDispatchingItemList = await QueryAsync("SelectDispatchingItemList", "10001", values);

			if (dtDispatchingItemList.Rows.Count < 1)
			{
				// 조회할 데이터가 없습니다.
				ShowMessage("NoSelectData");
				grdDispachingList.View.ClearDatas();
			}
			else
			{
				grdDispachingList.DataSource = dtDispatchingItemList;

				int currentIndex = grdDispachingList.View.FocusedRowHandle;
				if(currentIndex < 0)
				{
					grdDispachingList.View.FocusedRowHandle = 0;

					FocusedRowChange();
				}
				else
				{
					grdDispachingList.View.SelectRow(grdDispachingList.View.FocusedRowHandle);
					FocusedRowChange();
				}
			}
			


		}

		/// <summary>
		/// 디스패칭 항목의 포커스 Row 조회
		/// </summary>
		private void FocusedRowChange()
		{
			if(grdDispachingList.View.FocusedRowHandle < 0) return;

			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
			param.Add("PLANTID", UserInfo.Current.Plant);

			DataRow currentRow = grdDispachingList.View.GetDataRow(grdDispachingList.View.FocusedRowHandle);
			if(currentRow != null)
			{
				string dispatchingItem = Format.GetString(currentRow["DISPATCHINGITEM"], "");
				param.Add("DISPATCHINGITEM", dispatchingItem);
			}

			grdSetPriorityList.DataSource = SqlExecuter.Query("SelectPriorityOfDispatchingItemList", "10001", param);

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
			grdDispachingList.View.CheckValidation();
			grdSetPriorityList.View.CheckValidation();

			DataTable changedDispatchingItemList = grdDispachingList.GetChangedRows();
			DataTable changedPriorityList = grdSetPriorityList.GetChangedRows();
			if (changedDispatchingItemList.Rows.Count == 0 && changedPriorityList.Rows.Count == 0)
			{
				// 저장할 데이터가 존재하지 않습니다.
				throw MessageException.Create("NoSaveData");
			}


			if(changedPriorityList.Rows.Count > 0)
			{
				List<DataRow> list = changedPriorityList.AsEnumerable().Where(r => string.IsNullOrEmpty(Format.GetString(r["FROMVALUE"])) 
																				|| string.IsNullOrEmpty(Format.GetString(r["TOVALUE"]))).ToList();
				if (list.Count > 0)
				{
					//범위 지정은 필수입니다.
					throw MessageException.Create("IsRequiredRange");
				}

				//범위 Validation
				int count = 0;
				decimal prevToValue = 0;
				foreach (DataRow row in changedPriorityList.Rows)
				{
					if (count > 0)
					{
						decimal currentFromValue = Format.GetDecimal(row["FROMVALUE"], 0);
						if (!currentFromValue.Equals(prevToValue + 1))
						{
							//범위는 빈 간격 없이 입력해야 합니다.
							throw MessageException.Create("NoAllowRangeGap");
						}
					}

					prevToValue = Format.GetDecimal(row["TOVALUE"], 0);
					count++;
				}
			}

			if(changedDispatchingItemList.Rows.Count > 0)
			{
				foreach (DataRow row in changedDispatchingItemList.Rows)
				{
					if (row["_STATE_"].Equals("deleted") && changedPriorityList.Rows.Count != 0)
					{
						//순위 범위가 지정되어있어 삭제할 수 없습니다.
						throw MessageException.Create("ExistRankingOfItem");
					}
				}
			}

		}

		#endregion

		#region Private Function

		#endregion
	}
}
