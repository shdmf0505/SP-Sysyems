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
using Micube.SmartMES.OutsideOrderMgnt.Popup;
#endregion

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리> 외주창고 > 외주처배분 
    /// 업  무  설  명  :외주창고입출고 현황한다.
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-08-08
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcedDistribution : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        

        #endregion

        #region 생성자

        public OutsourcedDistribution()
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

            grdOutsourcedDistribution.GridButtonItem =  GridButtonItem.Export;
            grdOutsourcedDistribution.View.SetIsReadOnly();
            grdOutsourcedDistribution.View.AddTextBoxColumn("ENTERPRISEID", 120)
                .SetIsHidden();                                                               //  회사 ID
            grdOutsourcedDistribution.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();                                                               //  공장 ID
            grdOutsourcedDistribution.View.AddTextBoxColumn("OSPRECEIPTUSER", 120)
                .SetIsHidden();
            grdOutsourcedDistribution.View.AddTextBoxColumn("OSPSENDER", 120)
                .SetIsHidden();
            grdOutsourcedDistribution.View.AddTextBoxColumn("LOTHISTKEY", 120)
                .SetIsHidden();                                                               //  LOTHISTKEY
            grdOutsourcedDistribution.View.AddTextBoxColumn("RECEIPTSEQUENCE", 120)
                .SetIsHidden();
            grdOutsourcedDistribution.View.AddTextBoxColumn("LOTID", 200)
                .SetIsReadOnly();                                                             //  LOTID
            grdOutsourcedDistribution.View.AddTextBoxColumn("RECEIPTDATE", 80)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetIsReadOnly();                                                              // 입고일 
            grdOutsourcedDistribution.View.AddTextBoxColumn("RECEIPTUSERNAME", 120)
                .SetIsReadOnly();                                                             // 입고자

            grdOutsourcedDistribution.View.AddComboBoxColumn("LOTTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=LotType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();                                                             //  양산구분               
            grdOutsourcedDistribution.View.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly();                                                             //  제품 정의 ID
            grdOutsourcedDistribution.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();                                                             //  제품 정의 Version
            grdOutsourcedDistribution.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                .SetIsReadOnly();                                                             //  제품명
            grdOutsourcedDistribution.View.AddTextBoxColumn("PROCESSSEGMENTID", 120)
                .SetIsHidden();                                                             //  공정 ID
            grdOutsourcedDistribution.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetIsReadOnly();                                                             //  공정명
            grdOutsourcedDistribution.View.AddTextBoxColumn("AREAID", 120)
                .SetIsHidden();                                                             //  작업장 AREAID
            grdOutsourcedDistribution.View.AddTextBoxColumn("AREANAME", 120)
                .SetIsReadOnly();                                                               //  작업장 AREAID
            grdOutsourcedDistribution.View.AddTextBoxColumn("USERSEQUENCENAME", 120)
                .SetIsReadOnly();
            grdOutsourcedDistribution.View.AddTextBoxColumn("ALTERNATESEQUENCENAME", 120)
                .SetIsReadOnly();                                                               // 공순// 공순
            grdOutsourcedDistribution.View.AddTextBoxColumn("PREVPROCESSSEGMENTID", 120)
                .SetIsHidden();                                                              //  이전공정 ID
            grdOutsourcedDistribution.View.AddTextBoxColumn("PREVPROCESSSEGMENTNAME", 150)
                .SetIsReadOnly();                                                             //  이전공정명
            grdOutsourcedDistribution.View.AddTextBoxColumn("PREVAREAID", 120)
                .SetIsHidden();                                                             //  이전 작업장 PREVAREAID
            grdOutsourcedDistribution.View.AddTextBoxColumn("PREVAREANAME", 120)
                .SetIsReadOnly();                                                               //  이전 작업장 PREVAREAID

            grdOutsourcedDistribution.View.AddTextBoxColumn("PCSQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty
            
            grdOutsourcedDistribution.View.AddTextBoxColumn("PANELQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  panelqty
            grdOutsourcedDistribution.View.AddTextBoxColumn("OSPMM", 120)
                .SetIsHidden()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);                                //  panelqty
            grdOutsourcedDistribution.View.AddTextBoxColumn("CHECKDATE", 80)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetIsReadOnly();                                                                  // 확인일
            grdOutsourcedDistribution.View.AddTextBoxColumn("CHECKUSERNAME", 120)
                .SetIsReadOnly();                                                             // 확인자
            grdOutsourcedDistribution.View.AddTextBoxColumn("PATHSEQUENCESTART", 120)
                .SetIsHidden();                                                              //  이전공정 ID
            grdOutsourcedDistribution.View.AddTextBoxColumn("PATHSEQUENCEEND", 120)
                .SetIsHidden();                                                              //  이전공정 ID
            grdOutsourcedDistribution.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            btnDistVen.Click += BtnDistVen_Click;
        }
        /// <summary>
        /// 외주처 변경 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDistVen_Click(object sender, EventArgs e)
        {
            //외주창고입고 목록 체크 부분 확인  
            if (grdOutsourcedDistribution.View.DataRowCount == 0)
            {
                //다국어메세지 외주창고 입고 목록이 없습니다. 
                ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                return;
            }
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("P_PROCESSSEGMENTID", grdOutsourcedDistribution.View.GetFocusedRowCellValue("PROCESSSEGMENTID"));
            OutsourcedDistributionPopup itemPopup = new OutsourcedDistributionPopup(values);
            itemPopup.ShowDialog(this);
            //재조회 해야함.
            OnSaveConfrimSearch();
        }
        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
           // base.OnToolbarSaveClick();

        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Ospareaidchange"))
            {

                BtnDistVen_Click(null, null);
            }
            

        }
        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            #region 품목코드 전환 처리 
            if (!(values["P_PRODUCTCODE"] == null))
             {
                string sproductcode = values["P_PRODUCTCODE"].ToString();
                // 품목코드값이 있으면
                if (!(sproductcode.Equals("")))
                {
                    string[] sproductd = sproductcode.Split('|');
                    // plant 정보 다시 가져오기 
                    values.Add("P_PRODUCTDEFID", sproductd[0].ToString());
                    values.Add("P_PRODUCTDEFVERSION", sproductd[1].ToString());
                }
            }
            #endregion

            #region 기간 검색형 전환 처리 

            if (!(values["P_RECEIPTDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_RECEIPTDATE_PERIODFR"]);
                values.Remove("P_RECEIPTDATE_PERIODFR");
                values.Add("P_RECEIPTDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_RECEIPTDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_RECEIPTDATE_PERIODTO"]);
                values.Remove("P_RECEIPTDATE_PERIODTO");
                //requestDateTo = requestDateTo.AddDays(1);
                values.Add("P_RECEIPTDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }

            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtInquiry = await SqlExecuter.QueryAsync("GetOutsourcedDistribution", "10001", values);

            if (dtInquiry.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdOutsourcedDistribution.DataSource = dtInquiry;


        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // SITE
            //InitializeConditionPopup_Plant();
            //입고일자1
            // 입고자 
            InitializeConditionPopup_Requestor();
            
            // 품목코드 
            InitializeConditionPopup_ProductDefId();
            // 작업장
            InitializeConditionPopup_Areaid();
            // 양산구분
            InitializeCondition_LotType();
            // LOTNO 
            InitializeCondition_Lotid();
            // 확인 여부 ( y,N)
            InitializeCondition_YesNo();
        }


        #region Setting InitializeCondition 
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {

            var planttxtbox = Conditions.AddComboBox("p_plantid", new SqlQuery("GetPlantList", "00001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
               .SetPosition(0.1)
               .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
            ;
           

        }

        /// <summary>
        /// 입고자 조회조건
        /// </summary>
        private void InitializeConditionPopup_Requestor()
        {
            // 팝업 컬럼설정
            var requesterPopupColumn = Conditions.AddSelectPopup("p_requestuser", new SqlQuery("GetRequestorListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "REQUESTUSERNAME", "REQUESTUSER")
               .SetPopupLayout("RECEIPTUSER", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("RECEIPTUSER")
               .SetPopupResultCount(1)
               .SetRelationIds("p_plantid")
               .SetPosition(1.2);
            requesterPopupColumn.Conditions.AddTextBox("RECEIPTUSER")
             .SetLabel("RECEIPTUSERNAME");

            // 팝업 그리드
            requesterPopupColumn.GridColumns.AddTextBoxColumn("REQUESTUSER", 150)
                .SetLabel("RECEIPTUSER")
                .SetValidationKeyColumn();
            requesterPopupColumn.GridColumns.AddTextBoxColumn("REQUESTUSERNAME", 200)
                .SetLabel("RECEIPTUSERNAME");
            //// 팝업 조회조건
            //requesterPopupColumn.Conditions.AddTextBox("RECEIPTUSER")
            //    .SetLabel("RECEIPTUSERNAME");

            //// 팝업 그리드
            //requesterPopupColumn.GridColumns.AddTextBoxColumn("RECEIPTUSER", 150)
            //    .SetValidationKeyColumn();
            //requesterPopupColumn.GridColumns.AddTextBoxColumn("RECEIPTUSERNAME", 200);
        }

        /// <summary>
        /// 품목코드 조회조건
        /// </summary>
        private void InitializeConditionPopup_ProductDefId()
        {
            var popupProduct = Conditions.AddSelectPopup("p_productcode",
                                                                new SqlQuery("GetProductdefidlistByOsp", "10001"
                                                                                , $"LANGUAGETYPE={UserInfo.Current.LanguageType }"
                                                                                , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                ), "PRODUCTDEFNAME", "PRODUCTDEFCODE")
                 .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                 .SetPopupLayoutForm(650, 600)
                 .SetLabel("PRODUCTDEFID")
                 .SetPopupResultCount(1)
                 .SetPosition(1.4);
            // 팝업 조회조건
            popupProduct.Conditions.AddComboBox("P_PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTDEFTYPE")
                .SetDefault("Product");

            popupProduct.Conditions.AddTextBox("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFID");


            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFCODE", 150)
                .SetIsHidden();
            popupProduct.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150).SetIsReadOnly();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsReadOnly();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();

            var txtProductName = Conditions.AddTextBox("P_PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFNAME")
                .SetPosition(1.5);
        }

        /// <summary>
        /// 작업장 설정 
        /// </summary>
        private void InitializeConditionPopup_Areaid()
        {
         
            var popupProduct = Conditions.AddSelectPopup("p_areaid",
                                                               new SqlQuery("GetAreaidListByOsp", "10001"
                                                                               , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                               , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                               , $"P_OWNTYPE={"Y"}"
                                                                               ), "AREANAME", "AREAID")
            .SetPopupLayout("AREAID", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(450, 600)
            .SetLabel("AREANAME")
            .SetPopupResultCount(1)
            .SetRelationIds("p_plantid")
            .SetPosition(2.0);

            // 팝업 조회조건
            popupProduct.Conditions.AddTextBox("AREANAME")
                .SetLabel("AREANAME");


            popupProduct.GridColumns.AddTextBoxColumn("AREAID", 100)
                .SetValidationKeyColumn();
            popupProduct.GridColumns.AddTextBoxColumn("AREANAME", 200);

        }
        /// <summary>
        /// 양산구분
        /// </summary>
        private void InitializeCondition_LotType()
        {
            var LotTypecbobox = Conditions.AddComboBox("p_lottype", new SqlQuery("GetCodeList", "00001", $"CODECLASSID=LotType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("LOTTYPE")
                .SetPosition(2.2)

               .SetEmptyItem("", "")
             ;
        }
        /// <summary>
        /// lotid 조회조건
        /// </summary>
        private void InitializeCondition_Lotid()
        {
             var txtLotid = Conditions.AddTextBox("p_lotid")
                .SetLabel("LOTID")
                .SetPosition(2.4);
        }

        /// <summary>
        /// 확인여부
        /// </summary>
        private void InitializeCondition_YesNo()
        {
            var YesNobox = Conditions.AddComboBox("p_yesno", new SqlQuery("GetCodeList", "00001", $"CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("CHECKYESNO")
                .SetPosition(2.6)

                .SetEmptyItem("전체")
             ;
        }

        #endregion
        
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

        /// <summary>
        /// 저장 후 재조회용 
        /// </summary>
        private void OnSaveConfrimSearch()
        {
            var values = Conditions.GetValues();

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            #region 품목코드 전환 처리 
            string sproductcode = "";
            if (!(values["P_PRODUCTCODE"] == null))
            {
                sproductcode = values["P_PRODUCTCODE"].ToString();
            }
            // 품목코드값이 있으면
            if (!(sproductcode.Equals("")))
            {
                string[] sproductd = sproductcode.Split('|');
                // plant 정보 다시 가져오기 
                values.Add("P_PRODUCTDEFID", sproductd[0].ToString());
                values.Add("P_PRODUCTDEFVERSION", sproductd[1].ToString());
            }
            #endregion

            #region 기간 검색형 전환 처리 

            if (!(values["P_RECEIPTDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_RECEIPTDATE_PERIODFR"]);
                values.Remove("P_RECEIPTDATE_PERIODFR");
                values.Add("P_RECEIPTDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_RECEIPTDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_RECEIPTDATE_PERIODTO"]);
                values.Remove("P_RECEIPTDATE_PERIODTO");
                //requestDateTo = requestDateTo.AddDays(1);
                values.Add("P_RECEIPTDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }


            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtInquiry =  SqlExecuter.Query("GetOutsourcedDistribution", "10001", values);

            if (dtInquiry.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdOutsourcedDistribution.DataSource = dtInquiry;


        }
        #endregion
    }
}
