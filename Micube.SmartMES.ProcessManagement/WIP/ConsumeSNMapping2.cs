#region using

using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정 관리 > 투입관리 > 자재 연결
    /// 업  무  설  명  : InTransit 상태의 LOT을 InProduction 상태로 바꾸고 새로운 수주와 연결 시킨다.
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2020-01-29
    /// 수  정  이  력  : 
    /// 
    /// </summary>
    public partial class ConsumeSNMapping2 : SmartConditionManualBaseForm
    {
        #region Local Variables

        private const string RTRSHT_RTR = "RTR"; // Roll
        private const string RTRSHT_SHT = "SHT"; // Sheet

        private bool _isShowMessage = false;

        #endregion
        
        #region 생성자

        public ConsumeSNMapping2()
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

            UseAutoWaitArea = false;
            InitializeGrid();
            InitializeEvent();
            SetExtraPanelReadOnly(true);
        }

        /// <summary>        
        /// 그리드를 초기화 한다.
        /// </summary>
        private void InitializeGrid()
        {
            #region 연결 대상 LOT 그리드
            grdMappingLot.GridButtonItem = GridButtonItem.None;
            grdMappingLot.ShowButtonBar = false;
            grdMappingLot.ShowStatusBar = false;
            grdMappingLot.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdMappingLot.View.SetIsReadOnly();
            grdMappingLot.View.EnableRowStateStyle = false;

            grdMappingLot.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetTextAlignment(TextAlignment.Left);                              // 품목 코드
            grdMappingLot.View.AddTextBoxColumn("PRODUCTREVISION", 70).SetTextAlignment(TextAlignment.Left);                            // 품목 버전
            grdMappingLot.View.AddTextBoxColumn("PRODUCTDEFNAME", 270).SetTextAlignment(TextAlignment.Left);                            // 품목 명
            grdMappingLot.View.AddSpinEditColumn("LOTID", 220).SetTextAlignment(TextAlignment.Center);                                  // LOT ID
            grdMappingLot.View.AddTextBoxColumn("AREANAME", 160).SetTextAlignment(TextAlignment.Left);                                  // 작업장 명
            grdMappingLot.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 160).SetTextAlignment(TextAlignment.Left);                        // 공정 명
            grdMappingLot.View.AddSpinEditColumn("TRANSITSTATUS", 70).SetTextAlignment(TextAlignment.Center).SetLabel("TRANSITSTATE");  // 물류상태 명
            grdMappingLot.View.AddSpinEditColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right);                                 // PANEL 수량
            grdMappingLot.View.AddSpinEditColumn("QTY", 80).SetTextAlignment(TextAlignment.Right);                                      // PCS 수량
            grdMappingLot.View.AddSpinEditColumn("EXTENT", 80).SetTextAlignment(TextAlignment.Right);                                   // 면적

            grdMappingLot.View.AddSpinEditColumn("PLANTID", 150).SetTextAlignment(TextAlignment.Center).SetIsHidden();                  // 공장 코드   
            grdMappingLot.View.AddTextBoxColumn("AREAID", 100).SetTextAlignment(TextAlignment.Left).SetIsHidden();                      // 작업장 코드
            grdMappingLot.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetTextAlignment(TextAlignment.Left).SetIsHidden();            // 공정 코드
            grdMappingLot.View.AddSpinEditColumn("TRANSITSTATUSCODE", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden();         // 물류상태 코드

            grdMappingLot.View.PopulateColumns();
            grdMappingLot.View.OptionsView.ShowIndicator = false;
            #endregion

            #region 자재 리스트 그리드(MappingLot 의 자재 중 해당 Plant 에서 투입되는 반제품 목록)
            grdMaterial.GridButtonItem = GridButtonItem.None;
            grdMaterial.ShowButtonBar = false;
            grdMaterial.ShowStatusBar = false;
            grdMaterial.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdMaterial.View.EnableRowStateStyle = false;

            grdMaterial.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetIsReadOnly();                             // 품목코드
            grdMaterial.View.AddTextBoxColumn("PRODUCTDEFNAME", 300).SetIsReadOnly();                           // 품목명
            grdMaterial.View.AddComboBoxColumn("RTRSHT", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RTRSHT", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();                                                                               // Roll/Sheet
            grdMaterial.View.AddSpinEditColumn("PNLQTY", 90).SetIsReadOnly();                                   // 수량(PNL) (상위 품목의 Panel 수량)
            grdMaterial.View.AddSpinEditColumn("LOTSIZE", 90);                                                  // Lot Size
            grdMaterial.View.AddSpinEditColumn("QTY", 90).SetIsReadOnly();                                      // 수량 (해당 품목의 PCS 수량 = PNLQTY * JOINTQTY)
            grdMaterial.View.AddSpinEditColumn("JOINTQTY", 90).SetIsReadOnly();                                 // 접합수 (BOM 수량 x 패널당 PCS수)
            grdMaterial.View.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID")
                .SetIsReadOnly();                                                                               // 단위

            grdMaterial.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetIsReadOnly().SetIsHidden();          // 품목버전

            grdMaterial.View.PopulateColumns();
            grdMaterial.View.OptionsView.ShowIndicator = false;
            #endregion

            #region 자재 LOT 그리드(LOT 생성 대상)
            grdMaterialLot.GridButtonItem = GridButtonItem.None;
            grdMaterialLot.ShowButtonBar = false;
            grdMaterialLot.ShowStatusBar = false;
            grdMaterialLot.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdMaterialLot.View.SetIsReadOnly();

            grdMaterialLot.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Left);     // 품목코드
            grdMaterialLot.View.AddTextBoxColumn("PRODUCTDEFNAME", 300).SetTextAlignment(TextAlignment.Left);   // 품목명
            grdMaterialLot.View.AddTextBoxColumn("LOTID", 220).SetTextAlignment(TextAlignment.Center);          // LOT ID
            grdMaterialLot.View.AddSpinEditColumn("PNLQTY", 80).SetTextAlignment(TextAlignment.Right);          // PANEL 수량
            grdMaterialLot.View.AddSpinEditColumn("QTY", 80).SetTextAlignment(TextAlignment.Right);             // PCS 수량

            grdMaterialLot.View.PopulateColumns();
            grdMaterialLot.View.OptionsView.ShowIndicator = false;
            #endregion
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            this.Shown += ConsumeSNMapping2_Shown;
            grdMappingLot.View.CheckStateChanged += grdWipView_CheckStateChanged;
            grdMaterial.View.CellValueChanged += grdMaterialView_CellValueChanged;
            grdMaterial.View.RowCellStyle += grdMaterialView_RowCellStyle;
        }

        private void ConsumeSNMapping2_Shown(object sender, EventArgs e)
        {
            smartSpliterContainer3.SplitterPosition = smartSpliterContainer3.Width / 3 * 2;
        }

        private void grdWipView_CheckStateChanged(object sender, EventArgs e)
        {
            BandedGridHitInfo hitInfo = grdMappingLot.View.CalcHitInfo(grdMappingLot.View.GridControl.PointToClient(Cursor.Position));
            BandedGridHitTest hitTest = hitInfo.HitTest;

            //int rowHandle = grdMappingLot.View.FocusedRowHandle;

            if (hitTest == BandedGridHitTest.RowCell)
            {
                int rowHandle = grdMappingLot.View.FocusedRowHandle;

                if (grdMappingLot.View.IsRowChecked(rowHandle))
                {
                    string transit = Format.GetTrimString(grdMappingLot.View.GetRowCellValue(rowHandle, "TRANSITSTATUSCODE"));
                    if (!transit.Equals("InStock"))
                    {
                        ShowMessage("CheckLogisticStatus");
                        grdMappingLot.View.CheckStateChanged -= grdWipView_CheckStateChanged;
                        grdMappingLot.View.CheckRow(rowHandle, false);
                        grdMappingLot.View.CheckStateChanged += grdWipView_CheckStateChanged;
                        return;
                    }
                }

                //InStock

                SearchMaterialList();
                CreateMaterialLotList();
            }
            else
            {
                var handles = grdMappingLot.View.GetCheckedRowsHandle();

                bool check = handles.Contains(grdMappingLot.View.RowCount - 1);
                foreach (int handle in handles)
                {
                    string transit = Format.GetTrimString(grdMappingLot.View.GetRowCellValue(handle, "TRANSITSTATUSCODE"));
                    if (!transit.Equals("InStock"))
                    {
                        _isShowMessage = true;

                        grdMappingLot.View.CheckStateChanged -= grdWipView_CheckStateChanged;
                        grdMappingLot.View.CheckRow(handle, false);
                        grdMappingLot.View.CheckStateChanged += grdWipView_CheckStateChanged;
                    }
                }

                if (check)
                {
                    if (_isShowMessage)
                    {
                        ShowMessage("CheckLogisticStatus");
                        _isShowMessage = false;
                        return;
                    }

                    SearchMaterialList();
                    CreateMaterialLotList();
                }
            }
        }

        private void grdMaterialView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "LOTSIZE")
            {
                CreateMaterialLotList();
            }
        }

        private void grdMaterialView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            if (e.Column.FieldName.ToUpper() == "LOTSIZE")
            {
                e.Appearance.BackColor = System.Drawing.Color.Yellow;
            }
        }

        private void SearchMaterialList()
        {
            DataTable checkedRows = grdMappingLot.View.GetCheckedRows();
            if (checkedRows.Rows.Count == 0)
            {
                grdMaterial.DataSource = null;
                grdMaterialLot.DataSource = null;
                return;
            }

            decimal totalPanelQty = checkedRows.AsEnumerable().Sum(r => Format.GetInteger(r.Field<object>("PANELQTY"))).ToDecimal();
            lseStandardInputPnl.EditValue = totalPanelQty;

            // 자재 리스트 조회
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("PLANTID", checkedRows.Rows[0]["PLANTID"].ToString());
            param.Add("PRODUCTDEFID", checkedRows.Rows[0]["PRODUCTDEFID"].ToString());
            param.Add("PRODUCTDEFVERSION", checkedRows.Rows[0]["PRODUCTDEFVERSION"].ToString());
            DataTable dtProductList = SqlExecuter.Query("SelectProductDefinitionListByProductionOrder", "10004", param);

            foreach (DataRow each in dtProductList.Rows)
            {
                double jointQty = Format.GetDouble(each["JOINTQTY"], 0);
                each["QTY"] = (int)(Format.GetDouble(totalPanelQty, 0) * jointQty);
                each["PNLQTY"] = totalPanelQty;
                each["LOTSIZE"] = 0;
            }
            grdMaterial.DataSource = dtProductList;
        }

        private void CreateMaterialLotList()
        {
            DataTable dtLotList = CreateEmptyLotListDataTable();
            var materialList = grdMaterial.DataSource as DataTable;
            if(materialList == null)
            {
                return;
            }
            foreach (DataRow eachProduct in materialList.Rows)
            {
                string rtrSht = eachProduct["RTRSHT"].ToString();
                if (string.IsNullOrEmpty(rtrSht))
                {
                    // 해당 품목의 Roll/Sheet 구분이 등록되지 않았습니다. 품목 기준정보를 확인하시기 바랍니다. {0}
                    ShowMessage("NotInputProductRTRSHT", string.Format("Product Id : {0}, Product Version : {1}", Format.GetString(eachProduct["PRODUCTDEFID"]), Format.GetString(eachProduct["PRODUCTDEFVERSION"])));
                    return;
                }

                string siteCode = GetSiteCode(UserInfo.Current.Plant);  // TODO : 확인필요
                if(siteCode == null)
                {
                    // 등록되지 않은 투입 SITE 코드입니다. {0}
                    ShowMessage("InputSiteCodeNotRegistered", string.Format("Plant Id : {0}", UserInfo.Current.Plant));
                    return;
                }

                string lotPrefix = DateTime.Now.ToString("yyMMdd") + siteCode;
                int inputSequece = GetNextInputSequence(lotPrefix);
                lotPrefix += inputSequece.ToString("000");

                string materialClass = Format.GetString(eachProduct["MATERIALCLASS"]);
                string materialSequence = Format.GetString(eachProduct["MATERIALSEQUENCE"]);
                if (string.IsNullOrWhiteSpace(materialClass) || string.IsNullOrWhiteSpace(materialSequence))
                {
                    // 자재품목구분 또는 자재순번이 등록되지 않았습니다. 자재품목구분, 자재순번을 확인하시기 바랍니다. {0}
                    throw MessageException.Create("NotExistsMaterialInfo", string.Format("Product Id : {0}, Product Version : {1}, Material Class : {2}, Material Sequence : {3}", Format.GetString(eachProduct["PRODUCTDEFID"]), Format.GetString(eachProduct["PRODUCTDEFVERSION"]), materialClass, materialSequence));
                }
                string materialCode = materialClass + Format.GetInteger(materialSequence).ToString("00");

                double productPanelQty = Format.GetDouble(eachProduct["PNLQTY"], 0);
                double lotSize = Format.GetDouble(eachProduct["LOTSIZE"], 0);
                double jointQty = Format.GetDouble(eachProduct["JOINTQTY"], 0);

                List<string> lotSequence = new List<string>();
                string splitSequence = "000";
                switch (rtrSht)
                {
                    case RTRSHT_RTR:
                        lotSequence.Add("000");
                        splitSequence = "000";
                        break;
                    case RTRSHT_SHT:
                        int numberOfLots = (int)(productPanelQty / lotSize) + (productPanelQty % lotSize == 0 ? 0 : 1);
                        for (int i = 0; i < numberOfLots; i++)
                        {
                            lotSequence.Add((i + 1).ToString("000"));
                        }
                        splitSequence = "001";
                        break;
                }

                int addCount = 0;

                string reInputSequence = "1";
                foreach (string sequence in lotSequence)
                {
                    string lotId = string.Join("-", lotPrefix, reInputSequence, materialCode, sequence, splitSequence);
                    DataRow eachLot = dtLotList.NewRow();
                    eachLot["LOTID"] = lotId;
                    eachLot["ENTERPRISEID"] = UserInfo.Current.Enterprise;  // TODO : 확인필요
                    eachLot["PLANTID"] = UserInfo.Current.Plant;            // TODO : 확인필요
                    eachLot["PRODUCTDEFID"] = eachProduct["PRODUCTDEFID"];
                    eachLot["PRODUCTDEFVERSION"] = eachProduct["PRODUCTDEFVERSION"];
                    eachLot["PRODUCTDEFNAME"] = eachProduct["PRODUCTDEFNAME"];
                    eachLot["PNLQTY"] = rtrSht == RTRSHT_RTR ? (int)productPanelQty : (int)(addCount == (int)(productPanelQty / lotSize) ? (productPanelQty % lotSize) : lotSize);
                    eachLot["QTY"] = rtrSht == RTRSHT_RTR ? (int)(productPanelQty * jointQty) : (int)(Format.GetDouble(eachLot["PNLQTY"], 0) * jointQty);
                    eachLot["UNIT"] = eachProduct["UNIT"];
                    eachLot["LOTSIZE"] = eachProduct["LOTSIZE"];
                    eachLot["JOINTQTY"] = eachProduct["JOINTQTY"];
                    dtLotList.Rows.Add(eachLot);
                    addCount++;
                }
            }
            dtLotList.AcceptChanges();
            grdMaterialLot.DataSource = dtLotList;
        }

        private int GetNextInputSequence(string lotPrefix)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LOTID", lotPrefix);
            DataTable dtSequence = SqlExecuter.Query("GetLotIdMaxSequence", "10001", param);
            return Format.GetInteger(dtSequence.Rows.Cast<DataRow>().FirstOrDefault()["SEQUENCE"]);
        }

        private DataTable CreateEmptyLotListDataTable()
        {
            DataTable result = new DataTable();
            result.Columns.Add("LOTID");                // LOT ID
            result.Columns.Add("ENTERPRISEID");         // 회사 코드
            result.Columns.Add("PLANTID");              // 공장 코드
            result.Columns.Add("PRODUCTDEFID");         // 품목 코드
            result.Columns.Add("PRODUCTDEFVERSION");    // 품목 버전
            result.Columns.Add("PRODUCTDEFNAME");       // 품목 코드
            result.Columns.Add("PNLQTY");               // Panel 수량
            result.Columns.Add("QTY");                  // PCS 수량
            result.Columns.Add("UNIT");                 // 단위
            result.Columns.Add("LOTSIZE");              // LOT 크기
            result.Columns.Add("JOINTQTY");             // 합수
            return result;
        }

        private string GetSiteCode(string plantId)
        {
            switch (plantId)
            {
                case "IFC":
                    return "F";
                case "IFV":
                    return "V";
                case "CCT":
                    return "C";
                case "YPE":
                    return "Y";
                case "YPEV":
                    return "P";
                default:
                    return null;
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

            string[] salesOrder = Format.GetString(txtSalesOrderNo.EditValue).Split('|');
            string salesOrderId = salesOrder[0].Trim();
            string salesOrderLineNo = salesOrder[1].Trim();

            DataTable dtMappingLots = grdMappingLot.View.GetCheckedRows();
            DataTable dtMaterialLots = grdMaterialLot.DataSource as DataTable;

            try
            {
                pnlContent.ShowWaitArea();

                MessageWorker worker = new MessageWorker("SaveConsumConnect2");
                worker.SetBody(new MessageBody()
                {
                    { "ProductionOrderId", salesOrderId },
                    { "LineNo", salesOrderLineNo },
                    { "UserId", UserInfo.Current.Id },
                    { "PrintLotCard", chkPrintLotCard.Checked ? "Y" : "N" },
                    { "Lotlist", dtMappingLots },
                    { "MaterialLots", dtMaterialLots },
                    { "Comment", txtComment.Text.Trim() }
                });
                worker.Execute();
            }
            catch (Exception ex)
            {
                throw MessageException.Create(ex.Message);
            }
            finally
            {
                pnlContent.CloseWaitArea();
            }
            
            // LOT CARD 출력
            if (chkPrintLotCard.Checked)
            {
                string mappingLotIds = string.Join(",", dtMappingLots.AsEnumerable().Select(row => Format.GetString(row["LOTID"])));
                CommonFunction.PrintLotCard(mappingLotIds, LotCardType.Normal);
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

            ClearAllData();
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dt = await QueryAsync("GetConsumableLotConnectTarget", "10002", values);
            grdMappingLot.DataSource = dt;

            string productDefId = values["P_PRODUCTDEFID"].ToString().Split('|')[0];
            string productDefVersion = values["P_PRODUCTDEFID"].ToString().Split('|')[1];
            SetPorductionOrderSelectPopup(productDefId, productDefVersion, values["P_PLANTID"].ToString());
            lseStandardInputPnl.EditValue = null;

            SetExtraPanelReadOnly(false);

            // 검색 결과에 데이터가 없는 경우
            if (dt.Rows.Count < 1)
            {
                grdMaterial.DataSource = null;
                grdMaterialLot.DataSource = null;
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
            }
            else
            {
                txtProduct.Text = dt.Rows[0]["PRODUCTDEFNAME"].ToString();
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            CommonFunction.AddConditionLotPopup("P_LOTID", 0.1, true, Conditions);      // LOT ID
            AddConditionProductPopup("P_PRODUCTDEFID", 0.2, false, Conditions);         // 품목
            CommonFunction.AddConditionAreaPopup("P_AREAID", 0.3, false, Conditions);   // 작업장
        }

        /// <summary>
        /// 품목코드 조회 팝업
        /// </summary>
        /// <param name="id"></param>
        /// <param name="position"></param>
        /// <param name="isMultiSelect"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        private ConditionCollection AddConditionProductPopup(string id, double position, bool isMultiSelect, ConditionCollection conditions)
        {
            // SelectPopup 항목 추가
            var conditionProductId = conditions.AddSelectPopup(id, new SqlQuery("GetProductDefinitionList", "10001"), "PRODUCTDEFNAME", "PRODUCTDEF")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("UNIT")
                .SetLabel("PRODUCTDEFID")
                .SetPosition(position)
                .SetValidationIsRequired();

            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
            {
                conditionProductId.SetPopupResultCount(0);
            }
            else
            {
                conditionProductId.SetPopupResultCount(1);
            }

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                                        .SetDefault("Product");

            // 팝업 그리드에서 보여줄 컬럼 정의
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

            return conditions;
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            if (string.IsNullOrEmpty(txtSalesOrderNo.EditValue.ToString()))
            {
                //Salse Order 선택
                throw MessageException.Create("CheckSelectSalseOrder");
            }

            DataTable checkedLots = grdMappingLot.View.GetCheckedRows();
            if (checkedLots == null || checkedLots.Rows.Count == 0)
            {
                //Lot 선택여부
                throw MessageException.Create("ChecSelectLot");
            }
        }

        #endregion

        #region Private Function

        private void ClearAllData()
        {
            ClearInputData();
            grdMaterial.View.ClearDatas();
            grdMappingLot.View.ClearDatas();
        }

        private void ClearInputData()
        {
            txtProduct.Text = string.Empty;
            txtSalesOrderNo.EditValue = string.Empty;
            txtComment.EditValue = string.Empty;
        }

        private void SetExtraPanelReadOnly(bool isReadonly)
        {
            txtSalesOrderNo.Properties.ReadOnly = isReadonly;
            txtComment.Properties.ReadOnly = isReadonly;
        }

        private void SetPorductionOrderSelectPopup(string productDefId, string productDefVersion, string plantid)
        {
            ConditionItemSelectPopup cond = new ConditionItemSelectPopup();
            cond.Id = "PRODUCTIONORDER";
            cond.SearchQuery = new SqlQuery("GetProductionOrderIdList", "10004"
                , $"PLANTID={plantid}", $"PRODUCTDEFID={productDefId}", $"PRODUCTDEFVERSION={productDefVersion}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            cond.ValueFieldName = "VALUEFIELD";
            cond.DisplayFieldName = "VALUEFIELD";
            cond.SetPopupLayout("SELECTPRODUCTIONORDER", PopupButtonStyles.Ok_Cancel, true, true);
            cond.SetPopupResultCount(1);
            cond.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow);

            // 그리드의 컬럼
            cond.GridColumns.AddTextBoxColumn("PRODUCTIONORDERID", 160).SetTextAlignment(TextAlignment.Center);
            cond.GridColumns.AddTextBoxColumn("LINENO", 70).SetTextAlignment(TextAlignment.Center);
            cond.GridColumns.AddTextBoxColumn("STATE", 90).SetTextAlignment(TextAlignment.Center);
            cond.GridColumns.AddTextBoxColumn("PLANENDTIME", 100).SetTextAlignment(TextAlignment.Center);
            cond.GridColumns.AddTextBoxColumn("PLANQTY", 100).SetDisplayFormat("#,##0").SetTextAlignment(TextAlignment.Right);
            cond.GridColumns.AddTextBoxColumn("OWNER", 80).SetTextAlignment(TextAlignment.Center);
            cond.GridColumns.AddTextBoxColumn("DESCRIPTION", 150);

            cond.GridColumns.AddTextBoxColumn("VALUEFIELD", 150).SetIsHidden();

            // 검색조건
            cond.Conditions.AddTextBox("PRODUCTIONORDERID");

            txtSalesOrderNo.Editor.SelectPopupCondition = cond;
        }

        #endregion
    }
}
