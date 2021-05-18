#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
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
    /// 프 로 그 램 명  : 생산관리 > 우선 순위 관리 > 품목 우선 순위 등록
    /// 업  무  설  명  : 품목 별 우선순위를 등록하는 화면이다.
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2019-08-16
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class PriorityPerProduct : SmartConditionManualBaseForm
    {
        #region Local Variables

		#endregion

		#region 생성자

		public PriorityPerProduct()
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
            InitializeGrid();
        }

        /// <summary>        
        /// 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            grdPriorityPerProduct.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            //우선순위
            grdPriorityPerProduct.View.AddSpinEditColumn("PRIORITY", 80)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
			//품목코드
            InitializeProductId_Popup();
            //품목명
            grdPriorityPerProduct.View.AddTextBoxColumn("PRODUCTDEFNAME", 300)
                .SetIsReadOnly();

            //생성자, 수정자...
            grdPriorityPerProduct.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden();
            grdPriorityPerProduct.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden();
            grdPriorityPerProduct.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdPriorityPerProduct.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdPriorityPerProduct.View.PopulateColumns();

        }
        /// <summary>
        /// 팝업형 컬럼 초기화-품목코드
        /// </summary>
        private void InitializeProductId_Popup()
        {
            var productColumn = grdPriorityPerProduct.View.AddSelectPopupColumn("PRODUCTDEFID", 150, new SqlQuery("GetProductDefList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME")
				.SetValidationKeyColumn()
				.SetValidationIsRequired()
                .SetPopupApplySelection((selectedRows, dataGridRows) =>
                {
					string productDefId = "";
                    foreach (DataRow row in selectedRows)
                    {
						productDefId = Format.GetString(row["PRODUCTDEFID"], "");
						dataGridRows["PRODUCTDEFNAME"] = Format.GetString(row["PRODUCTDEFNAME"], "");
					}

					//List<DataRow> list = dataGridRows.Table.AsEnumerable().Where(r=> !Format.GetDecimal(r["PRIORITY"]).Equals(0) && r.Field<string>("PRODUCTDEFID").Equals(productDefId)).ToList();
					//if(list.Count > 0)
					//{
					//	//이미 우선순위가 등록된 품목입니다.
					//	ShowMessage("AlreadyInputPriority");
					//	return;
					//	//throw MessageException.Create("AlreadyInputPriority");
					//}
				})
				.SetPopupValidationCustom(ValidationProductDefId);

			productColumn.Conditions.AddTextBox("PRODUCTDEF");

            productColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            productColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 50);
            productColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
		}

		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		/// 
		public void InitializeEvent()
        {
			// TODO : 화면에서 사용할 이벤트 추가
			grdPriorityPerProduct.View.CellValueChanged += View_CellValueChanged;
        }

		/// <summary>
		/// 셀 변경 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			if(e.Column.FieldName.Equals("PRIORITY"))
			{
				DataRow row = grdPriorityPerProduct.View.GetFocusedDataRow();
				string productDefId = Format.GetString(row["PRODUCTDEFID"], "");

				if(string.IsNullOrEmpty(productDefId))
				{
					//품목코드를 먼저 입력하십시오.
					ShowMessage("PriorityInputSomething", Language.Get("PRODUCTDEFID"));

					grdPriorityPerProduct.View.CellValueChanged -= View_CellValueChanged;
					grdPriorityPerProduct.View.SetFocusedRowCellValue("PRIORITY", "");
					grdPriorityPerProduct.View.CellValueChanged += View_CellValueChanged;

					return;
				}
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
            DataTable changed = grdPriorityPerProduct.GetChangedRows();

            ExecuteRule("SaveProductPefPriority", changed);
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
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("PLANTID", UserInfo.Current.Plant);

            DataTable dtProductPriorityList = await QueryAsync("SelectProductPriorityList", "10002", values);

            if (dtProductPriorityList.Rows.Count < 1)
            {
				// 조회할 데이터가 없습니다.
				ShowMessage("NoSelectData"); 
            }

			grdPriorityPerProduct.DataSource = dtProductPriorityList;


		}

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            InitializeConditionProductID_Popup();


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
        /// 조회조건의 품목코드 팝업추가
        /// </summary>
        private void InitializeConditionProductID_Popup()
        {
            var productColumn = this.Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefList", "10001", "ISPRIORITY=Y", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFID", "PRODUCTDEFID")
                                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, true)
                                .SetPopupResultCount(0)
                                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                                .SetPosition(1.1)
                                .SetLabel("PRODUCTDEFID");

            productColumn.Conditions.AddTextBox("PRODUCTDEF");

            productColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            productColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 50);
            productColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150);

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
            grdPriorityPerProduct.View.CheckValidation();

            DataTable changed = grdPriorityPerProduct.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

		#endregion

		#region Private Function

		/// <summary>
		/// 품목 팝업 컬럼 Validation
		/// </summary>
		/// <param name="currentGridRow"></param>
		/// <param name="popupSelections"></param>
		/// <returns></returns>
		private ValidationResultCommon ValidationProductDefId(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
		{
			ValidationResultCommon result = new ValidationResultCommon();

			DataTable dt = grdPriorityPerProduct.DataSource as DataTable;
			DataRow[] rows = dt.AsEnumerable().Where(r => !string.IsNullOrEmpty(r.Field<string>("PRODUCTDEFID"))).ToArray();

			for (int i = 0; i < rows.Count(); i++)
			{
				if (popupSelections.Any(s => s.ToStringNullToEmpty("PRODUCTDEFID") == Format.GetString(rows[i]["PRODUCTDEFID"])))
				{
					//이미 우선순위가 등록된 품목입니다.
					Language.LanguageMessageItem item = Language.GetMessage("AlreadyInputPriority");
					result.IsSucced = false;
					result.FailMessage = item.Message;
					result.Caption = item.Title;
				}
			}

			return result;
		}

		#endregion
	}
}
