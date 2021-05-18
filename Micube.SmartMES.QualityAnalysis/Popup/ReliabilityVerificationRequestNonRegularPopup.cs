#region using

using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections;
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
    /// 프 로 그 램 명  : 품질관리 > 신뢰성 검증 > 정기외 검증 요청
    /// 업  무  설  명  : 정기외 검증 요청.
    /// 생    성    자  : 유호석
    /// 생    성    일  : 2019-08-13
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReliabilityVerificationRequestNonRegularPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }
        /// <summary>
        /// 그리드에 보여줄 데이터테이블
        /// </summary>
        string _sMode = string.Empty; // New/Modify
        string _sENTERPRISEID = string.Empty;
        string _sPLANTID = string.Empty;
        string _sREQUESTNO = string.Empty;
        string _sISSAMPLERECEIVE = string.Empty;
        StringBuilder _sbMailContents = new StringBuilder();
        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ReliabilityVerificationRequestNonRegularPopup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        public ReliabilityVerificationRequestNonRegularPopup(string sMode, string sEnterprise, string sPLANTID, string sREQUESTNO, string sISSAMPLERECEIVE)
        {
            InitializeComponent();
            InitializeEvent();

            _sMode = sMode;
            _sENTERPRISEID = sEnterprise;
            _sPLANTID = sPLANTID;
            _sREQUESTNO = sREQUESTNO;
            _sISSAMPLERECEIVE = sISSAMPLERECEIVE;
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            //grdQCReliabilityLot
            grdQCReliabilityLot.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;
            grdQCReliabilityLot.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            //grdQCReliabilityLot.View.SetIsReadOnly();
            grdQCReliabilityLot.View.AddTextBoxColumn("REQUESTNO", 100).SetIsReadOnly().SetIsHidden();                //의뢰번호
            grdQCReliabilityLot.View.AddTextBoxColumn("ENTERPRISEID", 100).SetIsReadOnly().SetIsHidden();             //회사 ID
            grdQCReliabilityLot.View.AddTextBoxColumn("PLANTID", 100).SetIsReadOnly().SetIsHidden();                  //Site ID
            grdQCReliabilityLot.View.AddTextBoxColumn("CUSTOMERID").SetTextAlignment(TextAlignment.Center).SetIsReadOnly();                    // 고객사 ID
            grdQCReliabilityLot.View.AddTextBoxColumn("CUSTOMERNAME").SetTextAlignment(TextAlignment.Left).SetIsReadOnly(); // 고객사 명                     // 고객사 명
            grdQCReliabilityLot.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();             //제품 정의 ID/품목
            InitializeOSP_ProductDefIdPopup();                                               //제품 정의 ID/품목
            //grdQCReliabilityLot.View.AddTextBoxColumn("PRODUCTDEFNAME", 180).SetIsReadOnly();
            grdQCReliabilityLot.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();        //제품 정의 Version

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_ENTERPRISEID", _sENTERPRISEID);
            param.Add("P_PLANTID", _sPLANTID);
            param.Add("P_REQUESTNO", _sREQUESTNO);
            param.Add("P_APPROVALTYPE", "ReliabilityVerificationRequestNonRegular");
            DataTable dtApproval = SqlExecuter.Query("GetQCApproval", "10001", param);                     //결재 정보

            if (_sMode == "New")//
            {
                InitializeQCReliabilityLotPopup();
            }
            else
            {
                var user = dtApproval.AsEnumerable().Where(r => r["PROCESSTYPE"].ToString() == "Draft").FirstOrDefault();
                string sDraftUser = user == null ? string.Empty : user["CHARGERID"].ToString();
                if (sDraftUser != UserInfo.Current.Id   //기안자와 로그인 다를경우
                    || dtApproval.AsEnumerable().Where(r => r["PROCESSTYPE"].ToString() == "Draft" && r["APPROVALSTATE"].ToString() == "Approval").ToList().Count > 0)//기안자가 승인
                    grdQCReliabilityLot.View.AddTextBoxColumn("LOTID", 200).SetIsReadOnly();                    //LOT ID
                else
                    InitializeQCReliabilityLotPopup();
            }
            grdQCReliabilityLot.View.AddTextBoxColumn("WEEK", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdQCReliabilityLot.View.AddTextBoxColumn("PROCESSSEGMENTID", 100).SetIsReadOnly().SetIsHidden();         //공정 ID
            grdQCReliabilityLot.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100).SetIsReadOnly().SetIsHidden();    //공정 Version

            grdQCReliabilityLot.View.AddTextBoxColumn("PROCESSDEFID", 100).SetIsReadOnly().SetIsHidden();         //라우팅 ID
            grdQCReliabilityLot.View.AddTextBoxColumn("PROCESSDEFVERSION", 100).SetIsReadOnly().SetIsHidden();    //라우팅 Version
            grdQCReliabilityLot.View.AddTextBoxColumn("USERSEQUENCE", 100).SetIsReadOnly().SetIsHidden();    //공정수순

            grdQCReliabilityLot.View.AddTextBoxColumn("AREAID", 100).SetIsReadOnly().SetIsHidden();                   //작업장 ID
            grdQCReliabilityLot.View.AddTextBoxColumn("OUTPUTDATE", 100).SetIsReadOnly().SetIsHidden();               //출력일시
            grdQCReliabilityLot.View.AddSpinEditColumn("REQUESTQTY", 100);               //의뢰수량
            grdQCReliabilityLot.View.AddTextBoxColumn("INSPITEMID", 100).SetIsReadOnly().SetIsHidden();       //검사항목아이템
            Initialize_InspItemPopup();
            grdQCReliabilityLot.View.AddTextBoxColumn("INSPITEMVERSION", 100).SetIsReadOnly().SetIsHidden();       //검사항목아이템
            //grdQCReliabilityLot.View.AddSpinEditColumn("INSPITEMNAME", 100);       //검사항목아이템
            grdQCReliabilityLot.View.AddTextBoxColumn("PURPOSE", 100).SetIsReadOnly().SetIsHidden();                  //의뢰목적
            grdQCReliabilityLot.View.AddTextBoxColumn("DETAILS", 100).SetIsReadOnly().SetIsHidden();                  //의뢰상세내용
            grdQCReliabilityLot.View.AddTextBoxColumn("ISPOSTPROCESS", 100).SetIsReadOnly().SetIsHidden();            //검증후 처리여부
            grdQCReliabilityLot.View.AddTextBoxColumn("DESCRIPTION", 100).SetIsReadOnly().SetIsHidden();              //설명

            grdQCReliabilityLot.View.PopulateColumns();

            fpcReport.grdFileList.View.Columns["FILENAME"].Width = 200;
            fpcReport.grdFileList.View.Columns["FILEEXT"].Width = 100;
            fpcReport.grdFileList.View.Columns["FILESIZE"].Width = 100;
            fpcReport.grdFileList.View.Columns["COMMENTS"].Width = 400;

            ctrlApproval.grdApproval.View.Columns["PROCESSTYPE"].Width = 80;// 절차구분
            ctrlApproval.grdApproval.View.Columns["CHARGETYPE"].Width = 75;//역할구분
            ctrlApproval.grdApproval.View.Columns["USERNAME"].Width = 75;//담당자
            ctrlApproval.grdApproval.View.Columns["REJECTCOMMENTS"].Width = 400;//반려사유

            //grdSegmentRef
            //grdMeasureHistory.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdMeasureHistory.GridButtonItem = GridButtonItem.Export;

            grdMeasureHistory.View
                .AddTextBoxColumn("USERSEQUENCE", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 공정순서(번호)

            grdMeasureHistory.View
                .AddTextBoxColumn("PROCESSSEGMENTNAME", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 공정명

            grdMeasureHistory.View
                .AddTextBoxColumn("AREANAME", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 작업장

            grdMeasureHistory.View
                .AddTextBoxColumn("EQUIPMENTNAME", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 설비(호기)

            grdMeasureHistory.View
                .AddTextBoxColumn("TRACKOUTTIME", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 작업종료시간

            grdMeasureHistory.View.PopulateColumns();
        }

        /// <summary>
        /// 품목을 선택하는 팝업
        /// </summary>
        private void InitializeOSP_ProductDefIdPopup()
        {
            //팝업 컬럼 설정

            //var productDefId = grdQCReliabilityLot.View.AddSelectPopupColumn("PRODUCTDEFID", new SqlQuery("GetProductDefList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            var productDefId = grdQCReliabilityLot.View.AddSelectPopupColumn("PRODUCTDEFNAME", 220, new SqlQuery("GetQCProductDefinitionList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME")
                                              .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, false)
                                              .SetPopupResultCount(1)
                                              .SetPopupLayoutForm(1000, 800, FormBorderStyle.FixedToolWindow)
                                              .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                                              .SetRelationIds("PLANTID")
                                              .SetLabel("PRODUCTDEFNAME")
                                              .SetValidationKeyColumn()
                                              .SetDefault("", "")
                                              .SetPopupQueryPopup((DataRow currentrow) =>
                                              {
                                                  if (string.IsNullOrWhiteSpace(_sPLANTID))
                                                  {
                                                      this.ShowMessage("NoSelectSite");
                                                      return false;
                                                  }

                                                  return true;
                                              })
                                               .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                               {
                                                   // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                   // dataGridRow : 현재 Focus가 있는 그리드의 DataRow

                                                   foreach (DataRow row in selectedRows)
                                                   {
                                                       dataGridRow["CUSTOMERID"] = row["CUSTOMERID"].ToString();
                                                       dataGridRow["CUSTOMERNAME"] = row["CUSTOMERNAME"].ToString();
                                                       dataGridRow["PRODUCTDEFVERSION"] = row["PRODUCTDEFVERSION"].ToString();
                                                       dataGridRow["PRODUCTDEFID"] = row["PRODUCTDEFID"].ToString();
                                                   }

                                                   if (selectedRows.Count() == 0)
                                                   {
                                                       dataGridRow["CUSTOMERID"] = "";
                                                       dataGridRow["CUSTOMERNAME"] = "";
                                                       dataGridRow["PRODUCTDEFID"] = "";
                                                       dataGridRow["PRODUCTDEFVERSION"] = "";
                                                   }
                                               })
                                               ;

            productDefId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");

            productDefId.Conditions.AddTextBox("PLANTID")
                                   .SetPopupDefaultByGridColumnId("PLANTID")
                                   .SetIsHidden();

            // 팝업 그리드
            // 고객사 ID
            productDefId.GridColumns.AddTextBoxColumn("CUSTOMERID", 100);
            // 고객사 명
            productDefId.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 100);
            // 품목코드
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목버전
            productDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 70);
            // 품목유형구분
            productDefId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 생산구분
            productDefId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 50, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 단위
            productDefId.GridColumns.AddComboBoxColumn("UNIT", 50, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");

        }
        /// <summary>
        /// 검증항목을 선택하는 팝업
        /// </summary>
        private void Initialize_InspItemPopup()
        {
            //팝업 컬럼 설정
            //var inspItem = grdQCReliabilityLot.View.AddSelectPopupColumn("INSPITEMID", 200, new SqlQuery("GetReliabilityVerificationInspItemList", "10001", $"P_PLANTID={UserInfo.Current.Plant}", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}"), "INSPITEMVERSION")
            var inspItem = grdQCReliabilityLot.View.AddSelectPopupColumn("INSPITEMNAME", 200, new SqlQuery("GetReliabilityVerificationInspItemList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMNAME")
                                              .SetPopupLayout("INSPITEMID", PopupButtonStyles.Ok_Cancel, true, false)
                                              .SetPopupResultCount(0)
                                              .SetPopupLayoutForm(600, 700, FormBorderStyle.FixedToolWindow)
                                              .SetLabel("INSPITEMNAME")
                                              .SetValidationKeyColumn()
                                              .SetDefault("", "")
                                               //.SetPopupQueryPopup((DataRow currentrow) =>
                                               //{
                                               //    if (string.IsNullOrWhiteSpace(_sPLANTID))
                                               //    {
                                               //        this.ShowMessage("NoSelectSite");
                                               //        return false;
                                               //    }

                                               //    return true;
                                               //})
                                               .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                               {
                                                   // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                                                   // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                                                   string sINSPITEMVERSION = "";
                                                   string sINSPITEMID = "";
                                                   foreach (DataRow row in selectedRows)
                                                   {
                                                       sINSPITEMVERSION = (sINSPITEMVERSION.Length == 0 ? sINSPITEMVERSION + "" : sINSPITEMVERSION + ",") + row["INSPITEMVERSION"].ToString();
                                                       sINSPITEMID = (sINSPITEMID.Length == 0 ? sINSPITEMID + "" : sINSPITEMID + ",") + row["INSPITEMID"].ToString();
                                                       //dataGridRow["INSPITEMVERSION"] = (dataGridRow["INSPITEMVERSION"].ToString().Length == 0 ? "" : "|") + row["INSPITEMVERSION"].ToString();
                                                       //dataGridRow["INSPITEMID"] = (dataGridRow["INSPITEMID"].ToString().Length == 0 ? "" : "|") + row["INSPITEMID"].ToString();
                                                   }
                                                   dataGridRow["INSPITEMVERSION"] = sINSPITEMVERSION;
                                                   dataGridRow["INSPITEMID"] = sINSPITEMID;
                                                   if (selectedRows.Count() == 0)
                                                   {
                                                       dataGridRow["INSPITEMID"] = "";
                                                   }
                                               })
                                               ;
          
            inspItem.Conditions.AddTextBox("TXTINSPITEMID").SetLabel("INSPITEMNAME");

            // 팝업 그리드
            // 품목코드           
            inspItem.GridColumns.AddTextBoxColumn("inspitemclassname", 170);//검증항목
            inspItem.GridColumns.AddTextBoxColumn("INSPITEMNAME", 100);//검증종류
            inspItem.GridColumns.AddTextBoxColumn("INSPITEMID", 150);
            // 품목명


            //inspItem.GridColumns.AddTextBoxColumn("inspitemclassid", 100);


        }
        /// <summary>
        ///  Lot No 선택하는 팝업
        /// </summary>
        private void InitializeQCReliabilityLotPopup()
        {
            //팝업 컬럼 설정
            //var conditionLotId = grdQCReliabilityLot.View.AddSelectPopupColumn("LOTID", new SqlQuery("GetLotIdList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            var conditionLotId = grdQCReliabilityLot.View.AddSelectPopupColumn("LOTID", 200, new SqlQuery("GetLotIdListByReliabilityVerificationNonRegularRequest", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                              .SetPopupLayout("SELECTLOTNO", PopupButtonStyles.Ok_Cancel, true, false)
                                              .SetPopupResultCount(1)
                                              .SetPopupLayoutForm(1200, 800, FormBorderStyle.FixedToolWindow)
                                              //.SetPopupAutoFillColumns("PRODUCTDEFNAME")
                                              .SetRelationIds("PLANTID")
                                              .SetLabel("LOTID")
                                              .SetPopupQueryPopup((DataRow currentrow) =>
                                              {
                                                  if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PLANTID")))
                                                  {
                                                      this.ShowMessage("NoSelectSite");
                                                      return false;
                                                  }

                                                  if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("PRODUCTDEFID")))
                                                  {
                                                      this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", Language.Get("ITEMCODE")); //메세지 
                                                      return false;
                                                  }

                                                  return true;
                                              })
                                               .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                               {
                                                   foreach (DataRow row in selectedRows)
                                                   {
                                                       dataGridRow["PROCESSSEGMENTID"] = row["PROCESSSEGMENTID"].ToString();
                                                       dataGridRow["PROCESSSEGMENTVERSION"] = row["PROCESSSEGMENTVERSION"].ToString();
                                                       dataGridRow["AREAID"] = row["AREAID"].ToString();
                                                       dataGridRow["WEEK"] = row["WEEK"].ToString();
                                                       dataGridRow["PROCESSDEFID"] = row["PROCESSDEFID"].ToString();//라우팅 ID
                                                       dataGridRow["PROCESSDEFVERSION"] = row["PROCESSDEFVERSION"].ToString();//라우팅 Version
                                                       dataGridRow["USERSEQUENCE"] = row["USERSEQUENCE"].ToString();//공정수순

                                                       GetMeasureHistory();
                                                   }

                                                   if (selectedRows.Count() == 0)
                                                   {
                                                       dataGridRow["PROCESSSEGMENTID"] = "";
                                                       dataGridRow["PROCESSSEGMENTVERSION"] = "";
                                                       dataGridRow["AREAID"] = "";
                                                       dataGridRow["WEEK"] = "";
                                                       dataGridRow["PROCESSDEFID"] = "";
                                                       dataGridRow["PROCESSDEFVERSION"] = "";
                                                       dataGridRow["USERSEQUENCE"] = "";
                                                   }
                                               })
                                               ;

            conditionLotId.Conditions.AddTextBox("LOTID");

            conditionLotId.Conditions.AddTextBox("PLANTID")
                                   .SetPopupDefaultByGridColumnId("PLANTID")
                                   .SetIsHidden();
            conditionLotId.Conditions.AddTextBox("PRODUCTDEFID")
                       .SetPopupDefaultByGridColumnId("PRODUCTDEFID")
                       .SetIsHidden();

            conditionLotId.Conditions.AddTextBox("PRODUCTDEFVERSION")
           .SetPopupDefaultByGridColumnId("PRODUCTDEFVERSION")
           .SetIsHidden();

            //// 팝업에서 사용되는 검색조건
            //var conditionProductdef = conditionLotId.Conditions.AddSelectPopup("TXTPRODUCTDEFNAME2", new SqlQuery("GetProductionOrderIdListOfLotInput", "10001", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFID")
            //    .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
            //    .SetPopupLayoutForm(600, 800)
            //    .SetLabel("PRODUCTDEF")
            //    .SetPopupResultCount(1)
            //    .SetPopupAutoFillColumns("PRODUCTDEFNAME");

            //conditionProductdef.Conditions.AddTextBox("TXTPRODUCTDEFNAME2");

            //conditionProductdef.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            //conditionProductdef.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);


            //// 품목코드/명
            //conditionLotId.Conditions.AddTextBox("TXTPRODUCTDEFIDNAME")
            //    .SetLabel("TXTPRODUCTDEFNAME");
            //// Lot No
            //conditionLotId.Conditions.AddTextBox("TXTLOTID")
            //    .SetLabel("LOTID");

            //var conditionProcessSegment = conditionLotId.Conditions.AddSelectPopup("TXTPROCESSSEGMENT", new SqlQuery("GetProcessSegmentList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
            //.SetPopupLayout("SELECTPROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
            //.SetPopupLayoutForm(600, 800)
            //.SetLabel("PROCESSSEGMENT")
            //.SetPopupResultCount(1)
            //.SetPopupAutoFillColumns("PROCESSSEGMENTNAME");

            //conditionProcessSegment.Conditions.AddTextBox("PROCESSSEGMENT");

            //conditionProcessSegment.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            //conditionProcessSegment.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

            // 팝업 그리드
            // 양산구분
            //conditionLotId.Conditions.AddComboBox("CBOPRODUCTIONTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //    .SetLabel("PRODUCTIONTYPE")
            //    .SetEmptyItem()
            //    .SetResultCount(0);
            // RTR/SHT
            //conditionLotId.Conditions.AddComboBox("CBORTRSHT", new SqlQuery("GetCodeList", "00001", "CODECLASSID=RTRSHT", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //    .SetLabel("RTRSHT")
            //    .SetEmptyItem()
            //    .SetResultCount(1);
            // 작업구분
            //conditionLotId.Conditions.AddComboBox("CBOWORKTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=WorkType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            //    .SetLabel("WORKTYPE")
            //    .SetResultCount(1);
            // 대공정
            //conditionLotId.Conditions.AddComboBox("CBOTOPPROCESS", new SqlQuery("GetProcessSegmentClassByType", "10001", "PROCESSSEGMENTCLASSTYPE=TopProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
            //    .SetLabel("TOPPROCESSSEGMENTCLASS")
            //    .SetEmptyItem()
            //    .SetResultCount(1);
            //// 중공정
            //conditionLotId.Conditions.AddComboBox("CBOMIDDLEPROCESS", new SqlQuery("GetProcessSegmentClassByType", "10001", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
            //    .SetLabel("MIDDLEPROCESSSEGMENTCLASS")
            //    .SetEmptyItem()
            //    .SetResultCount(1);

            // 팝업 그리드에서 보여줄 컬럼 정의
            // Lot No
            conditionLotId.GridColumns.AddTextBoxColumn("LOTID", 150);
            // 양산구분
            conditionLotId.GridColumns.AddComboBoxColumn("LOTTYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTIONTYPE");
            // 품목코드
            conditionLotId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 100);
            // 품목버전
            conditionLotId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 70);
            // 품목명
            conditionLotId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150);
            // 라우팅 ID
            conditionLotId.GridColumns.AddComboBoxColumn("PROCESSDEFID", 150, new SqlQuery("GetProcessDefinition", "1"), "PROCESSDEFNAME", "PROCESSDEFID");
            // 라우팅 Version
            conditionLotId.GridColumns.AddTextBoxColumn("PROCESSDEFVERSION", 70);
            // 공정 ID
            conditionLotId.GridColumns.AddComboBoxColumn("PROCESSSEGMENTID", 70, new SqlQuery("GetProcessSegmentList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID");

            // 공정 Version
            conditionLotId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTVERSION", 70);
            // 순서
            conditionLotId.GridColumns.AddTextBoxColumn("USERSEQUENCE", 70);
            // Site
            conditionLotId.GridColumns.AddComboBoxColumn("PLANTID", 70, new SqlQuery("GetPlantList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PLANTNAME", "PLANTID");
            // 작업장
            conditionLotId.GridColumns.AddComboBoxColumn("AREAID", 90, new SqlQuery("GetAreaList", "10003", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID");
            // Roll/Sheet
            conditionLotId.GridColumns.AddComboBoxColumn("RTRSHT", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RTRSHT", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 단위
            conditionLotId.GridColumns.AddComboBoxColumn("UNIT", 70, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");
            // 수량
            conditionLotId.GridColumns.AddSpinEditColumn("QTY", 70);
            // PCS 수량
            conditionLotId.GridColumns.AddSpinEditColumn("PCSQTY", 70);
            // PNL 수량
            conditionLotId.GridColumns.AddSpinEditColumn("PANELQTY", 70);
            // M2 수량
            conditionLotId.GridColumns.AddSpinEditColumn("M2QTY", 70);
            // 납기계획일
            conditionLotId.GridColumns.AddDateEditColumn("PLANENDTIME", 100)
                .SetDisplayFormat("yyyy-MM-dd");
            // 기한
            conditionLotId.GridColumns.AddSpinEditColumn("LEFTDATE", 70);
            // 인수 Step PCS 수량
            conditionLotId.GridColumns.AddSpinEditColumn("RECEIVEPCSQTY", 70);
            // 인수 Step PNL 수량
            conditionLotId.GridColumns.AddSpinEditColumn("RECEIVEPANELQTY", 70);
            // 시작 Step PCS 수량
            conditionLotId.GridColumns.AddSpinEditColumn("WORKSTARTPCSQTY", 70);
            // 시작 Step PNL 수량
            conditionLotId.GridColumns.AddSpinEditColumn("WORKSTARTPANELQTY", 70);
            // 완료 Step PCS 수량
            conditionLotId.GridColumns.AddSpinEditColumn("WORKENDPCSQTY", 70);
            // 완료 Step PNL 수량
            conditionLotId.GridColumns.AddSpinEditColumn("WORKENDPANELQTY", 70);
            // 인계 Step PCS 수량
            conditionLotId.GridColumns.AddSpinEditColumn("SENDPCSQTY", 70);
            // 인계 Step PNL 수량
            conditionLotId.GridColumns.AddSpinEditColumn("SENDPANELQTY", 70);
            // 공정 Lead Time
            conditionLotId.GridColumns.AddTextBoxColumn("LEADTIME", 70);
        }
        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ReliabilityVerificationRequestNonRegularPopup_Load;
            grdQCReliabilityLot.View.FocusedRowChanged += View_FocusedRowChanged;
            grdQCReliabilityLot.View.DoubleClick += grdQCReliabilityLotView_DoubleClick;
            treeMeasureHistory.FocusedNodeChanged += TreeMeasureHistory_FocusedNodeChanged;
            btnClose.Click += BtnClose_Click;
            //팝업저장버튼을 클릭시 이벤트
            btnSave.Click += BtnSave_Click;

            btnAddQCLot.Click += BtnAddQCLot_Click;
            btnDeleteQCLot.Click += BtnDeleteQCLot_Click;

            //btnADDAPPROVAL.Click += BtnADDAPPROVAL_Click;

            this.txtPURPOSE.EditValueChanged += new System.EventHandler(this.txtPURPOSE_EditValueChanged);
            this.txtDETAILS.EditValueChanged += new System.EventHandler(this.txtDETAILS_EditValueChanged);
        }
        #region 그리드이벤트
        /// <summary>
        /// fpcReport
        /// 저장할 파일의 Key생성에 사용되는 컬럼목록 추가 해줌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void OnRowChanged(object sender, DataRowChangeEventArgs args)
        {
            if (args.Action == DataRowAction.Add)
            {
                if (!args.Row.Table.Columns.Contains("ENTERPRISEID")) args.Row.Table.Columns.Add("ENTERPRISEID");
                if (!args.Row.Table.Columns.Contains("PLANTID")) args.Row.Table.Columns.Add("PLANTID");

                DataRow dr = args.Row;
                dr["ENTERPRISEID"] = _sENTERPRISEID;
                dr["PLANTID"] = _sPLANTID;
            }
        }
        #endregion

        private void BtnAddQCLot_Click(object sender, EventArgs e)
        {
            grdQCReliabilityLot.View.CloseEditor();
            DataTable dtQCReliabilityLot = grdQCReliabilityLot.DataSource as DataTable;
            DataRow newRow = dtQCReliabilityLot.NewRow();
            newRow["PLANTID"] = _sPLANTID;
            newRow["ENTERPRISEID"] = _sENTERPRISEID;
            dtQCReliabilityLot.Rows.Add(newRow);
        }
        /// <summary>
        /// 제품정보 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteQCLot_Click(object sender, EventArgs e)
        {
            ValidationCheckFile(grdQCReliabilityLot);

            grdQCReliabilityLot.View.DeleteCheckedRows();
        }
        private void txtPURPOSE_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPURPOSE.EditValue.ToString().Length > 0)
            {
                if (grdQCReliabilityLot.View.FocusedRowHandle < 0)
                {
                    ShowMessage("NoSeletedLot"); // LOT을 선택하여 주십시오
                    txtPURPOSE.Text = string.Empty;
                    txtPURPOSE.Refresh();
                    return;
                }
                else
                {
                    DataRow row = this.grdQCReliabilityLot.View.GetFocusedDataRow();
                    if (row != null)
                    {
                        if (row["PURPOSE"].ToString() != txtPURPOSE.EditValue.ToString())
                            row["PURPOSE"] = txtPURPOSE.EditValue;
                    }
                    else
                    {
                        ShowMessage("NoSeletedLot"); // LOT을 선택하여 주십시오
                        txtPURPOSE.Text = string.Empty;
                        txtPURPOSE.Refresh();
                        return;
                    }
                }
            }

        }
        private void txtDETAILS_EditValueChanged(object sender, EventArgs e)
        {
            if (txtDETAILS.EditValue.ToString().Length > 0)
            {
                if (grdQCReliabilityLot.View.FocusedRowHandle < 0)
                {
                    ShowMessage("NoSeletedLot"); // LOT을 선택하여 주십시오
                    txtDETAILS.Text = string.Empty;
                    txtDETAILS.Refresh();
                    return;
                }
                else
                {
                    DataRow row = this.grdQCReliabilityLot.View.GetFocusedDataRow();
                    if (row != null)
                    {
                        if (row["DETAILS"].ToString() != txtDETAILS.EditValue.ToString())
                            row["DETAILS"] = txtDETAILS.EditValue;
                    }
                    else
                    {
                        ShowMessage("NoSeletedLot"); // LOT을 선택하여 주십시오
                        txtDETAILS.Text = string.Empty;
                        txtDETAILS.Refresh();
                        return;
                    }
                }
            }

        }
        private void cboISPOSTPROCESS_EditValueChanged(object sender, EventArgs e)
        {
            if (grdQCReliabilityLot.View.FocusedRowHandle < 0 && cboISPOSTPROCESS.EditValue.ToString() != string.Empty)
            {
                ShowMessage("NoSeletedLot"); // LOT을 선택하여 주십시오
                cboISPOSTPROCESS.EditValue = string.Empty;
                cboISPOSTPROCESS.Refresh();
                return;
            }
            else
            {
                DataRow row = this.grdQCReliabilityLot.View.GetFocusedDataRow();
                if (row != null)
                {
                    if (row["ISPOSTPROCESS"].ToString() != cboISPOSTPROCESS.EditValue.ToString())
                        row["ISPOSTPROCESS"] = cboISPOSTPROCESS.EditValue;
                }
                else if (cboISPOSTPROCESS.EditValue.ToString() != string.Empty)
                {
                    ShowMessage("NoSeletedLot"); // LOT을 선택하여 주십시오
                    cboISPOSTPROCESS.EditValue = string.Empty;
                    cboISPOSTPROCESS.Refresh();
                    return;
                }
            }
        }
        /// <summary>
        /// 저장버튼을 클릭했을때 검사 결과를 저장하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.DialogResult.No;
            ctrlApproval.grdApproval.View.CloseEditor();
            grdQCReliabilityLot.View.CloseEditor();
            fpcReport.grdFileList.View.CloseEditor();
            //ctrlApproval.grdApproval.View.UpdateCurrentRow();
            //ctrlApproval.grdApproval.View.EndDataUpdate();
            //ctrlApproval.grdApproval.View.CheckValidation();
            //Reportfile DataTable
            DataTable fileChanged = fpcReport.GetChangedRows();
            grdQCReliabilityLot.View.CheckValidation();

            DataTable dtReliabilityRequest = new DataTable();                                               //신뢰성의뢰
            DataTable dtApproval = ctrlApproval.grdApproval.DataSource as DataTable;               //결재 정보
            DataTable dtReliabilityLot = grdQCReliabilityLot.GetChangedRows();                              //LOT정보
            DataTable dtFile = fpcReport.GetChangedRows();                                                  //파일

            //결재 유효성 검사
            ctrlApproval.ValidateApproval();

            if (!dtReliabilityRequest.Columns.Contains("REQUESTNO")) dtReliabilityRequest.Columns.Add("REQUESTNO");                             //의뢰번호
            if (!dtReliabilityRequest.Columns.Contains("ENTERPRISEID")) dtReliabilityRequest.Columns.Add("ENTERPRISEID");                       //회사 ID
            if (!dtReliabilityRequest.Columns.Contains("PLANTID")) dtReliabilityRequest.Columns.Add("PLANTID");                                 //Site ID
            if (!dtReliabilityRequest.Columns.Contains("RELIABILITYTYPE")) dtReliabilityRequest.Columns.Add("RELIABILITYTYPE");                 //신뢰성 구분
            if (!dtReliabilityRequest.Columns.Contains("REQUESTTYPE")) dtReliabilityRequest.Columns.Add("REQUESTTYPE");                         //의뢰구분
            if (!dtReliabilityRequest.Columns.Contains("REQUESTDATE")) dtReliabilityRequest.Columns.Add("REQUESTDATE");                         //의뢰일
            if (!dtReliabilityRequest.Columns.Contains("SAMPLERECEIVEDATE")) dtReliabilityRequest.Columns.Add("SAMPLERECEIVEDATE");             //시료접수일
            if (!dtReliabilityRequest.Columns.Contains("ISSAMPLERECEIVE")) dtReliabilityRequest.Columns.Add("ISSAMPLERECEIVE");                 //시료접수여부
            if (!dtReliabilityRequest.Columns.Contains("REQUESTOR")) dtReliabilityRequest.Columns.Add("REQUESTOR");                             //의뢰자
            if (!dtReliabilityRequest.Columns.Contains("REQUESTDEPT")) dtReliabilityRequest.Columns.Add("REQUESTDEPT");                         //의뢰부서
            if (!dtReliabilityRequest.Columns.Contains("REQUESTORJOBPOSITION")) dtReliabilityRequest.Columns.Add("REQUESTORJOBPOSITION");       //의뢰자 직책
            if (!dtReliabilityRequest.Columns.Contains("REQUESTEXTENSIONNO")) dtReliabilityRequest.Columns.Add("REQUESTEXTENSIONNO");           //의뢰자 내선번호
            if (!dtReliabilityRequest.Columns.Contains("REQUESTMOBILENO")) dtReliabilityRequest.Columns.Add("REQUESTMOBILENO");                 //의뢰자 휴대폰번호
            if (!dtReliabilityRequest.Columns.Contains("COMMENTS")) dtReliabilityRequest.Columns.Add("COMMENTS");                               //특이사항
            if (!dtReliabilityRequest.Columns.Contains("MEASURECOMPLETIONDATE")) dtReliabilityRequest.Columns.Add("MEASURECOMPLETIONDATE");     //계측완료일시
            if (!dtReliabilityRequest.Columns.Contains("PARENTREQUESTNO")) dtReliabilityRequest.Columns.Add("PARENTREQUESTNO");                 //원본 의뢰번호
            if (!dtReliabilityRequest.Columns.Contains("ISRECEIPT")) dtReliabilityRequest.Columns.Add("ISRECEIPT");                             //접수여부
            if (!dtReliabilityRequest.Columns.Contains("DESCRIPTION")) dtReliabilityRequest.Columns.Add("DESCRIPTION");                         //설명
            if (!dtReliabilityRequest.Columns.Contains("_STATE_")) dtReliabilityRequest.Columns.Add("_STATE_");
            if (!dtReliabilityRequest.Columns.Contains("ANALYSISTOOL")) dtReliabilityRequest.Columns.Add("ANALYSISTOOL");                       //분석Tool
            if (!dtReliabilityRequest.Columns.Contains("AREAPOINT")) dtReliabilityRequest.Columns.Add("AREAPOINT");                             //영역Point

            DataRow drReliabilityRequest = dtReliabilityRequest.NewRow();

            //drReliabilityRequest["REQUESTNO"] =                                                   //의뢰번호
            drReliabilityRequest["ENTERPRISEID"] = _sENTERPRISEID;                                  //회사 ID
            drReliabilityRequest["PLANTID"] = _sPLANTID;                                            //Site ID
            drReliabilityRequest["RELIABILITYTYPE"] = "NonRegular";                                 //신뢰성 구분
            drReliabilityRequest["REQUESTTYPE"] = cboRequestClass.EditValue.ToString();             //의뢰구분
                                                                                                    //drReliabilityRequest["REQUESTDATE"] = reqData.setRequestdate(Today());                //의뢰일
            drReliabilityRequest["SAMPLERECEIVEDATE"] = dtpSAMPLERECEIVEDATE.Text;                                          //시료접수일
                                                                                                                            //drReliabilityRequest["ISSAMPLERECEIVE"] =                                             //시료접수여부
                                                                                                                            //drReliabilityRequest["REQUESTOR"] = popupREQUESTOR.Text;                                //의뢰자 NAME
            drReliabilityRequest["REQUESTOR"] = popupREQUESTOR.GetValue();                          //의뢰자 ID
            drReliabilityRequest["REQUESTDEPT"] = txtREQUESTDEPT.Text;                              //의뢰부서
            drReliabilityRequest["REQUESTORJOBPOSITION"] = txtREQUESTORJOBPOSITION.Text;            //의뢰자 직책
            drReliabilityRequest["REQUESTEXTENSIONNO"] = txtREQUESTEXTENSIONNO.Text;                //의뢰자 내선번호
            drReliabilityRequest["REQUESTMOBILENO"] = txtREQUESTMOBILENO.Text;                      //의뢰자 휴대폰번호
                                                                                                    //drReliabilityRequest["COMMENTS"] =                                                    //특이사항
                                                                                                    //drReliabilityRequest["MEASURECOMPLETIONDATE"] =                                       //계측완료일시
                                                                                                    //drReliabilityRequest["PARENTREQUESTNO"] =                                             //원본 의뢰번호
                                                                                                    //drReliabilityRequest["ISRECEIPT"] =                                                   //접수여부
                                                                                                    //drReliabilityRequest["DESCRIPTION"] =                                                 //설명
            StringBuilder sANALYSISTOOL = new StringBuilder();
            if (chkVisual.Checked) sANALYSISTOOL.Append((sANALYSISTOOL.Length > 0 ? "," : "") + chkVisual.Tag);
            if (chkMS.Checked) sANALYSISTOOL.Append((sANALYSISTOOL.Length > 0 ? "," : "") + chkMS.Tag);
            if (chkXRay.Checked) sANALYSISTOOL.Append((sANALYSISTOOL.Length > 0 ? "," : "") + chkXRay.Tag);
            if (chkFE.Checked) sANALYSISTOOL.Append((sANALYSISTOOL.Length > 0 ? "," : "") + chkFE.Tag);
            if (chkEDS.Checked) sANALYSISTOOL.Append((sANALYSISTOOL.Length > 0 ? "," : "") + chkEDS.Tag);
            if (chk3D.Checked) sANALYSISTOOL.Append((sANALYSISTOOL.Length > 0 ? "," : "") + chk3D.Tag);
            drReliabilityRequest["ANALYSISTOOL"] = sANALYSISTOOL.ToString();

            StringBuilder sAREAPOINT = new StringBuilder();
            if (chkHole.Checked) sAREAPOINT.Append((sAREAPOINT.Length > 0 ? "," : "") + chkHole.Tag);
            if (chkPlate.Checked) sAREAPOINT.Append((sAREAPOINT.Length > 0 ? "," : "") + chkPlate.Tag);
            if (chkOLB.Checked) sAREAPOINT.Append((sAREAPOINT.Length > 0 ? "," : "") + chkOLB.Tag);
            if (chkPSR.Checked) sAREAPOINT.Append((sAREAPOINT.Length > 0 ? "," : "") + chkPSR.Tag);
            if (chkAdhesive.Checked) sAREAPOINT.Append((sAREAPOINT.Length > 0 ? "," : "") + chkAdhesive.Tag);
            if (chkPI.Checked) sAREAPOINT.Append((sAREAPOINT.Length > 0 ? "," : "") + chkPI.Tag);
            if (chkInforce.Checked) sAREAPOINT.Append((sAREAPOINT.Length > 0 ? "," : "") + chkInforce.Tag);
            drReliabilityRequest["AREAPOINT"] = sAREAPOINT.ToString();

            dtReliabilityRequest.Rows.Add(drReliabilityRequest);

            if (_sMode == "New")
            {
                drReliabilityRequest["_STATE_"] = "added";
            }
            else if (dtReliabilityRequest.Rows.Count > 0)
            {
                drReliabilityRequest["_STATE_"] = "modified";
                drReliabilityRequest["REQUESTNO"] = _sREQUESTNO;
            }

            //if (dtApproval.Rows.Count == 0
            //    && dtReliabilityLot.Rows.Count == 0
            //    && dtReliabilitySegmentRef.Rows.Count == 0
            //    && dtFile.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}

            foreach (DataRow dr in dtReliabilityLot.Rows)
            {
                //동일한 키가 존재 하면 서버단에서 기존 데이터를 삭제하기 떄문에 중복 체크를 해야 한다.
                var dList = from r in (grdQCReliabilityLot.DataSource as DataTable).AsEnumerable()
                            group r by new { PRODUCTDEFID = r.Field<string>("PRODUCTDEFID") == null ? "" : r.Field<string>("PRODUCTDEFID").ToString(), LOTID = r.Field<string>("LOTID") == null ? "" : r.Field<string>("LOTID") } into g
                            select g;
                dList = dList.Where(item => item.Count() > 1);

                if (dList.ToList().Count > 0)
                {
                    this.ShowMessage(MessageBoxButtons.OK, "CheckDupliProductDefId", Language.Get("ITEMCODE")); //동일 품목과 LOTID가 존재 합니다. 
                    return;
                }

                if (dr["_STATE_"].ToString() == "added" && dr["PRODUCTDEFID"].ToString() == string.Empty)//품목 없음
                {
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", Language.Get("ITEMCODE")); //메세지 
                    return;
                }
                string sREQUESTQTY = dr["REQUESTQTY"].ToString();
                int iREQUESTQTY = 0;
                if (!Int32.TryParse(sREQUESTQTY, out iREQUESTQTY))
                {
                    this.ShowMessage(MessageBoxButtons.OK, "ReliabilityVerificationRequestQty"); //의뢰수량은 0 이상입니다 
                    return;
                }
                else if (iREQUESTQTY <= 0)
                {
                    this.ShowMessage(MessageBoxButtons.OK, "ReliabilityVerificationRequestQty"); //의뢰수량은 0 이상입니다 
                    return;
                }
                if (dr["LOTID"] == DBNull.Value || dr["LOTID"].ToString() == string.Empty)
                {
                    dr["LOTID"] = "*";
                }

                if (!dtReliabilityLot.Columns.Contains("OriginalLotID")) dtReliabilityLot.Columns.Add("OriginalLotID");
                DataColumn dc = dtReliabilityLot.Columns["LOTID"];

                if (dr["_STATE_"].ToString() == "modified")
                {
                    dr["OriginalLotID"] = dr[dc, DataRowVersion.Original];
                }
                else
                {
                    dr["OriginalLotID"] = dr["LOTID"];
                }

                if (dr["PURPOSE"].ToString().Length > 160)
                {
                    dr["PURPOSE"] = dr["PURPOSE"].ToString().Substring(0, 160);
                }

                if (dr["DETAILS"].ToString().Length > 2500)
                {
                    dr["DETAILS"] = dr["DETAILS"].ToString().Substring(0, 2500);
                }
            }

            result = this.ShowMessage(MessageBoxButtons.YesNo, "InfoPopupSave");//변경 내용을 저장하시겠습니까??

            if (result == System.Windows.Forms.DialogResult.No) return;

            try
            {
                this.ShowWaitArea();
                btnSave.Enabled = false;
                btnClose.Enabled = false;

                dtReliabilityRequest.TableName = "list1";           //신뢰성의뢰
                dtApproval.TableName = "list2";                     //결재 정보
                dtReliabilityLot.TableName = "list3";               //LOT정보
                dtFile.TableName = "list5";

                if (_sMode == "Modify")
                {
                    if (dtReliabilityLot.Rows.Count > 0 && !dtReliabilityLot.Columns.Contains("REQUESTNO")) dtReliabilityLot.Columns.Add("REQUESTNO");
                    if (dtFile.Rows.Count > 0 && !dtFile.Columns.Contains("REQUESTNO")) dtFile.Columns.Add("REQUESTNO");

                    foreach (DataRow dr in dtReliabilityLot.Rows)
                    {
                        dr["REQUESTNO"] = _sREQUESTNO;
                    }
                    foreach (DataRow dr in dtFile.Rows)
                    {
                        dr["REQUESTNO"] = _sREQUESTNO;
                    }
                }

                if (dtApproval.Rows.Count > 0 && !dtApproval.Columns.Contains("REQUESTNO")) dtApproval.Columns.Add("REQUESTNO");
                if (dtApproval.Rows.Count > 0 && !dtApproval.Columns.Contains("_STATE_")) dtApproval.Columns.Add("_STATE_");//자바 에서 모두 삭제후 INSERT 처리 하기때문에
                foreach (DataRow dr in dtApproval.Rows)
                {
                    dr["REQUESTNO"] = _sREQUESTNO;
                    dr["_STATE_"] = "added";//자바 에서 모두 삭제후 INSERT 처리 하기때문에
                    dr["APPROVALTYPE"] = "ReliabilityVerificationRequestNonRegular";
                }

                if (dtFile.Rows.Count > 0 && !dtFile.Columns.Contains("RESOURCETYPE")) dtFile.Columns.Add("RESOURCETYPE");
                foreach (DataRow dr in dtFile.Rows)
                {
                    dr["RESOURCETYPE"] = "ReliabilityVerificationRequestNonRegular";
                }

                DataSet rullSet = new DataSet();
                rullSet.Tables.Add(dtReliabilityRequest);
                rullSet.Tables.Add(dtApproval.Copy());
                rullSet.Tables.Add(dtReliabilityLot);
                rullSet.Tables.Add(dtFile);
                ExecuteRule("SaveReliabilityVerificationRequestNonRegularRecept", rullSet);

                //메일 보내기
                ctrlApproval.ApprovalMail("신뢰성 의뢰내용 확인 및 결재요청", _sbMailContents.ToString());
                // 반려할 경우 기안자에게 반려 메일 보냄-2020.01.16
                ctrlApproval.ApprovalMail("신뢰성 의뢰 반려", _sbMailContents.ToString());
                ShowMessage("SuccessSave");
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnSave.Enabled = true;
                btnClose.Enabled = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        /// <summary>
        /// 그리드의 Row Click시 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focusedRowChanged();
        }
        /// <summary>
        /// 그리드 클릭시 이력조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdQCReliabilityLotView_DoubleClick(object sender, EventArgs e)
        {
            if (grdQCReliabilityLot.View.FocusedRowHandle < 0) return;

            GetMeasureHistory();
        }
        /// <summary>        
        /// 포커시가 변경 되었을 경우 선택한 LOT에 해당하는 이력 조회
        /// </summary>
        private void TreeMeasureHistory_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            DataRow dr = treeMeasureHistory.GetFocusedDataRow();

            Dictionary<string, object> param = new Dictionary<string, object>();
            if (dr == null)
                return;
            param.Add("P_LOTID", dr["ID"]);
            param.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            grdMeasureHistory.DataSource = SqlExecuter.Query("GetManufacturingHistoryPopupList", "10001", param);
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
        private void ReliabilityVerificationRequestNonRegularPopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            dtpREQUESTDATE.Enabled = false;
            dtpSAMPLERECEIVEDATE.Enabled = false;
            InitializeGrid();
            fpcReport.LanguageKey = "ATTACHEDFILE";

            popupREQUESTOR.SelectPopupCondition = UserPopup();

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "CODECLASSID", "RequestClass"}
            };

            cboRequestClass.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboRequestClass.ValueMember = "CODEID";
            cboRequestClass.DisplayMember = "CODENAME";
            //cboRequestClass.EditValue = "Y";
            //cboRequestClass.EmptyItemCaption = Language.Get("ALLVIEWS");
            //cboRequestClass.EmptyItemValue = "*";
            //cboRequestClass.UseEmptyItem = true;
            cboRequestClass.DataSource = SqlExecuter.Query("GetCodeList", "00001", param);
            cboRequestClass.ShowHeader = false;
            cboRequestClass.ItemIndex = 0;
            //cboRequestClass.EditValue = "*";
            param = new Dictionary<string, object>
            {
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "CODECLASSID", "ReliabilityProcessingStatus"}
            };

            cboISPOSTPROCESS.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboISPOSTPROCESS.ValueMember = "CODEID";
            cboISPOSTPROCESS.DisplayMember = "CODENAME";
            //cboISPOSTPROCESS.EditValue = "Y";
            //cboISPOSTPROCESS.EmptyItemCaption = Language.Get("ALLVIEWS");
            //cboISPOSTPROCESS.EmptyItemValue = "*";
            //cboISPOSTPROCESS.UseEmptyItem = true;
            cboISPOSTPROCESS.DataSource = SqlExecuter.Query("GetCodeList", "00001", param);
            cboISPOSTPROCESS.ShowHeader = false;
            //cboISPOSTPROCESS.EditValue = "*";
            //cboISPOSTPROCESS.ItemIndex = 0;

            cboISPOSTPROCESS.EditValueChanged += new EventHandler(cboISPOSTPROCESS_EditValueChanged);

            Search();
            focusedRowChanged();
        }

        #endregion

        #region Private Function
        private void GetMeasureHistory()
        {
            if (grdQCReliabilityLot.View.FocusedRowHandle < 0) return;

            //DXMouseEventArgs args = e as DXMouseEventArgs;
            //GridView view = sender as GridView;
            //GridHitInfo info = view.CalcHitInfo(args.Location);
            //if (info.InRowCell && (info.Column.FieldName == "RECEIVE"))//시료접수여부
            //    return;

            DataRow row = this.grdQCReliabilityLot.View.GetFocusedDataRow();
            string sLOTID = row["LOTID"].ToString();
            if (sLOTID == string.Empty) return;

            treeMeasureHistory.ClearNodes();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_LOTID", sLOTID);
            treeMeasureHistory.SetMember("NAME", "ID", "PARENT");
            treeMeasureHistory.DataSource = SqlExecuter.Query("GetManufacturingHistory_Tree", "10001", param);
            treeMeasureHistory.PopulateColumns();
            treeMeasureHistory.ExpandAll();

            treeMeasureHistory.SetFocusedNode(treeMeasureHistory.FindNodeByFieldValue("ID", sLOTID));
        }
        /// <summary>
        /// 체크한 항목이 있는지 확인
        /// </summary>
        private void ValidationCheckFile(SmartBandedGrid grd)
        {
            DataTable selectedFiles = grd.View.GetCheckedRows();

            if (selectedFiles.Rows.Count < 1)
            {
                throw MessageException.Create("GridNoChecked");
            }
        }
        /// <summary>
        /// 그리드의 포커스 행 변경시 행의 작업목록의 내용을 매핑처리 함.
        /// </summary>
        private void focusedRowChanged()
        {
            //포커스 행 체크 
            if (grdQCReliabilityLot.View.FocusedRowHandle < 0) return;

            var row = grdQCReliabilityLot.View.GetDataRow(grdQCReliabilityLot.View.FocusedRowHandle);

            txtPURPOSE.Text = string.Empty;
            txtDETAILS.Text = string.Empty;

            if (txtPURPOSE.Text != row["PURPOSE"].ToString())
                txtPURPOSE.Text = row["PURPOSE"].ToString();
            if (txtDETAILS.Text != row["DETAILS"].ToString())
                txtDETAILS.Text = row["DETAILS"].ToString();
            cboISPOSTPROCESS.EditValue = row["ISPOSTPROCESS"].ToString();

            //grdSegmentRef.View.ClearDatas();

            //string sLOTID = row["LOTID"].ToString();
            //string sREQUESTNO = row["REQUESTNO"].ToString();
            //if (sREQUESTNO == string.Empty) return;
            //Dictionary<string, object> param = new Dictionary<string, object>();
            //param.Add("P_ENTERPRISEID", _sENTERPRISEID);
            //param.Add("P_PLANTID", _sPLANTID);
            //param.Add("P_REQUESTNO", sREQUESTNO);
            //param.Add("P_LOTID", sLOTID);
            //DataTable dtReliabilitySegmentRef = SqlExecuter.Query("GetQCReliabilitySegmentRef", "10001", param);
            //grdSegmentRef.DataSource = dtReliabilitySegmentRef;

            string sLOTID = row["LOTID"].ToString();
            if (sLOTID == string.Empty) return;

            treeMeasureHistory.ClearNodes();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_LOTID", sLOTID);
            treeMeasureHistory.SetMember("NAME", "ID", "PARENT");
            treeMeasureHistory.DataSource = SqlExecuter.Query("GetManufacturingHistory_Tree", "10001", param);
            treeMeasureHistory.PopulateColumns();
            treeMeasureHistory.ExpandAll();

            treeMeasureHistory.SetFocusedNode(treeMeasureHistory.FindNodeByFieldValue("ID", sLOTID));
        }

        private void CheckAllCtrl(bool bEnable)
        {
            txtREQUESTDEPT.Enabled = bEnable;
            txtREQUESTEXTENSIONNO.Enabled = bEnable;
            txtREQUESTORJOBPOSITION.Enabled = bEnable;
            txtREQUESTMOBILENO.Enabled = bEnable;
            popupREQUESTOR.Enabled = bEnable;
            cboRequestClass.Enabled = bEnable;
            btnAddQCLot.Enabled = bEnable;
            btnDeleteQCLot.Enabled = bEnable;
            //txtPURPOSE.Enabled = bEnable;
            //txtDETAILS.Enabled = bEnable;
            cboISPOSTPROCESS.Enabled = bEnable;

            // 기안자가 승인을 해도 검토/합의, 승인 부서 담당자가 수정 가능하도록 비활성화 주석 처리-2020.01.16
            //chkVisual.Enabled = bEnable;
            //chkMS.Enabled = bEnable;
            //chkXRay.Enabled = bEnable;
            //chkFE.Enabled = bEnable;
            //chkEDS.Enabled = bEnable;
            //chk3D.Enabled = bEnable;
            //chkHole.Enabled = bEnable;
            //chkPlate.Enabled = bEnable;
            //chkOLB.Enabled = bEnable;
            //chkPSR.Enabled = bEnable;
            //chkAdhesive.Enabled = bEnable;
            //chkPI.Enabled = bEnable;
            //chkInforce.Enabled = bEnable;


            //btnSave.Enabled = bEnable;

            //ctrlApproval.btnADDAPPROVAL.Enabled = bEnable;

            fpcReport.btnFileAdd.Enabled = bEnable;
            fpcReport.btnFileDelete.Enabled = bEnable;
        }
        /// <summary>
        /// 검사자 팝업
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup UserPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            popup.SetPopupLayoutForm(565, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, true);
            popup.Id = "USERID";
            popup.SearchQuery = new SqlQuery("GetUserApproval", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "USERNAME";
            popup.ValueFieldName = "CHARGERID";
            popup.LanguageKey = "CHARGERID";
            popup.IsRequired = true;
            popup.SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                if (selectedRows.Count() < 1)
                {
                    return;
                }

                foreach (DataRow row in selectedRows)
                {
                    //dataGridRow["DEFECTCODE"] = row["DEFECTCODE"].ToString();
                    //dataGridRow["DEFECTNAME"] = row["DEFECTNAME"].ToString();

                    txtREQUESTDEPT.Text = row["DEPARTMENT"].ToString();
                    txtREQUESTORJOBPOSITION.Text = row["POSITION"].ToString();
                    txtREQUESTMOBILENO.Text = row["CELLPHONENUMBER"].ToString();
                }
            });

            popup.Conditions.AddTextBox("P_USERNAME").SetLabel("NAME");

            popup.GridColumns.AddTextBoxColumn("CHARGERID", 100).SetLabel("ID");
            popup.GridColumns.AddTextBoxColumn("USERNAME", 100);
            popup.GridColumns.AddTextBoxColumn("DEPARTMENT", 100);
            popup.GridColumns.AddTextBoxColumn("POSITION", 60);
            popup.GridColumns.AddTextBoxColumn("CELLPHONENUMBER", 100);

            return popup;
        }
        private DataTable GetApprovalStateAll()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            return SqlExecuter.Query("GetApprovalAllStateByReliabilityVerificationRequest", "10001", values);
        }
        private DataTable GetApprovalState(string processType)
        {
            //-기안(Draft)
            //- 검토(Review)
            //- 승인(Approval)
            //- 수신(Receiving)
            string codeClassID = string.Empty;
            switch (processType)
            {
                case "Review":
                case "Approval":
                    codeClassID = "ApprovalSettleState";
                    break;
                case "Receiving":
                    codeClassID = "ReceivingSettleState";
                    break;
                default:
                    codeClassID = "DraftSettleState";
                    break;
            }
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("CODECLASSID", codeClassID);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            return SqlExecuter.Query("GetCodeList", "00001", values);
        }
        //popup이 닫힐때 그리드를 재조회 하는 이벤트 (팝업결과 저장 후 재조회)
        private async void Popup_FormClosed()
        {
            //await OnSearchAsync();
        }
        /// <summary>
        /// 협력업체 정기등록 조회
        /// </summary>
        private void Search()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_ENTERPRISEID", _sENTERPRISEID);
            param.Add("P_PLANTID", _sPLANTID);
            param.Add("P_REQUESTNO", _sREQUESTNO);
            param.Add("P_RELIABILITYTYPE", "NonRegular");
            param.Add("P_APPROVALTYPE", "ReliabilityVerificationRequestNonRegular");

            if (_sMode == "Modify")
            {
                DataTable dtReliabilityRequest = SqlExecuter.Query("GetReliabilityVerificationRequestNonRegularRgisterList", "10001", param);           //신뢰성의뢰
                DataTable dtApproval = SqlExecuter.Query("GetQCApproval", "10001", param);                     //결재 정보
                DataTable dtReliabilityLot = SqlExecuter.Query("GetQCReliabilityLot", "10001", param);               //LOT정보
                //DataTable dtReliabilitySegmentRef = SqlExecuter.Query("GetQCReliabilitySegmentRef", "10001", param);

                Dictionary<string, object> fileValues = new Dictionary<string, object>();
                //이미지 파일Search parameter
                fileValues.Add("P_FILERESOURCETYPE", "ReliabilityVerificationRequestNonRegular");
                fileValues.Add("P_FILERESOURCEID", _sREQUESTNO);
                fileValues.Add("P_FILERESOURCEVERSION", "0");

                DataTable dtFile = SqlExecuter.Query("SelectFileInspResult", "10001", fileValues);
                ctrlApproval.SetApproval = dtApproval;
                //ctrlApproval.grdApproval.DataSource = dtApproval;
                //ctrlApproval.Approval_Enable();

                fpcReport.DataSource = dtFile;
                grdQCReliabilityLot.DataSource = dtReliabilityLot;


                //grdSegmentRef.DataSource = dtReliabilitySegmentRef;

                if (dtReliabilityRequest != null && dtReliabilityRequest.Rows.Count > 0)
                {
                    DataRow dr = dtReliabilityRequest.Rows[0];
                    DateTime deREQUESTDATE = new DateTime();
                    if (DateTime.TryParse(dr["REQUESTDATE"].ToString(), out deREQUESTDATE))
                    {
                        dtpREQUESTDATE.Text = deREQUESTDATE.ToShortDateString();
                    }
                    DateTime deSAMPLERECEIVEDATE = new DateTime();
                    if (DateTime.TryParse(dr["SAMPLERECEIVEDATE"].ToString(), out deSAMPLERECEIVEDATE))
                    {
                        dtpSAMPLERECEIVEDATE.Text = deSAMPLERECEIVEDATE.ToShortDateString();
                    }
                    txtREQUESTDEPT.Text = dr["REQUESTDEPT"].ToString();
                    txtREQUESTEXTENSIONNO.Text = dr["REQUESTEXTENSIONNO"].ToString();
                    txtREQUESTORJOBPOSITION.Text = dr["REQUESTORJOBPOSITION"].ToString();
                    txtREQUESTMOBILENO.Text = dr["REQUESTMOBILENO"].ToString();
                    popupREQUESTOR.SetValue(dr["REQUESTOR"].ToString());
                    popupREQUESTOR.EditValue = dr["USERNAME"].ToString();
                    cboRequestClass.EditValue = dr["REQUESTTYPE"].ToString();

                    string sANALYSISTOOL = dr["ANALYSISTOOL"].ToString();
                    string[] arrANALYSISTOOL = sANALYSISTOOL.Split(',');
                    foreach (string s in arrANALYSISTOOL)
                    {
                        if (s.Trim().Length > 0 && s.Trim() == "Visual") chkVisual.Checked = true;
                        if (s.Trim().Length > 0 && s.Trim() == "MS") chkMS.Checked = true;
                        if (s.Trim().Length > 0 && s.Trim() == "XRay") chkXRay.Checked = true;
                        if (s.Trim().Length > 0 && s.Trim() == "FE") chkFE.Checked = true;
                        if (s.Trim().Length > 0 && s.Trim() == "EDS") chkEDS.Checked = true;
                        if (s.Trim().Length > 0 && s.Trim() == "3D") chk3D.Checked = true;
                    }

                    string sAREAPOINT = dr["AREAPOINT"].ToString();
                    string[] arrAREAPOINT = sAREAPOINT.Split(',');
                    foreach (string s in arrAREAPOINT)
                    {
                        if (s.Trim().Length > 0 && s.Trim() == "Hole") chkHole.Checked = true;
                        if (s.Trim().Length > 0 && s.Trim() == "Plate") chkPlate.Checked = true;
                        if (s.Trim().Length > 0 && s.Trim() == "OLB") chkOLB.Checked = true;
                        if (s.Trim().Length > 0 && s.Trim() == "PSR") chkPSR.Checked = true;
                        if (s.Trim().Length > 0 && s.Trim() == "Adhesive") chkAdhesive.Checked = true;
                        if (s.Trim().Length > 0 && s.Trim() == "PI") chkPI.Checked = true;
                        if (s.Trim().Length > 0 && s.Trim() == "Inforce") chkInforce.Checked = true;
                    }
                    //Language.Get("REQUESTDATETIME")
                    //<p style="text-align:center;">가운데 정렬</p>
                    _sbMailContents = new StringBuilder();
                    _sbMailContents.Append("<p style='text-align:left;'>" + string.Format("○ {0}:", Language.Get("REQUESTDATETIME")));
                    _sbMailContents.AppendLine(dr["REQUESTDATE"].ToString() + "</p><br>");
                    _sbMailContents.Append("<p style='text-align:left;'>" + string.Format("○ {0}/{1}/{2}:", Language.Get("DEPARTMENT"), Language.Get("DUTY"), Language.Get("REQUESTTYPE")));
                    _sbMailContents.AppendLine(dr["REQUESTDEPT"].ToString() + "/" + dr["REQUESTDEPT"].ToString() + "/" + cboRequestClass.Text + "</p><br>");
                    _sbMailContents.Append("<p style='text-align:left;'>" + string.Format("○ {0}/{1}/{2}:", Language.Get("OSPREQUESTUSER"), Language.Get("EXTENSIONNUMBER"), Language.Get("PHONENUMBER")));
                    _sbMailContents.AppendLine(dr["REQUESTOR"].ToString() + "/" + dr["REQUESTEXTENSIONNO"].ToString() + "/" + dr["REQUESTMOBILENO"].ToString() + "</p><br>");

                    DataTable dtMailLot = SqlExecuter.Query("GetRequestNonRegularQCReliabilityLotMail", "10001", param);               //검증항목별 LOT
                    if (dtMailLot != null && dtMailLot.Rows.Count > 0)
                    {
                        foreach (DataRow drMailLot in dtMailLot.Rows)
                        {
                            _sbMailContents.Append("<p style='text-align:left;'>" + string.Format("○ {0}/{1}/{2}/{3}:", Language.Get("ITEMNAME"), Language.Get("PRODUCTREVISIONINPUT"), Language.Get("LOT"), Language.Get("INSPECTIONITEM")));
                            _sbMailContents.AppendLine(drMailLot["PRODUCTDEFNAME"].ToString() + "/" + drMailLot["PRODUCTDEFVERSION"].ToString() + "/" + drMailLot["LOTID"].ToString() + "/" + drMailLot["INSPITEMNAME"].ToString() + "</p><br>");
                        }
                    }

                    _sbMailContents.Append("<p style='text-align:left;'>" + string.Format("○ {0}/{1}:", Language.Get("AnalysisTool"), Language.Get("DomainPoint")));
                    _sbMailContents.AppendLine(sANALYSISTOOL + "/" + sAREAPOINT + "</p><br>");
                    if (dtReliabilityLot != null && dtReliabilityLot.Rows.Count > 0)
                    {
                        DataRow drPURPOSE = dtReliabilityLot.Rows[0];
                        _sbMailContents.Append("<p style='text-align:left;'>" + string.Format("○ {0}", Language.Get("REQUESTPURPOSE")));
                        _sbMailContents.AppendLine(drPURPOSE["PURPOSE"].ToString() + "</p><br>");
                        _sbMailContents.Append("<p style='text-align:left;'>" + string.Format("○ {0}:", Language.Get("REQUESTDETAILS")));
                        _sbMailContents.AppendLine(drPURPOSE["DETAILS"].ToString() + "</p><br>");
                        _sbMailContents.Append("<p style='text-align:left;'>" + string.Format("○ {0}:", Language.Get("ISPOSTPROCESS")));
                        _sbMailContents.AppendLine(drPURPOSE["ISPOSTPROCESS_NAME"].ToString() + "</p><br>");
                    }

                }

                //APPROVALSTATE : 승인(Approval), 회수(Reclamation), 반려(Companion)
                //PROCESSTYPE : 기안(Draft), 검토(Review), 승인(Approval), 수신(Receiving)
                if (dtApproval.AsEnumerable().Where(r => r["PROCESSTYPE"].ToString() == "Draft" && r["APPROVALSTATE"].ToString() == "Approval").ToList().Count > 0)
                {
                    CheckAllCtrl(false); //btnSave.Enabled = false;//기안자가 승인했으면
                }
                else
                {
                    var user = dtApproval.AsEnumerable().Where(r => r["PROCESSTYPE"].ToString() == "Draft").FirstOrDefault();
                    string sDraftUser = user == null ? string.Empty : user["CHARGERID"].ToString();
                    string sAPPROVALSTATE = user == null ? string.Empty : user["APPROVALSTATE"].ToString();
                    if (sDraftUser != UserInfo.Current.Id)
                        CheckAllCtrl(false);// btnSave.Enabled = false;//로그인과 기안자가 다르면
                    else
                        CheckAllCtrl(true); //btnSave.Enabled = true;
                }
                //결재선에 로그인한 사람이 포함되어 있어야 결재자 버튼을 활성화 할 수 있다.
                //if(dtApproval.AsEnumerable().Where(r => r["CHARGERID"].ToString() == UserInfo.Current.Id).ToList().Count > 0)
                //    ctrlApproval.btnADDAPPROVAL.Enabled = true;
                //else
                //    ctrlApproval.btnADDAPPROVAL.Enabled = false;
                //ctrlApproval.Approval_Enable();  

                //모든결재선이 승인됨 -> 시료접수 -> 검사결과등록
                //시료접수되었다면 수정이 불가능 하다
                if (_sISSAMPLERECEIVE == "1")
                    btnSave.Enabled = false;
                else
                    btnSave.Enabled = true;
            }
            else
            {
                DataTable dtApproval = SqlExecuter.Query("GetQCApproval", "10001", param);                     //결재 정보
                ctrlApproval.SetApproval = dtApproval;
            }
        }

        #endregion



        #region 유효성 검사
        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        //protected override void OnValidateContent()
        //{
        //    base.OnValidateContent();

        //    grdAuditManageregist.View.CheckValidation();

        //    DataTable changed = grdAuditManageregist.GetChangedRows();

        //    if (changed.Rows.Count == 0)
        //    {
        //        // 저장할 데이터가 존재하지 않습니다.
        //        throw MessageException.Create("NoSaveData");
        //    }
        //}
        #endregion


    }
}
