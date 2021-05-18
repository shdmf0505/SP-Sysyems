#region using

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

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 불량관리 > 불량품 폐기취소
    /// 업  무  설  명  : 불량Lot을 불량이 아닌Lot으로 지정하거나 병합하고 내역을 조회한다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-07-15
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class DefectCancel : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// 선택된 재작업 라우팅 상세정보(불량취소탭)
        /// </summary> 
        private DataTable _reworkRoutingDt = new DataTable();

        /// <summary>
        /// 선택된 재작업 라우팅 상세정보(불량병합탭)
        /// </summary> 
        private DataTable _reworkRoutingDt2 = new DataTable();

        #endregion

        #region 생성자

        public DefectCancel()
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

            InitializeDefectCodeGrid();
            InitializeDefectCodeCountGrid();
            InitializeDefectCodeRoutionGrid();

            InitializeDefectCodeGrid2();
            InitializeDefectCodeCountGrid2();
            InitializeDefectCodeRoutionGrid2();

            InitializeDefectCodeCancelHistoryGrid();

            InitializeDefectAMTGrid();
            InitializeDefectAMTDetailGrid();
            InitializationSummaryRow();

            (grdDefectCodeRouting.View.Columns["INPUTPROCESSDEFNAME"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit).ButtonClick += DefectCancel_ButtonClick;
            (grdDefectCodeRouting2.View.Columns["INPUTPROCESSDEFNAME"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit).ButtonClick += DefectCancel_ButtonClick;
        }

        #region 불량취소탭 Initialize

        /// <summary>        
        /// Lot별 불량코드를 모두 조회한다.(불량취소탭)
        /// </summary>
        private void InitializeDefectCodeGrid()
        {
            grdDefectCode.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdDefectCode.View.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            grdDefectCode.View.CheckMarkSelection.ShowCheckBoxHeader = false;
            grdDefectCode.GridButtonItem = GridButtonItem.Export;
            //grdDefectCode.View.CheckMarkSelection.MultiSelectCount = 1;

            var defectInfo = grdDefectCode.View.AddGroupColumn("DEFECTINFO");

            defectInfo.AddTextBoxColumn("PROCESSDATE", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 처리일시
            defectInfo.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetIsReadOnly(); // 품목코드
            defectInfo.AddTextBoxColumn("PRODUCTDEFNAME", 260)
                .SetIsReadOnly(); // 품목명
            defectInfo.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 품목 Version
            defectInfo.AddTextBoxColumn("PARENTLOTID", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetLabel("Lot No"); // Parnet Lot No
            defectInfo.AddTextBoxColumn("DEFECTCODE", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 불량코드
            defectInfo.AddTextBoxColumn("DEFECTNAME", 130)
                .SetIsReadOnly(); // 불량명
            defectInfo.AddTextBoxColumn("QCSEGMENTNAME", 130)
                .SetIsReadOnly(); // 품질공정명      
            defectInfo.AddSpinEditColumn("DEFECTPCSQTY", 80)
                .SetLabel("PCS")
                .SetIsReadOnly(); // 불량 PCS 수량 
            //defectInfo.AddSpinEditColumn("DEFECTPNLQTY", 80)
            //    .SetLabel("PNL")
            //    .SetIsReadOnly(); // 불량 PNL 수량 
            defectInfo.AddTextBoxColumn("USERSEQUENCE", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetIsReadOnly(); // 공정순서
            defectInfo.AddTextBoxColumn("PROCESSSEGMENTNAME", 180)
                .SetIsReadOnly(); // 공정명
            defectInfo.AddTextBoxColumn("AREANAME", 150)
                .SetIsReadOnly(); // 작업장명
            defectInfo.AddTextBoxColumn("PLANTID", 80)
                .SetIsReadOnly(); // Site
            defectInfo.AddTextBoxColumn("LOTID", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetLabel("DEFECTLOTID"); // 불량 Lot No

            var reasonSegment = grdDefectCode.View.AddGroupColumn("CAUSEPROCESS");

            reasonSegment.AddTextBoxColumn("REASONCONSUMABLEDEFNAME", 220)
                .SetLabel("REASONPRODUCT")
                .SetIsReadOnly(); // 원인품목
            reasonSegment.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("Rev")
                .SetIsReadOnly(); // 원인품목 Version
            reasonSegment.AddTextBoxColumn("REASONCONSUMABLELOTID", 220)
                .SetIsReadOnly(); // 원인자재 Lot No
            reasonSegment.AddTextBoxColumn("REASONSEGMENTNAME", 180)
                .SetLabel("REASONSEGMENT")
                .SetIsReadOnly(); // 원인공정
            reasonSegment.AddTextBoxColumn("REASONAREANAME", 180)
                .SetLabel("REASONAREA")
                .SetIsReadOnly(); // 원인작업장
            reasonSegment.AddTextBoxColumn("ISUNKNOWN", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 원인불명

            var etc = grdDefectCode.View.AddGroupColumn("ETC");

            etc.AddTextBoxColumn("LOTTYPENAME", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("LOTTYPE")
                .SetIsReadOnly(); // 양산구분
            etc.AddSpinEditColumn("OUTBOUNDQTY", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetIsReadOnly(); // 반출수
            etc.AddTextBoxColumn("RECEIVEAREANAME", 180)
                .SetLabel("STORAGE")
                .SetIsReadOnly(); // 인수작업장
            etc.AddTextBoxColumn("DEFINETIME", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 확정일시
            etc.AddTextBoxColumn("STATUS", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("CONFIRMATIONSTATUS")
                .SetIsReadOnly(); // 확정상태
            etc.AddTextBoxColumn("PROCESSSTATENAME", 100)
                .SetLabel("PROCESSSTATE")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // Lot의 공정진행상태
            etc.AddTextBoxColumn("UOM", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("UOM")
                .SetIsReadOnly(); // 불량 UOM 

            var hidden = grdDefectCode.View.AddGroupColumn("HIDDEN");

            hidden.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden(); // 회사 ID
            hidden.AddTextBoxColumn("AREAID", 100)
                .SetIsHidden(); // 작업장 ID
            hidden.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetIsHidden(); // 공정 ID
            hidden.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100)
                .SetIsHidden(); // 공정 Version
            hidden.AddTextBoxColumn("REASONCONSUMABLEDEFID", 100)
                .SetIsHidden(); // 원인품목 ID
            hidden.AddTextBoxColumn("REASONSEGMENTID", 100)
                .SetIsHidden(); // 원인공정 ID
            hidden.AddTextBoxColumn("REASONAREAID", 100)
                .SetIsHidden(); // 원인작업장 ID
            hidden.AddTextBoxColumn("LOTTYPE", 100)
                .SetIsHidden(); // 양산구분
            hidden.AddTextBoxColumn("TXNHISTKEY", 100)
                .SetIsHidden(); // TXNHISTKEY
            hidden.AddTextBoxColumn("PRODUCTIONORDERID", 100)
                .SetIsHidden(); // S/O번호
            hidden.AddTextBoxColumn("LINENO", 100)
                .SetIsHidden(); // Line No          
            hidden.AddTextBoxColumn("ISORIGINALLOTMERGE", 100)
                .SetIsHidden(); // Merge 가능여부
            hidden.AddTextBoxColumn("CANCELCODE", 100)
                .SetIsHidden(); // 취소사유
            hidden.AddTextBoxColumn("INPUTPROCESSDEFID", 100)
                .SetIsHidden(); // Routing
            hidden.AddTextBoxColumn("INPUTPLANTID", 100)
                .SetIsHidden(); // 투입Site ID
            hidden.AddTextBoxColumn("INPUTUSERSEQUENCE", 100)
                .SetIsHidden(); // 투입공정수순
            hidden.AddTextBoxColumn("INPUTSEGMENTID", 100)
                .SetIsHidden(); // 투입공정 ID
            hidden.AddTextBoxColumn("INPUTAREAID", 100)
                .SetIsHidden(); // 투입작업장 ID
            hidden.AddTextBoxColumn("REPAIRLOTNO", 100)
                .SetIsHidden(); // 취소후 재생성 Lot ID
            hidden.AddTextBoxColumn("CHECK", 100)
                .SetIsHidden(); // Original Lot Merge Check
            hidden.AddTextBoxColumn("ROUTINGTYPE", 100)
                .SetIsHidden(); // Routing Type
            hidden.AddTextBoxColumn("SUMLOTDEFECTQTY", 100)
                .SetIsHidden(); // Lot별 불량수량
            hidden.AddTextBoxColumn("DEFECTCODECOUNT", 100)
                 .SetIsHidden(); // Lot별 불량코드갯수
            hidden.AddTextBoxColumn("PANELPERQTY", 100)
                 .SetIsHidden(); // PNL별 PCS수량
            hidden.AddTextBoxColumn("INPUTPROCESSPATHID", 100)
                .SetIsHidden(); // 투입 라우팅 상세 ID
            hidden.AddTextBoxColumn("RETURNPROCESSDEFID", 100)
                .SetIsHidden(); // 재작업 후 돌아갈 품목 라우팅 ID
            hidden.AddTextBoxColumn("RETURNPROCESSDEFVERSION", 100)
                .SetIsHidden(); // 재작업 후 돌아갈 품목 라우팅 Version
            hidden.AddTextBoxColumn("RETURNPROCESSPATHID", 100)
                .SetIsHidden(); // 재작업 후 라우팅 상세 ID
            hidden.AddTextBoxColumn("RETURNSEGMENTNAME", 100)
                .SetIsHidden(); // 재작업 후 공정명
            hidden.AddTextBoxColumn("QCSEGMENTID", 100)
                .SetIsHidden(); // 품질공정 ID
            hidden.AddTextBoxColumn("PROCESSSTATE", 100)
                .SetIsHidden(); // Lot의 공정진행상태
            hidden.AddTextBoxColumn("INPUTRESOURCEID", 100)
                .SetIsHidden(); // 투입 Resource ID
            hidden.AddTextBoxColumn("RETURNRESOURCEID", 100)
                .SetIsHidden(); // 재작업 후 Resource ID
            hidden.AddTextBoxColumn("WORKCOUNT", 100)
                .SetIsHidden(); // Lot Work Count
            hidden.AddTextBoxColumn("PARENTPROCESSDEFID", 100)
                .SetIsHidden(); // 부모 Lot의 라우팅 ID
            hidden.AddTextBoxColumn("PARENTPROCESSDEFVERSION", 100)
                .SetIsHidden(); // 부모 Lot의 라우팅 Version
            hidden.AddTextBoxColumn("DESCRIPTION", 100)
                .SetIsHidden(); // 비고
            hidden.AddTextBoxColumn("ISMIGRATION", 100)
                .SetIsHidden(); // MIG여부
            hidden.AddTextBoxColumn("PROCESSPATHSTACK", 100)
                .SetIsHidden(); // 현재 불량의 ProcessPathId

            grdDefectCode.View.PopulateColumns();

            grdDefectCode.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        /// <summary>        
        /// 조회한 Lot별 불량코드들의 수량을 나타내는 그리드이다.(불량취소탭)
        /// </summary>
        private void InitializeDefectCodeCountGrid()
        {
            grdDefectCodeCnt.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            var defectCount = grdDefectCodeCnt.View.AddGroupColumn("DEFECTCOUNT");

            defectCount.AddSpinEditColumn("DEFECTPCSQTY", 80)
                .SetLabel("PCS")
                .SetIsReadOnly(); // 불량 PCS 수량 
            //defectCount.AddSpinEditColumn("DEFECTPNLQTY", 80)
            //    .SetLabel("PNL")
            //    .SetIsReadOnly(); // 불량 PNL 수량 
            defectCount.AddTextBoxColumn("UOM", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("UOM")
                .SetIsReadOnly(); // 불량 UOM

            var cancelCount = grdDefectCodeCnt.View.AddGroupColumn("CANCELCOUNT");

            cancelCount.AddSpinEditColumn("CANCELPCSQTY", 80)
                 .SetLabel("PCS"); // 취소 PCS 수량
            cancelCount.AddSpinEditColumn("CANCELPNLQTY", 80)
                .SetLabel("PNL"); // 취소 PNL 수량

            var defect = grdDefectCodeCnt.View.AddGroupColumn("DEFECTINFO");

            defect.AddTextBoxColumn("DEFECTCODE", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 불량코드
            defect.AddTextBoxColumn("DEFECTNAME", 130)
                .SetIsReadOnly(); // 불량명
            defect.AddTextBoxColumn("QCSEGMENTNAME", 130)
                .SetIsReadOnly(); // 품질공정명

            var hidden = grdDefectCodeCnt.View.AddGroupColumn("HIDDEN");

            hidden.AddTextBoxColumn("LOTID", 100)
                .SetIsHidden(); // 불량 Lot No
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
                .SetIsHidden(); // 제품 ID
            hidden.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                .SetIsHidden(); // 제품 Version
            hidden.AddTextBoxColumn("REASONCONSUMABLEDEFID", 100)
                .SetIsHidden(); // 원인품목 ID
            hidden.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 100)
                .SetIsHidden(); // 원인품목 Version
            hidden.AddTextBoxColumn("REASONCONSUMABLELOTID", 100)
                .SetIsHidden(); // 원인자재 ID
            hidden.AddTextBoxColumn("REASONSEGMENTID", 100)
                .SetIsHidden(); // 원인공정 ID
            hidden.AddTextBoxColumn("REASONAREAID", 100)
                .SetIsHidden(); // 원인작업장 ID
            hidden.AddTextBoxColumn("PARENTLOTID", 100)
                .SetIsHidden(); // 부모 Lot No
            hidden.AddTextBoxColumn("LOTTYPE", 100)
                .SetIsHidden(); // 양산구분
            hidden.AddTextBoxColumn("TXNHISTKEY", 100)
                .SetIsHidden(); // TXNHISTKEY
            hidden.AddTextBoxColumn("PRODUCTIONORDERID", 100)
                .SetIsHidden(); // S/O번호
            hidden.AddTextBoxColumn("LINENO", 100)
                .SetIsHidden(); // Line No          
            hidden.AddTextBoxColumn("ISORIGINALLOTMERGE", 100)
                .SetIsHidden(); // Merge 가능여부
            hidden.AddTextBoxColumn("CANCELCODE", 100)
                .SetIsHidden(); // 취소사유
            hidden.AddTextBoxColumn("INPUTPROCESSDEFID", 100)
                .SetIsHidden(); // Routing
            hidden.AddTextBoxColumn("INPUTPLANTID", 100)
                .SetIsHidden(); // 투입Site ID
            hidden.AddTextBoxColumn("INPUTUSERSEQUENCE", 100)
                .SetIsHidden(); // 투입공정수순
            hidden.AddTextBoxColumn("INPUTSEGMENTID", 100)
                .SetIsHidden(); // 투입공정 ID
            hidden.AddTextBoxColumn("INPUTAREAID", 100)
                .SetIsHidden(); // 투입작업장 ID
            hidden.AddTextBoxColumn("REPAIRLOTNO", 100)
                .SetIsHidden(); // 취소후 재생성 Lot ID
            hidden.AddTextBoxColumn("CHECK", 100)
                .SetIsHidden(); // Original Lot Merge Check
            hidden.AddTextBoxColumn("ROUTINGTYPE", 100)
                .SetIsHidden(); // Routing Type
            hidden.AddTextBoxColumn("SUMLOTDEFECTQTY", 100)
                .SetIsHidden(); // Lot별 불량수량
            hidden.AddTextBoxColumn("DEFECTCODECOUNT", 100)
                 .SetIsHidden(); // Lot별 불량코드갯수
            hidden.AddTextBoxColumn("PANELPERQTY", 100)
                 .SetIsHidden(); // PNL별 PCS수량
            hidden.AddTextBoxColumn("INPUTPROCESSPATHID", 100)
                 .SetIsHidden(); // 투입 라우팅 상세 ID
            hidden.AddTextBoxColumn("RETURNPROCESSDEFID", 100)
                .SetIsHidden(); // 재작업 후 돌아갈 품목 라우팅 ID
            hidden.AddTextBoxColumn("RETURNPROCESSDEFVERSION", 100)
                .SetIsHidden(); // 재작업 후 돌아갈 품목 라우팅 Version
            hidden.AddTextBoxColumn("RETURNPROCESSPATHID", 100)
                .SetIsHidden(); // 재작업 후 라우팅 상세 ID
            hidden.AddTextBoxColumn("RETURNSEGMENTNAME", 100)
                .SetIsHidden(); // 재작업 후 공정명
            hidden.AddTextBoxColumn("QCSEGMENTID", 100)
                .SetIsHidden(); // 품질공정 ID
            hidden.AddTextBoxColumn("PROCESSSTATE", 100)
                .SetIsHidden(); // Lot의 공정진행상태
            hidden.AddTextBoxColumn("INPUTRESOURCEID", 100)
                .SetIsHidden(); // 투입 Resource ID
            hidden.AddTextBoxColumn("RETURNRESOURCEID", 100)
                .SetIsHidden(); // 재작업 후 Resource ID
            hidden.AddTextBoxColumn("WORKCOUNT", 100)
                .SetIsHidden(); // Lot Work Count
            hidden.AddTextBoxColumn("PARENTPROCESSDEFID", 100)
                .SetIsHidden(); // 부모 Lot의 라우팅 ID
            hidden.AddTextBoxColumn("PARENTPROCESSDEFVERSION", 100)
                .SetIsHidden(); // 부모 Lot의 라우팅 Version
            hidden.AddTextBoxColumn("DESCRIPTION", 100)
                .SetIsHidden(); // 비고
            hidden.AddTextBoxColumn("ISMIGRATION", 100)
                .SetIsHidden(); // MIG여부
            hidden.AddTextBoxColumn("PROCESSPATHSTACK", 100)
                .SetIsHidden(); // 현재 불량의 ProcessPathId

            grdDefectCodeCnt.View.PopulateColumns();

            grdDefectCodeCnt.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        /// <summary>
        /// Routing을 매핑하는 그리드이다.(불량취소탭)
        /// </summary>
        private void InitializeDefectCodeRoutionGrid()
        {
            grdDefectCodeRouting.View.AddTextBoxColumn("ISORIGINALLOTMERGE", 150)
                .SetIsHidden()
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("Original Lot Merge")
                .SetIsReadOnly(); // Original Lot Merge 가능여부

            grdDefectCodeRouting.View.AddCheckBoxColumn("CHECK", 80)
                .SetIsHidden()
                .SetLabel("Check"); // 가능하면 활성화, 가능하지 않으면 비활성화

            var inputSegment = grdDefectCodeRouting.View.AddGroupColumn("INPUTSEGMENT");

            inputSegment.AddComboBoxColumn("CANCELCODE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ReasonCancel", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("REASONCANCEL"); // 취소사유코드

            // 투입라우팅 ID
            inputSegment.AddSelectPopupColumn("INPUTPROCESSDEFNAME", 200, new DefectCancelRoutingPopup())
                .SetLabel("ROUTINGNAME")
                .SetPopupCustomParameter
                (
                    (sender, currentRow) =>
                    {
                        (sender as DefectCancelRoutingPopup).CurrentDataRow = grdDefectCodeRouting.View.GetFocusedDataRow();
                        (sender as DefectCancelRoutingPopup).InputSegmentDataEvent += (dr) => // 투입공정 데이터
                        {
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("INPUTPROCESSDEFID", dr["PROCESSDEFID"]); // 투입 라우팅 ID
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("INPUTPROCESSDEFVERSION", dr["PROCESSDEFVERSION"]); // 투입 라우팅 Version
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("INPUTPROCESSDEFNAME", dr["PROCESSDEFNAME"]); // 투입 라우팅명
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("INPUTPROCESSPATHID", dr["PROCESSPATHID"]); // 투입 라우팅 상세 ID
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("INPUTUSERSEQUENCE", dr["USERSEQUENCE"]); // 순서
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("INPUTSEGMENTID", dr["PROCESSSEGMENTID"]); // 투입 공정 ID
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("INPUTSEGMENTVERSION", dr["PROCESSSEGMENTVERSION"]); // 투입 공정 Version
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("INPUTSEGMENTNAME", dr["PROCESSSEGMENTNAME"]); // 투입 공정명
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("INPUTAREAID", dr["AREAID"]); // 투입 작업장 ID
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("INPUTRESOURCEID", dr["RESOURCEID"]); // 투입 Resource ID
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("INPUTRESOURCENAME", dr["RESOURCENAME"]); // 투입 Resource명
                            _reworkRoutingDt.Clear();
                        };
                        (sender as DefectCancelRoutingPopup).ReturnSegmentDataEvent += (dr) => // 재작업후 공정 데이터
                        {
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("RETURNPROCESSDEFID", dr["PROCESSDEFID"]); // 재작업 후 라우팅 ID
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("RETURNPROCESSDEFVERSION", dr["PROCESSDEFVERSION"]); // 재작업 후 라우팅 Version
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("RETURNPROCESSPATHID", dr["PROCESSPATHID"]); // 재작업 후 라우팅 상세 ID
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("RETURNUSERSEQUENCE", dr["USERSEQUENCE"]); // 재작업 후 순서
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("RETURNSEGMENTID", dr["PROCESSSEGMENTID"]); // 재작업 후 공정 ID
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("RETURNSEGMENTVERSION", dr["PROCESSSEGMENTVERSION"]); // 재작업 후 공정 Version
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("RETURNSEGMENTNAME", dr["PROCESSSEGMENTNAME"]); // 재작업 후 공정명
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("RETURNAREAID", dr["AREAID"]); // 재작업 후 작업장 ID
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("RETURNRESOURCEID", dr["RESOURCEID"]); // 재작업 후 Resource ID
                            grdDefectCodeRouting.View.SetFocusedRowCellValue("RETURNRESOURCENAME", dr["RESOURCENAME"]); // 재작업 후 작업장명
                            _reworkRoutingDt.Clear();
                        };
                        (sender as DefectCancelRoutingPopup).ReworkRoutingDataEvent += (dt) =>
                        {
                            _reworkRoutingDt = dt;
                        };
                    }
                );

            inputSegment.AddTextBoxColumn("INPUTUSERSEQUENCE", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("USERSEQUENCE")
                .SetIsReadOnly(); // 투입 공정수순
            inputSegment.AddTextBoxColumn("INPUTSEGMENTNAME", 180)
                .SetLabel("PROCESSSEGMENTNAME")
                .SetIsReadOnly(); // 투입 공정명
            inputSegment.AddTextBoxColumn("INPUTRESOURCENAME", 220)
                .SetLabel("RESOURCENAME")
                .SetIsReadOnly(); // 투입 자원명

            var returnSegment = grdDefectCodeRouting.View.AddGroupColumn("RETURNSEGMENT");

            returnSegment.AddTextBoxColumn("RETURNUSERSEQUENCE", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("USERSEQUENCE")
                .SetIsReadOnly(); // 재작업후 공정수순
            returnSegment.AddTextBoxColumn("RETURNSEGMENTNAME", 180)
                .SetLabel("PROCESSSEGMENTNAME")
                .SetIsReadOnly(); // 재작업후 공정명
            returnSegment.AddTextBoxColumn("RETURNRESOURCENAME", 220)
                .SetLabel("RESOURCENAME")
                .SetIsReadOnly(); // 재작업후 자원명

            var etc = grdDefectCodeRouting.View.AddGroupColumn("ETC");

            etc.AddTextBoxColumn("DESCRIPTION", 200); // 비고

            var hidden = grdDefectCodeRouting.View.AddGroupColumn("HIDDEN");

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
                .SetIsHidden(); // 제품 ID
            hidden.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                 .SetIsHidden(); // 제품 Version
            hidden.AddTextBoxColumn("REASONCONSUMABLEDEFID", 100)
                .SetIsHidden(); // 원인품목 ID
            hidden.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 100)
                .SetIsHidden(); // 원인품목 Version
            hidden.AddTextBoxColumn("REASONCONSUMABLELOTID", 100)
                .SetIsHidden(); // 원인자재 ID
            hidden.AddTextBoxColumn("REASONSEGMENTID", 100)
                .SetIsHidden(); // 원인공정 ID
            hidden.AddTextBoxColumn("REASONAREAID", 100)
                .SetIsHidden(); // 원인작업장 ID
            hidden.AddTextBoxColumn("PARENTLOTID", 100)
                .SetIsHidden(); // 부모 Lot No
            hidden.AddTextBoxColumn("LOTTYPE", 100)
                .SetIsHidden(); // 양산구분
            hidden.AddTextBoxColumn("TXNHISTKEY", 100)
                .SetIsHidden(); // TXNHISTKEY
            hidden.AddTextBoxColumn("PRODUCTIONORDERID", 100)
                .SetIsHidden(); // S/O번호
            hidden.AddTextBoxColumn("LINENO", 100)
                .SetIsHidden(); // Line No          
            hidden.AddTextBoxColumn("ISORIGINALLOTMERGECODE", 100)
                .SetIsHidden(); // Merge 가능여부 코드
            hidden.AddTextBoxColumn("INPUTPLANTID", 100)
                .SetIsHidden(); // 투입Site ID
            hidden.AddTextBoxColumn("INPUTPROCESSDEFID", 100)
                .SetIsHidden(); // 투입라우팅 ID
            hidden.AddTextBoxColumn("INPUTPROCESSDEFVERSION", 100)
                .SetIsHidden(); // 투입 라우팅 Version
            hidden.AddTextBoxColumn("INPUTPROCESSPATHID", 100)
                .SetIsHidden(); // 투입 라우팅 상세 ID
            hidden.AddTextBoxColumn("INPUTSEGMENTID", 100)
                .SetIsHidden(); // 투입공정 ID
            hidden.AddTextBoxColumn("INPUTSEGMENTVERSION", 100)
                .SetIsHidden(); // 투입 공정 Version
            hidden.AddTextBoxColumn("INPUTAREAID", 100)
                .SetIsHidden(); // 투입 작업장 ID
            hidden.AddTextBoxColumn("REPAIRLOTNO", 100)
                .SetIsHidden(); // 취소후 재생성 Lot ID
            hidden.AddTextBoxColumn("SUMLOTDEFECTQTY", 100)
                .SetIsHidden(); // Lot별 불량수량
            hidden.AddTextBoxColumn("DEFECTCODECOUNT", 100)
                .SetIsHidden(); // Lot별 불량코드갯수
            hidden.AddTextBoxColumn("PANELPERQTY", 100)
                .SetIsHidden(); // PNL별 PCS수량
            hidden.AddTextBoxColumn("RETURNPROCESSDEFID", 100)
                .SetIsHidden(); // 재작업 후 돌아갈 품목 라우팅 ID
            hidden.AddTextBoxColumn("RETURNPROCESSDEFVERSION", 100)
                .SetIsHidden(); // 재작업 후 돌아갈 품목 라우팅 Version
            hidden.AddTextBoxColumn("RETURNPROCESSPATHID", 100)
                .SetIsHidden(); // 재작업 후 라우팅 상제 정의 ID
            hidden.AddTextBoxColumn("RETURNSEGMENTID", 100)
                .SetIsHidden(); // 재작업 후 공정 ID
            hidden.AddTextBoxColumn("RETURNSEGMENTVERSION", 100)
                .SetIsHidden(); // 재작업 후 공정 Version
            hidden.AddTextBoxColumn("RETURNAREAID", 100)
                .SetIsHidden(); // 재작업 후 작업장 ID
            hidden.AddTextBoxColumn("QCSEGMENTID", 100)
                .SetIsHidden(); // 품질공정 ID
            hidden.AddTextBoxColumn("PROCESSSTATE", 100)
                .SetIsHidden(); // Lot의 공정진행상태
            hidden.AddTextBoxColumn("INPUTRESOURCEID", 100)
                .SetIsHidden(); // 투입 Resource ID
            hidden.AddTextBoxColumn("RETURNRESOURCEID", 100)
                .SetIsHidden(); // 재작업 후 Resource ID
            hidden.AddTextBoxColumn("WORKCOUNT", 100)
                .SetIsHidden(); // Lot Work Count
            hidden.AddTextBoxColumn("PARENTPROCESSDEFID", 100)
                .SetIsHidden(); // 부모 Lot의 라우팅 ID
            hidden.AddTextBoxColumn("PARENTPROCESSDEFVERSION", 100)
                .SetIsHidden(); // 부모 Lot의 라우팅 Version
            hidden.AddTextBoxColumn("ISMIGRATION", 100)
                .SetIsHidden(); // MIG여부
            hidden.AddTextBoxColumn("PROCESSPATHSTACK", 100)
                .SetIsHidden(); // 현재 불량의 ProcessPathId

            grdDefectCodeRouting.View.PopulateColumns();

            grdDefectCodeRouting.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        #endregion

        #region 불량병합탭 Initialize

        /// <summary>        
        /// Lot별 불량코드를 모두 조회한다.(불량병합탭)
        /// </summary>
        private void InitializeDefectCodeGrid2()
        {
            grdDefectCode2.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdDefectCode2.View.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            grdDefectCode2.View.CheckMarkSelection.ShowCheckBoxHeader = false;
            grdDefectCode2.GridButtonItem = GridButtonItem.Export;
            //grdDefectCode2.View.CheckMarkSelection.MultiSelectCount = 1;

            var defectInfo = grdDefectCode2.View.AddGroupColumn("DEFECTINFO");

            defectInfo.AddTextBoxColumn("PROCESSDATE", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 처리일시
            defectInfo.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetIsReadOnly(); // 품목코드
            defectInfo.AddTextBoxColumn("PRODUCTDEFNAME", 260)
                .SetIsReadOnly(); // 품목명
            defectInfo.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 품목 Version
            defectInfo.AddTextBoxColumn("PARENTLOTID", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetLabel("Lot No"); // Parnet Lot No
            defectInfo.AddSpinEditColumn("DEFECTPCSQTY", 80)
                .SetLabel("PCS")
                .SetIsReadOnly(); // 불량 PCS 수량 
            //defectInfo.AddSpinEditColumn("DEFECTPNLQTY", 80)
            //    .SetLabel("PNL")
            //    .SetIsReadOnly(); // 불량 PNL 수량 
            defectInfo.AddTextBoxColumn("USERSEQUENCE", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetIsReadOnly(); // 공정순서
            defectInfo.AddTextBoxColumn("PROCESSSEGMENTNAME", 180)
                .SetIsReadOnly(); // 공정명
            defectInfo.AddTextBoxColumn("AREANAME", 150)
                .SetIsReadOnly(); // 작업장명
            defectInfo.AddTextBoxColumn("PLANTID", 80)
                .SetIsReadOnly(); // Site
            defectInfo.AddTextBoxColumn("LOTID", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetLabel("DEFECTLOTID"); // 불량 Lot No
            defectInfo.AddTextBoxColumn("DEFECTCODE", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 불량코드
            defectInfo.AddTextBoxColumn("DEFECTNAME", 130)
                .SetIsReadOnly(); // 불량명
            defectInfo.AddTextBoxColumn("QCSEGMENTNAME", 130)
                .SetIsReadOnly(); // 품질공정명      

            var reasonSegment = grdDefectCode2.View.AddGroupColumn("CAUSEPROCESS");

            reasonSegment.AddTextBoxColumn("REASONCONSUMABLEDEFNAME", 220)
                .SetLabel("REASONPRODUCT")
                .SetIsReadOnly(); // 원인품목
            reasonSegment.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("Rev")
                .SetIsReadOnly(); // 원인품목 Version
            reasonSegment.AddTextBoxColumn("REASONCONSUMABLELOTID", 220)
                .SetIsReadOnly(); // 원인자재 Lot No
            reasonSegment.AddTextBoxColumn("REASONSEGMENTNAME", 180)
                .SetLabel("REASONSEGMENT")
                .SetIsReadOnly(); // 원인공정
            reasonSegment.AddTextBoxColumn("REASONAREANAME", 180)
                .SetLabel("REASONAREA")
                .SetIsReadOnly(); // 원인작업장
            reasonSegment.AddTextBoxColumn("ISUNKNOWN", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 원인불명

            var etc = grdDefectCode2.View.AddGroupColumn("ETC");

            etc.AddTextBoxColumn("LOTTYPENAME", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("LOTTYPE")
                .SetIsReadOnly(); // 양산구분
            etc.AddSpinEditColumn("OUTBOUNDQTY", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetIsReadOnly(); // 반출수
            etc.AddTextBoxColumn("RECEIVEAREANAME", 180)
                .SetLabel("STORAGE")
                .SetIsReadOnly(); // 인수작업장
            etc.AddTextBoxColumn("DEFINETIME", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 확정일시
            etc.AddTextBoxColumn("STATUS", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("CONFIRMATIONSTATUS")
                .SetIsReadOnly(); // 확정상태
            etc.AddTextBoxColumn("PROCESSSTATENAME", 100)
                .SetLabel("PROCESSSTATE")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // Lot의 공정진행상태
            etc.AddTextBoxColumn("UOM", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("UOM")
                .SetIsReadOnly(); // 불량 UOM 

            var hidden = grdDefectCode2.View.AddGroupColumn("HIDDEN");

            hidden.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden(); // 회사 ID
            hidden.AddTextBoxColumn("AREAID", 100)
                .SetIsHidden(); // 작업장 ID
            hidden.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetIsHidden(); // 공정 ID
            hidden.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100)
                .SetIsHidden(); // 공정 Version
            hidden.AddTextBoxColumn("REASONSEGMENTID", 100)
                .SetIsHidden(); // 원인공정 ID
            hidden.AddTextBoxColumn("REASONAREAID", 100)
                .SetIsHidden(); // 원인작업장 ID
            hidden.AddTextBoxColumn("LOTTYPE", 100)
                .SetIsHidden(); // 양산구분
            hidden.AddTextBoxColumn("TXNHISTKEY", 100)
                .SetIsHidden(); // TXNHISTKEY
            hidden.AddTextBoxColumn("PRODUCTIONORDERID", 100)
                .SetIsHidden(); // S/O번호
            hidden.AddTextBoxColumn("LINENO", 100)
                .SetIsHidden(); // Line No          
            hidden.AddTextBoxColumn("ISORIGINALLOTMERGE", 100)
                .SetIsHidden(); // Merge 가능여부
            hidden.AddTextBoxColumn("CANCELCODE", 100)
                .SetIsHidden(); // 취소사유
            hidden.AddTextBoxColumn("INPUTPROCESSDEFID", 100)
                .SetIsHidden(); // Routing
            hidden.AddTextBoxColumn("INPUTPLANTID", 100)
                .SetIsHidden(); // 투입Site ID
            hidden.AddTextBoxColumn("INPUTUSERSEQUENCE", 100)
                .SetIsHidden(); // 투입공정수순
            hidden.AddTextBoxColumn("INPUTSEGMENTID", 100)
                .SetIsHidden(); // 투입공정 ID
            hidden.AddTextBoxColumn("INPUTAREAID", 100)
                .SetIsHidden(); // 투입작업장 ID
            hidden.AddTextBoxColumn("REPAIRLOTNO", 100)
                .SetIsHidden(); // 취소후 재생성 Lot ID
            hidden.AddTextBoxColumn("CHECK", 100)
                .SetIsHidden(); // Original Lot Merge Check
            hidden.AddTextBoxColumn("ROUTINGTYPE", 100)
                .SetIsHidden(); // Routing Type
            hidden.AddTextBoxColumn("SUMLOTDEFECTQTY", 100)
                .SetIsHidden(); // Lot별 불량수량
            hidden.AddTextBoxColumn("DEFECTCODECOUNT", 100)
                 .SetIsHidden(); // Lot별 불량코드갯수
            hidden.AddTextBoxColumn("PANELPERQTY", 100)
                 .SetIsHidden(); // PNL별 PCS수량
            hidden.AddTextBoxColumn("RETURNPROCESSDEFID", 100)
                .SetIsHidden(); // 재작업 후 돌아갈 품목 라우팅 ID
            hidden.AddTextBoxColumn("RETURNPROCESSDEFVERSION", 100)
                .SetIsHidden(); // 재작업 후 돌아갈 품목 라우팅 Version
            hidden.AddTextBoxColumn("RETURNPROCESSPATHID", 100)
                .SetIsHidden(); // 재작업 후 라우팅 상세 ID
            hidden.AddTextBoxColumn("RETURNSEGMENTNAME", 100)
                .SetIsHidden(); // 재작업 후 공정명
            hidden.AddTextBoxColumn("QCSEGMENTID", 100)
                .SetIsHidden(); // 품질공정 ID
            hidden.AddTextBoxColumn("PROCESSSTATE", 100)
                .SetIsHidden(); // Lot의 공정진행상태
            hidden.AddTextBoxColumn("INPUTRESOURCEID", 100)
                .SetIsHidden(); // 투입 Resource ID
            hidden.AddTextBoxColumn("RETURNRESOURCEID", 100)
                .SetIsHidden(); // 재작업 후 Resource ID
            hidden.AddTextBoxColumn("WORKCOUNT", 100)
                .SetIsHidden(); // Lot Work Count
            hidden.AddTextBoxColumn("PARENTPROCESSDEFID", 100)
                .SetIsHidden(); // 부모 Lot의 라우팅 ID
            hidden.AddTextBoxColumn("PARENTPROCESSDEFVERSION", 100)
                .SetIsHidden(); // 부모 Lot의 라우팅 Version
            hidden.AddTextBoxColumn("ISMIGRATION", 100)
                .SetIsHidden(); // MIG여부
            hidden.AddTextBoxColumn("PROCESSPATHSTACK", 100)
                .SetIsHidden(); // 현재 불량의 ProcessPathId

            grdDefectCode2.View.PopulateColumns();

            grdDefectCode2.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        /// <summary>        
        /// 불량코드들의 수량만 나타내는 그리드이다.(불량병합탭)
        /// </summary>
        private void InitializeDefectCodeCountGrid2()
        {
            grdDefectCodeCnt2.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            var defectCount = grdDefectCodeCnt2.View.AddGroupColumn("DEFECTCOUNT");

            defectCount.AddSpinEditColumn("DEFECTPCSQTY", 80)
                .SetLabel("PCS")
                .SetIsReadOnly(); // 불량 PCS 수량 
            //defectCount.AddSpinEditColumn("DEFECTPNLQTY", 80)
            //    .SetLabel("PNL")
            //    .SetIsReadOnly(); // 불량 PNL 수량 
            defectCount.AddTextBoxColumn("UOM", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("UOM")
                .SetIsReadOnly(); // 불량 UOM

            var cancelCount = grdDefectCodeCnt2.View.AddGroupColumn("CANCELCOUNT");

            cancelCount.AddSpinEditColumn("CANCELPCSQTY", 80)
                 .SetLabel("PCS"); // 취소 PCS 수량
            cancelCount.AddSpinEditColumn("CANCELPNLQTY", 80)
                .SetLabel("PNL"); // 취소 PNL 수량

            var defect = grdDefectCodeCnt2.View.AddGroupColumn("DEFECTINFO");

            defect.AddTextBoxColumn("PARENTLOTID", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetLabel("Lot No"); // Parent Lot No
            defect.AddTextBoxColumn("LOTID", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetLabel("DEFECTLOTID"); // 불량 Lot No
            defect.AddTextBoxColumn("DEFECTCODE", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 불량코드
            defect.AddTextBoxColumn("DEFECTNAME", 130)
                .SetIsReadOnly(); // 불량명
            defect.AddTextBoxColumn("QCSEGMENTNAME", 130)
                .SetIsReadOnly(); // 품질공정명


            var hidden = grdDefectCodeCnt2.View.AddGroupColumn("HIDDEN");

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
                .SetIsHidden(); // 제품 ID
            hidden.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                .SetIsHidden(); // 제품 Version
            hidden.AddTextBoxColumn("REASONCONSUMABLEDEFID", 100)
                .SetIsHidden(); // 원인품목 ID
            hidden.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 100)
                .SetIsHidden(); // 원인품목 Version
            hidden.AddTextBoxColumn("REASONCONSUMABLELOTID", 100)
                .SetIsHidden(); // 원인자재 ID
            hidden.AddTextBoxColumn("REASONSEGMENTID", 100)
                .SetIsHidden(); // 원인공정 ID
            hidden.AddTextBoxColumn("REASONAREAID", 100)
                .SetIsHidden(); // 원인작업장 ID
            hidden.AddTextBoxColumn("LOTTYPE", 100)
                .SetIsHidden(); // 양산구분
            hidden.AddTextBoxColumn("TXNHISTKEY", 100)
                .SetIsHidden(); // TXNHISTKEY
            hidden.AddTextBoxColumn("PRODUCTIONORDERID", 100)
                .SetIsHidden(); // S/O번호
            hidden.AddTextBoxColumn("LINENO", 100)
                .SetIsHidden(); // Line No          
            hidden.AddTextBoxColumn("ISORIGINALLOTMERGE", 100)
                .SetIsHidden(); // Merge 가능여부
            hidden.AddTextBoxColumn("CANCELCODE", 100)
                .SetIsHidden(); // 취소사유
            hidden.AddTextBoxColumn("INPUTPROCESSDEFID", 100)
                .SetIsHidden(); // Routing
            hidden.AddTextBoxColumn("INPUTPLANTID", 100)
                .SetIsHidden(); // 투입Site ID
            hidden.AddTextBoxColumn("INPUTUSERSEQUENCE", 100)
                .SetIsHidden(); // 투입공정수순
            hidden.AddTextBoxColumn("INPUTSEGMENTID", 100)
                .SetIsHidden(); // 투입공정 ID
            hidden.AddTextBoxColumn("INPUTAREAID", 100)
                .SetIsHidden(); // 투입작업장 ID
            hidden.AddTextBoxColumn("REPAIRLOTNO", 100)
                .SetIsHidden(); // 취소후 재생성 Lot ID
            hidden.AddTextBoxColumn("CHECK", 100)
                .SetIsHidden(); // Original Lot Merge Check
            hidden.AddTextBoxColumn("ROUTINGTYPE", 100)
                .SetIsHidden(); // Routing Type
            hidden.AddTextBoxColumn("SUMLOTDEFECTQTY", 100)
                .SetIsHidden(); // Lot별 불량수량
            hidden.AddTextBoxColumn("DEFECTCODECOUNT", 100)
                 .SetIsHidden(); // Lot별 불량코드갯수
            hidden.AddTextBoxColumn("PANELPERQTY", 100)
                 .SetIsHidden(); // PNL별 PCS수량
            hidden.AddTextBoxColumn("INPUTPROCESSPATHID", 100)
                 .SetIsHidden(); // 투입 라우팅 상세 ID
            hidden.AddTextBoxColumn("RETURNPROCESSDEFID", 100)
                .SetIsHidden(); // 재작업 후 돌아갈 품목 라우팅 ID
            hidden.AddTextBoxColumn("RETURNPROCESSDEFVERSION", 100)
                .SetIsHidden(); // 재작업 후 돌아갈 품목 라우팅 Version
            hidden.AddTextBoxColumn("RETURNPROCESSPATHID", 100)
                .SetIsHidden(); // 재작업 후 라우팅 상세 ID
            hidden.AddTextBoxColumn("RETURNSEGMENTNAME", 100)
                .SetIsHidden(); // 재작업 후 공정명
            hidden.AddTextBoxColumn("QCSEGMENTID", 100)
                .SetIsHidden(); // 품질공정 ID
            hidden.AddTextBoxColumn("PROCESSSTATE", 100)
                .SetIsHidden(); // Lot의 공정진행상태
            hidden.AddTextBoxColumn("INPUTRESOURCEID", 100)
                .SetIsHidden(); // 투입 Resource ID
            hidden.AddTextBoxColumn("RETURNRESOURCEID", 100)
                .SetIsHidden(); // 재작업 후 Resource ID
            hidden.AddTextBoxColumn("WORKCOUNT", 100)
                .SetIsHidden(); // Lot Work Count
            hidden.AddTextBoxColumn("PARENTPROCESSDEFID", 100)
                .SetIsHidden(); // 부모 Lot의 라우팅 ID
            hidden.AddTextBoxColumn("PARENTPROCESSDEFVERSION", 100)
                .SetIsHidden(); // 부모 Lot의 라우팅 Version
            hidden.AddTextBoxColumn("ISMIGRATION", 100)
                .SetIsHidden(); // MIG여부
            hidden.AddTextBoxColumn("PROCESSPATHSTACK", 100)
                .SetIsHidden(); // 현재 불량의 ProcessPathId

            grdDefectCodeCnt2.View.PopulateColumns();

            grdDefectCodeCnt2.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        /// <summary>
        /// Routing을 매핑하는 그리드이다.(불량병합탭)
        /// </summary>
        private void InitializeDefectCodeRoutionGrid2()
        {
            var inputSegment = grdDefectCodeRouting2.View.AddGroupColumn("INPUTSEGMENT");

            inputSegment.AddComboBoxColumn("CANCELCODE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ReasonCancel", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("REASONCANCEL"); // 취소사유코드

            // 투입라우팅 ID
            inputSegment.AddSelectPopupColumn("INPUTPROCESSDEFNAME", 200, new DefectCancelRoutingPopup())
                .SetLabel("ROUTINGNAME")
                .SetPopupCustomParameter
                (
                    (sender, currentRow) =>
                    {
                        (sender as DefectCancelRoutingPopup).CurrentDataRow = grdDefectCodeRouting2.View.GetFocusedDataRow();
                        (sender as DefectCancelRoutingPopup).InputSegmentDataEvent += (dr) => // 투입공정 데이터
                        {
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("INPUTPROCESSDEFID", dr["PROCESSDEFID"]); // 투입 라우팅 ID
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("INPUTPROCESSDEFVERSION", dr["PROCESSDEFVERSION"]); // 투입 라우팅 Version
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("INPUTPROCESSDEFNAME", dr["PROCESSDEFNAME"]); // 투입 라우팅명
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("INPUTPROCESSPATHID", dr["PROCESSPATHID"]); // 투입 라우팅 상세 ID
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("INPUTUSERSEQUENCE", dr["USERSEQUENCE"]); // 순서
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("INPUTSEGMENTID", dr["PROCESSSEGMENTID"]); // 투입 공정 ID
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("INPUTSEGMENTVERSION", dr["PROCESSSEGMENTVERSION"]); // 투입 공정 Version
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("INPUTSEGMENTNAME", dr["PROCESSSEGMENTNAME"]); // 투입 공정명
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("INPUTAREAID", dr["AREAID"]); // 투입 작업장 ID
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("INPUTRESOURCEID", dr["RESOURCEID"]); // 투입 Resource ID
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("INPUTRESOURCENAME", dr["RESOURCENAME"]); // 투입 Resource명
                            _reworkRoutingDt2.Clear();
                        };
                        (sender as DefectCancelRoutingPopup).ReturnSegmentDataEvent += (dr) => // 재작업후 공정 데이터
                        {
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("RETURNPROCESSDEFID", dr["PROCESSDEFID"]); // 재작업 후 라우팅 ID
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("RETURNPROCESSDEFVERSION", dr["PROCESSDEFVERSION"]); // 재작업 후 라우팅 Version
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("RETURNPROCESSPATHID", dr["PROCESSPATHID"]); // 재작업 후 라우팅 상세 ID
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("RETURNUSERSEQUENCE", dr["USERSEQUENCE"]); // 재작업 후 순서
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("RETURNSEGMENTID", dr["PROCESSSEGMENTID"]); // 재작업 후 공정 ID
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("RETURNSEGMENTVERSION", dr["PROCESSSEGMENTVERSION"]); // 재작업 후 공정 Version
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("RETURNSEGMENTNAME", dr["PROCESSSEGMENTNAME"]); // 재작업 후 공정명
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("RETURNAREAID", dr["AREAID"]); // 재작업 후 작업장 ID
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("RETURNRESOURCEID", dr["RESOURCEID"]); // 재작업 후 Resource ID
                            grdDefectCodeRouting2.View.SetFocusedRowCellValue("RETURNRESOURCENAME", dr["RESOURCENAME"]); // 재작업 후 작업장명
                            _reworkRoutingDt2.Clear();
                        };
                        (sender as DefectCancelRoutingPopup).ReworkRoutingDataEvent += (dt) =>
                        {
                            _reworkRoutingDt2 = dt;
                        };
                    }
                );

            inputSegment.AddTextBoxColumn("INPUTUSERSEQUENCE", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("USERSEQUENCE")
                .SetIsReadOnly(); // 투입 공정수순
            inputSegment.AddTextBoxColumn("INPUTSEGMENTNAME", 180)
                .SetLabel("PROCESSSEGMENTNAME")
                .SetIsReadOnly(); // 투입 공정명
            inputSegment.AddTextBoxColumn("INPUTRESOURCENAME", 220)
                .SetLabel("RESOURCENAME")
                .SetIsReadOnly(); // 투입 자원명

            var returnSegment = grdDefectCodeRouting2.View.AddGroupColumn("RETURNSEGMENT");

            returnSegment.AddTextBoxColumn("RETURNUSERSEQUENCE", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("USERSEQUENCE")
                .SetIsReadOnly(); // 재작업후 공정수순
            returnSegment.AddTextBoxColumn("RETURNSEGMENTNAME", 180)
                .SetLabel("PROCESSSEGMENTNAME")
                .SetIsReadOnly(); // 재작업후 공정명
            returnSegment.AddTextBoxColumn("RETURNRESOURCENAME", 220)
                .SetLabel("RESOURCENAME")
                .SetIsReadOnly(); // 재작업후 자원명

            var hidden = grdDefectCodeRouting2.View.AddGroupColumn("HIDDEN");

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
                .SetIsHidden(); // 제품 ID
            hidden.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                 .SetIsHidden(); // 제품 Version
            hidden.AddTextBoxColumn("REASONCONSUMABLEDEFID", 100)
                .SetIsHidden(); // 원인품목 ID
            hidden.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 100)
                .SetIsHidden(); // 원인품목 Version
            hidden.AddTextBoxColumn("REASONCONSUMABLELOTID", 100)
                .SetIsHidden(); // 원인자재 ID
            hidden.AddTextBoxColumn("REASONSEGMENTID", 100)
                .SetIsHidden(); // 원인공정 ID
            hidden.AddTextBoxColumn("REASONAREAID", 100)
                .SetIsHidden(); // 원인작업장 ID
            hidden.AddTextBoxColumn("PARENTLOTID", 100)
                .SetIsHidden(); // 부모 Lot No
            hidden.AddTextBoxColumn("LOTTYPE", 100)
                .SetIsHidden(); // 양산구분
            hidden.AddTextBoxColumn("TXNHISTKEY", 100)
                .SetIsHidden(); // TXNHISTKEY
            hidden.AddTextBoxColumn("PRODUCTIONORDERID", 100)
                .SetIsHidden(); // S/O번호
            hidden.AddTextBoxColumn("LINENO", 100)
                .SetIsHidden(); // Line No          
            hidden.AddTextBoxColumn("ISORIGINALLOTMERGECODE", 100)
                .SetIsHidden(); // Merge 가능여부 코드
            hidden.AddTextBoxColumn("INPUTPLANTID", 100)
                .SetIsHidden(); // 투입Site ID
            hidden.AddTextBoxColumn("INPUTPROCESSDEFID", 100)
                .SetIsHidden(); // 투입라우팅 ID
            hidden.AddTextBoxColumn("INPUTPROCESSDEFVERSION", 100)
                .SetIsHidden(); // 투입 라우팅 Version
            hidden.AddTextBoxColumn("INPUTPROCESSPATHID", 100)
                .SetIsHidden(); // 투입 라우팅 상세 ID
            hidden.AddTextBoxColumn("INPUTSEGMENTID", 100)
                .SetIsHidden(); // 투입공정 ID
            hidden.AddTextBoxColumn("INPUTSEGMENTVERSION", 100)
                .SetIsHidden(); // 투입 공정 Version
            hidden.AddTextBoxColumn("INPUTAREAID", 100)
                .SetIsHidden(); // 투입 작업장 ID
            hidden.AddTextBoxColumn("REPAIRLOTNO", 100)
                .SetIsHidden(); // 취소후 재생성 Lot ID
            hidden.AddTextBoxColumn("SUMLOTDEFECTQTY", 100)
                .SetIsHidden(); // Lot별 불량수량
            hidden.AddTextBoxColumn("DEFECTCODECOUNT", 100)
                .SetIsHidden(); // Lot별 불량코드갯수
            hidden.AddTextBoxColumn("PANELPERQTY", 100)
                .SetIsHidden(); // PNL별 PCS수량
            hidden.AddTextBoxColumn("RETURNPROCESSDEFID", 100)
                .SetIsHidden(); // 재작업 후 돌아갈 품목 라우팅 ID
            hidden.AddTextBoxColumn("RETURNPROCESSDEFVERSION", 100)
                .SetIsHidden(); // 재작업 후 돌아갈 품목 라우팅 Version
            hidden.AddTextBoxColumn("RETURNPROCESSPATHID", 100)
                .SetIsHidden(); // 재작업 후 라우팅 상제 정의 ID
            hidden.AddTextBoxColumn("RETURNSEGMENTID", 100)
                .SetIsHidden(); // 재작업 후 공정 ID
            hidden.AddTextBoxColumn("RETURNSEGMENTVERSION", 100)
                .SetIsHidden(); // 재작업 후 공정 Version
            hidden.AddTextBoxColumn("RETURNAREAID", 100)
                .SetIsHidden(); // 재작업 후 작업장 ID
            hidden.AddTextBoxColumn("QCSEGMENTID", 100)
                .SetIsHidden(); // 품질공정 ID
            hidden.AddTextBoxColumn("PROCESSSTATE", 100)
                .SetIsHidden(); // Lot의 공정진행상태
            hidden.AddTextBoxColumn("INPUTRESOURCEID", 100)
                .SetIsHidden(); // 투입 Resource ID
            hidden.AddTextBoxColumn("RETURNRESOURCEID", 100)
                .SetIsHidden(); // 재작업 후 Resource ID
            hidden.AddTextBoxColumn("WORKCOUNT", 100)
                .SetIsHidden(); // Lot Work Count
            hidden.AddTextBoxColumn("PARENTPROCESSDEFID", 100)
                .SetIsHidden(); // 부모 Lot의 라우팅 ID
            hidden.AddTextBoxColumn("PARENTPROCESSDEFVERSION", 100)
                .SetIsHidden(); // 부모 Lot의 라우팅 Version
            hidden.AddTextBoxColumn("ISMIGRATION", 100)
                .SetIsHidden(); // MIG여부
            hidden.AddTextBoxColumn("PROCESSPATHSTACK", 100)
                .SetIsHidden(); // 현재 불량의 ProcessPathId

            grdDefectCodeRouting2.View.PopulateColumns();

            grdDefectCodeRouting2.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        #endregion

        #region 불량 취소 금액 Initialize
        ///<summry>
        /// 불량 취소금액 Grid 초기화
        ///</summry>

        private void InitializeDefectAMTGrid()
        {
            grdDefectAMT.GridButtonItem = GridButtonItem.Export;

            var AMTInfo = grdDefectAMT.View.AddGroupColumn("AMTINFO");
            AMTInfo.AddTextBoxColumn("PRODUCTDEFID", 160)
                .SetIsReadOnly()       //  품목ID
                .SetTextAlignment(TextAlignment.Center);
            AMTInfo.AddTextBoxColumn("PRODUCTDEFNAME", 260)
                .SetIsReadOnly();       // 품목명
            AMTInfo.AddSpinEditColumn("INPUTPCSQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            AMTInfo.AddSpinEditColumn("INPUTPCSQTYAMT", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            AMTInfo.AddSpinEditColumn("DEFECTQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            AMTInfo.AddSpinEditColumn("DEFECTQTYAMT", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            AMTInfo.AddSpinEditColumn("QTY", 120)
                .SetLabel("GOODQTY")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            AMTInfo.AddSpinEditColumn("QTYAMT", 120)
                .SetLabel("GOODQTYAMT")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            AMTInfo.AddSpinEditColumn("YIELD", 100)
                .SetLabel("YIELD")
                .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            AMTInfo.AddTextBoxColumn("UNIT", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            AMTInfo.AddTextBoxColumn("CURRENCY", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            AMTInfo.AddSpinEditColumn("EXCHANGEVALUE", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);


            grdDefectAMT.View.PopulateColumns();

            grdDefectAMT.View.OptionsNavigation.AutoMoveRowFocus = true;


        }
        #endregion

        #region 불량 취소 금액 Detail Initialize
        ///<summry>
        /// 불량 취소 금액 Detail 초기화 .
        ///</summry>

        private void InitializeDefectAMTDetailGrid()
        {
            grdDefectAMTDetail.GridButtonItem = GridButtonItem.Export;

            var cancelInfo = grdDefectAMTDetail.View.AddGroupColumn("CANCELINFO");

            cancelInfo.AddTextBoxColumn("CANCELTIME", 160)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("CANCELDATE")
                .SetIsReadOnly();       //  불량품 폐기 취소 일시
            cancelInfo.AddTextBoxColumn("PRODUCTDEFID", 100)
                .SetIsReadOnly()       //  품목ID
                .SetTextAlignment(TextAlignment.Center);
            cancelInfo.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();       // 품목버전
            cancelInfo.AddTextBoxColumn("PRODUCTDEFNAME", 260)
                .SetIsReadOnly();       // 품목명
            cancelInfo.AddTextBoxColumn("USERSEQUENCE", 60)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            cancelInfo.AddTextBoxColumn("PROCESSSEGMENTNAME", 160)
                .SetIsReadOnly();
            cancelInfo.AddTextBoxColumn("AREANAME", 160)
                .SetIsReadOnly();
            cancelInfo.AddTextBoxColumn("REPAIRLOTNO", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            cancelInfo.AddTextBoxColumn("CANCELREASONNAME", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            cancelInfo.AddTextBoxColumn("ISSUER", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();

            var AMTInfo = grdDefectAMTDetail.View.AddGroupColumn("AMTINFO");

            AMTInfo.AddSpinEditColumn("INPUTPCSQTY", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            AMTInfo.AddSpinEditColumn("INPUTPCSQTYAMT", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            AMTInfo.AddSpinEditColumn("DEFECTQTY", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            AMTInfo.AddSpinEditColumn("DEFECTQTYAMT", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            AMTInfo.AddSpinEditColumn("QTY", 80)
                .SetLabel("GOODQTY")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            AMTInfo.AddSpinEditColumn("QTYAMT", 120)
                .SetLabel("GOODQTYAMT")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            AMTInfo.AddSpinEditColumn("YIELD", 120)
                .SetLabel("YIELD")
                .SetIsReadOnly()
                .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric)
                .SetTextAlignment(TextAlignment.Right);
            AMTInfo.AddTextBoxColumn("UNIT", 60)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            AMTInfo.AddSpinEditColumn("UNITPRICE", 60)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            AMTInfo.AddTextBoxColumn("CURRENCY", 60)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            AMTInfo.AddSpinEditColumn("EXCHANGEVALUE", 60)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);

            var LotInfo = grdDefectAMTDetail.View.AddGroupColumn("LOTINFO");
            LotInfo.AddTextBoxColumn("LOTID", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("DEFECTLOTID"); // 불량 LOT ID
            LotInfo.AddTextBoxColumn("PARENTLOTID", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);


            grdDefectAMTDetail.View.PopulateColumns();

            grdDefectAMTDetail.View.OptionsNavigation.AutoMoveRowFocus = false;


        }
        #endregion

        private void InitializationSummaryRow()
        {

            grdDefectAMT.View.Columns["PRODUCTDEFNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdDefectAMT.View.Columns["PRODUCTDEFNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdDefectAMT.View.Columns["INPUTPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdDefectAMT.View.Columns["INPUTPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdDefectAMT.View.Columns["INPUTPCSQTYAMT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdDefectAMT.View.Columns["INPUTPCSQTYAMT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdDefectAMT.View.Columns["DEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdDefectAMT.View.Columns["DEFECTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdDefectAMT.View.Columns["DEFECTQTYAMT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdDefectAMT.View.Columns["DEFECTQTYAMT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdDefectAMT.View.Columns["QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdDefectAMT.View.Columns["QTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdDefectAMT.View.Columns["QTYAMT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdDefectAMT.View.Columns["QTYAMT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdDefectAMT.View.OptionsView.ShowFooter = true;
            grdDefectAMT.ShowStatusBar = false;
        }

        #region 내역조회탭 Initialize

        /// <summary>        
        /// 취소한 불량코드 내역을 조회한다.
        /// </summary>
        private void InitializeDefectCodeCancelHistoryGrid()
        {
            grdCancelHistory.GridButtonItem = GridButtonItem.Export;

            var cancelInfo = grdCancelHistory.View.AddGroupColumn("CANCELINFO");

            cancelInfo.AddTextBoxColumn("CANCELTIME", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("CANCELDATE")
                .SetIsReadOnly(); // 취소일시
            cancelInfo.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetIsReadOnly(); // 품목 ID
            cancelInfo.AddTextBoxColumn("PRODUCTDEFNAME", 260)
                .SetIsReadOnly(); // 품목명
            cancelInfo.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 품목 Version
            cancelInfo.AddTextBoxColumn("PARENTLOTID", 200)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("Lot No"); // 취소 Parent Lot Id
            cancelInfo.AddTextBoxColumn("DEFECTCODE", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 취소 불량코드
            cancelInfo.AddTextBoxColumn("DEFECTCODENAME", 200)
                .SetIsReadOnly(); // 취소 불량코드명
            cancelInfo.AddTextBoxColumn("QCSEGMENTNAME", 150)
                .SetIsReadOnly(); // 품질공정명
            cancelInfo.AddSpinEditColumn("PCSQTY", 80)
                .SetLabel("PCS")
                .SetIsReadOnly(); // 취소 PCS 수량 
            //cancelInfo.AddSpinEditColumn("PANELQTY", 80)
            //    .SetLabel("PNL")
            //    .SetIsReadOnly(); // 취소 PNL 수량 
            cancelInfo.AddSpinEditColumn("USERSEQUENCE", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetIsReadOnly(); // 취소 공정순서
            cancelInfo.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetIsReadOnly(); // 취소 공정명
            cancelInfo.AddTextBoxColumn("AREANAME", 150)
                .SetIsReadOnly(); // 취소 작업장명
            cancelInfo.AddTextBoxColumn("PLANTID", 80)
                .SetIsReadOnly(); // 취소 Site
            cancelInfo.AddTextBoxColumn("LOTID", 200)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("DEFECTLOTID"); // 취소 불량 Lot Id

            var etc = grdCancelHistory.View.AddGroupColumn("ETC");

            etc.AddTextBoxColumn("REPAIRLOTNO", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("Repair Lot No")
                .SetIsReadOnly(); // 취소 Repair Lot Id
            etc.AddTextBoxColumn("CANCELREASON", 100)
                .SetLabel("REASONCANCEL")
                .SetIsReadOnly(); // 취소사유
            etc.AddTextBoxColumn("CANCELTYPE", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 취소타입
            etc.AddTextBoxColumn("CANCELUSER", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 취소 담당자
            etc.AddSpinEditColumn("UNIT", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("UOM")
                .SetIsReadOnly(); // UOM
            etc.AddTextBoxColumn("DESCRIPTION", 200)
                .SetIsReadOnly(); // 코멘트

            var hidden = grdCancelHistory.View.AddGroupColumn("HIDDEN");

            hidden.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetIsHidden(); // 취소 공정 ID
            hidden.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100)
                .SetIsHidden(); // 취소 공정 Version
            hidden.AddTextBoxColumn("AREAID", 100)
                .SetIsHidden(); // 취소 작업장 ID
            hidden.AddTextBoxColumn("PROCESSDEFID", 100)
                .SetIsHidden(); // 취소후 라우팅 ID
            hidden.AddTextBoxColumn("PROCESSDEFVERSION", 100)
                .SetIsHidden(); // 취소후 라우팅 Version
            hidden.AddTextBoxColumn("PROCESSDEFNAME", 100)
                .SetIsHidden(); // 취소후 라우팅명
            hidden.AddTextBoxColumn("PROCESSPATHID", 100)
                .SetIsHidden(); // 취소후 라우팅 상세 ID
            hidden.AddTextBoxColumn("CANCELROUTINGTYPE", 100)
                .SetIsHidden(); // 라우팅타입
            hidden.AddTextBoxColumn("TXNHISTKEY", 100)
                .SetIsHidden(); // 취소 Txn Hist Key
            hidden.AddTextBoxColumn("REQUESTNO", 100)
                .SetIsHidden(); // 요청번호
            hidden.AddTextBoxColumn("QCSEGMENTID", 100)
                .SetIsHidden(); // 품질공정 ID
            hidden.AddTextBoxColumn("RESOURCEID", 100)
                .SetIsHidden(); // 투입 자원 ID
            hidden.AddTextBoxColumn("RESOURCENAME", 100)
                .SetIsHidden(); // 투입 자원명
            hidden.AddTextBoxColumn("RETURNPROCESSPATHID", 100)
                .SetIsHidden(); // 재작업 후 라우팅 상세 ID
            hidden.AddTextBoxColumn("RETURNUSERSEQUENCE", 100)
                .SetIsHidden(); // 재작업 후 공정수순
            hidden.AddTextBoxColumn("RETURNPROCESSSEGMENTID", 100)
                .SetIsHidden(); // 재작업 후 공정 ID
            hidden.AddTextBoxColumn("RETURNPROCESSSEGMENTVERSION", 100)
                .SetIsHidden(); // 재작업 후 공정 Version
            hidden.AddTextBoxColumn("RETURNAREAID", 100)
                .SetIsHidden(); // 재작업 후 작업장 ID
            hidden.AddTextBoxColumn("RETURNRESOURCEID", 100)
                .SetIsHidden(); // 재작업 후 자원 ID
            hidden.AddTextBoxColumn("RETURNRESOURCENAME", 100)
                .SetIsHidden(); // 재작업 후 자원명
            hidden.AddTextBoxColumn("ISCOPYROUTING", 100)
                .SetIsHidden(); // 라우팅 복사 여부
            hidden.AddTextBoxColumn("INPUTSEGMENTID", 100)
                .SetIsHidden(); // 투입 공정 ID

            grdCancelHistory.View.PopulateColumns();

            grdCancelHistory.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        #endregion

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            btnDataUp.Click += BtnDataUp_Click;
            btnDataDown.Click += BtnDataDown_Click;

            btnDefectCancel.Click += BtnDefectCancel_Click;

            grdDefectCode.View.CheckStateChanged += View_CheckStateChanged;
            grdDefectCode.View.RowStyle += View_RowStyle;
            grdDefectCodeCnt.View.CellValueChanged += View_CellValueChanged1;
            grdDefectCodeCnt.View.ShowingEditor += View_ShowingEditor2;
            grdDefectCodeRouting.View.ShowingEditor += View_ShowingEditor;
            grdDefectCodeRouting.View.CellValueChanged += View_CellValueChanged;

            btnDataUp2.Click += BtnDataUp_Click;
            btnDataDown2.Click += BtnDataDown_Click;

            btnDefectMerge.Click += BtnDefectMerge_Click;

            grdDefectCode2.View.CheckStateChanged += View_CheckStateChanged1;
            grdDefectCode2.View.RowStyle += View_RowStyle;
            grdDefectCodeCnt2.View.CellValueChanged += View_CellValueChanged2;
            grdDefectCodeCnt2.View.ShowingEditor += View_ShowingEditor2;
            grdDefectCodeRouting2.View.ShowingEditor += View_ShowingEditor1;
            grdDefectCodeRouting2.View.CellValueChanged += View_CellValueChanged;

            grdDefectAMT.View.FocusedRowChanged += grdDefectAMT_FocusedRowChanged;

            grdCancelHistory.View.RowClick += View_RowClick;
        }

        /// <summary>
        /// 불량처리 단위에 따른 취소수량 ReadOnly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor2(object sender, CancelEventArgs e)
        {
            // 불량취소탭일때 
            if (tabDefectCancel.SelectedTabPage.Name == "tpgDefectCancel")
            {/*2020-03-04 강유라 임시 이벤트삭제
                if (grdDefectCodeCnt.View.FocusedColumn.FieldName == "CANCELPCSQTY")
                {
                    // PNL단위로 떨궜다면 취소 PCS컬럼 ReadOnly
                    if (grdDefectCodeCnt.View.GetFocusedRowCellValue("UOM").Equals("PNL"))
                    {
                        if (Format.GetInteger(grdDefectCodeCnt.View.GetFocusedRowCellValue("DEFECTPNLQTY")) != 0)
                        {
                            e.Cancel = true;
                        }
                    } 
                }
                else if (grdDefectCodeCnt.View.FocusedColumn.FieldName == "CANCELPNLQTY")
                {
                    // 불량 PNL 갯수가 0이라면 PNL컬럼 ReadOnly
                    if (Format.GetInteger(grdDefectCodeCnt.View.GetFocusedRowCellValue("DEFECTPNLQTY")) == 0)
                    {
                        e.Cancel = true;
                    }
                }*/
            }
            // 불량병합탭일때
            else
            {
                //if (grdDefectCodeCnt2.View.FocusedColumn.FieldName == "CANCELPCSQTY")
                //{
                //    // PNL단위로 떨궜다면 취소 PCS컬럼 ReadOnly
                //    if (grdDefectCodeCnt2.View.GetFocusedRowCellValue("UOM").Equals("PNL"))
                //    {
                //        if (Format.GetInteger(grdDefectCodeCnt2.View.GetFocusedRowCellValue("DEFECTPNLQTY")) != 0)
                //        {
                //            e.Cancel = true;
                //        }
                //    }
                //}
                //else if (grdDefectCodeCnt2.View.FocusedColumn.FieldName == "CANCELPNLQTY")
                //{
                //    // 불량 PNL 갯수가 0이라면 PNL컬럼 ReadOnly
                //    if (Format.GetInteger(grdDefectCodeCnt2.View.GetFocusedRowCellValue("DEFECTPNLQTY")) == 0)
                //    {
                //        e.Cancel = true;
                //    }
                //}
            }
        }

        /// <summary>
        /// 취소사유가 양품인계인 경우는 Lot의 공정진행상태가 작업완료일때만 가능하다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            // 불량취소탭일때 
            if (tabDefectCancel.SelectedTabPage.Name == "tpgDefectCancel")
            {
                if (e.Column.FieldName.Equals("CANCELCODE"))
                {
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTPROCESSDEFNAME", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTUSERSEQUENCE", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTSEGMENTNAME", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTRESOURCENAME", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNUSERSEQUENCE", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNSEGMENTNAME", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNRESOURCENAME", null);

                    if (e.Value.Equals("TakeoverGoods"))
                    {
                        string processState = grdDefectCodeRouting.View.GetRowCellValue(0, "PROCESSSTATE").ToString();

                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                        param.Add("LOTID", grdDefectCodeRouting.View.GetRowCellValue(0, "PARENTLOTID"));
                        param.Add("CANCELCODE", "TakeoverGoods");
                        param.Add("PROCESSDEFID", grdDefectCodeRouting.View.GetRowCellValue(0, "PARENTPROCESSDEFID"));
                        param.Add("PROCESSDEFVERSION", grdDefectCodeRouting.View.GetRowCellValue(0, "PARENTPROCESSDEFVERSION"));
                        param.Add("PROCESSSEGMENTID", grdDefectCodeRouting.View.GetRowCellValue(0, "PROCESSSEGMENTID"));

                        DataTable dt = SqlExecuter.Query("GetProductRoutingPreviousProcessPaths", "10002", param);

                        // Lot의 공정진행상태가 작업완료가 아니라면 양품인계가 불가능하다.
                        if (!processState.Equals("WaitForSend"))
                        {
                            ShowMessage("ProcessstateIsWaitForSendPossibleTakeoverGoods");
                            grdDefectCodeRouting.View.CellValueChanged -= View_CellValueChanged;
                            grdDefectCodeRouting.View.SetRowCellValue(0, "CANCELCODE", null);
                            grdDefectCodeRouting.View.CellValueChanged += View_CellValueChanged;
                        }

                        // 불량난 해당 공정이 마지막 공정이라면 양품인계가 불가능하다.
                        else if (dt.Rows.Count == 0)
                        {
                            ShowMessage("LastProcesssegmentIsNotTakeoverGoods");
                            grdDefectCodeRouting.View.CellValueChanged -= View_CellValueChanged;
                            grdDefectCodeRouting.View.SetRowCellValue(0, "CANCELCODE", null);
                            grdDefectCodeRouting.View.CellValueChanged += View_CellValueChanged;
                        }
                    }
                }
            }
            // 불량병합탭일때
            else
            {
                if (e.Column.FieldName.Equals("CANCELCODE"))
                {
                    grdDefectCodeRouting2.View.SetRowCellValue(0, "INPUTPROCESSDEFNAME", null);
                    grdDefectCodeRouting2.View.SetRowCellValue(0, "INPUTUSERSEQUENCE", null);
                    grdDefectCodeRouting2.View.SetRowCellValue(0, "INPUTSEGMENTNAME", null);
                    grdDefectCodeRouting2.View.SetRowCellValue(0, "INPUTRESOURCENAME", null);
                    grdDefectCodeRouting2.View.SetRowCellValue(0, "RETURNUSERSEQUENCE", null);
                    grdDefectCodeRouting2.View.SetRowCellValue(0, "RETURNSEGMENTNAME", null);
                    grdDefectCodeRouting2.View.SetRowCellValue(0, "RETURNRESOURCENAME", null);

                    if (e.Value.Equals("TakeoverGoods"))
                    {
                        string processState = grdDefectCodeRouting2.View.GetRowCellValue(0, "PROCESSSTATE").ToString();

                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                        param.Add("LOTID", grdDefectCodeRouting2.View.GetRowCellValue(0, "LOTID"));
                        param.Add("CANCELCODE", "TakeoverGoods");
                        param.Add("PROCESSDEFID", grdDefectCodeRouting2.View.GetRowCellValue(0, "PARENTPROCESSDEFID"));
                        param.Add("PROCESSDEFVERSION", grdDefectCodeRouting2.View.GetRowCellValue(0, "PARENTPROCESSDEFVERSION"));
                        param.Add("PROCESSSEGMENTID", grdDefectCodeRouting2.View.GetRowCellValue(0, "PROCESSSEGMENTID"));

                        DataTable dt = SqlExecuter.Query("GetProductRoutingPreviousProcessPaths", "10002", param);

                        // Lot의 공정진행상태가 작업완료가 아니라면 양품인계가 불가능하다.
                        if (!processState.Equals("WaitForSend"))
                        {
                            ShowMessage("ProcessstateIsWaitForSendPossibleTakeoverGoods");
                            grdDefectCodeRouting2.View.CellValueChanged -= View_CellValueChanged;
                            grdDefectCodeRouting2.View.SetRowCellValue(0, "CANCELCODE", null);
                            grdDefectCodeRouting2.View.CellValueChanged += View_CellValueChanged;
                        }

                        // 불량난 해당 공정이 마지막 공정이라면 양품인계가 불가능하다.
                        else if (dt.Rows.Count == 0)
                        {
                            ShowMessage("LastProcesssegmentIsNotTakeoverGoods");
                            grdDefectCodeRouting2.View.CellValueChanged -= View_CellValueChanged;
                            grdDefectCodeRouting2.View.SetRowCellValue(0, "CANCELCODE", null);
                            grdDefectCodeRouting2.View.CellValueChanged += View_CellValueChanged;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 라우팅 타입이 삭제되면 연관된 데이터 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefectCancel_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            // 불량취소탭일때
            if (tabDefectCancel.SelectedTabPage.Name == "tpgDefectCancel")
            {
                if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
                {
                    grdDefectCodeRouting.View.SetFocusedRowCellValue("INPUTUSERSEQUENCE", null);
                    grdDefectCodeRouting.View.SetFocusedRowCellValue("INPUTSEGMENTNAME", null);
                    grdDefectCodeRouting.View.SetFocusedRowCellValue("INPUTRESOURCENAME", null);
                    grdDefectCodeRouting.View.SetFocusedRowCellValue("RETURNUSERSEQUENCE", null);
                    grdDefectCodeRouting.View.SetFocusedRowCellValue("RETURNSEGMENTNAME", null);
                    grdDefectCodeRouting.View.SetFocusedRowCellValue("RETURNAREAID", null);
                    grdDefectCodeRouting.View.SetFocusedRowCellValue("RETURNRESOURCENAME", null);
                }
            }
            // 불량병합탭일때
            else
            {
                if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
                {
                    grdDefectCodeRouting2.View.SetFocusedRowCellValue("INPUTUSERSEQUENCE", null);
                    grdDefectCodeRouting2.View.SetFocusedRowCellValue("INPUTSEGMENTNAME", null);
                    grdDefectCodeRouting2.View.SetFocusedRowCellValue("INPUTRESOURCENAME", null);
                    grdDefectCodeRouting2.View.SetFocusedRowCellValue("RETURNUSERSEQUENCE", null);
                    grdDefectCodeRouting2.View.SetFocusedRowCellValue("RETURNSEGMENTNAME", null);
                    grdDefectCodeRouting2.View.SetFocusedRowCellValue("RETURNAREAID", null);
                    grdDefectCodeRouting2.View.SetFocusedRowCellValue("RETURNRESOURCENAME", null);
                }
            }
        }

        /// <summary>
        /// 내역조회 Row 더블클릭시 어떤 라우팅을 태웠는지 확인할 수 있는 팝업
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                pnlContent.ShowWaitArea();

                DefectRoutingPathPopup popup = new DefectRoutingPathPopup();
                popup.Owner = this;
                popup.CurrentDataRow = grdCancelHistory.View.GetFocusedDataRow();
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.ShowDialog();

                pnlContent.CloseWaitArea();
            }
        }

        /// <summary>
        ///   불량취소금액 Row 클릭시 해당 품목 상세 내역 조회 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdDefectAMT_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdDefectAMT.View.FocusedRowHandle < 0) return;

            DataRow drFocusRow = grdDefectAMT.View.GetFocusedDataRow();
            if (drFocusRow == null) return;




            var values = Conditions.GetValues();

            Dictionary<string, object> paramDetail = new Dictionary<string, object>();

            paramDetail.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            paramDetail.Add("P_PRODUCTDEFID", drFocusRow["PRODUCTDEFID"].ToString());
            //paramDetail.Add("VALIDSTATE", values["P_VALIDSTATE"].ToString());
            paramDetail.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            paramDetail.Add("P_PLNATID", values["P_PLANTID"].ToString());
            paramDetail.Add("P_PERIOD_PERIODFR", values["P_PERIOD_PERIODFR"].ToString().Substring(0, 10));
            paramDetail.Add("P_PERIOD_PERIODTO", values["P_PERIOD_PERIODTO"].ToString().Substring(0, 10));
            DataTable dtAmtDetailList = SqlExecuter.Query("GetDefectAMT", "10001", paramDetail);

            grdDefectAMTDetail.DataSource = dtAmtDetailList;

        }

        /// <summary>
        /// 취소 PCS 수량이 변할때마다 Total PCS 수량과 취소 PNL 수량을 계산한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            // 취소 PCS수량 입력할때
            if (e.Column.FieldName == "CANCELPCSQTY")
            {
                DataTable dt = (grdDefectCodeCnt.DataSource as DataTable);

                double totalCancelPcs = 0; // 취소할 총 불량PCS(전체)
                double totalCancelPnl = 0; // 취소할 총 불량PNL(전체)

                double cancelPcs = Convert.ToDouble(e.Value); // 취소할 불량수량(개별)
                double panelPerQty = Format.GetDouble(grdDefectCodeCnt.View.GetFocusedRowCellValue("PANELPERQTY"), 0); // PNL당 PCS수량(개별)
                double cancelPnl = panelPerQty == 0 ? 0 : Math.Ceiling(cancelPcs / panelPerQty); // 취소 PNL 수량(개별)
                dt.Rows[e.RowHandle]["CANCELPNLQTY"] = cancelPnl;

                // 모든 Row에 대한 취소 PCS수량의 합을 구한다.
                foreach (DataRow row in dt.Rows)
                {
                    totalCancelPcs += Convert.ToDouble(row["CANCELPCSQTY"]);
                    totalCancelPnl += Convert.ToDouble(row["CANCELPNLQTY"]);
                }

                // 취소할 불량수량이 총 불량수량보다 많다면 Exception
                if (cancelPcs > Convert.ToInt32(grdDefectCodeCnt.View.GetFocusedRowCellValue("DEFECTPCSQTY")))
                {
                    this.ShowMessage("CancelGreatThenDefect"); // 취소할 수량이 불량수량보다 많습니다.
                    dt.Rows[e.RowHandle]["CANCELPCSQTY"] = 0;
                    dt.Rows[e.RowHandle]["CANCELPNLQTY"] = 0;

                    // 모든 Row에 대한 취소 PCS수량과 PNL수량의 합을 다시 구한다.
                    totalCancelPcs = 0;
                    totalCancelPnl = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        totalCancelPcs += Convert.ToDouble(row["CANCELPCSQTY"]);
                        totalCancelPnl += Convert.ToDouble(row["CANCELPNLQTY"]);
                    }
                    txtPcsCnt.EditValue = totalCancelPcs; // 취소할 총 불량 PCS
                    txtPnlCnt.EditValue = totalCancelPnl; // 취소할 총 불량 PNL
                    return;
                }

                txtPcsCnt.EditValue = totalCancelPcs; // 취소할 총 불량 PCS
                txtPnlCnt.EditValue = totalCancelPnl; // 취소할 총 불량 PNL

                // 해당 Lot 완불처리 Check
                if (IsAllQtyDefectLot(Format.GetString(dt.Rows[0]["LOTID"]), Convert.ToDouble(txtPcsCnt.EditValue)).Equals("Y"))
                {
                    // 완불처리된 Lot의 전량취소이므로 원래 Lot에 원복처리됩니다.
                    this.ShowMessage("AllDefectLotAllCancelisOriginalLotRestore");

                    grdDefectCodeRouting.View.SetRowCellValue(0, "CHECK", true);

                    grdDefectCodeRouting.View.SetRowCellValue(0, "CANCELCODE", "Retest");
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTPROCESSDEFID", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTPROCESSDEFVERSION", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTPROCESSDEFNAME", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTPROCESSPATHID", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTUSERSEQUENCE", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTSEGMENTID", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTSEGMENTVERSION", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTSEGMENTNAME", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTAREAID", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTRESOURCEID", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTRESOURCENAME", null);

                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNPROCESSDEFID", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNPROCESSDEFVERSION", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNPROCESSPATHID", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNUSERSEQUENCE", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNSEGMENTID", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNSEGMENTVERSION", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNSEGMENTNAME", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNAREAID", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNRESOURCEID", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNRESOURCENAME", null);

                    _reworkRoutingDt.Clear();
                }
                else
                {
                    grdDefectCodeRouting.View.SetRowCellValue(0, "CHECK", false);
                }
            }
            // 취소 PNL 수량 입력할때
            else if (e.Column.FieldName == "CANCELPNLQTY")
            {
                DataTable dt = (grdDefectCodeCnt.DataSource as DataTable);

                double totalCancelPcs = 0; // 취소할 총 불량PCS(전체)
                double totalCancelPnl = 0; // 취소할 총 불량PNL(전체)

                double cancelPnl = Convert.ToDouble(e.Value); // 취소할 불량수량(개별)
                double panelPerQty = Format.GetDouble(grdDefectCodeCnt.View.GetFocusedRowCellValue("PANELPERQTY"), 0); // PNL당 PCS수량(개별)
                double cancelPcs = 0;

                // 취소할 불량수량이 총 불량수량보다 많다면 Exception
                if (Convert.ToInt32(grdDefectCodeCnt.View.GetFocusedRowCellValue("CANCELPNLQTY")) > Convert.ToInt32(grdDefectCodeCnt.View.GetFocusedRowCellValue("DEFECTPNLQTY")))
                {
                    this.ShowMessage("CancelGreatThenDefect"); // 취소할 수량이 불량수량보다 많습니다.
                    dt.Rows[e.RowHandle]["CANCELPCSQTY"] = 0;
                    dt.Rows[e.RowHandle]["CANCELPNLQTY"] = 0;

                    // 모든 Row에 대한 취소 PCS수량과 PNL수량의 합을 다시 구한다.
                    totalCancelPcs = 0;
                    totalCancelPnl = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        totalCancelPcs += Convert.ToDouble(row["CANCELPCSQTY"]);
                        totalCancelPnl += Convert.ToDouble(row["CANCELPNLQTY"]);
                    }
                    txtPcsCnt.EditValue = totalCancelPcs; // 취소할 총 불량 PCS
                    txtPnlCnt.EditValue = totalCancelPnl; // 취소할 총 불량 PNL
                    return;
                }

                if (panelPerQty * cancelPnl > Convert.ToDouble(dt.Rows[e.RowHandle]["DEFECTPCSQTY"]))
                {
                    cancelPcs = Convert.ToDouble(dt.Rows[e.RowHandle]["DEFECTPCSQTY"]);
                    dt.Rows[e.RowHandle]["CANCELPCSQTY"] = cancelPcs;
                }
                else
                {
                    cancelPcs = cancelPnl * panelPerQty; // 취소 PCS 수량(개별)
                    dt.Rows[e.RowHandle]["CANCELPCSQTY"] = cancelPcs;
                }

                // 모든 Row에 대한 취소 PCS수량과 PNL수량의 합을 구한다.
                foreach (DataRow row in dt.Rows)
                {
                    totalCancelPcs += Convert.ToDouble(row["CANCELPCSQTY"]);
                    totalCancelPnl += Convert.ToDouble(row["CANCELPNLQTY"]);
                }

                txtPcsCnt.EditValue = totalCancelPcs; // 취소할 총 불량 PCS
                txtPnlCnt.EditValue = totalCancelPnl; // 취소할 총 불량 PNL

                // 해당 Lot 완불처리 Check
                if (IsAllQtyDefectLot(Format.GetString(dt.Rows[0]["LOTID"]), Convert.ToDouble(txtPcsCnt.EditValue)).Equals("Y"))
                {
                    // 완불처리된 Lot의 전량취소이므로 원래 Lot에 원복처리됩니다.
                    this.ShowMessage("AllDefectLotAllCancelisOriginalLotRestore");

                    grdDefectCodeRouting.View.SetRowCellValue(0, "CHECK", true);

                    grdDefectCodeRouting.View.SetRowCellValue(0, "CANCELCODE", "Retest");
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTPROCESSDEFID", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTPROCESSDEFVERSION", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTPROCESSDEFNAME", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTPROCESSPATHID", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTUSERSEQUENCE", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTSEGMENTID", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTSEGMENTVERSION", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTSEGMENTNAME", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTAREAID", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTRESOURCEID", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "INPUTRESOURCENAME", null);

                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNPROCESSDEFID", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNPROCESSDEFVERSION", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNPROCESSPATHID", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNUSERSEQUENCE", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNSEGMENTID", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNSEGMENTVERSION", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNSEGMENTNAME", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNAREAID", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNRESOURCEID", null);
                    grdDefectCodeRouting.View.SetRowCellValue(0, "RETURNRESOURCENAME", null);

                    _reworkRoutingDt.Clear();
                }
                else
                {
                    grdDefectCodeRouting.View.SetRowCellValue(0, "CHECK", false);
                }
            }
        }

        /// <summary>
        /// 취소 PCS 수량이 변할때마다 Total PCS 수량과 취소 PNL 수량을 계산한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged2(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            // 취소 PCS수량 입력할때
            if (e.Column.FieldName == "CANCELPCSQTY")
            {
                DataTable dt = (grdDefectCodeCnt2.DataSource as DataTable);

                double totalCancelPcs = 0; // 취소할 총 불량PCS(전체)
                double totalCancelPnl = 0; // 취소할 총 불량PNL(전체)

                double cancelPcs = Convert.ToDouble(e.Value); // 취소할 불량수량(개별)
                double panelPerQty = Format.GetDouble(grdDefectCodeCnt2.View.GetFocusedRowCellValue("PANELPERQTY"), 0); // PNL당 PCS수량(개별)
                double cancelPnl = panelPerQty == 0 ? 0 : Math.Ceiling(cancelPcs / panelPerQty); // 취소 PNL 수량(개별)
                dt.Rows[e.RowHandle]["CANCELPNLQTY"] = cancelPnl;

                // 모든 Row에 대한 취소 PCS수량과 PNL수량의 합을 구한다.
                foreach (DataRow row in dt.Rows)
                {
                    totalCancelPcs += Convert.ToDouble(row["CANCELPCSQTY"]);
                    totalCancelPnl += Convert.ToDouble(row["CANCELPNLQTY"]);
                }

                // 취소할 불량수량이 총 불량수량보다 많다면 Exception
                if (cancelPcs > Convert.ToInt32(grdDefectCodeCnt2.View.GetFocusedRowCellValue("DEFECTPCSQTY")))
                {
                    this.ShowMessage("CancelGreatThenDefect"); // 취소할 수량이 불량수량보다 많습니다.
                    dt.Rows[e.RowHandle]["CANCELPCSQTY"] = 0;
                    dt.Rows[e.RowHandle]["CANCELPNLQTY"] = 0;

                    // 모든 Row에 대한 취소 PCS수량과 PNL수량의 합을 다시 구한다.
                    totalCancelPcs = 0;
                    totalCancelPnl = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        totalCancelPcs += Convert.ToDouble(row["CANCELPCSQTY"]);
                        totalCancelPnl += Convert.ToDouble(row["CANCELPNLQTY"]);
                    }
                    txtPcsCnt2.EditValue = totalCancelPcs; // 취소할 총 불량 PCS
                    txtPnlCnt2.EditValue = totalCancelPnl; // 취소할 총 불량 PNL
                    return;
                }

                txtPcsCnt2.EditValue = totalCancelPcs; // 취소할 총 불량 PCS
                txtPnlCnt2.EditValue = totalCancelPnl; // 취소할 총 불량 PNL
            }
            // 취소 PNL 수량 입력할때
            else if (e.Column.FieldName == "CANCELPNLQTY")
            {
                DataTable dt = (grdDefectCodeCnt2.DataSource as DataTable);

                double totalCancelPcs = 0; // 취소할 총 불량PCS(전체)
                double totalCancelPnl = 0; // 취소할 총 불량PNL(전체)

                double cancelPnl = Convert.ToDouble(e.Value); // 취소할 불량수량(개별)
                double panelPerQty = Format.GetDouble(grdDefectCodeCnt2.View.GetFocusedRowCellValue("PANELPERQTY"), 0); // PNL당 PCS수량(개별)
                double cancelPcs = 0;

                // 취소할 불량수량이 총 불량수량보다 많다면 Exception
                if (Convert.ToInt32(grdDefectCodeCnt2.View.GetFocusedRowCellValue("CANCELPNLQTY")) > Convert.ToInt32(grdDefectCodeCnt2.View.GetFocusedRowCellValue("DEFECTPNLQTY")))
                {
                    this.ShowMessage("CancelGreatThenDefect"); // 취소할 수량이 불량수량보다 많습니다.
                    dt.Rows[e.RowHandle]["CANCELPCSQTY"] = 0;
                    dt.Rows[e.RowHandle]["CANCELPNLQTY"] = 0;

                    // 모든 Row에 대한 취소 PCS수량과 PNL수량의 합을 다시 구한다.
                    totalCancelPcs = 0;
                    totalCancelPnl = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        totalCancelPcs += Convert.ToDouble(row["CANCELPCSQTY"]);
                        totalCancelPnl += Convert.ToDouble(row["CANCELPNLQTY"]);
                    }
                    txtPcsCnt2.EditValue = totalCancelPcs; // 취소할 총 불량 PCS
                    txtPnlCnt2.EditValue = totalCancelPnl; // 취소할 총 불량 PNL
                    return;
                }

                if (panelPerQty * cancelPnl > Convert.ToDouble(dt.Rows[e.RowHandle]["DEFECTPCSQTY"]))
                {
                    cancelPcs = Convert.ToDouble(dt.Rows[e.RowHandle]["DEFECTPCSQTY"]);
                    dt.Rows[e.RowHandle]["CANCELPCSQTY"] = cancelPcs;
                }
                else
                {
                    cancelPcs = cancelPnl * panelPerQty; // 취소 PCS 수량(개별)
                    dt.Rows[e.RowHandle]["CANCELPCSQTY"] = cancelPcs;
                }

                // 모든 Row에 대한 취소 PCS수량과 PNL수량의 합을 구한다.
                foreach (DataRow row in dt.Rows)
                {
                    totalCancelPcs += Convert.ToDouble(row["CANCELPCSQTY"]);
                    totalCancelPnl += Convert.ToDouble(row["CANCELPNLQTY"]);
                }

                txtPcsCnt2.EditValue = totalCancelPcs; // 취소할 총 불량 PCS
                txtPnlCnt2.EditValue = totalCancelPnl; // 취소할 총 불량 PNL
            }
        }

        /// <summary>
        /// 불량병합요청시 다른 작업장은 선택할 수 없다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CheckStateChanged1(object sender, EventArgs e)
        {
            DataTable dt = grdDefectCode2.View.GetCheckedRows();

            // 작업장 Id 체크
            int areaCount = dt.AsEnumerable().Select(r => r.Field<string>("AREAID")).Distinct().Count();
            // 공정 체크
            int segmentCount = dt.AsEnumerable().Select(r => r.Field<string>("PROCESSSEGMENTID")).Distinct().Count();
            // 품목 체크
            int productCount = dt.AsEnumerable().Select(r => new { productId = r.Field<string>("PRODUCTDEFID"), productVersion = r.Field<string>("PRODUCTDEFVERSION") }).Distinct().Count();

            if (areaCount > 1)
            {
                grdDefectCode2.View.CheckRow(grdDefectCode2.View.GetFocusedDataSourceRowIndex(), false);

                // 다른 작업장은 선택할 수 없습니다.
                throw MessageException.Create("MixSelectAreaId");
            }
            else if (segmentCount > 1)
            {
                grdDefectCode2.View.CheckRow(grdDefectCode2.View.GetFocusedDataSourceRowIndex(), false);

                // 다른 공정은 선택할 수 없습니다.
                throw MessageException.Create("MixSelectSegmentId");
            }
            else if (productCount > 1)
            {
                grdDefectCode2.View.CheckRow(grdDefectCode2.View.GetFocusedDataSourceRowIndex(), false);

                // 다른 품목은 선택할 수 없습니다.
                throw MessageException.Create("MixSelectProduct");
            }
        }

        /// <summary>
        /// Check한 Row색깔변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            // 불량취소탭일때
            if (tabDefectCancel.SelectedTabPage.Name == "tpgDefectCancel")
            {
                if (e.RowHandle < 0) return;
                bool isChecked = grdDefectCode.View.IsRowChecked(e.RowHandle);

                if (isChecked)
                {
                    e.Appearance.BackColor = Color.FromArgb(254, 254, 16);
                    e.HighPriority = true;
                }
            }
            // 불량병합탭일때
            else
            {
                if (e.RowHandle < 0) return;
                bool isChecked = grdDefectCode2.View.IsRowChecked(e.RowHandle);

                if (isChecked)
                {
                    e.Appearance.BackColor = Color.FromArgb(254, 254, 16);
                    e.HighPriority = true;
                }
            }
        }

        /// <summary>
        /// 같은 Lot No이 아니면 Grid Row Down을 막는다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            DataTable dt = grdDefectCode.View.GetCheckedRows();

            // Lot 체크
            int lotCount = dt.AsEnumerable().Select(r => r.Field<string>("LOTID")).Distinct().Count();
            // 품목 체크
            int productCount = dt.AsEnumerable().Select(r => new { productId = r.Field<string>("PRODUCTDEFID"), productVersion = r.Field<string>("PRODUCTDEFVERSION") }).Distinct().Count();

            if (lotCount > 1)
            {
                grdDefectCode.View.CheckRow(grdDefectCode.View.GetFocusedDataSourceRowIndex(), false);

                // 다른 Lot ID는 선택할 수 없습니다.
                throw MessageException.Create("MixSelectLotID");
            }
            else if (productCount > 1)
            {
                grdDefectCode.View.CheckRow(grdDefectCode.View.GetFocusedDataSourceRowIndex(), false);

                // 다른 품목은 선택할 수 없습니다.
                throw MessageException.Create("MixSelectProudct");
            }
        }

        /// <summary>
        /// Original Lot Merge가 가능이면 활성화 불가능이면 비활성화(불량취소탭)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            // Original Lot Merge가 불가능하면 체크박스 비활성화
            if (grdDefectCodeRouting.View.FocusedColumn.FieldName == "CHECK")
            {
                if (grdDefectCodeRouting.View.GetFocusedRowCellValue("ISORIGINALLOTMERGECODE").ToString() == "N")
                {
                    e.Cancel = true;
                }
            }

            // Original Lot Merge를 할 경우 Row 비활성화
            if (grdDefectCodeRouting.View.FocusedColumn.FieldName != "CHECK"
                && grdDefectCodeRouting.View.GetFocusedRowCellValue("CHECK").Equals(true))
            {
                e.Cancel = true;
                throw MessageException.Create("OriginalLotMergeNotSelect");
            }

            // 취소사유가 선택되지 않았다면 라우팅 선택팝업 비활성화
            if (grdDefectCodeRouting.View.FocusedColumn.FieldName == "INPUTPROCESSDEFNAME")
            {
                // 취소사유가 선택되지 않았다면 Exception
                if (string.IsNullOrWhiteSpace(grdDefectCodeRouting.View.GetFocusedRowCellValue("CANCELCODE").ToString()))
                {
                    e.Cancel = true;
                    throw MessageException.Create("CancelReasonSelectAfterRouting");
                }
            }
        }

        /// <summary>
        /// 취소사유나 라우팅타입이 입력되지 않았다면 라우팅 선택 불가능(불량병합탭)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor1(object sender, CancelEventArgs e)
        {
            // 취소사유가 선택되지 않았다면 라우팅 선택팝업 비활성화
            if (grdDefectCodeRouting2.View.FocusedColumn.FieldName == "INPUTPROCESSDEFNAME")
            {
                // 취소사유가 선택되지 않았다면 Exception
                if (string.IsNullOrWhiteSpace(grdDefectCodeRouting2.View.GetFocusedRowCellValue("CANCELCODE").ToString()))
                {
                    e.Cancel = true;
                    throw MessageException.Create("CancelReasonSelectAfterRouting");
                }
            }
        }

        /// <summary>
        /// 불량취소버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDefectCancel_Click(object sender, EventArgs e)
        {
            DataTable dt1 = (grdDefectCodeCnt.DataSource as DataTable);
            dt1.TableName = "list1";
            DataTable dt2 = (grdDefectCodeRouting.DataSource as DataTable);
            dt1.TableName = "list2";
            _reworkRoutingDt.TableName = "list3";

            // 품목라우팅을 태우는 경우에는 사용자 지정 재작업라우팅 테이블을 초기화해준다.
            if ((grdDefectCodeRouting.DataSource as DataTable).Rows[0]["ROUTINGTYPE"].Equals("Product"))
            {
                _reworkRoutingDt.Clear();
            }
            else
            {
                // Lot No 삽입
                foreach (DataRow row in _reworkRoutingDt.Rows)
                {
                    row["LOTID"] = txtDefectLotNo.EditValue;
                }
            }

            // 저장할 데이터가 없다면 Exception
            if (dt1.Rows.Count == 0)
            {
                throw MessageException.Create("GridNoData");
            }
            // 저장할 총 취소수량이 0이라면 Exception
            else if (Convert.ToInt32(txtPcsCnt.EditValue) == 0)
            {
                throw MessageException.Create("CancelQtyIsZero");
            }
            // 취소할 불량중에 취소수량이 0인것이 있다면 Exception
            else if (dt1.AsEnumerable().Where(r => r["CANCELPCSQTY"].Equals(0)).Count() > 0)
            {
                throw MessageException.Create("CancelDefectIsZero");
            }
            // Original Lot Merge가 아닌데, 라우팅이 선택되지 않았다면 Exception
            else if (dt2.Rows[0]["CHECK"].Equals(false)
                    && (string.IsNullOrWhiteSpace(dt2.Rows[0]["INPUTPROCESSDEFNAME"].ToString()) && string.IsNullOrWhiteSpace(dt2.Rows[0]["INPUTPROCESSDEFID"].ToString())))
            {
                throw MessageException.Create("NecessaryRouting");
            }

            if (this.ShowMessage(MessageBoxButtons.YesNo, "DoDefectCancel") == DialogResult.Yes)
            {
                Dictionary<string, object> param = new Dictionary<string, object>()
                {
                    { "LOTID", dt1.Rows[0]["LOTID"] },
                    { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                    { "CANCELCODE", "TakeoverGoods" },
                    { "PROCESSDEFID", dt1.Rows[0]["PARENTPROCESSDEFID"] },
                    { "PROCESSDEFVERSION", dt1.Rows[0]["PARENTPROCESSDEFVERSION"] },
                    { "PROCESSSEGMENTID", dt1.Rows[0]["PROCESSSEGMENTID"] },
                };

                DataTable dt = SqlExecuter.Query("GetProductRoutingPreviousProcessPaths", "10002", param); // 다음공정이 있는지 체크
                Boolean isNextSegment = dt.Rows.Count == 0 ? false : true;


                DataTable totalDefectQtyDt = SqlExecuter.Query("GetTotalLotDefect", "10001", param);
                double totalDefectQty = Convert.ToDouble(totalDefectQtyDt.Rows[0]["TOTALDEFECTQTY"]); // 해당 Lot의 불량 총수량

                //   DataTable totalDefectQtyDt2 = grdDefectCodeCnt.DataSource as DataTable;
                //  double totalDefectQty = Convert.ToDouble(totalDefectQtyDt2.Rows[0]["DEFECTPCSQTY"].ToString()); // 해당 Lot의 불량 수량

                MessageWorker worker = new MessageWorker("SaveLotDefectCancel");
                worker.SetBody(new MessageBody()
                {
                    { "cancelUser", UserInfo.Current.Id }, // 취소자
                    { "cancelDate", DateTime.Now }, // 취소일시
                    { "lotId", dt1.Rows[0]["LOTID"] }, // Lot ID
                    { "parentLotId", dt1.Rows[0]["PARENTLOTID"] }, // 모 Lot ID
                    { "productDefId", dt1.Rows[0]["PRODUCTDEFID"] }, // 품목 ID
                    { "productDefVersion", dt1.Rows[0]["PRODUCTDEFVERSION"]}, // 품목 Version
                    { "panelPerQty", dt1.Rows[0]["PANELPERQTY"] }, // PNL당 PCS수량
                    { "totalDefectQty", totalDefectQty }, // Lot에 대한 불량 총수량                    
                    { "cancelDefectQty", txtPcsCnt.EditValue }, // Lot에 대한 취소할 불량 총 PCS
                    { "cancelDefectPnl", txtPnlCnt.EditValue }, // Lot에 대한 취소할 불량 총 PNL
                    { "isNextSegment", isNextSegment }, // 다음공정이 있는지 유무
                    { "list1", dt1 }, // 취소할 불량코드 데이터테이블
                    { "list2", dt2}, // 취소할 Lot 데이터테이블
                    { "list3", _reworkRoutingDt}, // 재작업 라우팅을 태우는 경우 선택된 라우팅 상세정의
                });

                var result = worker.Execute<DataTable>();
                var resultData = result.GetResultSet();

                // Split할 불량이 하나도 없다면 Repiar Lot 생성하지 않음
                if (dt2.AsEnumerable().Where(r => Convert.ToBoolean(r["CHECK"]) == false).Count() == 0)
                {
                    this.ShowMessage("CompleteDefectCancel"); // 불량 취소되었습니다.
                    this.AllSearch();
                }
                else
                {
                    this.ShowMessage("CompleteDefectCancelParam"
                                    , resultData.Rows[0]["LOTID"].ToString()
                                    , resultData.Rows[0]["QTY"].ToString());

                    this.AllSearch();

                    // 품목라우팅일때
                    if (_reworkRoutingDt.Rows.Count == 0)
                    {
                        CommonFunction.PrintLotCard_Ver2(resultData.Rows[0]["LOTID"].ToString(), LotCardType.Normal);
                    }
                    // 재작업라우팅일때
                    else
                    {
                        CommonFunction.PrintLotCard_Ver2(resultData.Rows[0]["LOTID"].ToString(), LotCardType.Normal);
                        CommonFunction.PrintLotCard_Ver2(resultData.Rows[0]["LOTID"].ToString(), LotCardType.Rework);
                    }
                }
            }
        }

        /// <summary>
        /// 병합요청버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDefectMerge_Click(object sender, EventArgs e)
        {
            DataTable dt1 = (grdDefectCodeCnt2.DataSource as DataTable); // 병합되어질 Lot 정보
            dt1.TableName = "list1";
            DataTable dt2 = (grdDefectCodeRouting2.DataSource as DataTable); // 병합될 Lot 정보(요청정보)
            dt2.TableName = "list2";
            _reworkRoutingDt2.TableName = "list3";

            // 저장할 데이터가 없다면 Exception
            if (dt2 == null)
            {
                // 저장할 데이터가 없습니다.
                throw MessageException.Create("GridNoData");
            }
            // 취소할 총 수량이 0이라면 Exception
            else if (Convert.ToInt32(txtPcsCnt2.EditValue) == 0)
            {
                throw MessageException.Create("CancelQtyIsZero");
            }
            // 취소할 불량중에 취소수량이 0인것이 있다면 Exception
            else if (dt1.AsEnumerable().Where(r => r["CANCELPCSQTY"].Equals(0)).Count() > 0)
            {
                throw MessageException.Create("CancelDefectIsZero");
            }
            // Merge할 Lot이 한가지라면 Exception
            else if (dt1.AsEnumerable().Select(r => r.Field<string>("LOTID")).Distinct().Count() == 1)
            {
                throw MessageException.Create("TwoMorethenLotMerge");
            }
            // 라우팅이 선택되지 않았다면 Exception
            else if (string.IsNullOrWhiteSpace(dt2.Rows[0]["INPUTPROCESSDEFNAME"].ToString()) && string.IsNullOrWhiteSpace(dt2.Rows[0]["INPUTPROCESSDEFID"].ToString()))
            {
                throw MessageException.Create("NecessaryRouting");
            }
            else
            {
                if (this.ShowMessage(MessageBoxButtons.YesNo, "DoDefectMergeRequest") == DialogResult.Yes)
                {
                    MessageWorker worker = new MessageWorker("SaveLotDefectMergeRequest");
                    worker.SetBody(new MessageBody()
                    {
                        { "requestUser", UserInfo.Current.Id }, // 요청자
                        { "requestDate", DateTime.Now }, // 요청일시   
                        { "requestComment", memoComment.Text }, // 요청 Comment
                        { "productDefId", dt1.Rows[0]["PRODUCTDEFID"] }, // 품목 ID
                        { "productDefVersion", dt1.Rows[0]["PRODUCTDEFVERSION"]}, // 품목 Version
                        { "cancelDefectPcsQty", txtPcsCnt2.EditValue }, // 병합해서 생길 Lot의 PCS수량
                        { "cancelDefectPnlQty", txtPnlCnt2.EditValue }, // 병합해서 생길 Lot의 PNL수량
                        { "list1", dt1 }, // 병합되어질 Lot 정보(불량코드별)
                        { "list2", dt2 }, // 병합될 Lot 정보(요청정보)
                        { "list3", _reworkRoutingDt2}, // 재작업 라우팅을 태우는 경우 선택된 라우팅 상세정의
                    });

                    worker.Execute();

                    this.ShowMessage("CompleteDefectLotMergeRequest"); // 병합 요청되었습니다.
                    this.AllSearch();
                }
            }
        }

        /// <summary>
        /// Data Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDataDown_Click(object sender, EventArgs e)
        {
            if (tabDefectCancel.SelectedTabPage.Name == "tpgDefectCancel")
            {
                SetDataDown(grdDefectCode, grdDefectCodeCnt, grdDefectCodeRouting);
                SetCalcCount();
            }
            else if (tabDefectCancel.SelectedTabPage.Name == "tpgDefectMerge")
            {
                SetDataDown(grdDefectCode2, grdDefectCodeCnt2, grdDefectCodeRouting2);
                SetCalcCount();
            }
        }

        /// <summary>
        /// Data Up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDataUp_Click(object sender, EventArgs e)
        {
            if (tabDefectCancel.SelectedTabPage.Name == "tpgDefectCancel")
            {
                SetDataUp(grdDefectCodeCnt, grdDefectCodeRouting, grdDefectCode);
                SetCalcCount();
            }
            else if (tabDefectCancel.SelectedTabPage.Name == "tpgDefectMerge")
            {
                SetDataUp(grdDefectCodeCnt2, grdDefectCodeRouting2, grdDefectCode2);
                SetCalcCount();
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
            //DataTable changed = grdList.GetChangedRows();

            //ExecuteRule("SaveCodeClass", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            SmartSelectPopupEdit productPopup = Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID");
            SmartTextBox lotBox = Conditions.GetControl<SmartTextBox>("P_LOTID");

            string product = Format.GetString(productPopup.GetValue());
            string lot = Format.GetString(lotBox.EditValue);

            // 불량취소탭일때
            if (tabDefectCancel.SelectedTabPage.Name == "tpgDefectCancel")
            {
                //if (string.IsNullOrWhiteSpace(product) && string.IsNullOrWhiteSpace(lot))
                //{
                //    grdDefectCode.View.ClearDatas();
                //    grdDefectCodeCnt.View.ClearDatas();
                //    grdDefectCodeRouting.View.ClearDatas();
                //    throw MessageException.Create("RequiredInputLotOrProduct"); // Lot No와 품목중 한가지는 필수로 입력해야합니다.
                //}

                DataTable dt = await SqlExecuter.QueryAsync("GetLotDefectCode", "10001", values);

                if (dt.Rows.Count == 0)
                {
                    this.ShowMessage("NoSelectData");
                    grdDefectCode.DataSource = null;
                    grdDefectCodeCnt.DataSource = null;
                    grdDefectCodeRouting.DataSource = null;
                    return;
                }

                grdDefectCode.DataSource = dt;
                grdDefectCodeCnt.DataSource = null;
                grdDefectCodeRouting.DataSource = null;

                txtProductId.EditValue = null;
                txtProductName.EditValue = null;
                txtDefectLotNo.EditValue = null;
                txtLotNo.EditValue = null;
                txtPcsCnt.EditValue = null;
                txtPnlCnt.EditValue = null;
            }
            // 불량병합탭일때
            else if (tabDefectCancel.SelectedTabPage.Name == "tpgDefectMerge")
            {
                //if (string.IsNullOrWhiteSpace(product) && string.IsNullOrWhiteSpace(lot))
                //{
                //    grdDefectCode2.View.ClearDatas();
                //    grdDefectCodeCnt2.View.ClearDatas();
                //    grdDefectCodeRouting2.View.ClearDatas();
                //    throw MessageException.Create("RequiredInputLotOrProduct"); // Lot No와 품목중 한가지는 필수로 입력해야합니다.
                //}

                DataTable dt = await SqlExecuter.QueryAsync("GetLotDefectCode", "10001", values);

                if (dt.Rows.Count == 0)
                {
                    this.ShowMessage("NoSelectData");
                    grdDefectCode2.DataSource = null;
                    grdDefectCodeCnt2.DataSource = null;
                    grdDefectCodeRouting2.DataSource = null;
                    memoComment.Text = null;
                    return;
                }

                grdDefectCode2.DataSource = dt;
                grdDefectCodeCnt2.DataSource = null;
                grdDefectCodeRouting2.DataSource = null;

                txtProductId2.EditValue = null;
                txtProductName2.EditValue = null;
                txtLotCnt.EditValue = null;
                txtPcsCnt2.EditValue = null;
                txtPnlCnt2.EditValue = null;
                memoComment.Text = null;
            }

            // 불량취소금액
            else if (tabDefectCancel.SelectedTabPage.Name == "tpgDefectAMT")
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetDefectAMTGroupBy", "10001", values);

                if (dt.Rows.Count == 0)
                {
                    ShowMessage("NoSelectData");
                    grdDefectAMT.DataSource = null;
                    return;
                }

                grdDefectAMT.DataSource = dt;
            }
            // 내역조회탭일때
            else
            {
                //if (string.IsNullOrWhiteSpace(product) && string.IsNullOrWhiteSpace(lot))
                //{
                //    grdCancelHistory.View.ClearDatas();
                //    throw MessageException.Create("RequiredInputLotOrProduct"); // Lot No와 품목중 한가지는 필수로 입력해야합니다.
                //}

                DataTable dt = await SqlExecuter.QueryAsync("GetDefectCancelHistory", "10001", values);

                if (dt.Rows.Count == 0)
                {
                    ShowMessage("NoSelectData");
                    grdCancelHistory.DataSource = null;
                    return;
                }

                grdCancelHistory.DataSource = dt;
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //Conditions.AddComboBox("p_plantId", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PLANTNAME", "PLANTID")
            //    .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
            //    .SetDefault(UserInfo.Current.Plant)
            //    .SetLabel("PLANT")
            //    .SetPosition(1.1);

            InitializeConditionPopup_Area();
            InitializeConditionPopup_Product();

            Conditions.AddComboBox("p_storage", new SqlQuery("GetDefectArea", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
                .SetLabel("STORAGE")
                .SetEmptyItem()
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetPosition(3.1);
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        /// <summary>
        /// Site 조회조건
        /// </summary>
        private void InitializeConditionPopup_Site()
        {
            // 팝업 컬럼설정
            var sitePopup = Conditions.AddSelectPopup("p_plantId", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
               .SetPopupLayout("PLANT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("PLANT")
               .SetPopupResultCount(1)
               .SetPosition(1.1);

            // 팝업 조회조건
            sitePopup.Conditions.AddTextBox("PLANTIDNAME")
                .SetLabel("PLANTIDNAME");

            // 팝업 그리드
            sitePopup.GridColumns.AddTextBoxColumn("PLANTID", 150)
                .SetValidationKeyColumn();
            sitePopup.GridColumns.AddTextBoxColumn("PLANTNAME", 200);
        }

        /// <summary>
        /// 작업장 조회조건
        /// </summary>
        private void InitializeConditionPopup_Area()
        {
            // 팝업 컬럼설정
            var areaPopup = Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAreaListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("AREA")
               .SetPopupResultCount(0)
               .SetPosition(1.2)
               .SetRelationIds("p_plantId");

            // 팝업 조회조건
            areaPopup.Conditions.AddTextBox("AREAIDNAME")
                .SetLabel("AREAIDNAME");

            // 팝업 그리드
            areaPopup.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetValidationKeyColumn();
            areaPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
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
               .SetPopupResultCount(0)
               .SetPosition(1.3);
            //.SetValidationIsRequired();
            //.SetDefault("1FM00246A7|A1", "1FM00246A7|A1");

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

        /// <summary>
        /// 선택된 데이터 복사 이동(DataDown)
        /// </summary>
        /// <param name="sourceGrid">원본 Grid</param>
        /// <param name="targetGrid">대상 Grid</param>
        /// <param name="subTargetGrid">Sub 대상 Grid</param>
        private void SetDataDown(SmartBandedGrid sourceGrid, SmartBandedGrid targetGrid, SmartBandedGrid subTargetGrid)
        {
            List<DataRow> listAddRows = new List<DataRow>();

            DataTable sourceData = sourceGrid.View.GetCheckedRows();
            DataTable targetData = (DataTable)targetGrid.DataSource;
            DataTable subTargetData = (DataTable)subTargetGrid.DataSource;

            if (sourceData.Rows.Count == 0) return;

            targetData = SetInitDataTable(sourceData);
            subTargetData = SetInitDataTable(sourceData);

            if (targetData == null || subTargetData == null) return;

            if (targetGrid.DataSource == null) targetGrid.DataSource = targetData;
            if (subTargetGrid.DataSource == null) subTargetGrid.DataSource = subTargetData;

            // Check Checked Data Row
            for (int i = sourceGrid.View.RowCount - 1; i >= 0; i--)
            {
                if (!sourceGrid.View.IsRowChecked(i))
                    continue;

                DataRow row = sourceGrid.View.GetDataRow(i) as DataRow;

                listAddRows.Add(row);
            }

            // 불량취소일땐 1종류의 Lot만 내려올 수 있다.
            if (tabDefectCancel.SelectedTabPage.Name == "tpgDefectCancel")
            {
                // Lot ID 체크
                if (subTargetGrid.View.RowCount != 0)
                {
                    string lotId = sourceData.AsEnumerable().Select(r => r.Field<string>("LOTID")).Distinct().FirstOrDefault().ToString();
                    string tgLotId = (targetGrid.DataSource as DataTable).AsEnumerable().Select(r => r.Field<string>("LOTID")).Distinct().FirstOrDefault().ToString();
                    string product = sourceData.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().FirstOrDefault().ToString() + sourceData.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().FirstOrDefault().ToString();
                    string tgProduct = (targetGrid.DataSource as DataTable).AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().FirstOrDefault().ToString() + (targetGrid.DataSource as DataTable).AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().FirstOrDefault().ToString();

                    if (!lotId.Equals(tgLotId))
                    {
                        grdDefectCode.View.CheckRow(grdDefectCode.View.GetFocusedDataSourceRowIndex(), false);

                        // 다른 Lot ID는 선택할 수 없습니다.
                        throw MessageException.Create("MixSelectLotID");
                    }
                    else if (!product.Equals(tgProduct))
                    {
                        grdDefectCode.View.CheckRow(grdDefectCode.View.GetFocusedDataSourceRowIndex(), false);

                        // 다른 품목은 선택할 수 없습니다.
                        throw MessageException.Create("MixSelectProduct");
                    }
                }
                txtDefectLotNo.EditValue = sourceData.AsEnumerable().Select(r => r.Field<string>("LOTID")).Distinct().FirstOrDefault().ToString();
                txtLotNo.EditValue = sourceData.AsEnumerable().Select(r => r.Field<string>("PARENTLOTID")).Distinct().FirstOrDefault().ToString();
            }
            // 불량병합일땐 공정과 작업장이 동일해야한다.
            else
            {
                // Area ID 체크
                if (subTargetGrid.View.RowCount != 0)
                {
                    string areaId = sourceData.AsEnumerable().Select(r => r.Field<string>("AREAID")).Distinct().FirstOrDefault().ToString();
                    string tgAreaId = (targetGrid.DataSource as DataTable).AsEnumerable().Select(r => r.Field<string>("AREAID")).Distinct().FirstOrDefault().ToString();

                    string segmentId = sourceData.AsEnumerable().Select(r => r.Field<string>("PROCESSSEGMENTID")).Distinct().FirstOrDefault().ToString();
                    string tgSegmentId = (targetGrid.DataSource as DataTable).AsEnumerable().Select(r => r.Field<string>("PROCESSSEGMENTID")).Distinct().FirstOrDefault().ToString();

                    string product = sourceData.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().FirstOrDefault().ToString() + sourceData.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().FirstOrDefault().ToString();
                    string tgProduct = (targetGrid.DataSource as DataTable).AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().FirstOrDefault().ToString() + (targetGrid.DataSource as DataTable).AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().FirstOrDefault().ToString();

                    if (!areaId.Equals(tgAreaId))
                    {
                        grdDefectCode2.View.CheckRow(grdDefectCode2.View.GetFocusedDataSourceRowIndex(), false);

                        // 다른 작업장은 선택할 수 없습니다.
                        throw MessageException.Create("MixSelectAreaId");
                    }
                    else if (!segmentId.Equals(tgSegmentId))
                    {
                        grdDefectCode2.View.CheckRow(grdDefectCode2.View.GetFocusedDataSourceRowIndex(), false);

                        // 다른 공정은 선택할 수 없습니다.
                        throw MessageException.Create("MixSelectSegmentId");
                    }
                    else if (!product.Equals(tgProduct))
                    {
                        grdDefectCode.View.CheckRow(grdDefectCode.View.GetFocusedDataSourceRowIndex(), false);

                        // 다른 품목은 선택할 수 없습니다.
                        throw MessageException.Create("MixSelectProduct");
                    }
                }
            }

            // Loop For data copy
            foreach (DataRow row in listAddRows)
            {
                DataRow newRow = (targetGrid.DataSource as DataTable).Rows.Add();
                newRow.BeginEdit();
                newRow.ItemArray = row.ItemArray.Clone() as object[];

                if ((subTargetGrid.DataSource as DataTable).Rows.Count == 0)
                {
                    DataRow newRow2 = (subTargetGrid.DataSource as DataTable).Rows.Add();
                    newRow2.BeginEdit();
                    newRow2.ItemArray = row.ItemArray.Clone() as object[];

                    if (row["ISORIGINALLOTMERGECODE"].Equals("Y"))
                    {
                        newRow2["ISORIGINALLOTMERGE"] = Language.Get("POSSIBLE"); // 가능
                    }
                    else
                    {
                        newRow2["ISORIGINALLOTMERGE"] = Language.Get("IMPOSSIBLE"); // 불가능
                    }

                    newRow2.EndEdit();
                }
                row.Delete();
                newRow.EndEdit();
            }

            (sourceGrid.DataSource as DataTable).AcceptChanges();
            (targetGrid.DataSource as DataTable).AcceptChanges();
            (subTargetGrid.DataSource as DataTable).AcceptChanges();
        }

        /// <summary>
        /// 선택된 데이터 복사 이동(DataUp)
        /// </summary>
        /// <param name="sourceGrid">원본 Grid</param>
        /// <param name="subSourceGrid">원본 Sub Grid</param>
        /// <param name="targetGrid">Sub 대상 Grid</param>
        private void SetDataUp(SmartBandedGrid sourceGrid, SmartBandedGrid subSourceGrid, SmartBandedGrid targetGrid)
        {
            List<DataRow> listAddRows = new List<DataRow>();
            List<DataRow> listAddRows2 = new List<DataRow>();

            DataTable sourceData = (DataTable)sourceGrid.DataSource;
            DataTable subSourceData = (DataTable)subSourceGrid.DataSource;
            DataTable targetData = (DataTable)targetGrid.DataSource;

            targetData = SetInitDataTable(sourceData);

            if (targetData == null) return;

            if (targetGrid.DataSource == null) targetGrid.DataSource = targetData;

            // Check Checked Data Row
            for (int i = sourceGrid.View.RowCount - 1; i >= 0; i--)
            {
                if (!sourceGrid.View.IsRowChecked(i))
                    continue;

                DataRow row = sourceGrid.View.GetDataRow(i) as DataRow;
                DataRow row2 = subSourceGrid.View.GetDataRow(i) as DataRow;

                listAddRows.Add(row);
                listAddRows2.Add(row2);
            }

            // Loop For data copy
            foreach (DataRow row in listAddRows)
            {
                if (!(targetGrid.DataSource as DataTable).Columns.Contains(("ISPOSSIBLEMERGE")))
                {
                    (targetGrid.DataSource as DataTable).Columns.Add("ISPOSSIBLEMERGE");
                }

                DataRow newRow = (targetGrid.DataSource as DataTable).Rows.Add();
                newRow.BeginEdit();
                newRow.ItemArray = row.ItemArray.Clone() as object[];

                row.Delete();
                newRow.EndEdit();
            }

            if (sourceGrid.View.RowCount == 0)
            {
                if (subSourceGrid.View.RowCount == 0) return;

                subSourceGrid.View.RemoveRow(0);
            }

            (sourceGrid.DataSource as DataTable).AcceptChanges();
            (subSourceGrid.DataSource as DataTable).AcceptChanges();
            (targetGrid.DataSource as DataTable).AcceptChanges();
        }

        /// <summary>
        /// Inittialize DataTable Column
        /// </summary>
        /// <param name="sourcedt">Source DataTable</param>
        /// <returns></returns>
        private DataTable SetInitDataTable(DataTable sourcedt)
        {
            if (sourcedt == null) return null;

            DataTable dt = new DataTable();

            foreach (DataColumn col in sourcedt.Columns)
            {
                if (dt.Columns.Contains(col.ColumnName))
                    continue;

                dt.Columns.Add(col.ColumnName.ToString(), col.DataType);
            }

            if (!dt.Columns.Contains("ISPOSSIBLEMERGE"))
                dt.Columns.Add("ISPOSSIBLEMERGE");

            return dt;
        }

        /// <summary>
        /// 체크된 불량코드를 가져와 LOT, PCS, PNL 수량을 계산한다.
        /// </summary>
        /// <param name="dt"></param>
        private void SetCalcCount()
        {
            DataTable dt = (grdDefectCodeCnt.DataSource as DataTable);
            DataTable dt2 = (grdDefectCodeCnt2.DataSource as DataTable);

            // 불량취소탭일때
            if (tabDefectCancel.SelectedTabPage.Name == "tpgDefectCancel")
            {
                if (dt == null) return;

                if (dt.Rows.Count == 0)
                {
                    txtProductId.EditValue = null;
                    txtProductName.EditValue = null;
                    txtDefectLotNo.EditValue = null;
                    txtLotNo.EditValue = null;
                    txtPcsCnt.EditValue = null;
                    txtPnlCnt.EditValue = null;
                }
                else
                {
                    double pcsCnt = dt.Rows.Cast<DataRow>().Select(r => r.Field<int>("CANCELPCSQTY")).Sum();
                    double pnlCnt = dt.Rows.Cast<DataRow>().Select(r => r.Field<int>("CANCELPNLQTY")).Sum();

                    txtProductId.EditValue = dt.AsEnumerable().First().Field<string>("PRODUCTDEFID");
                    txtProductName.EditValue = dt.AsEnumerable().First().Field<string>("PRODUCTDEFNAME");
                    txtPcsCnt.EditValue = pcsCnt;
                    txtPnlCnt.EditValue = pnlCnt;
                }
            }
            // 불량병합탭일때
            else if (tabDefectCancel.SelectedTabPage.Name == "tpgDefectMerge")
            {
                if (dt2 == null) return;

                if (dt2.Rows.Count == 0)
                {
                    txtProductId2.EditValue = null;
                    txtProductName2.EditValue = null;
                    txtLotCnt.EditValue = null;
                    txtPcsCnt2.EditValue = null;
                    txtPnlCnt2.EditValue = null;
                }
                else
                {
                    int lotCnt = dt2.Rows.Cast<DataRow>().Select(r => r.Field<string>("LOTID")).Distinct().Count();
                    int pcsCnt = dt2.Rows.Cast<DataRow>().Select(r => r.Field<int>("CANCELPCSQTY")).Sum();
                    int pnlCnt = dt2.Rows.Cast<DataRow>().Select(r => r.Field<int>("CANCELPNLQTY")).Sum();

                    txtProductId2.EditValue = dt2.AsEnumerable().First().Field<string>("PRODUCTDEFID");
                    txtProductName2.EditValue = dt2.AsEnumerable().First().Field<string>("PRODUCTDEFNAME");
                    txtLotCnt.EditValue = lotCnt;
                    txtPcsCnt2.EditValue = pcsCnt;
                    txtPnlCnt2.EditValue = pnlCnt;
                }
            }
        }

        /// <summary>
        /// 불량취소 또는 병합요청을 한 뒤 모든 탭의 그리드 재검색
        /// </summary>
        private void AllSearch()
        {
            var values = Conditions.GetValues();
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt1 = SqlExecuter.Query("GetLotDefectCode", "10001", values);

            if (dt1.Rows.Count == 0)
            {
                this.ShowMessage("NoSelectData");
                grdDefectCode.DataSource = null;
                grdDefectCodeCnt.DataSource = null;
                grdDefectCodeRouting.DataSource = null;

                grdDefectCode2.DataSource = null;
                grdDefectCodeCnt2.DataSource = null;
                grdDefectCodeRouting2.DataSource = null;
                memoComment.Text = null;
                return;
            }

            grdDefectCode.DataSource = dt1;
            grdDefectCodeCnt.DataSource = null;
            grdDefectCodeRouting.DataSource = null;
            txtProductId.EditValue = null;
            txtProductName.EditValue = null;
            txtDefectLotNo.EditValue = null;
            txtLotNo.EditValue = null;
            txtPcsCnt.EditValue = null;
            txtPnlCnt.EditValue = null;

            grdDefectCode2.DataSource = dt1;
            grdDefectCodeCnt2.DataSource = null;
            grdDefectCodeRouting2.DataSource = null;

            txtProductId2.EditValue = null;
            txtProductName2.EditValue = null;
            txtLotCnt.EditValue = null;
            txtPcsCnt2.EditValue = null;
            txtPnlCnt2.EditValue = null;
            memoComment.Text = null;

            DataTable dt2 = SqlExecuter.Query("GetDefectCancelHistory", "10001", values);

            if (dt2.Rows.Count == 0)
            {
                grdCancelHistory.DataSource = null;
                return;
            }

            grdCancelHistory.DataSource = dt2;
        }

        /// <summary>
        /// 해당 Lot의 취소수량을 받아와 완불처리 됬는지 판별(Y - 완불처리, N - 완불처리아님)
        /// </summary>
        /// <param name="lotNo">불량 Lot</param>
        /// <param name="cancelQty">사용자입력 취소수량</param>
        /// <returns></returns>
        private string IsAllQtyDefectLot(string lotNo, double cancelQty)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LOTID", lotNo);
            param.Add("CANCELQTY", cancelQty);

            DataTable dt = SqlExecuter.Query("GetIsAllQtyDefectLot", "10001", param);

            string flag = Format.GetInteger(dt.Rows[0]["COUNT"]) == 1 ? "Y" : "N";

            return flag;
        }

        #endregion

        private void DefectCancel_Load(object sender, EventArgs e)
        {

        }
    }
}
