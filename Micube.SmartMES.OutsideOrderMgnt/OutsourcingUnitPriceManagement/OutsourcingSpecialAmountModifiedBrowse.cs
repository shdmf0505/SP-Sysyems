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
    /// 프 로 그 램 명  : 외주관리 > 외주가공비마감 > Special 단가변경이력
    /// 업  무  설  명  : Special 단가의 변경이력을 조회한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-10-10
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcingSpecialAmountModifiedBrowse : SmartConditionManualBaseForm
    {
        #region Local Variables
        #endregion

        #region 생성자

        public OutsourcingSpecialAmountModifiedBrowse()
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
        }

        #region InitializeGrid : 그리드를 초기화한다.
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSpecialAmount.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdSpecialAmount.View.SetIsReadOnly(true);

            grdSpecialAmount.View.AddTextBoxColumn("PROCESSSEGMENTID", 10).SetIsHidden();
            grdSpecialAmount.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 180);
            grdSpecialAmount.View.AddTextBoxColumn("AREAID", 80);
            grdSpecialAmount.View.AddTextBoxColumn("AREANAME", 120);                        
            grdSpecialAmount.View.AddTextBoxColumn("PRODUCTDEFID", 150);
            grdSpecialAmount.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60);
            grdSpecialAmount.View.AddTextBoxColumn("PRODUCTDEFNAME", 250);                    
            grdSpecialAmount.View.AddTextBoxColumn("STARTDATE", 150).SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdSpecialAmount.View.AddTextBoxColumn("ENDDATE", 150).SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdSpecialAmount.View.AddSpinEditColumn("REDUCERATE", 100);                    
            grdSpecialAmount.View.AddTextBoxColumn("DESCRIPTION", 200);                   
            grdSpecialAmount.View.AddTextBoxColumn("CREATORID", 10).SetIsHidden();
            grdSpecialAmount.View.AddTextBoxColumn("CREATOR", 80);
            grdSpecialAmount.View.AddTextBoxColumn("CREATEDTIME", 150).SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdSpecialAmount.View.AddTextBoxColumn("MODIFIERID", 10).SetIsHidden();
            grdSpecialAmount.View.AddTextBoxColumn("MODIFIER", 80);
            grdSpecialAmount.View.AddTextBoxColumn("MODIFIEDTIME", 150).SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdSpecialAmount.View.AddTextBoxColumn("VALIDSTATE", 80);
            grdSpecialAmount.View.AddTextBoxColumn("TXNUSERID", 10).SetIsHidden();
            grdSpecialAmount.View.AddTextBoxColumn("TXNUSER", 80);
            grdSpecialAmount.View.AddTextBoxColumn("TXNTIME", 150).SetDisplayFormat("yyyy-MM-dd HH:mm:ss");

            grdSpecialAmount.View.PopulateColumns();
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

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            #endregion
            if (!(values["P_APPLYDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_APPLYDATE_PERIODFR"]);
                values.Remove("P_APPLYDATE_PERIODFR");
                values.Add("P_APPLYDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_APPLYDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_APPLYDATE_PERIODTO"]);
                values.Remove("P_APPLYDATE_PERIODTO");
                values.Add("P_APPLYDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable specialAmountTable = SqlExecuter.Query("GetOutsourcingSpecialAmountHistoryBrowseByOsp", "10001", values);

            grdSpecialAmount.DataSource = specialAmountTable;

            if (specialAmountTable.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
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

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //InitializePlant();
            InitializeConditionProcesssegmentidPopup();
            InitializeConditionPopup_OspAreaid();
            //InitializeVendors();
            InitializeConditionProductPopup();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
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

        #region InitializeConditionProcesssegmentidPopup : 공정검색
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
            .SetPosition(1.1);

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
        /// <summary>
        /// 작업장
        /// </summary>
        private void InitializeConditionPopup_OspAreaid()
        {
            // 팝업 컬럼설정
            var popupProduct = Conditions.AddSelectPopup("p_areaid",
                                                                new SqlQuery("GetAreaidListByOsp", "10001"
                                                                                , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                                , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                ), "AREANAME", "AREAID")
             .SetPopupLayout("AREAID", PopupButtonStyles.Ok_Cancel, true, false)
             .SetPopupLayoutForm(450, 600)
             .SetLabel("AREANAME")
             .SetPopupResultCount(1)
             .SetRelationIds("p_plantid")
              .SetPosition(1.8);
            // 팝업 조회조건
            popupProduct.Conditions.AddTextBox("AREANAME")
                .SetLabel("AREANAME");

            // 팝업 그리드
            popupProduct.GridColumns.AddTextBoxColumn("AREAID", 120);
            popupProduct.GridColumns.AddTextBoxColumn("AREANAME", 200);


        }
        #region InitializeVendors : 협력사 제어
        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeVendors()
        {
            ConditionItemSelectPopup toolCodePopup = Conditions.AddSelectPopup("VENDORID", new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE ={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
            .SetPopupLayout("VENDOR", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("VENDORID", "VENDORID")
            .SetLabel("VENDOR")
            .SetPopupResultCount(1)
            .SetPosition(1.2)
            ;

            toolCodePopup.ValueFieldName = "VENDORID";
            toolCodePopup.DisplayFieldName = "VENDORNAME";
            // 팝업에서 사용할 조회조건 항목 추가
            toolCodePopup.Conditions.AddTextBox("VENDORNAME");

            // 팝업 그리드 설정
            toolCodePopup.GridColumns.AddTextBoxColumn("VENDORID", 300)
                .SetIsHidden();
            toolCodePopup.GridColumns.AddTextBoxColumn("VENDORNAME", 300)
                .SetIsReadOnly();
        }
        #endregion

        #region InitializeConditionProductPopup : 품목검색
        private void InitializeConditionProductPopup()
        {
            ConditionItemSelectPopup toolCodePopup = Conditions.AddSelectPopup("PRODUCTDEFID", new SqlQuery("GetProductdefidPoplistByOsp", "10001", $"LANGUAGETYPE ={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
            .SetPopupLayout("PRODUCTDEFIDPOPUP", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("PRODUCTDEFID", "PRODUCTDEFID")
            .SetLabel("PRODUCTDEFID")
            .SetPopupResultCount(1)
            .SetPosition(1.3)
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
