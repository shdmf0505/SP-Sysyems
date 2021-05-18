#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 >자주/최종검사> 최종검사 결과 조회
    /// 업  무  설  명  :  최종검사 결과 조회
    /// 생    성    자  : 최별
    /// 생    성    일  : 2019-10-22
    /// 수  정  이  력  :
    ///
    ///
    /// </summary>
    public partial class FinalInspectionResultInquiry : SmartConditionManualBaseForm
    {
        #region 생성자

        public FinalInspectionResultInquiry()
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
            InitializeEvent();
        }

        /// <summary>
        ///  그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            InitializeGrid_Master();
            InitializeGrid_Detail();
            InitializeGrid_File();
        }

        /// <summary>
        /// MASTER 그리드
        /// </summary>
        private void InitializeGrid_Master()
        {
            grdMaster.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdMaster.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMaster.View.AddTextBoxColumn("TXNGROUPHISTKEY").SetIsHidden();
            grdMaster.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdMaster.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            grdMaster.View.AddTextBoxColumn("PROCESSSEGMENTVERSION").SetIsHidden();
            grdMaster.View.AddTextBoxColumn("AREAID").SetIsHidden();
            grdMaster.View.AddTextBoxColumn("DESTINATIONLOTID").SetIsHidden();

            var costprd = grdMaster.View.AddGroupColumn("QCMLRRPRDINFOR");
            costprd.AddTextBoxColumn("PRODUCTDEFID", 100);
            costprd.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            costprd.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            costprd.AddTextBoxColumn("PARENTLOTID", 200);
            costprd.AddTextBoxColumn("DEGREE", 100);
            costprd.AddTextBoxColumn("LOTID", 200);
            costprd.AddTextBoxColumn("WORKENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costprd.AddTextBoxColumn("WORKENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);

            var costprocess = grdMaster.View.AddGroupColumn("QCMFINALINSPECTINFOR");
            costprocess.AddTextBoxColumn("INSPECTIONDATE", 120).SetLabel("INSPECTIONPROCESSDATE").SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            costprocess.AddTextBoxColumn("PROCESSSEGMENTNAME", 100);
            costprocess.AddTextBoxColumn("AREANAME", 100);
            costprocess.AddComboBoxColumn("FINISHINSPECTIONTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=FinishInspectionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));  // 진행상태
            costprocess.AddTextBoxColumn("INSPECTIONUSER", 100).SetIsHidden();
            costprocess.AddTextBoxColumn("INSPECTORNAME", 100);

            var costInsp = grdMaster.View.AddGroupColumn("QCMFINALINSPECTQTY");
            costInsp.AddTextBoxColumn("INSPECTIONQTYPCS", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costInsp.AddTextBoxColumn("INSPECTIONQTYPNL", 100).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);

            var costSpec = grdMaster.View.AddGroupColumn("QCMFINALINSPECTSPECOUT");//검사수
            costSpec.AddTextBoxColumn("SPECOUTQTYPCS", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costSpec.AddTextBoxColumn("SPECOUTQTYPNL", 100).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);

            var costRATE = grdMaster.View.AddGroupColumn("");
            costRATE.AddTextBoxColumn("SPECOUTQTYRATE", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);

            var costIsncr = grdMaster.View.AddGroupColumn("");
            costIsncr.AddTextBoxColumn("ISNCRPUBLISH", 100);

            grdMaster.View.PopulateColumns();
            grdMaster.View.SetIsReadOnly();

            #region Summary

            grdMaster.View.Columns["WORKENDPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["WORKENDPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["WORKENDPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["WORKENDPANELQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["SPECOUTQTYPCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["SPECOUTQTYPCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["SPECOUTQTYPNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["SPECOUTQTYPNL"].SummaryItem.DisplayFormat = "{0:###,###}";

            #endregion Summary

            grdMaster.View.OptionsCustomization.AllowColumnMoving = false;
            grdMaster.View.OptionsView.ShowFooter = true;
            grdMaster.View.FooterPanelHeight = 10;
            grdMaster.ShowStatusBar = false;
        }

        /// <summary>
        /// DETAIL 그리드
        /// </summary>
        private void InitializeGrid_Detail()
        {
            grdDetail.GridButtonItem = GridButtonItem.Export; // | GridButtonItem.Expand | GridButtonItem.Restore;

            grdDetail.View.AddTextBoxColumn("PLANTID", 120).SetIsHidden();
            grdDetail.View.AddTextBoxColumn("DEGREE", 100).SetIsHidden();

            var costprocess1 = grdDetail.View.AddGroupColumn("");// 작업정보
            costprocess1.AddTextBoxColumn("DECISIONDEGREE", 100);

            var costprocess2 = grdDetail.View.AddGroupColumn("");// 작업정보
            costprocess2.AddTextBoxColumn("DEFECTCODENAME", 100);
            costprocess2.AddTextBoxColumn("QCSEGMENTNAME", 150);

            var costInsp = grdDetail.View.AddGroupColumn("QCMFINALINSPECTQTY");//검사수
            costInsp.AddTextBoxColumn("INSPECTIONQTYPCS", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costInsp.AddTextBoxColumn("INSPECTIONQTYPNL", 100).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);

            var costSpec = grdDetail.View.AddGroupColumn("QCMFINALINSPECTSPECOUT");//검사수
            costSpec.AddTextBoxColumn("SPECOUTQTYPCS", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costSpec.AddTextBoxColumn("SPECOUTQTYPNL", 100).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);

            var costRATE = grdDetail.View.AddGroupColumn("");
            costRATE.AddTextBoxColumn("SPECOUTQTYRATE", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric);

            var costRes = grdDetail.View.AddGroupColumn("QCMFINALINSPECTREASON"); //원인공정 .
            costRes.AddTextBoxColumn("REASONPRODUCTDEFNAME", 100);
            costRes.AddTextBoxColumn("REASONCONSUMABLELOTID", 100);
            costRes.AddTextBoxColumn("REASONSEGMENTNAME", 100);
            costRes.AddTextBoxColumn("REASONAREANAME", 100);

            grdDetail.View.PopulateColumns();

            grdDetail.View.SetIsReadOnly();
            grdDetail.ShowStatusBar = true;
        }

        /// <summary>
        /// FILE 그리드
        /// </summary>
        private void InitializeGrid_File()
        {
            grdFile.GridButtonItem = GridButtonItem.None;

            grdFile.View.AddTextBoxColumn("FILEPATH", 100).SetIsHidden();
            grdFile.View.AddTextBoxColumn("URL").SetIsHidden();

            grdFile.View.AddTextBoxColumn("FILENAME", 150);
            grdFile.View.AddTextBoxColumn("FILEEXT", 60).SetTextAlignment(TextAlignment.Center);
            grdFile.View.AddSpinEditColumn("FILESIZE", 60).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);

            grdFile.View.PopulateColumns();

            grdFile.View.SetAutoFillColumn("FILENAME");
            grdFile.View.SetIsReadOnly();
            grdFile.ShowStatusBar = false;
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 외부에서 호출시 자동 조회
        /// </summary>
        /// <param name="parameters"></param>
        public override void LoadForm(Dictionary<string, object> parameters)
        {
            base.LoadForm(parameters);

            if (parameters != null)
            {
                _parameters = parameters;

                string lotIdParam = parameters.ContainsKey("RESOURCEID") ? parameters["RESOURCEID"].ToString() : string.Empty;
                //string inspectionDateParam = parameters.ContainsKey("FINALINSPECTIONDATE") ? parameters["FINALINSPECTIONDATE"].ToString() : string.Empty;

                if (lotIdParam.Length > 0)
                {
                    Conditions.SetValue("P_LOTID", 0, lotIdParam);
                }

                OnSearchAsync(); // 조회
            }
        }

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // 메인 Grid 포커스 변경시 이벤트
            grdMaster.View.FocusedRowChanged += (s, e) =>
            {
                if (grdMaster.View.FocusedRowHandle < 0)
                {
                    return;
                }

                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "P_LOTID", grdMaster.View.GetFocusedRowCellValue("LOTID") },
                    { "P_DEGREE", grdMaster.View.GetFocusedRowCellValue("DEGREE") },
                    { "TXNGROUPHISTKEY", grdMaster.View.GetFocusedRowCellValue("TXNGROUPHISTKEY") },
                    { "P_DESTINATIONLOTID", grdMaster.View.GetFocusedRowCellValue("DESTINATIONLOTID") },
                    { "P_PROCESSSEGMENTID", grdMaster.View.GetFocusedRowCellValue("PROCESSSEGMENTID") },
                    { "P_PCS", grdMaster.View.GetFocusedRowCellValue("INSPECTIONQTYPCS") },
                    { "P_PNL", grdMaster.View.GetFocusedRowCellValue("INSPECTIONQTYPNL") },
                    { "P_LANGUAGETYPE", UserInfo.Current.LanguageType },
                };

                string version = "10001";

                switch (Format.GetString(grdMaster.View.GetFocusedRowCellValue("FINISHINSPECTIONTYPE"), string.Empty))
                {
                    case "7038":
                    case "7512":
                    case "7532":
                        version = "10002";
                        break;
                }

                grdDetail.DataSource = SqlExecuter.Query("GetFinalInspectionResultInquiryDetail", version, param);
            };

            // Detail 그리드의 포커스 행 변경 이벤트
            grdDetail.View.FocusedRowChanged += (s, e) =>
            {
                if (grdDetail.View.GetFocusedDataRow() is DataRow focusedRow)
                {
                    grdFile.DataSource = SqlExecuter.Query("SelectSelfInspMeasureImage", "10001", new Dictionary<string, object>
                                                        {
                                                            { "FILERESOURCETYPE", focusedRow["INSPECTIONDEFID"] },
                                                            { "FILERESOURCEID", focusedRow["FILERESOURCEID"] }
                                                        });
                }
            };

            //File 그리드의 포커스 행 변경 이벤트
            grdFile.View.FocusedRowChanged += (s, e) =>
            {
                picBox.Image = null;

                if (grdFile.View.GetFocusedDataRow() is DataRow focusedRow)
                {
                    string filePath = focusedRow["FILEPATH"].ToString();
                    string fileName = string.Join(".", focusedRow["FILENAME"].ToString(), focusedRow["FILEEXT"].ToString());
                    Bitmap image = CommonFunction.GetFtpImageFileToBitmap(filePath, fileName);
                    picBox.Image = image;
                }
            };
        }

        #endregion Event

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            #region 기간 검색형 전환 처리

            if (!(values["P_INSPECTIONDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_INSPECTIONDATE_PERIODFR"]);
                values.Remove("P_INSPECTIONDATE_PERIODFR");
                values.Add("P_INSPECTIONDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_INSPECTIONDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_INSPECTIONDATE_PERIODTO"]);
                values.Remove("P_INSPECTIONDATE_PERIODTO");
                requestDateTo = requestDateTo.AddDays(1);
                values.Add("P_INSPECTIONDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }

            #endregion 기간 검색형 전환 처리

            string version = "10001";

            switch (Format.GetString(values["P_FINISHINSPECTIONTYPE"], string.Empty))
            {
                case "7038":
                case "7512":
                case "7532":
                    version = "10002";
                    break;
            }

            grdMaster.View.ClearDatas();
            grdDetail.View.ClearDatas();
            picBox.Image = null;

            grdMaster.DataSource = await SqlExecuter.QueryAsync("GetFinalInspectionResultInquiryMaster", version, values);
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            #region 품목코드

            var condition = Conditions.AddSelectPopup("p_productdefId", new SqlQuery("GetProductListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFIDVERSION")
                                      .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, true)
                                      .SetPopupLayoutForm(600, 800)
                                      .SetLabel("PRODUCT")
                                      .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                                      .SetPopupResultCount(1)
                                      .SetPosition(2.2);

            condition.Conditions.AddTextBox("PRODUCTDEFIDNAME");
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120).SetValidationKeyColumn();
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 220);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            condition.GridColumns.AddTextBoxColumn("PRODUCTDEFIDVERSION", 100).SetIsHidden();

            #endregion 품목코드

            #region LOT, 모LOT

            Conditions.AddTextBox("P_PARENTLOTID").SetLabel("PARENTLOTID").SetPosition(2.5);
            Conditions.AddTextBox("P_LOTID").SetLabel("LOTID").SetPosition(2.7);

            #endregion LOT, 모LOT

            #region 공정

            Conditions.AddComboBox("P_PROCESSSEGMENTID", new SqlQuery("GetProsesssegmentByClassId", "10001",
                                                DefectMapHelper.AddLanguageTypeToConditions(new Dictionary<string, object>
                                                {
                                                    { "ENTERPRISEID", UserInfo.Current.Enterprise }
                                                })))
                      .SetLabel("PROCESSSEGMENT")
                      .SetRelationIds("P_FINISHINSPECTIONTYPE")
                      .SetEmptyItem()
                      .SetPosition(4);

            #endregion 공정

            #region 작업장

            condition = Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAuthorityUserUseArea", "10001",
                                                        DefectMapHelper.AddLanguageTypeToConditions(new Dictionary<string, object>
                                                        {
                                                            { "P_USERID", UserInfo.Current.Id }
                                                        })), "AREANAME", "AREAID")
                                  .SetPopupLayout(Language.Get("SELECTAREAID"), PopupButtonStyles.Ok_Cancel, true, true)
                                  .SetPopupResultCount(1)
                                  .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                  .SetPopupAutoFillColumns("AREANAME")
                                  .SetLabel("AREA")
                                  .SetPosition(5.0);

            condition.Conditions.AddTextBox("AREAIDNAME").SetLabel("AREAIDNAME");
            condition.GridColumns.AddTextBoxColumn("AREAID", 150);
            condition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            #endregion 작업장
        }

        #endregion 검색
    }
}