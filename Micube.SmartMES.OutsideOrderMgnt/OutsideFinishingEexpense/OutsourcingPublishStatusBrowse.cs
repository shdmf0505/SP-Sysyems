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
    /// 프 로 그 램 명  : 외주관리 > 외주가공비마감 > 외주가공비발행내역
    /// 업  무  설  명  : 외주가공비발행내역(기타실적)에 대한 조회
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-10-07
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcingPublishStatusBrowse : SmartConditionManualBaseForm
    {
        #region Local Variables
        #endregion

        #region 생성자

        public OutsourcingPublishStatusBrowse()
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

            InitializeGrid();
            InitializeVendorInfoGrid();
        }

        #region InitializeGrid : 그리드를 초기화한다.
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdDetailInfo.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdDetailInfo.View.SetIsReadOnly(true);

            grdDetailInfo.View.AddTextBoxColumn("ENTERPRISEID", 120).SetIsHidden();
            grdDetailInfo.View.AddTextBoxColumn("PLANTID", 60).SetIsHidden();
            grdDetailInfo.View.AddTextBoxColumn("OSPETCTYPE", 60).SetIsHidden();
            grdDetailInfo.View.AddTextBoxColumn("OSPETCTYPENAME", 120);
            grdDetailInfo.View.AddTextBoxColumn("SETTLEDATE", 150).SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdDetailInfo.View.AddTextBoxColumn("PROCESSSEGMENTID", 60).SetIsHidden();
            grdDetailInfo.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120);
            grdDetailInfo.View.AddTextBoxColumn("AREAID", 100).SetIsHidden();
            grdDetailInfo.View.AddTextBoxColumn("AREANAME", 150);
            grdDetailInfo.View.AddTextBoxColumn("VENDORID", 100).SetIsHidden();
            grdDetailInfo.View.AddTextBoxColumn("OSPVENDORNAME", 150);
            grdDetailInfo.View.AddTextBoxColumn("PRODUCTDEFID", 180);
            grdDetailInfo.View.AddTextBoxColumn("PRODUCTDEFNAME", 300);
            grdDetailInfo.View.AddSpinEditColumn("SETTLEAMOUNT", 150);
            grdDetailInfo.View.AddTextBoxColumn("REMARK", 300);

            grdDetailInfo.View.PopulateColumns();
        }
        #endregion

        #region InitializeVendorInfoGrid : 그리드를 초기화한다.
        private void InitializeVendorInfoGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdVendorInfo.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdVendorInfo.View.SetIsReadOnly(true);

            grdVendorInfo.View.AddTextBoxColumn("ENTERPRISEID", 120).SetIsHidden();
            grdVendorInfo.View.AddTextBoxColumn("PLANTID", 60).SetIsHidden();
            grdVendorInfo.View.AddTextBoxColumn("AREAID", 60).SetIsHidden();
            grdVendorInfo.View.AddTextBoxColumn("AREANAME", 150);
            grdVendorInfo.View.AddTextBoxColumn("VENDORID", 60).SetIsHidden();
            grdVendorInfo.View.AddTextBoxColumn("VENDORNAME", 150);
            grdVendorInfo.View.AddTextBoxColumn("OSPETCTYPE", 60).SetIsHidden();
            grdVendorInfo.View.AddTextBoxColumn("OSPETCTYPENAME", 120);
            grdVendorInfo.View.AddSpinEditColumn("TODAYAMOUNT", 150);
            grdVendorInfo.View.AddSpinEditColumn("SETTLEAMOUNT", 150);

            grdVendorInfo.View.PopulateColumns();
        }
        #endregion
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
        }
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

        #region OnSearchAsync : 검색
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            if (values["P_SETTLEDATE_PERIODFR"].ToString().Equals("") && values["P_SETTLEDATE_PERIODFR"].ToString().Equals("")
               && values["P_YEARMONTH"].ToString().Equals(""))
            {
                ShowMessage("NoConditions_03"); // 검색조건중 (정산기간은 입력해야합니다.다국어
                return;
            }
            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("USERID", UserInfo.Current.Id.ToString());
            #endregion
            #region 기간 검색형 전환 처리 
            if (!(values["P_SETTLEDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_SETTLEDATE_PERIODFR"]);
                values.Remove("P_SETTLEDATE_PERIODFR");
                values.Add("P_SETTLEDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_SETTLEDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_SETTLEDATE_PERIODTO"]);
                values.Remove("P_SETTLEDATE_PERIODTO");

                values.Add("P_SETTLEDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }


            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable vendorTable = SqlExecuter.Query("GetOutsourcingPublishStatusBrowseMaster", "10001", values);

            grdVendorInfo.DataSource = vendorTable;

            DataTable detailTable = SqlExecuter.Query("GetOutsourcingPublishStatusBrowseDetail", "10001", values);

            grdDetailInfo.DataSource = detailTable;
        }
        #endregion

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //InitializePlant();
            InitializeConditionPopup_PeriodTypeOSP();
            InitializeCondition_Yearmonth();
            InitializeConditionProcesssegmentidPopup();
            InitializeOspEtcType();

            InitializeConditionPopup_OspVendorid();
            InitializeConditionProductCodePopup();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용 		Id	"P_SETTLEDATE"	string
            Conditions.GetControl<SmartPeriodEdit>("P_SETTLEDATE").LanguageKey = "EXPSETTLEDATE";
        }

        #region InitializePlant : Site설정
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializePlant()
        {

            var planttxtbox = Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
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
               .SetDefault("") //
               .SetEmptyItem("", "")//
            ;
        }
        /// <summary>
        /// 마감년월
        /// </summary>
        private void InitializeCondition_Yearmonth()
        {
            DateTime dateNow = DateTime.Now;
            string strym = dateNow.ToString("yyyy-MM");
            var YearmonthDT = Conditions.AddDateEdit("p_yearmonth")
               .SetLabel("CLOSEYM")
               .SetDisplayFormat("yyyy-MM")
               .SetPosition(2.1)
            // .SetDefault(strym)
            //.SetValidationIsRequired()
            ;

        }
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
            .SetPosition(3.3);

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
               .SetPosition(3.4)
               .SetEmptyItem("", "", true)
            ;
        }
        #endregion

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
               .SetPosition(4.1);
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
               .SetRelationIds("p_plantid")
               .SetPosition(5.2);

            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("OSPVENDORNAME")
                .SetLabel("OSPVENDORNAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);

            var txtOspVendor = Conditions.AddTextBox("P_OSPVENDORNAME")
               .SetLabel("OSPVENDORNAME")
               .SetPosition(5.4);

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
        //    .SetLabel("OSPVENDORID")
        //    .SetPopupResultCount(1)
        //    .SetPosition(1.1)
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
            .SetPosition(6.2)
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
