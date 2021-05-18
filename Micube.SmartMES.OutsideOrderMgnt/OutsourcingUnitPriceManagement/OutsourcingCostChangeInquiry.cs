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
    /// 프 로 그 램 명  :  외주관리> 외주비현황 > 외주가공단가 변동내역 조회
    /// 업  무  설  명  :  외주가공단가 변동내역 조회
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-09-17
    /// 수  정  이  력  : 
    /// 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcingCostChangeInquiry : SmartConditionManualBaseForm
    {
        #region Local Variables

       

        #endregion

        #region 생성자

        public OutsourcingCostChangeInquiry()
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

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSearch.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdSearch.View.SetIsReadOnly();

            grdSearch.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();                                                          //  회사 ID
            grdSearch.View.AddTextBoxColumn("ENTERPRISEID", 200)
                 .SetIsHidden();                                                           //  공장 ID
           
            grdSearch.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 100);
            grdSearch.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 250);
            grdSearch.View.AddTextBoxColumn("PROCESSSEGMENTID", 100);
            grdSearch.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdSearch.View.AddTextBoxColumn("OSPPRICECODE", 100);
            grdSearch.View.AddTextBoxColumn("OSPPRICENAME", 150);
            grdSearch.View.AddComboBoxColumn("OWNTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OwnType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("*", "*");  // 
            grdSearch.View.AddComboBoxColumn("PROCESSPRICETYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProcessPriceType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("*", "*");  // 

            grdSearch.View.AddTextBoxColumn("AREAID", 80);
            grdSearch.View.AddTextBoxColumn("AREANAME", 100);
            grdSearch.View.AddTextBoxColumn("PRODUCTDEFID", 150);
            grdSearch.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            grdSearch.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            grdSearch.View.AddSpinEditColumn("PREVPRICE", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            grdSearch.View.AddSpinEditColumn("OSPPRICE", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            grdSearch.View.AddDateEditColumn("STARTDATE", 100)
                 .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime);                              //  
            grdSearch.View.AddDateEditColumn("ENDDATE", 100)
                 .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime);                              //  
            grdSearch.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetDefault("Valid");                             //

            grdSearch.View.AddTextBoxColumn("MODIFIERNAME", 100)
                 .SetIsReadOnly();
            grdSearch.View.AddTextBoxColumn("MODIFIEDTIME", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetDefault("")
                 .SetIsReadOnly();
            grdSearch.View.AddTextBoxColumn("DESCRIPTION", 200);
           // grdSearch.View.SetAutoFillColumn("DESCRIPTION");
            grdSearch.View.PopulateColumns();
        }

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

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            
            await base.OnSearchAsync();
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 
            if (!(values["P_SEARCHDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_SEARCHDATE_PERIODFR"]);
                values.Remove("P_SEARCHDATE_PERIODFR");
                values.Add("P_SEARCHDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_SEARCHDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_SEARCHDATE_PERIODTO"]);
                //requestDateTo = requestDateTo.AddDays(1);
                values.Remove("P_SEARCHDATE_PERIODTO");
                values.Add("P_SEARCHDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }


            #endregion

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtSearch = await SqlExecuter.QueryAsync("GetOutsourcingCostChangeInquiry", "10001", values);
            if (dtSearch.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.

            }
            grdSearch.DataSource = dtSearch;

        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // site
            //InitializeConditionPopup_Plant();
            // 중공정 
            InitializeConditionPopup_Processsegmentclassid();

            InitializeConditionPopup_OspAreaid();
            // 작업장 ??
            //4.양산구분
            InitializeConditionPopup_ProcessPriceType();
            //외주단가코드 ,외주단가명 
            InitializeConditionPopup_Ospprice();
            //6.품목
            InitializeConditionPopup_ProductDefId();
        }
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {
            var plantCbobox = Conditions.AddComboBox("p_plantid", new SqlQuery("GetPlantList", "00001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
              .SetLabel("PLANT")
              .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
              .SetPosition(0.1)
              .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
              .SetIsReadOnly(true);
        }

        /// <summary>
        /// 중공정 설정 
        /// </summary>
        private void InitializeConditionPopup_Processsegmentclassid()
        {

            // 팝업 컬럼설정
            var processsegmentclassidPopupColumn = Conditions.AddSelectPopup("p_processsegmentclassid", new SqlQuery("GetProcesssegmentclassidListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "MIDDLEPROCESSSEGMENTCLASSNAME", "MIDDLEPROCESSSEGMENTCLASSID")
               .SetPopupLayout("MIDDLEPROCESSSEGMENTCLASS", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("MIDDLEPROCESSSEGMENTCLASS")
               .SetPopupResultCount(1)
               .SetPosition(1.2);

            // 팝업 조회조건
            processsegmentclassidPopupColumn.Conditions.AddTextBox("MIDDLEPROCESSSEGMENTCLASSNAME")
                .SetLabel("MIDDLEPROCESSSEGMENTCLASSNAME");

            // 팝업 그리드
            processsegmentclassidPopupColumn.GridColumns.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSID", 150);
            processsegmentclassidPopupColumn.GridColumns.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSNAME", 200);

        }
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
        /// <summary>
        ///양산구분
        /// </summary>
        private void InitializeConditionPopup_ProcessPriceType()
        {

            var owntypecbobox = Conditions.AddComboBox("p_ProcessPriceType", new SqlQuery("GetCodeAllListByOspPrice", "10001", "CODECLASSID=ProcessPriceType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetLabel("PROCESSPRICETYPE")
               .SetPosition(2.2)
               .SetEmptyItem("","")
            ;
          
        }
        /// <summary>
        ///  외주단가코드
        /// </summary>
        private void InitializeConditionPopup_Ospprice()
        {
            var txtOsppricecode = Conditions.AddSelectPopup("p_osppricecode", new SqlQuery("GetOsppricecodeListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "OSPPRICENAME", "OSPPRICECODE")
               .SetLabel("OSPPRICECODE")
               .SetPopupLayoutForm(400, 600)
               .SetPopupResultCount(1)
               .SetRelationIds("p_plantid" ,"P_PROCESSSEGMENTCLASSID")
               .SetPosition(2.6);
            // 팝업 조회조건
            txtOsppricecode.Conditions.AddTextBox("OSPPRICENAME")
                .SetLabel("OSPPRICENAME");
            txtOsppricecode.GridColumns.AddTextBoxColumn("OSPPRICECODE", 100)
              .SetValidationKeyColumn();
            txtOsppricecode.GridColumns.AddTextBoxColumn("OSPPRICENAME", 150);

        }
       

        /// <summary>
        /// 품목코드 조회조건
        /// </summary>
        private void InitializeConditionPopup_ProductDefId()
        {
            var txtOsppricecode = Conditions.AddTextBox("P_PRODUCTDEFID")
               .SetLabel("PRODUCTDEFID")
               .SetPosition(3.3);
            var txtOsppricename = Conditions.AddTextBox("P_PRODUCTDEFNAME")
               .SetLabel("PRODUCTDEFNAME")
               .SetPosition(3.4);
           
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
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

        #endregion
    }
}
