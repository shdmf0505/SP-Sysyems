#region using
using DevExpress.XtraTreeList.Nodes;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.StandardInfo.Popup;

using Newtonsoft.Json;
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
using DevExpress.XtraTreeList.Columns;
#endregion

namespace Micube.SmartMES.StandardInfo
{
	/// <summary>
	/// 프 로 그 램 명  : 기준정보 > 사양관리 > 재작업 라우팅 window time
	/// 업  무  설  명  : 
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2020-01-15
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class ReworkRoutingQueueTime : SmartConditionManualBaseForm
	{
        #region Local Variables
        DataTable dttreeSet = new DataTable();
        #endregion

        #region 생성자
        public ReworkRoutingQueueTime()
		{
			InitializeComponent();
		}
		#endregion

		#region 컨텐츠 영역 초기화
		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitializeGridList()
		{
			#region 라우팅 그리드 초기화
			grdProcessPath.GridButtonItem = GridButtonItem.Export;
			grdProcessPath.View.SetIsReadOnly();
			grdProcessPath.View.SetAutoFillColumn("PROCESSSEGMENTNAME");

			grdProcessPath.View.AddTextBoxColumn("PROCESSPATHID", 100).SetIsHidden();
            grdProcessPath.View.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
            grdProcessPath.View.AddTextBoxColumn("PROCESSDEFID", 100).SetIsHidden();
            grdProcessPath.View.AddTextBoxColumn("PROCESSDEFVERSION", 100).SetIsHidden();
            grdProcessPath.View.AddTextBoxColumn("PATHSEQUENCE", 100).SetIsHidden();
            grdProcessPath.View.AddTextBoxColumn("PATHTYPE", 100).SetIsHidden();
            grdProcessPath.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100).SetIsHidden();
            grdProcessPath.View.AddSpinEditColumn("USERSEQUENCE", 40).SetTextAlignment(TextAlignment.Center);
            grdProcessPath.View.AddTextBoxColumn("PROCESSSEGMENTID", 80).SetTextAlignment(TextAlignment.Center);
            grdProcessPath.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 250);
            grdProcessPath.View.PopulateColumns();
			#endregion

			#region W-Time
			grdWtime.GridButtonItem = GridButtonItem.All;
			InitializeGrid_ProductPopup();
			grdWtime.View.AddTextBoxColumn("PRODUCTDEFVERSION", 50).SetIsReadOnly();
			grdWtime.View.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetIsReadOnly();
			grdWtime.View.AddTextBoxColumn("WTIMEDEFID", 120).SetIsHidden();
            grdWtime.View.AddTextBoxColumn("WTIMEDEFNAME", 100);
            grdWtime.View.AddTextBoxColumn("DESCRIPTION", 100).SetIsHidden();
            grdWtime.View.AddTextBoxColumn("AREAID", 100).SetIsHidden();
            grdWtime.View.AddTextBoxColumn("USERSEQUENCE", 60).SetIsHidden();
            grdWtime.View.AddTextBoxColumn("PROCESSDEFID", 100).SetIsHidden();
            grdWtime.View.AddTextBoxColumn("PROCESSDEFVERSION", 100).SetIsHidden();
            grdWtime.View.AddTextBoxColumn("FR_USERSEQUENCE", 60).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdWtime.View.AddTextBoxColumn("PROCESSPATHID", 100).SetIsHidden();
			grdWtime.View.AddTextBoxColumn("HIDDEN_PROCESSSEGMENTID").SetIsReadOnly().SetIsHidden();
			InitializeGrid_ProcesssegMnetFromPopup();
			grdWtime.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 100).SetIsReadOnly();
            grdWtime.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100).SetIsHidden();
            grdWtime.View.AddComboBoxColumn("PROCESSSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=4Step", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetTextAlignment(TextAlignment.Center);
            grdWtime.View.AddTextBoxColumn("TOPROCESSSEGMENTID", 100).SetIsHidden();
            grdWtime.View.AddTextBoxColumn("TOPROCESSDEFID", 100).SetIsHidden();
            grdWtime.View.AddTextBoxColumn("TOPROCESSDEFVERSION", 100).SetIsHidden();
            grdWtime.View.AddTextBoxColumn("TOPROCESSPATHID", 100).SetIsHidden();
            grdWtime.View.AddTextBoxColumn("TO_USERSEQUENCE", 60).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            InitializeGrid_ProcesssegMnetToPopup();
            grdWtime.View.AddTextBoxColumn("TOPROCESSSEGMENTNAME", 100).SetIsReadOnly();
            grdWtime.View.AddTextBoxColumn("TOPROCESSSEGMENTVERSION").SetIsHidden();
            grdWtime.View.AddComboBoxColumn("TOPROCESSSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=4Step", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdWtime.View.AddComboBoxColumn("WTIMETYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=QtimeType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdWtime.View.AddSpinEditColumn("WTIMELIMIT", 100).SetDisplayFormat("#,##0.#########").SetValidationIsRequired();
            grdWtime.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdWtime.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdWtime.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdWtime.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
            grdWtime.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly()
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center);

            grdWtime.View.PopulateColumns();
			#endregion

			#region W-time Action
			grdWtimeAction.GridButtonItem = GridButtonItem.All;
            grdWtimeAction.View.AddTextBoxColumn("WTIMEDEFID", 100).SetIsHidden();
            InitializeGrid_ActionPopup();
            grdWtimeAction.View.AddTextBoxColumn("WTIMEACTIONNAME", 200);
            grdWtimeAction.View.AddSpinEditColumn("ACTIONSEQUENCE", 100).SetValidationIsRequired();
            grdWtimeAction.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdWtimeAction.View.PopulateColumns();
			#endregion
		}

		/// <summary>
		/// 팝업형 그리드 컬럼 초기화 - 품목코드
		/// </summary>
		private void InitializeGrid_ProductPopup()
		{
			// SelectPopup 항목 추가
			var conditionProductId = grdWtime.View.AddSelectPopupColumn("PRODUCTDEFID", 100, new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"))
				.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("PRODUCTDEFNAME")
				.SetLabel("PRODUCTDEFID")
				.SetPopupResultCount(0)
				.SetPopupApplySelection((selectRow, gridRow) => 
				{
					/*
					 if(selectRow.Count() > 0)
					{
						DataTable originalDt = grdWtime.DataSource as DataTable;
						int handle = grdWtime.View.FocusedRowHandle;
						DataRow dr = grdWtime.View.GetFocusedDataRow();

						foreach(DataRow r in selectRow)
						{
							DataRow newRow = null;
							if(originalDt.Rows.Count < handle + 1)
							{
								newRow = originalDt.NewRow();
								originalDt.Rows.Add(newRow);
							}
							else
							{
								newRow = grdWtime.View.GetDataRow(handle);
							}

							newRow["PRODUCTDEFID"] = r["PRODUCTDEFID"];
							newRow["PRODUCTDEFVERSION"] = r["PRODUCTDEFVERSION"];
							newRow["PRODUCTDEFNAME"] = r["PRODUCTDEFNAME"];

							newRow["PROCESSDEFID"] = dr["PROCESSDEFID"];
							newRow["PROCESSDEFVERSION"] = dr["PROCESSDEFVERSION"];

							newRow["TOPROCESSDEFID"] = dr["PROCESSDEFID"];
							newRow["TOPROCESSDEFVERSION"] = dr["PROCESSDEFID"];

							newRow["PROCESSSEGMENTVERSION"] = "*";
							newRow["TOPROCESSSEGMENTVERSION"] = "*";

							newRow["WTIMETYPE"] = "Max";
							newRow["VALIDSTATE"] = "Valid";

							
							grdWtime.View.RaiseValidateRow();

							handle++;
						}
					}
					 */
					DataTable originalDt = grdWtime.DataSource as DataTable;
					DataTable dt = originalDt.Clone();
					int handle = grdWtime.View.FocusedRowHandle;
					DataRow dr = grdWtime.View.GetFocusedDataRow();

					selectRow.AsEnumerable().ForEach(r => 
					{
						DataRow newRow = dt.NewRow();

						newRow["PRODUCTDEFID"] = r["PRODUCTDEFID"];
						newRow["PRODUCTDEFVERSION"] = r["PRODUCTDEFVERSION"];
						newRow["PRODUCTDEFNAME"] = r["PRODUCTDEFNAME"];

						newRow["PROCESSDEFID"] = dr["PROCESSDEFID"];
						newRow["PROCESSDEFVERSION"] = dr["PROCESSDEFVERSION"];

						newRow["TOPROCESSDEFID"] = dr["PROCESSDEFID"];
						newRow["TOPROCESSDEFVERSION"] = dr["PROCESSDEFID"];

						newRow["PROCESSSEGMENTVERSION"] = "*";
						newRow["TOPROCESSSEGMENTVERSION"] = "*";

						newRow["WTIMETYPE"] = "Max";
						newRow["VALIDSTATE"] = "Valid";


						dt.Rows.Add(newRow);
					});

					//originalDt = originalDt.AsEnumerable().Where(r => string.IsNullOrWhiteSpace(Format.GetString(r["PRODUCTDEFID"])) == false).CopyToDataTable();

					originalDt.Rows.RemoveAt(handle);
					originalDt.Merge(dt);

					for (int i = 0; i < originalDt.Rows.Count; i++)
					{
						grdWtime.View.RaiseValidateRow(i);
					}
				});

			conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
			conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
				.SetDefault("Product");

			// 품목코드
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
			// 품목명
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200); 
			// 품목버전
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
			// 품목유형구분
			conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
			// 생산구분
			conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
		}

		/// <summary>
		/// 팝업형 그리드 컬럼 초기화 - WTIME ACTION
		/// </summary>
		private void InitializeGrid_ActionPopup()
        {
            var Action = this.grdWtimeAction.View.AddSelectPopupColumn("WTIMEACTIONID", 150, new SqlQuery("GetWtimeAction", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
            .SetPopupLayout("WTIMEACTION", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupResultCount(0)
            .SetPopupResultMapping("WTIMEACTIONID", "ACTIONID")
			.SetPopupAutoFillColumns("ACTIONNAME")
            .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
			.SetValidationIsRequired()
            .SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
				DataTable originalDt = grdWtimeAction.DataSource as DataTable;
				DataTable dt = originalDt.Clone();
				int handle = grdWtimeAction.View.FocusedRowHandle;
				int seq = originalDt.Rows.Count;

				DataRow dr = grdWtime.View.GetFocusedDataRow();

				selectedRows.AsEnumerable().ForEach(r =>
				{
					DataRow newRow = dt.NewRow();
					newRow["WTIMEDEFID"] = dr["WTIMEDEFID"];
					newRow["WTIMEACTIONID"] = r["ACTIONID"];
					newRow["WTIMEACTIONNAME"] = r["ACTIONNAME"];
					newRow["ACTIONSEQUENCE"] = seq++;
					dt.Rows.Add(newRow);
				});

				originalDt.Rows.RemoveAt(handle);
				originalDt.Merge(dt);
				grdWtimeAction.View.RaiseValidateRow();
			});

            Action.Conditions.AddTextBox("ACTIONID");
            Action.Conditions.AddTextBox("ACTIONNAME");
            Action.Conditions.AddTextBox("WTIMEDEFID")
                .SetPopupDefaultByGridColumnId("WTIMEDEFID")
                .SetIsHidden();

            // 팝업 그리드 설정   
            Action.GridColumns.AddTextBoxColumn("ACTIONID", 100);
            Action.GridColumns.AddTextBoxColumn("ACTIONNAME", 300);

        }

		/// <summary>
		/// 팝업형 그리드 컬럼 초기화 - FROM 공정
		/// </summary>
		private void InitializeGrid_ProcesssegMnetFromPopup()
        {
            var ProcesssegMnet = this.grdWtime.View.AddSelectPopupColumn("PROCESSSEGMENTID", new SqlQuery("GetRoutingQueueTimeProcesssegmentPopup", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            .SetPopupLayout("PROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupResultCount(1)
            .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
            .SetValidationIsRequired()
            .SetPopupValidationCustom(ValidationProcesssegMnetFromPopup);


            ProcesssegMnet.Conditions.AddTextBox("PROCESSSEGMENTID");
            ProcesssegMnet.Conditions.AddTextBox("PROCESSSEGMENTNAME");
            ProcesssegMnet.Conditions.AddTextBox("PROCESSDEFID").SetPopupDefaultByGridColumnId("PROCESSDEFID").SetIsHidden();
            ProcesssegMnet.Conditions.AddTextBox("PROCESSDEFVERSION").SetPopupDefaultByGridColumnId("PROCESSDEFVERSION").SetIsHidden();
			ProcesssegMnet.Conditions.AddTextBox("CURRENT_PROCESSSEGMENTID").SetPopupDefaultByGridColumnId("HIDDEN_PROCESSSEGMENTID").SetIsHidden().SetLabel("PROCESSSEGMENTID");

			// 팝업 그리드 설정   
			ProcesssegMnet.GridColumns.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
            ProcesssegMnet.GridColumns.AddTextBoxColumn("USERSEQUENCE", 80).SetTextAlignment(TextAlignment.Center);
            ProcesssegMnet.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 100).SetTextAlignment(TextAlignment.Center);
            ProcesssegMnet.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 300);
            ProcesssegMnet.GridColumns.AddTextBoxColumn("PROCESSPATHID", 300).SetIsHidden();
        }

		/// <summary>
		/// 팝업형 그리드 컬럼 초기화 - TO 공정
		/// </summary>
		private void InitializeGrid_ProcesssegMnetToPopup()
        {
            var parentProcesssegMnet = this.grdWtime.View.AddSelectPopupColumn("TOPROCESSSEGMENTID", new SqlQuery("GetRoutingQueueTimeProcesssegmentPopup", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}",$"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            .SetPopupLayout("TOPROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupResultCount(1)
             .SetPopupResultMapping("TOPROCESSSEGMENTID", "PROCESSSEGMENTID")
            .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
            .SetValidationIsRequired()
            .SetPopupValidationCustom(ValidationProcesssegMnetToPopup);

            // 팝업에서 사용할 조회조건 항목 추가
            parentProcesssegMnet.Conditions.AddTextBox("PROCESSSEGMENTID");
            parentProcesssegMnet.Conditions.AddTextBox("PROCESSSEGMENTNAME");

            parentProcesssegMnet.Conditions.AddTextBox("PROCESSDEFID").SetPopupDefaultByGridColumnId("PROCESSDEFID").SetIsHidden();
            parentProcesssegMnet.Conditions.AddTextBox("PROCESSDEFVERSION").SetPopupDefaultByGridColumnId("PROCESSDEFVERSION").SetIsHidden();
            parentProcesssegMnet.Conditions.AddTextBox("USERSEQUENCE").SetPopupDefaultByGridColumnId("FR_USERSEQUENCE").SetIsHidden();

            // 팝업 그리드 설정
            parentProcesssegMnet.GridColumns.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
            parentProcesssegMnet.GridColumns.AddTextBoxColumn("USERSEQUENCE", 80).SetTextAlignment(TextAlignment.Center);
            parentProcesssegMnet.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 100).SetTextAlignment(TextAlignment.Center);
            parentProcesssegMnet.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 300);

        }

		/// <summary>
		/// 설정 초기화
		/// </summary>
		protected override void InitializeContent()
		{
			base.InitializeContent();

			InitializeGridList();
            InitializeEvent();
        }

        #endregion

        #region 이벤트
        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
		{
			grdProcessPath.View.FocusedRowChanged += View_FocusedRowChanged;
            grdWtime.View.FocusedRowChanged += grdQtimedefinition_FocusedRowChanged;
			grdWtime.View.AddingNewRow += grdQtimedefinition_AddingNewRow;
			(grdWtime.View.Columns["PRODUCTDEFID"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit).ButtonClick += ReworkRoutingQueueTime_ButtonClick;
			(grdWtime.View.Columns["PROCESSSEGMENTID"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit).ButtonClick += RoutingQueueTime_ButtonClick;
			(grdWtime.View.Columns["PROCESSSEGMENTID"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit).ButtonClick += RoutingQueueTime_ButtonClick1;

			grdWtimeAction.View.AddingNewRow += View_AddingNewRow;
		}

		/// <summary>
		/// TO 공정 팝업 그리드 컬럼 x 눌렀을 때
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RoutingQueueTime_ButtonClick1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
			{
				DataRow row = grdWtime.View.GetFocusedDataRow();
				row["TOPROCESSSEGMENTVERSION"] = string.Empty;
				row["TOPROCESSSEGMENTNAME"] = string.Empty;
			}
		}

		/// <summary>
		/// from 공정 팝업 그리드 컬럼 x 눌렀을 때
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RoutingQueueTime_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
			{
				DataRow row = grdWtime.View.GetFocusedDataRow();
				row["PROCESSSEGMENTVERSION"] = string.Empty;
				row["PROCESSSEGMENTNAME"] = string.Empty;
			}
		}

		/// <summary>
		/// ACTION 추가 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
		{
			DataRow selectRow = grdWtime.View.GetFocusedDataRow();
			if(selectRow == null)
			{
				args.IsCancel = true;
				return;
			}

			args.NewRow["WTIMEDEFID"] = selectRow["WTIMEDEFID"];
		}

		/// <summary>
		/// 라우팅 그리드 row 포커스 이동 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			//if(grdProcessPath.View.FocusedRowHandle < 0) return;

			//DataRow selectRow = grdProcessPath.View.GetFocusedDataRow();
			//if(selectRow == null) return;

			//DataTable dtWtime = grdWtime.GetChangedRows();
			//DataTable dtAction = grdWtimeAction.GetChangedRows();

			//if (dtWtime.Rows.Count > 0 || dtAction.Rows.Count > 0)
			//{
			//	DialogResult result = ShowMessageBox("WRITTENBEDELETE", Language.Get("MESSAGEINFO"), MessageBoxButtons.OKCancel);
			//	if (result == DialogResult.Cancel)
			//	{
			//		grdProcessPath.View.FocusedRowChanged -= View_FocusedRowChanged;
			//		grdProcessPath.View.FocusedRowHandle = e.PrevFocusedRowHandle;
			//		grdProcessPath.View.SelectRow(e.PrevFocusedRowHandle);
			//		grdProcessPath.View.FocusedRowChanged += View_FocusedRowChanged;
			//		return;
			//	}
			//}


			//grdWtime.View.ClearDatas();
			//grdWtimeAction.View.ClearDatas();

			////w-time 정의 조회
			//FocusedDataBindWtime(selectRow);
		}


		/// <summary>
		/// 품목 팝업 그리드 컬럼 x 눌렀을 때 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ReworkRoutingQueueTime_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
			{
				DataRow row = grdWtime.View.GetFocusedDataRow();
				if(row == null) return;
				row["PRODUCTDEFVERSION"] = string.Empty;
				row["PRODUCTDEFNAME"] = string.Empty;
			}
		}

		/// <summary>
		/// w-time 정의 row 포커스 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void grdQtimedefinition_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdWtime.View.FocusedRowHandle < 0) return;

            DataRow dataRow = grdWtime.View.GetFocusedDataRow();
			if(dataRow == null) return;

			DataTable dtAction = grdWtimeAction.GetChangedRows();
			if (dtAction.Rows.Count > 0)
			{
				DialogResult result = ShowMessageBox("WRITTENBEDELETE", Language.Get("MESSAGEINFO"), MessageBoxButtons.OKCancel);
				if (result == DialogResult.Cancel)
				{
					grdWtime.View.FocusedRowChanged -= grdQtimedefinition_FocusedRowChanged;
					grdWtime.View.FocusedRowHandle = e.PrevFocusedRowHandle;
					grdWtime.View.SelectRow(e.PrevFocusedRowHandle);
					grdWtime.View.FocusedRowChanged += grdQtimedefinition_FocusedRowChanged;
					return;
				}
			}

			grdWtimeAction.View.ClearDatas();

			//w-time 액션 조회
			FocusedDataBindWtimeAction(dataRow);
		}


        private void grdQtimedefinition_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
			DataRow selectRow = grdProcessPath.View.GetFocusedDataRow();
			if(selectRow == null)
			{
				args.IsCancel = true;
				return;
			}

			args.NewRow["PROCESSDEFID"] = selectRow["PROCESSDEFID"];
            args.NewRow["PROCESSDEFVERSION"] = selectRow["PROCESSDEFVERSION"];

			args.NewRow["TOPROCESSDEFID"] = selectRow["PROCESSDEFID"];
			args.NewRow["TOPROCESSDEFVERSION"] = selectRow["PROCESSDEFID"];

            //args.NewRow["PROCESSSEGMENTVERSION"] = "*";  // 2021-02-08 오근영 추가
            args.NewRow["PROCESSSEGMENTVERSION"] = selectRow["PROCESSSEGMENTVERSION"];  // 2021-02-08 오근영 추가
            args.NewRow["TOPROCESSSEGMENTVERSION"] = "*";

			args.NewRow["WTIMETYPE"] = "Max";
			args.NewRow["VALIDSTATE"] = "Valid";

			args.NewRow["HIDDEN_PROCESSSEGMENTID"] = selectRow["PROCESSSEGMENTID"];
            args.NewRow["PROCESSSEGMENTID"] = selectRow["PROCESSSEGMENTID"];  // 2021-02-08 오근영 추가
            args.NewRow["PROCESSSEGMENTNAME"] = selectRow["PROCESSSEGMENTNAME"];  // 2021-02-08 오근영 추가
            args.NewRow["FR_USERSEQUENCE"] = selectRow["USERSEQUENCE"];  // 2021-02-08 오근영 추가
        }

        #endregion

        #region 조회조건 영역

        /// <summary>
        /// 조회조건 영역 초기화 시작
        /// </summary>
        protected override void InitializeCondition()
		{
			base.InitializeCondition();

			//재작업 라우팅
			InitializeCondition_ProcessDefPopup();
        }
              
        /// <summary>
        /// 팝업형 조회조건 생성 - 재작업 라우팅
        /// </summary>
        private void InitializeCondition_ProcessDefPopup()
        {
            // SelectPopup 항목 추가
            var conditionProductId = Conditions.AddSelectPopup("P_REWORKROUTING", new SqlQuery("GetProcessDefList", "10031", "PROCESSDEFTYPE=Rework", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}"), "REWORKNUMBER", "REWORKNUMBER")
                .SetPopupLayout("SELECTREWORKROUTING", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("REWORKNAME")
                .SetLabel("REWORKNUMBER")
                .SetPosition(1.1)
                .SetValidationIsRequired()
                .SetPopupResultCount(1)
				.SetPopupApplySelection((selectRow, gridRow) => 
				{
					DataRow r = selectRow.FirstOrDefault();
					if(r == null)
					{
						Conditions.GetControl<SmartTextBox>("P_REWORKROUTINGVERSION").EditValue = string.Empty;
						Conditions.GetControl<SmartTextBox>("P_REWORKROUTINGNAME").EditValue = string.Empty;
						return;
					}

					Conditions.GetControl<SmartTextBox>("P_REWORKROUTINGVERSION").EditValue = r["REWORKVERSION"];
					Conditions.GetControl<SmartTextBox>("P_REWORKROUTINGNAME").EditValue = r["REWORKNAME"];
				});

            conditionProductId.Conditions.AddTextBox("TXTREWORKPROCESSDEF");

			//적용구분
			conditionProductId.GridColumns.AddTextBoxColumn("APPLICATIONTYPE", 60).SetTextAlignment(TextAlignment.Center);
			//재작업구분
			conditionProductId.GridColumns.AddTextBoxColumn("REWORKTYPE", 100);
			//대공정
			conditionProductId.GridColumns.AddTextBoxColumn("TOPPROCESSSEGMENTCLASS", 80);
			//재작업번호
			conditionProductId.GridColumns.AddTextBoxColumn("REWORKNUMBER", 150);
            //재작업버전
            conditionProductId.GridColumns.AddTextBoxColumn("REWORKVERSION", 60).SetTextAlignment(TextAlignment.Center);
			//재작업명
			conditionProductId.GridColumns.AddTextBoxColumn("REWORKNAME", 200);
		}


        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

			SmartTextBox txtVersion = Conditions.GetControl<SmartTextBox>("P_REWORKROUTINGVERSION");
			SmartTextBox txtName = Conditions.GetControl<SmartTextBox>("P_REWORKROUTINGNAME");

			txtVersion.ReadOnly = true;
			txtName.ReadOnly = true;

			Conditions.GetControl<SmartSelectPopupEdit>("P_REWORKROUTING").EditValueChanged += ReworkRoutingQueueTime_EditValueChanged;
		}

		/// <summary>
		/// 재작업 라우팅 조회조건  x 클릭 시 관련 다른 조회조건 같이 삭제
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ReworkRoutingQueueTime_EditValueChanged(object sender, EventArgs e)
		{
			SmartSelectPopupEdit routing = sender as SmartSelectPopupEdit;

			if (Format.GetFullTrimString(routing.EditValue).Equals(string.Empty))
			{
				Conditions.GetControl<SmartTextBox>("P_REWORKROUTINGNAME").EditValue = string.Empty;
				Conditions.GetControl<SmartTextBox>("P_REWORKROUTINGVERSION").EditValue = string.Empty;

				grdProcessPath.View.ClearDatas();
				grdWtime.View.ClearDatas();
				grdWtimeAction.View.ClearDatas();
			}
		}

		#endregion

		#region 툴바
		/// <summary>
		/// 저장버튼 클릭
		/// </summary>
		protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

			DataRow drSegment = grdProcessPath.View.GetFocusedDataRow();
			if(drSegment == null)
			{
				//저장할 데이터가 없습니다.
				throw MessageException.Create("NoSaveData");
			}

			DataTable dtWtime = grdWtime.GetChangedRows();
			DataTable dtAction = grdWtimeAction.GetChangedRows();

			bool isMulti = false;
			int iState = dtWtime.AsEnumerable().Where(r => r["_STATE_"].Equals("added")).Count();
			int iState2 = dtAction.AsEnumerable().Where(r => r["_STATE_"].Equals("added")).Count();
			if (iState > 0 && iState2 > 0)
			{
				isMulti = true;
			}

			MessageWorker worker = new MessageWorker("SaveReworkRoutingWtime");
			worker.SetBody(new MessageBody()
			{
				{ "enterpriseId", UserInfo.Current.Enterprise },
				{ "plantId", UserInfo.Current.Plant },
				{ "wtimeList", dtWtime },
				{ "actionList", dtAction },
				{ "isMulti", isMulti }
			});

			worker.Execute();
        }
		#endregion

		#region 검색

		/// <summary>
		/// 비동기 override 모델
		/// </summary>
		protected async override Task OnSearchAsync()
		{
            await base.OnSearchAsync();

			int prevHandle = grdProcessPath.View.FocusedRowHandle;
			int prevHandle2 = grdWtime.View.FocusedRowHandle;

			//초기화
			grdProcessPath.View.ClearDatas();
            grdWtimeAction.View.ClearDatas();
            grdWtime.View.ClearDatas();

            var values = Conditions.GetValues();	
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
			values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

			DataTable dtReworkRoutingList = SqlExecuter.Query("GetProcessPathList", "10011", values);
			if(dtReworkRoutingList.Rows.Count < 1)
			{
				// 조회할 데이터가 없습니다.
				ShowMessage("NoSelectData");
			}

			grdProcessPath.DataSource = dtReworkRoutingList;

			if(dtReworkRoutingList.Rows.Count > 0)
			{
				int iHandle = (prevHandle <= 0) ? 0 : prevHandle;
				grdProcessPath.View.FocusedRowChanged -= View_FocusedRowChanged;
				grdProcessPath.View.FocusedRowHandle = iHandle;
				grdProcessPath.View.SelectRow(iHandle);
				grdProcessPath.View.FocusedRowChanged += View_FocusedRowChanged;

				DataRow currentRow = grdProcessPath.View.GetDataRow(iHandle);
				if(currentRow != null)
				{
					FocusedDataBindWtime(currentRow);
				}

				int iHandle2 = (prevHandle2 <= 0) ? 0 : prevHandle2;
				grdWtime.View.FocusedRowChanged -= grdQtimedefinition_FocusedRowChanged;
				grdWtime.View.FocusedRowHandle = iHandle2;
				grdWtime.View.SelectRow(iHandle2);
				grdWtime.View.FocusedRowChanged += grdQtimedefinition_FocusedRowChanged;

				DataRow currentRow2 = grdWtime.View.GetDataRow(iHandle2);
				if(currentRow2 != null)
				{
					FocusedDataBindWtimeAction(currentRow2);
				}
				
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

			grdWtime.View.CheckValidation();
			grdWtimeAction.View.CheckValidation();
        }
		#endregion

		#region private Fuction

		/// <summary>
		/// w-tiem 정의 조회
		/// </summary>
		/// <param name="row"></param>
		private void FocusedDataBindWtime(DataRow row)
		{
			Dictionary<string, object> param = new Dictionary<string, object>();
			//param.Add("PROCESSSEGMENTID", row["PROCESSSEGMENTID"]);
			param.Add("PROCESSDEFID", row["PROCESSDEFID"]);
			param.Add("PROCESSDEFVERSION", row["PROCESSDEFVERSION"]);
			//param.Add("PROCESSPATHID", row["PROCESSPATHID"]);
			param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

			grdWtime.DataSource = SqlExecuter.Query("GetWindowTimeList", "10001", param);
		}

		/// <summary>
		/// w-time 액션 조회
		/// </summary>
		/// <param name="row"></param>
		private void FocusedDataBindWtimeAction(DataRow row)
		{
			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("WTIMEDEFID", row["WTIMEDEFID"]);

			grdWtimeAction.DataSource = SqlExecuter.Query("GetWindowActionList", "10001", param);
		}

		/// <summary>
		/// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 (표준공정)
		/// </summary>
		/// <param name="currentGridRow">화면 grid row</param>
		/// <param name="popupSelections">팝업에서 선택한 row</param>
		/// <returns></returns>
		private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationProcesssegMnetPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
		{
			Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["PROCESSSEGMENTNAME"] = row["PROCESSSEGMENTNAME"];
            }
            return result;
		}

        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 작업장)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationAreaPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["AREANAME"] = row["AREANAME"];
            }
            return result;
        }

        
        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 작업장)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationProcesssegMnetToPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
				currentGridRow["TO_USERSEQUENCE"] = row["USERSEQUENCE"];
                currentGridRow["TOPROCESSPATHID"] = row["PROCESSPATHID"];
                currentGridRow["TOPROCESSSEGMENTNAME"] = row["PROCESSSEGMENTNAME"];
            }
            return result;
        }

        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 작업장)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationProcesssegMnetFromPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["PLANTID"] = row["PLANTID"];
                currentGridRow["USERSEQUENCE"] = row["USERSEQUENCE"];
                currentGridRow["FR_USERSEQUENCE"] = row["USERSEQUENCE"];
                currentGridRow["PROCESSPATHID"] = row["PROCESSPATHID"];
                currentGridRow["PROCESSSEGMENTNAME"] = row["PROCESSSEGMENTNAME"];
				currentGridRow["HIDDEN_PROCESSSEGMENTID"] = row["PROCESSSEGMENTID"];
            }
            return result;
        }

        #endregion
    }
}

