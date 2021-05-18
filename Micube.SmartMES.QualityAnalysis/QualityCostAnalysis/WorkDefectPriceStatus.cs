#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 품질 비용분석 > 재공불량 금액현황
    /// 업  무  설  명  : 재공 불량에 대해 단가를 조회한다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-09-03
    /// 수  정  이  력  :
    ///         2021.03.11 전우성 : 고객ID, 고객명 추가 및 화면 최적화
    ///
    /// </summary>
    public partial class WorkDefectPriceStatus : SmartConditionManualBaseForm
    {
        #region 생성자

        public WorkDefectPriceStatus()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();
        }

        /// <summary>
        /// 재공불량 금액현황 그리드
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMain.View.AddTextBoxColumn("OCCURDATE", 200).SetTextAlignment(TextAlignment.Center); // 발생일시
            grdMain.View.AddTextBoxColumn("PLANTID", 100).SetLabel("OCCURPLANT"); // 발생 Site
            grdMain.View.AddTextBoxColumn("CUSTOMERID", 100); // 고객ID
            grdMain.View.AddTextBoxColumn("CUSTOMERNAME", 150); // 고객명
            grdMain.View.AddTextBoxColumn("LOTTYPE", 100).SetTextAlignment(TextAlignment.Center); // 양산구분
            grdMain.View.AddTextBoxColumn("PRODUCTDEFID", 150); // 품목코드
            grdMain.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center); // 품목 Rev
            grdMain.View.AddTextBoxColumn("PRODUCTDEFNAME", 250); // 품목명
            grdMain.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center).SetLabel("Lot No"); // Lot No
            grdMain.View.AddTextBoxColumn("PARENTLOTID", 200).SetTextAlignment(TextAlignment.Center); // Parent Lot No
            grdMain.View.AddTextBoxColumn("REASONPLANTID", 100).SetLabel("REASONPLANT"); // 원인 Site
            grdMain.View.AddTextBoxColumn("REASONCONSUMABLEDEFNAME", 150).SetLabel("REASONCONSUMABLEDEFID"); // 원인품목
            grdMain.View.AddTextBoxColumn("REASONCONSUMABLELOTID", 200).SetTextAlignment(TextAlignment.Center); // 원인자재 Lot
            grdMain.View.AddTextBoxColumn("REASONSEGMENTNAME", 150).SetLabel("REASONSEGMENT"); // 원인공정
            grdMain.View.AddTextBoxColumn("REASONAREANAME", 150).SetLabel("REASONAREA"); // 원인작업장
            grdMain.View.AddTextBoxColumn("VENDORNAME", 150).SetLabel("REASONVENDOR"); // 원인업체
            grdMain.View.AddTextBoxColumn("DEFECTCODENAME", 180).SetLabel("DEFECTNAME"); // 불량명
            grdMain.View.AddSpinEditColumn("DEFECTQTY", 80); // 불량수량
            grdMain.View.AddSpinEditColumn("UNITPRICE", 120).SetLabel("ORDERUNITPRICE"); // 수주단가
            grdMain.View.AddSpinEditColumn("DEFECTAMOUNT2", 120); // 불량반영금액(판가)
            grdMain.View.AddSpinEditColumn("PCSAMOUNT", 120); // PCS단가
            grdMain.View.AddSpinEditColumn("DEFECTAMOUNT", 120); // 불량반영금액
            grdMain.View.AddSpinEditColumn("CURRENCY", 80).SetTextAlignment(TextAlignment.Center); // 통화
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 180).SetLabel("OCCURSEGMENT"); // 발견공정
            grdMain.View.AddTextBoxColumn("DISCOVERUSERNAME", 100).SetTextAlignment(TextAlignment.Center); // 발견자
            grdMain.View.AddTextBoxColumn("ISINBOUND", 80).SetTextAlignment(TextAlignment.Center); // 인수여부
            grdMain.View.AddTextBoxColumn("ISCONFIRMATION", 80).SetTextAlignment(TextAlignment.Center); // 확정여부
            grdMain.View.AddTextBoxColumn("ISCLOSE", 80).SetTextAlignment(TextAlignment.Center); // 마감여부
            grdMain.View.AddTextBoxColumn("ISCLAIM", 120).SetTextAlignment(TextAlignment.Center); // Claim 포함여부

            grdMain.View.PopulateColumns();

            grdMain.View.SetIsReadOnly();

            grdMain.View.OptionsView.ShowFooter = true;
            grdMain.ShowStatusBar = false;
            grdMain.View.OptionsNavigation.AutoMoveRowFocus = false;

            grdMain.View.Columns["DEFECTCODENAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMain.View.Columns["DEFECTCODENAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdMain.View.Columns["DEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMain.View.Columns["DEFECTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdMain.View.Columns["UNITPRICE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMain.View.Columns["UNITPRICE"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdMain.View.Columns["DEFECTAMOUNT2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMain.View.Columns["DEFECTAMOUNT2"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdMain.View.Columns["PCSAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMain.View.Columns["PCSAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdMain.View.Columns["DEFECTAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMain.View.Columns["DEFECTAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";
        }

        #endregion 컨텐츠 영역 초기화

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                grdMain.View.ClearDatas();

                var values = Conditions.GetValues();
                values.Add("p_languageType", UserInfo.Current.LanguageType);

                if (await SqlExecuter.QueryAsync("GetWorkDefectPriceStatus", "10001", values) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        this.ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }

                    grdMain.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            #region 품목

            var condition = Conditions.AddSelectPopup("p_productdefId", new SqlQuery("GetProductListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFID")
                                      .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, true)
                                      .SetPopupLayoutForm(400, 600)
                                      .SetLabel("PRODUCT")
                                      .SetPopupResultCount(1)
                                      .SetPosition(3.1);

            condition.Conditions.AddTextBox("PRODUCTDEFIDNAME");

            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);

            #endregion 품목

            // 원인 Site
            this.Conditions.AddComboBox("p_reasonPlantId", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PLANTNAME", "PLANTID")
                           .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                           .SetDefault(UserInfo.Current.Plant)
                           .SetLabel("REASONPLANT")
                           .SetEmptyItem()
                           .SetPosition(4.1);

            #region 원인 작업장

            condition = Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAreaListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
                                  .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(400, 600)
                                  .SetLabel("REASONAREA")
                                  .SetPopupResultCount(1)
                                  .SetPosition(5.1);

            condition.Conditions.AddTextBox("AREAIDNAME");

            condition.GridColumns.AddTextBoxColumn("AREAID", 150).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            #endregion 원인 작업장

            #region 원인업체

            condition = Conditions.AddSelectPopup("p_vendorId", new SqlQuery("GetVendorListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "VENDORNAME", "VENDORID")
                                  .SetPopupLayout("VENDOR", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(400, 600)
                                  .SetLabel("REASONVENDOR")
                                  .SetPopupResultCount(1)
                                  .SetPosition(5.2);

            condition.Conditions.AddTextBox("VENDORIDNAME");

            condition.GridColumns.AddTextBoxColumn("VENDORID", 150).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("VENDORNAME", 200);

            #endregion 원인업체

            #region 불량코드

            condition = Conditions.AddSelectPopup("p_defectCode", new SqlQuery("GetDefectCodeByProcess", "10001", "RESOURCETYPE=QCSegmentID", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DEFECTCODENAME", "DEFECTCODE")
                                  .SetPopupLayout("SELECTDEFECTCODE", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                                  .SetPopupResultCount(1)
                                  .SetPosition(5.3)
                                  .SetLabel("DEFECTCODE")
                                  .SetPopupAutoFillColumns("QCSEGMENTNAME");

            condition.Conditions.AddTextBox("DEFECTCODENAME");
            condition.Conditions.AddTextBox("QCSEGMENTNAME");
            condition.Conditions.AddTextBox("DEFECTCODEID");
            condition.Conditions.AddTextBox("QCSEGMENTID");

            condition.GridColumns.AddTextBoxColumn("DEFECTCODE", 100); // 불량코드
            condition.GridColumns.AddTextBoxColumn("DEFECTCODENAME", 200); // 불량코드명
            condition.GridColumns.AddTextBoxColumn("QCSEGMENTID", 100); // 품질공정ID
            condition.GridColumns.AddTextBoxColumn("QCSEGMENTNAME", 200); // 품질공정명

            #endregion 불량코드

            #region 발견공정

            condition = Conditions.AddSelectPopup("p_discoverySegmentId", new SqlQuery("GetSmallProcesssegmentListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                                  .SetPopupLayout("DISCOVERYSEGMENT", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(400, 600)
                                  .SetLabel("DISCOVERYSEGMENT")
                                  .SetPopupResultCount(1)
                                  .SetPosition(5.4);

            condition.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");

            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

            #endregion 발견공정

            // 통화
            this.Conditions.AddComboBox("p_currency", new SqlQuery("GetUomDefinitionListByOsp", "10001", $"UOMTYPE=Currency"), "UOMDEFNAME", "UOMDEFID")
                           .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                           .SetLabel("CURRENCY")
                           .SetEmptyItem()
                           .SetPosition(100);
        }

        #endregion 검색
    }
}