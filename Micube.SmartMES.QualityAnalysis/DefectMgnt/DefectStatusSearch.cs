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
    /// 프 로 그 램 명  : 품질관리 > 불량관리 > 불량품 현황조회
    /// 업  무  설  명  : 불량품의 처리상태를 조회한다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-07-26
    /// 수  정  이  력  :
    ///         2021.03.11 전우성 : 고객ID, 고객명 추가 및 화면 최적화
    ///
    /// </summary>
    public partial class DefectStatusSearch : SmartConditionManualBaseForm
    {
        #region 생성자

        public DefectStatusSearch()
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
        /// 불량품 현황조회 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMain.GridButtonItem = GridButtonItem.Export;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            var group = grdMain.View.AddGroupColumn("PRODUCTINFO"); // 제품정보
            group.AddTextBoxColumn("PROCESSDATE", 200).SetTextAlignment(TextAlignment.Center); // 발생일시
            group.AddTextBoxColumn("LOTTYPE", 80).SetTextAlignment(TextAlignment.Center); // 양산구분
            group.AddTextBoxColumn("CUSTOMERID", 100); // 고객ID
            group.AddTextBoxColumn("CUSTOMERNAME", 150); // 고객명
            group.AddTextBoxColumn("PRODUCTDEFID", 120); // 품목코드
            group.AddTextBoxColumn("PRODUCTDEFNAME", 260); // 품목명
            group.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center); // 품목 Version
            group.AddTextBoxColumn("PARENTLOTID", 200).SetTextAlignment(TextAlignment.Center).SetLabel("Lot No"); // Parent Lot No
            group.AddTextBoxColumn("DEFECTNAME", 120); // 불량명
            group.AddTextBoxColumn("QCSEGMENTNAME", 120); // 품질공정명
            group.AddSpinEditColumn("DEFECTPNLQTY", 80).SetLabel("PNL"); // 불량수량
            group.AddSpinEditColumn("DEFECTQTY", 80).SetLabel("PCS"); // 불량수량
            group.AddSpinEditColumn("AMOUNTS", 80); // 금액
            group.AddTextBoxColumn("USERSEQUENCE", 80).SetTextAlignment(TextAlignment.Right); // 발생 공정수순
            group.AddTextBoxColumn("PROCESSSEGMENTNAME", 150); // 발생 공정
            group.AddTextBoxColumn("AREANAME", 150); // 발생 작업장
            group.AddTextBoxColumn("DISCOVERYSITE", 80).SetLabel("SITE"); // 발생 Site
            group.AddTextBoxColumn("PROCESSUSER", 100).SetTextAlignment(TextAlignment.Center); // 처리자
            group.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center).SetLabel("DEFECTLOTID"); // Lot No
            group.AddSpinEditColumn("UNIT", 80).SetTextAlignment(TextAlignment.Center).SetLabel("UOM"); // UOM

            group = grdMain.View.AddGroupColumn("REASONSEGMENT"); // 원인공정
            group.AddTextBoxColumn("REASONCONSUMABLEDEFNAME", 220).SetLabel("REASONPRODUCT"); // 원인품목
            group.AddTextBoxColumn("REASONCONSUMABLELOTID", 200); // 원인자재 Lot
            group.AddTextBoxColumn("REASONSEGMENTNAME", 180).SetLabel("REASONSEGMENT"); // 원인공정
            group.AddTextBoxColumn("REASONAREANAME", 180).SetLabel("REASONAREA"); // 원인작업장
            group.AddTextBoxColumn("REASONSITE", 100).SetLabel("REASONPLANT"); // 원인 Site
            group.AddTextBoxColumn("VENDORNAME", 180); // 원인업체명(?)

            group = grdMain.View.AddGroupColumn("DEFECTPROCESSSTATUS"); // 불량품 진행현황
            group.AddTextBoxColumn("ISINBOUND", 80).SetTextAlignment(TextAlignment.Center); // 인수여부
            group.AddTextBoxColumn("RECEIVETIME", 200).SetTextAlignment(TextAlignment.Center); // 인수일시
            group.AddTextBoxColumn("ISCONFIRMATION", 80).SetTextAlignment(TextAlignment.Center); // 확정여부
            group.AddTextBoxColumn("DEFINETIME", 200).SetTextAlignment(TextAlignment.Center); // 확정일시
            group.AddTextBoxColumn("ISCLOSE", 80).SetTextAlignment(TextAlignment.Center).SetLabel("SCRAPSTATUS"); // 마감여부
            group.AddTextBoxColumn("CLOSETIME", 200).SetTextAlignment(TextAlignment.Center).SetLabel("SCRAPDATE"); // 마감일시

            grdMain.View.PopulateColumns();
            grdMain.View.SetIsReadOnly();

            grdMain.View.OptionsNavigation.AutoMoveRowFocus = false;
            grdMain.View.OptionsView.ShowFooter = true;
            grdMain.ShowStatusBar = false;

            grdMain.View.Columns["QCSEGMENTNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMain.View.Columns["QCSEGMENTNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdMain.View.Columns["DEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMain.View.Columns["DEFECTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdMain.View.Columns["DEFECTPNLQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMain.View.Columns["DEFECTPNLQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
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
                values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

                if (await SqlExecuter.QueryAsync("GetDefectStatus", "10001", values) is DataTable dt)
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

            #region 작업장

            var condition = Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAreaListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
                                      .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
                                      .SetPopupLayoutForm(500, 600)
                                      .SetLabel("AREA")
                                      .SetPopupResultCount(1)
                                      .SetPosition(1.2)
                                      .SetRelationIds("p_plantId");

            condition.Conditions.AddTextBox("AREAIDNAME").SetLabel("AREAIDNAME");

            condition.GridColumns.AddTextBoxColumn("AREAID", 150).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            #endregion 작업장

            #region 발견공정

            condition = Conditions.AddSelectPopup("p_discoverySegmentId", new SqlQuery("GetSmallProcesssegmentListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                                  .SetPopupLayout("DISCOVERYSEGMENT", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(500, 600)
                                  .SetLabel("DISCOVERYSEGMENT")
                                  .SetPopupResultCount(1)
                                  .SetPosition(1.3)
                                  .SetRelationIds("p_plantId", "p_areaId");

            condition.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");

            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

            #endregion 발견공정

            #region 품목

            condition = Conditions.AddSelectPopup("p_productdefId", new SqlQuery("GetProductListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFIDVERSION")
                                  .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(600, 800)
                                  .SetLabel("PRODUCT")
                                  .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                                  .SetPopupResultCount(1)
                                  .SetPosition(1.4);

            condition.Conditions.AddTextBox("PRODUCTDEFIDNAME");

            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 220);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFIDVERSION", 100).SetIsHidden();

            #endregion 품목

            #region 업체

            condition = Conditions.AddSelectPopup("p_vendorId", new SqlQuery("GetVendorList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "VENDORNAME", "VENDORID")
                                  .SetPopupLayout("VENDOR", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(500, 600)
                                  .SetLabel("VENDOR")
                                  .SetPopupResultCount(1)
                                  .SetPosition(3.1)
                                  .SetRelationIds("p_plantId");

            condition.Conditions.AddTextBox("VENDORIDNAME");

            condition.GridColumns.AddTextBoxColumn("VENDORID", 150).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("VENDORNAME", 200);

            #endregion 업체

            #region 불량코드

            condition = Conditions.AddSelectPopup("p_defectCode", new SqlQuery("GetDefectCodeByProcess", "10001", "RESOURCETYPE=QCSegmentID", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DEFECTCODENAME", "DEFECTCODE")
                                  .SetPopupLayout("SELECTDEFECTCODE", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                                  .SetPopupResultCount(1)
                                  .SetPosition(3.2)
                                  .SetLabel("DEFECTCODE")
                                  .SetPopupAutoFillColumns("QCSEGMENTNAME");

            condition.Conditions.AddTextBox("DEFECTCODENAME");
            condition.Conditions.AddTextBox("QCSEGMENTNAME");
            condition.Conditions.AddTextBox("DEFECTCODEID");
            condition.Conditions.AddTextBox("QCSEGMENTID");

            condition.GridColumns.AddTextBoxColumn("DEFECTCODE", 100);// 불량코드
            condition.GridColumns.AddTextBoxColumn("DEFECTCODENAME", 200); // 불량코드명
            condition.GridColumns.AddTextBoxColumn("QCSEGMENTID", 100); // 품질공정ID
            condition.GridColumns.AddTextBoxColumn("QCSEGMENTNAME", 200); // 품질공정명

            #endregion 불량코드

            #region 원인공정

            condition = Conditions.AddSelectPopup("p_reasonSegmentId", new SqlQuery("GetSmallProcesssegmentListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                                  .SetPopupLayout("REASONSEGMENT", PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupLayoutForm(500, 600)
                                  .SetLabel("REASONSEGMENT")
                                  .SetPopupResultCount(1)
                                  .SetPosition(3.3);

            condition.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");

            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

            #endregion 원인공정
        }

        #endregion 검색
    }
}