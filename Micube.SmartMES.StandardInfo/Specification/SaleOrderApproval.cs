#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

#endregion using

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 사양관리 > 수주사양결재
    /// 업  무  설  명  : ERP의 수주정보를 사양에서 확인하여 결제처리 하는 화면
    /// 생    성    자  : 정승원
    /// 생    성    일  : 2019-05-29
    /// 수  정  이  력  :
    ///
    ///
    /// </summary>
    public partial class SaleOrderApproval : SmartConditionManualBaseForm
    {
        #region 생성자

        public SaleOrderApproval()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 컨텐츠 영역 초기화 시작
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();
            InitializeEvent();
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.Import | GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            //사이트
            grdMain.View.AddTextBoxColumn("PLANTID", 50).SetIsReadOnly().SetLabel("SITE").SetTextAlignment(TextAlignment.Center);
            //생산구분
            grdMain.View.AddComboBoxColumn("PRODUCTIONTYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //작업구분
            grdMain.View.AddComboBoxColumn("JOBTYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=JobType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                        .SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //품목계정
            grdMain.View.AddComboBoxColumn("ITEMACCOUNT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ItemAccount", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //품목종류
            grdMain.View.AddComboBoxColumn("ITEMCLASS", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ItemClass2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //승인용
            grdMain.View.AddCheckBoxColumn("USAGEAPPROVAL", 45);
            //초도양산
            grdMain.View.AddCheckBoxColumn("FIRSTPRODUCTTYPE", 45);
            //사양변경
            grdMain.View.AddCheckBoxColumn("SPECCHANGEFLAG2", 45).SetLabel("ISRC");
            // 사양변경(공정)
            grdMain.View.AddCheckBoxColumn("SPECCHANGEPROCESSSEGMENT2", 45).SetLabel("SPECCHANGEPROCESS");
            //수주번호
            grdMain.View.AddTextBoxColumn("SALESORDERID", 100).SetIsReadOnly().SetTextAlignment(TextAlignment.Center).SetLabel("PRODUCTIONREQUESTNO");
            //라인
            grdMain.View.AddTextBoxColumn("LINENO", 50).SetIsReadOnly().SetIsHidden();
            //ERP수주번호
            grdMain.View.AddTextBoxColumn("ERPSALESORDERNO", 100).SetIsReadOnly().SetTextAlignment(TextAlignment.Center).SetLabel("SALESORDERID");
            grdMain.View.AddTextBoxColumn("SALESORDER").SetIsHidden();
            //수주일
            grdMain.View.AddTextBoxColumn("SALESORDERDATE", 80).SetDisplayFormat("yyyy-MM-dd").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            //품목
            grdMain.View.AddTextBoxColumn("ITEMID", 80).SetIsReadOnly();
            grdMain.View.AddComboBoxColumn("ITEMVERSION", 40, new SqlQuery("GetItemMasterVersion", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "ITEMVERSION", "ITEMVERSION").SetRelationIds("ITEMID");
            grdMain.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("PRODUCTVALUE").SetIsReadOnly().SetIsHidden();

            //승인여부
            grdMain.View.AddTextBoxColumn("ISAPPROVAL", 50).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //LOT 생성여부
            grdMain.View.AddTextBoxColumn("ISLOTCREATE", 50).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            // 제품타입
            grdMain.View.AddComboBoxColumn("PRODUCTTYPE", 70, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);

            // 층수
            grdMain.View.AddComboBoxColumn("LAYER", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))        // 층수'
                        .SetEmptyItem("", "")
                        .SetTextAlignment(TextAlignment.Center)
                        .SetIsReadOnly();

            //수주량
            grdMain.View.AddSpinEditColumn("ORDERQTY", 60).SetIsReadOnly().SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
            //납기요청일
            grdMain.View.AddTextBoxColumn("DELIVERYDATE", 80).SetDisplayFormat("yyyy-MM-dd").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //영업담당
            grdMain.View.AddTextBoxColumn("SALESCHARGE", 80).SetIsHidden().SetIsReadOnly();
            grdMain.View.AddTextBoxColumn("SALESCHARGENAME", 80).SetIsReadOnly();
            //사양담당
            grdMain.View.AddTextBoxColumn("SPECIFICTIONCHARGE", 80).SetIsReadOnly().SetIsHidden();
            grdMain.View.AddTextBoxColumn("SPECIFICATIONCHARGENAME", 80).SetIsReadOnly();
            // 수주처
            grdMain.View.AddTextBoxColumn("CONTRACTOR", 80).SetIsReadOnly();
            // 납품처
            grdMain.View.AddTextBoxColumn("SHIPTO", 80).SetIsReadOnly();
            // 매출처
            grdMain.View.AddTextBoxColumn("BILLTO", 80).SetIsReadOnly();
            // RTR/SHT
            grdMain.View.AddTextBoxColumn("RTRSHT", 50).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            // 비고
            grdMain.View.AddTextBoxColumn("COMMENT", 150).SetLabel("REMARK");
            // 내용
            grdMain.View.AddTextBoxColumn("DESCRIPTION", 150).SetLabel("COMMENTS");
            //단가
            grdMain.View.AddSpinEditColumn("UNITPRICE", 100).SetDisplayFormat("#,##0.###", MaskTypes.Numeric, true).SetIsReadOnly();
            //SMT단가
            grdMain.View.AddSpinEditColumn("SMTUNITPRICE", 100).SetDisplayFormat("#,##0.###", MaskTypes.Numeric, true).SetIsReadOnly();
            //단가단위
            grdMain.View.AddTextBoxColumn("CURRENCY", 50).SetLabel("CURRENCYUNIT");
            //유효상태, 생성자, 수정자...
            grdMain.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                        .SetDefault("Valid")
                        .SetTextAlignment(TextAlignment.Center);

            grdMain.View.AddTextBoxColumn("FIRSTAPPROVER", 80).SetIsReadOnly().SetLabel("CREATOR").SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("FIRSTAPPROVEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetLabel("CREATEDTIME").SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("APPROVALMODIFIER", 80).SetIsReadOnly().SetLabel("MODIFIER").SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("APPROVALMODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetLabel("MODIFIEDTIME").SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("CREATOR", 80).SetIsReadOnly().SetIsHidden().SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetIsHidden().SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("MODIFIER", 80).SetIsReadOnly().SetIsHidden().SetTextAlignment(TextAlignment.Center);
            grdMain.View.AddTextBoxColumn("MODIFIEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly().SetIsHidden().SetTextAlignment(TextAlignment.Center);
            //interface 실행
            grdMain.View.AddTextBoxColumn("INTERFACEACTION", 50).SetTextAlignment(TextAlignment.Center).SetIsReadOnly().SetTextAlignment(TextAlignment.Center).SetIsHidden();
            //interface 결과
            grdMain.View.AddTextBoxColumn("INTERFACERESULTS", 50).SetTextAlignment(TextAlignment.Center).SetIsReadOnly().SetTextAlignment(TextAlignment.Center).SetIsHidden();

            grdMain.View.PopulateColumns();
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            grdMain.View.ShowingEditor += (s, e) =>
            {
                if(grdMain.View.GetFocusedDataRow() is DataRow dr)
                {
                    string focusColumn = grdMain.View.FocusedColumn.FieldName;

                    if (dr["ISAPPROVAL"].Equals("Y"))
                    {
                        if (focusColumn.Equals("DESCRIPTION") || focusColumn.Equals("ITEMVERSION") || focusColumn.Equals("USAGEAPPROVAL")
                         || focusColumn.Equals("FIRSTPRODUCTTYPE") || focusColumn.Equals("SPECCHANGEFLAG"))
                        {
                            e.Cancel = true;
                        }
                    }
                }
            };

            grdMain.View.RowCellStyle += (s, e) =>
            {
                if(grdMain.View.GetFocusedDataRow() is DataRow dr)
                {
                    if (dr["ISAPPROVAL"].ToString().Equals("Y"))
                    {
                        e.Appearance.ForeColor = Color.Blue;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                }
            };

            Conditions.GetControls<SmartSelectPopupEdit>().ForEach(control =>
            {
                // 조회조건의 ID 항목을 backSpace로 모두 삭제시에 이름 삭제
                control.EditValueChanged += (s, e) =>
                {
                    if (Format.GetFullTrimString(control.EditValue).Equals(string.Empty))
                    {
                        switch (control.Name)
                        {
                            case "ITEMID":
                                Conditions.GetControl<SmartTextBox>("ITEMNAME").EditValue = string.Empty;
                                Conditions.GetControl<SmartTextBox>("ITEMVERSION").EditValue = string.Empty;
                                break;

                            default:
                                break;
                        }
                    }
                };
            });
        }

        #endregion Event

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable changed = grdMain.GetChangedRows();

            DataSet dschanged = new DataSet();
            changed.TableName = "saleorderapproval";
            dschanged.Tables.Add(changed);
            ExecuteRule("SaleOrderApproval", dschanged);
        }

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Approval"))
            {
                DataTable changed = grdMain.View.GetCheckedRows().Copy();

                if (changed != null && changed.Rows.Count > 0)
                {
                    List<string> valueList = new List<string>();
                    foreach (DataRow row in changed.Rows)
                    {
                        valueList.Add(row["ITEMID"] + "|" + row["ITEMVERSION"]);
                    }

                    Dictionary<string, object> param = new Dictionary<string, object>
                    {
                        { "ITEMID", string.Join(",", valueList) }
                    };

                    if (SqlExecuter.Query("GetCheckInvalidation", "10001", param) is DataTable dtProductionOrder)
                    {
                        if (dtProductionOrder.Rows[0]["INVALIDCHECK"].Equals("N"))
                        {
                            throw MessageException.Create("OverInvalidateDate");
                        }
                    }

                    if (changed.Select("ISAPPROVAL = 'Y'").Length != 0)
                    {
                        ShowMessage("AlreadyCARAcceptCompleted");
                        return;
                    }

                    changed.Columns.Add("_STATE_");
                    foreach (DataRow row in changed.Rows)
                    {
                        row["_STATE_"] = "modified";
                        row["ISAPPROVAL"] = "Y";
                    }

                    DataTable changed1 = changed.Copy();

                    foreach (DataRow row in changed1.Rows)
                    {
                        row["_STATE_"] = "added";
                    }

                    //작업구분 = '신규 또는 변경' 이면
                    //모델등록 & 승인에서 승인된 건만 결재가능
                    var temp = changed.AsEnumerable().Where(r => !string.IsNullOrWhiteSpace(Format.GetString(r["JOBTYPE"])) && !Format.GetString(r["JOBTYPE"]).Equals("Same"));

                    if (temp.Count() > 0)
                    {
                        DataTable dtNewOrChange = temp.CopyToDataTable();
                        List<string> orderList = dtNewOrChange.AsEnumerable().Select(r => Format.GetString(r["PRODUCTVALUE"])).Distinct().Cast<string>().ToList();
                        DataTable dtProduct = SqlExecuter.Query("GetApprovalProductList", "10001", new Dictionary<string, object>() { { "PRODUCTVALUE", string.Join(",", orderList) } });
                        bool approved = false;

                        foreach (DataRow each in dtNewOrChange.Rows)
                        {
                            foreach (DataRow eachGov in dtProduct.Rows)
                            {
                                if (each["ITEMID"].ToString().Equals(eachGov["PRODUCTDEFID"].ToString()) && each["ITEMVERSION"].ToString().Equals(eachGov["PRODUCTDEFVERSION"].ToString()))
                                {
                                    approved = true;
                                }
                            }

                            if (!approved)
                            {
                                //orderList = dtProduct.AsEnumerable().Select(r => Format.GetString(r["PRODUCT"])).Distinct().Cast<string>().ToList();
                                //작업구분이 '신규' 또는 '변경'인 수주사양 중 모델등록 & 승인에서 승인된 건만 결재 가능합니다. 결재 불가능 품목 = {0}
                                ShowMessage("NOTAPPROVALPRODUCT", string.Format("[{0} {1}]", each["ITEMID"].ToString(), each["ITEMVERSION"].ToString()));
                                return;
                            }
                        }
                    }

                    DataSet dschanged = new DataSet();
                    changed.TableName = "saleorderapproval";
                    changed1.TableName = "productionorder";
                    dschanged.Tables.Add(changed);
                    dschanged.Tables.Add(changed1);

                    ExecuteRule("SaleOrderApproval", dschanged); // todo : 확인중
                    ShowMessage("ApprovalComplete");

                    var values = Conditions.GetValues();
                    DataTable dtSaleOrderApproval = SqlExecuter.Query("SelectSaleOrderApprovalSearch", "10001", values);
                    grdMain.DataSource = dtSaleOrderApproval;
                }
            }
            else if (btn.Name.ToString().Equals("Cancel"))
            {
                DataTable changed = grdMain.View.GetCheckedRows().Copy();

                if (changed != null && changed.Rows.Count > 0)
                {
                    changed.Columns.Add("_STATE_");
                    foreach (DataRow row in changed.Rows)
                    {
                        if (row["ISAPPROVAL"].ToString() != "Y")
                        {
                            ShowMessage("ApprovalCountInfo");
                            return;
                        }

                        row["_STATE_"] = "modified";
                        row["ISAPPROVAL"] = "N";

                        Dictionary<string, object> ParamProductionOrder = new Dictionary<string, object>
                        {
                            { "ENTERPRISEID", row["ENTERPRISEID"] },
                            { "PRODUCTIONORDERID", row["SALESORDERID"] },
                            { "LINENO", row["LINENO"] }
                        };

                        if (SqlExecuter.Query("GetProductionOrderChk", "10001", ParamProductionOrder) is DataTable dtProductionOrder)
                        {
                            if (dtProductionOrder.Rows.Count != 0)
                            {
                                ShowMessage("ISLOTCREATE");
                                return;
                            }
                        }
                    }

                    DataTable changed1 = changed.Copy();

                    foreach (DataRow row in changed1.Rows)
                    {
                        row["_STATE_"] = "deleted";
                    }

                    DataSet dschanged = new DataSet();
                    changed.TableName = "saleorderapproval";
                    changed1.TableName = "productionorder";
                    dschanged.Tables.Add(changed);
                    dschanged.Tables.Add(changed1);

                    ExecuteRule("SaleOrderApproval", dschanged);
                    ShowMessage("NoApprovalComplete");

                    var values = Conditions.GetValues();
                    DataTable dtSaleOrderApproval = SqlExecuter.Query("SelectSaleOrderApprovalSearch", "10001", values);
                    grdMain.DataSource = dtSaleOrderApproval;
                }
            }
        }

        #endregion 툴바

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            grdMain.View.ClearDatas();

            if (await QueryAsync("SelectSaleOrderApprovalSearch", "10001", Conditions.GetValues()) is DataTable dt)
            {
                if (dt.Rows.Count.Equals(0))
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdMain.DataSource = dt;
            }
        }

        /// <summary>
        /// 조회조건 추가 구성
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            #region 고객사

            var condition = Conditions.AddSelectPopup("CUSTOMERID", new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
                                      .SetPopupLayout("CUSTOMERID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
                                      .SetPopupResultCount(1)   //팝업창 선택가능한 개수
                                      .SetPosition(3);

            condition.Conditions.AddTextBox("TXTCUSTOMERID");

            condition.GridColumns.AddTextBoxColumn("CUSTOMERID", 80);
            condition.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
            condition.GridColumns.AddTextBoxColumn("ADDRESS", 250);
            condition.GridColumns.AddTextBoxColumn("CEONAME", 100);
            condition.GridColumns.AddTextBoxColumn("TELNO", 100);
            condition.GridColumns.AddTextBoxColumn("FAXNO", 100);

            #endregion 고객사

            // 승인여부
            Conditions.AddComboBox("MEASURINGISAPPROVAL", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetEmptyItem()
                      .SetDefault("N")
                      .SetPosition(4);

            #region 품목코드

            condition = Conditions.AddSelectPopup("ITEMID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFID", "PRODUCTDEFID")
                                  .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                                  .SetPopupLayoutForm(800, 800)
                                  .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                                  .SetLabel("PRODUCTDEFID")
                                  .SetPosition(5)
                                  .SetPopupResultCount(0)
                                  .SetPopupApplySelection((selectRow, gridRow) =>
                                  {
                                      List<string> productDefnameList = new List<string>();
                                      List<string> productRevisionList = new List<string>();

                                      selectRow.AsEnumerable().ForEach(r =>
                                      {
                                          productDefnameList.Add(Format.GetString(r["PRODUCTDEFNAME"]));
                                          productRevisionList.Add(Format.GetString(r["PRODUCTDEFVERSION"]));
                                      });

                                      Conditions.GetControl<SmartTextBox>("ITEMNAME").EditValue = string.Join(",", productDefnameList);
                                      Conditions.GetControl<SmartTextBox>("ITEMVERSION").EditValue = string.Join(",", productRevisionList);
                                  });

            condition.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            condition.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                                .SetDefault("Product");

            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            condition.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            condition.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            condition.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");

            #endregion 품목코드

            // 버전
            Conditions.AddTextBox("ITEMVERSION").SetIsReadOnly().SetPosition(6);
            Conditions.AddTextBox("ITEMNAME").SetIsReadOnly().SetPosition(7);

            // 작업구분
            Conditions.AddComboBox("JOBTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=JobType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetEmptyItem().SetPosition(8);

            // 생산구분
            Conditions.AddComboBox("PRODUCTIONTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetEmptyItem().SetPosition(9);
        }

        #endregion 검색

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdMain.View.CheckValidation();

            DataTable changed = grdMain.GetChangedRows();

            if (changed.Rows.Count.Equals(0))
            {
                throw MessageException.Create("NoSaveData");
            }

            foreach (DataRow row in changed.Rows)
            {
                if (row["ISAPPROVAL"].ToString().Equals("Y"))
                {
                    throw MessageException.Create("ApprovedSave");
                }
            }
        }

        #endregion 유효성 검사
    }
}