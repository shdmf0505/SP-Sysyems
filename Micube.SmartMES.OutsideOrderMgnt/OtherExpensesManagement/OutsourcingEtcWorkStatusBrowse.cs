#region using

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

#endregion

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  : 외주관리 > 외주가공비마감 > 기타작업현황관리
    /// 업  무  설  명  : 기타작업현황관리를 조회한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-10-07
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcingEtcWorkStatusBrowse : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자
        public OutsourcingEtcWorkStatusBrowse()
        {
            InitializeComponent();
        }
        #endregion

        #region 컨텐츠 영역 초기화
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            InitializeGrid();
            InitializationSummaryRow();
        }

        #region InitializeGrid : 그리드를 초기화한다.
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdWorkStatus.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;         
            grdWorkStatus.View.SetIsReadOnly(true);

            grdWorkStatus.View.AddTextBoxColumn("OSPETCPROGRESSSTATUSNAME", 120);                       
            grdWorkStatus.View.AddTextBoxColumn("ISREQUEST", 60);                          
            grdWorkStatus.View.AddTextBoxColumn("ISACTUAL", 60);                         
            grdWorkStatus.View.AddTextBoxColumn("ISAPPROVAL", 60);                        
            grdWorkStatus.View.AddTextBoxColumn("ISSETTLE", 60);                        
            grdWorkStatus.View.AddTextBoxColumn("REQUESTNO", 120);                        
            grdWorkStatus.View.AddTextBoxColumn("OSPETCTYPE", 100).SetIsHidden();                         
            grdWorkStatus.View.AddTextBoxColumn("OSPETCTYPENAME", 100);                         
            grdWorkStatus.View.AddTextBoxColumn("CUSTOMERID", 150).SetIsHidden();                        
            grdWorkStatus.View.AddTextBoxColumn("CUSTOMERNAME", 150);                          
            grdWorkStatus.View.AddTextBoxColumn("REQUESTDEPARTMENT", 120);                         
            grdWorkStatus.View.AddTextBoxColumn("REQUESTUSER", 100).SetIsHidden();                       
            grdWorkStatus.View.AddTextBoxColumn("REQUESTUSERNAME", 100);                        
            grdWorkStatus.View.AddTextBoxColumn("REQUESTDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");                                     
            grdWorkStatus.View.AddTextBoxColumn("LOTPRODUCTTYPE", 100);              
            grdWorkStatus.View.AddTextBoxColumn("PRODUCTDEFID", 120);             
            grdWorkStatus.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60);                     
            grdWorkStatus.View.AddTextBoxColumn("PRODUCTDEFNAME", 250);                            

            grdWorkStatus.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();                          
            grdWorkStatus.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);                         
            grdWorkStatus.View.AddTextBoxColumn("ISCHARGE", 60);
            grdWorkStatus.View.AddTextBoxColumn("AREANAME", 120);
            grdWorkStatus.View.AddTextBoxColumn("OSPVENDORNAME", 120);                       
            grdWorkStatus.View.AddTextBoxColumn("UNIT", 80);                         
            grdWorkStatus.View.AddSpinEditColumn("REQUESTQTY", 80);                           
            grdWorkStatus.View.AddSpinEditColumn("REQUESTPRICE", 80);                                  
            grdWorkStatus.View.AddSpinEditColumn("REQUESTAMOUNT", 80);                                
            grdWorkStatus.View.AddTextBoxColumn("ISREQUESTDEFECTACCEPT", 60);                              
            grdWorkStatus.View.AddTextBoxColumn("REQUESTDESCRIPTION", 200);                        
            grdWorkStatus.View.AddSpinEditColumn("ACTUALQTY", 80);                              
            grdWorkStatus.View.AddSpinEditColumn("ACTUALDEFECTQTY", 80);                             
            grdWorkStatus.View.AddSpinEditColumn("ACTUALPRICE", 80);                        
            grdWorkStatus.View.AddSpinEditColumn("ACTUALAMOUNT", 80);                            
            grdWorkStatus.View.AddTextBoxColumn("ISACTUALDEFECTACCEPT", 60);                             
            grdWorkStatus.View.AddTextBoxColumn("ACTUALUSERNAME", 100);                             
            grdWorkStatus.View.AddTextBoxColumn("ACTUALDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");                            
            grdWorkStatus.View.AddTextBoxColumn("ACTUALDESCRIPTION", 200);                         
            grdWorkStatus.View.AddTextBoxColumn("APPROVALUSERNAME", 100);                           
            grdWorkStatus.View.AddTextBoxColumn("APPROVALDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");                           
            grdWorkStatus.View.AddTextBoxColumn("SETTLEUSERNAME", 100);                                   
            grdWorkStatus.View.AddTextBoxColumn("SETTLEDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");                        
            grdWorkStatus.View.AddSpinEditColumn("SETTLEQTY", 80);                             
            grdWorkStatus.View.AddSpinEditColumn("SETTLEDEFECTQTY", 80);                                 
            grdWorkStatus.View.AddSpinEditColumn("SETTLEPRICE", 80);                         
            grdWorkStatus.View.AddTextBoxColumn("ISSETTLEDEFECTACCEPT", 60);                         
            grdWorkStatus.View.AddSpinEditColumn("SETTLEAMOUNT", 80);                               
            grdWorkStatus.View.AddTextBoxColumn("SETTLEDESCRIPTION", 200);                               

            grdWorkStatus.View.PopulateColumns();
        }

        private void InitializationSummaryRow()
        {
            grdWorkStatus.View.Columns["UNIT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkStatus.View.Columns["UNIT"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdWorkStatus.View.Columns["REQUESTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkStatus.View.Columns["REQUESTQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdWorkStatus.View.Columns["REQUESTAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkStatus.View.Columns["REQUESTAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWorkStatus.View.Columns["ACTUALQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkStatus.View.Columns["ACTUALQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdWorkStatus.View.Columns["ACTUALDEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkStatus.View.Columns["ACTUALDEFECTQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdWorkStatus.View.Columns["ACTUALAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkStatus.View.Columns["ACTUALAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdWorkStatus.View.Columns["SETTLEDEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkStatus.View.Columns["SETTLEDEFECTQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdWorkStatus.View.Columns["SETTLEAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkStatus.View.Columns["SETTLEAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdWorkStatus.View.OptionsView.ShowFooter = true;
            grdWorkStatus.ShowStatusBar = false;
        }
        #endregion
        #endregion

        #region Event
        public void InitializeEvent()
        {
        }
        #endregion

        #region 툴바
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
        }
        #endregion

        #region 검색
        #region OnSearchAsync : 검색
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            #region 
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("USERID", UserInfo.Current.Id.ToString());
            #endregion
            #region 기간 검색형 전환 처리 
            if (!(values["P_REQUESTDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_REQUESTDATE_PERIODFR"]);
                values.Remove("P_REQUESTDATE_PERIODFR");
                values.Add("P_REQUESTDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_REQUESTDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_REQUESTDATE_PERIODTO"]);
                values.Remove("P_REQUESTDATE_PERIODTO");

                values.Add("P_REQUESTDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }


            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable workStatusTable = SqlExecuter.Query("GetOutsourcingEtcWorkStatusBrowseByOsp", "10001", values);

            grdWorkStatus.DataSource = workStatusTable;

            if (workStatusTable.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
        }
        #endregion

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //InitializePlant();
            InitializeConditionPopup_PeriodTypeOSP();

            InitializeConditionPopup_OspAreaid();
            InitializeConditionPopup_OspVendorid();
            InitializeOspEtcType();
            InitializeConditionProductCodePopup();
            InitializeConditionProcesssegmentidPopup();

            InitializeOspEtcProgressStatus();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        }

        #region InitializePlant : Site설정
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializePlant()
        {

            var planttxtbox = Conditions.AddComboBox("P_PLANTID", new SqlQuery("GetPlantList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.1)
               .SetIsReadOnly(true)
               .SetValidationIsRequired()
               .SetDefault(UserInfo.Current.Plant, "PLANTID") //기본값 설정 UserInfo.Current.Plant
            ;
        }
        #endregion
        /// <summary>
        ///외주실적
        /// </summary>
        private void InitializeConditionPopup_PeriodTypeOSP()
        {

            var owntypecbobox = Conditions.AddComboBox("p_PeriodType", new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodTypeOSP", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetLabel("PERIODTYPEOSP")
               .SetPosition(0.2)
               .SetDefault("")
               .SetEmptyItem("", "")//
            ;
        }
        #region InitializeVendors : 협력사 제어

        /// <summary>
        /// 작업장
        /// </summary>
        private void InitializeConditionPopup_OspAreaid()
        {
            // 팝업 컬럼설정
            var vendoridPopupColumn = Conditions.AddSelectPopup("p_areaid", new SqlQuery("GetAreaidListAuthorityByOsp", "10001"
                                                         , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                         , $"USERID={UserInfo.Current.Id}"
                                                         , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(520, 600)
               .SetLabel("AREANAME")
               .SetRelationIds("p_plantId")
               .SetPopupResultCount(1)
               .SetPosition(0.3);
            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("P_AREANAME")
                .SetLabel("AREANAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("AREAID", 120);
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("AREANAME", 200);


        }
        /// <summary>
        /// 작업업체 .고객 조회조건
        /// </summary>
        private void InitializeConditionPopup_OspVendorid()
        {
            // 팝업 컬럼설정
            var vendoridPopupColumn = Conditions.AddSelectPopup("p_ospvendorid", new SqlQuery("GetVendorListAuthorityByOsp", "10001"
               , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
               , $"USERID={UserInfo.Current.Id}"), "OSPVENDORNAME", "OSPVENDORID")
               .SetPopupLayout("OSPVENDORID", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("OSPVENDORID")
               .SetPopupResultCount(1)
               .SetRelationIds("p_plantid", "p_areaid")
               .SetPosition(0.4);

            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("OSPVENDORNAME")
                .SetLabel("OSPVENDORNAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);

          

        }
        ///// <summary>
        ///// 팝업창 제어
        ///// </summary>
        //private void InitializeVendors()
        //{
        //    ConditionItemSelectPopup toolCodePopup = Conditions.AddSelectPopup("VENDORID", new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE ={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
        //    .SetPopupLayout("VENDOR", PopupButtonStyles.Ok_Cancel, true, false)
        //    .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
        //    .SetPopupResultMapping("VENDORID", "VENDORID")
        //    .SetLabel("VENDOR")
        //    .SetPopupResultCount(1)
        //    .SetPosition(0.3)
        //    ;

        //    toolCodePopup.ValueFieldName = "VENDORID";
        //    toolCodePopup.DisplayFieldName = "VENDORNAME";
        //    // 팝업에서 사용할 조회조건 항목 추가
        //    toolCodePopup.Conditions.AddTextBox("VENDORNAME");

        //    // 팝업 그리드 설정
        //    toolCodePopup.GridColumns.AddTextBoxColumn("VENDORID", 300)
        //        .SetIsHidden();
        //    toolCodePopup.GridColumns.AddTextBoxColumn("VENDORNAME", 300)
        //        .SetIsReadOnly();
        //}
        #endregion

        #region 품목검색
        /// <summary>
        /// 품목코드 조회 설정
        /// </summary>
        private void InitializeConditionProductCodePopup()
        {
            ConditionItemSelectPopup toolCodePopup = Conditions.AddSelectPopup("PRODUCTDEFID", new SqlQuery("GetProductdefidPoplistByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
            .SetPopupLayout("PRODUCTDEFIDPOPUP", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("PRODUCTDEFID", "PRODUCTDEFID")
            .SetLabel("PRODUCTDEFID")
            .SetPopupResultCount(1)
            .SetPosition(0.5)
            ;

            toolCodePopup.ValueFieldName = "PRODUCTDEFID";
            toolCodePopup.DisplayFieldName = "PRODUCTDEFNAME";

            // 팝업 조회조건
            toolCodePopup.Conditions.AddComboBox("PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "CODENAME", "CODEID")
                .SetEmptyItem("", "", true)
                .SetLabel("PRODUCTDEFTYPE")
                .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
                ;
            toolCodePopup.Conditions.AddTextBox("PRODUCTDEFID")
                .SetLabel("PRODUCTDEFID");
            toolCodePopup.Conditions.AddTextBox("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFNAME");

            // 팝업 그리드
            toolCodePopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 250)
                .SetIsReadOnly();
            toolCodePopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();
            toolCodePopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 350)
                .SetIsReadOnly();
            toolCodePopup.GridColumns.AddTextBoxColumn("PRODUCTDEFCODE", 50)
                .SetIsHidden();
            toolCodePopup.GridColumns.AddTextBoxColumn("PRODUCTDEFTYPE", 200)
                .SetIsReadOnly();
        }
        #endregion

        #region 공정검색
        /// <summary>
        /// 공정 선택팝업
        /// </summary>
        private void InitializeConditionProcesssegmentidPopup()
        {


            var popupProcesssegmentid = Conditions.AddSelectPopup("PROCESSSEGMENTID",
                                                               new SqlQuery("GetProcessSegmentListByOsp", "10001"
                                                                               , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                               , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                               ), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
            .SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(500, 600)
            .SetLabel("PROCESSSEGMENTNAME")
            .SetPopupResultCount(1)
            .SetPosition(0.6);

            popupProcesssegmentid.ValueFieldName = "PROCESSSEGMENTID";
            popupProcesssegmentid.DisplayFieldName = "PROCESSSEGMENTNAME";

            // 팝업 조회조건
            popupProcesssegmentid.Conditions.AddTextBox("PROCESSSEGMENTNAME")
                 .SetLabel("PROCESSSEGMENT"); 


            popupProcesssegmentid.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetValidationKeyColumn();
            popupProcesssegmentid.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 250);
        }
        #endregion

        #region InitializeOspEtcType : 외주비구분검색
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeOspEtcType()
        {
            var ospEtcTypeBox = Conditions.AddComboBox("OSPETCTYPE", new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=OSPEtcType"), "CODENAME", "CODEID")
               .SetLabel("OSPETCTYPE")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.7)
               .SetEmptyItem("", "", true)
            ;
        }
        #endregion

        #region InitializeOspEtcType : 작업진행상태
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeOspEtcProgressStatus()
        {
            var ospEtcTypeBox = Conditions.AddComboBox("OSPETCPROGRESSSTATUS", new SqlQuery("GetCodeAllListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=OSPEtcProgressStatus"), "CODENAME", "CODEID")
               .SetLabel("OSPETCPROGRESSSTATUS")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(1.1)
               //.SetEmptyItem("", "", true)
            ;
        }
        #endregion
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

        #endregion
    }
}
