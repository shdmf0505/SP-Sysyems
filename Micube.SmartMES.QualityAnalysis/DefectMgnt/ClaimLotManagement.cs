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
    /// 프 로 그 램 명  : 품질관리 > 불량관리 > ClaimLot 관리
    /// 업  무  설  명  : 인수처리후 원인판정 확정까지 된 불량Lot들에 대해 Claim처리를 한다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-12-03
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ClaimLotManagement : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public ClaimLotManagement()
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

            InitializeClaimLotHistoryGrid();
            InitializationSummaryRow();
        }

        #region Claim처리탭 Initialize

        /// <summary>        
        /// Lot별 불량코드를 모두 조회한다.
        /// </summary>
        private void InitializeDefectCodeGrid()
        {
            grdDefectCode.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdDefectCode.GridButtonItem = GridButtonItem.Export;

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
            defectInfo.AddTextBoxColumn("DEFECTCODE", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 불량코드
            defectInfo.AddTextBoxColumn("DEFECTNAME", 130)
                .SetIsReadOnly(); // 불량명
            defectInfo.AddTextBoxColumn("QCSEGMENTNAME", 130)
                .SetIsReadOnly(); // 품질공정명
            defectInfo.AddSpinEditColumn("DEFECTPCSQTY", 80)
                .SetLabel("PCS")
                .SetIsReadOnly(); // 불량 PCS 수량 
            defectInfo.AddSpinEditColumn("DEFECTPNLQTY", 80)
                .SetLabel("PNL")
                .SetIsReadOnly(); // 불량 PNL 수량 
            defectInfo.AddTextBoxColumn("USERSEQUENCE", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetIsReadOnly(); // 공정순서
            defectInfo.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
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

            reasonSegment.AddTextBoxColumn("REASONCONSUMABLEDEFNAME", 200)
                .SetLabel("REASONPRODUCT")
                .SetIsReadOnly(); // 원인품목명
            reasonSegment.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("Rev")
                .SetIsReadOnly(); // 원인품목 Version
            reasonSegment.AddTextBoxColumn("REASONCONSUMABLELOTID", 220)
                .SetIsReadOnly(); // 원인자재 ID
            reasonSegment.AddTextBoxColumn("REASONSEGMENTNAME", 180)
                .SetLabel("REASONSEGMENT")
                .SetIsReadOnly(); // 원인공정명
            reasonSegment.AddTextBoxColumn("REASONAREANAME", 180)
                .SetLabel("REASONAREA")
                .SetIsReadOnly(); // 원인작업장명
            reasonSegment.AddTextBoxColumn("REASONPLANTID", 100)
                .SetLabel("REASONPLANT")
                .SetIsReadOnly(); // 원인 Site
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
            hidden.AddTextBoxColumn("REASONSEGMENTID", 100)
                .SetIsHidden(); // 원인공정 ID
            hidden.AddTextBoxColumn("REASONAREAID", 100)
                .SetIsHidden(); // 원인작업장 ID
            hidden.AddTextBoxColumn("REASONCONSUMABLEDEFID", 100)
                .SetIsHidden(); // 원인품목 ID
            hidden.AddTextBoxColumn("LOTTYPE", 100)
                .SetIsHidden(); // 양산구분
            hidden.AddTextBoxColumn("TXNHISTKEY", 100)
                .SetIsHidden(); // TXNHISTKEY
            hidden.AddTextBoxColumn("INPUTPROCESSDEFID", 100)
                .SetIsHidden(); // 투입라우팅 ID
            hidden.AddTextBoxColumn("INPUTPROCESSDEFVERSION", 100)
                .SetIsHidden(); // 투입라우팅 Version
            hidden.AddTextBoxColumn("INPUTPROCESSDEFNAME", 100)
                .SetIsHidden(); // 투입라우팅명
            hidden.AddTextBoxColumn("INPUTPROCESSPATHID", 100)
                .SetIsHidden(); // 투입라우팅 상세정의 ID
            hidden.AddTextBoxColumn("INPUTUSERSEQUENCE", 100)
                .SetIsHidden(); // 투입공정수순
            hidden.AddTextBoxColumn("INPUTPROCESSSEGMENTID", 100)
                .SetIsHidden(); // 투입공정 ID
            hidden.AddTextBoxColumn("INPUTPROCESSSEGMENTVERSION", 100)
                .SetIsHidden(); // 투입공정 Version
            hidden.AddTextBoxColumn("INPUTPROCESSSEGMENTNAME", 100)
                .SetIsHidden(); // 투입공정명
            hidden.AddTextBoxColumn("INPUTAREAID", 100)
                .SetIsHidden(); // 투입작업장 ID
            hidden.AddTextBoxColumn("INPUTAREANAME", 100)
                .SetIsHidden(); // 투입작업장명
            hidden.AddTextBoxColumn("INPUTRESOURCEID", 100)
                .SetIsHidden(); // 투입자원 ID
            hidden.AddTextBoxColumn("INPUTRESOURCENAME", 100)
                .SetIsHidden(); // 투입자원명
            hidden.AddTextBoxColumn("QCSEGMENTID", 100)
                .SetIsHidden(); // 품질공정 ID
            hidden.AddTextBoxColumn("CLAIMLOTNO", 100)
                .SetIsHidden(); // Claim Lot No
            hidden.AddTextBoxColumn("PROCESSSTATE", 100)
                .SetIsHidden(); // Lot의 공정진행상태
            hidden.AddTextBoxColumn("DEFECTCODECOUNT", 100)
                .SetIsHidden(); // Lot별 불량코드 갯수
            hidden.AddTextBoxColumn("SUMLOTDEFECTQTY", 100)
                .SetIsHidden(); // Lot별 불량수량
            hidden.AddTextBoxColumn("CLAIMLOTID", 100)
                .SetIsHidden(); // Claim Lot No
            hidden.AddTextBoxColumn("PRODUCTIONORDERID", 100)
                .SetIsHidden(); // 수주번호
            hidden.AddTextBoxColumn("LINENO", 100)
                .SetIsHidden(); // Line No
            hidden.AddTextBoxColumn("PANELPERQTY", 100)
                .SetIsHidden(); // 1 Pnl당 Pcs수량
            hidden.AddTextBoxColumn("MATERIALCLASS", 100)
                .SetIsHidden(); // 자재 구분 채번용
            hidden.AddTextBoxColumn("MATERIALSEQUENCE", 100)
                .SetIsHidden(); // 자재 Seq 채번용
            hidden.AddTextBoxColumn("RTRSHT", 100)
                .SetIsHidden(); // Lot 차수 및 Split 차수 채번용
            hidden.AddTextBoxColumn("DESCRIPTION", 100)
                .SetIsHidden(); // 비고

            grdDefectCode.View.PopulateColumns();

            grdDefectCode.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        /// <summary>        
        /// 조회한 Lot별 불량코드들의 수량을 나타내는 그리드이다.
        /// </summary>
        private void InitializeDefectCodeCountGrid()
        {
            grdDefectCodeCnt.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            var defectCount = grdDefectCodeCnt.View.AddGroupColumn("DEFECTCOUNT");

            defectCount.AddSpinEditColumn("DEFECTPCSQTY", 80)
                .SetLabel("PCS")
                .SetIsReadOnly(); // 불량 PCS 수량 
            defectCount.AddSpinEditColumn("DEFECTPNLQTY", 80)
                .SetLabel("PNL")
                .SetIsReadOnly(); // 불량 PNL 수량 
            defectCount.AddTextBoxColumn("UOM", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("UOM")
                .SetIsReadOnly(); // 불량 UOM

            var cancelCount = grdDefectCodeCnt.View.AddGroupColumn("CLAIMCOUNT");

            cancelCount.AddSpinEditColumn("DEFECTPCSQTY", 80)
                 .SetLabel("PCS")
                 .SetIsReadOnly(); // Claim PCS 수량
            cancelCount.AddSpinEditColumn("DEFECTPNLQTY", 80)
                .SetLabel("PNL")
                .SetIsReadOnly(); // Claim PNL 수량

            var defectInfo = grdDefectCodeCnt.View.AddGroupColumn("DEFECTINFO");

            defectInfo.AddTextBoxColumn("PARENTLOTID", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetLabel("Lot No"); // Parnet Lot No
            defectInfo.AddTextBoxColumn("LOTID", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetLabel("DEFECTLOTID"); // Lot No
            defectInfo.AddTextBoxColumn("DEFECTCODE", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 불량코드
            defectInfo.AddTextBoxColumn("DEFECTNAME", 130)
                .SetIsReadOnly(); // 불량명
            defectInfo.AddTextBoxColumn("QCSEGMENTNAME", 130)
                .SetIsReadOnly(); // 품질공정명

            var hidden = grdDefectCodeCnt.View.AddGroupColumn("HIDDEN");

            hidden.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden(); // 회사 ID
            hidden.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden(); // Site ID
            hidden.AddTextBoxColumn("AREAID", 100)
                .SetIsHidden(); // 작업장 ID
            hidden.AddTextBoxColumn("PRODUCTDEFID", 100)
                .SetIsHidden(); // 품목코드
            hidden.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                .SetIsHidden(); // 품목 Version
            hidden.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetIsHidden(); // 공정 ID
            hidden.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100)
                .SetIsHidden(); // 공정 Version
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
            hidden.AddTextBoxColumn("REASONPLANTID", 100)
                .SetIsHidden(); // 원인 Site
            hidden.AddTextBoxColumn("LOTTYPE", 100)
                .SetIsHidden(); // 양산구분
            hidden.AddTextBoxColumn("TXNHISTKEY", 100)
                .SetIsHidden(); // TXNHISTKEY
            hidden.AddTextBoxColumn("INPUTPROCESSDEFID", 100)
                .SetIsHidden(); // 투입라우팅 ID
            hidden.AddTextBoxColumn("INPUTPROCESSDEFVERSION", 100)
                .SetIsHidden(); // 투입라우팅 Version
            hidden.AddTextBoxColumn("INPUTPROCESSDEFNAME", 100)
                .SetIsHidden(); // 투입라우팅명
            hidden.AddTextBoxColumn("INPUTPROCESSPATHID", 100)
                .SetIsHidden(); // 투입라우팅 상세정의 ID
            hidden.AddTextBoxColumn("INPUTUSERSEQUENCE", 100)
                .SetIsHidden(); // 투입공정수순
            hidden.AddTextBoxColumn("INPUTPROCESSSEGMENTID", 100)
                .SetIsHidden(); // 투입공정 ID
            hidden.AddTextBoxColumn("INPUTPROCESSSEGMENTVERSION", 100)
                .SetIsHidden(); // 투입공정 Version
            hidden.AddTextBoxColumn("INPUTPROCESSSEGMENTNAME", 100)
                .SetIsHidden(); // 투입공정명
            hidden.AddTextBoxColumn("INPUTAREAID", 100)
                .SetIsHidden(); // 투입작업장 ID
            hidden.AddTextBoxColumn("INPUTAREANAME", 100)
                .SetIsHidden(); // 투입작업장명
            hidden.AddTextBoxColumn("INPUTRESOURCEID", 100)
                .SetIsHidden(); // 투입자원 ID
            hidden.AddTextBoxColumn("INPUTRESOURCENAME", 100)
                .SetIsHidden(); // 투입자원명
            hidden.AddTextBoxColumn("QCSEGMENTID", 100)
                .SetIsHidden(); // 품질공정 ID
            hidden.AddTextBoxColumn("CLAIMLOTNO", 100)
                .SetIsHidden(); // Claim Lot No
            hidden.AddTextBoxColumn("PROCESSSTATE", 100)
                .SetIsHidden(); // Lot의 공정진행상태
            hidden.AddTextBoxColumn("DEFECTCODECOUNT", 100)
                .SetIsHidden(); // Lot별 불량코드 갯수
            hidden.AddTextBoxColumn("SUMLOTDEFECTQTY", 100)
                .SetIsHidden(); // Lot별 불량수량
            hidden.AddTextBoxColumn("CLAIMLOTID", 100)
                .SetIsHidden(); // Claim Lot No
            hidden.AddTextBoxColumn("PRODUCTIONORDERID", 100)
                .SetIsHidden(); // 수주번호
            hidden.AddTextBoxColumn("LINENO", 100)
                .SetIsHidden(); // Line No
            hidden.AddTextBoxColumn("PANELPERQTY", 100)
                .SetIsHidden(); // 1 Pnl당 Pcs수량
            hidden.AddTextBoxColumn("MATERIALCLASS", 100)
                .SetIsHidden(); // 자재 구분 채번용
            hidden.AddTextBoxColumn("MATERIALSEQUENCE", 100)
                .SetIsHidden(); // 자재 Seq 채번용
            hidden.AddTextBoxColumn("RTRSHT", 100)
                .SetIsHidden(); // Lot 차수 및 Split 차수 채번용
            hidden.AddTextBoxColumn("DESCRIPTION", 100)
                .SetIsHidden(); // 비고

            grdDefectCodeCnt.View.PopulateColumns();

            grdDefectCodeCnt.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        /// <summary>
        /// Routing을 매핑하는 그리드이다.
        /// </summary>
        private void InitializeDefectCodeRoutionGrid()
        {
            grdClaimLotRouting.View.SetAutoFillColumn("INPUTRESOURCEID");

            var inputSegment = grdClaimLotRouting.View.AddGroupColumn("INPUTSEGMENT");

            inputSegment.AddTextBoxColumn("CLAIMLOTID", 250)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("Claim Lot No")
                .SetIsReadOnly(); // Claim Lot No
            inputSegment.AddTextBoxColumn("INPUTPROCESSDEFNAME", 180)
                .SetLabel("PROCESSDEFNAME")
                .SetIsReadOnly(); // 투입 라우팅명
            inputSegment.AddTextBoxColumn("INPUTUSERSEQUENCE", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("USERSEQUENCE")
                .SetIsReadOnly(); // 투입 공정수순
            inputSegment.AddTextBoxColumn("INPUTPROCESSSEGMENTNAME", 150)
                .SetLabel("PROCESSSEGMENTNAME")
                .SetIsReadOnly(); // 투입 공정명
            inputSegment.AddTextBoxColumn("INPUTRESOURCENAME", 250)
                .SetLabel("RESOURCENAME")
                .SetIsReadOnly(); // 투입 대상자원

            var etc = grdClaimLotRouting.View.AddGroupColumn("ETC");

            etc.AddTextBoxColumn("DESCRIPTION", 200); // 비고

            var hidden = grdClaimLotRouting.View.AddGroupColumn("HIDDEN");

            hidden.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden(); // 회사 ID
            hidden.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden(); // Site ID
            hidden.AddTextBoxColumn("AREAID", 100)
                .SetIsHidden(); // 작업장 ID
            hidden.AddTextBoxColumn("PRODUCTDEFID", 100)
                .SetIsHidden(); // 품목코드
            hidden.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                .SetIsHidden(); // 품목 Version
            hidden.AddTextBoxColumn("PARENTLOTID", 100)
                .SetIsHidden(); // Parnet Lot No
            hidden.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetIsHidden(); // 공정 ID
            hidden.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100)
                .SetIsHidden(); // 공정 Version
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
            hidden.AddTextBoxColumn("REASONPLANTID", 100)
                .SetIsHidden(); // 원인 Site
            hidden.AddTextBoxColumn("LOTTYPE", 100)
                .SetIsHidden(); // 양산구분
            hidden.AddTextBoxColumn("TXNHISTKEY", 100)
                .SetIsHidden(); // TXNHISTKEY
            hidden.AddTextBoxColumn("INPUTPROCESSDEFID", 100)
                .SetIsHidden(); // 투입라우팅 ID
            hidden.AddTextBoxColumn("INPUTPROCESSDEFVERSION", 100)
                .SetIsHidden(); // 투입라우팅 Version
            hidden.AddTextBoxColumn("INPUTPROCESSPATHID", 100)
                .SetIsHidden(); // 투입라우팅 상세정의 ID
            hidden.AddTextBoxColumn("INPUTPROCESSSEGMENTID", 100)
                .SetIsHidden(); // 투입공정 ID
            hidden.AddTextBoxColumn("INPUTPROCESSSEGMENTVERSION", 100)
                .SetIsHidden(); // 투입공정 Version
            hidden.AddTextBoxColumn("INPUTAREAID", 100)
                .SetIsHidden(); // 투입작업장 ID
            hidden.AddTextBoxColumn("INPUTAREANAME", 100)
                .SetIsHidden(); // 투입작업장명
            hidden.AddTextBoxColumn("INPUTRESOURCEID", 100)
                .SetIsHidden(); // 투입자원 ID
            hidden.AddTextBoxColumn("QCSEGMENTID", 100)
                .SetIsHidden(); // 품질공정 ID
            hidden.AddTextBoxColumn("CLAIMLOTNO", 100)
                .SetIsHidden(); // Claim Lot No
            hidden.AddTextBoxColumn("PROCESSSTATE", 100)
                .SetIsHidden(); // Lot의 공정진행상태
            hidden.AddTextBoxColumn("DEFECTCODECOUNT", 100)
                .SetIsHidden(); // Lot별 불량코드 갯수
            hidden.AddTextBoxColumn("SUMLOTDEFECTQTY", 100)
                .SetIsHidden(); // Lot별 불량수량
            hidden.AddTextBoxColumn("PRODUCTIONORDERID", 100)
                .SetIsHidden(); // 수주번호
            hidden.AddTextBoxColumn("LINENO", 100)
                .SetIsHidden(); // Line No
            hidden.AddTextBoxColumn("PANELPERQTY", 100)
                .SetIsHidden(); // 1 Pnl당 Pcs수량
            hidden.AddTextBoxColumn("UOM", 100)
                .SetIsHidden(); // Unit
            hidden.AddTextBoxColumn("MATERIALCLASS", 100)
                .SetIsHidden(); // 자재 구분 채번용
            hidden.AddTextBoxColumn("MATERIALSEQUENCE", 100)
                .SetIsHidden(); // 자재 Seq 채번용
            hidden.AddTextBoxColumn("RTRSHT", 100)
                .SetIsHidden(); // Lot 차수 및 Split 차수 채번용

            grdClaimLotRouting.View.PopulateColumns();

            grdClaimLotRouting.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        #endregion

        #region 내역조회탭 Initialize

        /// <summary>        
        /// Claim처리한 Lot 내역을 조회한다.
        /// </summary>
        private void InitializeClaimLotHistoryGrid()
        {
            grdClaimHistory.GridButtonItem = GridButtonItem.Export;

            var claimInfo = grdClaimHistory.View.AddGroupColumn("CLAIMINFO");

            claimInfo.AddTextBoxColumn("PROCESSDATE", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 처리일시
            claimInfo.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetIsReadOnly(); // 품목코드
            claimInfo.AddTextBoxColumn("PRODUCTDEFNAME", 260)
                .SetIsReadOnly(); // 품목명
            claimInfo.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 품목 Version
            claimInfo.AddTextBoxColumn("PARENTLOTID", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("Lot No")
                .SetIsReadOnly(); // 폐기 Parent Lot Id
            claimInfo.AddTextBoxColumn("DEFECTCODE", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 폐기 불량코드
            claimInfo.AddTextBoxColumn("DEFECTCODENAME", 200)
                .SetIsReadOnly(); // 폐기 불량코드명
            claimInfo.AddTextBoxColumn("QCSEGMENTNAME", 150)
                .SetIsReadOnly(); // 품질공정명
            claimInfo.AddSpinEditColumn("PCSQTY", 80)
                .SetLabel("PCS")
                .SetIsReadOnly(); // 폐기 PCS 수량 
            claimInfo.AddSpinEditColumn("PANELQTY", 80)
                .SetLabel("PNL")
                .SetIsReadOnly(); // 폐기 PNL 수량 
            claimInfo.AddSpinEditColumn("USERSEQUENCE", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetIsReadOnly(); // 처리 공정수순
            claimInfo.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetIsReadOnly(); // 처리 공정명
            claimInfo.AddTextBoxColumn("AREANAME", 150)
                .SetIsReadOnly(); // 처리 작업장명
            claimInfo.AddTextBoxColumn("PLANTID", 80)
                .SetIsReadOnly(); // 처리 Site
            claimInfo.AddTextBoxColumn("LOTID", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetLabel("DEFECTLOTID"); // 폐기 Lot Id
            claimInfo.AddTextBoxColumn("CLAIMLOTID", 220)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("Claim Lot No")
                .SetIsReadOnly(); // Claim Lot Id

            var etc = grdClaimHistory.View.AddGroupColumn("ETC");

            etc.AddTextBoxColumn("PROCESSUSERNAME", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("PROCESSUSER")
                .SetIsReadOnly(); // 처리자
            etc.AddSpinEditColumn("UNIT", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("UOM")
                .SetIsReadOnly(); // UOM
            etc.AddTextBoxColumn("DESCRIPTION", 200)
                .SetIsReadOnly(); // 비고

            var hidden = grdClaimHistory.View.AddGroupColumn("HIDDEN");

            hidden.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetIsHidden(); // 폐기 공정 ID
            hidden.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100)
                .SetIsHidden(); // 폐기 공정 Version
            hidden.AddTextBoxColumn("AREAID", 100)
                .SetIsHidden(); // 폐기 작업장 ID
            hidden.AddTextBoxColumn("PROCESSDEFID", 100)
                .SetIsHidden(); // 폐기후 라우팅 ID
            hidden.AddTextBoxColumn("PROCESSDEFVERSION", 100)
                .SetIsHidden(); // 폐기후 라우팅 Version
            hidden.AddTextBoxColumn("PROCESSDEFNAME", 100)
                .SetIsHidden(); // 폐기후 라우팅명
            hidden.AddTextBoxColumn("PROCESSPATHID", 100)
                .SetIsHidden(); // 폐기후 라우팅 상세 ID
            hidden.AddTextBoxColumn("TXNHISTKEY", 100)
                .SetIsHidden(); // 폐기 Txn Hist Key
            hidden.AddTextBoxColumn("QCSEGMENTID", 100)
                .SetIsHidden(); // 품질공정 ID
            hidden.AddTextBoxColumn("RESOURCEID", 100)
                .SetIsHidden(); // 투입 자원 ID

            grdClaimHistory.View.PopulateColumns();

            grdClaimHistory.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        // 그리드 합계 초기화
        private void InitializationSummaryRow()
        {
            grdClaimHistory.View.Columns["QCSEGMENTNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdClaimHistory.View.Columns["QCSEGMENTNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdClaimHistory.View.Columns["PCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdClaimHistory.View.Columns["PCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdClaimHistory.View.Columns["PANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdClaimHistory.View.Columns["PANELQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdClaimHistory.View.OptionsView.ShowFooter = true;
            grdClaimHistory.ShowStatusBar = false;
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

            btnClaimLot.Click += BtnClaimLot_Click;

            grdDefectCode.View.CheckStateChanged += View_CheckStateChanged;
            grdDefectCode.View.RowStyle += View_RowStyle;

        }  

        /// <summary>
        /// Check한 Row색깔변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            bool isChecked = grdDefectCode.View.IsRowChecked(e.RowHandle);

            if (isChecked)
            {
                e.Appearance.BackColor = Color.FromArgb(254, 254, 16);
                e.HighPriority = true;
            }         
        }

        /// <summary>
        /// 같은 공정이 아니면 Grid Row Down을 막는다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            DataTable dt = grdDefectCode.View.GetCheckedRows();

            // 공정 체크
            int segmentCount = dt.AsEnumerable().Select(r => new { segmentId = r["PROCESSSEGMENTID"], segmentVersion = r["PROCESSSEGMENTVERSION"] }).Distinct().Count();
            // 품목 체크
            int productCount = dt.AsEnumerable().Select(r => new { productId = r.Field<string>("PRODUCTDEFID"), productVersion = r.Field<string>("PRODUCTDEFVERSION") }).Distinct().Count();

            if (segmentCount > 1)
            {
                grdDefectCode.View.CheckRow(grdDefectCode.View.GetFocusedDataSourceRowIndex(), false);

                // 다른 공정은 선택할 수 없습니다.
                throw MessageException.Create("MixSelectSegmentId");
            }
            else if (productCount > 1)
            {
                grdDefectCode.View.CheckRow(grdDefectCode.View.GetFocusedDataSourceRowIndex(), false);

                // 다른 품목은 선택할 수 없습니다.
                throw MessageException.Create("MixSelectProudct");
            }
        }

        /// <summary>
        /// Claim처리버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClaimLot_Click(object sender, EventArgs e)
        {
            DataTable dt1 = (grdDefectCodeCnt.DataSource as DataTable);
            dt1.TableName = "list1";
            DataTable dt2 = (grdClaimLotRouting.DataSource as DataTable);
            dt1.TableName = "list2";

            // 저장할 데이터가 없다면 Exception
            if (dt1.Rows.Count == 0)
            {
                throw MessageException.Create("GridNoData");
            }

            if (this.ShowMessage(MessageBoxButtons.YesNo, "DoDefectClaim") == DialogResult.Yes)
            {
                MessageWorker worker = new MessageWorker("SaveClaimLot");
                worker.SetBody(new MessageBody()
                {
                    { "processlUser", UserInfo.Current.Id }, // 처리자
                    { "processDate", DateTime.Now }, // 처리일시
                    { "productDefId", dt1.Rows[0]["PRODUCTDEFID"] }, // 품목 ID
                    { "productDefVersion", dt1.Rows[0]["PRODUCTDEFVERSION"] }, // 품목 Version
                    { "totalPcsQty", txtPcsCnt.EditValue }, // ClaimTotal Pcs수량
                    { "totalPnlQty", txtPnlCnt.EditValue }, // ClaimTotal Pnl수량
                    { "panelPerQty", dt1.Rows[0]["PANELPERQTY"] }, // 1 Pnl당 Pcs수량
                    { "productionOrderId", dt1.Rows[0]["PRODUCTIONORDERID"]}, // 수주번호
                    { "lineNo", dt1.Rows[0]["LINENO"]}, // Line No
                    { "list1", dt1 }, // Claim할 불량코드 데이터테이블
                    { "list2", dt2}, // Claim할 Lot 데이터테이블
                });

                var result = worker.Execute<DataTable>();
                var resultData = result.GetResultSet();

                this.ShowMessage("CompleteDefectClaimParam"
                                , resultData.Rows[0]["LOTID"].ToString()
                                , resultData.Rows[0]["QTY"].ToString());
                this.AllSearch();

                CommonFunction.PrintLotCard(resultData.Rows[0]["LOTID"].ToString(), LotCardType.Normal);
            }
        }

        /// <summary>
        /// Data Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDataDown_Click(object sender, EventArgs e)
        {
            if (tabClaimLot.SelectedTabPage.Name == "tpgClaimLot")
            {
                SetDataDown(grdDefectCode, grdDefectCodeCnt, grdClaimLotRouting);  
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
            if (tabClaimLot.SelectedTabPage.Name == "tpgClaimLot")
            {
                SetDataUp(grdDefectCodeCnt, grdClaimLotRouting ,grdDefectCode);
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

            // Claim처리탭일때
            if (tabClaimLot.SelectedTabPage.Name == "tpgClaimLot")
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetTargetClaimLot", "10001", values);

                if (dt.Rows.Count == 0)
                {
                    this.ShowMessage("NoSelectData");
                    grdDefectCode.DataSource = null;
                    grdDefectCodeCnt.DataSource = null;
                    grdClaimLotRouting.DataSource = null;
                    return;
                }

                grdDefectCode.DataSource = dt;
                grdDefectCodeCnt.DataSource = null;
                grdClaimLotRouting.DataSource = null;

                txtProductId.EditValue = null;
                txtProductName.EditValue = null;
                txtProcessSegmentName.EditValue = null;
                txtPcsCnt.EditValue = null;
                txtPnlCnt.EditValue = null;
            }
            // 내역조회탭일때
            else
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetClaimLotHistory", "10001", values);

                if (dt.Rows.Count == 0)
                {
                    ShowMessage("NoSelectData");
                    grdClaimHistory.DataSource = null;
                    return;
                }

                grdClaimHistory.DataSource = dt;
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
            //    .SetPosition(1.1)
            //    .SetValidationIsRequired();

            Conditions.AddComboBox("p_reasonPlantId", new SqlQuery("GetAnotherPlant", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PLANTNAME", "PLANTID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetLabel("REASONPLANT")
                .SetPosition(0.9)
                .SetRelationIds("P_PLANTID")
                .SetValidationIsRequired();

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
            SmartComboBox plantCombo = Conditions.GetControl<SmartComboBox>("p_plantId");
            SmartComboBox reasonPlantCombo = Conditions.GetControl<SmartComboBox>("p_reasonPlantId");
            reasonPlantCombo.UseEmptyItem = false;
            reasonPlantCombo.ItemIndex = 0;

            plantCombo.EditValueChanged += (s, e) =>
            {
                reasonPlantCombo.EditValue = null;
                reasonPlantCombo.Text = null;
            };
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
               .SetPopupResultCount(1)
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
               .SetPopupResultCount(1)
               .SetPosition(1.3);
               //.SetValidationIsRequired();

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

            // 공정이 같아야함
            if (subTargetGrid.View.RowCount != 0)
            {
                string processsegmentId = sourceData.AsEnumerable().Select(r => r.Field<string>("PROCESSSEGMENTID")).Distinct().FirstOrDefault().ToString();
                string processsegmentVersion = sourceData.AsEnumerable().Select(r => r.Field<string>("PROCESSSEGMENTVERSION")).Distinct().FirstOrDefault().ToString();
                string tgProcesssegmentId = (targetGrid.DataSource as DataTable).AsEnumerable().Select(r => r.Field<string>("PROCESSSEGMENTID")).Distinct().FirstOrDefault().ToString();
                string tgProcesssegmentVersion = (targetGrid.DataSource as DataTable).AsEnumerable().Select(r => r.Field<string>("PROCESSSEGMENTVERSION")).Distinct().FirstOrDefault().ToString();

                string product = sourceData.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().FirstOrDefault().ToString() + sourceData.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().FirstOrDefault().ToString();
                string tgProduct = (targetGrid.DataSource as DataTable).AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().FirstOrDefault().ToString() + (targetGrid.DataSource as DataTable).AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().FirstOrDefault().ToString();

                if (!processsegmentId.Equals(tgProcesssegmentId))
                {
                    grdDefectCode.View.CheckRow(grdDefectCode.View.GetFocusedDataSourceRowIndex(), false);

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
                    newRow2.EndEdit();
                    GetReasonPlantProcessDefinition();
                    CreateClaimLotId(newRow2);
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
 
            return dt;
        }

        /// <summary>
        /// 체크된 불량코드를 가져와 PCS, PNL 수량을 계산한다.
        /// </summary>
        /// <param name="dt"></param>
        private void SetCalcCount()
        {
            DataTable dt = (grdDefectCodeCnt.DataSource as DataTable);

            if (dt == null) return;

            if (dt.Rows.Count == 0)
            {
                txtProductId.EditValue = null;
                txtProductName.EditValue = null;
                txtProcessSegmentName.EditValue = null;
                txtPcsCnt.EditValue = null;
                txtPnlCnt.EditValue = null;
            }
            else
            {
                double pcsCnt = dt.Rows.Cast<DataRow>().Select(r => r.Field<int>("DEFECTPCSQTY")).Sum();
                double panelPerQty = Format.GetInteger(dt.Rows[0]["PANELPERQTY"]);
                double pnlCnt = Math.Ceiling(pcsCnt / panelPerQty);

                txtProductId.EditValue = dt.AsEnumerable().First().Field<string>("PRODUCTDEFID");
                txtProductName.EditValue = dt.AsEnumerable().First().Field<string>("PRODUCTDEFNAME");
                txtProcessSegmentName.EditValue = dt.AsEnumerable().First().Field<string>("PROCESSSEGMENTNAME");
                txtPcsCnt.EditValue = pcsCnt;
                txtPnlCnt.EditValue = pnlCnt;
            }         
        }

        /// <summary>
        /// ClaimLot 처리한 뒤 재검색
        /// </summary>
        private void AllSearch()
        {
            var values = Conditions.GetValues();
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt1 = SqlExecuter.Query("GetTargetClaimLot", "10001", values);

            if (dt1.Rows.Count == 0)
            {
                this.ShowMessage("NoSelectData");
                grdDefectCode.DataSource = null;
                grdDefectCodeCnt.DataSource = null;
                grdClaimLotRouting.DataSource = null;
                return;
            }

            grdDefectCode.DataSource = dt1;
            grdDefectCodeCnt.DataSource = null;
            grdClaimLotRouting.DataSource = null;
            txtProductId.EditValue = null;
            txtProductName.EditValue = null;
            txtProcessSegmentName.EditValue = null;
            txtPcsCnt.EditValue = null;
            txtPnlCnt.EditValue = null;        
  
            DataTable dt2 = SqlExecuter.Query("GetClaimLotHistory", "10001", values);

            if (dt2.Rows.Count == 0)
            {
                grdClaimHistory.DataSource = null;
                return;
            }

            grdClaimHistory.DataSource = dt2;           
        }

        // 회사의 Site 기준으로 Claim 라우팅을 조회후 바인딩
        private void GetReasonPlantProcessDefinition()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            
            // 인터플렉스 기준(영풍은 사용안함)
            if (grdDefectCode.View.GetRowCellValue(0, "ENTERPRISEID").Equals("INTERFLEX"))
            {
                switch (grdDefectCode.View.GetRowCellValue(0, "PLANTID"))
                {
                    case "IFC":
                        param.Add("PROCESSDEFID", "80191202001");
                        param.Add("PROCESSDEFVERSION", "A1");
                        break;
                    case "IFV":
                        param.Add("PROCESSDEFID", "80191202002");
                        param.Add("PROCESSDEFVERSION", "A1");
                        break;
                    case "CCT":
                        param.Add("PROCESSDEFID", "80191202003");
                        param.Add("PROCESSDEFVERSION", "A1");
                        break;
                }
            }

            DataTable resultDt = SqlExecuter.Query("GetClaimLotRouting", "10001", param);

            if (resultDt.Rows.Count != 0)
            {
                grdClaimLotRouting.View.SetRowCellValue(0, "INPUTPROCESSDEFID", resultDt.Rows[0]["PROCESSDEFID"]);
                grdClaimLotRouting.View.SetRowCellValue(0, "INPUTPROCESSDEFVERSION", resultDt.Rows[0]["PROCESSDEFVERSION"]);
                grdClaimLotRouting.View.SetRowCellValue(0, "INPUTPROCESSDEFNAME", resultDt.Rows[0]["PROCESSDEFNAME"]);
                grdClaimLotRouting.View.SetRowCellValue(0, "INPUTPROCESSPATHID", resultDt.Rows[0]["PROCESSPATHID"]);
                grdClaimLotRouting.View.SetRowCellValue(0, "INPUTUSERSEQUENCE", resultDt.Rows[0]["USERSEQUENCE"]);
                grdClaimLotRouting.View.SetRowCellValue(0, "INPUTPROCESSSEGMENTID", resultDt.Rows[0]["PROCESSSEGMENTID"]);     
                grdClaimLotRouting.View.SetRowCellValue(0, "INPUTPROCESSSEGMENTVERSION", resultDt.Rows[0]["PROCESSSEGMENTVERSION"]);
                grdClaimLotRouting.View.SetRowCellValue(0, "INPUTPROCESSSEGMENTNAME", resultDt.Rows[0]["PROCESSSEGMENTNAME"]);
                grdClaimLotRouting.View.SetRowCellValue(0, "INPUTAREAID", resultDt.Rows[0]["AREAID"]);
                grdClaimLotRouting.View.SetRowCellValue(0, "INPUTAREANAME", resultDt.Rows[0]["AREANAME"]);
                grdClaimLotRouting.View.SetRowCellValue(0, "INPUTRESOURCEID", resultDt.Rows[0]["RESOURCEID"]);
                grdClaimLotRouting.View.SetRowCellValue(0, "INPUTRESOURCENAME", resultDt.Rows[0]["RESOURCENAME"]);
            }
        }

        /// <summary>
        /// 품목을 기준으로 ClaimLotId를 채번후 그리드에 바인딩한다.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private void CreateClaimLotId(DataRow row)
        {
            string claimLotId = ""; // 채번 ClaimLotId

            // 투입년월일 구하기
            string year = Format.GetString(DateTime.Now.Year).Substring(2, 2); // 투입년
            string month = Format.GetString(DateTime.Now.Month).Length == 1 ? "0" + Format.GetString(DateTime.Now.Month) : Format.GetString(DateTime.Now.Month); // 투입월
            string day = Format.GetString(DateTime.Now.Day); // 투입일
            string inputDate = year + month + day; // 투입년월일

            // 발행 Site 구하기
            string site = GetSiteCode(Format.GetString(row["PLANTID"]));

            // 원인 Site 구하기
            string reasonSite = GetSiteCode(Format.GetString(row["REASONPLANTID"]));

            // 자재구분 구하기
            string materialClass = Format.GetString(row["MATERIALCLASS"]);
            string materialSequence = Format.GetString(row["MATERIALSEQUENCE"]);

            if (string.IsNullOrWhiteSpace(materialClass) || string.IsNullOrWhiteSpace(materialSequence))
            {
                // 자재품목구분 또는 자재순번이 등록되지 않았습니다. 자재품목구분, 자재순번을 확인하시기 바랍니다.
                throw MessageException.Create("NotExistsMaterialInfo", string.Format("Product Id : {0}, Product Version : {1}, Material Class : {2}, Material Sequence : {3}", Format.GetString(row["PRODUCTDEFID"]), Format.GetString(row["PRODUCTDEFVERSION"]), materialClass, materialSequence));
            }

            string material = materialClass + Format.GetInteger(materialSequence).ToString("00");

            // Lot 차수 및 분할차수 구하기
            string lotDegree = GetClaimLotMaxDegree(string.Join("-", "CLAIM" , inputDate , site , reasonSite , material));
            string lotSplitDegree = row["RTRSHT"].Equals("RTR") ? "000" : "001";

            // ClaimLotId 채번
            claimLotId = string.Join("-", "CLAIM", inputDate, site, reasonSite, material, lotDegree, lotSplitDegree);

            grdClaimLotRouting.View.SetRowCellValue(0, "CLAIMLOTID", claimLotId);
        }

        /// <summary>
        /// 발행사이트 및 원인사이트 코드 가져오기
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        private string GetSiteCode(string site)
        {
            string code;

            switch (site)
            {
                case "IFC":
                    code = "F";
                    break;

                case "CCT":
                    code = "C";
                    break;

                case "IFV":
                    code = "V";
                    break;

                default:
                    code = "N";
                    break;
            }

            return code;
        }

        /// <summary>
        /// ILIKE 검색조건으로 해당 ClaimLot의 최대차수 조회 
        /// </summary>
        /// <param name="lotCode"></param>
        /// <returns></returns>
        private string GetClaimLotMaxDegree(string lotCode)
        {
            string maxDegree;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LOTID", lotCode);

            DataTable dt = SqlExecuter.Query("GetClaimLotMaxDegree", "10001", param);

            maxDegree = dt.Rows.Count == 0 ? "000" : (Format.GetInteger(dt.Rows[0]["MAXDEGREE"]) + 1).ToString("000");

            return maxDegree;
        }

        #endregion
    }
}
