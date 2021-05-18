#region using

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.SmartMES.Commons;

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

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정 관리 > 투입관리 > LOT 생성
    /// 업  무  설  명  : EPR에서 Interface 된 Product Order 정보로 Lot을 생성 한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-06-17
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LotCreateSheet : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public LotCreateSheet()
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
            InitializeProductionOrderGrid();
            InitializeProductListGrid();
            InitializeLotListGrid();
        }

        /// <summary>        
        /// 수주 리스트 그리드를 초기화 한다.
        /// </summary>
        private void InitializeProductionOrderGrid()
        {
            grdProductionOrderList.GridButtonItem = GridButtonItem.None;
            grdProductionOrderList.ShowButtonBar = false;
            grdProductionOrderList.ShowStatusBar = false;

            grdProductionOrderList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdProductionOrderList.View.SetIsReadOnly();
            grdProductionOrderList.View.SetSortOrder("PRODUCTIONORDERID");
            grdProductionOrderList.View.SetSortOrder("MAINSEQ");
            grdProductionOrderList.View.SetSortOrder("SUBSEQ");
            grdProductionOrderList.View.EnableRowStateStyle = false;


            if(UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_InterFlex))
            {
                grdProductionOrderList.View.AddTextBoxColumn("CALDATE", 120).SetIsHidden();
            }
            else
            {
                grdProductionOrderList.View.AddTextBoxColumn("CALDATE", 120);
            }

            // 생산구분
            grdProductionOrderList.View.AddComboBoxColumn("PRODUCTIONTYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);
            // S/O번호
            grdProductionOrderList.View.AddTextBoxColumn("PRODUCTIONORDERID", 100);
            // 라인
            grdProductionOrderList.View.AddTextBoxColumn("LINENO", 50);
            // 순서
            grdProductionOrderList.View.AddSpinEditColumn("MAINSEQ", 60)
                .SetIsHidden();
            // 순서
            grdProductionOrderList.View.AddSpinEditColumn("SUBSEQ", 60)
                .SetIsHidden();
            // 품목코드
            grdProductionOrderList.View.AddTextBoxColumn("PRODUCTDEFID", 100);
            // 품목버전
            grdProductionOrderList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            // 품목명
            grdProductionOrderList.View.AddTextBoxColumn("PRODUCTDEFNAME", 300);
            // 단가
            grdProductionOrderList.View.AddSpinEditColumn("SALESPRICE", 80)
                .SetDisplayFormat("#,##0.0");
            // Layer
            grdProductionOrderList.View.AddTextBoxColumn("LAYER", 50)
                .SetTextAlignment(TextAlignment.Center);
            // 합수
            grdProductionOrderList.View.AddSpinEditColumn("PCSPNL", 60)
                .SetLabel("ARRAY");
            // 산출수
            grdProductionOrderList.View.AddSpinEditColumn("PCSMM", 60)
                .SetLabel("CALCULATION");
            // 수주량
            grdProductionOrderList.View.AddSpinEditColumn("PLANQTY", 70);
            // 수주PNL
            grdProductionOrderList.View.AddSpinEditColumn("PLANPNLQTY", 60);
            if (UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_InterFlex))
            {
                grdProductionOrderList.View.AddSpinEditColumn("CALPNL", 70).SetIsHidden();
            }
            else
            {
                grdProductionOrderList.View.AddSpinEditColumn("CALPNL", 70);
            }
                // 면적
            grdProductionOrderList.View.AddSpinEditColumn("PLANMM", 80)
            .SetLabel("EXTENT")
            .SetDisplayFormat("#,##0.00");
            // 분할여부
            grdProductionOrderList.View.AddComboBoxColumn("ISSPLIT", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);
            // Lot 생성 여부
            grdProductionOrderList.View.AddComboBoxColumn("ISLOTCREATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);
            // 사양결재여부
            grdProductionOrderList.View.AddComboBoxColumn("ISSPECAPPROVE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);
            // 수주상태
            grdProductionOrderList.View.AddComboBoxColumn("STATE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=SalesOrderState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsHidden();
            // Roll/Sheet
            grdProductionOrderList.View.AddComboBoxColumn("RTRSHT", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RTRSHT", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsHidden();
            // 순수주
            grdProductionOrderList.View.AddSpinEditColumn("PUREORDER", 90)
                .SetIsHidden();
            // 순투입
            grdProductionOrderList.View.AddSpinEditColumn("PUREINPUT", 90)
                .SetIsHidden();
            // 잉여재고
            grdProductionOrderList.View.AddSpinEditColumn("SURPLUSSTOCK", 90)
                .SetIsHidden();
            // 잉여재공
            grdProductionOrderList.View.AddSpinEditColumn("SURPLUSWIP", 90)
                .SetIsHidden();
            // 부족
            grdProductionOrderList.View.AddSpinEditColumn("UNDERAGE", 90)
                .SetIsHidden();
            // 기준투입
            grdProductionOrderList.View.AddSpinEditColumn("STDINPUTPNLQTY", 90)
                .SetIsHidden();
            // LOT PNL
            grdProductionOrderList.View.AddSpinEditColumn("LOTINPUTPNLQTY", 90)
                .SetIsHidden();


            grdProductionOrderList.View.PopulateColumns();


            grdProductionOrderList.View.OptionsView.ShowIndicator = false;
        }

        /// <summary>
        /// 품목 List 그리드를 초기화 한다.
        /// </summary>
        private void InitializeProductListGrid()
        {
            grpProductList.GridButtonItem = GridButtonItem.None;

            grdProductList.GridButtonItem = GridButtonItem.None;
            grdProductList.ShowButtonBar = false;
            grdProductList.ShowStatusBar = false;

            grdProductList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdProductList.View.EnableRowStateStyle = false;


            // 품목코드
            grdProductList.View.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly();
            // 품목버전
            grdProductList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                .SetIsReadOnly()
                .SetIsHidden();
            // 품목명
            grdProductList.View.AddTextBoxColumn("PRODUCTDEFNAME", 300)
                .SetIsReadOnly();
            // Roll/Sheet
            grdProductList.View.AddComboBoxColumn("RTRSHT", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RTRSHT", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();
            // 수량(PNL)
            grdProductList.View.AddSpinEditColumn("PNLQTY", 90);
            // Lot Size
            grdProductList.View.AddSpinEditColumn("LOTSIZE", 90);
            // 수량
            grdProductList.View.AddSpinEditColumn("QTY", 90)
                .SetIsReadOnly();
            // 접합수
            grdProductList.View.AddSpinEditColumn("JOINTQTY", 90)
                .SetIsReadOnly();
            // 단위
            grdProductList.View.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID")
                .SetIsReadOnly();
            // 실투입로스
            grdProductList.View.AddSpinEditColumn("ACTUALINPUTLOSS", 90)
                .SetIsReadOnly()
                .SetDisplayFormat("#,##0.00");
            // 순수주로스
            grdProductList.View.AddSpinEditColumn("PUREORDERLOSS", 90)
                .SetIsReadOnly()
                .SetDisplayFormat("#,##0.00");
            // 자재 품목구분
            grdProductList.View.AddTextBoxColumn("MATERIALCLASS", 70)
                .SetIsReadOnly()
                .SetIsHidden();
            // 자재차수
            grdProductList.View.AddTextBoxColumn("MATERIALSEQUENCE", 70)
                .SetIsReadOnly()
                .SetIsHidden();
            // Lot생성여부
            grdProductList.View.AddComboBoxColumn("ISLOTCREATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            // 품목구분
            grdProductList.View.AddTextBoxColumn("PRODUCTDEFTYPE", 100)
                .SetIsReadOnly()
                .SetIsHidden();
            // 생지배수
            grdProductList.View.AddSpinEditColumn("MULTIPLE", 90)
                .SetIsReadOnly()
                .SetIsHidden();
            // 내층공용여부
            grdProductList.View.AddTextBoxColumn("ISINNERPUBLIC", 60)
                .SetIsReadOnly()
                .SetIsHidden();
            // BOM사용회수
            //grdProductList.View.AddSpinEditColumn("USEBOMCNT")
            //    .SetIsReadOnly()
            //    .SetIsHidden();


            grdProductList.View.PopulateColumns();


            grdProductList.View.OptionsView.ShowIndicator = false;
        }

        /// <summary>
        /// LOT List 그리드를 초기화 한다.
        /// </summary>
        private void InitializeLotListGrid()
        {
            grdLotList.GridButtonItem = GridButtonItem.None;
            grdLotList.ShowStatusBar = false;

            grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdLotList.View.SetSortOrder("PRODUCTDEFID");
            grdLotList.View.SetSortOrder("LOTID");
            grdLotList.View.SetIsReadOnly();
            //grdLotList.View.SetSortOrder("LOTID");
            grdLotList.View.EnableRowStateStyle = false;


            // 법인ID
            grdLotList.View.AddTextBoxColumn("ENTERPRISEID", 90)
                .SetIsHidden();
            // 공장ID
            grdLotList.View.AddTextBoxColumn("PLANTID", 90)
                .SetIsHidden();
            // S/O번호
            grdLotList.View.AddTextBoxColumn("PRODUCTIONORDERID", 150)
                .SetIsHidden();
            // 품목코드
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFID", 150);
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                .SetIsHidden();
            // LOT ID
            grdLotList.View.AddTextBoxColumn("LOTID", 200);
            // 수량(PNL)
            grdLotList.View.AddSpinEditColumn("PNLQTY", 90);
            // 수량
            grdLotList.View.AddSpinEditColumn("QTY", 90);
            // 접합수
            grdLotList.View.AddSpinEditColumn("JOINTQTY", 90)
                .SetIsHidden();
            // 순투입
            grdLotList.View.AddSpinEditColumn("PUREINPUT", 90);
            // 순투입/로스
            grdLotList.View.AddSpinEditColumn("PUREINPUTLOSS", 90);
            // 로스율
            grdLotList.View.AddSpinEditColumn("LOSSRATE", 90)
                .SetDisplayFormat("#,##0.00");
            // 순수주
            grdLotList.View.AddSpinEditColumn("PUREORDER", 90);
            // 단위
            grdLotList.View.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID")
                .SetIsHidden();
            // 투입여부
            grdLotList.View.AddComboBoxColumn("ISINPUT", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);
            // Lot생성여부
            grdLotList.View.AddComboBoxColumn("ISLOTCREATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden();
            // 비고
            grdLotList.View.AddTextBoxColumn("DESCRIPTION", 100)
                .SetIsHidden();
            // 품목구분
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFTYPE", 90)
                .SetIsHidden();


            grdLotList.View.PopulateColumns();


            grdLotList.View.OptionsView.ShowIndicator = false;

            if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                chkIsOEM.Visible = true;
            else
                chkIsOEM.Visible = false;
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdProductionOrderList.View.FocusedRowChanged += View_FocusedRowChanged;

            numSurplusStock.Editor.EditValueChanged += Editor_EditValueChanged;
            numSurplusWip.Editor.EditValueChanged += Editor_EditValueChanged;
            numLackQty.Editor.EditValueChanged += Editor_EditValueChanged;
            numStandardInputPnl.Editor.EditValueChanged += Editor_EditValueChanged;
            numLotSize.Editor.EditValueChanged += Editor_EditValueChanged;

            //numStandardInputPnl.Editor.Leave += Editor_Leave;

            chkBaseProduct.CheckedChanged += ChkBaseProduct_CheckedChanged;
            chkEtcProduct.CheckedChanged += ChkEtcProduct_CheckedChanged;

            grdProductList.View.CellValueChanged += View_CellValueChanged;
            grdProductList.View.CheckStateChanged += View_CheckStateChanged;

            btnDivisionComplete.Click += BtnDivisionComplete_Click;
            btnDivisionRelease.Click += BtnDivisionRelease_Click;
            btnCreateCancel.Click += BtnCreateCancel_Click;
        }

        /// <summary>
        /// 수주 리스트 그리드의 선택된 행이 변경될 경우 호출 한다.
        /// 순수주, 순투입, 기준투입(PCS), 기준로스 값을 다시 계산 한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowChanged(e.FocusedRowHandle);
        }

        /// <summary>
        /// 잉여 재고, 잉여 재공, 부족, 기준 투입, LOT PNL 컨트롤이 값이 변경될 경우 호출 한다.
        /// 순수주, 순투입, 기준투입(PCS), 기준로스 값을 다시 계산 한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editor_EditValueChanged(object sender, EventArgs e)
        {
            CalcProductionOrderInfo(grdProductionOrderList.View.FocusedRowHandle);

            SmartSpinEdit editor = sender as SmartSpinEdit;

            if (editor == numStandardInputPnl.Editor || editor == numLotSize.Editor)
            {
                SetProductDefinitionListQtyLoss();
            }
        }
        /// <summary>
        ///  수주선택시 자동 조회 2019.08.01 배선용
        ///  우영민 과장 요청
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LotCreateSheet_EditValueChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit poPopup = sender as SmartSelectPopupEdit;

            if (Format.GetFullTrimString(poPopup.EditValue).Equals(string.Empty))
            {
                poPopup.ClearValue();
                Conditions.GetControl<SmartComboBox>("P_LINENO").EditValue = "*";
            }

            //await this.OnSearchAsync();
        }

        /// <summary>
        /// 기준 투입 컨트롤 값 변경 후 기준로스 계산 결과가 0 보다 큰지 체크한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editor_Leave(object sender, EventArgs e)
        {
            SmartSpinEdit editor = sender as SmartSpinEdit;

            if (!CheckStandardLoss())
            {
                // 기준로스는 0 보다 큰 값이어야 합니다.
                ShowMessage("InValidStandardLoss");

                editor.Focus();

                return;
            }
        }

        private void ChkBaseProduct_CheckedChanged(object sender, EventArgs e)
        {
            if (grdProductList.View.RowCount < 1)
                return;

            SmartCheckBox chk = sender as SmartCheckBox;

            SetProductCheckState("Base", chk.Checked);
        }

        private void ChkEtcProduct_CheckedChanged(object sender, EventArgs e)
        {
            if (grdProductList.View.RowCount < 1)
                return;

            SmartCheckBox chk = sender as SmartCheckBox;

            SetProductCheckState("Etc", chk.Checked);
        }

        /// <summary>
        /// 품목 리스트 그리드의 수량 값을 변경하면 실투입로스, 순수주로스 값을 다시 계산 한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "PNLQTY")
            {
                DataRow row = grdProductList.View.GetDataRow(e.RowHandle);

                double pcsPnl = Format.GetDouble(grdProductionOrderList.View.GetRowCellValue(grdProductionOrderList.View.FocusedRowHandle, "PCSPNL"), 0);

                double pureOrder = Format.GetDouble(numPureOrder.EditValue, 0);
                double pureInput = Format.GetDouble(numPureInput.EditValue, 0);

                double jointQty = Format.GetDouble(row["JOINTQTY"], 0);

                row["QTY"] = (int)(Format.GetDouble(e.Value, 0) * jointQty);

                double actualInputLoss = ((Format.GetDouble(row["QTY"], 0) / (pureInput * (Format.GetDouble(row["JOINTQTY"], 0) / pcsPnl))) - 1) * 100;
                double pureOrderLoss = ((Format.GetDouble(row["QTY"], 0) / (pureOrder * (Format.GetDouble(row["JOINTQTY"], 0) / pcsPnl))) - 1) * 100;

                if (actualInputLoss <= 0)
                {
                    // 실투입로스는 0 보다 큰 값이어야 합니다.
                    ShowMessage("InValidPureInputLoss");
                    return;
                }

                grdProductList.View.SetRowCellValue(e.RowHandle, "ACTUALINPUTLOSS", actualInputLoss);
                grdProductList.View.SetRowCellValue(e.RowHandle, "PUREORDERLOSS", pureOrderLoss);
            }
        }

        /// <summary>
        /// 품목 리스트 그리드의 Row Check 상태가 변경되면 Lot 리스트에 추가 또는 삭제 한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            var checkedRowHandles = grdProductList.View.GetCheckedRowsHandle();

            DataTable dtLotList = (grdLotList.DataSource as DataTable).Clone();

            int focusedRowHandle = grdProductionOrderList.View.FocusedRowHandle;

            string productionOrderId = grdProductionOrderList.View.GetRowCellValue(focusedRowHandle, "PRODUCTIONORDERID").ToString();
            string productDefId = grdProductionOrderList.View.GetRowCellValue(focusedRowHandle, "PRODUCTDEFID").ToString();
            string productDefVersion = grdProductionOrderList.View.GetRowCellValue(focusedRowHandle, "PRODUCTDEFVERSION").ToString();
            string state = grdProductionOrderList.View.GetRowCellValue(focusedRowHandle, "STATE").ToString();

            if (state == "LotCreate")
                return;

            DataTable dt = grdLotList.DataSource as DataTable;

            int rowCount = 0;

            DataTable dataSource = new DataTable();

            if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
            {
                rowCount = dt.AsEnumerable().Where(r => Format.GetString(r["ISLOTCREATE"]) == "Y" && Format.GetString(r["PRODUCTDEFTYPE"]) != "Product").Count();

                int iCheck = dt.AsEnumerable().Where(r => Format.GetString(r["ISLOTCREATE"]) == "Y").Count();

                if (iCheck > 0)
                    dataSource = dt.AsEnumerable().Where(r => Format.GetString(r["ISLOTCREATE"]) == "Y").CopyToDataTable();
                else
                    dataSource = dt.Clone();
            }
            else
            {
                rowCount = dt.AsEnumerable().Where(r => Format.GetString(r["ISLOTCREATE"]) == "Y").Count();

                if (rowCount > 0)
                    dataSource = dt.AsEnumerable().Where(r => Format.GetString(r["ISLOTCREATE"]) == "Y").CopyToDataTable();
                else
                    dataSource = dt.Clone();
            }


            checkedRowHandles.ForEach(rowHandle =>
            {
                DataRow dataRow = grdProductList.View.GetDataRow(rowHandle);

                string isLotCreate = Format.GetString(dataRow["ISLOTCREATE"]);

                if (isLotCreate == "N")
                {
                    string rtrSht = dataRow["RTRSHT"].ToString();

                    if (string.IsNullOrEmpty(rtrSht))
                    {
                        // 해당 품목의 Roll/Sheet 구분이 등록되지 않았습니다. 품목 기준정보를 확인하시기 바랍니다. {0}
                        ShowMessage("NotInputProductRTRSHT", string.Format("Product Id : {0}, Product Version : {1}", Format.GetString(dataRow["PRODUCTDEFID"]), Format.GetString(dataRow["PRODUCTDEFVERSION"])));
                        return;
                    }

                    string siteCode = "";

                    switch (UserInfo.Current.Plant)
                    {
                        case "IFC":
                            siteCode = "F";
                            break;
                        case "IFV":
                            siteCode = "V";
                            break;
                        case "CCT":
                            siteCode = "C";
                            break;
                        case "YPE":
                            siteCode = "Y";
                            break;
                        case "YPEV":
                            siteCode = "P";
                            break;
                    }

                    string productDefType = Format.GetString(dataRow["PRODUCTDEFTYPE"]);

                    string lotNo = "";
                    var migLot = (grdLotList.DataSource as DataTable).AsEnumerable().Where(r => Format.GetString(r["DESCRIPTION"]) == "MIG");
                    int migCount = migLot.Count();

                    if (migCount > 0)
                    {
                        var normalLot = (grdLotList.DataSource as DataTable).AsEnumerable().Where(r => string.IsNullOrEmpty(Format.GetString(r["DESCRIPTION"])));

                        int normalCount = normalLot.Count();

                        if (normalCount > 0)
                        {
                            DataRow normalLotRow = normalLot.FirstOrDefault();
                            lotNo = Format.GetString(normalLotRow["LOTID"]).Substring(0, 10);
                        }
                        else
                        {
                            DataRow migLotRow = migLot.FirstOrDefault();
                            string lotStartNo = Format.GetString(migLotRow["LOTID"]).Substring(2, 6) + siteCode;

                            Dictionary<string, object> values = new Dictionary<string, object>();
                            values.Add("LOTID", lotStartNo);

                            DataTable dtSequence = SqlExecuter.Query("GetLotIdMaxSequence", "10001", values);

                            int sequece = Format.GetInteger(dtSequence.Rows.Cast<DataRow>().FirstOrDefault()["SEQUENCE"]);

                            lotNo = lotStartNo + sequece.ToString("000");
                        }
                    }
                    else
                    {
                        if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong && productDefType == "Product")
                        {
                            string lotStartNo = DateTime.Now.ToString("yyMMdd") + siteCode;

                            Dictionary<string, object> param = new Dictionary<string, object>();
                            param.Add("PRODUCTDEFID", productDefId);
                            param.Add("PRODUCTDEFVERSION", productDefVersion);
                            param.Add("LOTID", lotStartNo);

                            DataTable dtSequence = SqlExecuter.Query("GetLotIdMaxSequenceForProduct", "10001", param);

                            int sequence = Format.GetInteger(dtSequence.AsEnumerable().FirstOrDefault()["SEQUENCE"]);

                            lotNo = lotStartNo + sequence.ToString("000");
                        }
                        else
                        {
                            if (rowCount > 0)
                                lotNo = Format.GetString(dataSource.AsEnumerable().FirstOrDefault()["LOTID"]).Substring(0, 10);
                            else
                            {
                                string lotStartNo = DateTime.Now.ToString("yyMMdd") + siteCode;

                                Dictionary<string, object> values = new Dictionary<string, object>();
                                values.Add("LOTID", lotStartNo);

                                DataTable dtSequence = SqlExecuter.Query("GetLotIdMaxSequence", "10001", values);

                                int sequece = Format.GetInteger(dtSequence.Rows.Cast<DataRow>().FirstOrDefault()["SEQUENCE"]);

                                lotNo = lotStartNo + sequece.ToString("000");
                            }
                        }
                    }
                    string reInput = "1";

                    string materialClass = Format.GetString(dataRow["MATERIALCLASS"]);
                    string materialSequence = Format.GetString(dataRow["MATERIALSEQUENCE"]);

                    if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
                    {
                        if (string.IsNullOrWhiteSpace(materialClass) || string.IsNullOrWhiteSpace(materialSequence))
                        {
                            // 자재품목구분 또는 자재순번이 등록되지 않았습니다. 자재품목구분, 자재순번을 확인하시기 바랍니다. {0}
                            throw MessageException.Create("NotExistsMaterialInfo", string.Format("Product Id : {0}, Product Version : {1}, Material Class : {2}, Material Sequence : {3}", Format.GetString(dataRow["PRODUCTDEFID"]), Format.GetString(dataRow["PRODUCTDEFVERSION"]), materialClass, materialSequence));
                        }
                    }
                    else if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                    {
                        if (string.IsNullOrWhiteSpace(materialClass))
                        {
                            // 자재품목구분이 등록되지 않았습니다. 자재품목구분을 확인하시기 바랍니다. {0}
                            throw MessageException.Create("NotExistsMaterialInfo", string.Format("Product Id : {0}, Product Version : {1}, Material Class : {2}", Format.GetString(dataRow["PRODUCTDEFID"]), Format.GetString(dataRow["PRODUCTDEFVERSION"]), materialClass));
                        }
                    }

                    string material = "";

                    if (Format.GetString(dataRow["PRODUCTDEFTYPE"]) == "SubAssembly" && Format.GetInteger(materialSequence) == 0)
                    {
                        int materialSeq = 1;

                        int cnt = dtLotList.AsEnumerable().Where(r => Format.GetString(r["LOTID"]).Substring(13, 2) == materialClass).Count();

                        if (cnt > 0)
                            materialSeq = cnt + 1;


                        material = materialClass + Format.GetInteger(materialSeq).ToString("00");
                    }
                    else
                    {
                        material = materialClass + Format.GetInteger(materialSequence).ToString("00");
                    }

                    //if (dataRow["PRODUCTDEFID"].Equals(productDefId))
                    //    material = "FG00";
                    //else
                    //    material = dataRow["PRODUCTDEFID"].ToString().Substring(8, 4);
                    List<string> lotDegree = new List<string>();
                    string lotSplit = "000";


                    double planQty = Format.GetDouble(grdProductionOrderList.View.GetRowCellValue(grdProductionOrderList.View.FocusedRowHandle, "PLANQTY"), 0);
                    double pcsPnl = Format.GetDouble(grdProductionOrderList.View.GetRowCellValue(grdProductionOrderList.View.FocusedRowHandle, "PCSPNL"), 0);

                    double pureOrder = Format.GetDouble(numPureOrder.EditValue, 0);
                    double pureInput = Format.GetDouble(numPureInput.EditValue, 0);

                    double standardInput = Format.GetDouble(numStandardInputPnl.EditValue, 0);
                    //double lotPnl = Format.GetDouble(numLotSize.EditValue, 0);
                    double lotPnl = Format.GetDouble(dataRow["LOTSIZE"], 0);

                    double pnlQty = Format.GetDouble(dataRow["PNLQTY"], 0);
                    double jointQty = Format.GetDouble(dataRow["JOINTQTY"], 0);

                    //double rollStandardInput = Format.GetDouble(numRollStandardInput.EditValue, 0);


                    if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                    {
                        if (rtrSht == "RTR" && (materialClass == "FG" || materialClass == "SB"))
                        {
                            //int nRtrLotDegree = (standardQty / rollStandardInput) + (standardQty % rollStandardInput == 0 ? 0 : 1);

                            //for (int i = 0; i < nRtrLotDegree; i++)
                            //{
                            //    lotDegree.Add((i + 1).ToString("000"));
                            //}

                            lotDegree.Add("000");

                            lotSplit = "000";
                        }
                        else
                        {
                            int nShtLotDegree = (int)(pnlQty / lotPnl) + (pnlQty % lotPnl == 0 ? 0 : 1);

                            int totalSeq = 1;
                            int lastSeq = 1;

                            for (int i = 0; i < nShtLotDegree; i++)
                            {
                                if (i >= 999)
                                {
                                    if (totalSeq % 100 == 0)
                                        totalSeq++;

                                    int startIndex = Format.GetInteger(totalSeq.ToString().Substring(0, 2));

                                    if (lastSeq > 99)
                                        lastSeq = 1;

                                    lotDegree.Add(CommonFunction.GetBase36String(startIndex) + lastSeq.ToString("00"));
                                }
                                else
                                {
                                    lotDegree.Add(lastSeq.ToString("000"));
                                }

                                totalSeq++;
                                lastSeq++;
                            }

                            lotSplit = "001";
                        }
                    }
                    else
                    {
                        switch (rtrSht)
                        {
                            case "RTR":
                                //int nRtrLotDegree = (standardQty / rollStandardInput) + (standardQty % rollStandardInput == 0 ? 0 : 1);

                                //for (int i = 0; i < nRtrLotDegree; i++)
                                //{
                                //    lotDegree.Add((i + 1).ToString("000"));
                                //}

                                lotDegree.Add("000");

                                lotSplit = "000";

                                break;
                            case "SHT":
                                int nShtLotDegree = (int)(pnlQty / lotPnl) + (pnlQty % lotPnl == 0 ? 0 : 1);

                                int totalSeq = 1;
                                int lastSeq = 1;

                                for (int i = 0; i < nShtLotDegree; i++)
                                {
                                    if (i >= 999)
                                    {
                                        if (totalSeq % 100 == 0)
                                            totalSeq++;

                                        int startIndex = Format.GetInteger(totalSeq.ToString().Substring(0, 2));

                                        if (lastSeq > 99)
                                            lastSeq = 1;

                                        lotDegree.Add(CommonFunction.GetBase36String(startIndex) + lastSeq.ToString("00"));
                                    }
                                    else
                                    {
                                        lotDegree.Add(lastSeq.ToString("000"));
                                    }

                                    totalSeq++;
                                    lastSeq++;
                                }

                                lotSplit = "001";

                                break;
                        }
                    }


                    int addCount = 0;

                    lotDegree.ForEach(value =>
                    {
                        string lotId = string.Join("-", lotNo, reInput, material, value, lotSplit);
                        

                        DataRow newRow = dtLotList.NewRow();

                        if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
                        {
                            newRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                            newRow["PLANTID"] = UserInfo.Current.Plant;
                            newRow["PRODUCTIONORDERID"] = productionOrderId;
                            newRow["PRODUCTDEFID"] = dataRow["PRODUCTDEFID"];
                            newRow["PRODUCTDEFVERSION"] = dataRow["PRODUCTDEFVERSION"];
                            newRow["LOTID"] = lotId;
                            newRow["PNLQTY"] = rtrSht == "RTR" ? (int)pnlQty : (int)(addCount == (int)(pnlQty / lotPnl) ? (pnlQty % lotPnl) : lotPnl);
                            newRow["QTY"] = rtrSht == "RTR" ? (int)(pnlQty * jointQty) : (int)(Format.GetDouble(newRow["PNLQTY"], 0) * jointQty);
                            newRow["JOINTQTY"] = jointQty;
                            newRow["PUREINPUT"] = (int)((pureInput / pcsPnl) * jointQty * (Format.GetDouble(newRow["PNLQTY"], 0) / standardInput));
                            newRow["PUREINPUTLOSS"] = Format.GetInteger(newRow["QTY"]) - Format.GetInteger(newRow["PUREINPUT"]);
                            newRow["LOSSRATE"] = (Format.GetDecimal(Format.GetDouble(newRow["QTY"], 0) / Format.GetDouble(newRow["PUREINPUT"], 0)) - 1) * 100;
                            newRow["PUREORDER"] = (int)((pureOrder / pcsPnl) * jointQty * (Format.GetDouble(newRow["PNLQTY"], 0) / standardInput));
                            newRow["UNIT"] = dataRow["UNIT"];
                            newRow["ISINPUT"] = "N";
                            newRow["ISLOTCREATE"] = "N";
                            newRow["PRODUCTDEFTYPE"] = dataRow["PRODUCTDEFTYPE"];
                        }
                        else if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                        {
                            newRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                            newRow["PLANTID"] = UserInfo.Current.Plant;
                            newRow["PRODUCTIONORDERID"] = productionOrderId;
                            newRow["PRODUCTDEFID"] = dataRow["PRODUCTDEFID"];
                            newRow["PRODUCTDEFVERSION"] = dataRow["PRODUCTDEFVERSION"];
                            newRow["LOTID"] = lotId;
                            if (rtrSht == "RTR" && (materialClass == "FG" || materialClass == "SB"))
                            {
                                newRow["PNLQTY"] = (int)pnlQty;
                                newRow["QTY"] = (int)(pnlQty * jointQty);
                            }
                            else
                            {
                                newRow["PNLQTY"] = (int)(addCount == (int)(pnlQty / lotPnl) ? (pnlQty % lotPnl) : lotPnl);
                                newRow["QTY"] = (int)(Format.GetDouble(newRow["PNLQTY"], 0) * jointQty);
                            }
                            //newRow["PNLQTY"] = rtrSht == "RTR" ? (int)pnlQty : (int)(addCount == (int)(pnlQty / lotPnl) ? (pnlQty % lotPnl) : lotPnl);
                            //newRow["QTY"] = rtrSht == "RTR" ? (int)(pnlQty * jointQty) : (int)(Format.GetDouble(newRow["PNLQTY"], 0) * jointQty);
                            newRow["JOINTQTY"] = jointQty;
                            newRow["PUREINPUT"] = Math.Ceiling(pureInput * Format.GetDouble(newRow["PNLQTY"], 0) / pnlQty);
                            newRow["PUREINPUTLOSS"] = Format.GetInteger(newRow["QTY"]) - Format.GetInteger(newRow["PUREINPUT"]);
                            newRow["LOSSRATE"] = (Format.GetDecimal(Format.GetDouble(newRow["QTY"], 0) / Format.GetDouble(newRow["PUREINPUT"], 0)) - 1) * 100;
                            newRow["PUREORDER"] = Math.Ceiling(pureOrder * Format.GetDouble(newRow["PNLQTY"], 0) / pnlQty);
                            newRow["UNIT"] = dataRow["UNIT"];
                            newRow["ISINPUT"] = "N";
                            newRow["ISLOTCREATE"] = "N";
                            newRow["PRODUCTDEFTYPE"] = dataRow["PRODUCTDEFTYPE"];
                        }

                        dtLotList.Rows.Add(newRow);

                        addCount++;
                    });

                    dtLotList.AcceptChanges();
                }
            });


            dataSource.Merge(dtLotList);


            grdLotList.DataSource = dataSource;
        }

        /// <summary>
        /// 분할완료 버튼 클릭 시 호출 한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDivisionComplete_Click(object sender, EventArgs e)
        {
            if (grdProductionOrderList.View.FocusedRowHandle < 0)
                return;

            DataRow row = grdProductionOrderList.View.GetDataRow(grdProductionOrderList.View.FocusedRowHandle);

            string productionOrderId = row["PRODUCTIONORDERID"].ToString();
            string lineNo = row["LINENO"].ToString();

            // "S/O번호 : {0}, 라인 : {1}" 수주 정보를 분할완료 처리 하시겠습니까?
            if (ShowMessage(MessageBoxButtons.YesNo, "CompletePoConfirm", productionOrderId, lineNo) == DialogResult.Yes)
            {
                MessageWorker worker = new MessageWorker("SaveCreateLot");
                worker.SetBody(new MessageBody()
                {
                    { "TransactionType", "CompletePO" },
                    { "ProductionOrderId", productionOrderId },
                    { "LineNo", lineNo },
                    { "IsSplit", "Y" }
                });

                worker.Execute();

                // 정상적으로 처리되었습니다.
                ShowMessage("ProcessingSuccess");

                grdProductionOrderList.View.SetRowCellValue(grdProductionOrderList.View.FocusedRowHandle, "ISSPLIT", "Y");
            }
        }

        /// <summary>
        /// 분할해제 버튼 클릭 시 호출 한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDivisionRelease_Click(object sender, EventArgs e)
        {
            if (grdProductionOrderList.View.FocusedRowHandle < 0)
                return;

            DataRow row = grdProductionOrderList.View.GetDataRow(grdProductionOrderList.View.FocusedRowHandle);

            string productionOrderId = row["PRODUCTIONORDERID"].ToString();
            string lineNo = row["LINENO"].ToString();

            // "S/O번호 : {0}, 라인 : {1}" 수주 정보를 분할해제 처리 하시겠습니까?
            if (ShowMessage(MessageBoxButtons.YesNo, "CancelCompletePoConfirm", productionOrderId, lineNo) == DialogResult.Yes)
            {
                MessageWorker worker = new MessageWorker("SaveCreateLot");
                worker.SetBody(new MessageBody()
                {
                    { "TransactionType", "CancelCompletePO" },
                    { "ProductionOrderId", productionOrderId },
                    { "LineNo", lineNo },
                    { "IsSplit", "N" }
                });

                worker.Execute();

                // 정상적으로 처리되었습니다.
                ShowMessage("ProcessingSuccess");

                grdProductionOrderList.View.SetRowCellValue(grdProductionOrderList.View.FocusedRowHandle, "ISSPLIT", "N");
            }
        }

        /// <summary>
        /// 생성취소 버튼 클릭 시 호출 한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreateCancel_Click(object sender, EventArgs e)
        {
            if (grdProductionOrderList.View.FocusedRowHandle < 0)
                return;

            if (grdProductList.View.GetCheckedRows().Rows.Count < 1)
            {
                // 체크된 행이 없습니다
                ShowMessage("GridNoChecked");
                return;
            }

            DataRow row = grdProductionOrderList.View.GetDataRow(grdProductionOrderList.View.FocusedRowHandle);

            string productionOrderId = row["PRODUCTIONORDERID"].ToString();
            string lineNo = row["LINENO"].ToString();

            // "S/O번호 : {0}, 라인 : {1}" 수주 정보에 생성된 Lot을 생성취소 하시겠습니까?
            if (ShowMessage(MessageBoxButtons.YesNo, "CancelCreateLotConfirm", productionOrderId, lineNo) == DialogResult.Yes)
            {
                if (grdProductList.View.GetCheckedRows().AsEnumerable().Where(r => Format.GetString(r["ISLOTCREATE"]) == "Y").Count() < 1)
                {
                    // Lot 생성을 취소할 품목이 선택되지 않았습니다.
                    ShowMessage("NotExistsLotCreateCancel");
                    return;
                }

                DataTable productList = grdProductList.View.GetCheckedRows().AsEnumerable().Where(r => Format.GetString(r["ISLOTCREATE"]) == "Y").CopyToDataTable();

                MessageWorker worker = new MessageWorker("SaveCreateLot");
                worker.SetBody(new MessageBody()
                {
                    { "TransactionType", "CancelCreateLot" },
                    { "ProductionOrderId", productionOrderId },
                    { "LineNo", lineNo },
                    { "State", "Create" },
                    { "ProductList", productList }
                });

                worker.Execute();

                // 정상적으로 처리되었습니다.
                ShowMessage("ProcessingSuccess");

                grdProductionOrderList.View.SetRowCellValue(grdProductionOrderList.View.FocusedRowHandle, "STATE", "Create");
                grdProductionOrderList.View.SetRowCellValue(grdProductionOrderList.View.FocusedRowHandle, "ISLOTCREATE", "N");

                string productDefId = row["PRODUCTDEFID"].ToString();

                FocusedRowChanged(grdProductionOrderList.View.FocusedRowHandle);
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
            DataTable lotList = grdLotList.DataSource as DataTable;

            DataTable itemList = grdProductList.DataSource as DataTable;

            foreach(DataRow dr in itemList.Rows)
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("PLANTID", UserInfo.Current.Plant);
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("P_PRODUCTDEFID", dr["PRODUCTDEFID"]);
                param.Add("P_PRODUCTDEFVERSION", dr["PRODUCTDEFVERSION"]);
                DataTable lineNoInfo = SqlExecuter.Query("GetRoutingOperationList", "10001", param);

                if (!Format.GetString(lineNoInfo.Rows[0]["USERSEQUENCE"]).Equals("10") && Format.GetString(lineNoInfo.Rows[0]["ASSEMBLYITEMCLASS"]).Equals("Product"))
                {
                    throw MessageException.Create("NoLotTen");

                }

            }

            
            if (lotList.AsEnumerable().Where(r => Format.GetString(r["ISLOTCREATE"]) == "N").Count() == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            lotList = lotList.AsEnumerable().Where(r => Format.GetString(r["ISLOTCREATE"]) == "N").CopyToDataTable();


            string isOEM = "";
            if (chkIsOEM.Checked)
                isOEM = "Y";
            else
                isOEM = "N";


            //if (lotList.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}

            DataRow row = grdProductionOrderList.View.GetDataRow(grdProductionOrderList.View.FocusedRowHandle);

            int lotInputPnlQty = 0;

            if (Format.GetInteger(row["LOTINPUTPNLQTY"]) > 0)
            {
                if (lotList.AsEnumerable().Where(r => Format.GetString(r["PRODUCTDEFID"]) == Format.GetString(row["PRODUCTDEFID"])).Count() > 0)
                    lotInputPnlQty = Format.GetInteger(numLotSize.EditValue);
                else
                    lotInputPnlQty = Format.GetInteger(row["LOTINPUTPNLQTY"]);
            }
            else
            {
                lotInputPnlQty = Format.GetInteger(numLotSize.EditValue);
            }

            MessageWorker worker = new MessageWorker("SaveCreateLot");
            worker.SetBody(new MessageBody()
            {
                { "TransactionType", "CreateLot" },
                { "PlantId", UserInfo.Current.Plant },
                { "ProductionOrderId", row["PRODUCTIONORDERID"].ToString() },
                { "LineNo", row["LINENO"].ToString() },
                { "ProductDefId", Format.GetString(row["PRODUCTDEFID"]) },
                { "ProductDefVersion", Format.GetString(row["PRODUCTDEFVERSION"]) },
                { "State", "LotCreate" },
                { "PureOrder", Format.GetInteger(numPureOrder.EditValue) },
                { "PureInput", Format.GetInteger(numPureInput.EditValue) },
                { "SurplusStock", Format.GetInteger(numSurplusStock.EditValue) },
                { "SurplusWip", Format.GetInteger(numSurplusWip.EditValue) },
                { "LackQty", Format.GetInteger(numLackQty.EditValue) },
                { "StandardInput", Format.GetInteger(numStandardInputPnl.EditValue) },
                { "LotPnl", Format.GetInteger(numLotSize.EditValue) },
                { "UserId", UserInfo.Current.Id },
                { "IsOem", isOEM },
                { "list", lotList }
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
            var values = Conditions.GetValues();
            values.Add("P_PLANTID", UserInfo.Current.Plant);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtProductionOrder = await QueryAsync("SelectProductionOrderList", "10001", values);

            int beforeFocusedRowHandle = grdProductionOrderList.View.FocusedRowHandle;

            grdProductionOrderList.DataSource = dtProductionOrder;

            // 검색 결과에 데이터가 없는 경우
            if (dtProductionOrder.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
                ClearProductionOrderInfo();
                numPureOrder.EditValue = 0;
                grdProductList.View.ClearDatas();
                grdLotList.View.ClearDatas();
            }
            else
            {
                // 검색 전 FocusedRowHandle과 검색 후 FocusedRowHandle이 같은 경우 내부적으로 FocusedRowChanged 이벤트가 호출되지 않음
                // FocusedRowChanged 이벤트 로직 강제 호출
                if (beforeFocusedRowHandle >= grdProductionOrderList.View.RowCount)
                {
                    grdProductionOrderList.View.FocusedRowHandle = 0;
                    FocusedRowChanged(0);
                }
                else
                {
                    if (beforeFocusedRowHandle == grdProductionOrderList.View.FocusedRowHandle)
                        FocusedRowChanged(grdProductionOrderList.View.FocusedRowHandle);
                }
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // S/O번호 검색조건 팝업 추가
            var conditionProductionOrderId = Conditions.AddSelectPopup("P_PRODUCTIONORDERID", new SqlQuery("GetProductionOrderIdList", "10001", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTIONORDERID", "PRODUCTIONORDERID")
                //.SetMultiGrid(true)
                .SetPopupLayout("SELECTPRODUCTIONORDER", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(0)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetLabel("PRODUCTIONORDERID")
                .SetPosition(1.2)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME");


            // 팝업에서 사용되는 검색조건 (P/O번호)
            conditionProductionOrderId.Conditions.AddTextBox("TXTPRODUCTIONORDERID");

            // 팝업 그리드에서 보여줄 컬럼 정의
            // Value
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("VALUEFIELD", 100)
                .SetValidationKeyColumn()
                .SetIsHidden();
            // S/O번호
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTIONORDERID", 80);
            // 라인
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("LINENO", 50);
            // 수주량
            conditionProductionOrderId.GridColumns.AddSpinEditColumn("PLANQTY", 70);
            // 품목코드
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120);
            // 품목명
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);

            // 팝업 값 선택 이벤트
            conditionProductionOrderId.SetPopupApplySelection(async (selectedRows, dataGridRows) =>
            {
                List<DataRow> selectedList = selectedRows.ToList();
                List<string> lineNoList = new List<string>();

                string poList = "";

                selectedList.ForEach(row =>
                {
                    string productionOrderId = Format.GetString(row["PRODUCTIONORDERID"]);
                    string lineNo = Format.GetString(row["LINENO"]);

                    poList += productionOrderId + ",";
                    lineNoList.Add(lineNo);
                });


                poList = poList.TrimEnd(',');

                lineNoList = lineNoList.Distinct().ToList();

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("PLANTID", UserInfo.Current.Plant);
                param.Add("P_PRODUCTIONORDERID", poList);

                DataTable lineNoInfo = SqlExecuter.Query("GetLineNoByProductionOrder", "10001", param);


                Conditions.GetControl<SmartComboBox>("P_LINENO").DataSource = lineNoInfo;

                if (lineNoList.Count == 1)
                    Conditions.GetControl<SmartComboBox>("P_LINENO").EditValue = Format.GetFullTrimString(lineNoList[0]);
                else
                    Conditions.GetControl<SmartComboBox>("P_LINENO").EditValue = "*";

                await OnSearchAsync();
            });


            // 품목 검색조건 팝업 추가
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 2.2, true, Conditions, "PRODUCTDEFNAME", "PRODUCTDEF");
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTIONORDERID").EditValueChanged += LotCreateSheet_EditValueChanged;
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
            if (!CheckStandardLoss())
            {
                // 기준로스는 0 보다 큰 값이어야 합니다.
                throw MessageException.Create("InValidStandardLoss");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 수주 리스트 그리드의 선택된 행이 변경된 경우 다시 계산하는 로직을 호출한다.
        /// </summary>
        /// <param name="focusedRowHandle">그리드에 선택된 행 Index</param>
        private void FocusedRowChanged(int focusedRowHandle)
        {
            chkBaseProduct.CheckState = CheckState.Unchecked;
            chkEtcProduct.CheckState = CheckState.Unchecked;

            if (focusedRowHandle < 0)
            {
                ClearProductionOrderInfo();
                grdProductList.View.ClearDatas();
                grdLotList.View.ClearDatas();
                return;
            }

            DataRow row = grdProductionOrderList.View.GetDataRow(focusedRowHandle);

            decimal planQty = Format.GetDecimal(row["PLANQTY"]);
            string rtrSht = row["RTRSHT"].ToString();
            string state = row["STATE"].ToString();

            numPureOrder.EditValue = planQty;

            //numStandardInputPnl

            //if (rtrSht == "RTR")
            //    numRollStandardInput.Visible = true;
            //else if (rtrSht == "SHT")
            //    numRollStandardInput.Visible = false;

            string productionOrderId = Format.GetString(row["PRODUCTIONORDERID"]);
            string lineNo = Format.GetString(row["LINENO"]);
            string productDefId = Format.GetString(row["PRODUCTDEFID"]);
            string productDefVersion = Format.GetString(row["PRODUCTDEFVERSION"]);

            //if (state == "Create")
            //{
            //    ClearProductionOrderInfo();
            //    grdLotList.View.ClearDatas();
            //}
            //else if (state == "LotCreate")
            //else
            //{
                //string productionOrderId = row["PRODUCTIONORDERID"].ToString();
                //string lineNo = row["LINENO"].ToString();

                //GetProductionOrderInfo(focusedRowHandle);
            GetLotList(productionOrderId, lineNo);

            if (grdLotList.View.RowCount > 0)
                GetProductionOrderInfo(focusedRowHandle);
            else
                ClearProductionOrderInfo();
            //}

            GetProductDefinitionList(productionOrderId, lineNo, productDefId, productDefVersion);

            //기준 투입 파넬 셋팅(영풍만 ㅡㅡ;)
            if (UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_YoungPoong))
            {
                decimal panelqty = Format.GetDecimal(row["PLANPNLQTY"]);
                decimal Calpanelqty = Format.GetDecimal(row["CALPNL"]);

                if (Calpanelqty.Equals(0))
                {
                    numStandardInputPnl.Editor.Value = panelqty;
                }
                else
                {
                    numStandardInputPnl.Editor.Value = Calpanelqty;
                }

            }
        }

        /// <summary>
        /// 수주 정보의 값(순수주, 순투입, 기준투입(PCS), 기준로스)을 다시 계산한다.
        /// </summary>
        /// <param name="focusedRowHandle">수주 리스트 그리드에 선택된 행 Index</param>
        private void CalcProductionOrderInfo(int focusedRowHandle)
        {
            int planQty = Format.GetInteger(grdProductionOrderList.View.GetRowCellValue(focusedRowHandle, "PLANQTY"));
            int pcsPnl = Format.GetInteger(grdProductionOrderList.View.GetRowCellValue(focusedRowHandle, "PCSPNL"));

            int surplusStock = Format.GetInteger(numSurplusStock.EditValue);
            int surplusWip = Format.GetInteger(numSurplusWip.EditValue);
            int lackQty = Format.GetInteger(numLackQty.EditValue);

            int standardInput = Format.GetInteger(numStandardInputPnl.EditValue);
            int lotPnl = Format.GetInteger(numLotSize.EditValue);

            //numPureOrder.EditValue = planQty;
            numPureInput.EditValue = planQty - (surplusStock + surplusWip) + lackQty;
            numStandardInputPCS.EditValue = standardInput * pcsPnl;

            double pureInput = Format.GetDouble(numPureInput.EditValue, 0);
            double standardInputPcs = Format.GetDouble(numStandardInputPCS.EditValue, 0);

            double standardLoss = ((standardInputPcs / pureInput) - 1) * 100;

            if (standardInput > 0 && lotPnl > 0)
            {
                numStandardLoss.EditValue = standardLoss;
            }
        }

        /// <summary>
        /// 수주 리스트 그리드에 정보가 없는 경우 상세 정보를 초기화 한다.
        /// </summary>
        private void ClearProductionOrderInfo()
        {
            tlpProductionOrderInfo.Controls.Find<SmartLabelSpinEdit>(true).ForEach(control =>
            {
                if (control.Name != "numPureOrder")
                {
                    control.Editor.EditValueChanged -= Editor_EditValueChanged;

                    control.EditValue = 0;

                    control.Editor.EditValueChanged += Editor_EditValueChanged;
                }
            });
        }

        /// <summary>
        /// 품목 리스트 그리드의 데이터를 조회 한다.
        /// </summary>
        /// <param name="productDefId">품목코드</param>
        private void GetProductDefinitionList(string productionOrderId, string lineNo, string productDefId, string productDefVersion)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("PLANTID", UserInfo.Current.Plant);
            values.Add("PRODUCTIONORDERID", productionOrderId);
            values.Add("LINENO", lineNo);
            values.Add("PRODUCTDEFID", productDefId);
            values.Add("PRODUCTDEFVERSION", productDefVersion);

            DataTable dtProductDefinition = new DataTable();

            if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                dtProductDefinition = SqlExecuter.Query("SelectProductDefinitionListByProductionOrder_YP", "10003", values);
            else
                dtProductDefinition = SqlExecuter.Query("SelectProductDefinitionListByProductionOrder", "10003", values);

            grdProductList.DataSource = dtProductDefinition;

            //grdLotList.View.ClearDatas();

            SetProductDefinitionListQtyLoss();
        }

        /// <summary>
        /// 품목 리스트 그리드의 수량, 실투입로스, 순수주로스 값을 계산 한다.
        /// </summary>
        private void SetProductDefinitionListQtyLoss()
        {
            if (grdProductionOrderList.View.FocusedRowHandle < 0)
                return;

            DataRow selectedRow = grdProductionOrderList.View.GetFocusedDataRow();

            int pcsPnl = Format.GetInteger(selectedRow["PCSPNL"]);

            DataTable dtProductDefinition = grdProductList.DataSource as DataTable;

            double pureOrder = Format.GetDouble(numPureOrder.EditValue, 0);
            double pureInput = Format.GetDouble(numPureInput.EditValue, 0);
            double standardInputPcs = Format.GetDouble(numStandardInputPCS.EditValue, 0);

            int standardInput = Format.GetInteger(numStandardInputPnl.EditValue);
            int lotSize = Format.GetInteger(numLotSize.EditValue);

            double pureInputLoss = 0;
            if (!pureInput.Equals(0))
                pureInputLoss = ((standardInputPcs / pureInput) - 1) * 100;

            double pureOrderLoss = 0;
            if (!pureOrder.Equals(0))
                pureOrderLoss = ((standardInputPcs / pureOrder) - 1) * 100;


            foreach (DataRow row in dtProductDefinition.Rows)
            {
                if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
                {
                    row["PNLQTY"] = standardInput;
                    row["QTY"] = standardInput * Format.GetInteger(row["JOINTQTY"]);

                    if (standardInput > 0)
                    {
                        row["ACTUALINPUTLOSS"] = pureInputLoss;
                        row["PUREORDERLOSS"] = pureOrderLoss;
                    }
                }
                else if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                {
                    string productDefType = Format.GetString(row["PRODUCTDEFTYPE"]);
                    int jointQty = Format.GetInteger(row["JOINTQTY"]);


                    if (jointQty <= 0)
                    {
                        // 해당 반제품의 배열수가 입력되지 않았습니다. Routing 화면에서 배열수를 입력해 주시기 바랍니다. Product Id : {0}, Product Version : {1}
                        ShowMessage("NotInputJointQty", Format.GetString(row["PRODUCTDEFID"]), Format.GetString(row["PRODUCTDEFVERSION"]));
                        return;
                    }

                    if (productDefType == "Product")    // 제품
                    {
                        row["PNLQTY"] = standardInput;
                        row["QTY"] = standardInputPcs;
                    }
                    else //반제품일때
                    {                                                                                                   //         총작업량/접합수*이건 공용인지  y이면 2곱학 아니면 1곱합 * 공용숫자?
                        row["PNLQTY"] = Math.Ceiling((decimal)standardInputPcs / (decimal)jointQty) * (Format.GetString(row["ISINNERPUBLIC"]) == "Y" ? 2 : 1) * Format.GetInteger(row["USEBOMCNT"]); //* Format.GetInteger(row["MULTIPLE"]);
                        row["QTY"] = Format.GetInteger(row["PNLQTY"]) * Format.GetInteger(row["JOINTQTY"]);
                    }


                    if (standardInput > 0)
                    {
                        if (pureInput > 0)
                            row["ACTUALINPUTLOSS"] = Math.Round((Format.GetDecimal(row["QTY"]) / (Format.GetDecimal(pureInput) * (Format.GetString(row["ISINNERPUBLIC"]) == "Y" ? 2 : 1) * Format.GetInteger(row["USEBOMCNT"])) - 1) * 100, 2);
                        if (pureOrder > 0)
                            row["PUREORDERLOSS"] = Math.Round((Format.GetDecimal(row["QTY"]) / (Format.GetDecimal(pureOrder) * (Format.GetString(row["ISINNERPUBLIC"]) == "Y" ? 2 : 1) * Format.GetInteger(row["USEBOMCNT"])) - 1) * 100, 2);
                    }
                }

                if (lotSize > 0)
                {
                    row["LOTSIZE"] = lotSize;
                }
            }

            dtProductDefinition.AcceptChanges();
        }

        /// <summary>
        /// 기준로스 값이 0 보다 큰지 확인 한다.
        /// </summary>
        /// <returns>0 이하인 경우 false 반환.</returns>
        private bool CheckStandardLoss()
        {
            if (grdProductionOrderList.View.FocusedRowHandle < 0)
                return true;

            double standardInput = Format.GetDouble(numStandardInputPnl.EditValue, 0);
            double pcsPnl = Format.GetDouble(grdProductionOrderList.View.GetRowCellValue(grdProductionOrderList.View.FocusedRowHandle, "PCSPNL"), 0);
            int lotPnl = Format.GetInteger(numLotSize.EditValue);

            double standardLoss = (((standardInput * pcsPnl) / Format.GetDouble(numPureInput.EditValue, 0)) - 1) * 100;

            if (standardInput > 0)
            {
                if (standardLoss <= 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 수주 정보의 상태가 Lot 생성 일 경우 수량 정보를 조회 한다.
        /// </summary>
        /// <param name="focusedRowHandle"></param>
        private void GetProductionOrderInfo(int focusedRowHandle)
        {
            DataRow row = grdProductionOrderList.View.GetDataRow(focusedRowHandle);

            numPureOrder.EditValue = Format.GetInteger(row["PUREORDER"]);
            numPureInput.EditValue = Format.GetInteger(row["PUREINPUT"]);
            numSurplusStock.EditValue = Format.GetInteger(row["SURPLUSSTOCK"]);
            numSurplusWip.EditValue = Format.GetInteger(row["SURPLUSWIP"]);
            numLackQty.EditValue = Format.GetInteger(row["UNDERAGE"]);
            numStandardInputPnl.EditValue = Format.GetInteger(row["STDINPUTPNLQTY"]);
            numLotSize.EditValue = Format.GetInteger(row["LOTINPUTPNLQTY"]);


            CalcProductionOrderInfo(focusedRowHandle);
        }

        /// <summary>
        /// 수주 정보의 상태가 Lot 생성 일 경우 생성된 Lot List를 조회 한다.
        /// </summary>
        /// <param name="productionOrderId"></param>
        /// <param name="lineNo"></param>
        private void GetLotList(string productionOrderId, string lineNo)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("PRODUCTIONORDERID", productionOrderId);
            param.Add("LINENO", lineNo);

            DataTable lotList = SqlExecuter.Query("SelectLotListByProductionOrder", "10001", param);

            grdLotList.DataSource = lotList;
        }

        private void SetProductCheckState(string type, bool state)
        {
            for (int i = 0; i < grdProductList.View.RowCount; i++)
            {
                string materialClass = Format.GetString(grdProductList.View.GetRowCellValue(i, "MATERIALCLASS"));

                switch (type)
                {
                    case "Base":
                        if (materialClass == "FG" || materialClass == "SB")
                            grdProductList.View.CheckRow(i, state);

                        break;
                    case "Etc":
                        if (materialClass != "FG"/* && materialClass != "SB"*/)
                            grdProductList.View.CheckRow(i, state);

                        break;
                }
            }
        }

        #endregion
    }
}