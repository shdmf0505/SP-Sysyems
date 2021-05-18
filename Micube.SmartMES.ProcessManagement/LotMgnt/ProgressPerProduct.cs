#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using Micube.SmartMES.Commons.Controls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using System.Linq;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 재공관리 > 품목별 진행현황
    /// 업  무  설  명  : 품목별 진행현황 조회
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2020-02-21
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ProgressPerProduct : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |
        private int summaryRowHandle;
        private string summaryFieldName;
        DXMenuItem _myContextMenu1, _myContextMenu2;
        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public ProgressPerProduct()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Form_Load(object sender, EventArgs e)
        {
            InitializeComboBox();
            InitializeGrid();
        }
        #endregion

        #region ◆ 컨텐츠 영역 초기화 |
        /// <summary>
        /// Control 설정
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
        }

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 2.5, true, Conditions, "PRODUCTDEF", "PRODUCTDEF", false);
            //Conditions.GetCondition("P_PRODUCTDEFID").SetValidationIsRequired();  // 2021-02-03 오근영 수정 담당자 추가에 따라 품목코드 필수 입력 해제 요청(우영민)
            // 2020-12-30 오근영 (22) 담당자 작업장 추가
            InitializeGrid_AreaListPopup();
            InitializeGrid_OwnerIdListPopup();
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
            Conditions.GetControl<SmartComboBox>("P_HOLD").EditValue = "N";
            Conditions.GetControl<SmartComboBox>("P_SUMMARYTYPE").EditValue = "LOT";
            Conditions.GetControl<SmartComboBox>("P_WORKTYPE").EditValue = "Normal";
            // 2020-12-30 오근영 (22) 시간조회 조회조건 시분초 추가
            SmartPeriodEdit period = Conditions.GetControl<SmartPeriodEdit>("P_PERIOD");
            period.datePeriodFr.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            period.datePeriodTo.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
        }

        /// <summary>
        /// 팝업형 컬럼 초기화 - 담당자
        /// </summary>
        private void InitializeGrid_OwnerIdListPopup()
        {
            Dictionary<string, object> dicParam;
            dicParam = new Dictionary<string, object> {
                { "ENTERPRISEID", UserInfo.Current.Enterprise }
                , { "PLANTID", UserInfo.Current.Plant }
            };
            var parentCodeClassPopupColumn = Conditions.AddSelectPopup("OWNERNAME", new SqlQuery("GetUserList", "10001", dicParam), "USERNAME", "USERID")
                .SetPopupLayout("SELECTOWNERID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPosition(12)
                .SetPopupAutoFillColumns("USERNAME")
                .SetPopupResultMapping("OWNERNAME", "USERNAME")
            ;
            parentCodeClassPopupColumn.Conditions.AddTextBox("USERIDNAME").SetLabel("MANAGERIDNAME");

            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("USERID", 150).SetLabel("MANAGERID");
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("USERNAME", 200).SetLabel("MANAGER");
        }

        /// <summary>
        /// 창고 컬럼 팝업
        /// </summary>
        private void InitializeGrid_AreaListPopup()
        {
            Dictionary<string, object> dicParam;
            dicParam = new Dictionary<string, object> {
                { "ENTERPRISEID", UserInfo.Current.Enterprise }
                , { "PLANTID", UserInfo.Current.Plant }
                , { "LANGUAGETYPE", UserInfo.Current.LanguageType }
            };
            var parentAreaPopupColumn = Conditions.AddSelectPopup("P_AREAID", new SqlQuery("GetAreaList", "10006", dicParam), "AREANAME", "AREAID")
                .SetPopupLayout("SELECTWAREHOUSEID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetPosition(13)
                .SetLabel("AREANAME")
            ;
            parentAreaPopupColumn.Conditions.AddTextBox("TXTAREA");

            parentAreaPopupColumn.GridColumns.AddTextBoxColumn("AREAID", 120);
            parentAreaPopupColumn.GridColumns.AddTextBoxColumn("AREANAME", 200);
            parentAreaPopupColumn.GridColumns.AddTextBoxColumn("OWNTYPE", 80).SetTextAlignment(TextAlignment.Center);
            parentAreaPopupColumn.GridColumns.AddTextBoxColumn("WAREHOUSEID", 90);
            parentAreaPopupColumn.GridColumns.AddTextBoxColumn("WAREHOUSENAME", 110);
            parentAreaPopupColumn.GridColumns.AddTextBoxColumn("VENDORID", 90);
            parentAreaPopupColumn.GridColumns.AddTextBoxColumn("VENDORNAME", 110);

        }

        #endregion

        #region ▶ ComboBox 초기화 |
        /// <summary>
        /// 콤보박스를 초기화 하는 함수
        /// </summary>
        private void InitializeComboBox()
        {

        }
        #endregion

        #region ▶ Grid 설정 |
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdSummary.GridButtonItem = GridButtonItem.Export;

            #region - 재공 Grid 설정 |
            // 2020-12-14 오근영 (21) 엑셀 다운로드 추가
            //grdWip.GridButtonItem = GridButtonItem.None;
            grdWip.GridButtonItem = GridButtonItem.Export;

            grdWip.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdWip.View.UseBandHeaderOnly = true;
            grdWip.ShowStatusBar = true;
            grdWip.View.SetIsReadOnly();

            grdWip.View.AddTextBoxColumn("PRODUCTDEFNAME", 220).SetTextAlignment(TextAlignment.Center);
            grdWip.View.AddTextBoxColumn("PRODUCTDEFID", 130).SetTextAlignment(TextAlignment.Center);
            grdWip.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            grdWip.View.AddTextBoxColumn("PROCESSCLASSID_R", 80).SetTextAlignment(TextAlignment.Center).SetLabel("TXNNAME");
            grdWip.View.AddTextBoxColumn("LOTID", 220).SetTextAlignment(TextAlignment.Center);
            grdWip.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 180);
            grdWip.View.AddTextBoxColumn("PROCESSUOM", 90).SetTextAlignment(TextAlignment.Center);

            // 2020-12-14 오근영 (19) Step 상태 추가
            //grdWip.View.AddTextBoxColumn("processstate",      90).SetTextAlignment(TextAlignment.Center); // Step 상태
            //grdWip.View.AddTextBoxColumn("codeid", 90).SetTextAlignment(TextAlignment.Center); // Code
            //grdWip.View.AddTextBoxColumn("codename", 90).SetTextAlignment(TextAlignment.Center); // Code명
            grdWip.View.AddTextBoxColumn("processstate", 90).SetTextAlignment(TextAlignment.Center);
            // 2021-01-06 오근영 (19) 수량 추가 및 하단 상태별 수량 주석처리
            grdWip.View.AddTextBoxColumn("processstateqty", 100).SetLabel("QTY");
            //var grpWipQty = grdWip.View.AddGroupColumn("QTY");
            //// 2020-12-14 오근영 (21) 엑셀 다운로드 추가
            ////grpWipQty.AddTextBoxColumn("SENDPCSQTY", 100).SetDisplayFormat("#,###.#").SetLabel("WAITFORRECEIVE");
            ////grpWipQty.AddTextBoxColumn("RECEIVEPCSQTY", 100).SetDisplayFormat("#,###.#").SetLabel("ACCEPT");
            ////grpWipQty.AddTextBoxColumn("SELFINSPECTIONQTY", 100).SetDisplayFormat("#,###.#").SetLabel("SELFINSPECTIONSTAKE").SetIsHidden();
            ////grpWipQty.AddTextBoxColumn("WORKSTARTPCSQTY", 100).SetDisplayFormat("#,###.#").SetLabel("WIPSTARTQTY");
            ////grpWipQty.AddTextBoxColumn("SHIPINSPECTIONQTY", 100).SetDisplayFormat("#,###.#").SetLabel("SEG0570").SetIsHidden();
            ////grpWipQty.AddTextBoxColumn("WORKENDPCSQTY", 100).SetDisplayFormat("#,###.#").SetLabel("WIPENDQTY");
            //grpWipQty.AddSpinEditColumn("SENDPCSQTY", 100).SetDisplayFormat("#,###.#").SetLabel("WAITFORRECEIVE");
            //grpWipQty.AddSpinEditColumn("RECEIVEPCSQTY", 100).SetDisplayFormat("#,###.#").SetLabel("ACCEPT");
            //grpWipQty.AddSpinEditColumn("SELFINSPECTIONQTY", 100).SetDisplayFormat("#,###.#").SetLabel("SELFINSPECTIONSTAKE").SetIsHidden();
            //grpWipQty.AddSpinEditColumn("WORKSTARTPCSQTY", 100).SetDisplayFormat("#,###.#").SetLabel("WIPSTARTQTY");
            //grpWipQty.AddSpinEditColumn("SHIPINSPECTIONQTY", 100).SetDisplayFormat("#,###.#").SetLabel("SEG0570").SetIsHidden();
            //grpWipQty.AddSpinEditColumn("WORKENDPCSQTY", 100).SetDisplayFormat("#,###.#").SetLabel("WIPENDQTY");

            grdWip.View.AddTextBoxColumn("AREANAME", 180);
            grdWip.View.AddTextBoxColumn("STAYINGHOUR", 100).SetLabel("DELAYTIME");

            var grpWipTime = grdWip.View.AddGroupColumn("TIME");
            grpWipTime.AddTextBoxColumn("SENDTIME", 140).SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center).SetLabel("WAITFORRECEIVE");
            grpWipTime.AddTextBoxColumn("RECEIVETIME", 140).SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center).SetLabel("ACCEPT");
            grpWipTime.AddTextBoxColumn("SELFINSPECTIONDATE", 140).SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center).SetLabel("SELFINSPECTIONSTAKE").SetIsHidden();
            grpWipTime.AddTextBoxColumn("WORKSTARTTIME", 140).SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center).SetLabel("WIPSTARTQTY");
            grpWipTime.AddTextBoxColumn("SHIPINSPECTIONDATE", 140).SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center).SetLabel("SEG0570").SetIsHidden();
            grpWipTime.AddTextBoxColumn("WORKENDTIME", 140).SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center).SetLabel("WIPENDQTY");

            grdWip.View.PopulateColumns();
            #endregion

            #region - 실적 Grid 설정 |
            // 2020-12-14 오근영 (21) 엑셀 다운로드 추가
            //grdResult.GridButtonItem = GridButtonItem.None;
            grdResult.GridButtonItem = GridButtonItem.Export;

            grdResult.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdResult.View.UseBandHeaderOnly = true;
            grdResult.ShowStatusBar = true;
            grdResult.View.SetIsReadOnly();

            grdResult.View.AddTextBoxColumn("PRODUCTDEFNAME", 220).SetTextAlignment(TextAlignment.Center);
            grdResult.View.AddTextBoxColumn("PRODUCTDEFID", 130).SetTextAlignment(TextAlignment.Center);
            grdResult.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            grdResult.View.AddTextBoxColumn("PROCESSCLASSID_R", 80).SetTextAlignment(TextAlignment.Center).SetLabel("TXNNAME");
            grdResult.View.AddTextBoxColumn("LOTID", 220).SetTextAlignment(TextAlignment.Center);
            grdResult.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 180);
            grdResult.View.AddTextBoxColumn("PROCESSUOM", 90).SetTextAlignment(TextAlignment.Center);

            var grpResultQty = grdResult.View.AddGroupColumn("FIGURE");
            // 2020-12-14 오근영 (21) 엑셀 다운로드 추가
            //grpResultQty.AddTextBoxColumn("RESULT_PNLQTY", 100).SetDisplayFormat("#,###").SetLabel("PNL");
            //grpResultQty.AddTextBoxColumn("RESULT_PCSQTY", 100).SetDisplayFormat("#,###").SetLabel("PCS");
            //grpResultQty.AddTextBoxColumn("RESULT_MMQTY", 100).SetDisplayFormat("#,###.0").SetLabel("MM");
            grpResultQty.AddSpinEditColumn("RESULT_PNLQTY", 100).SetDisplayFormat("#,###").SetLabel("PNL");
            grpResultQty.AddSpinEditColumn("RESULT_PCSQTY", 100).SetDisplayFormat("#,###").SetLabel("PCS");
            grpResultQty.AddSpinEditColumn("RESULT_MMQTY", 100).SetDisplayFormat("#,###.0").SetLabel("MM");

            var grpResultDefectQty = grdResult.View.AddGroupColumn("DEFECT");
            // 2020-12-14 오근영 (21) 엑셀 다운로드 추가
            //grpResultDefectQty.AddTextBoxColumn("DEFECT_PNLQTY", 100).SetDisplayFormat("#,###").SetLabel("PNL");
            //grpResultDefectQty.AddTextBoxColumn("DEFECT_PCSQTY", 100).SetDisplayFormat("#,###").SetLabel("PCS");
            //grpResultDefectQty.AddTextBoxColumn("DEFECT_MMQTY", 100).SetDisplayFormat("#,###.0").SetLabel("MM");
            grpResultDefectQty.AddSpinEditColumn("DEFECT_PNLQTY", 100).SetDisplayFormat("#,###").SetLabel("PNL");
            grpResultDefectQty.AddSpinEditColumn("DEFECT_PCSQTY", 100).SetDisplayFormat("#,###").SetLabel("PCS");
            grpResultDefectQty.AddSpinEditColumn("DEFECT_MMQTY", 100).SetDisplayFormat("#,###.0").SetLabel("MM");

            grdResult.View.AddTextBoxColumn("AREANAME", 180);
            grdResult.View.AddTextBoxColumn("STAYINGHOUR", 100).SetLabel("DELAYTIME");

            var grpResultTime = grdResult.View.AddGroupColumn("TIME");
            grpResultTime.AddTextBoxColumn("RECEIVETIME", 140).SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center).SetLabel("ACCEPT");
            grpResultTime.AddTextBoxColumn("SELFINSPECTIONDATE", 140).SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center).SetLabel("SELFINSPECTIONSTAKE").SetIsHidden();
            grpResultTime.AddTextBoxColumn("WORKSTARTTIME", 140).SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center).SetLabel("WIPSTARTQTY");
            grpResultTime.AddTextBoxColumn("SHIPINSPECTIONDATE", 140).SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center).SetLabel("SEG0570").SetIsHidden();
            grpResultTime.AddTextBoxColumn("WORKENDTIME", 140).SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center).SetLabel("WIPENDQTY");
            grpResultTime.AddTextBoxColumn("SENDTIME", 140).SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center).SetLabel("WIPSENDQTY2");

            grdResult.View.PopulateColumns();
            #endregion
        }
        #endregion

        #endregion

        #region ◆ Event |

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            this.Load += Form_Load;
            this.Shown += ProgressPerProduct_Shown;

            grdWip.InitContextMenuEvent += GrdWip_InitContextMenuEvent;
            grdResult.InitContextMenuEvent += GrdResult_InitContextMenuEvent;

            grdSummary.View.CustomRowFilter += View_CustomRowFilter;
            grdSummary.View.RowCellStyle += View_RowCellStyle;
            grdSummary.View.CellMerge += View_CellMerge;
            grdSummary.View.RowCellClick += View_RowCellClick;
        }

        private void GrdWip_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
        {
            args.AddMenus.Add(_myContextMenu1 = new DXMenuItem(Language.Get("MENU_PG-SG-0340"), OpenFormFromWip) { BeginGroup = true, Tag = "PG-SG-0340" });
        }

        private void GrdResult_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
        {
            args.AddMenus.Add(_myContextMenu2 = new DXMenuItem(Language.Get("MENU_PG-SG-0340"), OpenFormFromResult) { BeginGroup = true, Tag = "PG-SG-0340" });
        }

        private void ProgressPerProduct_Shown(object sender, EventArgs e)
        {
            smartSpliterContainer1.SplitterPosition = this.Height / 2;
        }

        private void View_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            GridView view = sender as GridView;

            if (view == null)
            {
                return;
            }

            if (!IsNumeric(e.Column.FieldName))
            {
                return;
            }

            string productSegments = GetProductSegments(e.Column.FieldName
            , Format.GetString(view.GetRowCellValue(e.RowHandle, "PRODUCTDEFID"))
            , Format.GetString(view.GetRowCellValue(e.RowHandle, "PRODUCTDEFVERSION")));

            if (string.IsNullOrEmpty(productSegments))
            {
                return;
            }

            var values = Conditions.GetValues();

            this.ShowWaitArea();
            try
            {
                if (view.GetRowCellValue(e.RowHandle, "TYPE").ToString() == "2")        // 재공
                {
                    tabControl.SelectedTabPageIndex = 0;

                    this.summaryRowHandle = e.RowHandle;
                    this.summaryFieldName = e.Column.FieldName;

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("P_PLANTID", values["P_PLANTID"]);
                    param.Add("P_HOLD", values["P_HOLD"]);
                    param.Add("P_PRODUCTSEGMENT", productSegments);
                    param.Add("P_SUMMARYTYPE", values["P_SUMMARYTYPE"]);
                    param.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
                    param.Add("P_WORKTYPE", values["P_WORKTYPE"]);
                    param.Add("P_USERID", UserInfo.Current.Id);
                    param.Add("OWNERNAME", values["OWNERNAME"]);
                    param.Add("P_PRODUCTDEFID", values["P_PRODUCTDEFID"]);

                    grdWip.DataSource = SqlExecuter.Query("SelectProgressPerProductWipList2", "10002", param);
                    grdSummary.View.LayoutChanged();
                }
                else if (view.GetRowCellValue(e.RowHandle, "TYPE").ToString() == "3")   // 실적
                {
                    tabControl.SelectedTabPageIndex = 1;

                    this.summaryRowHandle = e.RowHandle;
                    this.summaryFieldName = e.Column.FieldName;

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("P_PLANTID", values["P_PLANTID"]);
                    param.Add("P_PERIOD_PERIODFR", values["P_PERIOD_PERIODFR"]);
                    param.Add("P_PERIOD_PERIODTO", values["P_PERIOD_PERIODTO"]);
                    param.Add("P_PRODUCTSEGMENT", productSegments);
                    param.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
                    param.Add("P_USERID", UserInfo.Current.Id);
                    param.Add("OWNERNAME", values["OWNERNAME"]);
                    param.Add("P_PRODUCTDEFID", values["P_PRODUCTDEFID"]);

                    grdResult.DataSource = SqlExecuter.Query("SelectProgressPerProductResultList2", "10002", param);
                    grdSummary.View.LayoutChanged();
                }
            }
            finally
            {
                this.CloseWaitArea();
            }
        }

        private string GetProductSegments(string fieldName, string productDefId, string productDefVersion)
        {
            DataTable dataSource = grdSummary.DataSource as DataTable;
            if (dataSource == null)
            {
                return null;
            }

            string processSegmentId = null;
            List<string> productSegments = new List<string>();
            for (int i = dataSource.Rows.Count - 1; i >= 0; i--)
            {
                DataRow each = dataSource.Rows[i];
                if (each["TYPE"].ToString() == "4") // 공정ID 가 있는 행
                {
                    if (!string.IsNullOrEmpty(productDefVersion)
                        && (productDefId != Format.GetString(each["PRODUCTDEFID"])
                            || productDefVersion != Format.GetString(each["PRODUCTDEFVERSION"])))   // 전체조회가 아닌경우 지정한 품목 외에는 스킵
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(productDefVersion) && each[fieldName].ToString() == "")  // 공정이 없는 행은 제외
                    {
                        continue;
                    }
                    if (each["PRODUCTDEFVERSION"] == DBNull.Value)  // 합계 행
                    {
                        if (string.IsNullOrEmpty(productDefVersion))
                        {
                            processSegmentId = each[fieldName].ToString();
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    string productSegment;
                    if (fieldName != "0")
                    {
                        if (!string.IsNullOrEmpty(productDefVersion))
                        {
                            productSegment = each["PRODUCTDEFID"].ToString() + "|" + each["PRODUCTDEFVERSION"].ToString()
                                + "|" + each[fieldName].ToString();
                        }
                        else
                        {
                            productSegment = each["PRODUCTDEFID"].ToString() + "|" + each["PRODUCTDEFVERSION"].ToString()
                                + "|" + processSegmentId;
                        }
                    }
                    else
                    {
                        productSegment = each["PRODUCTDEFID"].ToString() + "|" + each["PRODUCTDEFVERSION"].ToString()
                            + "|" + "*";
                    }
                    productSegments.Add(productSegment);
                }
            }
            return string.Join(",", productSegments);
        }

        private void View_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            GridView view = sender as GridView;

            if (view == null)
            {
                return;
            }

            DataView dataSource = view.DataSource as DataView;
            if (dataSource == null)
            {
                return;
            }

            if (dataSource[e.ListSourceRow]["TYPE"].ToString() == "4" || dataSource[e.ListSourceRow]["TYPE"].ToString() == "5")
            {
                e.Visible = false;
                e.Handled = true;
            }
        }

        private void View_CellMerge(object sender, CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (view == null)
            {
                return;
            }

            if (e.Column.FieldName == "PRODUCTDEFID")
            {
                string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);

                e.Merge = (str1 == str2);
            }
            else if (e.Column.FieldName == "PRODUCTDEFVERSION")
            {
                string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);

                string id1 = view.GetRowCellValue(e.RowHandle1, "PRODUCTDEFID").ToString();
                string id2 = view.GetRowCellValue(e.RowHandle2, "PRODUCTDEFID").ToString();

                e.Merge = (str1 == str2 && id1 == id2);
            }
            else if (e.Column.FieldName == "PRODUCTDEFNAME" || e.Column.FieldName == "PCSPNL" || e.Column.FieldName == "INVENTORY")
            {
                string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);

                string id1 = view.GetRowCellValue(e.RowHandle1, "PRODUCTDEFID").ToString();
                string id2 = view.GetRowCellValue(e.RowHandle2, "PRODUCTDEFID").ToString();

                string ver1 = view.GetRowCellValue(e.RowHandle1, "PRODUCTDEFVERSION").ToString();
                string ver2 = view.GetRowCellValue(e.RowHandle2, "PRODUCTDEFVERSION").ToString();

                e.Merge = (str1 == str2 && id1 == id2 && ver1 == ver2);
            }
            else if (e.Column.FieldName == "LAYERNAME")
            {
                // 2021-01-07 오근영 (20) 제품Type, 층수 그리드 데이타 컬럼 머지
                string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);

                //e.Merge = (str1 == str2);
                string id1 = view.GetRowCellValue(e.RowHandle1, "PRODUCTDEFID").ToString();
                string id2 = view.GetRowCellValue(e.RowHandle2, "PRODUCTDEFID").ToString();

                string ver1 = view.GetRowCellValue(e.RowHandle1, "PRODUCTDEFVERSION").ToString();
                string ver2 = view.GetRowCellValue(e.RowHandle2, "PRODUCTDEFVERSION").ToString();

                e.Merge = (str1 == str2 && id1 == id2 && ver1 == ver2);

            }
            else
            {
                e.Merge = false;
            }
            e.Handled = true;
        }

        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            SmartBandedGridView view = sender as SmartBandedGridView;
            if (view == null)
            {
                return;
            }
            // 재공, 실적은 우측정렬
            if (view.GetDataRow(e.RowHandle)["TYPE"].ToString() != "1"
                && IsNumeric(e.Column.FieldName))
            {
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            }
            // 공정은 바탕색 노랑
            if (view.GetDataRow(e.RowHandle)["TYPE"].ToString() == "1"
                && IsNumeric(e.Column.FieldName))
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
            // 2020/03/05 유태근 추가 -- 실적은 글자색 빨강
            if (view.GetDataRow(e.RowHandle)["TYPE"].ToString() == "3"
                && IsNumeric(e.Column.FieldName))
            {
                e.Appearance.ForeColor = Color.Red;
            }
            // 합계 바탕색상 변경
            if (view.GetDataRow(e.RowHandle)["PRODUCTDEFVERSION"].ToString() == "" && IsNumeric(e.Column.FieldName)
                && view.GetDataRow(e.RowHandle)["TYPE"].ToString() == "1")
            {
                e.Appearance.BackColor = Color.LightCyan;
            }
            // 첫번째 공정의 SITE와 다른 SITE인 공정은 색상 변경
            if ((view.GetDataRow(e.RowHandle)["TYPE"].ToString() == "2" || view.GetDataRow(e.RowHandle)["TYPE"].ToString() == "3")
                && IsNumeric(e.Column.FieldName))
            {
                DataTable dataSource = view.GridDataSource as DataTable;
                if (dataSource == null)
                {
                    return;
                }

                int index = view.GetDataSourceRowIndex(e.RowHandle);
                int plantIndex = -1;
                int segmentIndex = -1;
                if (view.GetDataRow(e.RowHandle)["TYPE"].ToString() == "2")
                {
                    plantIndex = index + 3;
                    segmentIndex = index + 2;
                }
                else if (view.GetDataRow(e.RowHandle)["TYPE"].ToString() == "3")
                {
                    plantIndex = index + 2;
                    segmentIndex = index + 1;
                }
                string firstPlantId = dataSource.Rows[plantIndex]["1"].ToString();
                string currentPlantId = dataSource.Rows[plantIndex][e.Column.FieldName].ToString();
                string processSegmentId = dataSource.Rows[segmentIndex][e.Column.FieldName].ToString();

                if (view.GetDataRow(e.RowHandle)["PRODUCTDEFVERSION"].ToString() != ""
                    && string.IsNullOrEmpty(processSegmentId))
                {
                    e.Appearance.BackColor = Color.WhiteSmoke;
                }
                else if (firstPlantId != currentPlantId)
                {
                    e.Appearance.BackColor = Color.MistyRose;
                }
            }
            if (e.RowHandle == this.summaryRowHandle && e.Column.FieldName == this.summaryFieldName)
            {
                e.Appearance.BackColor = Color.DarkBlue;
                e.Appearance.ForeColor = Color.White;
            }
        }

        private bool IsNumeric(string str)
        {
            return int.TryParse(str, out int val);
        }
        #endregion

        #region ◆ 툴바 |
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
        }

        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {

            //2021 - 02 - 04 오근영 조회조건 둘중 하나가 필수 입력
            // 담당자 OWNERNAME
            // 품목코드 P_PRODUCTDEFID
            if (Conditions.GetControl<SmartSelectPopupEdit>("OWNERNAME").GetValue().Equals(string.Empty) &&
                Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").GetValue().Equals(string.Empty))
            {
                //InValidRequiredField {0} 항목은 필수입력 항목입니다.
                this.ShowMessage("InValidRequiredField2n1", new string[2] { Language.Get("ITEMCODE"), Language.Get("OWNERNAME") });
            }
            else
            {
                await base.OnSearchAsync();

                var values = Conditions.GetValues();
                values.Remove("P_PERIOD");
                values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("P_USERID", UserInfo.Current.Id);

                // 기존 Grid Data 초기화
                this.grdSummary.DataSource = null;
                this.grdWip.DataSource = null;
                this.grdResult.DataSource = null;

                this.summaryRowHandle = 0;
                this.summaryFieldName = null;

                DataTable dtResult = this.Procedure("usp_wip_selectprogressperproduct", values);

                if (dtResult.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }
                GrdWipCreateColumns(dtResult);
                grdSummary.DataSource = dtResult;
            }
        }

        private void GrdWipCreateColumns(DataTable table)
        {
            grdSummary.View.ClearColumns();
            grdSummary.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdSummary.View.SetIsReadOnly();
            grdSummary.View.OptionsView.AllowCellMerge = true;
            grdSummary.GridButtonItem = GridButtonItem.Export;

            grdSummary.View.AddTextBoxColumn("PRODUCTDEFID", 130);
            grdSummary.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            grdSummary.View.AddTextBoxColumn("PRODUCTDEFNAME", 220);
            grdSummary.View.AddTextBoxColumn("INVENTORY", 90).SetTextAlignment(TextAlignment.Right).SetIsHidden();
            grdSummary.View.AddTextBoxColumn("PCSPNL", 90).SetTextAlignment(TextAlignment.Right);

            // 2020-12-29 오근영 (20) 층수 추가
            grdSummary.View.AddTextBoxColumn("LAYERNAME", 90).SetTextAlignment(TextAlignment.Center);

            grdSummary.View.AddTextBoxColumn("TYPENAME", 60).SetTextAlignment(TextAlignment.Center);
            foreach (DataColumn each in table.Columns)
            {
                if (each.ColumnName != "PRODUCTDEFID"
                    && each.ColumnName != "PRODUCTDEFVERSION"
                    && each.ColumnName != "PRODUCTDEFNAME"
                    //&& each.ColumnName != "INVENTORY"
                    && each.ColumnName != "PCSPNL"
                    && each.ColumnName != "LAYERNAME"
                    && each.ColumnName != "TYPENAME"
                    && each.ColumnName != "TYPE")
                {
                    if (each.ColumnName != "0")
                    {
                        grdSummary.View.AddTextBoxColumn(each.ColumnName, 100);
                    }
                    else
                    {
                        grdSummary.View.AddTextBoxColumn(each.ColumnName, 100).SetLabel(" ");
                    }
                }
            }
            grdSummary.View.PopulateColumns();
            grdSummary.View.FixColumn(new string[] { "PRODUCTDEFID", "PRODUCTDEFVERSION", "PRODUCTDEFNAME"/*, "INVENTORY"*/, "PCSPNL", "LAYERNAME", "TYPENAME", "0" });
        }

        #endregion

        #region ◆ 유효성 검사 |

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #endregion

        #region ◆ Private Function |

        private void OpenFormFromWip(object sender, EventArgs args)
        {
            if (grdWip.View.FocusedRowHandle < 0)
                return;

            try
            {
                DialogManager.ShowWaitDialog();

                DataRow currentRow = grdWip.View.GetFocusedDataRow();

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

        private void OpenFormFromResult(object sender, EventArgs args)
        {
            if (grdResult.View.FocusedRowHandle < 0)
                return;

            try
            {
                DialogManager.ShowWaitDialog();

                DataRow currentRow = grdResult.View.GetFocusedDataRow();

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
    }
}
