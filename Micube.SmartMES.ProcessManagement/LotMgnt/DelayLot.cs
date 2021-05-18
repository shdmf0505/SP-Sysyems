#region using

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
    /// 프 로 그 램 명  : 공정관리 > 재공 관리 > 체공 LOT조회
    /// 업  무  설  명  : 체공 LOT을 조회한다.
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2019-10-08
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class DelayLot : SmartConditionManualBaseForm
    {
        #region Local Variables
        #endregion

        #region 생성자

        public DelayLot()
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
            InitializeDelayListGrid();

            Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFTYPE").EditValue = "Product";
        }
        
        /// <summary>        
        /// 체공 LOT 조회 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdDelayLotList.GridButtonItem = GridButtonItem.Export;
			grdDelayLotList.View.SetIsReadOnly();

			var delayInfo = grdDelayLotList.View.AddGroupColumn("");
            //SITE
            delayInfo.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
            //양산구분
            delayInfo.AddTextBoxColumn("LOTTYPE", 60).SetTextAlignment(TextAlignment.Center);
            //작업장
            delayInfo.AddTextBoxColumn("AREANAME", 100).SetTextAlignment(TextAlignment.Left);
            //품목코드
            delayInfo.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left).SetLabel("ITEMID");
            //품목 리비전
            delayInfo.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetTextAlignment(TextAlignment.Center).SetLabel("PRODUCTREVISION");
            //품목명
            delayInfo.AddTextBoxColumn("PRODUCTDEFNAME", 150).SetTextAlignment(TextAlignment.Left);
            //보류 여부
            delayInfo.AddTextBoxColumn("ISHOLD", 60).SetTextAlignment(TextAlignment.Center);
            //라킹 여부
            delayInfo.AddTextBoxColumn("ISLOCKING", 60).SetTextAlignment(TextAlignment.Center);
            //공정수순
            delayInfo.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            //공명명
            delayInfo.AddTextBoxColumn("PROCESSSEGMENTNAME", 120).SetTextAlignment(TextAlignment.Left).SetLabel("PROCESSSEGMENT");
            //자원명
            delayInfo.AddTextBoxColumn("RESOURCENAME", 100).SetTextAlignment(TextAlignment.Left).SetLabel("RESOURCE");
            //LOT ID
            delayInfo.AddTextBoxColumn("LOTID", 120).SetTextAlignment(TextAlignment.Left);
            //공정진행 상태
            delayInfo.AddTextBoxColumn("WIPPROCESSSTATE", 80).SetTextAlignment(TextAlignment.Left).SetLabel("PROCESSSTATE");
            //LOT 상태
            delayInfo.AddTextBoxColumn("LOTSTATE", 80).SetTextAlignment(TextAlignment.Left);
            //품목명
            //인수대기
            var grdSendInfo = grdDelayLotList.View.AddGroupColumn("WAITFORRECEIVE");
            grdSendInfo.AddSpinEditColumn("WAITRECEIVEQTY", 60).SetLabel("QTY");
            grdSendInfo.AddSpinEditColumn("WAITRECEIVEPNL", 60).SetLabel("PNL");

            //인수
            var grdReceivedtInfo = grdDelayLotList.View.AddGroupColumn("ACCEPT");
            grdReceivedtInfo.AddSpinEditColumn("WAITQTY", 80).SetLabel("QTY");
            grdReceivedtInfo.AddSpinEditColumn("WAITPNL", 80).SetLabel("PNL");

            //작업시작
            var grdAccept = grdDelayLotList.View.AddGroupColumn("WIPSTARTQTY");
            grdAccept.AddSpinEditColumn("RUNQTY", 80).SetLabel("QTY");
            grdAccept.AddSpinEditColumn("RUNPNL", 80).SetLabel("PNL");

            //작업완료
            var grdWorkEndInfo = grdDelayLotList.View.AddGroupColumn("WIPENDQTY");
            grdWorkEndInfo.AddSpinEditColumn("WAITSENDQTY", 80).SetLabel("QTY");
            grdWorkEndInfo.AddSpinEditColumn("WAITSENDPNL", 80).SetLabel("PNL");
            //투입일
            var delayInfo2 = grdDelayLotList.View.AddGroupColumn("");
            delayInfo2.AddTextBoxColumn("LOTSTARTDATE", 100).SetTextAlignment(TextAlignment.Center).SetLabel("INPUTDAY");
            //체공기준시간
            delayInfo2.AddSpinEditColumn("STAYINGLOCKSTD", 80).SetTextAlignment(TextAlignment.Right).SetLabel("STDSTAYINGTIME");
            //공정체공시간
            delayInfo2.AddTextBoxColumn("SEGMNETDELAYTIME", 80).SetTextAlignment(TextAlignment.Right).SetLabel("SEGMENTSTAYINGTIME");
            //공정체공(h)
            delayInfo2.AddTextBoxColumn("SEGMENTDELAYTIMEH", 80).SetTextAlignment(TextAlignment.Right).SetLabel("SEGMENTSTAYINGTIMEH");
            //현상태체공(h)
            delayInfo2.AddTextBoxColumn("CURRENTSTATESTAYINGTIME", 80).SetTextAlignment(TextAlignment.Right).SetLabel("CURRENTSTATESTAYING");
            //체공사유
            delayInfo2.AddTextBoxColumn("REASONCODEID", 100).SetTextAlignment(TextAlignment.Left).SetLabel("DELAYREASON");
            //체공걸렸는지 여부
            delayInfo2.AddTextBoxColumn("STAYINGLOCK", 100).SetTextAlignment(TextAlignment.Left).SetIsHidden();
            delayInfo2.AddTextBoxColumn("CHECKTIME", 100).SetTextAlignment(TextAlignment.Left).SetIsHidden();

            grdDelayLotList.View.PopulateColumns();
            #region 기존 로직 일단 주석 처리
            /*
			// 체공구분
			delayInfo.AddTextBoxColumn("DELAYTYPE", 80).SetTextAlignment(TextAlignment.Center).SetLabel("WAITINGTYPE");
			// 양산구분
			delayInfo.AddTextBoxColumn("LOTTYPE", 80).SetTextAlignment(TextAlignment.Center);
            // SITE
            delayInfo.AddTextBoxColumn("PLANTID", 80).SetTextAlignment(TextAlignment.Center);
            // AREANAME
            delayInfo.AddTextBoxColumn("AREANAME", 120);
            // LOTID
            delayInfo.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            // 품목코드
            delayInfo.AddTextBoxColumn("PRODUCTDEFID", 150).SetLabel("ITEMID");
            // 품목명
            delayInfo.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetLabel("ITEMNAME");
            // 공정순서
            delayInfo.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            // 공정
            delayInfo.AddTextBoxColumn("PROCESSSEGMENTNAME", 150).SetLabel("PROCESSSEGMENT");
            // PCS
            delayInfo.AddSpinEditColumn("PCS", 80);
            // PNL
            delayInfo.AddSpinEditColumn("PNL", 80);
            // 상태
            delayInfo.AddTextBoxColumn("PROCESSSTATE", 120).SetTextAlignment(TextAlignment.Center);
			// 현상태 L/T(시간)
			delayInfo.AddSpinEditColumn("CURRENT_LEADTIME", 120).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
            // 공정입고일
            delayInfo.AddTextBoxColumn("SEGMENTINBOUNDTIME", 130).SetTextAlignment(TextAlignment.Center);
			// 공정입고 L/T(일)
			delayInfo.AddTextBoxColumn("SEGMENT_LEADTIME", 80);
            // 투입일
            delayInfo.AddTextBoxColumn("LOTSTARTDATE", 130).SetLabel("STARTEDDATE").SetTextAlignment(TextAlignment.Center);
			// 투입 L/T
			delayInfo.AddSpinEditColumn("INPUT_LEADTIME", 100);
            // 보류
            delayInfo.AddTextBoxColumn("ISHOLD", 50).SetTextAlignment(TextAlignment.Center);
			// 보류사유
			delayInfo.AddTextBoxColumn("HOLDCODE", 120);
            // Locking
            delayInfo.AddTextBoxColumn("ISLOCKING", 50).SetTextAlignment(TextAlignment.Center);
			// Locking 사유
			delayInfo.AddTextBoxColumn("LOCKINGCODE", 120);
            // 체공 사유
            delayInfo.AddTextBoxColumn("DELAYCODE", 120).SetLabel("DELAYREASON");

			//재공계
            var grdWipInfo = grdDelayLotList.View.AddGroupColumn("WIPSTOCK");
			grdWipInfo.AddSpinEditColumn("QTY", 80);
			grdWipInfo.AddSpinEditColumn("PANELQTY", 80);

			//인수대기
			var grdSendInfo = grdDelayLotList.View.AddGroupColumn("WAITFORRECEIVE");
			grdSendInfo.AddSpinEditColumn("SENDPCSQTY", 80);
			grdSendInfo.AddSpinEditColumn("SENDPANELQTY", 80);

			//인수
			var grdReceivedtInfo = grdDelayLotList.View.AddGroupColumn("ACCEPT");
			grdReceivedtInfo.AddSpinEditColumn("RECEIVEPCSQTY", 80);
			grdReceivedtInfo.AddSpinEditColumn("RECEIVEPANELQTY", 80);

			//작업시작
            var grdAccept = grdDelayLotList.View.AddGroupColumn("WIPSTARTQTY");
			grdAccept.AddSpinEditColumn("WORKSTARTPCSQTY", 80);
			grdAccept.AddSpinEditColumn("WORKSTARTPANELQTY", 80);

			//작업완료
            var grdWorkEndInfo = grdDelayLotList.View.AddGroupColumn("WIPENDQTY");
			grdWorkEndInfo.AddSpinEditColumn("WORKENDPCSQTY", 80);
			grdWorkEndInfo.AddSpinEditColumn("WORKENDPANELQTY", 80);

            grdDelayLotList.View.PopulateColumns();
            */
            #endregion
        }

        private void InitializeDelayListGrid()
        {
            grdDelayList.GridButtonItem = GridButtonItem.Export;
            grdDelayList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            // Site
            grdDelayList.View.AddTextBoxColumn("PLANTID", 60)
                .SetLabel("PLANT")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            // 양산구분
            grdDelayList.View.AddTextBoxColumn("LOTTYPENAME", 60)
                .SetLabel("LOTTYPE")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center)
                 .SetIsReadOnly();
            // 작업장
            grdDelayList.View.AddTextBoxColumn("AREANAME", 150)
                 .SetIsReadOnly();
            // LOTID
            grdDelayList.View.AddTextBoxColumn("LOTID", 150)
                 .SetIsReadOnly();
            // 품목코드
            grdDelayList.View.AddTextBoxColumn("PRODUCTDEFID", 100)
                  .SetIsReadOnly();
            // 품목Rev.
            grdDelayList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60)
                .SetIsReadOnly()
                .SetLabel("PRODUCTREVISION")
                .SetTextAlignment(TextAlignment.Center);
            // 품목명
            grdDelayList.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                 .SetIsReadOnly();
            // 공정
            grdDelayList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                 .SetIsReadOnly();
            // 상태
            grdDelayList.View.AddTextBoxColumn("PROCESSSTATE2", 80)
                .SetLabel("PROCESSSTATE")
                .SetIsReadOnly();
            // 체공사유
            grdDelayList.View.AddTextBoxColumn("DELAYREASONCODE", 120)
                .SetLabel("DELAYREASON")
                .SetIsReadOnly();
            // 체공상세사유
            grdDelayList.View.AddTextBoxColumn("DELAYCOMMENT", 200)
                .SetIsReadOnly();
            // 기준체공시간(분)
            grdDelayList.View.AddSpinEditColumn("STDDELAYTIME", 90)
                .SetIsReadOnly();
            // 체공시간
            grdDelayList.View.AddTextBoxColumn("DELAYTIME", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            // 이전Step완료시간
            grdDelayList.View.AddTextBoxColumn("CHECKSTARTTIME", 120)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center);
            // 체공사유입력시간
            grdDelayList.View.AddTextBoxColumn("CHECKTIME", 120)
                .SetLabel("CHECKENDTIME")
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center);

            // 확정여부
            grdDelayList.View.AddTextBoxColumn("CONFIRMSTATE", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            // 확정자
            grdDelayList.View.AddTextBoxColumn("CONFIRMUSER", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            // 체공사유입력시간
            grdDelayList.View.AddTextBoxColumn("CONFIRMDATE", 120)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center);

            grdDelayList.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            tabDelayLot.SelectedPageChanged += TabDelayLot_SelectedPageChanged;
            grdDelayList.View.RowCellClick += View_RowCellClick;
            grdDelayList.View.RowCellStyle += View_RowCellStyle;

            grdDelayLotList.View.RowCellClick += DelayLotListView_RowCellClickAsync;
            grdDelayLotList.View.RowCellStyle += DelayLotListView_RowCellStyle;
        }

        private void View_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

            if (e.RowHandle > -1)
            {
                string stdStayLock = Format.GetFullTrimString(grdDelayList.View.GetRowCellValue(e.RowHandle, "CONFIRMSTATE"));

                if (stdStayLock.Equals("Y"))
                {
                    Color c = Color.FromArgb(253, 253, 150);
                    e.Appearance.BackColor = c;
                }
            }



        }

        private async void View_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DataRow dr = grdDelayList.View.GetFocusedDataRow();
            if (e.Clicks == 2 && e.Column.FieldName.Equals("DELAYREASONCODE") && string.IsNullOrEmpty(Format.GetString(dr["CONFIRMSTATE"])) )
            {
    

                        AffectLotByDelayPopup popup = new AffectLotByDelayPopup(dr, tabDelayLot.SelectedTabPageIndex);
                        popup.ShowDialog();

                        if (popup.DialogResult == DialogResult.OK)
                        {
                            await OnSearchAsync();
                        }

            }
            

        }


        protected override void OnToolbarClick(ToolbarClickEventArgs e)
        {
            base.OnToolbarClick(e);

            if (e.Id == "Confirmation")
            {


                DataTable tbl = grdDelayList.View.GetCheckedRows();

                foreach(DataRow dr in tbl.Rows)
                {
                    if(dr["CONFIRMSTATE"].Equals("Y"))
                    {
                        throw MessageException.Create("CONFIRMSTATE");

                    }


                }


                MessageWorker worker = new MessageWorker("AbnormalOccurenceByDelayLot");
                worker.SetBody(new MessageBody(){
                { "enterpriseId", Framework.UserInfo.Current.Enterprise },
                { "plantId", Framework.UserInfo.Current.Plant },
                { "userId", Framework.UserInfo.Current.Id },
                { "list2", tbl },
                    {"page",2 }

            });

                worker.Execute();
            }


            ShowMessage("CONFIRMOK");


             OnSearchAsync();
        }

        private void TabDelayLot_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == tpgDelayLotList)
                SetConditionVisiblility("P_STAYING", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
            else if (e.Page == tpgDelayList)
                SetConditionVisiblility("P_STAYING", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
        }

        private void DelayLotListView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if(e.RowHandle > -1)
            {
                string stdStayLock = Format.GetFullTrimString(grdDelayLotList.View.GetRowCellValue(e.RowHandle, "STAYINGLOCK"));

                if (stdStayLock.Equals("Staying"))
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// 체공 사유 팝업 OPEN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DelayLotListView_RowCellClickAsync(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
		{
            string stdStayLock = Format.GetFullTrimString(grdDelayLotList.View.GetRowCellValue(e.RowHandle, "STAYINGLOCK"));

            if (e.Clicks == 2 && e.Column.FieldName.Equals("REASONCODEID") && string.IsNullOrEmpty(Format.GetString(e.CellValue)) && stdStayLock.Equals("Staying"))
            {

                DataRow dr = grdDelayLotList.View.GetFocusedDataRow();

                AffectLotByDelayPopup popup = new AffectLotByDelayPopup(dr, tabDelayLot.SelectedTabPageIndex);
                popup.ShowDialog();

                if (popup.DialogResult == DialogResult.OK)
                {
                    await OnSearchAsync();
                }
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
            var values = Conditions.GetValues();
			values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
			values["P_PRODUCTNAME"] = Format.GetString(values["P_PRODUCTNAME"]).TrimEnd(',');

            if (tabDelayLot.SelectedTabPage == tpgDelayLotList)
            {
                DataTable dtDelayLotList = await QueryAsync("SelectDelayLotList", "10001", values);

                if (dtDelayLotList.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdDelayLotList.DataSource = dtDelayLotList;
            }
            else if (tabDelayLot.SelectedTabPage == tpgDelayList)
            {
                DataTable dtDelayList = await QueryAsync("SelectDelayList", "10001", values);

                if (dtDelayList.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdDelayList.DataSource = dtDelayList;
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

			//품목코드
			InitializeCondition_ProductPopup();
			//LOTID
			CommonFunction.AddConditionLotPopup("P_LOTID", 1.1, true, Conditions);
			//공정
            CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENTID", 1.2, true, Conditions);
			//작업장
			CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 1.3, true, Conditions, false, false);
		}

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용

			//다국어변경
			SmartComboBox processState = Conditions.GetControl<SmartComboBox>("P_PROCESSTYPE");
			processState.LanguageKey = "STATETYPE";
			processState.LabelText = Language.Get(processState.LanguageKey);

            Conditions.GetControl<SmartComboBox>("P_STAYING").EditValue = "Staying";

            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += (sender, e) =>
            {
                SmartSelectPopupEdit edit = sender as SmartSelectPopupEdit;

                if (string.IsNullOrEmpty(Format.GetString(edit.EditValue)))
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = "";
            };
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
				.SetPosition(1.1)
				.SetPopupResultCount(0)
				.SetPopupApplySelection((selectRow, gridRow) => {
					string productDefName = "";

                    if (selectRow.Count() > 0)
                    {
                        selectRow.AsEnumerable().ForEach(r =>
                        {
                            productDefName += Format.GetString(r["PRODUCTDEFNAME"]) + ",";
                        });
                    }

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
		#endregion

	}
}