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
	/// 프 로 그 램 명  : 공정관리 > 자재관리 > 자재인수
	/// 업  무  설  명  : 각 LOT별 BOM 자재 기준 자재 불출 요청내역을 보여주고 각 자재별 LOT 인수를 하여 저장한다.
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-07-02
	/// 수  정  이  력  : 
	///						1. 일괄인수 체크 -> 불출요청 그리드 && 자재랏정보 그리드 ALL 체크
	///						2. 불출요청 그리드 체크하면 자재랏 정보 보여주기
	///						3. 자재랏 정보에서 4개 있었는데 한개만 체크해서 저장하면
	///						4. 체크 기준으로 저장 하기!
	/// 
	/// </summary>
	public partial class RecieveConsumable : SmartConditionManualBaseForm
	{
		#region Local Variables

		#endregion

		#region 생성자

		public RecieveConsumable()
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
			InitializeGridMaterialListByRequest();
			InitializeGridLotListByMaterial();
		}

		/// <summary>
		/// 불출요청현황 그리드 초기화
		/// </summary>
		private void InitializeGridMaterialListByRequest()
		{
			grdMaterialListByRequest.GridButtonItem = GridButtonItem.None;
			grdMaterialListByRequest.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
			grdMaterialListByRequest.ShowButtonBar = false;
			grdMaterialListByRequest.View.SetIsReadOnly();

			
			grdMaterialListByRequest.View.AddTextBoxColumn("REQUESTID", 120).SetTextAlignment(TextAlignment.Left);
            grdMaterialListByRequest.View.AddTextBoxColumn("INBOUNDNO", 200).SetIsHidden();
            grdMaterialListByRequest.View.AddTextBoxColumn("CONSUMABLEDEFID", 100).SetTextAlignment(TextAlignment.Left);
            grdMaterialListByRequest.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 100).SetTextAlignment(TextAlignment.Left).SetIsHidden();
            grdMaterialListByRequest.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200).SetTextAlignment(TextAlignment.Left);
            grdMaterialListByRequest.View.AddTextBoxColumn("WAREHOUSEID", 70).SetTextAlignment(TextAlignment.Left).SetIsHidden();
            grdMaterialListByRequest.View.AddTextBoxColumn("WAREHOUSENAME", 120).SetTextAlignment(TextAlignment.Left);
            grdMaterialListByRequest.View.AddTextBoxColumn("AREAID", 70).SetTextAlignment(TextAlignment.Left).SetIsHidden();
            grdMaterialListByRequest.View.AddTextBoxColumn("AREANAME", 120).SetTextAlignment(TextAlignment.Left); 
            grdMaterialListByRequest.View.AddSpinEditColumn("CHARGEQTY", 100).SetDisplayFormat("#,##0.000000000", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
            grdMaterialListByRequest.View.AddSpinEditColumn("ISSUEQTY", 100).SetDisplayFormat("#,##0.000000000", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
            grdMaterialListByRequest.View.AddSpinEditColumn("LEFTQTY", 100).SetDisplayFormat("#,##0.000000000", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
            grdMaterialListByRequest.View.AddTextBoxColumn("RECEIPTDATE", 120).SetTextAlignment(TextAlignment.Center);
            grdMaterialListByRequest.View.AddTextBoxColumn("REQUESTUSER", 70).SetTextAlignment(TextAlignment.Center);
            grdMaterialListByRequest.View.AddTextBoxColumn("REQDATE", 120).SetTextAlignment(TextAlignment.Center);
            grdMaterialListByRequest.View.AddTextBoxColumn("CHECKRECEIVE").SetIsHidden();


            grdMaterialListByRequest.View.PopulateColumns();
		}

		/// <summary>
		/// 자재 LOT 인수 그리드 초기화
		/// </summary>
		private void InitializeGridLotListByMaterial()
		{
			grdConsumableLotList.GridButtonItem = GridButtonItem.None;
			grdConsumableLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
			grdConsumableLotList.View.SetIsReadOnly();

			//청구번호
			grdConsumableLotList.View.AddTextBoxColumn("INBOUNDNO")
				.SetIsHidden();
			//자재품목코드
			grdConsumableLotList.View.AddTextBoxColumn("CONSUMABLEDEFID", 100);
            //자재품목버전
            grdConsumableLotList.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 100).SetTextAlignment(TextAlignment.Left).SetIsHidden();
            //자재품목명
            grdConsumableLotList.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 150);
			//자재 LOT NO
			grdConsumableLotList.View.AddTextBoxColumn("CONSUMABLELOTID", 150);
            //창고ID
            grdConsumableLotList.View.AddTextBoxColumn("AREAID", 100).SetIsHidden();
            //창고명
            grdConsumableLotList.View.AddTextBoxColumn("AREANAME", 120);
            //작업장ID
            grdConsumableLotList.View.AddTextBoxColumn("WAREHOUSEID", 90).SetIsHidden();
            //작업장명
            grdConsumableLotList.View.AddTextBoxColumn("WAREHOUSENAME", 100);
            //시퀀스
            grdConsumableLotList.View.AddSpinEditColumn("TRANSACTIONSEQUENCE", 50)
				.SetIsHidden();
			//단위
			grdConsumableLotList.View.AddTextBoxColumn("UNIT", 50);
			//수량
			grdConsumableLotList.View.AddSpinEditColumn("INQTY", 100).SetDisplayFormat("#,##0.000000000", MaskTypes.Numeric, true);
			//인수자
			grdConsumableLotList.View.AddTextBoxColumn("RECEIVEUSER", 80);
			//인수일
			grdConsumableLotList.View.AddTextBoxColumn("RECEIVEDATE", 100);
            //반제품 입고 여부
            grdConsumableLotList.View.AddTextBoxColumn("ISHALFPRODUCT", 100).SetIsHidden();

            grdConsumableLotList.View.PopulateColumns();
		}
		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			chkIsAllReceive.CheckedChanged += ChkIsAllReceive_CheckedChanged;
			grdMaterialListByRequest.View.CheckStateChanged += View_CheckStateChanged;
			grdMaterialListByRequest.View.FocusedRowChanged += View_FocusedRowChanged;
            grdMaterialListByRequest.View.RowStyle += View_RowStyle;
		}

        private void View_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;
            //CHECKRECEIVE
            DataRow dr = grdMaterialListByRequest.View.GetDataRow(e.RowHandle);

            string CheckLot = Format.GetString(dr["CHECKRECEIVE"]);
            if (CheckLot.Equals("Y"))
            {
                e.Appearance.BackColor = Color.FromArgb(254, 254, 16);
                e.HighPriority = true;
            }
        }


        /// <summary>
        /// 일괄 인수 체크 시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkIsAllReceive_CheckedChanged(object sender, EventArgs e)
		{
			if(chkIsAllReceive.Checked)
			{
				grdMaterialListByRequest.View.CheckedAll(true);
				grdConsumableLotList.View.CheckedAll(true);
			}
			else
			{
				grdMaterialListByRequest.View.CheckedAll(false);
				grdConsumableLotList.View.CheckedAll(false);
			}
		}

		/// <summary>
		/// 불출요청현황 그리드 체크 선택 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_CheckStateChanged(object sender, EventArgs e)
		{
			DataTable dtRequestList = grdMaterialListByRequest.View.GetCheckedRows();
			DataTable dtNewMaterial = (grdConsumableLotList.DataSource as DataTable).Clone();

			foreach(DataRow row in dtRequestList.Rows)
			{
				DataTable dtConsumableLotList = ExecuteConsumableLotInfoByRequestNo(row);
				for (int i = dtConsumableLotList.Rows.Count - 1; i >= 0; i--)
				{
					DataRow dr = dtConsumableLotList.Rows[i];
					if (dr["CONSUMABLELOTID"].Equals("*"))
					{
						dr.Delete();
					}					
				}
				dtConsumableLotList.AcceptChanges();
				dtNewMaterial.Merge(dtConsumableLotList);
			}

			grdConsumableLotList.DataSource = dtNewMaterial;
		}


		/// <summary>
		/// 불출요청현황 그리드 포커스 ROW 변경 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			if(grdMaterialListByRequest.View.FocusedRowHandle < 0) return;

			DataTable dtChecked = grdMaterialListByRequest.View.GetCheckedRows();
			if(dtChecked.Rows.Count > 0)
			{
				View_CheckStateChanged(null, null);
			}
			else
			{
				DataRow row = grdMaterialListByRequest.View.GetFocusedDataRow();
				DataTable dtConsumableLotList = ExecuteConsumableLotInfoByRequestNo(row);
				for (int i = dtConsumableLotList.Rows.Count - 1; i >= 0; i--)
				{
					DataRow dr = dtConsumableLotList.Rows[i];
					if (dr["CONSUMABLELOTID"].Equals("*"))
					{
						dr.Delete();
					}
				}
				dtConsumableLotList.AcceptChanges();
				grdConsumableLotList.DataSource = dtConsumableLotList;		
			}
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
			//DataTable dtAllData = grdConsumableLotList.DataSource as DataTable;
			DataTable checkedData = grdConsumableLotList.View.GetCheckedRows();

			MessageWorker worker = new MessageWorker("ReceiveConsumable");
			worker.SetBody(new MessageBody()
			{
				{ "enterpriseId", UserInfo.Current.Enterprise },
				{ "plantId", UserInfo.Current.Plant },
				{ "list", checkedData }
			});
			worker.Execute();
			grdConsumableLotList.View.ClearDatas();
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
			var values = Conditions.GetValues();
			values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

			DataTable dtRequestList = await QueryAsync("SelectConsumableRequestState", "10002", values);

			if (dtRequestList.Rows.Count < 1) 
			{
				//조회할 데이터가 없습니다.
				ShowMessage("NoSelectData");
                grdConsumableLotList.View.ClearDatas();
			}

			grdMaterialListByRequest.DataSource = dtRequestList;
		}

		/// <summary>
		/// 조회조건 항목을 추가한다.
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

			// TODO : 조회조건 추가 구성이 필요한 경우 사용
			//작업장
			//Commons.CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 1.1, false, Conditions, true, true);
            //AddConditionWareHouseID
            Commons.CommonFunction.AddConditionWareHouseID("P_WARESHOUSE", 1.01, false, Conditions,true);
            //자재코드
            InitializeConditionConsumableDefId_Popup();
		}

		/// <summary>
		/// 조회조건의 컨트롤을 추가한다.
		/// </summary>
		protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();

			// TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
		}

		/// <summary>
		/// 팝업형 조회조건 - 자재 코드
		/// </summary>
		private void InitializeConditionConsumableDefId_Popup()
		{
			var conditionConsumableDefId = Conditions.AddSelectPopup("P_CONSUMABLEDEFID", new SqlQuery("GetConsumableDefList", "10001", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CONSUMABLEDEFNAME", "CONSUMABLEDEFID")
			   .SetPopupLayout("SELECTCONSUMABLEDEFID", PopupButtonStyles.Ok_Cancel, true, false)
			   .SetPopupResultCount(0)
			   .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
			   .SetLabel("MATERIALDEF")
			   .SetPopupAutoFillColumns("COMSUMABLEDEFNAME")
			   .SetPosition(3.1);

			//자재코드 / 명
			conditionConsumableDefId.Conditions.AddTextBox("CONSUMABLEDEF");

			//자재 코드
			conditionConsumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);
			//자재 버전
			conditionConsumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 100);
			//자재 명
			conditionConsumableDefId.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 250);
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
			grdConsumableLotList.View.CheckValidation();

			DataTable dtChecked = grdConsumableLotList.View.GetCheckedRows();

			if (dtChecked.Rows.Count == 0)
			{
				// 저장할 데이터가 존재하지 않습니다.
				throw MessageException.Create("NoSaveData");
			}
		}

		#endregion

		#region Private Function
		/// <summary>
		/// 불출요청 자재별 자재 LOT정보 조회
		/// </summary>
		/// <param name="row"></param>
		private DataTable ExecuteConsumableLotInfoByRequestNo(DataRow row)
		{
			DataTable dataTable = new DataTable();

			string inboundNo = row["INBOUNDNO"].ToString();
			string consumableDefId = row["CONSUMABLEDEFID"].ToString();
			string areaId = row["AREAID"].ToString();
            string consumdefid = row["CONSUMABLEDEFID"].ToString();
            string consumdefversion = row["CONSUMABLEDEFVERSION"].ToString();
            string whid = row["WAREHOUSEID"].ToString();

            Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("InBoundNo", inboundNo);
			param.Add("ConsumableDefId", consumableDefId);
			param.Add("AreaId", areaId);
            param.Add("P_CONSUMABLEDEFID", consumdefid);
            param.Add("P_CONSUMABLEDEFVERSION", consumdefversion);
            param.Add("P_WAREHOUSENAME", whid);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            dataTable = SqlExecuter.Query("SelectConsumableLotByInboundNo", "10001", param);

			return dataTable;
		}
		#endregion
	}
}