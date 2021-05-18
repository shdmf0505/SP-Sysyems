#region using

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
using DevExpress.XtraGrid;
using DevExpress.Data;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 >출하검사 > 출하검사 LRR 실적
    /// 업  무  설  명  : 출하검사 LRR 실적
    /// 생    성    자  : 최별
    /// 생    성    일  : 2019-10-22
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ShipmentInspLRRList : SmartConditionManualBaseForm
    {
        #region Local Variables

      

        #endregion

        #region 생성자

        public ShipmentInspLRRList()
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

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();

            InitializeEvent();
        }

        /// <summary>        
        ///  그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            InitializeGrid_Master();
            //InitializeGrid_Detail();
        }
        private void InitializeGrid_Master()
        {
            grdMaster.GridButtonItem = GridButtonItem.Export;

            grdMaster.View.SetIsReadOnly();
            grdMaster.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();
            grdMaster.View.AddTextBoxColumn("RECEIVETIME", 120)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss"); // 인수일시
            grdMaster.View.AddTextBoxColumn("WORKSTARTTIME", 120)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss"); // 작업시작시간
            grdMaster.View.AddTextBoxColumn("WORKENDTIME", 120)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss"); // 작업완료시간
            grdMaster.View.AddTextBoxColumn("SENDTIME", 120)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetLabel("TAKEOVERDATE"); // 인계일시
            grdMaster.View.AddTextBoxColumn("CUSTOMERNAME", 100); // 고객명
            grdMaster.View.AddTextBoxColumn("PRODUCTDEFNAME", 200); // 품목명
            grdMaster.View.AddTextBoxColumn("PRODUCTDEFID", 100); // 품목코드
            grdMaster.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80); // 내부 Rev
            grdMaster.View.AddTextBoxColumn("LOTID", 180); // Lot No
            grdMaster.View.AddTextBoxColumn("INSPECTIONRESULT", 100); // 판정결과
            grdMaster.View.AddTextBoxColumn("NGCOUNT", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric); // NG 횟수
            grdMaster.View.AddTextBoxColumn("RECEIVEPCSQTYQCM", 80)
                .SetLabel("TOTALQTY")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric); // 총수량
            grdMaster.View.AddTextBoxColumn("GOODQTY", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric); // 양품수량
            grdMaster.View.AddTextBoxColumn("INSPECTIONQTY", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric); // 검사수량
            grdMaster.View.AddTextBoxColumn("SPECOUTPCSQTY", 100)
                .SetLabel("DEFECTQTYPCS")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric); // 불량수(PCS)
            grdMaster.View.AddTextBoxColumn("SPECOUTPERCENTAGE", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric); // 불량율(%)
            grdMaster.View.AddTextBoxColumn("DEFECTNAME", 100); // 불량명
            grdMaster.View.AddTextBoxColumn("SHIPINSPECTORNAME", 100); // 검사자
            grdMaster.View.AddTextBoxColumn("FINISHAREANAME", 150); // 최종검사 작업장
            grdMaster.View.AddTextBoxColumn("PREVPROCESSAREA", 150);
            grdMaster.View.AddTextBoxColumn("MESSAGE", 200); // 전달사항

            grdMaster.View.AddComboBoxColumn("PRODUCTIONTYPE", 100, new SqlQuery("GetShipProducttionTypeCodeList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("LOTPRODUCTTYPE"); // 양산구분

            grdMaster.View.AddTextBoxColumn("PREVPROCESSSEGMENT", 150); // 전공정
            grdMaster.View.AddTextBoxColumn("WEEK", 100); // 주차
            grdMaster.View.AddTextBoxColumn("AFIAREANAME", 100);
            grdMaster.View.AddTextBoxColumn("FINISHPROCESSSEGNAME", 150);
            grdMaster.View.AddTextBoxColumn("DEGREE", 80);
            grdMaster.View.AddTextBoxColumn("FINISHINSPECTORNAME", 100); // 최종검사자

            // Detail 컬럼추가
            grdMaster.View.AddTextBoxColumn("DECISIONDEGREE", 100);
            grdMaster.View.AddTextBoxColumn("QCSEGMENTNAME", 150);

            grdMaster.View.AddTextBoxColumn("SPECINSPECTIONPNLQTY", 80)
                .SetLabel("INSPECTQTYPNL")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdMaster.View.AddTextBoxColumn("SPECINSPECTIONPCSQTY", 100)
                .SetLabel("INSPECTQTYPCS")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            //var costSpec = grdMaster.View.AddGroupColumn("QCMFINALINSPECTSPECOUT");//불량수
            grdMaster.View.AddTextBoxColumn("SPECOUTPNLQTY", 80)
                .SetLabel("DEFECTQTYPNL")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            //원인공정 .
            //var costRes = grdMaster.View.AddGroupColumn("QCMFINALINSPECTREASON"); //원인공정
            grdMaster.View.AddTextBoxColumn("REASONPRODUCTDEFNAME", 150);
            grdMaster.View.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 150);
            grdMaster.View.AddTextBoxColumn("REASONCONSUMABLELOTID", 150);
            grdMaster.View.AddTextBoxColumn("REASONSEGMENTNAME", 150);
            grdMaster.View.AddTextBoxColumn("REASONAREANAME", 150);

            grdMaster.View.PopulateColumns();
            SetFooterSummary();

            //grdMaster.View.OptionsCustomization.AllowColumnMoving = false;
            grdMaster.View.OptionsView.AllowCellMerge = true; // CellMerge
            grdMaster.View.OptionsView.ShowFooter = true;
            grdMaster.View.FooterPanelHeight = 10;
            grdMaster.ShowStatusBar = false;

            // 2020.04.07-Merge된 cell 합계 구학기
            int sum = 0;
            grdMaster.View.CustomSummaryCalculate += (sender, e) => {
                GridView view = sender as GridView;
                if (e.IsTotalSummary)
                {
                    GridSummaryItem item = e.Item as GridSummaryItem;
                    if (item.FieldName == "RECEIVEPCSQTYQCM" || item.FieldName == "GOODQTY" || item.FieldName == "INSPECTIONQTY")
                    {
                        switch (e.SummaryProcess)
                        {
                            case CustomSummaryProcess.Start:
                                sum = 0;
                                break;
                            case CustomSummaryProcess.Calculate:
                                //bool shouldSum = (bool)view.GetRowCellValue(e.RowHandle, "MARK");

                                if (view.GetRowCellValue(e.RowHandle, "MARK").Equals("true"))
                                {
                                    sum += e.FieldValue.ToSafeInt32();
                                }
                                break;
                            case CustomSummaryProcess.Finalize:
                                e.TotalValue = sum;
                                break;
                        }
                    }
                }
            };
        }
        /*
        private void InitializeGrid_Detail()
        {
            grdDetail.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdDetail.View.SetIsReadOnly();

            var costprocess = grdDetail.View.AddGroupColumn("");// 작업정보 
            costprocess.AddTextBoxColumn("DECISIONDEGREE", 100);
            costprocess.AddTextBoxColumn("DEFECTNAME", 100);
            costprocess.AddTextBoxColumn("QCSEGMENTNAME", 150);

            var costInsp = grdDetail.View.AddGroupColumn("QCMFINALINSPECTQTY");//검사수
            costInsp.AddTextBoxColumn("INSPECTIONPNLQTY", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costInsp.AddTextBoxColumn("INSPECTIONPCSQTY", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costSpec = grdDetail.View.AddGroupColumn("QCMFINALINSPECTSPECOUT");//불량수
            costSpec.AddTextBoxColumn("SPECOUTPNLQTY", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costSpec.AddTextBoxColumn("SPECOUTPCSQTY", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            var costRATE = grdDetail.View.AddGroupColumn("");
            costRATE.AddTextBoxColumn("SPECOUTPERCENTAGE", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            //원인공정 .
            var costRes = grdDetail.View.AddGroupColumn("QCMFINALINSPECTREASON"); //원인공정 .
            costRes.AddTextBoxColumn("REASONPRODUCTDEFNAME", 100);
            costRes.AddTextBoxColumn("REASONCONSUMABLELOTID", 100);
            costRes.AddTextBoxColumn("REASONSEGMENTNAME", 100);
            costRes.AddTextBoxColumn("REASONAREANAME", 100);

            grdDetail.View.PopulateColumns();
        }
        */

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            //grdMaster.View.FocusedRowChanged += View_FocusedRowChanged;
            grdMaster.View.CellMerge += View_CellMerge;
            grdMaster.View.DoubleClick += View_DoubleClick;
        }

        /// <summary>
        /// lot 검사결과 detail조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            DataRow focusedRow = grdMaster.View.GetFocusedDataRow();

            DialogManager.ShowWaitArea(pnlContent);
            ShipmentInspHistoryPopup detailPopup = new ShipmentInspHistoryPopup();
            detailPopup.CurrentDataRow = focusedRow;
            detailPopup._currentDt = (grdMaster.DataSource as DataTable).Clone();
            detailPopup.StartPosition = FormStartPosition.CenterParent;
            detailPopup.FormBorderStyle = FormBorderStyle.Sizable;
            DialogManager.CloseWaitArea(pnlContent);

            detailPopup.ShowDialog();
        }

        /// <summary>
        /// 사용자 지정 Cell Merge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (view == null) return;

            if (e.Column.FieldName == "RECEIVETIME" || e.Column.FieldName == "WORKSTARTTIME"
                || e.Column.FieldName == "WORKENDTIME" || e.Column.FieldName == "PRODUCTIONTYPE"
                || e.Column.FieldName == "AFIAREANAME" || e.Column.FieldName == "FINISHPROCESSSEGNAME"
                || e.Column.FieldName == "FINISHAREANAME" || e.Column.FieldName == "CUSTOMERNAME"
                || e.Column.FieldName == "PRODUCTDEFID" || e.Column.FieldName == "PRODUCTDEFNAME"
                || e.Column.FieldName == "PRODUCTDEFVERSION" || e.Column.FieldName == "LOTID"
                || e.Column.FieldName == "DEGREE" || e.Column.FieldName == "INSPECTIONRESULT"
                || e.Column.FieldName == "RECEIVEPCSQTYQCM" || e.Column.FieldName == "INSPECTIONQTY"
                || e.Column.FieldName == "NGCOUNT" || e.Column.FieldName == "SHIPINSPECTORNAME"
                || e.Column.FieldName == "FINISHINSPECTORNAME" || e.Column.FieldName == "PREVPROCESSSEGMENT"
                || e.Column.FieldName == "PREVPROCESSAREA" || e.Column.FieldName == "WEEK"
                || e.Column.FieldName == "MESSAGE" || e.Column.FieldName == "GOODQTY"
                || e.Column.FieldName == "SENDTIME")
            {
                var dr1 = grdMaster.View.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = grdMaster.View.GetDataRow(e.RowHandle2); //아래 행 정보

                //비교하는 이유 그래야 정상적으로 나옴.
                e.Merge = dr1["RECEIVETIME"].ToString().Equals(dr2["RECEIVETIME"].ToString()) && dr1["WORKSTARTTIME"].ToString().Equals(dr2["WORKSTARTTIME"].ToString())
                             && dr1["WORKENDTIME"].ToString().Equals(dr2["WORKENDTIME"].ToString()) && dr1["PRODUCTIONTYPE"].ToString().Equals(dr2["PRODUCTIONTYPE"].ToString())
                             && dr1["AFIAREANAME"].ToString().Equals(dr2["AFIAREANAME"].ToString()) && dr1["FINISHPROCESSSEGNAME"].ToString().Equals(dr2["FINISHPROCESSSEGNAME"].ToString())
                             && dr1["FINISHAREANAME"].ToString().Equals(dr2["FINISHAREANAME"].ToString()) && dr1["CUSTOMERNAME"].ToString().Equals(dr2["CUSTOMERNAME"].ToString())
                             && dr1["PRODUCTDEFID"].ToString().Equals(dr2["PRODUCTDEFID"].ToString()) && dr1["PRODUCTDEFNAME"].ToString().Equals(dr2["PRODUCTDEFNAME"].ToString())
                             && dr1["PRODUCTDEFVERSION"].ToString().Equals(dr2["PRODUCTDEFVERSION"].ToString()) && dr1["LOTID"].ToString().Equals(dr2["LOTID"].ToString())
                             && dr1["DEGREE"].ToString().Equals(dr2["DEGREE"].ToString()) && dr1["INSPECTIONRESULT"].ToString().Equals(dr2["INSPECTIONRESULT"].ToString())
                             && dr1["RECEIVEPCSQTYQCM"].ToString().Equals(dr2["RECEIVEPCSQTYQCM"].ToString()) && dr1["INSPECTIONQTY"].ToString().Equals(dr2["INSPECTIONQTY"].ToString())
                             && dr1["NGCOUNT"].ToString().Equals(dr2["NGCOUNT"].ToString()) && dr1["SHIPINSPECTORNAME"].ToString().Equals(dr2["SHIPINSPECTORNAME"].ToString())
                             && dr1["FINISHINSPECTORNAME"].ToString().Equals(dr2["FINISHINSPECTORNAME"].ToString()) && dr1["PREVPROCESSSEGMENT"].ToString().Equals(dr2["PREVPROCESSSEGMENT"].ToString())
                             && dr1["PREVPROCESSAREA"].ToString().Equals(dr2["PREVPROCESSAREA"].ToString()) && dr1["MESSAGE"].ToString().Equals(dr2["MESSAGE"].ToString())
                             && dr1["GOODQTY"].ToString().Equals(dr2["GOODQTY"].ToString()) && dr1["SENDTIME"].ToString().Equals(dr2["SENDTIME"].ToString());

                /*
                string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);
                e.Merge = (str1 == str2);
                */
            }
            else
            {
                e.Merge = false;
            }

            e.Handled = true;
        }

        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 그리드의 포커스 행 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focusedRowChanged();
        }
        */
        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

           
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

            values.Add("P_LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            #region 기간 검색형 전환 처리 
            // 2021.01.20 전우성 인수기간 조회조건 삭제
            //if (!(values["P_RECEIVETIME_PERIODFR"].ToString().Equals("")))
            //{
            //    DateTime requestDateFr = Convert.ToDateTime(values["P_RECEIVETIME_PERIODFR"]);
            //    values.Remove("P_RECEIVETIME_PERIODFR");
            //    values.Add("P_RECEIVETIME_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            //}
            //if (!(values["P_RECEIVETIME_PERIODTO"].ToString().Equals("")))
            //{
            //    DateTime requestDateTo = Convert.ToDateTime(values["P_RECEIVETIME_PERIODTO"]);
            //    values.Remove("P_RECEIVETIME_PERIODTO");
            //    requestDateTo = requestDateTo.AddDays(1);
            //    values.Add("P_RECEIVETIME_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            //}

            if (!(values["P_SENDTIME_PERIODFR"].ToString().Equals("")))
            {
                DateTime actualDateFr = Convert.ToDateTime(values["P_SENDTIME_PERIODFR"]);
                values.Remove("P_SENDTIME_PERIODFR");
                values.Add("P_SENDTIME_PERIODFR", string.Format("{0:yyyy-MM-dd}", actualDateFr));
            }
            if (!(values["P_SENDTIME_PERIODTO"].ToString().Equals("")))
            {
                DateTime actualDateTo = Convert.ToDateTime(values["P_SENDTIME_PERIODTO"]);
                values.Remove("P_SENDTIME_PERIODTO");
                actualDateTo = actualDateTo.AddDays(1);
                values.Add("P_SENDTIME_PERIODTO", string.Format("{0:yyyy-MM-dd}", actualDateTo));
            }
           
            #endregion
            DataTable dt = await SqlExecuter.QueryAsync("GetShipmentInspLRRListMaster", "10001", values);

            /*
            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData");
                DataTable dtPirceCode = (grdDetail.DataSource as DataTable).Clone();
                grdDetail.DataSource = dtPirceCode;
            }
            */

            // 2020.04.17-유석진-messge를 rtf형식으로 되어 있는 부분을 일반텍스트 형태로 변경
            foreach (DataRow dr in dt.Rows)
            {
                dr["MESSAGE"] = getplaintext(dr["MESSAGE"].ToString());
            }

            grdMaster.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                grdMaster.View.FocusedRowHandle = 0;
                grdMaster.View.SelectRow(0);
                //focusedRowChanged();
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            //InitializeConditionPopup_Plant();
            InitializeConditionPopup_Customer();

            InitializeConditionPopup_Product();
            InitializeConditionFinalInspectPopup_Area();
            InitializeConditionPrevProcessPopup_Area();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();


        }
        /// <summary>
        /// 사이트 조회조건
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {
            this.Conditions.AddComboBox("p_plantId", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetDefault(UserInfo.Current.Plant)
                .SetValidationIsRequired()
                .SetLabel("PLANT")
                .SetPosition(0.1);
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
               .SetPosition(2.2);

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
        /// <summary>
        /// 고객사 조회조건
        /// </summary>
        private void InitializeConditionPopup_Customer()
        {
            // 팝업 컬럼설정
            var areaPopup = Conditions.AddSelectPopup("p_customerId", new SqlQuery("GetCustomerList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
               .SetPopupLayout("CUSTOMER", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("CUSTOMER")
               .SetPopupResultCount(1)
               .SetPosition(2.4);

            // 팝업 조회조건
            areaPopup.Conditions.AddTextBox("TXTCUSTOMERID");

            // 팝업 그리드
            areaPopup.GridColumns.AddTextBoxColumn("CUSTOMERID", 150)
                .SetValidationKeyColumn();
            areaPopup.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);
        }

        /// <summary>
        /// 최종검사작업장 조회조건
        /// </summary>
        private void InitializeConditionFinalInspectPopup_Area()
        {
            // 팝업 컬럼설정
            var areaPopup = Conditions.AddSelectPopup("P_FIANLINSPECTAREAID", new SqlQuery("GetQCFinalInspectArea", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("FINISHAREANAME")
               .SetPopupResultCount(1)
               .SetPosition(4.1);

            // 팝업 조회조건
            areaPopup.Conditions.AddTextBox("AREA");

            // 팝업 그리드
            areaPopup.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetValidationKeyColumn();
            areaPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }

        /// <summary>
        /// 전공정작업장 조회조건
        /// </summary>
        private void InitializeConditionPrevProcessPopup_Area()
        {
            // 팝업 컬럼설정
            var areaPopup = Conditions.AddSelectPopup("P_PREVPROCESSAREAID", new SqlQuery("GetQCPrevProcessArea", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("PREVPROCESSAREA")
               .SetPopupResultCount(1)
               .SetPosition(4.2);

            // 팝업 조회조건
            areaPopup.Conditions.AddTextBox("AREA");

            // 팝업 그리드
            areaPopup.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetValidationKeyColumn();
            areaPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

           
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }
        /// <summary>
        /// 그리드의 포커스 행 변경시 행의 작업목록의 내용을 매핑처리 함.
        /// </summary>
        /*
        private void focusedRowChanged()
        {
            //포커스 행 체크 
            if (grdMaster.View.FocusedRowHandle < 0) return;

            //단가코드 정보 가져오기 
            Dictionary<string, object> Param = new Dictionary<string, object>();
 
            Param.Add("P_LOTID", grdMaster.View.GetFocusedRowCellValue("LOTID"));
            Param.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param.Add("P_TXNGROUPHISTKEY", grdMaster.View.GetFocusedRowCellValue("TXNGROUPHISTKEY"));
            DataTable dtDetail = SqlExecuter.Query("GetShipmentInspLRRListDetail", "10001", Param);

            grdDetail.DataSource = dtDetail;
        }
        */

        /// <summary>
        /// Main Grid에 Layer에 Footer에 Summary 처리
        /// </summary>
        private void SetFooterSummary()
        {
            grdMaster.View.Columns["RECEIVEPCSQTYQCM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grdMaster.View.Columns["RECEIVEPCSQTYQCM"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["GOODQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grdMaster.View.Columns["GOODQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["INSPECTIONQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grdMaster.View.Columns["INSPECTIONQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            //grdMaster.View.Columns["SPECINSPECTIONPNLQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //grdMaster.View.Columns["SPECINSPECTIONPNLQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            //grdMaster.View.Columns["SPECINSPECTIONPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //grdMaster.View.Columns["SPECINSPECTIONPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["SPECOUTPNLQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["SPECOUTPNLQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["SPECOUTPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["SPECOUTPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
        }

        /// <summary>
        /// 2020.04.17-유석진-messge 내용을 rtf 형식에서 일반내용으로 변경
        /// </summary>
        public string getplaintext(string rtftext)
        {
            string plainText = "";
            if (!string.IsNullOrEmpty(rtftext))
            {
                //Create the RichTextBox. (Requires a reference   to System.Windows.Forms.dll.)
                using (System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox())
                {
                    // Convert the RTF to plain text.
                    rtBox.Rtf = rtftext;
                    plainText = rtBox.Text;

                    // Now just remove the new line constants
                    plainText = plainText.Replace("\r\n", ",");
                    // Output plain text to file, encoded as UTF-8.
                }
            }
            return plainText;
        }
        #endregion
    }
}
