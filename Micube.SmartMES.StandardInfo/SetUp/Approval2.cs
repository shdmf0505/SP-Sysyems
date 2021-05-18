#region using
using Micube.Framework.Net;
using Micube.Framework;
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

using DevExpress.XtraCharts;
using DevExpress.Utils;
using Micube.Framework.SmartControls.Validations;

using System.Globalization;
using DevExpress.Utils.Menu;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램 명 : 기준정보 > 사양진행관리 > 승인처리
    /// 업 무 설 명 : 
    /// 생  성  자 : 윤성원
    /// 생  성  일 : 2019-06-27
    /// 수 정 이 력 : 2019-11-27 정승원 설계변경
    /// </summary> 
	public partial class Approval2 : SmartConditionManualBaseForm
	{
        #region Local Variables
        private List<DXMenuItem> menuList = new List<DXMenuItem>();
        #endregion

        #region 생성자
        public Approval2()
		{
			InitializeComponent();
        }

		#endregion

		#region 컨텐츠 영역 초기화

		/// <summary>
		/// 컨텐츠 영역 초기화
		/// </summary>
		protected override void InitializeContent()
		{
			base.InitializeContent();

			InitializeEvent();
			InitializeGrid();

            // Context Menu Intialize
            InitializeQuickMenuList();

        }

		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitializeGrid()
        {
			#region 승인
			grdApproval.GridButtonItem = GridButtonItem.Export;

			//결재요청일
			grdApproval.View.AddTextBoxColumn("REQUESTDATE", 130).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
			//상태
			grdApproval.View.AddComboBoxColumn("APPROVALSTATUS", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ApprovalStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center).SetIsReadOnly().SetLabel("STATE");
			//작업구분
			grdApproval.View.AddComboBoxColumn("APPROVALTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=GovernanceType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
			//생산구분
			grdApproval.View.AddComboBoxColumn("PRODUCTIONTYPE", 70, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
			//고객사
			grdApproval.View.AddTextBoxColumn("CUSTOMERNAME", 200).SetIsReadOnly();
			//품목코드
			grdApproval.View.AddTextBoxColumn("ITEMID", 100).SetIsReadOnly();
			//품목버전
			grdApproval.View.AddTextBoxColumn("ITEMVERSION", 50).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
			//품목명
			grdApproval.View.AddTextBoxColumn("ITEMNAME", 200).SetIsReadOnly();
			//사양담당자ID
			grdApproval.View.AddTextBoxColumn("SPECOWNER", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden().SetIsReadOnly();
			//사양담당자명
			grdApproval.View.AddTextBoxColumn("SPECOWNERNAME", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
			//검토자ID
			grdApproval.View.AddTextBoxColumn("REVIEWERID", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden().SetIsReadOnly();
			//검토자명
			grdApproval.View.AddTextBoxColumn("REVIEWERNAME", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
			//승인자ID
			grdApproval.View.AddTextBoxColumn("APPROVER", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden().SetIsReadOnly();
			//승인자명
			grdApproval.View.AddTextBoxColumn("APPROVERNAME", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
			//승인일
			grdApproval.View.AddTextBoxColumn("APPROVEDATE", 120).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
			//반려사유
			grdApproval.View.AddTextBoxColumn("REJECTREASON", 200);

			grdApproval.View.PopulateColumns();

			#endregion

			#region 승인현황
			grdApprovaltransaction.GridButtonItem = GridButtonItem.Export;
			grdApprovaltransaction.View.SetIsReadOnly();

            //승인상태
            grdApprovaltransaction.View.AddComboBoxColumn("ACCEPTSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ApprovalStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetTextAlignment(TextAlignment.Center);
            
            //승인자ID
            grdApprovaltransaction.View.AddTextBoxColumn("ACTOR", 70).SetIsHidden();
			//승인자명
			grdApprovaltransaction.View.AddTextBoxColumn("ACCEPTUSER", 100).SetTextAlignment(TextAlignment.Center);
			//승인일
			grdApprovaltransaction.View.AddTextBoxColumn("ACCEPTDATE", 150).SetTextAlignment(TextAlignment.Center);
			//반려사유
			grdApprovaltransaction.View.AddTextBoxColumn("REJECTREASON", 250);
			//반려일
			//grdApprovaltransaction.View.AddTextBoxColumn("REJECTDATE", 130).SetTextAlignment(TextAlignment.Center);

			grdApprovaltransaction.View.PopulateColumns();
			#endregion
			
        }


        /// <summary>
        /// 퀵 메뉴 리스트 등록
        /// </summary>
        private void InitializeQuickMenuList()
        {
            // 품목, 품목사양, 라우팅
            menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SD-0290"), OpenForm) { BeginGroup = true, Tag = "PG-SD-0290" });
            menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SD-0292"), OpenForm) { BeginGroup = false, Tag = "PG-SD-0292" });
            menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SD-0350"), OpenForm) { BeginGroup = false, Tag = "PG-SD-0350" });
        }

        #endregion

        #region 이벤트

        private void InitializeEvent()
        {
            grdApproval.View.FocusedRowChanged += grdApproval_FocusedRowChanged;
            grdApproval.InitContextMenuEvent += GrdApproval_InitContextMenuEvent;
        }

        private void GrdApproval_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
        {
            for (int i = 0; i < menuList.Count; i++)
            {
                args.AddMenus.Add(menuList[i]);
            }
        }



        /// <summary>
        /// 승인 grid 포커스 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdApproval_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdApproval.View.FocusedRowHandle < 0)
                return;

			FocusedApprovalDetailData();
		}

		/// <summary>
		/// 승인 현황 조회
		/// </summary>
		private void FocusedApprovalDetailData()
		{
			DataRow row = grdApproval.View.GetFocusedDataRow();
			if(row == null) return;

			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
			param.Add("PLANTID", UserInfo.Current.Plant);
			//param.Add("APPROVALTYPE", row["APPROVALTYPE"].ToString());
			param.Add("APPROVALID", row["APPROVALID"].ToString());
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = SqlExecuter.Query("SelectApprovalTransactionList", "10002", param);
			grdApprovaltransaction.DataSource = dt;
		}
       
        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

			(grdApproval.DataSource as DataTable).AcceptChanges();
			(grdApprovaltransaction.DataSource as DataTable).AcceptChanges();

			int beforeHandle = grdApproval.View.FocusedRowHandle;

			//그리드 초기화
			grdApproval.View.ClearDatas();
			grdApprovaltransaction.View.ClearDatas();
			

			var values = this.Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
			values["P_PRODUCTNAME"] = Format.GetString(values["P_PRODUCTNAME"]).TrimEnd(',');

			DataTable dtApproval = await SqlExecuter.QueryAsync("SelectApprovalList", "10002", values);

            if (dtApproval.Rows.Count < 1) 
            {
				// 조회할 데이터가 없습니다.
				ShowMessage("NoSelectData");
            }
            
            this.grdApproval.DataSource = dtApproval;

			grdApproval.View.FocusedRowHandle = (beforeHandle <= 0) ? 0 : beforeHandle;
			FocusedApprovalDetailData();

		}
        #endregion


        /// <summary>
        /// 검색조건 초기화
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

			//고객사
			InitializeCondition_CustomerPopup();
			//제품범주
			//InitializeGrid_InventoryCgPopup();
			//품목코드
			InitializeCondition_ProductPopup();
			//사양담당
			InitializeCondition_Customer();
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 고객사
		/// </summary>
		private void InitializeCondition_CustomerPopup()
		{
			var conditionCustomer = Conditions.AddSelectPopup("P_CUSTOMER", new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
				.SetPopupLayout("SELECTCUSTOMERID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("CUSTOMERNAME")
				.SetLabel("CUSTOMERID")
				.SetPosition(2.5)
				.SetPopupResultCount(0);

			// 팝업 조회조건
			conditionCustomer.Conditions.AddTextBox("TXTCUSTOMERID");

			// 팝업 그리드
			conditionCustomer.GridColumns.AddTextBoxColumn("CUSTOMERID", 80);
			conditionCustomer.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 제품범주
		/// </summary>
		private void InitializeGrid_InventoryCgPopup()
		{
			var parentPopupColumn = Conditions.AddSelectPopup("P_INVENTORYCATEGORY", new SqlQuery("GetCategoryPopup", "10021", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetPopupLayout("INVENTORYCATEGORY", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupResultCount(1)
				.SetPopupResultMapping("INVENTORYCATEGORY", "CATEGORYID")
				.SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
				.SetPosition(2.6)
				.SetLabel("INVENTORYCATEGORY");

			parentPopupColumn.Conditions.AddComboBox("PARENTCATEGORYID", new SqlQuery("GetCategoryMidList", "10001", "TOPPARENTCATEGORYID=Inventory"), "CATEGORYNAME", "CATEGORYID").SetValidationIsRequired();
			parentPopupColumn.Conditions.AddTextBox("CATEGORYNAME");

			// 팝업 그리드 설정
			parentPopupColumn.GridColumns.AddTextBoxColumn("CATEGORYID", 150);
			parentPopupColumn.GridColumns.AddTextBoxColumn("CATEGORYNAME", 200);

		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 품목
		/// </summary>
		private void InitializeCondition_ProductPopup()
		{
			// SelectPopup 항목 추가
			var conditionProductId = Conditions.AddSelectPopup("P_ITEMID", new SqlQuery("GetItemMasterList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "ITEM", "ITEM")
				.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("ITEMNAME")
				.SetLabel("ITEMID")
				.SetPosition(3.2)
				.SetPopupResultCount(0)
				.SetPopupApplySelection((selectRow, gridRow) => {
					string productDefName = "";

					selectRow.AsEnumerable().ForEach(r => {
						productDefName += Format.GetString(r["ITEMNAME"]) + ",";
					});

					Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = productDefName;
				});

			// 팝업에서 사용되는 검색조건 (품목코드/명)
			conditionProductId.Conditions.AddTextBox("TXTITEM");
			

			// 품목코드
			conditionProductId.GridColumns.AddTextBoxColumn("ITEMID", 150);
			// 품목명
			conditionProductId.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
			// 품목버전
			conditionProductId.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);
		}
		
		/// <summary>
		/// 팝업형 조회조건 생성 - 사양담당
		/// </summary>
		private void InitializeCondition_Customer()
		{
			var conditionCustomer = Conditions.AddSelectPopup("P_SPECOWNER", new SqlQuery("GetUserList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "USERNAME", "USERID")
				.SetPopupLayout("SELECTSPECOWNER", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("DEPARTMENT")
				.SetLabel("USERID")
				.SetPosition(9.5)
				.SetPopupResultCount(0);

			// 팝업 조회조건
			conditionCustomer.Conditions.AddTextBox("USERIDNAME");

			// 팝업 그리드
			conditionCustomer.GridColumns.AddTextBoxColumn("USERID", 150);
			conditionCustomer.GridColumns.AddTextBoxColumn("USERNAME", 200);
			conditionCustomer.GridColumns.AddTextBoxColumn("DEPARTMENT", 200);
		}

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

			switch(btn.Name.ToString())
			{ 
            case "Approval":
            
                DataRow row = grdApproval.View.GetFocusedDataRow();

                if (UserInfo.Current.Id != Format.GetString(row["APPROVER"])
                    && UserInfo.Current.Id != Format.GetString(row["REVIEWERID"]))
                {
                    //승인 권한이 없습니다.
                    ShowMessage("NOTPERMISSIONAPPROVE");
                    return;
                }

                //DataTable approvalListDt = grdApprovaltransaction.DataSource as DataTable;
                //DataRow[] arrayRow = approvalListDt.AsEnumerable().Where(r => Format.GetString(r["ACTOR"]) == UserInfo.Current.Id).ToArray();
                //string state = (arrayRow.Count() > 0) ? Format.GetString(arrayRow[0]["ACCEPTSTATE"]) : string.Empty;
				DataRow currentRow = grdApproval.View.GetFocusedDataRow();
                if (Format.GetString(currentRow["APPROVALSTATUS"]).Equals("Approval"))
                {
                    //이미 승인 완료하였습니다.
                    ShowMessage("ALREADYAPPROVED");
                    return;
                }

				if(Format.GetString(currentRow["APPROVALSTATUS"]).Equals("Reject"))
				{
					//이미 반려된 항목입니다.
					ShowMessage("ALREADYREJECTED");
					return;
				}

				if((grdApprovaltransaction.DataSource as DataTable).Rows.Count == 3)
				{
					string lastApprover = Format.GetString(grdApprovaltransaction.View.GetRowCellValue(2, "ACTOR"));
					if (lastApprover.Equals(UserInfo.Current.Id))
					{
						string lastApproval = Format.GetString(grdApprovaltransaction.View.GetRowCellValue(1, "ACCEPTSTATE"));
						if(string.IsNullOrWhiteSpace(lastApproval))
						{
							//결제 순서가  올바르지 않습니다.
							ShowMessage("ApprovalSequenceInfo");
							return;
						}
						
					}
				}


                string type = string.Empty;
                if (Format.GetString(row["APPROVER"]).Equals(UserInfo.Current.Id))
                {
                    type = "Approver";
                }

                if (Format.GetString(row["REVIEWERID"]).Equals(UserInfo.Current.Id))
                {
                    type = "Reviewer";
                }

				DataTable dtDetail = grdApprovaltransaction.DataSource as DataTable;
				DataRow dataRow = null;
				switch(type)
				{
					case "Approver":
						dataRow = dtDetail.AsEnumerable().Where(r => Format.GetString(r["RESULTTYPE"]).Equals("Approver")).ToList()[0];
						break;
					case "Reviewer":
						dataRow = dtDetail.AsEnumerable().Where(r => Format.GetString(r["RESULTTYPE"]).Equals("Reviewer")).ToList()[0];
						break;
				}
				
				if(dataRow != null && dataRow["ACCEPTSTATE"].Equals("Approval"))
				{
					//이미 승인 완료하였습니다.
					ShowMessage("ALREADYAPPROVED");
					return;
				}


                DataTable dt = (grdApproval.DataSource as DataTable).Clone();
                DataRow newRow = dt.NewRow();
                newRow.ItemArray = row.ItemArray;
                dt.Rows.Add(newRow);

                MessageWorker worker = new MessageWorker("SaveApproval");
                worker.SetBody(new MessageBody()
				{
					{ "approver", UserInfo.Current.Id },
					{ "resultType", type },
					{ "approvalList", dt }
				});
                worker.Execute();

                ShowMessage("SuccessSave");

                OnSearchAsync();

				break;
            case "Return":
                DataRow rowReturn = grdApproval.View.GetFocusedDataRow();
				
				bool isReturn = false;
				DataTable transactionDt = grdApprovaltransaction.DataSource as DataTable;

				if(transactionDt.Rows.Count == 3)
				{
					//결재라인의 검토자와 로그인 사용자 비교
					transactionDt.AsEnumerable().ForEach(r =>
					{
						if (r["RESULTTYPE"].Equals("Reviewer"))
						{
							if (UserInfo.Current.Id != Format.GetString(rowReturn["REVIEWERID"]))
							{
								isReturn = true;
							}
						}
					});
				}
				else if(transactionDt.Rows.Count == 2)
				{
					//검토자가 없는 경우
					isReturn = true;
				}


				if(isReturn)
				{
					//반려 권한이 없습니다.
					ShowMessage("NOTPERMISSIONREJECT");
					return;
				}

                if (Format.GetString(rowReturn["APPROVALSTATUS"]).Equals("Reject"))
                {
                    //이미 반려된 항목입니다.
                    ShowMessage("ALREADYREJECTED");
                    return;
                }

				if (Format.GetString(rowReturn["APPROVALSTATUS"]).Equals("Approval"))
				{
					//이미 승인 완료하였습니다.
					ShowMessage("ALREADYAPPROVED");
					return;
				}

				string typeReturn = string.Empty;
                if (Format.GetString(rowReturn["APPROVER"]).Equals(UserInfo.Current.Id))
                {
					typeReturn = "Approver";
                }

                if (Format.GetString(rowReturn["REVIEWERID"]).Equals(UserInfo.Current.Id))
                {
					typeReturn = "Reviewer";
                }

                DataTable dtReturn = (grdApproval.DataSource as DataTable).Clone();
                DataRow newRowReturn = dtReturn.NewRow();
				newRowReturn.ItemArray = rowReturn.ItemArray;
				dtReturn.Rows.Add(newRowReturn);

                MessageWorker workerReturn = new MessageWorker("SaveApproval");
				workerReturn.SetBody(new MessageBody()
				{
					{ "rejector", UserInfo.Current.Id },
					{ "resultType", typeReturn },
					{ "approvalList", dtReturn }
				});
				workerReturn.Execute();

                ShowMessage("SuccessSave");

                OnSearchAsync();

				break;
            case "Decide":

                DataRow selectRow = grdApproval.View.GetFocusedDataRow();
                if (selectRow == null) return;

                if (UserInfo.Current.Id != Format.GetString(selectRow["REVIEWERID"]))
                {
                    //전결 처리 권한이 없습니다.
                    ShowMessage("NOTPERMISSIONMASTERAPPROVE");
                    return;
                }


                if (this.ShowMessageBox("DecideArbitrarily", "Information", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtDecide = (grdApproval.DataSource as DataTable).Clone();
                    DataRow newRowDecide = dtDecide.NewRow();
                    newRowDecide.ItemArray = selectRow.ItemArray;
                    dtDecide.Rows.Add(newRowDecide);

                    MessageWorker workerDecide = new MessageWorker("SaveApproval");
                    workerDecide.SetBody(new MessageBody()
                    {
                        { "reviewer", UserInfo.Current.Id },
                        { "resultType", "Reviewer" },
                        { "approvalList", dtDecide }
                    });
                    workerDecide.Execute();

                    ShowMessage("SuccessSave");

                    OnSearchAsync();
                }
				break;
			}

        }
        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

			//진행상태 다국어 변경
			SmartComboBox approvalState = Conditions.GetControl<SmartComboBox>("P_APPROVALSTATUS");
			approvalState.LanguageKey = "WORKORDERSTATUS";
			approvalState.LabelText = Language.Get(approvalState.LanguageKey);

			//사양담당자
			SmartSelectPopupEdit specOwner = Conditions.GetControl<SmartSelectPopupEdit>("P_SPECOWNER");
			specOwner.LanguageKey = "SPECOWNER";
			specOwner.LabelText = Language.Get(specOwner.LanguageKey);

            Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").ReadOnly = true;
            Conditions.GetControl<SmartSelectPopupEdit>("P_ITEMID").EditValueChanged += ProductDefIDChanged;

        }

        private void ProductDefIDChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit PopProdutid = sender as SmartSelectPopupEdit;

            if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
            {
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = string.Empty;
            }
        }

        /// <summary>
        /// Customizing Context Menu Item Click 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OpenForm(object sender, EventArgs args)
        {
            try
            {
                DialogManager.ShowWaitDialog();

                DataRow currentRow = grdApproval.View.GetFocusedDataRow();
                if (currentRow == null) return;

                string menuId = (sender as DXMenuItem).Tag.ToString();

                var param = currentRow.Table.Columns
                    .Cast<DataColumn>()
                    .ToDictionary(col => col.ColumnName, col => currentRow[col.ColumnName]);

                param.Add("CALLMENU", "Approval2");
                OpenMenu(menuId, param); //다른창 호출..
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                DialogManager.Close();
            }
        }

    }
}

