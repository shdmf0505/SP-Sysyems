#region using

using DevExpress.Utils.Menu;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
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
    /// 프 로 그 램 명  : 공정관리 > 생산실적 > 재작업이력조회
    /// 업  무  설  명  : 재작업 이력을 조회한다.
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2019-10-08
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReworkHistory : SmartConditionManualBaseForm
    {
		#region Local Variables
		//메뉴 리스트
		List<DXMenuItem> menuList = new List<DXMenuItem>();
		#endregion

		#region 생성자

		public ReworkHistory()
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
			InitializeQuickMenuList();

		}

        /// <summary>        
        /// 재작업 이력 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            #region 재작업 이력
            grdReworkHistory.GridButtonItem = GridButtonItem.Export;
			grdReworkHistory.View.SetIsReadOnly();
            grdReworkHistory.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            // 처리일시
            grdReworkHistory.View.AddTextBoxColumn("CREATEDTIME", 140).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center).SetLabel("OSPPRINTTIME");
            //품목코드
            grdReworkHistory.View.AddTextBoxColumn("PRODUCTDEFID", 160).SetLabel("ITEMID");
            //버전
            grdReworkHistory.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetLabel("PRODUCTREVISION");
            //품목명
            grdReworkHistory.View.AddTextBoxColumn("PRODUCTDEFNAME", 230).SetLabel("ITEMNAME");
            //LOTID
            grdReworkHistory.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            //작업구분
            grdReworkHistory.View.AddTextBoxColumn("WORKTYPE", 60).SetTextAlignment(TextAlignment.Center);
            //재작업번호
            grdReworkHistory.View.AddTextBoxColumn("SUBPROCESSDEFID", 100).SetLabel("PROCESSDEFID_R").SetTextAlignment(TextAlignment.Center);
            //재작업버전
            grdReworkHistory.View.AddTextBoxColumn("SUBPROCESSDEFVERSION", 50).SetTextAlignment(TextAlignment.Center).SetLabel("PRODUCTREVISION");
            //투입공정
            grdReworkHistory.View.AddTextBoxColumn("FROMPROCESSSEGMENTNAME", 100).SetTextAlignment(TextAlignment.Left).SetLabel("INPUTSEGMENT");
            //재작업후공정
            grdReworkHistory.View.AddTextBoxColumn("TOPROCESSSEGMENTNAME", 100).SetTextAlignment(TextAlignment.Left).SetLabel("REWORKRETURNSEGMENT");
            //재작업명
            grdReworkHistory.View.AddTextBoxColumn("PROCESSDEFNAME", 130).SetTextAlignment(TextAlignment.Left).SetLabel("PROCESSDEFNAME_R");
            //차수
            grdReworkHistory.View.AddSpinEditColumn("WORKCOUNT", 50).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:#,##0}").SetLabel("DEGREE");
            //재작업 상태
            grdReworkHistory.View.AddTextBoxColumn("MOVESTATE", 80).SetTextAlignment(TextAlignment.Center);
            //처리 PCS
            grdReworkHistory.View.AddSpinEditColumn("PCSQTY", 100).SetDisplayFormat("{0:#,##0}").SetTextAlignment(TextAlignment.Right);
            //처리 PNL
            grdReworkHistory.View.AddSpinEditColumn("PANELQTY", 100).SetDisplayFormat("{0:#,##0}").SetTextAlignment(TextAlignment.Right);
            //재작업 인계작업장
            grdReworkHistory.View.AddTextBoxColumn("AREANAME", 120).SetLabel("TRANSITAREA");
            //처리자
            grdReworkHistory.View.AddTextBoxColumn("USERNAME", 80).SetTextAlignment(TextAlignment.Center).SetLabel("PROCESSUSER");
            //승인자
            grdReworkHistory.View.AddTextBoxColumn("APPROVEDUSER", 80).SetTextAlignment(TextAlignment.Center).SetLabel("APPROVER");
            //승인일자
            grdReworkHistory.View.AddTextBoxColumn("APPROVEDDATE", 140).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdReworkHistory.View.PopulateColumns();
            #endregion 재작업 이력
            #region 잠시 주석처리 나중에 삭제 할것임
            ////         // 처리일시
            ////         grdReworkHistory.View.AddTextBoxColumn("PROCESSDATE", 130).SetTextAlignment(TextAlignment.Center).SetLabel("RECEIVETIME");
            ////         //품목코드
            ////         grdReworkHistory.View.AddTextBoxColumn("PRODUCTDEFID", 130).SetLabel("ITEMID");
            ////         //품목명
            ////         grdReworkHistory.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetLabel("ITEMNAME");
            ////         //LOTID
            ////         grdReworkHistory.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
            ////         //부모 LOT
            ////         grdReworkHistory.View.AddTextBoxColumn("PARENTLOTID", 180).SetTextAlignment(TextAlignment.Center);
            ////         //차수
            ////         grdReworkHistory.View.AddSpinEditColumn("WORKCOUNT", 50).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:#,##0}").SetLabel("DEGREE");
            ////         //순번
            ////         grdReworkHistory.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            ////         //공정명
            ////         grdReworkHistory.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 130);
            //////재작업구분
            ////grdReworkHistory.View.AddTextBoxColumn("REWORKPROCESSCLASS", 80).SetTextAlignment(TextAlignment.Center);
            //////재작업번호
            ////grdReworkHistory.View.AddTextBoxColumn("REWORKPROCESSDEFID", 100);
            //////재작업버전
            ////grdReworkHistory.View.AddTextBoxColumn("REWORKPROCESSDEFVERSION", 50).SetTextAlignment(TextAlignment.Center);
            //////재작업명
            ////grdReworkHistory.View.AddTextBoxColumn("REWORKPROCESSDEFNAME", 100);
            //////UOM
            ////grdReworkHistory.View.AddTextBoxColumn("UNIT", 70).SetTextAlignment(TextAlignment.Center).SetLabel("UOM");
            ////         //처리 PCS
            ////         grdReworkHistory.View.AddSpinEditColumn("PCSQTY", 100).SetDisplayFormat("{0:#,##0}").SetTextAlignment(TextAlignment.Right);
            ////         //처리 PNL
            ////         grdReworkHistory.View.AddSpinEditColumn("PNLQTY", 100).SetDisplayFormat("{0:#,##0}").SetTextAlignment(TextAlignment.Right);
            ////         //재작업 인계작업장
            ////         grdReworkHistory.View.AddTextBoxColumn("REWORKAREANAME", 150).SetLabel("TRANSITAREA");
            ////         //처리자
            ////         grdReworkHistory.View.AddTextBoxColumn("SENDUSERNAME", 70).SetTextAlignment(TextAlignment.Center).SetLabel("PROCESSUSER");
            //////투입출처
            ////grdReworkHistory.View.AddTextBoxColumn("INPUTSOURCES", 60).SetTextAlignment(TextAlignment.Center);
            ////         //재작업수량
            ////         grdReworkHistory.View.AddSpinEditColumn("REWORKQTY", 80).SetDisplayFormat("{0:#,##0}").SetTextAlignment(TextAlignment.Right);
            ////         //상태
            ////         //grdReworkHistory.View.AddTextBoxColumn("WIPSTATE", 60).SetTextAlignment(TextAlignment.Center).SetLabel("STATE");

            //grdReworkHistory.View.PopulateColumns();
            #endregion
            #region 재작업 이력 (투입)
            grdReworkHistoryInput.GridButtonItem = GridButtonItem.Export;
            grdReworkHistoryInput.View.SetIsReadOnly();
            grdReworkHistoryInput.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            // 처리일시
            grdReworkHistoryInput.View.AddTextBoxColumn("CREATEDTIME", 140).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center).SetLabel("OSPPRINTTIME");
            //품목코드
            grdReworkHistoryInput.View.AddTextBoxColumn("PRODUCTDEFID", 160).SetLabel("ITEMID");
            //버전
            grdReworkHistoryInput.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetLabel("PRODUCTREVISION");
            //품목명
            grdReworkHistoryInput.View.AddTextBoxColumn("PRODUCTDEFNAME", 230).SetLabel("ITEMNAME");
            //LOTID
            grdReworkHistoryInput.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            //작업구분
            grdReworkHistoryInput.View.AddTextBoxColumn("WORKTYPE", 60).SetTextAlignment(TextAlignment.Center);
            //재작업번호
            grdReworkHistoryInput.View.AddTextBoxColumn("SUBPROCESSDEFID", 100).SetLabel("PROCESSDEFID_R").SetTextAlignment(TextAlignment.Center);
            //재작업버전
            grdReworkHistoryInput.View.AddTextBoxColumn("SUBPROCESSDEFVERSION", 50).SetTextAlignment(TextAlignment.Center).SetLabel("PRODUCTREVISION");
            //투입공정
            grdReworkHistoryInput.View.AddTextBoxColumn("FROMPROCESSSEGMENTNAME", 100).SetTextAlignment(TextAlignment.Left).SetLabel("INPUTSEGMENT");
            //재작업후공정
            grdReworkHistoryInput.View.AddTextBoxColumn("TOPROCESSSEGMENTNAME", 100).SetTextAlignment(TextAlignment.Left).SetLabel("REWORKRETURNSEGMENT");
            //재작업명
            grdReworkHistoryInput.View.AddTextBoxColumn("PROCESSDEFNAME", 130).SetTextAlignment(TextAlignment.Left).SetLabel("PROCESSDEFNAME_R");
            //차수
            grdReworkHistoryInput.View.AddSpinEditColumn("WORKCOUNT", 50).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:#,##0}").SetLabel("DEGREE");
            //재작업 상태
            grdReworkHistoryInput.View.AddTextBoxColumn("MOVESTATE", 80).SetTextAlignment(TextAlignment.Center);
            //처리 PCS
            grdReworkHistoryInput.View.AddSpinEditColumn("PCSQTY", 100).SetDisplayFormat("{0:#,##0}").SetTextAlignment(TextAlignment.Right);
            //처리 PNL
            grdReworkHistoryInput.View.AddSpinEditColumn("PANELQTY", 100).SetDisplayFormat("{0:#,##0}").SetTextAlignment(TextAlignment.Right);
            //재작업 인계작업장
            grdReworkHistoryInput.View.AddTextBoxColumn("AREANAME", 120).SetLabel("TRANSITAREA");
            //처리자
            grdReworkHistoryInput.View.AddTextBoxColumn("USERNAME", 80).SetTextAlignment(TextAlignment.Center).SetLabel("PROCESSUSER");
            //승인자
            grdReworkHistoryInput.View.AddTextBoxColumn("APPROVEDUSER", 80).SetTextAlignment(TextAlignment.Center).SetLabel("APPROVER");
            //승인일자
            grdReworkHistoryInput.View.AddTextBoxColumn("APPROVEDDATE", 140).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);
            grdReworkHistoryInput.View.PopulateColumns();
            #endregion 재작업 이력 (투입)
        }

        /// <summary>
        /// 퀵 메뉴 리스트 등록
        /// </summary>
        private void InitializeQuickMenuList()
		{
			menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SG-0340"), OpenForm) { BeginGroup = true, Tag = "PG-SG-0340" });
			menuList.Add(new DXMenuItem(Language.Get("MENU_PG-SG-0460"), OpenForm) { Tag = "PG-SG-0460" });
		}
		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
        {
			grdReworkHistoryInput.InitContextMenuEvent += GrdReworkHistoryInput_InitContextMenuEvent;
            grdReworkHistoryInput.View.DoubleClick += View_DoubleClick;

            // Tab Page 변경 이벤트
            tabMain.SelectedPageChanged += (s, e) =>
            {
                GetToolbarButtonById("Approval").Visible = e.Page.Equals(tabPageStandard) ? true : false;
                //GetToolbarButtonById("Cancel").Visible = e.Page.Equals(tabPageStandard) ? true : false;
            };

        }

        private void View_DoubleClick(object sender, EventArgs e)
        {
            int rowHandle = grdReworkHistoryInput.View.FocusedRowHandle;
            if (rowHandle < 0)
            {
                return;
            }

            if(grdReworkHistoryInput.View.FocusedColumn.FieldName != "SUBPROCESSDEFID"
                && grdReworkHistoryInput.View.FocusedColumn.FieldName != "SUBPROCESSDEFVERSION"
                && grdReworkHistoryInput.View.FocusedColumn.FieldName != "PROCESSDEFNAME")
            {
                return;
            }

            DataRow row = grdReworkHistoryInput.View.GetDataRow(rowHandle);
            ReworkHistoryPopup popup = new ReworkHistoryPopup();
            popup.ProcessDefId = row["SUBPROCESSDEFID"].ToString();
            popup.ProcessDefVersion = row["SUBPROCESSDEFVERSION"].ToString();
            popup.ShowDialog();
        }

        /// <summary>
        /// Customizing Context Menu Item 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GrdReworkHistoryInput_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
		{
			for (int i = 0; i < menuList.Count; i++)
			{
				args.AddMenus.Add(menuList[i]);
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

				DataRow currentRow = grdReworkHistoryInput.View.GetFocusedDataRow();
				if(currentRow == null) return;

				string menuId = (sender as DXMenuItem).Tag.ToString();

				var param = currentRow.Table.Columns
					.Cast<DataColumn>()
					.ToDictionary(col => col.ColumnName, col => currentRow[col.ColumnName]);

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
            //values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
            //values["P_PRODUCTNAME"] = Format.GetString(values["P_PRODUCTNAME"]).TrimEnd(',');

            //DataTable dtReworkHistory = await QueryAsync("SelectReworkHistory", "10002", values);

            //if (dtReworkHistory.Rows.Count < 1)
            //{
            //	// 조회할 데이터가 없습니다.
            //	ShowMessage("NoSelectData"); 
            //}

            //grdReworkHistory.DataSource = dtReworkHistory;

            var searchParam = Conditions.GetValues();
            searchParam.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
            searchParam["P_PRODUCTNAME"] = Format.GetString(searchParam["P_PRODUCTNAME"]).TrimEnd(',');
            searchParam.Add(
            "P_TABNAME"
            , tabMain.SelectedTabPageIndex.Equals(0)
            ?
            "INPUT"
            :
            "SelectReworkHistory"
            );

            SmartBandedGrid grid = 
                tabMain.SelectedTabPageIndex.Equals(0) 
                ? grdReworkHistoryInput 
                : grdReworkHistory
            ;
            grid.View.ClearDatas();

            if 
            (
                await SqlExecuter.QueryAsync
                (
                    //tabMain.SelectedTabPageIndex.Equals(0) 
                    //? 
                    //"SelectReworkHistoryInput" 
                    //: 
                    //"SelectReworkHistory"
                    "SelectReworkHistoryInput"
                    ,
                    //tabMain.SelectedTabPageIndex.Equals(0)
                    //?
                    //"10004"
                    //:
                    //"10002"
                    "10004"
                    ,
                    searchParam
                ) is DataTable dt
            )
            {
                if (dt.Rows.Count.Equals(0))
                {
                    ShowMessage("NoSelectData");
                    return;
                }

                grid.DataSource = dt;
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //작업장
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 1.1, false, Conditions, false, false);                 // 작업장
            
            //품목
            InitializeCondition_ProductPopup();
            //CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 0.1, true, Conditions);

            //LOT ID
            //CommonFunction.AddConditionLotPopup("P_LOTID", 3.1, true, Conditions);
            CommonFunction.AddConditionLotHistPopup("P_LOTID", 3.1, true, Conditions);

            //승인자
            InitializeConditionUserPopup();

            //승인여부
            InitializeConditionIsReceipt();

            //재작업번호 
            InitializeConditionIsSubprocessdefid();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").LanguageKey = "TRANSITAREA";
            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용

            //재작업번호 
            //InitializeConditionIsSubprocessdefid();
        }

		/// <summary>
		/// 팝업형 조회조건 생성 - 품목
		/// </summary>
		private void InitializeCondition_ProductPopup()
		{
			// SelectPopup 항목 추가
			var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEF", "PRODUCTDEF")
				.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("PRODUCTDEFNAME")
				.SetLabel("PRODUCTDEFID")
				.SetPosition(1.2)
				.SetPopupResultCount(0)
				.SetPopupApplySelection((selectRow, gridRow) => {
					string productDefName = "";

					selectRow.AsEnumerable().ForEach(r => {
						productDefName += Format.GetString(r["PRODUCTDEFNAME"]) + ",";
					});

					Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = productDefName;
				});

			// 팝업에서 사용되는 검색조건 (품목코드/명)
			conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
			//제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
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
			// 단위
			conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");
		}

        /// <summary>
        /// 승인자 조회 팝업
        /// </summary>
        private void InitializeConditionUserPopup()
        {
            ConditionItemSelectPopup userCodePopup = Conditions.AddSelectPopup("P_APPROVEDUSER"
                , new SqlQuery("GetUserList", "10001"
                    , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                    , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                    , $"PLANTID={UserInfo.Current.Plant}")
                , "USERNAME", "USERID")
            .SetPopupLayout("USERNAME", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("USERNAME", "USERNAME")
            .SetLabel("APPROVER")
            .SetPopupResultCount(0)
            .SetPosition(3.2);

            // 팝업 조회조건            
            userCodePopup.Conditions.AddTextBox("USERNAME")
                .SetLabel("USERNAME");

            // 팝업 그리드
            userCodePopup.GridColumns.AddTextBoxColumn("USERID", 10)
                .SetIsHidden();
            userCodePopup.GridColumns.AddTextBoxColumn("USERNAME", 150)
                .SetIsReadOnly();
            userCodePopup.GridColumns.AddTextBoxColumn("DEPARTMENT", 150)
                .SetIsReadOnly();
            userCodePopup.GridColumns.AddTextBoxColumn("DUTY", 100)
                .SetIsReadOnly();
            userCodePopup.GridColumns.AddTextBoxColumn("PLANT", 150)
                .SetIsReadOnly();
            userCodePopup.GridColumns.AddTextBoxColumn("ENTERPRISE", 200)
                .SetIsReadOnly();
        }

        /// <summary>
        /// 승인여부 설정
        /// </summary>
        private void InitializeConditionIsReceipt()
        {
            Conditions.AddComboBox("P_ISAPPROVED", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=YesNo"), "CODENAME", "CODEID")
               .SetLabel("ISACCEPT")
               .SetEmptyItem("", "", true)
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(3.3)
               .SetDefault("");
        }


        /// <summary>
        /// 재작업번호 설정
        /// </summary>
        private void InitializeConditionIsSubprocessdefid()
        {
            string strP_VALIDSTATE = string.Empty;
            if (Conditions.GetControl<SmartSelectPopupEdit>("P_VALIDSTATE") == null)
                strP_VALIDSTATE = "Valid";
            else
                strP_VALIDSTATE = Conditions.GetControl<SmartSelectPopupEdit>("P_VALIDSTATE").Text.ToString();

            string strP_PROCESSCLASSTYPE = string.Empty;
            if (Conditions.GetControl<SmartSelectPopupEdit>("P_PROCESSCLASSTYPE") == null)
                strP_PROCESSCLASSTYPE = "*";
            else
                strP_PROCESSCLASSTYPE = Conditions.GetControl<SmartSelectPopupEdit>("P_PROCESSCLASSTYPE").Text.ToString();

            string strPROCESSCLASSID_R = string.Empty;
            if (Conditions.GetControl<SmartTextBox>("PROCESSCLASSID_R") == null)
                strPROCESSCLASSID_R = "*";
            else
                strPROCESSCLASSID_R = Format.GetString(Conditions.GetControl<SmartTextBox>("PROCESSCLASSID_R").Text.ToString()).TrimEnd(',');

            string strPROCESSDEFID_R = string.Empty;
            if (Conditions.GetControl<SmartSelectPopupEdit>("PROCESSDEFID_R") == null)
                strPROCESSDEFID_R = "";
            else
                strPROCESSDEFID_R = Conditions.GetControl<SmartSelectPopupEdit>("PROCESSDEFID_R").Text.ToString();

            string strPROCESSDEFVERSION_R = string.Empty;
            if (Conditions.GetControl<SmartSelectPopupEdit>("PROCESSDEFVERSION_R") == null)
                strPROCESSDEFVERSION_R = "*";
            else
                strPROCESSDEFVERSION_R = Conditions.GetControl<SmartSelectPopupEdit>("PROCESSDEFVERSION_R").Text.ToString();

            string strTOPPROCESSSEGMENTID = string.Empty;
            if (Conditions.GetControl<SmartComboBox>("TOPPROCESSSEGMENTID") == null)
                strTOPPROCESSSEGMENTID = "*";
            else
                strTOPPROCESSSEGMENTID = Conditions.GetControl<SmartComboBox>("TOPPROCESSSEGMENTID").EditValue.ToString();

            //string strPROCESSDEFNAME_R = string.Empty;
            //if (Conditions.GetControl<SmartComboBox>("PROCESSDEFNAME_R") == null)
            //    strPROCESSDEFNAME_R = "";
            //else
            //    strPROCESSDEFNAME_R = Conditions.GetControl<SmartComboBox>("PROCESSDEFNAME_R").EditValue.ToString();
            Dictionary<string, object> dicParam;
            dicParam = new Dictionary<string, object> {
                {   "ENTERPRISEID"          , UserInfo.Current.Enterprise }
                , { "P_VALIDSTATE"          , strP_VALIDSTATE }
                , { "P_PLANTID"             , UserInfo.Current.Plant }
                , { "P_PROCESSCLASSTYPE"    , strP_PROCESSCLASSTYPE }
                , { "PROCESSCLASSID_R"      , strPROCESSCLASSID_R }
                , { "PROCESSDEFID_R"        , strPROCESSDEFID_R }
                , { "PROCESSDEFVERSION_R"   , strPROCESSDEFVERSION_R }
                , { "TOPPROCESSSEGMENTID"   , strTOPPROCESSSEGMENTID }
                //, { "PROCESSDEFNAME_R"      , strPROCESSDEFNAME_R }
            };

            ConditionItemSelectPopup userCodePopup = Conditions.AddSelectPopup
            (
            "P_SUBPROCESSDEFID"
            , new SqlQuery("GetProcessdefinitionList", "10001", dicParam)
            //, "PROCESSDEFNAME_R"
            , "PROCESSDEFID_R"
            , "PROCESSDEFID_R"
            )
            .SetPopupLayout("PROCESSDEFID_R", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("PROCESSDEFNAME", "PROCESSDEFNAME_R")
            .SetLabel("PROCESSDEFID_R")
            .SetPopupResultCount(0)
            .SetPosition(4.0);

            // 팝업 조회조건            
            userCodePopup.Conditions.AddTextBox("PROCESSDEFID_R")
                .SetLabel("PROCESSDEFID_R");

            // 팝업 그리드
            userCodePopup.GridColumns.AddComboBoxColumn("PROCESSCLASSTYPE"      , 80    , new SqlQuery("GetCodeList"            , "00001", "CODECLASSID=ProcessClassType"               , $"LANGUAGETYPE={UserInfo.Current.LanguageType}")                                                    ).SetIsReadOnly();    //적용구분
            userCodePopup.GridColumns.AddComboBoxColumn("PROCESSCLASSID_R"      , 100   , new SqlQuery("GetProcessclassCombo"   , "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSCLASSNAME"       , "PROCESSCLASSID"       ).SetIsReadOnly();    //재작업구분
            userCodePopup.GridColumns.AddComboBoxColumn("TOPPROCESSSEGMENTID"   , 100   , new SqlQuery("GetProcessSegMentTop"   , "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID").SetIsReadOnly();    //대공정
            userCodePopup.GridColumns.AddTextBoxColumn("PROCESSDEFID_R"         , 150).SetIsReadOnly(); //재작업번호
            userCodePopup.GridColumns.AddTextBoxColumn("PROCESSDEFVERSION_R"    , 150).SetIsReadOnly(); //재작업버전
            userCodePopup.GridColumns.AddTextBoxColumn("PROCESSDEFNAME_R"       , 150).SetIsReadOnly(); //재작업명
            userCodePopup.GridColumns.AddTextBoxColumn("DESCRIPTION"            , 250).SetIsReadOnly(); //설명
            userCodePopup.GridColumns.AddTextBoxColumn("CREATORNAME"            , 80 ).SetIsReadOnly(); //생성자명
        }
        #endregion

        #region Tool Bar 버튼
        protected override async void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            string isCancel = null;
            if (btn.Name.ToString() == "Approval")
            {
                isCancel = "N";
            }
            else if (btn.Name.ToString() == "Cancel")
            {
                isCancel = "Y";
            }

            DataTable checkedRows = grdReworkHistoryInput.View.GetCheckedRows();
            if(checkedRows.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            MessageWorker messageWorker = new MessageWorker("SaveReworkHistory");
            messageWorker.SetBody(new MessageBody()
            {
                { "isCancel", isCancel },
                { "list", checkedRows }
            });
            messageWorker.Execute();

            ShowMessage("SuccedSave");

            await OnSearchAsync(); // 재조회
        }
        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        #endregion
    }
}