#region using

using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using Micube.SmartMES.QualityAnalysis.Helper;
using Micube.SmartMES.QualityAnalysis.ShipmentInspection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 출하검사 > 결과 등록 및 작업완료
    /// 프 로 그 램 명  : 품질관리 > 출하검사 > 결과 등록 및 작업완료
    /// 업  무  설  명  : 출하 검사 경과를 등록한다
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-09-23 -- 통합테스트 이전에 출하검사 먼저 개발
    /// 수  정  이  력  : 2020-03-04 돌아갈 최종 자원 선택로직 추가
    ///                  2020-03-07 유태근 / 재작업라우팅 로직 적용
    ///                  2020-03-08 강유라 / 파라미터 추가
    ///
    ///
    /// </summary>
    public partial class ShipmentInspectionResult : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        //private string _type = "";_inspStandard
        private string _lotId = "";

        private string _areaId = "";
        private string _processsegmentName = "";
        private string _aqlOrNcr = "";
        private string _decisionDegree = "";
        private string _aqlInspectionLevel = "";
        private string _aqlDefectLevel = "";
        private string _cboOldValue = "";
        //private string _nowAreaId = "";

        /// <summary>
        /// 임시 저장 DataTable
        /// </summary>
        private DataTable _tempSave;

        private DataTable _InspectorDegree;
        private DataTable _selectedDt;
        private DataTable _messageDt;
        private DataTable _toSaveMessage;

        //2020-02-19 강유라 이메일 첨부용
        private DataTable _FileToSendEmail;

        private DataRow _childLotRow = null;

        private DataRow _lotRow;
        private DataRow _selectedRow;
        private DataRow _processRow;
        private DataRow _creatorRow;
        private DataRow _standardRow;

        private int _allQtyPCS = 0; // Lot별 PCS 수량
        private int _allQtyPNL = 0; // Lot별 PNL 수량
        private decimal _inspectionQty = 0;//Lot별 샘플 수량

        private int _defectQtySumPCS = 0; //Lot별 PCS 불량 수량 (불량코드 별 PCS 불량 수량 합 )

        private int _defectQtySumPNL = 0; //Lot별 PNL 불량 수량 (불량코드 별 PNL 불량 수량 합 )
        private int _beforeDefectQty = 0;
        private bool autoChange = false;
        private bool _isAutoTransit = false;

        private string _strPlantId = "";
        private string _conditionPlantId = "";

        private DXMenuItem _myContextMenu1;

        // 2020-03-07 유태근 / 재작업라우팅 정의변수
        private string _reworkProcessDefId = "";

        private string _reworkProcessDefVersion = "";
        private string _reworkPathId = "";
        private string _reworkResourceId = "";
        private string _reworkAreaId = "";
        private string _reworkProcesssegmentId = "";
        private string _reworkProcesssegmentVersion = "";
        private string _reworkUserSequence = "";

        #endregion Local Variables

        #region 생성자

        public ShipmentInspectionResult()
        {
            InitializeComponent();
        }

        private void grdChildLot_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
        {
            //최종검사 결과조회 : PG-QC-0570, 프로그램ID : Micube.SmartMES.QualityAnalysis.FinalInspectionResultInquiry
            args.AddMenus.Add(_myContextMenu1 = new DXMenuItem(Language.Get("MENU_PG-QC-0570"), OpenForm) { BeginGroup = true, Tag = "PG-QC-0570" });
        }

        private void OpenForm(object sender, EventArgs args)
        {
            try
            {
                DialogManager.ShowWaitDialog();

                DataRow currentRow = grdChildLot.View.GetFocusedDataRow();
                if (currentRow == null) return;

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

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            // 유석진 수정-2019-12-10
            ucShipmentInfo.SetInspectorReadOnly(false); // 검사자 비활성화
            ucShipmentInfo.SetReadOnly(false); // 인계작업장 비활성화

            InitializeEvent();
            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
            InitializationSummaryRow();
            InitializeControl();
            InitializePopup();
        }

        /// <summary>
        /// 컨트롤 초기화한다.
        /// </summary>
        private void InitializeControl()
        {
            // LOT 정보 GRID
            grdLot.ColumnCount = 5;
            grdLot.LabelWidthWeight = "40%";
            grdLot.SetInvisibleFields("PROCESSSTATE", "PROCESSPATHID", "PROCESSSEGMENTID", "PROCESSSEGMENTVERSION", "NEXTPROCESSSEGMENTID",
                "NEXTPROCESSSEGMENTVERSION", "PRODUCTTYPE", "ISHOLD", "ISREWORK", "DEFECTUNIT",
                "PANELPERQTY", "PROCESSSEGMENTTYPE", "STEPTYPE", "ISPRINTLOTCARD", "ISPRINTRCLOTCARD", "TRACKINUSER",
                "TRACKINUSERNAME", "MATERIALCLASS");

            grdLot.Enabled = false;
            //***ucShipmentInfo.Enabled = false;
            //***grpTab.Enabled = false;

            picBox1.Properties.ShowMenu = false;
            picBox2.Properties.ShowMenu = false;
            picBox3.Properties.ShowMenu = false;
        }

        /// <summary>
        /// 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가

            #region 분할 LOT 합불/판정 탭 초기화

            #region 분할Lot 그리드 초기화

            //grdChildLot.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdChildLot.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect; // 유석진 수정-2019-12-10

            //childLot 정보
            var childLotInfo = grdChildLot.View.AddGroupColumn("");
            childLotInfo.AddTextBoxColumn("RESOURCEID", 200)
                .SetLabel("CHILDLOTID");

            childLotInfo.AddTextBoxColumn("DEGREE", 80);
            //.SetIsHidden();

            //전체수량
            var allQty = grdChildLot.View.AddGroupColumn("ALL");
            allQty.AddTextBoxColumn("ALLQTYPNL", 100)
                .SetLabel("PNL");
            allQty.AddTextBoxColumn("ALLQTYPCS", 100)
                .SetLabel("PCS");

            //양품
            var goodQty = grdChildLot.View.AddGroupColumn("GOODQTY");
            goodQty.AddTextBoxColumn("GOODQTYPNL", 100)
                 .SetLabel("PNL");
            goodQty.AddTextBoxColumn("GOODQTYPCS", 100)
                .SetLabel("PCS");

            //불량
            var defectQty = grdChildLot.View.AddGroupColumn("DEFECTCOUNT");
            defectQty.AddTextBoxColumn("DEFECTQTYPNL", 100)
                 .SetLabel("PNL");
            defectQty.AddTextBoxColumn("SPECOUTQTY", 100)
                .SetLabel("PCS"); ;
            defectQty.AddTextBoxColumn("DEFECTRATE", 100);

            //시료량
            var sampleQty = grdChildLot.View.AddGroupColumn("INSPECTIONQTY");
            sampleQty.AddSpinEditColumn("INSPECTIONQTY", 100)
                 .SetLabel("PCS");

            //검사결과
            var inspInfo = grdChildLot.View.AddGroupColumn("");
            inspInfo.AddTextBoxColumn("INSPECTIONUSER", 100)
                .SetIsHidden();
            inspInfo.AddTextBoxColumn("INSPECTIONRESULT", 100);

            grdChildLot.View.PopulateColumns();

            #endregion 분할Lot 그리드 초기화

            #region 분할 Lot 불량 그리드 초기화

            grdChildLotDetail.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            grdChildLotDetail.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdChildLotDetail.View.AddTextBoxColumn("RESOURCEID", 200)
                .SetLabel("CHILDLOTID")
                .SetIsHidden();

            grdChildLotDetail.View.AddTextBoxColumn("DEGREE", 80);

            InitializeGrid_DefectCodeListPopup();

            grdChildLotDetail.View.AddTextBoxColumn("QCSEGMENTNAME", 150)
                .SetIsReadOnly();

            grdChildLotDetail.View.AddTextBoxColumn("DEFECTCODE", 150)
                .SetIsHidden();

            grdChildLotDetail.View.AddTextBoxColumn("QCSEGMENTID", 150)
                .SetIsHidden();

            grdChildLotDetail.View.AddSpinEditColumn("DEFECTQTY", 100)
                .SetValidationIsRequired();

            grdChildLotDetail.View.AddTextBoxColumn("DEFECTQTYPNL", 100)
                .SetIsHidden();

            grdChildLotDetail.View.AddTextBoxColumn("DEFECTRATE", 100);

            grdChildLotDetail.View.AddTextBoxColumn("INSPECTIONRESULT", 100)
                .SetIsReadOnly();

            grdChildLotDetail.View.AddTextBoxColumn("QCGRADE", 100)
                .SetIsHidden();

            grdChildLotDetail.View.AddTextBoxColumn("PRIORITY", 100)
                .SetIsHidden();

            grdChildLotDetail.View.PopulateColumns();

            #endregion 분할 Lot 불량 그리드 초기화

            #endregion 분할 LOT 합불/판정 탭 초기화

            #region 주차 정보 탭 초기화

            #region 주차 No 그리드 초기화

            grdWeekNo.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdWeekNo.View.SetIsReadOnly();
            //주차 정보
            var weekNo = grdWeekNo.View.AddGroupColumn("");
            weekNo.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            weekNo.AddTextBoxColumn("WEEK", 100);

            //수량
            var weekQty = grdWeekNo.View.AddGroupColumn("QTY");
            weekQty.AddTextBoxColumn("PNLQTY", 80);
            weekQty.AddTextBoxColumn("QTY", 80);

            grdWeekNo.View.PopulateColumns();

            #endregion 주차 No 그리드 초기화

            #region 주차 No Lot 그리드 초기화

            grdWeekChildLotNo.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdWeekChildLotNo.View.SetIsReadOnly();
            // LotInfo
            var weekLotInfo = grdWeekChildLotNo.View.AddGroupColumn("");
            weekLotInfo.AddTextBoxColumn("PRODUCTDEFID", 150);
            weekLotInfo.AddTextBoxColumn("PRODUCTDEFVERSION", 150);
            weekLotInfo.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            weekLotInfo.AddTextBoxColumn("LOTID", 200);

            //수량
            var weekLotQty = grdWeekChildLotNo.View.AddGroupColumn("QTY");
            weekLotQty.AddTextBoxColumn("PNLQTY", 100);
            weekLotQty.AddTextBoxColumn("QTY", 100);

            grdWeekChildLotNo.View.PopulateColumns();

            #endregion 주차 No Lot 그리드 초기화

            #endregion 주차 정보 탭 초기화

            #region 변경이력 탭 초기화

            #region 변경이력 그리드 초기화

            this.grdProductHist.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            this.grdProductHist.GridButtonItem = GridButtonItem.Export;
            this.grdProductHist.View.SetIsReadOnly();

            this.grdProductHist.View.AddTextBoxColumn("PLANTID", 60)
                .SetTextAlignment(TextAlignment.Center);

            this.grdProductHist.View.AddTextBoxColumn("PRODUCTDEFID", 100)
                .SetIsHidden();

            this.grdProductHist.View.AddTextBoxColumn("ITEMID", 80)
                .SetTextAlignment(TextAlignment.Center);

            this.grdProductHist.View.AddTextBoxColumn("CUSTOMERREV", 50)
                .SetTextAlignment(TextAlignment.Center);

            this.grdProductHist.View.AddTextBoxColumn("PRODUCTDEFVERSION", 50)
               .SetTextAlignment(TextAlignment.Center);

            this.grdProductHist.View.AddTextBoxColumn("PRODUCTNAME", 200)
                .SetLabel("PRODUCTDEFNAME");

            this.grdProductHist.View.AddTextBoxColumn("SPECOWNERNAME", 80);

            this.grdProductHist.View.AddTextBoxColumn("CREATEDTIME", 80)
                .SetDisplayFormat("yyyy-MM-dd")
               .SetTextAlignment(TextAlignment.Center);

            this.grdProductHist.View.AddTextBoxColumn("GOVERNANCENO", 100)
                .SetIsHidden();
            this.grdProductHist.View.AddTextBoxColumn("RCPRODUCTDEFID", 100)
                .SetIsHidden();
            this.grdProductHist.View.AddTextBoxColumn("RCPRODUCTDEFVERSION", 100)
                .SetIsHidden();

            this.grdProductHist.View.AddTextBoxColumn("ISRC", 70)
                .SetTextAlignment(TextAlignment.Center);

            this.grdProductHist.View.AddTextBoxColumn("ISPCN", 70)
                .SetTextAlignment(TextAlignment.Center);

            this.grdProductHist.View.AddTextBoxColumn("PCNREQUESTDATE", 100)
                .SetIsHidden();

            this.grdProductHist.View.AddTextBoxColumn("CHANGECOMMENT", 300)
                .SetLabel("SPECCHANGE");
            this.grdProductHist.View.AddTextBoxColumn("CHANGENOTE", 300)
                .SetLabel("COMMENT");

            this.grdProductHist.View.PopulateColumns();

            this.grdProductHist.View.OptionsView.RowAutoHeight = true;

            DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit memoEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            memoEdit.AutoHeight = true;
            memoEdit.WordWrap = true;

            this.grdProductHist.View.Columns["CHANGECOMMENT"].ColumnEdit = memoEdit;
            this.grdProductHist.View.Columns["CHANGENOTE"].ColumnEdit = memoEdit;

            #endregion 변경이력 그리드 초기화

            #endregion 변경이력 탭 초기화

            #region 메세지 탭 초기화

            grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdLotList.View.SetIsReadOnly();
            grdLotList.View.SetAutoFillColumn("RESOURCEID");

            grdLotList.View.AddTextBoxColumn("RESOURCEID")
                .SetLabel("LOTID");

            grdLotList.View.PopulateColumns();

            grdProcessSegmentList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdProcessSegmentList.View.SetIsReadOnly();
            grdProcessSegmentList.View.SetAutoFillColumn("PROCESSSEGMENTNAME");

            grdProcessSegmentList.View.AddTextBoxColumn("PROCESSSEGMENTNAME");

            grdProcessSegmentList.View.PopulateColumns();

            grdCreatorList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdCreatorList.View.SetIsReadOnly();
            grdCreatorList.View.SetAutoFillColumn("CREATORNAME");

            grdCreatorList.View.AddTextBoxColumn("CREATORNAME");

            grdCreatorList.View.PopulateColumns();

            #endregion 메세지 탭 초기화
        }

        #region 그리드 팝업 초기화

        private void Initialize_ProcessSegment()
        {
            var processSegmentId = grdChildLotDetail.View.AddSelectPopupColumn("PROCESSSEGMENTNAME", new SqlQuery("GetProcessSegmentList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTID")
                                                   .SetPopupLayout("PROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
                                                   .SetPopupResultCount(1)
                                                   .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                                                   .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                                                   .SetValidationKeyColumn();

            processSegmentId.Conditions.AddTextBox("PROCESSSEGMENT");

            // 팝업 그리드
            processSegmentId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processSegmentId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTVERSION", 150);
            processSegmentId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
        }

        /// <summary>
        /// 불량 코드 팝업
        /// </summary>
        private void InitializeGrid_DefectCodeListPopup()
        {
            var defectCodePopupColumn = grdChildLotDetail.View.AddSelectPopupColumn("DEFECTCODENAME", 250, new SqlQuery("GetDefectCodeByProcess", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("SELECTDEFECTCODEID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("DEFECTCODENAME")
                .SetLabel("DEFECTCODENAME")
                .SetPopupApplySelection((selectedRows, dataGridRows) =>
                {
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow

                    DataTable dt = grdChildLotDetail.DataSource as DataTable;
                    DataRow dr = selectedRows.FirstOrDefault();

                    if (dr != null)
                    {
                        int icnt = dt.Select("DEFECTCODE = '" + dr["DEFECTCODE"].ToString() + "'").Count();
                        int icntQCId = dt.Select("QCSEGMENTID = '" + dr["QCSEGMENTID"].ToString() + "'").Count();
                        if (icnt > 0 && icntQCId > 0)

                        {
                            throw MessageException.Create("DuplicationDefectCode");
                        }

                        dataGridRows["DEFECTCODE"] = dr["DEFECTCODE"].ToString();
                        dataGridRows["QCSEGMENTID"] = dr["QCSEGMENTID"].ToString();
                        dataGridRows["QCSEGMENTNAME"] = dr["QCSEGMENTNAME"].ToString();
                    }

                    /*
                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRows["DEFECTCODE"] = row["DEFECTCODE"].ToString();
                        dataGridRows["QCSEGMENTID"] = row["QCSEGMENTID"].ToString();
                        dataGridRows["QCSEGMENTNAME"] = row["QCSEGMENTNAME"].ToString();
                    }
                    */
                });

            // 팝업의 검색조건 항목 추가 (불량코드/명)
            defectCodePopupColumn.Conditions.AddTextBox("TXTDEFECTCODE").
                SetLabel("DEFECTCODE");
            // 팝업의 검색조건 항목 추가 (품질공정id/명)
            defectCodePopupColumn.Conditions.AddTextBox("TXTQCSEGMENT")
                .SetLabel("QCSEGMENT");

            defectCodePopupColumn.GridColumns.AddTextBoxColumn("DEFECTCODE", 150)
                .SetIsReadOnly();
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("DEFECTCODENAME", 200)
                .SetIsReadOnly();
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("QCSEGMENTID", 150)
                .SetIsReadOnly();
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("QCSEGMENTNAME", 200)
                .SetIsReadOnly();
        }

        #endregion 그리드 팝업 초기화

        #region Row 합계 초기화

        /// <summary>
        /// 합계 Row 초기화
        /// </summary>
        private void InitializationSummaryRow()
        {
            #region grdChildLot Row 합계 초기화

            grdChildLot.View.Columns["RESOURCEID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdChildLot.View.Columns["RESOURCEID"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdChildLot.View.Columns["ALLQTYPNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdChildLot.View.Columns["ALLQTYPNL"].SummaryItem.DisplayFormat = "{0}";

            grdChildLot.View.Columns["ALLQTYPCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdChildLot.View.Columns["ALLQTYPCS"].SummaryItem.DisplayFormat = "{0}";

            grdChildLot.View.Columns["GOODQTYPNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdChildLot.View.Columns["GOODQTYPNL"].SummaryItem.DisplayFormat = "{0}";

            grdChildLot.View.Columns["GOODQTYPCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdChildLot.View.Columns["GOODQTYPCS"].SummaryItem.DisplayFormat = "{0}";

            grdChildLot.View.Columns["DEFECTQTYPNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdChildLot.View.Columns["DEFECTQTYPNL"].SummaryItem.DisplayFormat = "{0}";

            grdChildLot.View.Columns["SPECOUTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdChildLot.View.Columns["SPECOUTQTY"].SummaryItem.DisplayFormat = "{0}";

            grdChildLot.View.Columns["DEFECTRATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grdChildLot.View.Columns["DEFECTRATE"].SummaryItem.DisplayFormat = "{0:f2} %";

            grdChildLot.View.Columns["INSPECTIONQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdChildLot.View.Columns["INSPECTIONQTY"].SummaryItem.DisplayFormat = "{0}";

            grdChildLot.View.OptionsView.ShowFooter = true;
            grdChildLot.ShowStatusBar = false;

            #endregion grdChildLot Row 합계 초기화

            #region grdChildLot Row 합계 초기화

            grdChildLotDetail.View.Columns["DEGREE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdChildLotDetail.View.Columns["DEGREE"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdChildLotDetail.View.Columns["DEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdChildLotDetail.View.Columns["DEFECTQTY"].SummaryItem.DisplayFormat = "{0}";

            grdChildLotDetail.View.Columns["DEFECTQTYPNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdChildLotDetail.View.Columns["DEFECTQTYPNL"].SummaryItem.DisplayFormat = "{0}"; ;

            grdChildLotDetail.View.Columns["DEFECTRATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;//***불량율 합계는 100넘을 수있음 다시계산?***
            grdChildLotDetail.View.Columns["DEFECTRATE"].SummaryItem.DisplayFormat = "{0:f2} %";

            grdChildLotDetail.View.OptionsView.ShowFooter = true;
            grdChildLotDetail.ShowStatusBar = false;

            #endregion grdChildLot Row 합계 초기화
        }

        #endregion Row 합계 초기화

        #region Popup 초기화

        /// <summary>
        /// 작업장 팝업
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup Initialize_AreaPopup()
        {
            //2020-01-03 plant조회조건 -> 작업장 팝업 파라미터 적용
            string plantId = Conditions.GetControl<SmartComboBox>("P_PLANTID").EditValue.ToString();

            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            popup.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true);
            popup.Id = "AREAID";
            popup.SearchQuery = new SqlQuery("GetShipmentInspAreaList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"P_PLANTID={plantId}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "AREANAME";
            popup.ValueFieldName = "AREAID";
            popup.LanguageKey = "AREA";
            popup.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                foreach (DataRow row in selectedRow)
                {
                    _areaId = row["AREAID"].ToString();
                    //popLotId.SelectPopupCondition = Initialize_LotPopup();
                }
            });

            popup.Conditions.AddTextBox("AREA");

            popup.GridColumns.AddTextBoxColumn("AREAID", 150);
            popup.GridColumns.AddTextBoxColumn("AREANAME", 200);

            return popup;
        }

        /*
            /// <summary>
            /// LOTID 팝업 초기화***내부 쿼리 / 항목수정
            /// </summary>
            /// <returns></returns>
            private ConditionItemSelectPopup Initialize_LotPopup()
            {
                ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

                popup.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
                popup.SetPopupLayout("LOT", PopupButtonStyles.Ok_Cancel, true, true);
                popup.Id = "LOTID";
                popup.SearchQuery = new SqlQuery("GetCondLotPopupShipmentInsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"AREAID={_areaId}");
                popup.IsMultiGrid = false;
                popup.DisplayFieldName = "LOTID";
                popup.ValueFieldName = "LOTID";
                popup.LanguageKey = "LOTID";

                popup.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                {
                    foreach (DataRow row in selectedRow)
                    {
                        txtOriginLotId.Editor.EditValue = row["LOTID"].ToString();
                        //txtOriginLotId.Editor.();
                    }
                });

                popup.Conditions.AddTextBox("USERIDNAME");

                popup.GridColumns.AddTextBoxColumn("LOTID", 150);
                popup.GridColumns.AddTextBoxColumn("AREANAME", 200);
                popup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 200);
                popup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
                popup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 200);

                return popup;
            }
            */

        #endregion Popup 초기화

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            #region Load 이벤트

            this.Load += (s, e) =>
            {
                grdChildLotDetail.DataSource = CreateDataSource();
                CreateTableToSaveMessage();
                SetStandartCombo();
            };

            #endregion Load 이벤트

            #region 이미지 이벤트

            //이미지 추가 버튼 클릭 이벤트
            btnAddImage.Click += BtnAddImage_Click;

            //이미지 삭제 이벤트
            picBox1.KeyDown += PicDefect_KeyDown;
            picBox2.KeyDown += PicDefect_KeyDown;
            picBox3.KeyDown += PicDefect_KeyDown;

            //이미지 tooltip 이벤트
            picBox1.MouseEnter += PicBox_MouseEnter;
            picBox2.MouseEnter += PicBox_MouseEnter;
            picBox3.MouseEnter += PicBox_MouseEnter;

            #endregion 이미지 이벤트

            #region grdChildLot 그리드 이벤트

            //Lot 그리드 포커스row 체인지이벤트
            grdChildLot.View.FocusedRowChanged += View_FocusedRowChanged;

            grdChildLot.View.ShowingEditor += View_ShowingEditor;

            //grdChildLot 그리드의 cell 값 변경 이벤트
            grdChildLot.View.CellValueChanged += GrdChildLot_CellValueChanged;

            //grdChildLot 불량율 Custom 계산 이벤트 (Footer)
            grdChildLot.View.CustomSummaryCalculate += GrdChildLot_CustomSummaryCalculate;

            ///grdChildLot 화면 연계
            grdChildLot.InitContextMenuEvent += grdChildLot_InitContextMenuEvent;

            #endregion grdChildLot 그리드 이벤트

            #region grdChildLotDetail 그리드 이벤트

            //grdChildLotDetail +버튼 클릭시 이벤트
            grdChildLotDetail.ToolbarAddingRow += (s, e) =>
            {
                DataRow row = grdChildLot.View.GetFocusedDataRow();
                if (row == null)
                {
                    e.Cancel = true;
                }
                else if (string.IsNullOrWhiteSpace(row["INSPECTIONQTY"].ToString()))
                {
                    ShowMessage("NeedToInputSampleQty");//시료량을 먼저 입력해야 합니다.
                    e.Cancel = true;
                }
            };

            grdChildLotDetail.View.AddingNewRow += (s, e) =>
            {
                SetStandardData(e.NewRow);
            };

            grdChildLotDetail.ToolbarDeletingRow += (s, e) =>
              {
                  DataRow deleteRow = grdChildLotDetail.View.GetFocusedDataRow();

                  if (deleteRow == null) return;

                  if (!deleteRow.RowState.Equals(DataRowState.Added))
                  {
                      deleteRow["ISDELETE"] = "Y";

                      picBox1.Image = null;
                      picBox2.Image = null;
                      picBox3.Image = null;

                      e.Cancel = true;
                  }
              };

            grdChildLotDetail.ToolbarDeleteRow += (s, e) =>
            {
                DataRow childLotRow = grdChildLot.View.GetFocusedDataRow();

                if (childLotRow == null) return;
                DataTable dt = grdChildLotDetail.DataSource as DataTable;
                int NGCount = GetNGCount(dt);
                string finalResult = "";
                string messageId = null;

                grdChildLot.View.CellValueChanged -= GrdChildLot_CellValueChanged;

                if (dt.Rows.Count > 0)
                {
                    if (NGCount == 0)
                    {//NG 한건도 없을 때
                        finalResult = "OK";

                        //2020-03-27 강유라 불량항목별 판정 후 => 전체 수량으로 판정
                        //전체 LOT AQL 판정
                        if (_aqlOrNcr.Equals("AQL"))
                        {
                            //AQL기준
                            finalResult = InspectionHelper.SetQcGradeAndResultAQLType(_childLotRow, _aqlInspectionLevel, _aqlDefectLevel, "ShipmentInspection", _decisionDegree, _childLotRow["ALLQTYPCS"].ToString(), false, out messageId);
                        }
                        else if (_aqlOrNcr.Equals("NCR"))
                        {
                            //NCR기준
                            finalResult = InspectionHelper.SetQcGradeAndResultNCRQtyRateType(_childLotRow, "ShipmentInspection", _decisionDegree, false, out messageId);
                        }

                        grdChildLot.View.SetFocusedRowCellValue("INSPECTIONRESULT", finalResult);
                    }
                    else
                    {
                        grdChildLot.View.SetFocusedRowCellValue("INSPECTIONRESULT", "NG");
                    }
                }
                else
                {
                    grdChildLot.View.SetFocusedRowCellValue("SPECOUTQTY", 0);
                    grdChildLot.View.SetFocusedRowCellValue("DEFECTQTYPNL", 0);
                    grdChildLot.View.SetFocusedRowCellValue("INSPECTIONRESULT", "OK");
                    grdChildLot.View.SetFocusedRowCellValue("DEFECTRATE", "0%");
                }

                grdChildLot.View.CellValueChanged += GrdChildLot_CellValueChanged;
            };

            //grdChildLotDetail 불량율 Custom 계산 이벤트 (Footer)
            grdChildLotDetail.View.CustomSummaryCalculate += GrdChildLotDetail_CustomSummaryCalculate;

            //Lot별 불량코드등록 그리드 포커스row 체인지이벤트 (사진 조회)
            grdChildLotDetail.View.FocusedRowChanged += FocusedRowChangedBeforeSave;

            //불량수량이 변경 될 때 이벤트
            grdChildLotDetail.View.CellValueChanged += GrdChildLotDetail_CellValueChanged;

            #endregion grdChildLotDetail 그리드 이벤트

            #region grdWeekNo 그리드 이벤트

            grdWeekNo.View.FocusedRowChanged += (s, e) =>
              {
                  DataRow row = grdWeekNo.View.GetDataRow(e.FocusedRowHandle);
                  SearchLotInfoByWeekNo(row);
              };

            #endregion grdWeekNo 그리드 이벤트

            #region Message 탭이벤트

            //grdLotList focusedRow 체인지 이벤트
            grdLotList.View.FocusedRowChanged += (s, e) =>
            {
                _lotRow = grdLotList.View.GetDataRow(e.FocusedRowHandle);
                SearchProcessSegmentList(_lotRow);
            };

            //grdProcessSegmentList focusedRow 체인지 이벤트
            grdProcessSegmentList.View.FocusedRowChanged += (s, e) =>
            {
                _lotRow = grdLotList.View.GetFocusedDataRow();
                _processRow = grdProcessSegmentList.View.GetDataRow(e.FocusedRowHandle);
                SearchCreatorList(_processRow, _lotRow);
            };

            //grdCreatorList focusedRow 체인지 이벤트
            grdCreatorList.View.FocusedRowChanged += (s, e) =>
            {
                _lotRow = grdLotList.View.GetFocusedDataRow();
                _processRow = grdProcessSegmentList.View.GetFocusedDataRow();
                _creatorRow = grdCreatorList.View.GetDataRow(e.FocusedRowHandle);
                SearchMessageList(_creatorRow, _processRow, _lotRow);
            };

            #endregion Message 탭이벤트

            #region 컨트롤 이벤트

            //초기화 버튼 클릭 이벤트
            //btnInit.Click += (s, e) =>
            //{
            //ClearAllData();
            //};

            //임시저장 클릭시 이벤트
            btnTempSave.Click += BtnTempSave_Click;

            //lotId 입력 후 엔터 이벤트★★
            txtOriginLotId.Editor.KeyDown += Editor_KeyDown;

            //lotIdPopup x 버튼 클릭이벤트
            popAreaId.ButtonClick += PopAreaId_ButtonClick;

            //btnWrite 버튼 클릭이벤트
            btnWrite.Click += (s, e) =>
            {
                DataTable dt = (grdChildLot.DataSource as DataTable).Copy();
                if (dt == null || dt.Rows.Count < 1)
                {
                    throw MessageException.Create("NoLotToSaveMessage");//메세지를 저장할 Lot이 없습니다.
                }
                else
                {
                    DialogManager.ShowWaitArea(pnlContent);

                    ShipmentInspMessagePopup messagePopup = new ShipmentInspMessagePopup();
                    messagePopup.StartPosition = FormStartPosition.CenterParent;
                    messagePopup.FormBorderStyle = FormBorderStyle.Sizable;
                    messagePopup._lotListDt = dt;
                    messagePopup._processSegmentName = _processsegmentName;
                    messagePopup._userName = Framework.UserInfo.Current.Name;
                    messagePopup.Owner = this;
                    messagePopup.SaveMessageTable += (checkedRows, title, contents) => SetDataTableSaveMessage(checkedRows, title, contents);
                    DialogManager.CloseWaitArea(pnlContent);
                    messagePopup.ShowDialog();
                }
            };
            //lotIdPopup x 버튼 클릭이벤트
            //popLotId.ButtonClick += PopupLotId_ButtonClick;

            /*-----------------조회쪽으로 이동
               //NCR발행 버튼 클릭
               btnNCR.Click += (s, e) =>
               {
                   ShipmentInspNCRPopup ncrPopup = new ShipmentInspNCRPopup();
                   ncrPopup.StartPosition = FormStartPosition.CenterParent;
                   ncrPopup.ShowDialog();
               };
               */

            //2020-01-03 plant조회조건 -> 작업장 팝업 파라미터 적용
            popAreaId.Click += (s, e) =>
            {
                popAreaId.SelectPopupCondition = Initialize_AreaPopup();
            };

            //판정기준 변경이벤트
            cboStandardType.Editor.EditValueChanged += Editor_EditValueChanged;

            //2020-01-17 판정기준 변경전 예전 값 할당
            cboStandardType.Editor.Click += (s, e) =>
            {
                _cboOldValue = Format.GetString(cboStandardType.EditValue);
            };

            #endregion 컨트롤 이벤트
        }

        /// <summary>
        /// tooltip이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicBox_MouseEnter(object sender, EventArgs e)
        {
            SmartPictureEdit pic = sender as SmartPictureEdit;

            SuperToolTip tooltip = new SuperToolTip();
            SuperToolTipSetupArgs args = new SuperToolTipSetupArgs();

            Image image = null;
            image = pic.Image;
            pic.SuperTip = null;
            args.Contents.Image = null;

            if (image == null) return;

            args.Contents.Image = image;
            tooltip.Setup(args);

            pic.SuperTip = tooltip;
        }

        /// <summary>
        /// cboStandardType 값 바뀔때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editor_EditValueChanged(object sender, EventArgs e)
        {
            if ((grdChildLot.DataSource as DataTable).Rows.Count == 0) return;

            //2020-01-17 판정기준 바꿀때 초기화 여부 확인하기
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "ChangeStandardResetShipInsp");//판정 기준을 바꾸면 입력한 모든데이터가 초기화 됩니다. 판정기준을 바꾸시겠습니까?

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    grdChildLotDetail.DataSource = CreateDataSource();
                    _tempSave = null;

                    picBox1.Image = null;
                    picBox2.Image = null;
                    picBox3.Image = null;

                    SetInspStandard(_standardRow);
                    getShipmentInspectionLotList();
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
            }
            else
            {
                cboStandardType.Editor.EditValueChanged -= Editor_EditValueChanged;
                cboStandardType.Editor.EditValue = _cboOldValue;
                cboStandardType.Editor.EditValueChanged += Editor_EditValueChanged;
            }
        }

        #region 이미지 관련 이벤트

        /// <summary>
        /// 포커스된 row에 등록된 이미지를 보여주는 이벤트(저장전)
        /// 포커스된 row에 등록된 이미지를 보여주는 이벤트(저장전)
        /// inspItem dt에 파일정보 저장하여 이미지 보여주기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FocusedRowChangedBeforeSave(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = grdChildLotDetail.View.GetDataRow(e.FocusedRowHandle);

            SearchImage(row);
        }

        /// <summary>
        /// 이미지 추가버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddImage_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row = grdChildLotDetail.View.GetFocusedDataRow();
                if (row == null) return;

                if (string.IsNullOrWhiteSpace(row["DEFECTCODE"].ToString()))
                {
                    this.ShowMessage("DefectCodeIsRequired");//불량코드를 먼저 입력하세요.
                    return;
                }
                /*
                if (_type.Equals("updateData"))
                {
                    ShowMessage("CanAddImageBeforeResultSave");//검사 결과가 저장된 후에는 이미지를 추가 할 수 없습니다.
                    return;
                }*/

                string childLotId = row["RESOURCEID"].ToString();
                string degree = row["DEGREE"].ToString();
                string defectCode = row["DEFECTCODE"].ToString();
                string QCsegmentId = row["QCSEGMENTID"].ToString();

                DialogManager.ShowWaitArea(this);

                string imageFile = string.Empty;

                OpenFileDialog dialog = new OpenFileDialog
                {
                    Multiselect = false,
                    Filter = "Image Files (*.bmp, *.jpg, *.jpeg, *.png)|*.BMP;*.JPG;*.JPEG;*.PNG",
                    FilterIndex = 0
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    //****lotid/defectcode/degree
                    row["FILEINSPECTIONTYPE"] = "ShipmentInspection";
                    row["FILERESOURCEID"] = childLotId + degree + defectCode + QCsegmentId;
                    row["PROCESSRELNO"] = "*";

                    imageFile = dialog.FileName;
                    FileInfo fileInfo = new FileInfo(dialog.FileName);
                    using (FileStream fs = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        byte[] data = new byte[fileInfo.Length];
                        fs.Read(data, 0, (int)fileInfo.Length);

                        MemoryStream ms = new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(data).ToString()));
                        if (picBox1.Image == null)
                        {
                            row["FILENAME1"] = dialog.SafeFileName;
                            string a = fileInfo.Name;
                            string oldb = dialog.SafeFileName;
                            //2. 파일을 읽어들일때 File Binary를 읽어오던 부분을 경로를 저장하는 것으로 변경
                            //=========================================================================================================================
                            //YJKIM : binary파일을 저장하지 않고 File을 Upload하는 형태로 변경
                            //File Data를 저장할 필요가 없음
                            //row["FILEDATA1"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(dialog.FileName), typeof(byte[]));
                            row["FILEFULLPATH1"] = dialog.FileName; //파일의 전체경로 저장
                            row["FILEID1"] = "FILE-" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            //2020-02-19 강유라 이메일 첨부용
                            row["FILEDATA1"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(dialog.FileName), typeof(byte[]));
                            //row["FILEDATA1"] = data;
                            //-------------------------------------------------------------------------------------------------------------------------
                            row["FILECOMMENTS1"] = "InspectionResult/ShipmentInspection";
                            row["FILESIZE1"] = Math.Round(Convert.ToDouble(fileInfo.Length) / 1024);
                            row["FILEEXT1"] = fileInfo.Extension.Substring(1);

                            picBox1.Image = Image.FromStream(ms);
                        }
                        else if (picBox2.Image == null)
                        {
                            row["FILENAME2"] = dialog.SafeFileName;
                            //2. 파일을 읽어들일때 File Binary를 읽어오던 부분을 경로를 저장하는 것으로 변경
                            //=========================================================================================================================
                            //YJKIM : binary파일을 저장하지 않고 File을 Upload하는 형태로 변경
                            //File Data를 저장할 필요가 없음
                            //row["FILEDATA1"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(dialog.FileName), typeof(byte[]));
                            row["FILEFULLPATH2"] = dialog.FileName; //파일의 전체경로 저장
                            row["FILEID2"] = "FILE-" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            //2020-02-19 강유라 이메일 첨부용
                            row["FILEDATA2"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(dialog.FileName), typeof(byte[]));
                            //-------------------------------------------------------------------------------------------------------------------------
                            row["FILECOMMENTS2"] = "InspectionResult/ShipmentInspection";
                            row["FILESIZE2"] = Math.Round(Convert.ToDouble(fileInfo.Length) / 1024);
                            row["FILEEXT2"] = fileInfo.Extension.Substring(1);

                            picBox2.Image = Image.FromStream(ms);
                        }
                        else
                        {
                            row["FILENAME3"] = dialog.SafeFileName;
                            //2. 파일을 읽어들일때 File Binary를 읽어오던 부분을 경로를 저장하는 것으로 변경
                            //=========================================================================================================================
                            //YJKIM : binary파일을 저장하지 않고 File을 Upload하는 형태로 변경
                            //File Data를 저장할 필요가 없음
                            //row["FILEDATA1"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(dialog.FileName), typeof(byte[]));
                            row["FILEFULLPATH3"] = dialog.FileName; //파일의 전체경로 저장
                            row["FILEID3"] = "FILE-" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            //2020-02-19 강유라 이메일 첨부용
                            row["FILEDATA3"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(dialog.FileName), typeof(byte[]));
                            //-------------------------------------------------------------------------------------------------------------------------
                            row["FILECOMMENTS3"] = "InspectionResult/ShipmentInspection";
                            row["FILESIZE3"] = Math.Round(Convert.ToDouble(fileInfo.Length) / 1024);
                            row["FILEEXT3"] = fileInfo.Extension.Substring(1);

                            picBox3.Image = Image.FromStream(ms);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw Framework.MessageException.Create(ex.ToString());
            }
            finally
            {
                DialogManager.CloseWaitArea(this);
            }
        }

        /// <summary>
        /// PictureBox에 delete버튼 클릭시 사진지우는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicDefect_KeyDown(object sender, KeyEventArgs e)
        {
            // if (e.KeyCode == Keys.Delete && _type.Equals("insertData"))
            if (e.KeyCode == Keys.Delete)
            {
                DataRow row = grdChildLotDetail.View.GetFocusedDataRow();
                if (row == null) return;
                SmartPictureEdit picBox = sender as SmartPictureEdit;
                picBox.Image = null;

                if (picBox.Name.Equals(picBox1.Name))
                {
                    row["FILEDATA1"] = null;
                    row["FILECOMMENTS1"] = null;
                    row["FILESIZE1"] = DBNull.Value;
                    row["FILEEXT1"] = null;
                    row["FILENAME1"] = null;
                    //===========================================================================
                    //YJKIM : File의 FullPath를 저장할 필드를 설정
                    row["FILEFULLPATH1"] = null;
                    row["FILEID1"] = null;
                    //---------------------------------------------------------------------------
                }
                else if (picBox.Name.Equals(picBox2.Name))
                {
                    row["FILEDATA2"] = null;
                    row["FILECOMMENTS2"] = null;
                    row["FILESIZE2"] = DBNull.Value;
                    row["FILEEXT2"] = null;
                    row["FILENAME2"] = null;
                    //===========================================================================
                    //YJKIM : File의 FullPath를 저장할 필드를 설정
                    row["FILEFULLPATH2"] = null;
                    row["FILEID2"] = null;
                    //----------------------------------------------------------------------------
                }
                else if (picBox.Name.Equals(picBox3.Name))
                {
                    row["FILEDATA3"] = null;
                    row["FILECOMMENTS3"] = null;
                    row["FILESIZE3"] = DBNull.Value;
                    row["FILEEXT3"] = null;
                    row["FILENAME3"] = null;
                    //===========================================================================
                    //YJKIM : File의 FullPath를 저장할 필드를 설정
                    row["FILEFULLPATH3"] = null;
                    row["FILEID3"] = null;
                    //---------------------------------------------------------------------------
                }
            }
        }

        #endregion 이미지 관련 이벤트

        #region grdChildLot 이벤트

        /// <summary>
        /// 그리드 readOnly 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (!grdChildLot.View.FocusedColumn.FieldName.ToString().Equals("INSPECTIONQTY"))
            {
                e.Cancel = true;
            }

            if (grdChildLot.View.FocusedColumn.FieldName.ToString().Equals("INSPECTIONQTY")
                && Format.GetString(cboStandardType.Editor.EditValue).Equals("AQL"))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// grdChildLot 포커스 된 row가 바뀔때 임시 저장한(_tempSave) Data를 grdChildLotDetail에 보여 주는 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (autoChange == true)
            {
                autoChange = false;
                return;
            }

            DataRow beforeRow = grdChildLot.View.GetDataRow(e.PrevFocusedRowHandle);

            int tempCount = 0;
            if (_tempSave != null && e.PrevFocusedRowHandle >= 0)
            {
                tempCount = _tempSave.AsEnumerable().
                Where(r => r["RESOURCEID"].ToString().Equals(beforeRow["RESOURCEID"].ToString())
                && r["DEGREE"].ToString().Equals(beforeRow["DEGREE"].ToString()))
                .ToList().Count;
            }

            if ((grdChildLotDetail.DataSource as DataTable).Rows.Count > 0 && tempCount == 0)
            {
                DialogResult result = System.Windows.Forms.DialogResult.No;

                result = this.ShowMessage(MessageBoxButtons.YesNo, "HasDefectData");//임시저장하지 않은 불량정보가 있습니다. 임시저장하지 않은 데이터는 저장되지않습니다. 선택한 LOT을 바꾸시겠습니까?

                if (result == System.Windows.Forms.DialogResult.No)
                {
                    autoChange = true;
                    grdChildLot.View.FocusedRowHandle = e.PrevFocusedRowHandle;
                    return;
                }
            }
            _childLotRow = grdChildLot.View.GetDataRow(e.FocusedRowHandle);

            picBox1.Image = null;
            picBox2.Image = null;
            picBox3.Image = null;

            SetChildLotInfo(_childLotRow);

            SearchDetailByLotId();

            if (_childLotRow != null)
            {   //불량율을 계산 하기위해 변수에 할당
                _inspectionQty = _childLotRow["INSPECTIONQTY"].ToSafeInt32();
                _allQtyPCS = _childLotRow["ALLQTYPCS"].ToSafeInt32();
                _allQtyPNL = _childLotRow["ALLQTYPNL"].ToSafeInt32();
            }
        }

        private void GrdChildLot_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Equals("INSPECTIONQTY") && e.Value != DBNull.Value)
            {/*
                if (e.Value == DBNull.Value || string.IsNullOrWhiteSpace(e.Value.ToString()))
                {
                    ShowMessage("InspQtyIsRequired");//검사수량은 빈값일 수 없습니다.
                    return;
                }
                */
                _inspectionQty = e.Value.ToSafeInt64();

                if (_inspectionQty <= 0 && e.Value != DBNull.Value)
                {
                    ShowMessage("InvalidSampleQty");//샘플 수량은 음수 또는 0이 될 수 없습니다.
                    grdChildLot.View.SetFocusedRowCellValue(e.Column, DBNull.Value);
                }
                else if (_inspectionQty > _childLotRow["ALLQTYPCS"].ToSafeInt64() && e.Value != DBNull.Value)
                {
                    ShowMessage("InvalidSampleQtyOverQty");//샘플 수량은 전체 수량보다 클 수 없습니다
                    grdChildLot.View.SetFocusedRowCellValue(e.Column, DBNull.Value);
                }

                DataTable defectDt = grdChildLotDetail.DataSource as DataTable;

                foreach (DataRow row in defectDt.Rows)
                {
                    if (_inspectionQty < _defectQtySumPCS)
                    {
                        //grdChildLotDetail 불량율 계산
                        row["DEFECTQTY"] = 0;
                        row["DEFECTRATE"] = "0%";
                        row["DEFECTQTYPNL"] = 0;
                    }
                    else if (!string.IsNullOrWhiteSpace(row["DEFECTQTY"].ToString()) && _inspectionQty != 0)
                    {
                        //grdChildLotDetail 불량율 계산
                        decimal defectRate = Math.Round((row["DEFECTQTY"].ToSafeDecimal() / _inspectionQty * 100).ToSafeDecimal(), 1);
                        row["DEFECTRATE"] = defectRate + "%";

                        decimal defectQtyPNL = Math.Round((row["DEFECTQTY"].ToSafeDecimal() / row["PANELPERQTY"].ToSafeDecimal()).ToSafeDecimal());
                        row["DEFECTQTYPNL"] = defectQtyPNL;
                    }
                }

                if (_inspectionQty < _defectQtySumPCS)
                {
                    ShowMessage("DefectQtyMoreThanInspQty");//입력된 불량수량보다 검사 수량이 적습니다.
                    grdChildLot.View.SetFocusedRowCellValue(e.Column, DBNull.Value);
                }
            }
            //else if (e.Column.FieldName.Equals("SPECOUTQTY") && e.Value != DBNull.Value)
            //{
            //   // GrdChildLot_CustomSummaryCalculate(sender, );
            //}
        }

        private void GrdChildLot_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                GridSummaryItem item = e.Item as GridSummaryItem;
                if (item.FieldName == "DEFECTRATE")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        //모든 Lot의 전체 수량
                        //decimal allLotQty = Convert.ToDecimal((sender as GridView).Columns["ALLQTYPCS"].SummaryItem.SummaryValue);
                        decimal allLotQty = Convert.ToDecimal((sender as GridView).Columns["INSPECTIONQTY"].SummaryItem.SummaryValue);
                        //모든 Lot의 전체 불량 수량 PCS
                        decimal allLotDefectQty = Convert.ToDecimal((sender as GridView).Columns["SPECOUTQTY"].SummaryItem.SummaryValue);

                        //모든 Lot의 전체 양품 수량 PCS
                        decimal goodPCS = Convert.ToDecimal((sender as GridView).Columns["GOODQTYPCS"].SummaryItem.SummaryValue);

                        //모든 Lot의 전체 양품 수량 PNL
                        decimal goodPNL = Convert.ToDecimal((sender as GridView).Columns["GOODQTYPNL"].SummaryItem.SummaryValue);

                        //모든 Lot의 전체 불량 수량 PNL
                        decimal defectPNL = Convert.ToDecimal((sender as GridView).Columns["DEFECTQTYPNL"].SummaryItem.SummaryValue);

                        //***allLotQty 현재 : Lot 전체수량 // 샘플수량으로 계산 해야 하는지 확인후 수정 필
                        if (allLotQty != 0 && allLotDefectQty != 0)
                        {//lot들의 전체 수량과 불량 수량 합이 0이 아닐때 (PCS로 계산 한 불량률)
                            decimal defectRate = Math.Round((allLotDefectQty / allLotQty * 100).ToSafeDecimal(), 1);
                            e.TotalValue = defectRate;
                        }

                        ucShipmentInfo.SetQty(goodPCS, allLotDefectQty, goodPNL, defectPNL);
                    }
                }
            }
        }

        #endregion grdChildLot 이벤트

        #region grdChildLotDetail 이벤트

        /// <summary>
        /// 불량률 계산 불량명 별
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdChildLotDetail_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                GridSummaryItem item = e.Item as GridSummaryItem;
                if (item.FieldName == "DEFECTRATE")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (_childLotRow != null && _childLotRow["INSPECTIONQTY"] != DBNull.Value && !string.IsNullOrWhiteSpace(_childLotRow["INSPECTIONQTY"].ToString()))
                        {
                            //개별 lot의 불량 PCS 수량 합
                            _defectQtySumPCS = Convert.ToInt16((sender as GridView).Columns["DEFECTQTY"].SummaryItem.SummaryValue);
                            //개별 lot의 불량 PNL 수량 합
                            // _defectQtySumPNL = Convert.ToInt16((sender as GridView).Columns["DEFECTQTYPNL"].SummaryItem.SummaryValue);

                            _defectQtySumPNL = Math.Ceiling(_defectQtySumPCS / _childLotRow["PANELPERQTY"].ToSafeDoubleNaN()).ToSafeInt32();

                            if (_inspectionQty != 0)
                            {//개별 lot의 전체 수량과 개별 lot의 불량 수량 합이 0이 아닐때 (PCS로 계산 한 불량률)
                                decimal defectRate = Math.Round((_defectQtySumPCS.ToSafeDecimal() / _inspectionQty * 100).ToSafeDecimal(), 1);
                                e.TotalValue = defectRate;

                                //2020-01-17 수치 늦게바뀌어 수정
                                //_childLotRow["SPECOUTQTY"] = _defectQtySumPCS;
                                //_childLotRow["DEFECTQTYPNL"] = _defectQtySumPNL;
                                //_childLotRow["DEFECTRATE"] = defectRate+"%";
                                grdChildLot.View.SetFocusedRowCellValue("SPECOUTQTY", _defectQtySumPCS);
                                grdChildLot.View.SetFocusedRowCellValue("DEFECTQTYPNL", _defectQtySumPNL);
                                grdChildLot.View.SetFocusedRowCellValue("DEFECTRATE", defectRate + "%");

                                //2020-01-17 불량코드의 판정결과중 하나라도 NG있으면 전체 lot 결과 NG로 수정 하면서 주석처리
                                //_childLotRow["INSPECTIONRESULT"] = "NG";
                            }

                            //grdChildLot의 양품 수 계산 PCS, PNL
                            int goodQtyPCS = _allQtyPCS.ToSafeInt32() - _defectQtySumPCS;
                            int goodQtyPNL = _allQtyPNL.ToSafeInt32() - _defectQtySumPNL;

                            if (_allQtyPCS != 0)
                            {
                                //2020-01-17 수치 늦게바뀌어 수정
                                //_childLotRow["GOODQTYPCS"] = goodQtyPCS;
                                //_childLotRow["GOODQTYPNL"] = goodQtyPNL<0?0:goodQtyPNL;
                                grdChildLot.View.SetFocusedRowCellValue("GOODQTYPCS", goodQtyPCS);
                                grdChildLot.View.SetFocusedRowCellValue("GOODQTYPNL", goodQtyPNL < 0 ? 0 : goodQtyPNL);
                            }

                            string messageId = null;
                            string finalResult = "";

                            //2020-01-17 불량코드의 판정결과중 하나라도 NG있으면 전체 lot 결과 NG로 수정
                            //2020-03-27 강유라 불량 수량 변경 => 전체 불량 반영 후 전체 결과 판정 필요 => 위치변경
                            int NGCount = GetNGCount(grdChildLotDetail.DataSource as DataTable);

                            if (NGCount > 0)
                            {//DetailRow 중 NG갯수 하나라도 있으면 전체결과 NG
                                finalResult = "NG";
                            }
                            else
                            {//DetailRow 중 NG갯수 하나도 없으면 전체결과 OK
                                finalResult = "OK";

                                //2020-03-27 강유라 불량항목별 판정 후 => 전체 수량으로 판정
                                //전체 LOT AQL 판정
                                if (_aqlOrNcr.Equals("AQL"))
                                {
                                    //AQL기준
                                    finalResult = InspectionHelper.SetQcGradeAndResultAQLType(_childLotRow, _aqlInspectionLevel, _aqlDefectLevel, "ShipmentInspection", _decisionDegree, _childLotRow["ALLQTYPCS"].ToString(), false, out messageId);
                                }
                                else if (_aqlOrNcr.Equals("NCR"))
                                {
                                    //NCR기준
                                    finalResult = InspectionHelper.SetQcGradeAndResultNCRQtyRateType(_childLotRow, "ShipmentInspection", _decisionDegree, false, out messageId);
                                }
                            }

                            _childLotRow["INSPECTIONRESULT"] = finalResult;

                            //2020-03-18 강유라 불량수량 바로 반영 안되는 문제
                            grdChildLot.View.RefreshData();

                            if (messageId != null)
                                ShowMessage(messageId);
                        }
                    }
                }
            }
        }

        private void GrdChildLotDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            #region 불량수량

            if (e.Column.FieldName.Equals("DEFECTQTY") && e.Value != DBNull.Value)
            {
                _childLotRow = grdChildLot.View.GetFocusedDataRow();
                _inspectionQty = _childLotRow["INSPECTIONQTY"].ToSafeInt32();

                _beforeDefectQty = e.Value.ToSafeInt32();

                if (string.IsNullOrWhiteSpace(_childLotRow["INSPECTIONQTY"].ToString()))
                {
                    ShowMessage("NeedToInputSampleQty");//시료량을 먼저 입력해야 합니다.
                    return;
                }

                if (e.Value.ToSafeDecimal() > _inspectionQty)
                {
                    ShowMessage("PcsQtyRangeOver");//검사수량보다 불량수량이 초과하였습니다.

                    grdChildLotDetail.View.SetRowCellValue(e.RowHandle, "DEFECTQTY", 0);

                    return;
                }

                decimal defectSumQty = 0;

                DataTable dt = grdChildLotDetail.DataSource as DataTable;
                foreach (DataRow detailRow in dt.Rows)
                {
                    defectSumQty += detailRow["DEFECTQTY"].ToSafeDecimal();
                    if (_inspectionQty < defectSumQty)
                    {
                        ShowMessage("PcsQtyRangeOver");//검사수량보다 불량수량이 초과하였습니다.
                        grdChildLotDetail.View.SetRowCellValue(e.RowHandle, "DEFECTQTY", 0);

                        return;
                    }
                }

                DataRow row = grdChildLotDetail.View.GetDataRow(e.RowHandle);

                if (_inspectionQty != 0)
                {
                    //grdChildLotDetail 불량율 계산
                    decimal defectRate = Math.Round((e.Value.ToSafeDecimal() / _inspectionQty * 100).ToSafeDecimal(), 1);
                    grdChildLotDetail.View.SetRowCellValue(e.RowHandle, "DEFECTRATE", defectRate + "%");

                    //grdChildLotDetail 불량 PNL 수량 계산
                    decimal defectQtyPNL = Math.Ceiling(e.Value.ToSafeDecimal() / row["PANELPERQTY"].ToSafeDecimal());
                    grdChildLotDetail.View.SetRowCellValue(e.RowHandle, "DEFECTQTYPNL", defectQtyPNL);
                }

                //판정 불량코드 합 -> 불량 코드별 적용 2019-12-04
                string result = "";
                string messageId = null;

                if (_aqlOrNcr.Equals("AQL"))
                {
                    //AQL기준
                    result = InspectionHelper.SetQcGradeAndResultAQLType(row, _aqlInspectionLevel, _aqlDefectLevel, "ShipmentInspection", _decisionDegree, _childLotRow["ALLQTYPCS"].ToString(), true, out messageId);
                }
                else if (_aqlOrNcr.Equals("NCR"))
                {
                    //NCR기준
                    result = InspectionHelper.SetQcGradeAndResultNCRQtyRateType(row, "ShipmentInspection", _decisionDegree, true, out messageId);
                }

                //2020-01-17 불량코드 별 판정 결과 표시
                grdChildLotDetail.View.SetFocusedRowCellValue("INSPECTIONRESULT", result);
            }

            #endregion 불량수량
        }

        #endregion grdChildLotDetail 이벤트

        #region Control 이벤트

        #region 버튼 이벤트

        /// <summary>
        /// LotId 검색 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var buttons = pnlToolbar.Controls.Find<SmartButton>(true);
                try
                {
                    buttons.ForEach(button => button.IsBusy = true);
                    pnlContent.ShowWaitArea();

                    ClearAllData(false);
                    //autoChange = true;
                    autoChange = false; // 유석진 수정-2019-12-10
                    if (txtOriginLotId.Editor.EditValue == null || string.IsNullOrWhiteSpace(txtOriginLotId.Editor.EditValue.ToString()) || string.IsNullOrWhiteSpace(popAreaId.GetValue().ToString()))
                    {
                        // 작업장, LOT No.는 필수 입력 항목입니다.
                        ShowMessage("AreaLotIdIsRequired");
                        //ClearAllData();
                        ClearAllData(false); // 유석진 수정-2019-12-10
                        return;
                    }

                    _lotId = txtOriginLotId.Editor.EditValue.ToString().Trim();

                    //작업장이나 resourceId가 없을때
                    Dictionary<string, object> lrParam = new Dictionary<string, object>();
                    lrParam.Add("LOTID", _lotId);

                    DataTable dt = SqlExecuter.Query("selectAreaResourceByLot", "10001", lrParam);

                    if (dt.Rows.Count < 1)
                    {
                        //존재 하지 않는 Lot No. 입니다.
                        ShowMessage("NotExistLotNo");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(Format.GetFullTrimString(dt.Rows[0]["AREAID"])) || string.IsNullOrWhiteSpace(Format.GetFullTrimString(dt.Rows[0]["RESOURCEID"])))
                    //if (Format.GetFullTrimString(dt.Rows[0]["AREAID"]).Equals("K0000000") || string.IsNullOrWhiteSpace(Format.GetFullTrimString(dt.Rows[0]["RESOURCEID"])))
                    {
                        SelectResourcePopup resourcePopup = new SelectResourcePopup(Format.GetString(dt.Rows[0]["LOTID"]), Format.GetString(dt.Rows[0]["PROCESSSEGMENTID"]), string.Empty);
                        resourcePopup.ShowDialog();

                        if (resourcePopup.DialogResult == DialogResult.OK)
                        {
                            MessageWorker resourceWorker = new MessageWorker("SaveLotResourceId");
                            resourceWorker.SetBody(new MessageBody()
                                {
                                    { "LotId", _lotId },
                                    { "ResourceId", resourcePopup.ResourceId }
                                });

                            resourceWorker.Execute();

                            Editor_KeyDown(sender, e);
                        }
                        else
                        {
                            // 현재 공정에서 사용할 자원을 선택하시기 바랍니다.
                            throw MessageException.Create("SelectResourceForCurrentProcess");
                        }
                    }

                    OpenCheckStayingPopup();

                    _strPlantId = Conditions.GetValues()["P_PLANTID"].ToString();

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    param.Add("PLANTID", _strPlantId);
                    param.Add("AREAID", popAreaId.GetValue());
                    param.Add("LOTID", CommonFunction.changeArgString(_lotId));
                    param.Add("PROCESSSTATE", Constants.Run);
                    param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    //param.Add("MIDDLESEGMENTCLASSID", "7030");

                    ///lot 정보조회
                    DataTable lotInfo = SqlExecuter.Query("SelectLotInfoByProcess", "40001", param);
                    DataRow lotRow;
                    //DataTable lotInfo = SqlExecuter.Query("SelectLotInfoByProcessShipment", "10001", param);

                    if (lotInfo.Rows.Count == 0)
                    {
                        // 조회할 데이터가 없습니다.
                        ShowMessage("NoSelectData");
                        //ClearAllData();
                        ClearAllData(false); // 유석진 수정-2019-12-10

                        txtOriginLotId.Text = "";
                        txtOriginLotId.Focus();
                        //popLotId.EditValue = string.Empty;
                        return;
                    }
                    else
                    {//2020-01-15 강유라
                     //검색한 LOT LOCKING시 메세지(검사대상나오지않음)/ 검색한 LOT LOCKING아닐때 LOCKING아닌 LOT검사대상으로 조회
                        lotRow = lotInfo.Rows[0];

                        if (Format.GetString(lotRow["ISLOCKING"]).Equals("Y"))
                        {
                            //해당 Lot이 Locking 상태입니다. {0}
                            ShowMessage("InLockingState", lotRow["LOTID"].ToString());

                            ClearAllData(false);
                            txtOriginLotId.Text = "";
                            txtOriginLotId.Focus();

                            return;
                        }
                    }

                    ucShipmentInfo.SetInspectorReadOnly(true); // 유석진 수정-2019-12-10
                    grdLot.Enabled = true;
                    //***ucShipmentInfo.Enabled = true;
                    //***grpTab.Enabled = true;

                    _selectedRow = lotInfo.Rows[0];
                    string lotId = lotRow["LOTID"].ToString();
                    string productDefId = lotRow["PRODUCTDEFID"].ToString();
                    string productDefVersion = lotRow["PRODUCTDEFVERSION"].ToString();
                    string processSegmentId = lotRow["PROCESSSEGMENTID"].ToString();
                    _processsegmentName = lotRow["PROCESSSEGMENTNAME"].ToString();

                    grdLot.DataSource = lotInfo;

                    //_strPlantId = lotRow["PLANTID"].ToString();
                    //_nowAreaId = lotRow["AREAID"].ToString();

                    string stepType = lotRow["STEPTYPE"].ToString();

                    // 인계 작업장
                    if (!stepType.Split(',').Contains("WaitForSend"))
                    {//자동 인계 - 인계작업장 선택
                        _isAutoTransit = true;
                        ucShipmentInfo.SetTransitAreaList(lotRow);
                        ucShipmentInfo.SetReadOnly(true);
                    }
                    else
                    {//수동인계 - 인계작업장 선택 안함 - readOnly
                        _isAutoTransit = false;
                        ucShipmentInfo.SetReadOnly(false);
                    }

                    //품목별 검사자 등급을 미리 조회
                    Dictionary<string, object> value = new Dictionary<string, object>
                    {
                        {"RESOURCETYPE","Product"},
                        {"INSPECTIONTYPE","ShipmentInspection"},
                        {"ENTERPRISEID",UserInfo.Current.Enterprise},
                        {"PLANTID",_strPlantId}
                    };
                    DataTable _InspectorDegree = SqlExecuter.Query("GetInspectorDegreeByInspType", "10001", value);

                    //품목검사등급 할당
                    //2020-01-07 강유라, 품질기준 정보 공통 기준 적용
                    //품목에 해당하는 기준 먼저 찾고, 없다면 공통기준 적용
                    var selected = _InspectorDegree.AsEnumerable().
                         Where(r => r["RESOURCEID"].ToString().Equals(productDefId)
                           && r["RESOURCEVERSION"].ToString().Equals("*")) // 2020.04.01-출하검사 기준정보 품목 Rev 대표(*) 관리로 인한 수정
                                                                           //&& r["RESOURCEVERSION"].ToString().Equals(productDefVersion))
                         .ToList();

                    if (selected.Count != 0)
                    {//품목에 해당하는 기준이 있을 경우
                        //DataRow standardRow = selected.CopyToDataTable().Rows[0];
                        _standardRow = selected.CopyToDataTable().Rows[0];
                        //SetInspStandard(_standardRow); 2019-12-10 주석
                        //ucShipmentInfo.SetProductInspectionGrade(_standardRow["INSPECTORDEGREE"].ToString());
                    }
                    else
                    {//품목에 해당하는 기준이 없을 경우 => 공통기준 적용
                        var commonStandard = _InspectorDegree.AsEnumerable().
                         Where(r => r["RESOURCEID"].ToString().Equals("*")
                         && r["RESOURCEVERSION"].ToString().Equals("*"))
                         .ToList();

                        if (commonStandard.Count > 0)
                        {
                            _standardRow = commonStandard.CopyToDataTable().Rows[0];
                        }
                        else
                        {
                            // 판정기준이 없습니다.
                            ShowMessage("NoStandardData");
                            ClearAllData();

                            txtOriginLotId.Text = "";
                            txtOriginLotId.Focus();
                            return;
                        }
                    }

                    // 검사자 팝업 초기화
                    ucShipmentInfo.SetInspectorPopup(_standardRow, lotRow); // 2020-01-07 강유라

                    //판정기준 할당
                    SetInspStandard(_standardRow);
                    //분할 Lot 정보조회
                    getShipmentInspectionLotList();

                    //변경이력 정보조회
                    Dictionary<string, object> dicParam = new Dictionary<string, object>()
                    {
                        {"PLANTID",Format.GetString(lotRow["PLANTID"])},
                        {"PRODUCTDEFID",Format.GetString(lotRow["PRODUCTDEFID"])}
                    };

                    grdProductHist.DataSource = SqlExecuter.Query("SelectProductChangeHistoryTab", "10001", dicParam);

                    //주자 정보 조회
                    grdWeekNo.DataSource = SqlExecuter.Query("SelectWeekInfoByLotId", "10001", param);
                    SearchLotInfoByWeekNo(grdWeekNo.View.GetFocusedDataRow());
                    //메세지 조회
                }
                finally
                {
                    pnlContent.CloseWaitArea();
                    buttons.ForEach(button => button.IsBusy = false);
                }
            }
        }

        /// <summary>
        /// 임시저장 클릭시 grdChildLotDetail 데이터를
        /// 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTempSave_Click(object sender, EventArgs e)
        {
            //분할 Lot 그리드 FocusedDataRow
            DataRow childLotRow = grdChildLot.View.GetFocusedDataRow();
            if (childLotRow == null) return;
            //임시 저장 할 그리드의 데이터
            DataTable childLotDetailDt = grdChildLotDetail.DataSource as DataTable;
            bool hasDefectCode = true;

            if (string.IsNullOrWhiteSpace(_childLotRow["INSPECTIONQTY"].ToString()))
            {
                ShowMessage("NeedToInputSampleQty");//시료량을 먼저 입력해야 합니다.
                return;
            }

            foreach (DataRow row in childLotDetailDt.Rows)
            {
                if (!row["ISDELETE"].ToString().Equals("Y"))
                {
                    //DEFECTCODE가 입력 되지 않거나 이미지가 하나도 등록 되지않은 경우
                    if (string.IsNullOrWhiteSpace(row["DEFECTCODE"].ToString()) ||
                    string.IsNullOrWhiteSpace(row["DEFECTQTY"].ToString()) ||
                    Format.GetInteger(row["DEFECTQTY"]) == 0 ||
                    (string.IsNullOrWhiteSpace(row["FILEFULLPATH1"].ToString()) && string.IsNullOrWhiteSpace(row["FILEFULLPATH2"].ToString())
                    && string.IsNullOrWhiteSpace(row["FILEFULLPATH3"].ToString())))
                    {
                        if (string.IsNullOrWhiteSpace(Format.GetString(row["DEFECTCODE"])))
                        {
                            ShowMessage("DefectCodeIsRequired");//불량코드를 먼저 입력하세요.
                            hasDefectCode = false;
                            break;
                        }
                        else if (string.IsNullOrWhiteSpace(Format.GetString(row["DEFECTQTY"])))
                        {
                            ShowMessage("DefectQtyRequired");//불량수량을 입력해야 합니다.
                            hasDefectCode = false;
                            break;
                        }
                        else if (Format.GetInteger(row["DEFECTQTY"]) == 0)
                        {
                            ShowMessage("DefectQtyInputZero");//불량수량은 0을 입력할수 없습니다.
                            hasDefectCode = false;
                            break;
                        }
                        else
                        {
                            if (UserInfo.Current.Enterprise.Equals("INTERFLEX"))
                            {
                                ShowMessage("DefectCodeAndLeastOneImageRequired");//불량코드와 적어도 하나의 이미지를 입력해야 합니다.
                                hasDefectCode = false;
                                break;
                            }/*//2020-03-03 강유라 영풍만 이미지 저장 필수 풀어달라함
                            else
                            {
                                if (Format.GetString(row["INSPECTIONRESULT"]).Equals("NG"))
                                {
                                    ShowMessage("DefectCodeAndLeastOneImageRequired");//불량코드와 적어도 하나의 이미지를 입력해야 합니다.
                                    hasDefectCode = false;
                                    break;
                                }
                            }*/
                        }
                    }

                    if (row["DEFECTQTY"].ToString().Equals("0"))
                    {
                        ShowMessage("DefectQtyInputZero");//불량수량은 0을 입력할수 없습니다.
                        hasDefectCode = false;
                        break;
                    }
                }
            }

            if (hasDefectCode == false)
                return;

            if (childLotDetailDt.Rows.Count > 0)
            {
                if (_tempSave == null)
                {//임시테이블 DataTable null 일때 -> grdChildLotDetail DataSource
                    _tempSave = childLotDetailDt.Clone();

                    //모든 ROW 저장
                    AddAllRowTempSaveTable(childLotDetailDt);
                }
                else
                {
                    var tempSaved = _tempSave.Rows.Cast<DataRow>()
                            .Where(r => r["RESOURCEID"].ToString().Equals(_childLotRow["RESOURCEID"].ToString()) &&
                           r["DEGREE"].ToString().Equals(_childLotRow["DEGREE"].ToString()))
                           .ToList();

                    if (tempSaved.Count == 0)
                    {
                        //모든 ROW 저장
                        AddAllRowTempSaveTable(childLotDetailDt);
                    }
                    else
                    {
                        AddAllRowTempSaveAfterDelete(childLotDetailDt);
                    }

                    SearchDetailByLotId();
                    /*
                        foreach (DataRow childDetailRow in childLotDetailDt.Rows)
                        {
                            //임시저장 테이블에 저장하려는 데이터 있는지 체크
                            var alreadyTempSaved = _tempSave.Rows.Cast<DataRow>()
                                .Where(r => r["RESOURCEID"].ToString().Equals(childDetailRow["RESOURCEID"].ToString()) &&
                               r["DEGREE"].ToString().Equals(childDetailRow["DEGREE"].ToString())&&
                               r["DEFECTCODE"].ToString().Equals(childDetailRow["DEFECTCODE"].ToString())&&
                               r["QCSEGMENTID"].ToString().Equals(childDetailRow["QCSEGMENTID"].ToString()))
                               .ToList();

                            if (alreadyTempSaved.Count == 0)
                            {//없을때
                                AddRowTempSaveTable(childDetailRow);
                            }
                            else
                            {//있을때
                                if (childDetailRow["ISDELETE"].Equals("Y"))
                                {//row 상태 delete
                                    DeleteTempSaveTable(childDetailRow);
                                }
                                else
                                {
                                    //DataTable toUpdateData = alreadyTempSaved.CopyToDataTable();
                                    //DataRow toUpdateRow = toUpdateData.Rows[0];
                                    ////기존 있는 Row에 Data update
                                    //UpdateTempSaveTable(childDetailRow);

                                    DeleteTempSaveTable(childDetailRow);
                                    AddRowTempSaveTable(childDetailRow);
                                }
                            }
                        }*/
                }
            }
            else
            {//현재 그리드에 데이터 없을 때
                if (_tempSave != null && _tempSave.Rows.Count > 0)
                {//임시저장 데이터에 해당 LotID의 데이터있는지 확인
                    var tempCount = _tempSave.AsEnumerable().
                        Where(r => r["RESOURCEID"].ToString().Equals(_childLotRow["RESOURCEID"].ToString())
                        && r["DEGREE"].ToString().Equals(_childLotRow["DEGREE"].ToString()))
                        .ToList().Count;

                    //임시저장한 데이터 있을 때 -> 임시저장한 데이터 삭제
                    if (tempCount > 0)
                    {
                        DeleteTempSaveByLotId(_childLotRow);
                    }
                    else
                    {
                        MSGBox.Show(MessageBoxType.Information, "NoDataToTempSave");//임시 저장 할 데이터가 없습니다.
                    }
                }
                else
                {
                    MSGBox.Show(MessageBoxType.Information, "NoDataToTempSave");//임시 저장 할 데이터가 없습니다.
                }
            }
        }

        #endregion 버튼 이벤트

        #region popup 이벤트

        /// <summary>
        /// LOT Popup에서 x버튼 눌렀을때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopupLotId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind.ToString() == "Clear")
            {
                txtOriginLotId.Text = string.Empty;
            }
        }

        /// <summary>
        /// AREA Popup에서 x버튼 눌렀을때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopAreaId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind.ToString() == "Clear")
            {
                _areaId = "";
                //popLotId.SelectPopupCondition = Initialize_LotPopup();
            }
        }

        #endregion popup 이벤트

        #endregion Control 이벤트

        #endregion Event

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.BTN_SAVE
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            // base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
        }

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
            {
                Btn_SaveClick(btn.Text);
            }
            else if (btn.Name.ToString().Equals("Initialization"))
            {
                ClearAllData(true, true);
            }
        }

        #endregion 툴바

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            /*-------조회조건 검색기능 사용하지않음
            var values = Conditions.GetValues();

            DataTable dtCodeClass = await ProcedureAsync("usp_com_selectCodeClass", values);

            if (dtCodeClass.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            // grdList.DataSource = dtCodeClass;
            */
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        }

        #endregion 검색

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
            /*--------유효성 검사 없음
            // TODO : 유효성 로직 변경
            //grdList.View.CheckValidation();

            // DataTable changed = grdList.GetChangedRows();
            /*
             if (changed.Rows.Count == 0)
             {
                 // 저장할 데이터가 존재하지 않습니다.
                 throw MessageException.Create("NoSaveData");
             }*/
        }

        #endregion 유효성 검사

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 팝업을 초기화 하는 함수
        /// </summary>
        private void InitializePopup()
        {
            //popLotId.SelectPopupCondition = Initialize_LotPopup();
            popAreaId.SelectPopupCondition = Initialize_AreaPopup();
        }

        /// <summary>
        /// 모든 컨트롤의 데이터를 초기화하는 함수
        /// </summary>
        private void ClearAllData(bool isAllClear = true, bool inspectorClear = false)
        {
            grdLot.ClearData();
            ucShipmentInfo.ClearData(inspectorClear);

            if (isAllClear == true)
            {
                txtOriginLotId.Editor.EditValue = string.Empty;
                popAreaId.EditValue = string.Empty;
            }

            txtChildLotId.Editor.EditValue = string.Empty;
            txtNGCount.Editor.EditValue = string.Empty;
            txtFinishInspInspector.Editor.EditValue = string.Empty;

            grdChildLot.DataSource = null;
            grdChildLotDetail.DataSource = CreateDataSource();

            grdWeekNo.DataSource = null;
            grdWeekChildLotNo.DataSource = null;

            grdProductHist.DataSource = null;

            grdLotList.DataSource = null;
            grdProcessSegmentList.DataSource = null;
            grdCreatorList.DataSource = null;

            ucShipmentInspDefect.ClearUserControlGrd();

            memoBox.Rtf = string.Empty;

            _tempSave = null;
            _selectedDt = null;
            _messageDt = null;
            _standardRow = null;
            _toSaveMessage.Clear();

            ucShipmentInfo.SetReadOnly(true);
            ucShipmentInfo.ClearTransitAreaList();
            grdLot.Enabled = false;

            _allQtyPCS = 0; // Lot별 PCS 수량
            _allQtyPNL = 0; // Lot별 PNL 수량
            _inspectionQty = 0;//Lot별 샘플 수량

            _defectQtySumPCS = 0; //Lot별 PCS 불량 수량 (불량코드 별 PCS 불량 수량 합 )
            _defectQtySumPNL = 0; //Lot별 PNL 불량 수량 (불량코드 별 PNL 불량 수량 합 )
            _beforeDefectQty = 0;
            //_aqlOrNcr = "";

            //_childLotRow = null;

            picBox1.Image = null;
            picBox2.Image = null;
            picBox3.Image = null;
            //***ucShipmentInfo.Enabled = false;
            //***grpTab.Enabled = false;
            autoChange = false;
            _strPlantId = "";

            _reworkProcessDefId = "";
            _reworkProcessDefVersion = "";
            _reworkPathId = "";
            _reworkResourceId = "";
            _reworkAreaId = "";
            _reworkProcesssegmentId = "";
            _reworkProcesssegmentVersion = "";
            _reworkUserSequence = "";
        }

        /// <summary>
        /// 출하검사 대상 LOT 조회 :: RootLotID 기준
        /// </summary>
        private void getShipmentInspectionLotList()
        {
            if (string.IsNullOrWhiteSpace(txtOriginLotId.Text.Trim()))
                return;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("PLANTID", _strPlantId);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", this.txtOriginLotId.Text.Trim());
            param.Add("PROCESSSTATE", Constants.Run);

            _selectedDt = SqlExecuter.Query("SelectLotListForShipmentInspection", "10001", param);

            if (_selectedDt.Rows.Count < 1)
            {
                //ClearAllData();
                ClearAllData(false); // 유석진 수정-2019-12-10

                txtOriginLotId.Text = "";
                txtOriginLotId.Focus();

                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                grdChildLot.DataSource = null;
                grdLotList.DataSource = null;
                ucShipmentInspDefect.ClearUserControlGrd();
            }
            else
            {
                grdChildLot.DataSource = null;
                if (_aqlOrNcr.Equals("AQL"))
                {
                    grdChildLot.DataSource = SetAQLInspectionQTY(_selectedDt);
                }
                else if (_aqlOrNcr.Equals("NCR"))
                {
                    grdChildLot.DataSource = SetNCRInspectionQTY(_selectedDt); // 유석진 수정-2019-12-10
                }

                _childLotRow = grdChildLot.View.GetFocusedDataRow();

                //2020-03-18 강유라 첫번째 row의 수량 바인딩
                _inspectionQty = _childLotRow["INSPECTIONQTY"].ToSafeInt32();
                _allQtyPCS = _childLotRow["ALLQTYPCS"].ToSafeInt32();
                _allQtyPNL = _childLotRow["ALLQTYPNL"].ToSafeInt32();

                SetChildLotInfo(_childLotRow);

                grdLotList.DataSource = _selectedDt.Copy();
                ucShipmentInspDefect.CurrentDataRow = _selectedDt.Rows[0];
                ucShipmentInspDefect.SetLotIdAndData(_selectedDt.Copy());
                _lotRow = grdLotList.View.GetFocusedDataRow();
                SearchProcessSegmentList(_lotRow);
            }
        }

        /// <summary>
        /// grdChildLotDetail DataSource를 생성하여 바인딩하는 함수
        /// 조회성 없기 때문에 DataTable 생성 후 바인딩
        /// </summary>
        private DataTable CreateDataSource()
        {
            DataTable detailDs = new DataTable();
            detailDs.Columns.Add(new DataColumn("RESOURCEID", typeof(string)));
            detailDs.Columns.Add(new DataColumn("DEGREE", typeof(string)));
            detailDs.Columns.Add(new DataColumn("PROCESSRELNO", typeof(string)));
            detailDs.Columns.Add(new DataColumn("DEFECTCODENAME", typeof(string)));
            detailDs.Columns.Add(new DataColumn("DEFECTCODE", typeof(string)));
            detailDs.Columns.Add(new DataColumn("QCSEGMENTID", typeof(string)));
            detailDs.Columns.Add(new DataColumn("QCSEGMENTNAME", typeof(string)));
            detailDs.Columns.Add(new DataColumn("PANELPERQTY", typeof(string)));
            detailDs.Columns.Add(new DataColumn("DEFECTQTY", typeof(string)));
            detailDs.Columns.Add(new DataColumn("DEFECTQTYPNL", typeof(string)));
            detailDs.Columns.Add(new DataColumn("DEFECTRATE", typeof(string)));
            detailDs.Columns.Add(new DataColumn("SAMPLEQTY", typeof(float)));
            detailDs.Columns.Add(new DataColumn("INSPECTIONQTY", typeof(float)));
            detailDs.Columns.Add(new DataColumn("INSPECTIONRESULT", typeof(string)));
            detailDs.Columns.Add(new DataColumn("ENTERPRISEID", typeof(string)));
            detailDs.Columns.Add(new DataColumn("PLANTID", typeof(string)));
            detailDs.Columns.Add(new DataColumn("ISDELETE", typeof(string)));

            detailDs.Columns.Add(new DataColumn("REASONCONSUMABLEDEFID", typeof(string)));
            detailDs.Columns.Add(new DataColumn("REASONCONSUMABLEDEFVERSION", typeof(string)));
            detailDs.Columns.Add(new DataColumn("REASONCONSUMABLELOTID", typeof(string)));
            detailDs.Columns.Add(new DataColumn("REASONSEGMENTID", typeof(string)));
            detailDs.Columns.Add(new DataColumn("REASONAREAID", typeof(string)));

            detailDs.Columns.Add(new DataColumn("QCGRADE", typeof(string)));
            detailDs.Columns.Add(new DataColumn("PRIORITY", typeof(string)));

            //파일 정보를 담을 컬럼
            detailDs.Columns.Add(new DataColumn("FILEINSPECTIONTYPE", typeof(string)));
            detailDs.Columns.Add(new DataColumn("FILERESOURCEID", typeof(string)));

            detailDs.Columns.Add(new DataColumn("FILENAME1", typeof(string)));
            detailDs.Columns.Add(new DataColumn("FILEFULLPATH1", typeof(string)));
            detailDs.Columns.Add(new DataColumn("FILEID1", typeof(string)));
            detailDs.Columns.Add(new DataColumn("FILEDATA1", typeof(byte[])));
            detailDs.Columns.Add(new DataColumn("FILECOMMENTS1", typeof(string)));
            detailDs.Columns.Add(new DataColumn("FILESIZE1", typeof(int)));
            detailDs.Columns.Add(new DataColumn("FILEEXT1", typeof(string)));

            detailDs.Columns.Add(new DataColumn("FILENAME2", typeof(string)));
            detailDs.Columns.Add(new DataColumn("FILEFULLPATH2", typeof(string)));
            detailDs.Columns.Add(new DataColumn("FILEID2", typeof(string)));
            detailDs.Columns.Add(new DataColumn("FILEDATA2", typeof(byte[])));
            detailDs.Columns.Add(new DataColumn("FILECOMMENTS2", typeof(string)));
            detailDs.Columns.Add(new DataColumn("FILESIZE2", typeof(int)));
            detailDs.Columns.Add(new DataColumn("FILEEXT2", typeof(string)));

            detailDs.Columns.Add(new DataColumn("FILENAME3", typeof(string)));
            detailDs.Columns.Add(new DataColumn("FILEFULLPATH3", typeof(string)));
            detailDs.Columns.Add(new DataColumn("FILEID3", typeof(string)));
            detailDs.Columns.Add(new DataColumn("FILEDATA3", typeof(byte[])));
            detailDs.Columns.Add(new DataColumn("FILECOMMENTS3", typeof(string)));
            detailDs.Columns.Add(new DataColumn("FILESIZE3", typeof(int)));
            detailDs.Columns.Add(new DataColumn("FILEEXT3", typeof(string)));

            return detailDs;
        }

        /// <summary>
        /// grdChildLotDetail 그리드에 기본적으로 넣어줄 값을 할당 하는 함수
        /// </summary>
        /// <param name="targetRow"></param>
        private void SetStandardData(DataRow targetRow)
        {
            if (_childLotRow != null)
            {
                targetRow["RESOURCEID"] = _childLotRow["RESOURCEID"];
                targetRow["DEGREE"] = _childLotRow["DEGREE"];
                targetRow["PROCESSRELNO"] = _childLotRow["PROCESSRELNO"];
                targetRow["INSPECTIONQTY"] = _childLotRow["INSPECTIONQTY"];
                //targetRow["SAMPLEQTY"] = _childLotRow["SAMPLEQTY"];
                targetRow["PANELPERQTY"] = _childLotRow["PANELPERQTY"];
                //2020-01-17 불량 코드 별 판정결과 보이게
                //targetRow["INSPECTIONRESULT"] = "NG";
                targetRow["INSPECTIONRESULT"] = string.Empty;
                targetRow["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
                targetRow["PLANTID"] = _childLotRow["PLANTID"];
            }
        }

        /// <summary>
        /// 임시저장 Dt에 Row를 Add하는 함수
        /// </summary>
        /// <param name="toSaveRow"></param>
        private void AddRowTempSaveTable(DataRow toSaveRow)
        {
            DataRow addRow = _tempSave.NewRow();
            addRow.ItemArray = toSaveRow.ItemArray.Clone() as object[];
            //addRow["_STATE_"] = "added";
            _tempSave.Rows.Add(addRow);
        }

        /// <summary>
        /// 임시저장 Dt에 모든Row를 Add하는 함수
        /// </summary>
        /// <param name="toSaveRow"></param>
        private void AddAllRowTempSaveTable(DataTable toSaveDt)
        {
            foreach (DataRow toSaveRow in toSaveDt.Rows)
            {
                if (!toSaveRow["ISDELETE"].ToString().Equals("Y"))
                {
                    DataRow addRow = _tempSave.NewRow();
                    addRow.ItemArray = toSaveRow.ItemArray.Clone() as object[];
                    _tempSave.Rows.Add(addRow);
                }
            }
        }

        /// <summary>
        /// grd에 데이터 없을때 임시저장 버튼을 눌렀을때 (임시저장한 해당Lot의 데이터 삭제)
        /// </summary>
        /// <param name="sourceRow"></param>
        private void DeleteTempSaveByLotId(DataRow sourceRow)
        {
            string expression = "RESOURCEID=" + "'" + sourceRow["RESOURCEID"].ToString() + "'" + " AND DEGREE =" + "'" + sourceRow["DEGREE"].ToString() + "'";
            DataRow[] rows = _tempSave.Select(expression);

            foreach (DataRow deleteRow in rows)
            {
                _tempSave.Rows.Remove(deleteRow);
            }

            _tempSave.AcceptChanges();
        }

        /*
        /// <summary>
        /// 임시저장 Dt에 Row를 Update하는 함수
        /// </summary>
        /// <param name="sourceRow"></param>
        private void UpdateTempSaveTable(DataRow sourceRow)
        {
            string expression = "RESOURCEID=" + "'" +sourceRow["RESOURCEID"].ToString() + "'"+ " AND DEGREE =" +"'" + sourceRow["DEGREE"].ToString() + "'" +
                 " AND DEFECTCODE=" + "'" + sourceRow["DEFECTCODE"].ToString() + "'" ;
            DataRow[] rows = _tempSave.Select(expression);
            DataRow targetRow = rows[0];

            targetRow["DEFECTQTY"] = sourceRow["DEFECTQTY"];
            targetRow["INSPECTIONRESULT"] = sourceRow["INSPECTIONRESULT"];

            targetRow["FILENAME1"] = sourceRow["FILENAME1"];
            targetRow["FILEDATA1"] = sourceRow["FILEDATA1"];
            targetRow["FILECOMMENTS1"] = sourceRow["FILECOMMENTS1"];
            targetRow["FILESIZE1"] = sourceRow["FILESIZE1"];
            targetRow["FILEEXT1"] = sourceRow["FILEEXT1"];

            targetRow["FILENAME2"] = sourceRow["FILENAME2"];
            targetRow["FILEDATA2"] = sourceRow["FILEDATA2"];
            targetRow["FILECOMMENTS2"] = sourceRow["FILECOMMENTS2"];
            targetRow["FILESIZE2"] = sourceRow["FILESIZE2"];
            targetRow["FILEEXT2"] = sourceRow["FILEEXT2"];

            targetRow["FILENAME3"] = sourceRow["FILENAME3"];
            targetRow["FILEDATA3"] = sourceRow["FILEDATA3"];
            targetRow["FILECOMMENTS3"] = sourceRow["FILECOMMENTS3"];
            targetRow["FILESIZE3"] = sourceRow["FILESIZE3"];
            targetRow["FILEEXT3"] = sourceRow["FILEEXT3"];

            _tempSave.AcceptChanges();
        }
        */
        /*
        /// <summary>
        /// 임시저장 Dt에 Row를 Delete하는 함수
        /// </summary>
        /// <param name="toSaveRow"></param>
        private void DeleteTempSaveTable(DataRow sourceRow)
        {
            string expression = "RESOURCEID=" + "'" + sourceRow["RESOURCEID"].ToString() + "'" + " AND DEGREE =" + "'" + sourceRow["DEGREE"].ToString() + "'" +
                 " AND DEFECTCODE=" + "'" + sourceRow["DEFECTCODE"].ToString() + "'";
            DataRow[] rows = _tempSave.Select(expression);
            DataRow deleteRow = rows[0];

            _tempSave.Rows.Remove(deleteRow);

            _tempSave.AcceptChanges();
        }
        */

        /// <summary>
        /// grdWeekNo의 포커스 Row가 바뀔때 WeekNo를 파라미터로 Lot조회
        /// </summary>
        /// <param name="weekRow"></param>
        private void SearchLotInfoByWeekNo(DataRow weekRow)
        {
            if (weekRow != null)
            {
                Dictionary<string, object> param = new Dictionary<string, object>()
                {
                    {"WEEK", weekRow["WEEK"]},
                    {"PRODUCTDEFID", weekRow["PRODUCTDEFID"]},
                    {"PRODUCTDEFVERSION", weekRow["PRODUCTDEFVERSION"]},
                    {"PROCESSSEGMENTID", weekRow["PROCESSSEGMENTID"]},
                    {"PROCESSSEGMENTVERSION", weekRow["PROCESSSEGMENTVERSION"]},
                    {"ENTERPRISEID",Framework.UserInfo.Current.Enterprise },
                    {"PLANTID",_strPlantId },
                    {"LANGUAGETYPE",Framework.UserInfo.Current.LanguageType },
                };

                grdWeekChildLotNo.DataSource = SqlExecuter.Query("SelectLotQtyInfoByWeek", "10001", param);
            }
        }

        /// <summary>
        /// LotList에 선택한 Lot의 메세지가 등록된 공정 조회함수
        /// </summary>
        /// <param name="row"></param>
        private void SearchProcessSegmentList(DataRow row)
        {
            if (row == null) return;

            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"LANGUAGETYPE",Framework.UserInfo.Current.LanguageType },
                {"ENTERPRISE",Framework.UserInfo.Current.Enterprise},
                {"PLANT",_strPlantId},
                {"LOTID",row["RESOURCEID"] },
                {"PRODUCTDEFID" ,row["PRODUCTDEFID"]},
                {"PRODUCTDEFVERSION",row["PRODUCTDEFVERSION"] }
            };

            _messageDt = SqlExecuter.Query("SelectLotMessageShipmentInsp", "10001", values);

            var processSegmentList = _messageDt.AsEnumerable()
                .Where(r => r["RESOURCEID"].ToString().Equals(row["RESOURCEID"]) && r["PRODUCTDEFID"].ToString().Equals(row["PRODUCTDEFID"]) && r["PRODUCTDEFVERSION"].ToString().Equals(row["PRODUCTDEFVERSION"]))
                .Select(r => new { PROCESSSEGMENTID = r["PROCESSSEGMENTID"], PROCESSSEGMENTVERSION = r["PROCESSSEGMENTVERSION"], PROCESSSEGMENTNAME = r["PROCESSSEGMENTNAME"] })
                .Distinct().ToList();

            DataTable bindingDt = ToDataTable(processSegmentList);

            if (bindingDt.Rows.Count < 1)
            {
                grdProcessSegmentList.DataSource = null;
                grdCreatorList.DataSource = null;
            }
            else
            {
                grdProcessSegmentList.DataSource = null;
                grdProcessSegmentList.DataSource = bindingDt;

                _lotRow = grdLotList.View.GetFocusedDataRow();
                _processRow = grdProcessSegmentList.View.GetFocusedDataRow();
                SearchCreatorList(_processRow, _lotRow);
            }
        }

        /// <summary>
        /// grdProcessSegmentList에서 선택한 공정의 메세지를 등록한 등록자 조회함수
        /// </summary>
        /// <param name="processRow"></param>
        private void SearchCreatorList(DataRow processRow, DataRow lotRow)
        {
            if (processRow == null || lotRow == null) return;

            var creatorList = _messageDt.AsEnumerable().
                OrderBy(r => r["SEQUENCE"])
                .Where(r => r["PROCESSSEGMENTID"].ToString().Equals(processRow["PROCESSSEGMENTID"]) && r["PROCESSSEGMENTVERSION"].ToString().Equals(processRow["PROCESSSEGMENTVERSION"])
                && r["PRODUCTDEFID"].ToString().Equals(lotRow["PRODUCTDEFID"]) && r["PRODUCTDEFVERSION"].ToString().Equals(lotRow["PRODUCTDEFVERSION"])
                && r["RESOURCEID"].ToString().Equals(lotRow["RESOURCEID"]))
                .Select(r => new { CREATOR = r["CREATOR"], SEQUENCE = r["SEQUENCE"], CREATORNAME = r["CREATORNAME"], })
                .ToList();

            DataTable creatorDt = ToDataTable(creatorList);

            if (creatorDt.Rows.Count < 1)
            {
                grdCreatorList.DataSource = null;
                txtTitle.Text = string.Empty;
                memoBox.Text = string.Empty;
            }
            else
            {
                grdCreatorList.DataSource = null;
                grdCreatorList.DataSource = creatorDt;

                _lotRow = grdLotList.View.GetFocusedDataRow();
                _processRow = grdProcessSegmentList.View.GetFocusedDataRow();
                _creatorRow = grdCreatorList.View.GetFocusedDataRow();
                SearchMessageList(_creatorRow, _processRow, _lotRow);
            }
        }

        /// <summary>
        /// grdCreatorList 에서 선택한 공정의 메세지를 등록한 등록자 조회함수
        /// </summary>
        /// <param name="creatorRow"></param>
        private void SearchMessageList(DataRow creatorRow, DataRow processRow, DataRow lotRow)
        {
            if (creatorRow == null || processRow == null || lotRow == null) return;

            var message = _messageDt.AsEnumerable()
                .Where(r => r["PROCESSSEGMENTID"].ToString().Equals(processRow["PROCESSSEGMENTID"]) && r["PROCESSSEGMENTVERSION"].ToString().Equals(processRow["PROCESSSEGMENTVERSION"])
                && r["PRODUCTDEFID"].ToString().Equals(lotRow["PRODUCTDEFID"]) && r["PRODUCTDEFVERSION"].ToString().Equals(lotRow["PRODUCTDEFVERSION"])
                && r["RESOURCEID"].ToString().Equals(lotRow["RESOURCEID"]) && r["CREATOR"].ToString().Equals(creatorRow["CREATOR"])
                && Convert.ToInt32(r["SEQUENCE"]) == Convert.ToInt32(creatorRow["SEQUENCE"]))
                .Select(r => new { TITLE = r["TITLE"], MESSAGE = r["MESSAGE"] })
                .ToList();

            DataTable messageDt = ToDataTable(message);

            if (messageDt.Rows.Count < 1)
            {
                txtTitle.Text = string.Empty;
                memoBox.Rtf = string.Empty;
            }
            else
            {
                txtTitle.Text = messageDt.Rows[0]["TITLE"].ToString();
                memoBox.Rtf = messageDt.Rows[0]["MESSAGE"].ToString();
            }
        }

        /// <summary>
        /// List를 dataTable로 반환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IList<T> list)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in list)
            {
                for (int i = 0; i < values.Length; i++)
                    values[i] = props[i].GetValue(item) ?? DBNull.Value;
                table.Rows.Add(values);
            }
            return table;
        }

        /// <summary>
        /// 메세지를 저장 할 테이블을 만드는 함수
        /// </summary>
        private void CreateTableToSaveMessage()
        {
            _toSaveMessage = new DataTable();
            _toSaveMessage.Columns.Add(new DataColumn("RESOURCEID", typeof(string)));
            _toSaveMessage.Columns.Add(new DataColumn("TITLE", typeof(string)));
            _toSaveMessage.Columns.Add(new DataColumn("MESSAGE", typeof(string)));
        }

        /// <summary>
        /// 저장할 메세지 테이블을 생성하는 함수
        /// </summary>
        /// <param name="checkedRows"></param>
        /// <param name="title"></param>
        /// <param name="contents"></param>
        private void SetDataTableSaveMessage(DataTable checkedRows, string title, string contents)
        {
            foreach (DataRow checkedRow in checkedRows.Rows)
            {
                DataRow newRow = _toSaveMessage.NewRow();
                newRow["RESOURCEID"] = checkedRow["RESOURCEID"];
                newRow["TITLE"] = title;
                newRow["MESSAGE"] = contents;

                _toSaveMessage.Rows.Add(newRow);
            }
        }

        /// <summary>
        /// 기본정보 세팅 함수
        /// </summary>
        /// <param name="row"></param>
        private void SetChildLotInfo(DataRow row)
        {
            if (row == null) return;
            txtChildLotId.Editor.EditValue = row["RESOURCEID"];
            txtNGCount.Editor.EditValue = row["DEGREE"].ToNullOrInt16() - 1;
            txtFinishInspInspector.Editor.EditValue = row["FINALINSPECTORNAME"];
        }

        /// <summary>
        /// Detail 정보가 없을때 Lot Row 값 없애는 함수
        /// </summary>
        /// <param name="row"></param>
        private void ResetRow(DataRow row)
        {
            if (_childLotRow != null && (row["INSPECTIONQTY"] == DBNull.Value || string.IsNullOrWhiteSpace(_childLotRow["INSPECTIONQTY"].ToString())
                || _childLotRow["INSPECTIONQTY"].ToString().Equals("0")))
            {
                //row["GOODQTYPNL"] = row["ALLQTYPNL"];
                //row["GOODQTYPCS"] = row["ALLQTYPCS"];

                //row["DEFECTQTYPNL"] = 0;
                //row["SPECOUTQTY"] = 0;
                // row["DEFECTRATE"] = "0%";

                //row["INSPECTIONQTY"] = DBNull.Value;
                row["INSPECTIONRESULT"] = "OK";
                _childLotRow.AcceptChanges();
            }
        }

        /// <summary>
        /// AQL/NCR기준인지 찾는함수
        /// </summary>
        /// <param name="row"></param>
        private void SetInspStandard(DataRow row)
        {
            //2019-12-10 콤보 추가
            _aqlOrNcr = cboStandardType.Editor.EditValue.ToString();

            if (row == null)
                return;

            if (_aqlOrNcr.Equals("AQL"))
            {
                if (string.IsNullOrWhiteSpace(row["AQLINSPECTIONLEVEL"].ToString())
                    || string.IsNullOrWhiteSpace(row["AQLDEFECTLEVEL"].ToString())
                    || string.IsNullOrWhiteSpace(row["AQLDECISIONDEGREE"].ToString()))
                {
                    throw MessageException.Create("NoStandardData");//판정 기준이 없습니다.
                }
                else
                {
                    _decisionDegree = row["AQLDECISIONDEGREE"].ToString();
                    _aqlInspectionLevel = row["AQLINSPECTIONLEVEL"].ToString();
                    _aqlDefectLevel = row["AQLDEFECTLEVEL"].ToString();
                }
            }
            else if (_aqlOrNcr.Equals("NCR"))
            {
                if (string.IsNullOrWhiteSpace(row["NCRDECISIONDEGREE"].ToString()))
                {
                    throw MessageException.Create("NoStandardData");//판정 기준이 없습니다.
                }
                else
                {
                    _decisionDegree = row["NCRDECISIONDEGREE"].ToString();
                }
            }
        }

        /// <summary>
        /// 해당 Lot, degree에 해당하는 불량 정보가 있을때 먼저 삭제후 임시저장 하는 함수
        /// </summary>
        /// <param name="toSaveDt"></param>
        private void AddAllRowTempSaveAfterDelete(DataTable toSaveDt)
        {
            string expression = "RESOURCEID=" + "'" + _childLotRow["RESOURCEID"].ToString() + "'" + " AND DEGREE =" + "'" + _childLotRow["DEGREE"].ToString() + "'";
            DataRow[] rows = _tempSave.Select(expression);

            foreach (DataRow toDeleteRow in rows)
            {
                _tempSave.Rows.Remove(toDeleteRow);
            }

            //["ISDELETE"].Equals("Y") => 삭제할 데이터
            //!r["ISDELETE"].Equals("Y") 임지 저장 할 데이터
            var toRealSaveCount = toSaveDt.AsEnumerable()
                .Where(r => !r["ISDELETE"].Equals("Y"))
                .ToList().Count();

            if (toRealSaveCount == 0)
            {//임지 저장 할 데이터 Row 수 == 0 => 불량 수 = 0, 율 = 0%, 결과 = OK
                grdChildLot.View.SetFocusedRowCellValue("SPECOUTQTY", 0);
                grdChildLot.View.SetFocusedRowCellValue("DEFECTQTYPNL", 0);
                grdChildLot.View.CellValueChanged -= GrdChildLot_CellValueChanged;
                grdChildLot.View.SetFocusedRowCellValue("INSPECTIONRESULT", "OK");
                grdChildLot.View.SetFocusedRowCellValue("DEFECTRATE", "0%");
                grdChildLot.View.CellValueChanged += GrdChildLot_CellValueChanged;
            }
            //모든 ROW 저장
            AddAllRowTempSaveTable(toSaveDt);

            _tempSave.AcceptChanges();
        }

        /// <summary>
        /// 선택된 LotId가 바뀔때 임시저장된 데이터로 재 바인딩 하는 함수
        /// </summary>
        private void SearchDetailByLotId()
        {
            DataRow childLotRow = grdChildLot.View.GetFocusedDataRow();
            if (childLotRow == null) return;

            //Focused 된 grdChildLot Data에 해당하는 detail 정보가 임시저장 테이블에 있는지 확인
            if (_tempSave != null)
            {
                var bindingData = _tempSave.Rows.Cast<DataRow>()
                    .Where(r => r["RESOURCEID"].ToString().Equals(_childLotRow["RESOURCEID"].ToString()) &&
                    r["DEGREE"].ToString().Equals(_childLotRow["DEGREE"].ToString())).ToList();

                if (bindingData.Count == 0)
                {//임시저장 dt에 데이터 없을때
                    grdChildLotDetail.DataSource = CreateDataSource();
                    ResetRow(_childLotRow);
                }
                else
                {//임시저장 dt에 데이터 있을때
                    grdChildLotDetail.DataSource = CreateDataSource();
                    DataTable toBindingDt = bindingData.CopyToDataTable().Copy();
                    grdChildLotDetail.DataSource = toBindingDt;

                    //2020-01-17 detail 제 바인딩 시 lot데이터 수정
                    int NGCount = GetNGCount(toBindingDt);
                    string finalResult = "";
                    string messageId = null;

                    grdChildLot.View.CellValueChanged -= GrdChildLot_CellValueChanged;
                    if (NGCount == 0)
                    {//NG 한건도 없을 때
                        finalResult = "OK";

                        //2020-03-27 강유라 불량항목별 판정 후 => 전체 수량으로 판정
                        //전체 LOT AQL 판정
                        if (_aqlOrNcr.Equals("AQL"))
                        {
                            //AQL기준
                            finalResult = InspectionHelper.SetQcGradeAndResultAQLType(_childLotRow, _aqlInspectionLevel, _aqlDefectLevel, "ShipmentInspection", _decisionDegree, _childLotRow["ALLQTYPCS"].ToString(), false, out messageId);
                        }
                        else if (_aqlOrNcr.Equals("NCR"))
                        {
                            //NCR기준
                            finalResult = InspectionHelper.SetQcGradeAndResultNCRQtyRateType(_childLotRow, "ShipmentInspection", _decisionDegree, false, out messageId);
                        }

                        grdChildLot.View.SetFocusedRowCellValue("INSPECTIONRESULT", finalResult);
                    }
                    else
                    {
                        grdChildLot.View.SetFocusedRowCellValue("INSPECTIONRESULT", "NG");
                    }
                    grdChildLot.View.CellValueChanged += GrdChildLot_CellValueChanged;

                    DataRow row = grdChildLotDetail.View.GetFocusedDataRow();
                    SearchImage(row);
                }
            }
            else
            {
                grdChildLotDetail.DataSource = CreateDataSource();
                ResetRow(_childLotRow);
            }
        }

        /// <summary>
        /// AQL 기준 검사수량 입력 함수
        /// </summary>
        /// <param name="selectedDt"></param>
        /// <returns></returns>
        private DataTable SetAQLInspectionQTY(DataTable selectedDt)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_SPCLEVEL", _aqlInspectionLevel);
            param.Add("P_DEFLEVEL", _aqlDefectLevel);
            param.Add("P_LOTQTY", 0);

            foreach (DataRow lotRow in selectedDt.Rows)
            {
                object qty = lotRow["ALLQTYPCS"];
                param.Remove("P_LOTQTY");
                param.Add("P_LOTQTY", qty);

                DataTable AQLQueryDt = SqlExecuter.Query("SelectAQLCheckBasis", "10001", param);

                if (AQLQueryDt.Rows.Count == 0)
                    ShowMessage("NoAQLStandardOnlyNCR");//Lot 수량에 맞는 AQL 기준이 없습니다. NCR로 기준으로만 판정 가능합니다.
                else
                {
                    DataRow AQLQueryRow = AQLQueryDt.Rows[0];

                    //2020-03-26 강유라
                    // 영풍 - AQL 기준 숫자보다 LOT수량 적으면 검사 수량  => lot 수량
                    // 인터 - 기존
                    decimal AqlSize = 0;
                    AqlSize = Format.GetDecimal(AQLQueryRow["AQLSIZE"]);

                    if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                    {
                        AqlSize = Format.GetDecimal(qty) < AqlSize ? qty.ToSafeDecimal() : AqlSize;
                    }

                    lotRow["INSPECTIONQTY"] = AqlSize;
                }
            }

            return selectedDt;
        }

        /// <summary>
        /// NCR 기준 검사수량 입력 함수
        /// 유석진 수정-2019-12-10
        /// </summary>
        /// <param name="selectedDt"></param>
        /// <returns></returns>
        private DataTable SetNCRInspectionQTY(DataTable selectedDt)
        {
            foreach (DataRow lotRow in selectedDt.Rows)
            {
                lotRow["INSPECTIONQTY"] = DBNull.Value;
                //2020-01-17 불량코드의 판정결과중 하나라도 NG있으면 전체 lot 결과 NG로 수정 하면서 주석처리
                //lotRow["INSPECTIONRESULT"] = "OK";
                lotRow["SPECOUTQTY"] = "0";
                lotRow["DEFECTQTYPNL"] = "0";
                lotRow["DEFECTRATE"] = "0%";
            }

            return selectedDt;
        }

        /// <summary>
        /// cboStandardType datasource 바인딩
        /// </summary>
        private void SetStandartCombo()
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"LANGUAGETYPE", UserInfo.Current.LanguageType},
                { "CODECLASSID","InspectionDecisionClass"}
            };

            DataTable dt2 = SqlExecuter.Query("GetTypeList", "10001", values);

            cboStandardType.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboStandardType.Editor.ShowHeader = false;
            cboStandardType.Editor.DisplayMember = "CODENAME";
            cboStandardType.Editor.ValueMember = "CODEID";
            cboStandardType.Editor.UseEmptyItem = false;
            cboStandardType.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboStandardType.Editor.DataSource = dt2;
            cboStandardType.Editor.EditValue = "AQL";
        }

        /// <summary>
        /// row가 바뀔때 이미지 바인딩 함수
        /// </summary>
        private void SearchImage(DataRow row)
        {
            if (row == null) return;

            picBox1.Image = null;
            picBox2.Image = null;
            picBox3.Image = null;

            if (row["FILEFULLPATH1"].ToString() == string.Empty && row["FILENAME1"].ToString() == string.Empty
                && row["FILEFULLPATH2"].ToString() == string.Empty && row["FILENAME2"].ToString() == string.Empty
                && row["FILEFULLPATH3"].ToString() == string.Empty && row["FILENAME3"].ToString() == string.Empty)
            {
                return;
            }

            ImageConverter converter = new ImageConverter();

            if (!string.IsNullOrWhiteSpace(row["FILEFULLPATH1"].ToString()))
            {
                picBox1.Image = QcmImageHelper.GetImageFromFile(row["FILEFULLPATH1"].ToString());//(Image)converter.ConvertFrom(row["FILEFULLPATH1"]);
            }

            if (!string.IsNullOrWhiteSpace(row["FILEFULLPATH2"].ToString()))
            {
                picBox2.Image = QcmImageHelper.GetImageFromFile(row["FILEFULLPATH2"].ToString());//(Image)converter.ConvertFrom(row["FILEFULLPATH1"]);
            }

            if (!string.IsNullOrWhiteSpace(row["FILEFULLPATH3"].ToString()))
            {
                picBox3.Image = QcmImageHelper.GetImageFromFile(row["FILEFULLPATH3"].ToString());//(Image)converter.ConvertFrom(row["FILEFULLPATH1"]);
            }
        }

        //6. 참조 메소드
        //=====================================================================================================================
        /// <summary>
        /// 저장 버튼 클릭 함수
        /// </summary>
        private void Btn_SaveClick(string strtitle)
        {
            if (ShowMessage(MessageBoxButtons.YesNo, DialogResult.No, "InfoSave").Equals(DialogResult.No))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(ucShipmentInfo.GetInpsector()) || (_isAutoTransit == true && string.IsNullOrWhiteSpace(ucShipmentInfo.GetTransitArea())))
            {
                throw MessageException.Create("SelectInspectorAndTransitArea");//검사자와 인계작업장을 입력하세요.
            }

            foreach (DataRow beforeCheckRow in grdChildLot.View.GetCheckedRows().Rows)
            {
                if (beforeCheckRow["INSPECTIONQTY"].ToSafeInt32().Equals(0))
                {
                    throw MessageException.Create("NoInspectionQtyAndPnl"); // 검사 수량을 먼저 입력하세요
                }

                if (string.IsNullOrEmpty(Format.GetString(beforeCheckRow["INSPECTIONRESULT"])))
                {
                    throw MessageException.Create("NoSaveData"); // 저장할 데이터가 존재하지 않습니다.
                }
            }

            //2019-12-13 강유라 체크한 ROW중 검사수량 입력 된것 체크
            DataTable changed = grdChildLot.View.GetCheckedRows().Copy();

            if (changed.Rows.Count.Equals(0))
            {
                throw MessageException.Create("NoSaveData"); // 저장할 데이터가 존재하지 않습니다.
            }

            if (!(grdChildLotDetail.DataSource as DataTable).Rows.Count.Equals(0) && (_tempSave == null || _tempSave.Rows.Count.Equals(0)))
            {
                throw MessageException.Create("NoSaveInspResultData"); // 임시저장 한 검사 데이터가 존재하지 않습니다.
            }

            DataTable changedDetail = (grdChildLotDetail.DataSource as DataTable).Clone();

            //2019-12-13 체크된 row에 해당하는 불량 검사결과만 저장
            List<string> lotList = changed.AsEnumerable().Select(r => r["RESOURCEID"].ToString()).ToList();

            if (_tempSave != null)
            {
                var checkLot = _tempSave.AsEnumerable().Where(r => lotList.Contains(r["RESOURCEID"].ToString())).ToList();

                if (checkLot.Count > 0)
                {
                    changedDetail = checkLot.CopyToDataTable();
                }
            }

            //2020-03-04 강유라 최종 실적이 없는 MIGLOT 자원 선택
            //220-03-05 -> 모든경우 되돌아갈 공정 선택
            if (changed.AsEnumerable().Where(r => Format.GetString(r["INSPECTIONRESULT"]).Equals("NG")).ToList().Count > 0)
            {
                // 출하검사 NG 재작업 라우팅 팝업 추가 -> 2020-03-07 유태근
                SelectReworkRoutingShipToFinish reworkPopup = new SelectReworkRoutingShipToFinish
                {
                    StartPosition = FormStartPosition.CenterParent
                };

                if (reworkPopup.ShowDialog().Equals(DialogResult.OK))
                {
                    _reworkProcessDefId = reworkPopup.ReworkProcessDefId;
                    _reworkProcessDefVersion = reworkPopup.ReworkProcessDefVersion;
                    _reworkPathId = reworkPopup.ReworkProcessPathId;
                    _reworkResourceId = reworkPopup.ReworkResourceId;
                    _reworkAreaId = reworkPopup.ReworkAreaId;
                    _reworkProcesssegmentId = reworkPopup.ReworkProcesSegmentId;
                    _reworkProcesssegmentVersion = reworkPopup.ReworkProcesSegmentVersion;
                    _reworkUserSequence = reworkPopup.ReworkUserSequence;
                }
                else
                {
                    throw MessageException.Create("SelectResourceToFinishInspection");// 결과가 NG인 경우 돌아갈 최종공정 자원을 선택하세요.
                }
            }

            DataTable fileUploadTableMain = QcmImageHelper.GetImageFileTable();
            int totlaFileSize = 0;
            foreach (DataRow originRow in changedDetail.Rows)
            {
                if (!originRow.IsNull("FILENAME1"))
                {
                    if (!originRow.GetString("FILEFULLPATH1").Equals(""))
                    {
                        DataRow newRow = fileUploadTableMain.NewRow();
                        newRow["FILEID"] = originRow["FILEID1"];         //Server에서 FileID를 생성하여서 가져와야 한다.
                        newRow["FILENAME"] = originRow["FILEID1"];
                        newRow["FILEEXT"] = originRow.GetString("FILEEXT1").Replace(".", "");      //파일업로드시에는 확장자에서 . 을 빼야 한다.
                        newRow["FILEPATH"] = originRow["FILEINSPECTIONTYPE"];// originRow["FILEFULLPATH1"];
                        newRow["SAFEFILENAME"] = originRow["FILENAME1"];
                        newRow["FILESIZE"] = originRow["FILESIZE1"];
                        newRow["SEQUENCE"] = 1;
                        newRow["LOCALFILEPATH"] = originRow["FILEFULLPATH1"];
                        newRow["RESOURCETYPE"] = originRow["FILEINSPECTIONTYPE"];
                        newRow["RESOURCEID"] = originRow["FILERESOURCEID"];
                        newRow["RESOURCEVERSION"] = "*";
                        newRow["PROCESSINGSTATUS"] = "Wait";

                        totlaFileSize += originRow.GetInteger("FILESIZE1");

                        fileUploadTableMain.Rows.Add(newRow);
                    }
                }

                if (!originRow.IsNull("FILENAME2"))
                {
                    if (!originRow.GetString("FILEFULLPATH2").Equals(""))
                    {
                        DataRow newRow2 = fileUploadTableMain.NewRow();
                        newRow2["FILEID"] = originRow["FILEID2"];          //Server에서 FileID를 생성하여서 가져와야 한다.
                        newRow2["FILENAME"] = originRow["FILEID2"];
                        newRow2["FILEEXT"] = originRow.GetString("FILEEXT2").Replace(".", "");
                        newRow2["FILEPATH"] = originRow["FILEINSPECTIONTYPE"];//originRow["FILEFULLPATH2"];
                        newRow2["SAFEFILENAME"] = originRow["FILENAME2"];
                        newRow2["FILESIZE"] = originRow["FILESIZE2"];
                        newRow2["SEQUENCE"] = 2;
                        newRow2["LOCALFILEPATH"] = originRow["FILEFULLPATH2"];
                        newRow2["RESOURCETYPE"] = originRow["FILEINSPECTIONTYPE"];
                        newRow2["RESOURCEID"] = originRow["FILERESOURCEID"];
                        newRow2["RESOURCEVERSION"] = "*";
                        newRow2["PROCESSINGSTATUS"] = "Wait";

                        totlaFileSize += originRow.GetInteger("FILESIZE2");

                        fileUploadTableMain.Rows.Add(newRow2);
                    }
                }

                if (!originRow.IsNull("FILENAME3"))
                {
                    if (!originRow.GetString("FILEFULLPATH3").Equals(""))
                    {
                        DataRow newRow3 = fileUploadTableMain.NewRow();
                        newRow3["FILEID"] = originRow["FILEID3"];          //Server에서 FileID를 생성하여서 가져와야 한다.
                        newRow3["FILENAME"] = originRow["FILEID3"];
                        newRow3["FILEEXT"] = originRow.GetString("FILEEXT3").Replace(".", "");
                        newRow3["FILEPATH"] = originRow["FILEINSPECTIONTYPE"];//originRow["FILEFULLPATH2"];
                        newRow3["SAFEFILENAME"] = originRow["FILENAME3"];
                        newRow3["FILESIZE"] = originRow["FILESIZE3"];
                        newRow3["SEQUENCE"] = 3;
                        newRow3["LOCALFILEPATH"] = originRow["FILEFULLPATH3"];
                        newRow3["RESOURCETYPE"] = originRow["FILEINSPECTIONTYPE"];
                        newRow3["RESOURCEID"] = originRow["FILERESOURCEID"];
                        newRow3["RESOURCEVERSION"] = "*";
                        newRow3["PROCESSINGSTATUS"] = "Wait";

                        totlaFileSize += originRow.GetInteger("FILESIZE3");

                        fileUploadTableMain.Rows.Add(newRow3);
                    }
                }
            }

            //2019-12-13 체크된 row에 해당하는 불량 검사결과만 저장
            var checkDefectLot = ucShipmentInspDefect.GetGridDataSource().AsEnumerable().Where(r => lotList.Contains(r["RESOURCEID"].ToString())).ToList();
            DataTable defectDt = checkDefectLot.Count > 0 ? checkDefectLot.CopyToDataTable() : ucShipmentInspDefect.GetGridDataSource().Clone();

            DataTable fileUploadTableDefect = QcmImageHelper.GetImageFileTable();
            foreach (DataRow originRow in defectDt.Rows)
            {
                if (!originRow.IsNull("FILENAME1"))
                {
                    if (!originRow.GetString("FILEFULLPATH1").Equals(""))
                    {
                        DataRow newRow = fileUploadTableDefect.NewRow();
                        newRow["FILEID"] = originRow["FILEID1"];         //Server에서 FileID를 생성하여서 가져와야 한다.
                        newRow["FILENAME"] = originRow["FILEID1"];
                        newRow["FILEEXT"] = originRow.GetString("FILEEXT1").Replace(".", "");      //파일업로드시에는 확장자에서 . 을 빼야 한다.
                        newRow["FILEPATH"] = originRow["FILEINSPECTIONTYPE"];// originRow["FILEFULLPATH1"];
                        newRow["SAFEFILENAME"] = originRow["FILENAME1"];
                        newRow["FILESIZE"] = originRow["FILESIZE1"];
                        newRow["SEQUENCE"] = 1;
                        newRow["LOCALFILEPATH"] = originRow["FILEFULLPATH1"];
                        newRow["RESOURCETYPE"] = originRow["FILEINSPECTIONTYPE"];
                        newRow["RESOURCEID"] = originRow["FILERESOURCEID"];
                        newRow["RESOURCEVERSION"] = "*";
                        newRow["PROCESSINGSTATUS"] = "Wait";

                        totlaFileSize += originRow.GetInteger("FILESIZE1");

                        fileUploadTableDefect.Rows.Add(newRow);
                    }
                }

                if (!originRow.IsNull("FILENAME2"))
                {
                    if (!originRow.GetString("FILEFULLPATH2").Equals(""))
                    {
                        DataRow newRow2 = fileUploadTableDefect.NewRow();
                        newRow2["FILEID"] = originRow["FILEID2"];          //Server에서 FileID를 생성하여서 가져와야 한다.
                        newRow2["FILENAME"] = originRow["FILEID2"];
                        newRow2["FILEEXT"] = originRow.GetString("FILEEXT2").Replace(".", "");
                        newRow2["FILEPATH"] = originRow["FILEINSPECTIONTYPE"];//originRow["FILEFULLPATH2"];
                        newRow2["SAFEFILENAME"] = originRow["FILENAME2"];
                        newRow2["FILESIZE"] = originRow["FILESIZE2"];
                        newRow2["SEQUENCE"] = 2;
                        newRow2["LOCALFILEPATH"] = originRow["FILEFULLPATH2"];
                        newRow2["RESOURCETYPE"] = originRow["FILEINSPECTIONTYPE"];
                        newRow2["RESOURCEID"] = originRow["FILERESOURCEID"];
                        newRow2["RESOURCEVERSION"] = "*";
                        newRow2["PROCESSINGSTATUS"] = "Wait";

                        totlaFileSize += originRow.GetInteger("FILESIZE2");

                        fileUploadTableDefect.Rows.Add(newRow2);
                    }
                }
            }

            //불량, 불량처리 사진 Merge
            fileUploadTableMain.Merge(fileUploadTableDefect);

            if (fileUploadTableMain.Rows.Count > 0)
            {
                FileProgressDialog fileProgressDialog = new FileProgressDialog(fileUploadTableMain, UpDownType.Upload, "", totlaFileSize);
                if (fileProgressDialog.ShowDialog(this).Equals(DialogResult.Cancel))
                {
                    throw MessageException.Create("CancelMessageToUploadFile");  //파일업로드를 취소하였습니다.
                }

                if (!fileProgressDialog.Result.IsSuccess)
                {
                    throw MessageException.Create("FailMessageToUploadFile"); //파일업로드를 실패하였습니다.
                }
            }

            DataTable isSendEmailRS = null;

            try
            {
                this.ShowWaitArea();

                string transitAreaId = null;
                string resourceId = null;

                if (ucShipmentInfo.GetTransitArea() != null && !string.IsNullOrWhiteSpace(ucShipmentInfo.GetTransitArea()))
                {
                    transitAreaId = ucShipmentInfo.GetTransitArea().Split('|')[1];
                    resourceId = ucShipmentInfo.GetTransitArea().Split('|')[0];
                }

                MessageWorker messageWorker = new MessageWorker("SaveShipmentInspection");

                //인터플렉스 : 자동 등록
                //검사결과(SF_INSPECTIONRESULT, SF_INSPECTIONDEFECT), 이상발생(CT_ABNORMALOCCURRENCE), CAR 1순번(CT_CARREQUEST), NCR발행(CT_QCNCRISSUE)

                //영풍 : 수동 등록 (어느 범위까지 자동인지 확인 필요)
                //검사결과(SF_INSPECTIONRESULT, SF_INSPECTIONDEFECT), 이상발생(CT_ABNORMALOCCURRENCE), CAR 1순번(CT_CARREQUEST), NCR발행(CT_QCNCRISSUE)

                messageWorker.SetBody(new MessageBody()
                {
                    { "INSPECTIONUSER", ucShipmentInfo.GetInpsector() },
                    { "AREAID", transitAreaId },
                    { "RESOURCEID", resourceId },
                    { "INSPECTIONDATE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                    { "INSPECTIONDEFID", "ShipmentInspection" },
                    { "ENTERPRISEID", UserInfo.Current.Enterprise},
                    { "PLANTID", UserInfo.Current.Plant},
                    { "AQLORNCR", _aqlOrNcr},
                    { "list", changed },
                    { "list2", changedDetail },
                    { "list3", _toSaveMessage },
                    { "list4", defectDt },

                    // 2020-03-07 유태근 NG난 Lot들에 대한 재작업라우팅 정의
                    { "REWORKPROCESSDEFID", _reworkProcessDefId},
                    { "REWORKPROCESSDEFVERSION", _reworkProcessDefVersion},
                    { "REWORKPATHID", _reworkPathId},
                    { "REWORKRESOURCEID", _reworkResourceId},
                    { "REWORKAREAID", _reworkAreaId},
                    { "REWORKREPOROCESSSEGMENTID", _reworkProcesssegmentId},
                    { "REWORKREPOROCESSSEGMENTVERSION", _reworkProcesssegmentVersion},
                    { "REWORKREUSERSEQUENCE", _reworkUserSequence}
                });

                isSendEmailRS = messageWorker.Execute<DataTable>().GetResultSet();

                //DataTable toSendDt = CommonFunction.CreateShipmentAbnormalEmailDt();

                //foreach (DataRow lotRow in changed.Rows)
                //{
                //    var lotDefect = isSendEmailRS.AsEnumerable()
                //                                 .Where(r => Format.GetString(r["RESOURCEID"]).Equals(Format.GetString(lotRow["RESOURCEID"]))
                //                                          && Format.GetString(r["DEGREE"]).Equals(Format.GetString(lotRow["DEGREE"])))
                //                                 .ToList();

                //    if (lotDefect.Count > 0)
                //    {
                //        string lotDefectNameContents = string.Empty;

                //        foreach (DataRow defectRow in lotDefect)
                //        {
                //            lotDefectNameContents += Format.GetString(defectRow["EXTERIORNG"]) + "<br></br>";
                //        }

                //        DataRow newRow = toSendDt.NewRow();
                //        newRow["PRODUCTDEFNAME"] = Format.GetString(_selectedRow["PRODUCTDEFNAME"]);
                //        newRow["PRODUCTDEFID"] = Format.GetString(_selectedRow["PRODUCTDEFID"]);
                //        newRow["PRODUCTDEFVERSION"] = Format.GetString(_selectedRow["PRODUCTDEFVERSION"]);
                //        newRow["LOTID"] = Format.GetString(lotRow["RESOURCEID"]);
                //        newRow["AREANAME"] = Format.GetString(_selectedRow["AREANAME"]);
                //        newRow["CUSTOMERNAME"] = Format.GetString(_selectedRow["CUSTOMERNAME"]);
                //        newRow["WEEK"] = Format.GetString(_selectedRow["WEEK"]);
                //        newRow["DEFECTNAME"] = lotDefectNameContents;
                //        newRow["REMARK"] = "";
                //        newRow["USERID"] = UserInfo.Current.Id;
                //        newRow["TITLE"] = Language.Get("SHIPMENTABNORMALTITLE");
                //        newRow["INSPECTION"] = "ShipmentInspection";
                //        newRow["LANGUAGETYPE"] = UserInfo.Current.LanguageType;

                //        toSendDt.Rows.Add(newRow);
                //        //contents += CommonFunction.GetShipmentAbnormalEmailContents(newRow);
                //    }
                //}

                //_FileToSendEmail = QcmImageHelper.GetImageFileTableToSendEmail();
                ////2020-02-19 강유라 이메일 첨부용

                //foreach (DataRow fileRow in changedDetail.Rows)
                //{
                //    if (!string.IsNullOrWhiteSpace(fileRow.GetString("FILEFULLPATH1")))
                //    {
                //        DataRow newRow = _FileToSendEmail.NewRow();
                //        newRow["FILEDATA"] = fileRow["FILEDATA1"];
                //        newRow["FILENAME"] = fileRow["FILENAME1"];

                //        _FileToSendEmail.Rows.Add(newRow);
                //    }

                //    if (!string.IsNullOrWhiteSpace(fileRow.GetString("FILEFULLPATH2")))
                //    {
                //        DataRow newRow = _FileToSendEmail.NewRow();
                //        newRow["FILEDATA"] = fileRow["FILEDATA2"];
                //        newRow["FILENAME"] = fileRow["FILENAME2"];

                //        _FileToSendEmail.Rows.Add(newRow);
                //    }

                //    if (!string.IsNullOrWhiteSpace(fileRow.GetString("FILEFULLPATH3")))
                //    {
                //        DataRow newRow = _FileToSendEmail.NewRow();
                //        newRow["FILEDATA"] = fileRow["FILEDATA3"];
                //        newRow["FILENAME"] = fileRow["FILENAME3"];

                //        _FileToSendEmail.Rows.Add(newRow);
                //    }
                //}

                ////2020-02-19 강유라 이메일 첨부용
                ////CommonFunction.ShowSendEmailPopupDataTable(toSendDt, _FileToSendEmail);
                //CommonFunction.ShowSendEmailPopupDataTable(toSendDt);

                ShowMessage("SuccessSave");
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();

                if (isSendEmailRS != null && isSendEmailRS.Rows.Count > 0)
                {
                    ShipmentInspectionMailPopup popup = new ShipmentInspectionMailPopup(false);
                    popup.SetMailData(isSendEmailRS, false);
                    popup.ShowDialog();
                }
            }

            ClearAllData(false);
        }

        /// <summary>
        /// dt의 판정결과 NG갯수 반환 함수
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private int GetNGCount(DataTable table) => table.AsEnumerable().Where(r => r["INSPECTIONRESULT"].ToString().Equals("NG")).ToList().Count;

        /// <summary>
        /// 체공 상태 체크 2020-02-27 강유라
        /// 2021.01.26 전우성  체공 상태 체크시 Lot이 없을 경우 오류 수정
        /// </summary>
        private void OpenCheckStayingPopup()
        {
            // 체공 상태 체크
            if (SqlExecuter.Query("GetCheckStaying", "10001", new Dictionary<string, object> { { "LOTID", _lotId } }) is DataTable dt)
            {
                if (dt.Rows.Count.Equals(0))
                {
                    return;
                }

                if (Format.GetTrimString(dt.Rows[0]["ISLOCKING"]).Equals("Y"))
                {
                    if (new StayReasonCode(dt).ShowDialog().Equals(DialogResult.Cancel))
                    {
                        return;
                    }
                }
            }
        }

        #endregion Private Function
    }
}