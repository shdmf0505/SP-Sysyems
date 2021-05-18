#region using

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
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

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 불량관리 > 불량품 인수등록
    /// 업  무  설  명  : Lot 기준으로 불량이 발생한 품목들에 대해 인수등록을 하거나 취소한다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-07-03
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class DefectAccept : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public DefectAccept()
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

            InitializeTakeOverGrid();
            InitializeHistoryGrid();
            InitializePopup();
            InitializationSummaryRow();

            if (pnlToolbar.Controls["layoutToolbar"] != null)
            {
                if (pnlToolbar.Controls["layoutToolbar"].Controls["CancelReceive"] != null)
                {
                    pnlToolbar.Controls["layoutToolbar"].Controls["CancelReceive"].Visible = false;
                }
            }
        }

        /// <summary>        
        /// 인수처리 그리드
        /// </summary>
        private void InitializeTakeOverGrid()
        {
            grdTakeover.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdTakeover.GridButtonItem = GridButtonItem.Export;
            grdTakeover.View.SetIsReadOnly();
            
            grdTakeover.View.AddTextBoxColumn("RECEIVEUSER", 100).SetIsHidden();
            grdTakeover.View.AddTextBoxColumn("ENTERPRISEID", 100).SetIsHidden();
            grdTakeover.View.AddTextBoxColumn("FACTORYID", 100).SetIsHidden();
            grdTakeover.View.AddTextBoxColumn("AREAID", 100).SetIsHidden();
            grdTakeover.View.AddTextBoxColumn("PROCESSDEFID", 100).SetIsHidden();
            grdTakeover.View.AddTextBoxColumn("PROCESSDEFVERSION", 100).SetIsHidden();
            grdTakeover.View.AddTextBoxColumn("PROCESSSEGMENTID", 100).SetIsHidden();
            grdTakeover.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100).SetIsHidden();
            grdTakeover.View.AddTextBoxColumn("PROCESSPATHID", 100).SetIsHidden();
            grdTakeover.View.AddTextBoxColumn("SEQUENCE", 100).SetIsHidden();
            grdTakeover.View.AddTextBoxColumn("INBOUNDUSER", 100).SetIsHidden();
            grdTakeover.View.AddTextBoxColumn("RECEIVETIME", 100).SetIsHidden();
            grdTakeover.View.AddTextBoxColumn("RECEIVEAREAID", 100).SetIsHidden();
            grdTakeover.View.AddTextBoxColumn("VALIDSTATE", 100).SetIsHidden();
            grdTakeover.View.AddTextBoxColumn("ISMODIFY", 100).SetIsHidden(); // 작업장 통제 권한에 따른 수정가능여부

            var group = grdTakeover.View.AddGroupColumn("DEFECTINFO");
            
            group.AddTextBoxColumn("PROCESSDATE", 200).SetTextAlignment(TextAlignment.Center); // 처리시간(불량으로 처리한 시간)
            group.AddTextBoxColumn("PROCESSSEGMENTNAME", 180); // 공정명
            group.AddTextBoxColumn("AREANAME", 150); // 작업장
            group.AddTextBoxColumn("PRODUCTDEFNAME", 260); // 품목명
            group.AddTextBoxColumn("PRODUCTDEFID", 120); // 품목코드
            group.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center); // REV
            group.AddTextBoxColumn("PARENTLOTID", 200).SetTextAlignment(TextAlignment.Center).SetLabel("Lot No"); // Parent Lot No
            group.AddSpinEditColumn("INPUTPNLQTY", 100); // 해당공정 인수 PNL수량
            group.AddSpinEditColumn("INPUTPCSQTY", 100); // 해당공정 인수 PCS수량
            group.AddSpinEditColumn("DEFECTQTY", 80) .SetLabel("PCS"); // 불량 PCS 갯수
            group.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center).SetLabel("DEFECTLOTID"); // 불량 Lot No
            group.AddTextBoxColumn("CREATORNAME", 100).SetTextAlignment(TextAlignment.Center).SetLabel("PROCESSUSER"); // 처리자
            group.AddTextBoxColumn("PLANTID", 80).SetIsHidden(); // Site
            group.AddTextBoxColumn("USERSEQUENCE", 80).SetIsHidden(); // 공정순서

            group = grdTakeover.View.AddGroupColumn("ETC");

            group.AddTextBoxColumn("ELAPSEDTIME", 120).SetTextAlignment(TextAlignment.Center); // 경과시간
            group.AddTextBoxColumn("PROCESSSEGMENTTYPE", 100).SetTextAlignment(TextAlignment.Center);
            group.AddTextBoxColumn("LOTTYPE", 100).SetTextAlignment(TextAlignment.Center); // 양산구분          
            group.AddTextBoxColumn("UNIT", 80).SetTextAlignment(TextAlignment.Center).SetLabel("UOM"); // 불량단위

            grdTakeover.View.PopulateColumns();

            grdTakeover.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        /// <summary>        
        /// 내역조회 그리드
        /// </summary>
        private void InitializeHistoryGrid()
        {
            grdHistory.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdHistory.GridButtonItem = GridButtonItem.Export;
            grdHistory.View.SetIsReadOnly();

            var defectInfo = grdHistory.View.AddGroupColumn("DEFECTINFO");

            defectInfo.AddTextBoxColumn("STATUSNAME", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("STATUS"); // 진행상태
            defectInfo.AddTextBoxColumn("RECEIVETIME", 200)
                .SetTextAlignment(TextAlignment.Center); // 인수일시
            defectInfo.AddTextBoxColumn("RECEIVEAREA", 150); // 인수작업장
            defectInfo.AddTextBoxColumn("RECEIVEUSERNAME", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("RECEIVEUSER"); // 인수자
            defectInfo.AddTextBoxColumn("PLANTID", 80); // Site
            defectInfo.AddTextBoxColumn("PRODUCTDEFID", 120); // 품목 ID
            defectInfo.AddTextBoxColumn("PRODUCTDEFNAME", 260); // 품목명
            defectInfo.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center);  // 품목 Version
            defectInfo.AddTextBoxColumn("PARENTLOTID", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("Lot No"); // Parent Lot No
            defectInfo.AddSpinEditColumn("DEFECTQTY", 80)
                .SetLabel("PCS"); // 불량 PCS 갯수
            //defectInfo.AddSpinEditColumn("DEFECTPNLQTY", 80)
            //    .SetLabel("PNL"); // 불량 PNL 갯수
            defectInfo.AddTextBoxColumn("USERSEQUENCE", 80)
                .SetTextAlignment(TextAlignment.Right); // 공정순서
            defectInfo.AddTextBoxColumn("PROCESSSEGMENTNAME", 180); // 공정명
            defectInfo.AddSpinEditColumn("INPUTPCSQTY", 100); // 해당공정 인수 PCS수량
            defectInfo.AddSpinEditColumn("INPUTPNLQTY", 100); // 해당공정 인수 PNL수량
            defectInfo.AddTextBoxColumn("LOTID", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("DEFECTLOTID"); // 불량 Lot No
            defectInfo.AddTextBoxColumn("PROCESSDATE", 180)
                .SetTextAlignment(TextAlignment.Center); // 처리일시
            defectInfo.AddTextBoxColumn("HANDLEAREA", 180); // 처리작업장
            defectInfo.AddTextBoxColumn("HANDLENAME", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("PROCESSUSER"); // 처리자
            defectInfo.AddTextBoxColumn("UNIT", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("UOM");

            var hidden = grdHistory.View.AddGroupColumn("HIDDEN");

            hidden.AddTextBoxColumn("RECEIVEAREAID", 100)
                .SetIsHidden();
            hidden.AddTextBoxColumn("HANDLEAREAID", 100)
                .SetIsHidden();
            hidden.AddTextBoxColumn("SEQUENCE", 100)
                .SetIsHidden();
            hidden.AddTextBoxColumn("RECEIVEUSER", 100)
                .SetIsHidden();
            hidden.AddTextBoxColumn("STATUS", 100)
                .SetIsHidden();
            hidden.AddTextBoxColumn("VALIDSTATE", 100)
                .SetIsHidden();
            hidden.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden();
            hidden.AddTextBoxColumn("AREAID", 100)
                .SetIsHidden();
            hidden.AddTextBoxColumn("PROCESSDEFID", 100)
                .SetIsHidden();
            hidden.AddTextBoxColumn("PROCESSDEFVERSION", 100)
                .SetIsHidden();
            hidden.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100)
                .SetIsHidden();
            hidden.AddTextBoxColumn("ISMODIFY", 100)
                .SetIsHidden(); // 작업장 통제 권한에 따른 수정가능여부
            hidden.AddTextBoxColumn("AREANAME", 100)
                .SetIsHidden(); // 작업장명

            grdHistory.View.PopulateColumns();

            grdHistory.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        // 인수처리 및 내역조회 그리드 합계 초기화
        private void InitializationSummaryRow()
        {
            #region 인수처리 그리드 합계 초기화

            grdTakeover.View.Columns["CREATORNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdTakeover.View.Columns["CREATORNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdTakeover.View.Columns["DEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdTakeover.View.Columns["DEFECTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            //grdTakeover.View.Columns["DEFECTPNLQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //grdTakeover.View.Columns["DEFECTPNLQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdTakeover.View.OptionsView.ShowFooter = true;
            grdTakeover.ShowStatusBar = false;

            #endregion

            #region 내역조회 그리드 합계 초기화

            grdHistory.View.Columns["PARENTLOTID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdHistory.View.Columns["PARENTLOTID"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdHistory.View.Columns["DEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdHistory.View.Columns["DEFECTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            //grdHistory.View.Columns["DEFECTPNLQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //grdHistory.View.Columns["DEFECTPNLQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdHistory.View.OptionsView.ShowFooter = true;
            grdHistory.ShowStatusBar = false;

            #endregion
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            tabDefectAccept.SelectedPageChanged += TabDefectAccept_SelectedPageChanged;

            grdTakeover.View.RowClick += View_RowClick1;
            grdTakeover.View.CustomSummaryCalculate += View_CustomSummaryCalculate;
            grdTakeover.View.RowStyle += View_RowStyle;
            grdHistory.View.RowClick += View_RowClick2;
            grdHistory.View.RowStyle += View_RowStyle;

            //btnTakeover.Click += BtnTakeover_Click;
            //btnCancelTakeover.Click += BtnCancleTakeover_Click;
        }

        private void View_RowStyle(object sender, RowStyleEventArgs e)
        {
            // 인수처리탭일때
            if (tabDefectAccept.SelectedTabPage.Name == "tpgTakeover")
            {
                if (e.RowHandle < 0) return;
                bool isChecked = grdTakeover.View.IsRowChecked(e.RowHandle);

                if (isChecked)
                {
                    e.Appearance.BackColor = Color.FromArgb(254, 254, 16);
                    e.HighPriority = true;
                }
            }
            // 내역조회탭일때
            else
            {
                if (e.RowHandle < 0) return;
                bool isChecked = grdHistory.View.IsRowChecked(e.RowHandle);

                if (isChecked)
                {
                    e.Appearance.BackColor = Color.FromArgb(254, 254, 16);
                    e.HighPriority = true;
                }
            }
        }

        /// <summary>
        /// 집계 Row Custom 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            //if (e.IsTotalSummary)
            //{
            //    GridSummaryItem item = e.Item as GridSummaryItem;
            //    if (item.FieldName == "DEFECTQTY" || item.FieldName == "DEFECTPNLQTY") 
            //    {
            //        if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
            //        {
            //            decimal allLotQty = Convert.ToDecimal((sender as GridView).Columns["INSPECTIONQTY"].SummaryItem.SummaryValue);
            //            //모든 Lot의 전체 불량 수량 PCS
            //            decimal allLotDefectQty = Convert.ToDecimal((sender as GridView).Columns["SPECOUTQTY"].SummaryItem.SummaryValue);

            //            //모든 Lot의 전체 양품 수량 PCS
            //            decimal goodPCS = Convert.ToDecimal((sender as GridView).Columns["GOODQTYPCS"].SummaryItem.SummaryValue);

            //            //모든 Lot의 전체 양품 수량 PNL
            //            decimal goodPNL = Convert.ToDecimal((sender as GridView).Columns["GOODQTYPNL"].SummaryItem.SummaryValue);

            //            //모든 Lot의 전체 불량 수량 PNL
            //            decimal defectPNL = Convert.ToDecimal((sender as GridView).Columns["DEFECTQTYPNL"].SummaryItem.SummaryValue);

            //            //***allLotQty 현재 : Lot 전체수량 // 샘플수량으로 계산 해야 하는지 확인후 수정 필 
            //            if (allLotQty != 0 && allLotDefectQty != 0)
            //            {//lot들의 전체 수량과 불량 수량 합이 0이 아닐때 (PCS로 계산 한 불량률)
            //                decimal defectRate = Math.Round((allLotDefectQty / allLotQty * 100).ToSafeDecimal(), 1);
            //                e.TotalValue = defectRate;
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 인수등록 그리드 더블클릭시 팝업호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowClick1(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                DefectLotDefectCodePopup popup = new DefectLotDefectCodePopup();
                popup.Owner = this;
                popup.CurrentDataRow = grdTakeover.View.GetFocusedDataRow();
                popup.ShowDialog();
            }
        }

        /// <summary>
        /// Row 더블클릭시 팝업호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowClick2(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                DefectLotDefectCodePopup popup = new DefectLotDefectCodePopup();
                popup.Owner = this;
                popup.CurrentDataRow = grdHistory.View.GetFocusedDataRow();
                popup.ShowDialog();
            }
        }

        /// <summary>
        /// 인수취소버튼 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancleTakeover_Click(object sender, EventArgs e)
        {
            DataTable dt = grdHistory.View.GetCheckedRows();
            dt.TableName = "list";

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("NoSaveData");
                return;
            }
            // 상태가 원인판정확정이거나, 마감처리상태이거나, 보류처리상태라면 Exception
            else if (dt.AsEnumerable().Where(r => r["STATUS"].Equals("CauseComplete") || r["STATUS"].Equals("DeadlineComplete") || r["STATUS"].Equals("HoldComplete")).Count() > 0)
            {
                throw MessageException.Create("DoNotInbountCancelDataExist"); // 인수취소를 할 수 없는 상태의 건이 존재합니다.
            }

            // 인수처리된 LOT의 불량이 하나라도 확정됬다면 Exception
            List<object> list = IsInboundLotDefectNotConfirm(dt);
            if (list.Count > 0)
            {
                string lotList = "";

                for (int i = 0; i < list.Count; i++)
                {
                    if (i + 1 == list.Count)
                    {
                        lotList += Format.GetString(list[i]);
                    }
                    else
                    {
                        lotList += Format.GetString(list[i]) + ", ";
                    }
                }

                throw MessageException.Create("InboundlotDuringConfirmDataExist", lotList); // 인수처리된 Lot중 불량확정된 건이 존재합니다.
            }

            else
            {
                // 작업장 권한 체크
                CheckAuthorityArea(dt);

                // 인수취소 하시겠습니까?
                if (this.ShowMessageBox("IsCancleTakeOver", "Information", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    this.ExecuteRule("SaveLotDefectInbound", dt);
                    this.ShowMessage("CancleTakeOverComplete"); // 인수취소 완료
                    this.OnSearchAsync();
                }
            }
        }

        /// <summary>
        /// 인수처리버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTakeover_Click(object sender, EventArgs e)
        {
            DataTable dt = grdTakeover.View.GetCheckedRows();
            dt.TableName = "list";

            if (dt.Rows.Count == 0)
            {
                // 저장할 데이터가 없습니다.
                this.ShowMessage("NoSaveData");
                return;
            }
            else if (string.IsNullOrWhiteSpace(popupInboundArea.GetValue().ToString()))
            {
                // 인수작업장이 선택되지 않았습니다.
                this.ShowMessage("NoSelectInboundArea");
                return;
            }
            else
            {
                // 작업장 권한 체크
                CheckAuthorityArea(dt);

                // 인수처리 하시겠습니까?
                if (this.ShowMessageBox("IsHandleTakeOver", "Information", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    // 현재 로그인한 사람을 불량 Lot 인수자로 등록하고 선택된 인수작업장을 추가한다.
                    foreach (DataRow row in dt.Rows)
                    {
                        row["INBOUNDUSER"] = UserInfo.Current.Id;
                        row["RECEIVEAREAID"] = popupInboundArea.GetValue();
                    }

                    this.ExecuteRule("SaveLotDefectInbound", dt);
                    this.ShowMessage("HandleTakeOverComplete"); // 인수처리 완료
                    this.OnSearchAsync();
                }
            }     
        }

        /// <summary>
        /// 인수처리탭 -> 인수처리버튼 visible, 내역조회탭 -> 인수취소버튼 visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabDefectAccept_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (tabDefectAccept.SelectedTabPageIndex == 0)
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Receive"] != null)
                    {
                        pnlToolbar.Controls["layoutToolbar"].Controls["Receive"].Visible = true;
                    }
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["CancelReceive"] != null)
                    {
                        pnlToolbar.Controls["layoutToolbar"].Controls["CancelReceive"].Visible = false;
                    }
                }
                lblInboundArea.Visible = true;
                popupInboundArea.Visible = true;           
                //btnTakeover.Visible = true;
                //btnCancelTakeover.Visible = false;
            }
            else
            {
                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Receive"] != null)
                    {
                        pnlToolbar.Controls["layoutToolbar"].Controls["Receive"].Visible = false;
                    }
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["CancelReceive"] != null)
                    {
                        pnlToolbar.Controls["layoutToolbar"].Controls["CancelReceive"].Visible = true;
                    }
                }
                lblInboundArea.Visible = false;
                popupInboundArea.Visible = false;
                //btnTakeover.Visible = false;
                //btnCancelTakeover.Visible = true;
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
            // DataTable changed = grdList.GetChangedRows();

            // ExecuteRule("SaveCodeClass", changed);
        }

        /// <summary>
        /// 툴바버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Receive"))
            {
                BtnTakeover_Click(null, null);
            }
            else if (btn.Name.ToString().Equals("CancelReceive"))
            {
                BtnCancleTakeover_Click(null, null);
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

            var values = Conditions.GetValues();
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("P_USERID", UserInfo.Current.Id);

            if (tabDefectAccept.SelectedTabPage == tpgTakeover)
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetDefectLot", "10001", values);

                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }

                grdTakeover.DataSource = dt;
            }
            else
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetInboundDefectLot", "10001", values);

                if (dt.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }

                grdHistory.DataSource = dt;
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //Conditions.AddComboBox("p_plantId", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PLANTNAME", "PLANTID")
            //    .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
            //    .SetDefault(UserInfo.Current.Plant)
            //    .SetLabel("PLANT")
            //    .SetEmptyItem()
            //    .SetPosition(1.1); 

            InitializeConditionPopup_Area();
            InitializeConditionPopup_Product();

            //Conditions.AddComboBox("p_productdefVersion", new SqlQuery("GetProductVersion", "10001"), "DISPLAYVERSION", "VALUEVERSION")
            //    .SetLabel("REV")
            //    .SetRelationIds("p_productdefId")
            //    .SetDefault("전체조회")
            //    .SetEmptyItem()
            //    .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
            //    .SetPosition(1.4);
            
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            SmartComboBox plantCombo = Conditions.GetControl<SmartComboBox>("P_PLANTID");
            plantCombo.EditValueChanged += (s, e) =>
            {
                popupInboundArea.Enabled = pnlToolbar.Controls["layoutToolbar"].Controls["Receive"].Enabled;
            };
        }

        /// <summary>
        /// 작업장 조회조건
        /// </summary>
        private void InitializeConditionPopup_Area()
        {
            // 팝업 컬럼설정
            var areaPopup = Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAuthorityUserUseArea", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"P_USERID={UserInfo.Current.Id}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("AREA")
               .SetPopupResultCount(1)
               .SetPosition(1.3)
               .SetRelationIds("p_plantId");

            // 팝업 조회조건
            areaPopup.Conditions.AddTextBox("AREAIDNAME")
                .SetLabel("AREAIDNAME");

            // 팝업 그리드
            areaPopup.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetValidationKeyColumn();
            areaPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }

        /// <summary>
        /// 품목 조회조건
        /// </summary>
        private void InitializeConditionPopup_Product()
        {
            // 팝업 컬럼설정
            var productPopup = Conditions.AddSelectPopup("p_productdefId", new SqlQuery("GetProductListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFIDVERSION")
               .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(600, 800)
               .SetLabel("PRODUCT")
               .SetPopupAutoFillColumns("PRODUCTDEFNAME")
               .SetPopupResultCount(1)
               .SetPosition(1.4);

            // 팝업 조회조건
            productPopup.Conditions.AddTextBox("PRODUCTDEFIDNAME");

            // 팝업 그리드
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetValidationKeyColumn();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 220);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFIDVERSION", 100)
                .SetIsHidden();
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
            grdTakeover.View.CheckValidation();

            DataTable changed = grdTakeover.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function   

        /// <summary>
        /// 인수처리된 불량 Lot의 하위 불량코드중 확정된 Lot List를 검색한다.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<object> IsInboundLotDefectNotConfirm(DataTable dt)
        {
            string lotList = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i + 1 == dt.Rows.Count)
                {
                    lotList += dt.Rows[i]["LOTID"];
                }
                else
                {
                    lotList += dt.Rows[i]["LOTID"] + ",";
                }
            }

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LOTLIST", lotList);

            DataTable defectDt = SqlExecuter.Query("GetInbountDefectLotState", "10001", param);
            List<object> list = defectDt.AsEnumerable().Where(r => Format.GetString(r["STATUS"]).Equals("Confirm")).Select(r => r["LOTID"]).Distinct().ToList();

            return list;
        }

        /// <summary>
        /// 로그인한 사용자가 해당 작업장에 대한 수정권한이 있는지 체크 (Table 단위)
        /// </summary>
        /// <param name="dt"></param>
        private void CheckAuthorityArea(DataTable dt)
        {
            List<object> noAuthorityArea = dt.AsEnumerable().Where(r => r["ISMODIFY"].Equals("N")).Select(r => r["AREANAME"]).Distinct().ToList();

            if (noAuthorityArea.Count > 0)
            {
                string areaList = "";

                for (int i = 0; i < noAuthorityArea.Count; i++)
                {
                    if (i == noAuthorityArea.Count - 1) areaList += noAuthorityArea[i];
                    else areaList += noAuthorityArea[i] + ", ";
                }

                throw MessageException.Create("NoMatchingAreaUser", areaList);
            }
        }

        #endregion

        #region InitalizePopup

        /// <summary>
        /// Popup 컨트롤 초기화
        /// </summary>
        private void InitializePopup()
        {
            popupInboundArea.SelectPopupCondition = InboundAreaPopup();
        }

        /// <summary>
        /// 인수작업장 팝업
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup InboundAreaPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            popup.SetPopupLayoutForm(500, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("AREALIST", PopupButtonStyles.Ok_Cancel, true, true);
            popup.Id = "AREA";
            popup.SearchQuery = new SqlQuery("GetDefectArea", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "AREANAME";
            popup.ValueFieldName = "AREAID";
            popup.LanguageKey = "RECEIVEAREA";
            popup.SetPopupAutoFillColumns("AREANAME");
            popup.SetRelationIds(UserInfo.Current.Plant);
            popup.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                foreach (DataRow row in selectedRow)
                {
                    if (row["ISMODIFY"].Equals("N")) throw MessageException.Create("NoMatchingAreaUser", Format.GetString(row["AREANAME"]));
                }
            });

            popup.Conditions.AddTextBox("AREAIDNAME");

            popup.GridColumns.AddTextBoxColumn("AREAID", 120);
            popup.GridColumns.AddTextBoxColumn("AREANAME", 180);

            return popup;
        }

        #endregion
    }
}
