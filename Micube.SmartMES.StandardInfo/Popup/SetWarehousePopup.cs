#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.StandardInfo.Popup
{
	/// <summary>
	/// 프 로 그 램 명  : ex> 시스템관리 > 코드 관리 > 코드그룹 정보
	/// 업  무  설  명  : 완료 창고 지정 팝업
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2020-02-20
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class SetWarehousePopup : SmartPopupBaseForm, ISmartCustomPopup
	{
		#region Interface
		public DataRow CurrentDataRow { get; set; }
		#endregion

		#region Local Variables
		string _productDefId = string.Empty;
		string _productDefVersion = string.Empty;
		#endregion

		#region 생성자

		public SetWarehousePopup()
		{
			InitializeComponent();
		}

		public SetWarehousePopup(List<string> plantList, string productDefId, string productDefVersion)
		{
			InitializeComponent();

			InitializeEvent();
			InitializeGrid(productDefId, productDefVersion);

			DataTable dt = SearchData(productDefId, productDefVersion);
			if(dt.Rows.Count > 0)
			{
				SetData(dt);
			}
			else
			{
				SetData(plantList, productDefId, productDefVersion);
			}

			_productDefId = productDefId;
			_productDefVersion = productDefVersion;
		}

		#endregion

		#region 컨텐츠 영역 초기화

		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitializeGrid(string productDefId, string productDefVersion)
		{
			grdWarehouseByPlant.GridButtonItem = GridButtonItem.None;
			grdWarehouseByPlant.View.SetAutoFillColumn("WAREHOUSENAME");

			grdWarehouseByPlant.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden().SetDefault(UserInfo.Current.Enterprise);
			grdWarehouseByPlant.View.AddTextBoxColumn("PRODUCTDEFID").SetIsHidden().SetDefault(productDefId);
			grdWarehouseByPlant.View.AddTextBoxColumn("PRODUCTDEFVERSION").SetIsHidden().SetDefault(productDefVersion);
			grdWarehouseByPlant.View.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();

			var warehousePopup = grdWarehouseByPlant.View.AddSelectPopupColumn("WAREHOUSEID", 100, new SqlQuery("GetWarehouseList", "10002"))
				.SetPopupLayout("SELECTPROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
				.SetValidationIsRequired()
				.SetPopupResultCount(1)
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("WAREHOUSENAME")
				.SetRelationIds("PLANTID")
				.SetPopupApplySelection((selectRow, gridRow) =>
				{
					DataRow row = selectRow.FirstOrDefault();
					if(row == null) return;

					gridRow["WAREHOUSENAME"] = row["WAREHOUSENAME"];
				});
			warehousePopup.Conditions.AddTextBox("TXTWAREHOUSE");
			warehousePopup.Conditions.AddTextBox("PLANTID").SetPopupDefaultByGridColumnId("PLANTID").SetIsHidden();
			warehousePopup.GridColumns.AddTextBoxColumn("WAREHOUSEID", 100);
			warehousePopup.GridColumns.AddTextBoxColumn("WAREHOUSENAME", 100);

			grdWarehouseByPlant.View.AddTextBoxColumn("WAREHOUSENAME", 100).SetIsReadOnly();

			grdWarehouseByPlant.View.PopulateColumns();

			(grdWarehouseByPlant.View.Columns["WAREHOUSEID"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit).ButtonClick += SetWarehousePopup_ButtonClick;
		}

		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			btnCancel.Click += BtnCancel_Click;
			btnSave.Click += BtnSave_Click;
		}

		/// <summary>
		///  팝업 컬럼 x버튼 누를 때 이벤트 - 창고ID
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SetWarehousePopup_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
			{
				grdWarehouseByPlant.View.SetFocusedRowCellValue("WAREHOUSENAME", string.Empty);
			}
		}

		/// <summary>
		/// 취소 버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// 저장 버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSave_Click(object sender, EventArgs e)
		{
			DataTable changed = grdWarehouseByPlant.GetChangedRows();

			MessageWorker worker = new MessageWorker("RoutingMgnt");
			worker.SetBody(new MessageBody()
			{
				{ "itemWarehouse", changed }
			});

			worker.Execute();

			ShowMessage("SuccedSave");

			DataTable dt = SearchData(_productDefId, _productDefVersion);
			SetData(dt);

            this.Close();
		}
		#endregion


		#region Private Function
		/// <summary>
		/// 품목 - 창고 맵핑된 데이터 조회
		/// </summary>
		/// <param name="list"></param>
		/// <param name="productDefId"></param>
		/// <param name="productDefVersion"></param>
		/// <returns></returns>
		private DataTable SearchData(string productDefId, string productDefVersion)
		{
			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("PRODUCTDEFID", productDefId);
			param.Add("PRODUCTDEFVERSION", productDefVersion);
			param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

			
			return SqlExecuter.Query("GetItemWarehouseList", "10001", param);
		}

		/// <summary>
		/// Set 데이터
		/// </summary>
		/// <param name="list"></param>
		private void SetData(List<string> list, string productDefId, string productDefVersion)
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("ENTERPRISEID", typeof(string));
			dt.Columns.Add("PRODUCTDEFID", typeof(string));
			dt.Columns.Add("PRODUCTDEFVERSION", typeof(string));
			dt.Columns.Add("PLANTID", typeof(string));
			dt.Columns.Add("WAREHOUSEID", typeof(string));
			dt.Columns.Add("WAREHOUSENAME", typeof(string));

			for(int i = 0; i < list.Count; i++)
			{
				dt.Rows.Add(UserInfo.Current.Enterprise, productDefId, productDefVersion, list[i], "", "");
			}
			
			grdWarehouseByPlant.DataSource = dt;
		}

		/// <summary>
		/// Set 데이터
		/// </summary>
		/// <param name="dt"></param>
		private void SetData(DataTable dt)
		{
			grdWarehouseByPlant.DataSource = dt;
		}
		#endregion
	}
}