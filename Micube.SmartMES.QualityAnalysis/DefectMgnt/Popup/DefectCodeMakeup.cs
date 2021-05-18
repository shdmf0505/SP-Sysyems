#region using

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using System;
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
    /// 프 로 그 램 명  : 품질관리 > 불량관리 > 불량품 원인판정 > 불량내역 조정
    /// 업  무  설  명  : 하나의 불량코드에 대해서 여러 불량코드로 쪼갤 수 있는 팝업이다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-07-10
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class DefectCodeMakeup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public DefectCodeMakeup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 불량코드 한줄을 보여줄 그리드
        /// </summary>
        private void InitializeDefectCode()
        {
            grdDefectCode.View.SetIsReadOnly();

            var defect = grdDefectCode.View.AddGroupColumn("DEFECTINFO");

            defect.AddTextBoxColumn("DEFECTCODE", 80)
                .SetTextAlignment(TextAlignment.Center); // 불량코드
            defect.AddTextBoxColumn("DEFECTCODENAME", 130); // 불량명
            defect.AddTextBoxColumn("QCSEGMENTNAME", 130); // 품질공정명
            defect.AddSpinEditColumn("DEFECTQTY", 80); // 불량수량
            defect.AddSpinEditColumn("OUTBOUNDQTY", 80); // 반출수량  
            defect.AddSpinEditColumn("LEFTQTY", 80); // 잔량
            defect.AddTextBoxColumn("UNIT", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("UOM"); // UOM  
            
            var reason = grdDefectCode.View.AddGroupColumn("REASONSEGMENT");

            reason.AddTextBoxColumn("REASONCONSUMABLEDEFNAME", 220)
                .SetLabel("REASONPRODUCT"); // 원인품목명
            reason.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("Rev")
                .SetIsReadOnly(); // 원인품목 Version
            reason.AddTextBoxColumn("REASONCONSUMABLELOTID", 200)
                .SetIsReadOnly(); // 원인자재 Lot
            reason.AddTextBoxColumn("REASONSEGMENTNAME", 180)
                .SetLabel("REASONSEGMENT"); // 원인공정명
            reason.AddTextBoxColumn("REASONAREANAME", 180)
                .SetLabel("REASONAREA"); // 원인작업장
            reason.AddTextBoxColumn("REASONPLANTID", 100)
                .SetLabel("REASONPLANT")
                .SetIsReadOnly(); // 원인 Site

            var hidden = grdDefectCode.View.AddGroupColumn("HIDDEN");

            hidden.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden(); // 회사 ID
            hidden.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden(); // Site ID
            hidden.AddTextBoxColumn("AREAID", 100)
                .SetIsHidden(); // 작업장 ID
            hidden.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetIsHidden(); // 공정 ID
            hidden.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100)
                .SetIsHidden(); // 공정 Version
            hidden.AddTextBoxColumn("PRODUCTDEFID", 100)
                .SetIsHidden(); // 품목 ID
            hidden.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                .SetIsHidden(); // 품목 Version
            hidden.AddTextBoxColumn("LOTID", 100)
                .SetIsHidden(); // Lot Id
            hidden.AddTextBoxColumn("TXNHISTKEY", 100)
                .SetIsHidden(); // TXN Hist key
            hidden.AddTextBoxColumn("REASONCONSUMABLEDEFID", 100)
                .SetIsHidden(); // 원인품목 ID
            hidden.AddTextBoxColumn("REASONSEGMENTID", 100)
                .SetIsHidden(); // 원인공정 ID
            hidden.AddTextBoxColumn("REASONSEGMENTVERSION", 100)
                .SetIsHidden(); // 원인공정 Version
            hidden.AddTextBoxColumn("REASONAREAID", 100)
                .SetIsHidden(); // 원인작업장 ID
            hidden.AddTextBoxColumn("FIRSTLEFTQTY", 100)
                .SetIsHidden(); // 계산되기전잔량
            hidden.AddTextBoxColumn("QCSEGMENTID", 100)
                .SetIsHidden(); // 품질공정 ID
            hidden.AddTextBoxColumn("CONFIRMSITE", 100)
                .SetIsHidden(); // Site 자동확정여부

            grdDefectCode.View.PopulateColumns();

            grdDefectCode.View.AddNewRow();
            grdDefectCode.View.SetRowCellValue(0, "QCSEGMENTID", CurrentDataRow["QCSEGMENTID"]);
            grdDefectCode.View.SetRowCellValue(0, "QCSEGMENTNAME", CurrentDataRow["QCSEGMENTNAME"]);
            grdDefectCode.View.SetRowCellValue(0, "DEFECTCODE", CurrentDataRow["DEFECTCODE"]);
            grdDefectCode.View.SetRowCellValue(0, "DEFECTCODENAME", CurrentDataRow["DEFECTCODENAME"]);
            grdDefectCode.View.SetRowCellValue(0, "REASONCONSUMABLEDEFID", CurrentDataRow["REASONCONSUMABLEDEFID"]);
            grdDefectCode.View.SetRowCellValue(0, "REASONCONSUMABLEDEFVERSION", CurrentDataRow["REASONCONSUMABLEDEFVERSION"]);
            grdDefectCode.View.SetRowCellValue(0, "REASONCONSUMABLEDEFNAME", CurrentDataRow["REASONCONSUMABLEDEFNAME"]);
            grdDefectCode.View.SetRowCellValue(0, "REASONSEGMENTNAME", CurrentDataRow["REASONSEGMENTNAME"]);
            grdDefectCode.View.SetRowCellValue(0, "REASONAREANAME", CurrentDataRow["REASONAREANAME"]);
            grdDefectCode.View.SetRowCellValue(0, "REASONPLANTID", CurrentDataRow["REASONPLANTID"]);
            grdDefectCode.View.SetRowCellValue(0, "DEFECTQTY", CurrentDataRow["DEFECTQTY"]);
            grdDefectCode.View.SetRowCellValue(0, "OUTBOUNDQTY", CurrentDataRow["OUTBOUNDQTY"]);
            grdDefectCode.View.SetRowCellValue(0, "LEFTQTY", CurrentDataRow["LEFTQTY"]);
            grdDefectCode.View.SetRowCellValue(0, "FIRSTLEFTQTY", CurrentDataRow["LEFTQTY"]);
            grdDefectCode.View.SetRowCellValue(0, "UNIT", CurrentDataRow["UNIT"]);
            grdDefectCode.View.SetRowCellValue(0, "LOTID", CurrentDataRow["LOTID"]);
            grdDefectCode.View.SetRowCellValue(0, "PARENTLOTID", CurrentDataRow["PARENTLOTID"]);
            grdDefectCode.View.SetRowCellValue(0, "TXNHISTKEY", CurrentDataRow["TXNHISTKEY"]);
            grdDefectCode.View.SetRowCellValue(0, "PRODUCTDEFID", CurrentDataRow["PRODUCTDEFID"]);
            grdDefectCode.View.SetRowCellValue(0, "PRODUCTDEFVERSION", CurrentDataRow["PRODUCTDEFVERSION"]);
            grdDefectCode.View.SetRowCellValue(0, "REASONSEGMENTID", CurrentDataRow["REASONSEGMENTID"]);
            grdDefectCode.View.SetRowCellValue(0, "REASONAREAID", CurrentDataRow["REASONAREAID"]);
            grdDefectCode.View.SetRowCellValue(0, "REASONCONSUMABLELOTID", CurrentDataRow["REASONCONSUMABLELOTID"]);
            grdDefectCode.View.SetRowCellValue(0, "ENTERPRISEID", CurrentDataRow["ENTERPRISEID"]);
            grdDefectCode.View.SetRowCellValue(0, "PLANTID", CurrentDataRow["PLANTID"]);
            grdDefectCode.View.SetRowCellValue(0, "AREAID", CurrentDataRow["AREAID"]);
            grdDefectCode.View.SetRowCellValue(0, "PROCESSSEGMENTID", CurrentDataRow["PROCESSSEGMENTID"]);
            grdDefectCode.View.SetRowCellValue(0, "PROCESSSEGMENTVERSION", CurrentDataRow["PROCESSSEGMENTVERSION"]);
            grdDefectCode.View.SetRowCellValue(0, "CONFIRMSITE", CurrentDataRow["CONFIRMSITE"]);
        }

        /// <summary>
        /// 한 불량코드에 대해 여러가지 불량코드로 나눌 그리드
        /// </summary>
        private void InitializeDefectCodeMakeup()
        {
            grdDefectCodeMakeup.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdDefectCodeMakeup.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            grdDefectCodeMakeup.View.CheckValidation();

            DefectCodePopup(); // 불량코드
            grdDefectCodeMakeup.View.AddTextBoxColumn("DEFECTCODENAME", 130)
                .SetIsReadOnly(); // 불량명
            grdDefectCodeMakeup.View.AddTextBoxColumn("QCSEGMENTNAME", 130)
                .SetIsReadOnly(); // 품질공정명
            grdDefectCodeMakeup.View.AddSpinEditColumn("DEFECTQTY", 80)
                .SetDefault(0)
                .SetValidationCustom(DefectQtyValidation); // 수량

            // 원인품목
            grdDefectCodeMakeup.View.AddComboBoxColumn("REASONCONSUMABLEDEFIDVERSION", 200, new SqlQuery("GetReasonConsumableList", "10003", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"LOTID={CurrentDataRow["PARENTLOTID"]}"), "CONSUMABLEDEFNAME", "CONSUMABLEDEFIDVERSION")
                                .SetLabel("REASONCONSUMABLEDEFID")
                                .SetMultiColumns(ComboBoxColumnShowType.Custom, true)
                                .SetRelationIds("PARENTLOTID")
                                .SetPopupWidth(600)
                                .SetVisibleColumns("CONSUMABLEDEFID", "CONSUMABLEDEFVERSION", "CONSUMABLEDEFNAME", "MATERIALTYPE")
                                .SetVisibleColumnsWidth(90, 70, 200, 80);
            grdDefectCodeMakeup.View.AddTextBoxColumn("REASONCONSUMABLEDEFID", 100).SetIsReadOnly().SetIsHidden();
            grdDefectCodeMakeup.View.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 100).SetIsReadOnly().SetIsHidden();

            // 원인자재LOT
            grdDefectCodeMakeup.View.AddComboBoxColumn("REASONCONSUMABLELOTID", 180, new SqlQuery("GetDefectReasonConsumableLot", "10003", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"LOTID={CurrentDataRow["PARENTLOTID"]}"), "CONSUMABLELOTID", "CONSUMABLELOTID")
                                .SetRelationIds("PARENTLOTID", "REASONCONSUMABLEDEFIDVERSION");

            // 원인공정
            grdDefectCodeMakeup.View.AddComboBoxColumn("REASONPROCESSSEGMENTID", 200, new SqlQuery("GetDefectReasonProcesssegment", "10003", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"LOTID={CurrentDataRow["PARENTLOTID"]}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                                .SetMultiColumns(ComboBoxColumnShowType.Custom, true)
                                .SetRelationIds("REASONCONSUMABLEDEFIDVERSION", "REASONCONSUMABLELOTID")
                                .SetPopupWidth(600)
                                .SetVisibleColumns("PROCESSSEGMENTID", "PROCESSSEGMENTNAME", "USERSEQUENCE", "AREANAME")
                                .SetVisibleColumnsWidth(90, 150, 70, 100);

            // 원인작업장
            grdDefectCodeMakeup.View.AddTextBoxColumn("REASONAREAID", 100).SetIsReadOnly().SetIsHidden();
            grdDefectCodeMakeup.View.AddTextBoxColumn("REASONAREANAME", 150).SetIsReadOnly().SetLabel("REASONAREAID");

            // 원인 Site
            grdDefectCodeMakeup.View.AddTextBoxColumn("REASONPLANTID", 100).SetIsReadOnly().SetLabel("REASONPLANT");

            grdDefectCodeMakeup.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden(); // 회사 ID
            grdDefectCodeMakeup.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden(); // Site ID
            grdDefectCodeMakeup.View.AddTextBoxColumn("AREAID", 100)
                .SetIsHidden(); // 작업장 ID
            grdDefectCodeMakeup.View.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetIsHidden(); // 공정 ID
            grdDefectCodeMakeup.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100)
                .SetIsHidden(); // 공정 Version
            grdDefectCodeMakeup.View.AddTextBoxColumn("TXNHISTKEY", 100)
                .SetIsHidden(); // TXN Hist key
            grdDefectCodeMakeup.View.AddTextBoxColumn("LOTID", 100)
                .SetIsHidden(); // Lot Id
            grdDefectCodeMakeup.View.AddTextBoxColumn("PARENTLOTID", 100)
                .SetIsHidden(); // Parent Lot Id
            grdDefectCodeMakeup.View.AddTextBoxColumn("PRODUCTDEFID", 100)
                .SetIsHidden(); // 품목 ID
            grdDefectCodeMakeup.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                .SetIsHidden(); // 품목 Version
            grdDefectCodeMakeup.View.AddTextBoxColumn("PROCESSDEFID", 100)
                .SetIsHidden(); // 라우팅 ID
            grdDefectCodeMakeup.View.AddTextBoxColumn("PROCESSDEFVERSION", 100)
                .SetIsHidden(); // 라우팅 Version
            grdDefectCodeMakeup.View.AddTextBoxColumn("PROCESSPATHID", 100)
                .SetIsHidden(); // 공정별 라우팅 ID 
            grdDefectCodeMakeup.View.AddTextBoxColumn("USERSEQUENCE", 100)
                .SetIsHidden(); // 공정수순
            grdDefectCodeMakeup.View.AddTextBoxColumn("QCSEGMENTID", 100)
                .SetIsHidden(); // 품질공정 ID
            grdDefectCodeMakeup.View.AddTextBoxColumn("CONFIRMSITE", 100)
                .SetIsHidden(); // Site 자동확정여부

            grdDefectCodeMakeup.View.PopulateColumns(); 
        }

        #endregion

        #region 그리드 팝업

        /// <summary>
        /// 그리드 불량코드 팝업
        /// </summary>
        private void DefectCodePopup()
        {
            var defectCodePopupColumn = grdDefectCodeMakeup.View.AddSelectPopupColumn("DEFECTCODE", 80, new SqlQuery("GetDefectCodeByProcess", "10001", "RESOURCETYPE=QCSegmentID", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DEFECTCODE")
                .SetPopupLayout("SELECTDEFECTCODE", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("QCSEGMENTNAME")
                .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                {
                    if (selectedRow.Count() > 0)
                    {
                        grdDefectCodeMakeup.View.SetFocusedRowCellValue("DEFECTCODENAME", selectedRow.FirstOrDefault()["DEFECTCODENAME"]);
                        grdDefectCodeMakeup.View.SetFocusedRowCellValue("QCSEGMENTID", selectedRow.FirstOrDefault()["QCSEGMENTID"]);
                        grdDefectCodeMakeup.View.SetFocusedRowCellValue("QCSEGMENTNAME", selectedRow.FirstOrDefault()["QCSEGMENTNAME"]);
                        grdDefectCodeMakeup.View.SetFocusedRowCellValue("CONFIRMSITE", selectedRow.FirstOrDefault()["CONFIRMSITE"]);

                        // 해당 불량코드의 Site 자동확정여부가 Y일경우 원인 Site 자동 바인딩
                        //if (selectedRow.FirstOrDefault()["CONFIRMSITE"].Equals("Y"))
                        //{
                        //    Dictionary<string, object> param = new Dictionary<string, object>()
                        //    {
                        //        { "PROCESSDEFID", CurrentDataRow["PROCESSDEFID"] },
                        //        { "PROCESSDEFVERSION", CurrentDataRow["PROCESSDEFVERSION"] },
                        //        { "USERSEQUENCE", CurrentDataRow["USERSEQUENCE"] },
                        //        { "DEFECTCODE", CurrentDataRow["DEFECTCODE"] },
                        //        { "QCSEGMENTID", CurrentDataRow["QCSEGMENTID"] },
                        //    };
                         
                        //    DataTable dt = SqlExecuter.Query("GetMappingReasonPlant", "10001", param);

                        //    int plantCount = dt.AsEnumerable().Select(r => r["PLANTID"]).Distinct().Count();
                            
                        //    // 매핑된 중공정이 없다면 발견 Site로 원인 Site 바인딩
                        //    if (plantCount == 0)
                        //    {
                        //        grdDefectCodeMakeup.View.SetFocusedRowCellValue("REASONPLANTID", grdDefectCodeMakeup.View.GetFocusedRowCellValue("PLANTID"));
                        //    }
                        //    // 매핑된 라우팅이 1개의 Site를 진행했다면 해당 Site로 원인 Site 바인딩
                        //    else if (plantCount == 1)
                        //    {
                        //        string reasonPlant = Format.GetString(dt.Rows[0]["PLANTID"]);
                        //        grdDefectCodeMakeup.View.SetFocusedRowCellValue("REASONPLANTID", reasonPlant);
                        //    }
                        //    // 매핑된 라우팅이 2개 이상의 Site를 진행했다면 발견 Site로 원인 Site 바인딩
                        //    else
                        //    {
                        //        grdDefectCodeMakeup.View.SetFocusedRowCellValue("REASONPLANTID", grdDefectCodeMakeup.View.GetFocusedRowCellValue("PLANTID"));
                        //    }
                        //}
                        //else
                        //{
                        //    grdDefectCodeMakeup.View.SetFocusedRowCellValue("REASONPLANTID", grdDefectCodeMakeup.View.GetFocusedRowCellValue("PLANTID")); 
                        //}
                    }
                    else
                    {
                        grdDefectCodeMakeup.View.SetFocusedRowCellValue("DEFECTCODENAME", "");
                        grdDefectCodeMakeup.View.SetFocusedRowCellValue("QCSEGMENTID", "");
                        grdDefectCodeMakeup.View.SetFocusedRowCellValue("QCSEGMENTNAME", "");
                        //grdDefectCodeMakeup.View.SetFocusedRowCellValue("REASONPLANTID", "");
                    }
                });

            // 검색조건 - 불량코드
            defectCodePopupColumn.Conditions.AddTextBox("DEFECTCODEID");
            defectCodePopupColumn.Conditions.AddTextBox("QCSEGMENTID");
            // 팝업의 검색조건 항목 추가 (불량코드/명)
            defectCodePopupColumn.Conditions.AddTextBox("DEFECTCODENAME");
            defectCodePopupColumn.Conditions.AddTextBox("QCSEGMENTNAME");

            // 팝업의 그리드에 컬럼 추가
            // 불량코드
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("DEFECTCODE", 100);
            // 불량코드명
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("DEFECTCODENAME", 200);
            // 품질공정ID
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("QCSEGMENTID", 100);
            // 품질공정명
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("QCSEGMENTNAME", 200);
            // Site 자동확정
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("CONFIRMSITE", 100)
                .SetIsHidden();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ChemicalissuePopup_Load;

            grdDefectCodeMakeup.View.AddingNewRow += View_AddingNewRow;
            grdDefectCodeMakeup.View.CellValueChanged += View_CellValueChanged;
            grdDefectCodeMakeup.ToolbarDeleteRow += GrdDefectCodeTakeOut_ToolbarDeleteRow;
            grdDefectCodeMakeup.View.ShowingEditor += View_ShowingEditor; 
            grdDefectCode.View.RowClick += View_RowClick;

            btnSave.Click += BtnSave_Click;
            btnClose.Click += BtnClose_Click;
        }

        /// <summary>
        /// 불량코드 한줄 더블클릭시 하단그리드로 복사
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                DataRow row = grdDefectCode.View.GetFocusedDataRow();

                // 다른 AddingNewRow 이벤트를 발생시키기 위해 임시로 빼기
                grdDefectCodeMakeup.View.AddingNewRow -= View_AddingNewRow;

                grdDefectCodeMakeup.View.AddingNewRow += (sender2, args) =>
                {
                    args.NewRow["DEFECTCODE"] = row["DEFECTCODE"];
                    args.NewRow["DEFECTCODENAME"] = row["DEFECTCODENAME"];
                    args.NewRow["QCSEGMENTID"] = row["QCSEGMENTID"];
                    args.NewRow["QCSEGMENTNAME"] = row["QCSEGMENTNAME"];
                    args.NewRow["ENTERPRISEID"] = CurrentDataRow["ENTERPRISEID"].ToString();
                    args.NewRow["PLANTID"] = CurrentDataRow["PLANTID"].ToString();
                    args.NewRow["AREAID"] = CurrentDataRow["AREAID"].ToString();
                    args.NewRow["PRODUCTDEFID"] = CurrentDataRow["PRODUCTDEFID"].ToString();
                    args.NewRow["PRODUCTDEFVERSION"] = CurrentDataRow["PRODUCTDEFVERSION"].ToString();
                    args.NewRow["PROCESSDEFID"] = CurrentDataRow["PROCESSDEFID"].ToString();
                    args.NewRow["PROCESSDEFVERSION"] = CurrentDataRow["PROCESSDEFVERSION"].ToString();
                    args.NewRow["PROCESSPATHID"] = CurrentDataRow["PROCESSPATHID"].ToString();
                    args.NewRow["USERSEQUENCE"] = CurrentDataRow["USERSEQUENCE"].ToString();
                    args.NewRow["PROCESSSEGMENTID"] = CurrentDataRow["PROCESSSEGMENTID"].ToString();
                    args.NewRow["PROCESSSEGMENTVERSION"] = CurrentDataRow["PROCESSSEGMENTVERSION"].ToString();
                    args.NewRow["LOTID"] = CurrentDataRow["LOTID"].ToString();
                    args.NewRow["PARENTLOTID"] = CurrentDataRow["PARENTLOTID"].ToString();
                    args.NewRow["TXNHISTKEY"] = CurrentDataRow["TXNHISTKEY"].ToString();
                };

                // 해당 불량코드의 Site 자동확정여부가 Y일경우 원인 Site 자동 바인딩 
                //if (CurrentDataRow["CONFIRMSITE"].Equals("Y"))
                //{
                //    Dictionary<string, object> param = new Dictionary<string, object>()
                //        {
                //            { "PROCESSDEFID", CurrentDataRow["PROCESSDEFID"] },
                //            { "PROCESSDEFVERSION", CurrentDataRow["PROCESSDEFVERSION"] },
                //            { "USERSEQUENCE", CurrentDataRow["USERSEQUENCE"] },
                //            { "DEFECTCODE", CurrentDataRow["DEFECTCODE"] },
                //            { "QCSEGMENTID", CurrentDataRow["QCSEGMENTID"] },
                //        };

                //    DataTable dt = SqlExecuter.Query("GetMappingReasonPlant", "10001", param);

                //    int plantCount = dt.AsEnumerable().Select(r => r["PLANTID"]).Distinct().Count();

                //    // 매핑된 중공정이 없다면 발견 Site로 원인 Site 바인딩
                //    if (plantCount == 0)
                //    {
                //        grdDefectCodeMakeup.View.SetFocusedRowCellValue("REASONPLANTID", grdDefectCodeMakeup.View.GetFocusedRowCellValue("PLANTID"));
                //    }
                //    // 매핑된 라우팅이 1개의 Site를 진행했다면 해당 Site로 원인 Site 바인딩
                //    else if (plantCount == 1)
                //    {
                //        string reasonPlant = Format.GetString(dt.Rows[0]["PLANTID"]);
                //        grdDefectCodeMakeup.View.SetFocusedRowCellValue("REASONPLANTID", reasonPlant);
                //    }
                //    // 매핑된 라우팅이 2개 이상의 Site를 진행했다면 발견 Site로 원인 Site 바인딩
                //    else
                //    {
                //        grdDefectCodeMakeup.View.SetFocusedRowCellValue("REASONPLANTID", grdDefectCodeMakeup.View.GetFocusedRowCellValue("PLANTID"));
                //    }
                //}
                //else
                //{
                //    grdDefectCodeMakeup.View.SetFocusedRowCellValue("REASONPLANTID", grdDefectCodeMakeup.View.GetFocusedRowCellValue("PLANTID"));
                //}

                // 이벤트 다시 등록
                grdDefectCodeMakeup.View.AddNewRow();

                // 다른 AddingNewRow 이벤트를 발생시킨 후 다시 추가하기
                grdDefectCodeMakeup.View.AddingNewRow += View_AddingNewRow;
            }
        }

        /// <summary>
        /// 원인품목, 자재Lot, 공정, 작업장 순으로 선택가능
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            // 원인자재 클릭시 원인품목이 선택되지 않았다면 ReadOnly
            if (grdDefectCodeMakeup.View.FocusedColumn.FieldName == "REASONCONSUMABLELOTID")
            {
                if (string.IsNullOrWhiteSpace(grdDefectCodeMakeup.View.GetFocusedRowCellValue("REASONCONSUMABLEDEFIDVERSION").ToString()))
                {
                    e.Cancel = true;           
                }
            }
            // 원인공정 클릭시 원인품목 또는 원인자재가 선택되지 않았다면 ReadOnly
            else if (grdDefectCodeMakeup.View.FocusedColumn.FieldName == "REASONPROCESSSEGMENTID")
            {
                if (string.IsNullOrWhiteSpace(grdDefectCodeMakeup.View.GetFocusedRowCellValue("REASONCONSUMABLEDEFIDVERSION").ToString())
                    || string.IsNullOrWhiteSpace(grdDefectCodeMakeup.View.GetFocusedRowCellValue("REASONCONSUMABLELOTID").ToString()))
                {
                    e.Cancel = true;
                }
            }
            // 원인작업장 클릭시 원인품목 또는 원인자재 또는 원인공정이 선택되지 않았다면 ReadOnly
            else if (grdDefectCodeMakeup.View.FocusedColumn.FieldName == "REASONAREAID")
            {
                if (string.IsNullOrWhiteSpace(grdDefectCodeMakeup.View.GetFocusedRowCellValue("REASONCONSUMABLEDEFIDVERSION").ToString())
                    || string.IsNullOrWhiteSpace(grdDefectCodeMakeup.View.GetFocusedRowCellValue("REASONCONSUMABLELOTID").ToString())
                    || string.IsNullOrWhiteSpace(grdDefectCodeMakeup.View.GetFocusedRowCellValue("REASONPROCESSSEGMENTID").ToString()))
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 불량코드 한줄 삭제시 계산
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdDefectCodeTakeOut_ToolbarDeleteRow(object sender, EventArgs e)
        {
            CalcOutbound(grdDefectCodeMakeup.View.FocusedRowHandle);
        }

        /// <summary>
        /// 불량수량 자동계산 및 원인품목, 자재Lot, 공정, 작업장 재바인딩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            // 불량수량이 입력됬을땐 수량체크 및 계산
            if (e.Column.FieldName.Equals("DEFECTQTY"))
            {
                CalcOutbound(e.RowHandle);
            }

            // 원인 품목 ID, Version 세팅
            if (e.Column.FieldName.Equals("REASONCONSUMABLEDEFIDVERSION"))
            {
                grdDefectCodeMakeup.View.SetFocusedRowCellValue("REASONCONSUMABLELOTID", "");
                grdDefectCodeMakeup.View.SetFocusedRowCellValue("REASONSEGMENTID", "");
                grdDefectCodeMakeup.View.SetFocusedRowCellValue("REASONAREAID", "");

                RepositoryItemLookUpEdit lookup = (RepositoryItemLookUpEdit)e.Column.ColumnEdit;
                string id = lookup.GetDataSourceValue("CONSUMABLEDEFID", lookup.GetDataSourceRowIndex("CONSUMABLEDEFIDVERSION", e.Value)).ToString();
                string version = lookup.GetDataSourceValue("CONSUMABLEDEFVERSION", lookup.GetDataSourceRowIndex("CONSUMABLEDEFIDVERSION", e.Value)).ToString();

                grdDefectCodeMakeup.View.SetFocusedRowCellValue("REASONCONSUMABLEDEFID", id);
                grdDefectCodeMakeup.View.SetFocusedRowCellValue("REASONCONSUMABLEDEFVERSION", version);
            }

            // 원인공정 선택시 원인작업장 및 원인 Site 세팅
            if (e.Column.FieldName == "REASONPROCESSSEGMENTID")
            {
                grdDefectCodeMakeup.View.CellValueChanged -= View_CellValueChanged;
                
                RepositoryItemLookUpEdit edit = e.Column.ColumnEdit as RepositoryItemLookUpEdit;

                string reasonLot = Format.GetString(grdDefectCodeMakeup.View.GetFocusedRowCellValue("REASONCONSUMABLELOTID"));
                string segmentId = Format.GetString(e.Value);
                string reasonLotSegment = reasonLot + "|" + segmentId;

                string areaId = Format.GetString(edit.GetDataSourceValue("AREAID", edit.GetDataSourceRowIndex("REASONLOTSEGMENT", reasonLotSegment)));
                string areaName = Format.GetString(edit.GetDataSourceValue("AREANAME", edit.GetDataSourceRowIndex("REASONLOTSEGMENT", reasonLotSegment)));
                string plantId = Format.GetString(edit.GetDataSourceValue("PLANTID", edit.GetDataSourceRowIndex("REASONLOTSEGMENT", reasonLotSegment)));

                grdDefectCodeMakeup.View.SetFocusedRowCellValue("REASONAREAID", areaId);
                grdDefectCodeMakeup.View.SetFocusedRowCellValue("REASONAREANAME", areaName);
                grdDefectCodeMakeup.View.SetFocusedRowCellValue("REASONPLANTID", plantId);

                grdDefectCodeMakeup.View.CellValueChanged += View_CellValueChanged;
            }

            //// 원인품목이 선택되면 원인자재 Lot ComboBox 세팅
            //if (e.Column.FieldName.Equals("REASONCONSUMABLEDEFID"))
            //{
            //    grdDefectCodeMakeup.View.CellValueChanged -= View_CellValueChanged;

            //    //RepositoryItemLookUpEdit lookup = (RepositoryItemLookUpEdit)e.Column.ColumnEdit;
            //    //string version = lookup.GetDataSourceValue("CONSUMABLEDEFVERSION", lookup.GetDataSourceRowIndex("CONSUMABLEDEFID", e.Value)).ToString();

            //    grdDefectCodeMakeup.View.SetFocusedRowCellValue("REASONCONSUMABLELOTID", "");
            //    grdDefectCodeMakeup.View.SetFocusedRowCellValue("REASONPROCESSSEGMENTID", "");
            //    grdDefectCodeMakeup.View.SetFocusedRowCellValue("REASONAREAID", "");

            //    string lotid = grdDefectCodeMakeup.View.GetFocusedRowCellValue("LOTID").ToString();
            //    string strProductDefId = grdDefectCodeMakeup.View.GetFocusedRowCellValue("REASONCONSUMABLEDEFID").ToString().Split('|')[0];
            //    string strProductDefVersion = grdDefectCodeMakeup.View.GetFocusedRowCellValue("REASONCONSUMABLEDEFID").ToString().Split('|')[1];

            //    grdDefectCodeMakeup.View.RefreshComboBoxDataSource("REASONCONSUMABLELOTID", new SqlQuery("GetDefectReasonConsumableLot", "10001"
            //        , $"LOTID={lotid}"
            //        , $"REASONCONSUMABLEDEFID={strProductDefId}"
            //        , $"REASONCONSUMABLEDEFVERSION={strProductDefVersion}"));

            //    grdDefectCodeMakeup.View.CellValueChanged += View_CellValueChanged;
            //}

            //// 원인자재 Lot이 선택되면 원인공정 ComboBox 세팅
            //if (e.Column.FieldName.Equals("REASONCONSUMABLELOTID"))
            //{
            //    grdDefectCodeMakeup.View.CellValueChanged -= View_CellValueChanged;

            //    string lotid = grdDefectCodeMakeup.View.GetFocusedRowCellValue("LOTID").ToString();
            //    string strProductDefId = grdDefectCodeMakeup.View.GetFocusedRowCellValue("REASONCONSUMABLEDEFID").ToString().Split('|')[0];
            //    string strProductDefVersion = grdDefectCodeMakeup.View.GetFocusedRowCellValue("REASONCONSUMABLEDEFID").ToString().Split('|')[1];
            //    string strConsumableLotId = grdDefectCodeMakeup.View.GetFocusedRowCellValue("REASONCONSUMABLELOTID").ToString();

            //    grdDefectCodeMakeup.View.RefreshComboBoxDataSource("REASONPROCESSSEGMENTID", new SqlQuery("GetDefectReasonProcesssegment", "10001"
            //        , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
            //        , $"LOTID={lotid}"
            //        , $"REASONCONSUMABLEDEFID={strProductDefId}"
            //        , $"REASONCONSUMABLEDEFVERSION={strProductDefVersion}"
            //        , $"REASONCONSUMABLELOTID={strConsumableLotId}"));

            //    grdDefectCodeMakeup.View.CellValueChanged += View_CellValueChanged;
            //}

            //// 원인공정이 선택되면 원인작업장 ComboBox 세팅
            //if (e.Column.FieldName.Equals("REASONPROCESSSEGMENTID"))
            //{
            //    grdDefectCodeMakeup.View.CellValueChanged -= View_CellValueChanged;

            //    string lotid = grdDefectCodeMakeup.View.GetFocusedRowCellValue("LOTID").ToString();
            //    string strProductDefId = grdDefectCodeMakeup.View.GetFocusedRowCellValue("REASONCONSUMABLEDEFID").ToString().Split('|')[0];
            //    string strProductDefVersion = grdDefectCodeMakeup.View.GetFocusedRowCellValue("REASONCONSUMABLEDEFID").ToString().Split('|')[1];
            //    string strConsumableLotId = grdDefectCodeMakeup.View.GetFocusedRowCellValue("REASONCONSUMABLELOTID").ToString();
            //    string strProcesssegmentId = grdDefectCodeMakeup.View.GetFocusedRowCellValue("REASONPROCESSSEGMENTID").ToString();

            //    grdDefectCodeMakeup.View.RefreshComboBoxDataSource("REASONAREAID", new SqlQuery("GetDefectReasonArea", "10001"
            //        , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
            //        , $"LOTID={lotid}"
            //        , $"REASONCONSUMABLEDEFID={strProductDefId}"
            //        , $"REASONCONSUMABLEDEFVERSION={strProductDefVersion}"
            //        , $"REASONCONSUMABLELOTID={strConsumableLotId}"
            //        , $"REASONPROCESSSEGMENTID={strProcesssegmentId}"));

            //    grdDefectCodeMakeup.View.CellValueChanged += View_CellValueChanged;
            //}
        }

        /// <summary>
        /// Row 추가시 자동입력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["DEFECTCODE"] = null;
            args.NewRow["DEFECTCODENAME"] = null;
            args.NewRow["ENTERPRISEID"] = CurrentDataRow["ENTERPRISEID"].ToString();
            args.NewRow["PLANTID"] = CurrentDataRow["PLANTID"].ToString();
            args.NewRow["AREAID"] = CurrentDataRow["AREAID"].ToString();
            args.NewRow["PRODUCTDEFID"] = CurrentDataRow["PRODUCTDEFID"].ToString();
            args.NewRow["PRODUCTDEFVERSION"] = CurrentDataRow["PRODUCTDEFVERSION"].ToString();
            args.NewRow["PROCESSDEFID"] = CurrentDataRow["PROCESSDEFID"].ToString();
            args.NewRow["PROCESSDEFVERSION"] = CurrentDataRow["PROCESSDEFVERSION"].ToString();
            args.NewRow["PROCESSPATHID"] = CurrentDataRow["PROCESSPATHID"].ToString();
            args.NewRow["USERSEQUENCE"] = CurrentDataRow["USERSEQUENCE"].ToString();
            args.NewRow["PROCESSSEGMENTID"] = CurrentDataRow["PROCESSSEGMENTID"].ToString();
            args.NewRow["PROCESSSEGMENTVERSION"] = CurrentDataRow["PROCESSSEGMENTVERSION"].ToString();
            args.NewRow["LOTID"] = CurrentDataRow["LOTID"].ToString();
            args.NewRow["PARENTLOTID"] = CurrentDataRow["PARENTLOTID"].ToString();
            args.NewRow["TXNHISTKEY"] = CurrentDataRow["TXNHISTKEY"].ToString();
        }

        /// <summary>
        /// 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            DataTable changed1 = grdDefectCode.DataSource as DataTable;
            DataTable changed2 = grdDefectCodeMakeup.GetChangedRows();

            if (changed2.Rows.Count == 0)
            {
                this.ShowMessage("NoSaveData");
            }
            else
            {
                try
                {
                    DataColumn[] primarykey = new DataColumn[1];
                    primarykey[0] = changed2.Columns["DEFECTCODE"];
                    changed2.PrimaryKey = primarykey;
                }
                catch (Exception)
                {
                    throw MessageException.Create("SameDefectCodeError");
                }

                foreach (DataRow row in changed2.Rows)
                {
                    if (Convert.ToInt32(row["DEFECTQTY"]) <= 0)
                    {
                        this.ShowMessage("DefectQtyValidation");
                        return;
                    }
                }

                if (this.ShowMessageBox("InfoPopupSave", "Information", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    changed1.TableName = "result";

                    DataSet ds = new DataSet();
                    ds.Tables.Add(changed1.Copy());
                    ds.Tables.Add(changed2.Copy());

                    this.ExecuteRule("SaveLotDefectMakeup", ds);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Form Road시 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChemicalissuePopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            InitializeDefectCode();
            
            InitializeDefectCodeMakeup();          
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 불량수량은 0이상 Validation
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <returns></returns>
        private IEnumerable<ValidationResult> DefectQtyValidation(int rowHandle)
        {
            var currentRow = grdDefectCodeMakeup.View.GetDataRow(rowHandle);
            List<ValidationResult> result = new List<ValidationResult>();
            ValidationResult defectQty = new ValidationResult();

            if (Convert.ToInt32(currentRow["DEFECTQTY"]) == 0)
            {
                defectQty.Caption = "Message";
                defectQty.FailMessage = Language.GetMessage("불량수량은 0상이어야 합니다.").Message;
                defectQty.Id = "DEFECTQTY";
                defectQty.IsSucced = false;
                result.Add(defectQty);
            }

            return result;
        }

        /// <summary>
        /// 새 불량코드의 불량수량에 따른 잔량 계산
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <param name="columnName"></param>
        private void CalcOutbound(int rowHandle)
        {
            // DB에 저장되있는 잔량값
            int leftQty = Convert.ToInt32(grdDefectCode.View.GetRowCellValue(0, "FIRSTLEFTQTY"));
            // 새로생긴 불량수량의 Total값
            int totalDefectCount = 0;

            DataTable dt = (grdDefectCodeMakeup.DataSource as DataTable);

            foreach (DataRow row in dt.Rows)
            {
                if (string.IsNullOrEmpty(row["DEFECTQTY"].ToString()))
                {
                    return;
                }
                totalDefectCount += Convert.ToInt32(row["DEFECTQTY"]);
            }

            if (totalDefectCount > leftQty)
            {
                this.ShowMessage("DefectCountGreatThenLeftQty");
                grdDefectCodeMakeup.View.SetRowCellValue(rowHandle, "DEFECTQTY", grdDefectCodeMakeup.View.ActiveEditor.OldEditValue);
            }
            else
            {
                grdDefectCode.View.SetRowCellValue(0, "LEFTQTY", leftQty - totalDefectCount);
                (grdDefectCode.DataSource as DataTable).AcceptChanges();
            }
        }

        /// <summary>
        /// 외부에서 LOT ID 세팅 시 원인품목 세팅
        /// </summary>
        public void SetConsumableDefComboBox(string lotId)
        {
            string lotid = lotId;

            grdDefectCodeMakeup.View.RefreshComboBoxDataSource("CONSUMABLEDEFIDVERSION", new SqlQuery("GetReasonConsumableListRelation", "10001", $"LOTID={lotid}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdDefectCodeMakeup.View.RefreshComboBoxDataSource("REASONCONSUMABLELOTID", new SqlQuery("GetReasonConsumableLotRelation", "10001", $"LOTID={lotid}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdDefectCodeMakeup.View.RefreshComboBoxDataSource("REASONSEGMENTID", new SqlQuery("GetReasonProcessSegmentRelation", "10001", $"LOTID={lotid}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdDefectCodeMakeup.View.RefreshComboBoxDataSource("REASONAREAID", new SqlQuery("GetReasonAreaRelation", "10001", $"LOTID={lotid}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
        }

        #endregion

        #region BackUp

        // 원인자재 선택팝업
        //InitializeGrid_CauseProductIdPopup();
        //grdDefectCodeMakeup.View.AddTextBoxColumn("CONSUMABLEDEFID").SetIsHidden();
        //grdDefectCodeMakeup.View.AddTextBoxColumn("CONSUMABLEDEFVERSION").SetIsHidden();
        //grdDefectCodeMakeup.View.AddTextBoxColumn("REASONPRODUCTDEFID").SetIsHidden();
        //grdDefectCodeMakeup.View.AddTextBoxColumn("REASONPRODUCTDEFVERSION").SetIsHidden();
        //grdDefectCodeMakeup.View.AddTextBoxColumn("HASROUTING").SetIsHidden();
        //grdDefectCodeMakeup.View.AddTextBoxColumn("MATERIALLOTID").SetIsHidden();
        //grdDefectCodeMakeup.View.AddTextBoxColumn("PARAMLOTID").SetIsHidden();

        //// 원인공정
        //InitializeGrid_CauseSegmentIdPopup();
        //grdDefectCodeMakeup.View.AddTextBoxColumn("REASONSEGMENTID").SetIsHidden(); // 원인공정 ID
        //grdDefectCodeMakeup.View.AddTextBoxColumn("REASONSEGMENTVERSION").SetIsHidden(); // 원인공정 Version

        //// 원인작업장 선택팝업
        //grdDefectCodeMakeup.View.AddTextBoxColumn("REASONAREANAME").SetIsReadOnly(); // 원인작업장명
        //grdDefectCodeMakeup.View.AddTextBoxColumn("REASONAREAID").SetIsHidden(); // 원인작업장 ID

        /// <summary>
        /// 원인 품목 팝업
        /// </summary>
        //private void InitializeGrid_CauseProductIdPopup()
        //{
        //    var causeProductIdColumn = grdDefectCodeMakeup.View.AddSelectPopupColumn("CONSUMABLEDEFNAME", 250, new SqlQuery("GetCauseMaterialList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
        //        .SetPopupLayout("SELECTREASONMATERIAL", PopupButtonStyles.Ok_Cancel, false, true)
        //        .SetPopupResultCount(1)
        //        .SetPopupLayoutForm(600, 600, FormBorderStyle.SizableToolWindow)
        //        .SetLabel("REASONPRODUCT")
        //        .SetPopupQueryPopup((DataRow currentRow) =>
        //        {
        //            currentRow["PARAMLOTID"] = CurrentDataRow["LOTID"];
        //            return true;
        //        })
        //        .SetPopupApplySelection((selectedRows, gridRows) =>
        //        {
        //            DataRow row = selectedRows.FirstOrDefault();

        //            if (row != null)
        //            {
        //                string hasRouting = Format.GetString(row["HASROUTING"]);
        //                string materialLotId = Format.GetString(row["MATERIALLOTID"]);

        //                if (hasRouting.Equals("Y"))
        //                {
        //                    gridRows["PARAMLOTID"] = materialLotId;
        //                    gridRows["REASONPRODUCTDEFID"] = Format.GetString(row["CONSUMABLEDEFID"]);
        //                    gridRows["REASONPRODUCTDEFVERSION"] = Format.GetString(row["CONSUMABLEDEFVERSION"]);
        //                }

        //                gridRows["MATERIALLOTID"] = materialLotId;
        //                gridRows["HASROUTING"] = hasRouting;
        //                gridRows["CONSUMABLEDEFID"] = Format.GetString(row["CONSUMABLEDEFID"]);
        //                gridRows["CONSUMABLEDEFVERSION"] = Format.GetString(row["CONSUMABLEDEFVERSION"]);
        //                gridRows["REASONAREAID"] = string.Empty;
        //                gridRows["REASONAREANAME"] = string.Empty;
        //            }
        //            else
        //            {
        //                gridRows["REASONPRODUCTDEFID"] = string.Empty;
        //                gridRows["REASONPRODUCTDEFVERSION"] = string.Empty;
        //                gridRows["MATERIALLOTID"] = string.Empty;
        //                gridRows["HASROUTING"] = string.Empty;
        //                gridRows["CONSUMABLEDEFID"] = string.Empty;
        //                gridRows["CONSUMABLEDEFVERSION"] = string.Empty;
        //                gridRows["REASONSEGMENTID"] = string.Empty;
        //                gridRows["REASONSEGMENTNAME"] = string.Empty;
        //                gridRows["REASONAREAID"] = string.Empty;
        //                gridRows["REASONAREANAME"] = string.Empty;
        //            }

        //        });

        //    causeProductIdColumn.Conditions.AddTextBox("LOTID")
        //        .SetPopupDefaultByGridColumnId("PARAMLOTID")
        //        .SetIsHidden();
        //    causeProductIdColumn.Conditions.AddTextBox("PROCESSSEGMENTID")
        //        .SetPopupDefaultByGridColumnId("REASONSEGMENTID")
        //        .SetIsHidden();
        //    causeProductIdColumn.Conditions.AddTextBox("PROCESSSEGMENTVERSION")
        //        .SetPopupDefaultByGridColumnId("REASONSEGMENTVERSION")
        //        .SetIsHidden();

        //    causeProductIdColumn.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);
        //    causeProductIdColumn.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 250);
        //    causeProductIdColumn.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80);
        //    causeProductIdColumn.GridColumns.AddTextBoxColumn("LOTID", 100)
        //        .SetIsHidden();
        //    causeProductIdColumn.GridColumns.AddTextBoxColumn("HASROUTING", 100)
        //        .SetIsHidden();
        //    causeProductIdColumn.GridColumns.AddTextBoxColumn("PRODUCT", 100)
        //        .SetIsHidden();
        //}

        ///// <summary>
        ///// 원인 공정 팝업
        ///// </summary>
        //private void InitializeGrid_CauseSegmentIdPopup()
        //{
        //    var causeSegmentIdColumn = grdDefectCodeMakeup.View.AddSelectPopupColumn("REASONSEGMENTNAME", 200, new SqlQuery("GetCauseProcessList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
        //        .SetPopupLayout("SELECTREASONSEGMENT", PopupButtonStyles.Ok_Cancel, false, true)
        //        .SetPopupResultCount(1)
        //        .SetPopupResultMapping("REASONSEGMENTID", "PROCESSSEGMENTID")
        //        .SetPopupResultMapping("REASONSEGMENTNAME", "PROCESSSEGMENTNAME")
        //        .SetPopupLayoutForm(900, 600, FormBorderStyle.SizableToolWindow)
        //        .SetLabel("CAUSEPROCESS")
        //        .SetPopupApplySelection((selectedRows, gridRows) => {
        //            DataRow row = selectedRows.FirstOrDefault();

        //            if (row != null)
        //            {
        //                gridRows["REASONSEGMENTID"] = Format.GetString(row["PROCESSSEGMENTID"]);
        //                gridRows["REASONSEGMENTVERSION"] = Format.GetString(row["PROCESSSEGMENTVERSION"]);
        //                gridRows["REASONAREAID"] = Format.GetString(row["AREAID"]);
        //                gridRows["REASONAREANAME"] = Format.GetString(row["AREANAME"]);
        //            }
        //            else
        //            {
        //                gridRows["REASONSEGMENTID"] = string.Empty;
        //                gridRows["REASONSEGMENTVERSION"] = string.Empty;
        //                gridRows["REASONAREAID"] = string.Empty;
        //                gridRows["REASONAREANAME"] = string.Empty;
        //            }

        //        });

        //    //조회조건
        //    causeSegmentIdColumn.Conditions.AddTextBox("LOTID")
        //        .SetPopupDefaultByGridColumnId("LOTID")
        //        .SetIsHidden();
        //    causeSegmentIdColumn.Conditions.AddTextBox("HASROUTING")
        //        .SetPopupDefaultByGridColumnId("HASROUTING")
        //        .SetIsHidden();
        //    causeSegmentIdColumn.Conditions.AddTextBox("CONSUMABLEDEFID")
        //        .SetPopupDefaultByGridColumnId("CONSUMABLEDEFID")
        //        .SetIsHidden();
        //    causeSegmentIdColumn.Conditions.AddTextBox("CONSUMABLEDEFVERSION")
        //        .SetPopupDefaultByGridColumnId("CONSUMABLEDEFVERSION")
        //        .SetIsHidden();
        //    causeSegmentIdColumn.Conditions.AddTextBox("LANGUAGETYPE")
        //        .SetDefault(UserInfo.Current.LanguageType)
        //        .SetIsHidden();
        //    causeSegmentIdColumn.Conditions.AddTextBox("MATERIALLOTID")
        //        .SetPopupDefaultByGridColumnId("MATERIALLOTID")
        //        .SetIsHidden();

        //    causeSegmentIdColumn.GridColumns.AddTextBoxColumn("USERSEQUENCE", 80);
        //    causeSegmentIdColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
        //    causeSegmentIdColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
        //    causeSegmentIdColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTVERSION", 80);
        //    causeSegmentIdColumn.GridColumns.AddTextBoxColumn("AREAID", 100);
        //    causeSegmentIdColumn.GridColumns.AddTextBoxColumn("AREANAME", 150);
        //    causeSegmentIdColumn.GridColumns.AddTextBoxColumn("PROCESSDEFID", 100)
        //        .SetIsHidden();
        //    causeSegmentIdColumn.GridColumns.AddTextBoxColumn("PROCESSDEFVERSION", 100)
        //        .SetIsHidden();
        //}

        #endregion
    }
}
