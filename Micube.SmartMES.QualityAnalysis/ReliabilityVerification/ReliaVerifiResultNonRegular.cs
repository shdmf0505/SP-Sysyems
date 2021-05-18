#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
//using Micube.SmartMES.ProcessManagement;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using System.IO;
using DevExpress.LookAndFeel;
using System.Reflection;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.ComponentModel;
using Micube.Framework.SmartControls.Grid.BandedGrid;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 신뢰성검증 > 신뢰성 검증 의뢰(정기외)
    /// 업  무  설  명  : 신뢰성 정기 의뢰를 하는 화면이다. 동도금 정기적으로 신뢰성 검증 하는 의뢰를 한다. 계측 값 등록 시 자동으로 신뢰성 의뢰가 등록 됨.  
    /// 생    성    자  : 유호석
    /// 생    성    일  : 2019-09-20
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReliaVerifiResultNonRegular : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ReliaVerifiResultNonRegular()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGridReliabilityVerificationRequestRgisterRegular(); // 신뢰성 의뢰접수(정기) 그리드 초기화
            InitializeEvent();
        } 

        /// <summary>        
        /// 그리드 초기화(신뢰서 의뢰 접수(정기))
        /// </summary>
        private void InitializeGridReliabilityVerificationRequestRgisterRegular()
        {
            grdReliabiVerifiReqRgistRegular.GridButtonItem -= GridButtonItem.Delete; // 삭제 버튼 비활성화
            grdReliabiVerifiReqRgistRegular.GridButtonItem -= GridButtonItem.Add; // 추가 버튼 비활성화
            grdReliabiVerifiReqRgistRegular.GridButtonItem -= GridButtonItem.Copy; // 복사 버튼 비활성화
            grdReliabiVerifiReqRgistRegular.GridButtonItem -= GridButtonItem.Import; // Import 버튼 비활성화

            grdReliabiVerifiReqRgistRegular.View.SetSortOrder("SAMPLERECEIVEDATE");
            grdReliabiVerifiReqRgistRegular.View.SetSortOrder("REQUESTNO");

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("REQUESTNO", 140)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 의뢰번호

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("SAMPLERECEIVEDATE", 180) 
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 시료접수일시

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("INSPECTIONDATE", 180)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("VERIFICOMPDATE"); // 검증완료일시

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("TRANSITIONDATE", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right); // 경과일

            //의뢰구분
            grdReliabiVerifiReqRgistRegular.View.AddComboBoxColumn("REQUESTTYPE", 150, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=RequestClass"))
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();// 공통코드(YesNo)

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("REQUESTDEPT", 160)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left); // 의뢰부서(공정)

            grdReliabiVerifiReqRgistRegular.View
                .AddTextBoxColumn("USERNAME", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center); // 의뢰자

            grdReliabiVerifiReqRgistRegular.View.PopulateColumns();


            //grdQCReliabilityLot
            grdQCReliabilityLot.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;
            grdQCReliabilityLot.View.SetIsReadOnly();
            grdQCReliabilityLot.View.AddTextBoxColumn("REQUESTNO", 100).SetIsReadOnly().SetIsHidden();                //의뢰번호
            grdQCReliabilityLot.View.AddTextBoxColumn("ENTERPRISEID", 100).SetIsReadOnly().SetIsHidden();             //회사 ID
            grdQCReliabilityLot.View.AddTextBoxColumn("PLANTID", 100).SetIsReadOnly().SetIsHidden();                  //Site ID
            grdQCReliabilityLot.View.AddTextBoxColumn("CUSTOMERID").SetTextAlignment(TextAlignment.Center).SetIsReadOnly();                    // 고객사 ID
            grdQCReliabilityLot.View.AddTextBoxColumn("CUSTOMERNAME").SetTextAlignment(TextAlignment.Left).SetIsReadOnly(); // 고객사 명                     // 고객사 명
            grdQCReliabilityLot.View.AddTextBoxColumn("PRODUCTDEFID", 130).SetIsReadOnly();             //제품 정의 ID/품목
            grdQCReliabilityLot.View.AddTextBoxColumn("PRODUCTDEFNAME", 180).SetIsReadOnly();
            grdQCReliabilityLot.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetTextAlignment(TextAlignment.Right).SetIsReadOnly().SetIsHidden();        //제품 정의 Version
            grdQCReliabilityLot.View.AddTextBoxColumn("LOTID", 200).SetIsReadOnly();                    //LOT ID
            //grdQCReliabilityLot.View.AddSpinEditColumn("INSPECTIONRESULT", 100);                                              //판정결과
            grdQCReliabilityLot.View.AddComboBoxColumn("INSPECTIONRESULT", 60, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=OKNG")).SetTextAlignment(TextAlignment.Center);// 판정결과 공통코드(OKNG)
            //grdQCReliabilityLot.View.AddSpinEditColumn("ISNCRPUBLISH", 100);                                              //NCR 발행여부(Y/N)
            grdQCReliabilityLot.View.AddComboBoxColumn("ISNCRPUBLISH", 100, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=YesNo")).SetTextAlignment(TextAlignment.Center);// NCR 발행여부(Y/N) 공통코드(YesNo)
            //DEFECTCODE
            //DEFECTNAME
            grdQCReliabilityLot.View.AddTextBoxColumn("DEFECTNAME", 200);
            //DefectCodePopup(); // 불량코드
            //grdQCReliabilityLot.View.AddSpinEditColumn("ISCOMPLETION", 100);                                              //완료여부(Y/N)
            grdQCReliabilityLot.View.AddComboBoxColumn("ISCOMPLETION", 60, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=YesNo")).SetTextAlignment(TextAlignment.Center);// 완료여부(Y/N) 공통코드(YesNo)
            grdQCReliabilityLot.View.PopulateColumns();

        }
        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            //grdReliabiVerifiReqRgistRegular.View.FocusedRowChanged += View_FocusedRowChanged;
            grdReliabiVerifiReqRgistRegular.View.DoubleClick += GrdReliabiVerifiReqRgistRegularView_DoubleClick;
            grdReliabiVerifiReqRgistRegular.View.Click += GrdReliabiVerifiReqRgistRegularView_Click;
        }
        /// <summary>        
        /// 의리서 출력 완료후 출력일시 반영 및 조회
        /// </summary>
        private void PrintingSystem_EndPrint1(object sender, EventArgs e)
        {
            Search();
        }
        private void GrdReliabiVerifiReqRgistRegularView_Click(object sender, EventArgs e)
        {
            focusedRowChanged();
        }
        /// <summary>        
        /// 의뢰접수 목록 더블클릭 시
        /// </summary>
        private void GrdReliabiVerifiReqRgistRegularView_DoubleClick(object sender, EventArgs e)
        {
            if (grdReliabiVerifiReqRgistRegular.View.FocusedRowHandle < 0) return;
            
            DXMouseEventArgs args = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(args.Location);
            DataRow row = this.grdReliabiVerifiReqRgistRegular.View.GetFocusedDataRow();
            ReliaVerifiResultNonRegularPopup popup = new ReliaVerifiResultNonRegularPopup("Modify", row["ENTERPRISEID"].ToString(), row["PLANTID"].ToString(), row["REQUESTNO"].ToString());
            popup.Owner = this;
            popup.btnSave.Enabled = btnFlag.Enabled;
            popup.ShowDialog();

            if (popup.DialogResult == DialogResult.OK)
            {
                Popup_FormClosed();
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
        #endregion

        #region 툴바
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            //DataTable changed = grdCodeClass.GetChangedRows();

            //ExecuteRule("SaveCodeClass", changed);
        }
        #endregion

        #region 검색
        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();

            string[] arrayStr = values["P_PRODUCTDEFID"] != null ? values["P_PRODUCTDEFID"].ToString().Split('|') : "".Split('|');
            string sPRODUCTDEFID = string.Empty;
            string sPRODUCTDEFVERSION = string.Empty;
            if (arrayStr.Length > 1)
            {
                sPRODUCTDEFID = arrayStr[0];
                sPRODUCTDEFVERSION = arrayStr[1];

                values["P_PRODUCTDEFID"] = sPRODUCTDEFID;
                values["P_PRODUCTDEFVERSION"] = sPRODUCTDEFVERSION;
            }
            else
            {
                values["P_PRODUCTDEFID"] = string.Empty;
            }
            string sISCOMPLETION = values["P_ISCOMPLETION"].ToString();
            if (sISCOMPLETION.Length > 0 && sISCOMPLETION == "Y")
                values["P_ISCOMPLETION"] = "0";
            else if(sISCOMPLETION.Length > 0 && sISCOMPLETION == "N")
                values["P_ISCOMPLETION"] = "1";

            DataTable dt = null;

            dt = await SqlExecuter.QueryAsync("GetReliabilityVerificationResultNonRegularRgisterList", "10001", values);
           
            
            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData");
            }

            grdReliabiVerifiReqRgistRegular.DataSource = dt;
            grdQCReliabilityLot.View.ClearDatas();
        }

        #endregion

        #region 조회조건 설정

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            Conditions.AddTextBox("P_REQUESTDEPT").SetPosition(4.1).SetLabel("REQUESTDEPT");
            InitializeConditionPopup_User();
            InitializeConditionPopup_DefectCode();
            InitializeOSP_ProductDefIdPopup();
        }
        #endregion

        private void InitializeConditionPopup_User()
        {
            //var vendorPopup = Conditions.AddSelectPopup("p_chargerid", new SqlQuery("GetUserApproval", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "USERNAME", "CHARGERID")
            //                           .SetPopupLayout("ID", PopupButtonStyles.Ok_Cancel, true, false)
            //                           .SetPopupLayoutForm(400, 600)
            //                           .SetPopupResultCount(1)
            //                           .SetPosition(4.2)
            //                           .SetLabel("OSPREQUESTUSER")
            //                           .SetPopupAutoFillColumns("USERNAME");

            //// 팝업 조회조건
            //vendorPopup.Conditions.AddTextBox("P_CHARGERID")
            //           .SetLabel("ID");

            //// 팝업 그리드
            //vendorPopup.GridColumns.AddTextBoxColumn("CHARGERID", 150);
            //vendorPopup.GridColumns.AddTextBoxColumn("USERNAME", 200);

            var UserPopup = Conditions.AddSelectPopup("p_chargerid", new SqlQuery("GetUserApproval", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "USERNAME", "CHARGERID")
                                       .SetPopupLayout("ID", PopupButtonStyles.Ok_Cancel, true, false)
                                       .SetPopupLayoutForm(565, 600)
                                       .SetPopupResultCount(1)
                                       .SetPosition(4.2)
                                       .SetLabel("OSPREQUESTUSER")
                                       .SetPopupAutoFillColumns("USERNAME");

            // 팝업 조회조건
            //UserPopup.Conditions.AddTextBox("P_CHARGERID").SetLabel("ID");
            UserPopup.Conditions.AddTextBox("P_USERNAME").SetLabel("NAME");
            // 팝업 그리드
            UserPopup.GridColumns.AddTextBoxColumn("CHARGERID", 100).SetLabel("ID");
            UserPopup.GridColumns.AddTextBoxColumn("USERNAME", 100);
            UserPopup.GridColumns.AddTextBoxColumn("DEPARTMENT", 100);
            UserPopup.GridColumns.AddTextBoxColumn("POSITION", 60);
            UserPopup.GridColumns.AddTextBoxColumn("CELLPHONENUMBER", 100);
        }
        //Conditions.AddSelectPopup("DEFECTCODE"
        private void InitializeOSP_ProductDefIdPopup()
        {
            // SelectPopup 항목 추가
            var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEF", "PRODUCTDEF")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                .SetLabel("PRODUCTIONDEFINITION")
                .SetPosition(4.3)
                .SetPopupResultCount(0);
            //.SetPopupApplySelection((selectRow, gridRow) =>
            //{
            //    string productDefName = "";

            //    selectRow.AsEnumerable().ForEach(r =>
            //    {
            //        productDefName += Format.GetString(r["PRODUCTDEFNAME"]) + ",";
            //    });

            //    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = productDefName;
            //});

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            //conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
            //    .SetDefault("Product");

            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목버전
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            // 품목유형구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 생산구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 단위
            conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");


        }
        /// <summary>
        /// 불량코드 조회조건
        /// </summary>
        private void InitializeConditionPopup_DefectCode()
        {
            // 팝업 컬럼설정
            var defectCodePopup = Conditions.AddSelectPopup("p_defectCode", new SqlQuery("GetDefectCodeList", "10004", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DEFECTCODENAME", "DEFECTCODE")
               .SetPopupLayout("DEFECTCODE", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(700, 500)
               .SetLabel("DEFECTNAME")
               .SetPopupResultCount(1)
               .SetPosition(4.4)
               .SetRelationIds("p_plantId");

            // 팝업 조회조건
            defectCodePopup.Conditions.AddTextBox("DEFECTCODENAME");

            // 팝업 그리드

            defectCodePopup.GridColumns.AddTextBoxColumn("DEFECTCODE", 150).SetValidationKeyColumn(); ;
            defectCodePopup.GridColumns.AddTextBoxColumn("DEFECTCODENAME", 150);
            defectCodePopup.GridColumns.AddTextBoxColumn("QCSEGMENTID", 150);
            defectCodePopup.GridColumns.AddTextBoxColumn("QCSEGMENTNAME", 150);
        }
        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            //grdCodeClass.View.CheckValidation();

            //DataTable changed = grdCodeClass.GetChangedRows();

            //if (changed.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}
        }

        #endregion

        #region Private Function
        /// <summary>
        /// Report 파일의 컨트롤 중 Tag(FieldName) 값이 있는 컨트롤에 DataBinding(Text)를 추가한다.
        /// </summary>
        private void SetReportControlDataBinding(XRControlCollection controls, DataTable dataSource)
        {
            if (controls.Count > 0)
            {
                foreach (XRControl control in controls)
                {
                    if (!string.IsNullOrWhiteSpace(control.Tag.ToString()))
                        control.DataBindings.Add("Text", dataSource, control.Tag.ToString());

                    SetReportControlDataBinding(control.Controls, dataSource);
                }
            }
        }
        //popup이 닫힐때 그리드를 재조회 하는 이벤트 (팝업결과 저장 후 재조회)
        private async void Popup_FormClosed()
        {
            await OnSearchAsync();
        }

        /// <summary>
        /// 그리드의 포커스 행 변경시 행의 작업목록의 내용을 매핑처리 함.
        /// </summary>
        private void focusedRowChanged()
        {
            //포커스 행 체크 
            if (grdReliabiVerifiReqRgistRegular.View.FocusedRowHandle < 0) return;

            var row = grdReliabiVerifiReqRgistRegular.View.GetDataRow(grdReliabiVerifiReqRgistRegular.View.FocusedRowHandle);

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_ENTERPRISEID", row["ENTERPRISEID"].ToString());
            param.Add("P_PLANTID", row["PLANTID"].ToString());
            param.Add("P_REQUESTNO", row["REQUESTNO"].ToString());
            DataTable dtReliabilityLot = SqlExecuter.Query("GetRequestNonRegularQCReliabilityLot", "10001", param);               //LOT정보

            grdQCReliabilityLot.DataSource = dtReliabilityLot;

        }
        #endregion

        #region Global Function

        /// <summary>
        /// Popup 닫혔을때 재검색하기 위한 함수
        /// </summary>
        public void Search()
        {
            OnSearchAsync();
        }

        #endregion
    }
}
