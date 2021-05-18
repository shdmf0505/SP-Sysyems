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
    /// 프 로 그 램 명  :  외주관리> 외주 단가 관리>정율 인하 단가 등록
    /// 업  무  설  명  : 정율 인하 단가 등록한다.
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-07-05
    /// 수  정  이  력  : 
    /// 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcedFixedRateDownPrice : SmartConditionManualBaseForm
    {
        #region Local Variables

       

        #endregion

        #region 생성자

        public OutsourcedFixedRateDownPrice()
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
            // 콤보박스 셋팅 
            InitializeComboBox();  
            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
            //변경구분
            cboPricechangetype.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboPricechangetype.ValueMember = "CODEID";
            cboPricechangetype.DisplayMember = "CODENAME";
            cboPricechangetype.EditValue = "Lowering";

            cboPricechangetype.DataSource = SqlExecuter.Query("GetCodeList", "00001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "PriceChangeType" } });

            cboPricechangetype.ShowHeader = false;
            cboDecimalplace.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboDecimalplace.ValueMember = "CODEID";
            cboDecimalplace.DisplayMember = "CODENAME";
          
            DataTable dtcode = createCodeHelpDatatable();
            dtcode.Rows.Add(new object[] { "0", "0" });
            dtcode.Rows.Add(new object[] { "1", "1" });
            dtcode.Rows.Add(new object[] { "2", "2" });
            cboDecimalplace.EditValue = "0";
            cboDecimalplace.DataSource = dtcode;
            cboDecimalplace.EditValue = "0";
            cboDecimalplace.ShowHeader = false;
            //소수점 이하처리 
            cboCalculatedeciamlpointtype.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboCalculatedeciamlpointtype.ValueMember = "CODEID";
            cboCalculatedeciamlpointtype.DisplayMember = "CODENAME";
            cboCalculatedeciamlpointtype.EditValue = "Round";

            cboCalculatedeciamlpointtype.DataSource = SqlExecuter.Query("GetCodeList", "00001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "CalculateDecimalPointType" } });

            cboCalculatedeciamlpointtype.ShowHeader = false;
            
        }

        #endregion
        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdFixedRate.GridButtonItem = GridButtonItem.Export;
            grdFixedRate.View.SetIsReadOnly();
            grdFixedRate.View.AddTextBoxColumn("ENTERPRISEID", 120).SetIsHidden();
            grdFixedRate.View.AddTextBoxColumn("PLANTID", 120).SetIsHidden();
            grdFixedRate.View.AddTextBoxColumn("PRICECOMBINATIONID", 120).SetIsHidden();
            grdFixedRate.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 120); //--중공정
            grdFixedRate.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 120); //
            grdFixedRate.View.AddTextBoxColumn("PROCESSSEGMENTID", 120); //--공정
            grdFixedRate.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120); //
            grdFixedRate.View.AddTextBoxColumn("OSPPRICECODE", 100); //           --외주단가코드
            grdFixedRate.View.AddTextBoxColumn("OSPPRICENAME", 150); //--외주단가명
          
            grdFixedRate.View.AddComboBoxColumn("OWNTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OwnType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("*", "*");  // 
            grdFixedRate.View.AddComboBoxColumn("PROCESSPRICETYPE", 100, new SqlQuery("GetCodeListByOspFixRate", "10001", "CODECLASSID=ProcessPriceType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("*", "*");  // 
            grdFixedRate.View.AddTextBoxColumn("PRODUCTDEFID", 120); //       --품목
            grdFixedRate.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80); //
            grdFixedRate.View.AddTextBoxColumn("PRODUCTDEFNAME", 200); //
            grdFixedRate.View.AddTextBoxColumn("AREAID", 80); //
            grdFixedRate.View.AddTextBoxColumn("AREANAME", 120); //
            grdFixedRate.View.AddTextBoxColumn("OSPPRICE", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric); //--단가
            grdFixedRate.View.AddTextBoxColumn("STARTDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd"); //   --적용시작일
            grdFixedRate.View.AddTextBoxColumn("ENDDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd"); //--적용종료일
            grdFixedRate.View.AddTextBoxColumn("CHANGEPRICE", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric); //
      
            grdFixedRate.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가

            txtChangerate.KeyPress += TxtChangerate_KeyPress;
            //계산처리 
            btnCal.Click += BtnCal_Click;
           
        }
        /// <summary>
        /// 숫자만. 입력 가능하도록 (-)값무시 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtChangerate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != '.' && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 8)
            {
                e.Handled = true;
                
            }
        }
        private void BtnCal_Click(object sender, EventArgs e)
        {

            string strChangerate = txtChangerate.Text.ToString();
            double decChangerate = (strChangerate.ToString().Equals("") ? 0 : Convert.ToDouble(strChangerate)); //변경율
            string strDecimalplace = cboDecimalplace.EditValue.ToString();
            int intDecimalplace = (strDecimalplace.ToString().Equals("") ? 0 : Convert.ToInt16(strDecimalplace)); //소수자리
            double dblplace = Math.Pow(10, intDecimalplace);
            //
            string strPricechangetype = cboPricechangetype.EditValue.ToString(); //단가변경구분
            string strcalculatedeciamlpointtype =cboCalculatedeciamlpointtype.EditValue.ToString();
            int intRowCount = grdFixedRate.View.DataRowCount;
            //
            if (intRowCount == 0) return;
            double decChangeratetot = 0;
            //단가변경구분 인상
            if (strPricechangetype.Equals("Raising"))
            {
                decChangeratetot =(100 + decChangerate)* (0.01);
            }
            else
            {
                decChangeratetot = (100 - decChangerate) * (0.01);
            }
            DataTable dtAmount = grdFixedRate.DataSource as DataTable;
           
            for (int i = 0; i < dtAmount.Rows.Count; i++)
            {
                DataRow row = dtAmount.Rows[i];
                string strPrice = row["OSPPRICE"].ToString();
                double decPrice = (strPrice.ToString().Equals("") ? 0 : Convert.ToDouble(strPrice)); //

                double decChangePrice = 0;
                decChangePrice = decPrice * decChangeratetot;
                //반올림
                if (strcalculatedeciamlpointtype.Equals("Round"))
                {
                    decChangePrice = Math.Round(decChangePrice, intDecimalplace);
                }
                else if (strcalculatedeciamlpointtype.Equals("RoundDown")) //버림내림
                {
                    if (intDecimalplace==0)
                    {
                        decChangePrice = Math.Truncate(decChangePrice);
                    }
                    else
                    {
                        decChangePrice = Math.Truncate(decChangePrice* dblplace) / dblplace;
                    }
                   
                }
                else   //RoundUp
                {
                    if (intDecimalplace == 0)
                    {
                        decChangePrice = Math.Ceiling(decChangePrice);
                    }
                    else
                    {
                        decChangePrice = Math.Ceiling(decChangePrice * dblplace) / dblplace;
                    }
                  
                }
                row["CHANGEPRICE"] = decChangePrice;//변경단가
               

            }
            grdFixedRate.DataSource = dtAmount;
        }
        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            //throw new NotImplementedException();
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();
           
            ////테이블 생성 
            ////조건값 insert  
            //var values = Conditions.GetValues();
            //values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            ////기간 처리 
            //#region 기간 검색형 전환 처리 
            //if (!(values["P_APPLYDATE_PERIODFR"].ToString().Equals("")))
            //{
            //    DateTime requestDateFr = Convert.ToDateTime(values["P_APPLYDATE_PERIODFR"]);
            //    values.Remove("P_APPLYDATE_PERIODFR");
            //    values.Add("P_APPLYDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            //}
            //if (!(values["P_APPLYDATE_PERIODTO"].ToString().Equals("")))
            //{
            //    DateTime requestDateTo = Convert.ToDateTime(values["P_APPLYDATE_PERIODTO"]);
            //    values.Remove("P_APPLYDATE_PERIODTO");
            //    values.Add("P_APPLYDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            //}
            //#endregion

            //#region 품목코드 전환 처리 
            //string sproductcode = "";
            //if (!(values["P_PRODUCTCODE"] == null))
            //{
            //    sproductcode = values["P_PRODUCTCODE"].ToString();
            //}
            //    // 품목코드값이 있으면
            //if (!(sproductcode.Equals("")))
            //{
            //    string[] sproductd = sproductcode.Split('|');
            //    // plant 정보 다시 가져오기 
            //    values.Add("P_PRODUCTDEFID", sproductd[0].ToString());
            //    values.Add("P_PRODUCTDEFVERSION", sproductd[1].ToString());
            //}
            //else
            //{
            //    values.Add("P_PRODUCTDEFID","");
            //    values.Add("P_PRODUCTDEFVERSION","");
            //}
            //#endregion
           
            //#region 저장용 data 생성 
            //DataTable dtSave = createSaveDatatable();
            //DataRow drSave = dtSave.NewRow();
            
            //drSave["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
            //drSave["PLANTID"] = values["P_PLANTID"].ToString();
            //drSave["SEQUENCE"] = "";
            //drSave["STARTDATE"] = values["P_APPLYDATE_PERIODFR"].ToString();
            //drSave["ENDDATE"] = values["P_APPLYDATE_PERIODTO"].ToString();
            //drSave["OSPPRICECODE"] = values["P_OSPPRICECODE"].ToString();
            //drSave["PROCESSSEGMENTCLASSID"] = values["P_PROCESSSEGMENTCLASSID"].ToString();
            //drSave["PROCESSSEGMENTID"] = values["P_PROCESSSEGMENTID"].ToString();
            //drSave["PROCESSPRICETYPE"] = values["P_PROCESSPRICETYPE"].ToString();
            //drSave["OWNTYPE"] = values["P_OWNTYPE"].ToString();
            //drSave["PRODUCTDEFVERSION"] = values["P_PRODUCTDEFVERSION"].ToString();
            //drSave["PRODUCTDEFID"] = values["P_PRODUCTDEFID"].ToString();
            //drSave["OSPVENDORID"] = values["P_OSPVENDORID"].ToString();
            //drSave["PRICECHANGETYPE"] = cboPricechangetype.EditValue.ToString();
            //drSave["CHANGERATE"] = txtChangerate.EditValue.ToString();
            //drSave["DECIMALPLACE"] = cboDecimalplace.EditValue.ToString();
            //drSave["CALCULATEDECIAMLPOINTTYPE"] = cboCalculatedeciamlpointtype.EditValue.ToString();
            //drSave["DESCRIPTION"] = txtDescription.Text.Trim().ToString();
            //dtSave.Rows.Add(drSave);
            //#endregion

            //ExecuteRule("OutsourcedFixedRateDownPrice", dtSave);
        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
            {

                ProcSave(btn.Text);
            }
            if (btn.Name.ToString().Equals("Pricecalculation"))
            {

                BtnCal_Click(null,null);
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
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            //기간 처리 
            #region 기간 검색형 전환 처리 
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
            #endregion

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
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtSpecial = await SqlExecuter.QueryAsync("GetOutsourcedFixedRateDownPrice", "10001", values);

            if (dtSpecial.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdFixedRate.DataSource = dtSpecial;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //site
            //InitializeConditionPopup_Plant();
            //적용일자 필수 
            //중공정
            InitializeConditionPopup_Processsegmentclassid();
            //공정코드 
            InitializeConditionPopup_Processsegmentid();
            //외주단가코드
            InitializeConditionPopup_Ospprice();

            //자사구분
            InitializeConditionPopup_Owntype();
            //가공단가구분
            InitializeConditionPopup_Processpricetype();
            //작업장
            InitializeConditionPopup_OspAreaid();
            ////협력사 협력사명
            //InitializeConditionPopup_OspVendorid();
            //품목코드
            InitializeConditionPopup_ProductDefId();
        }
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {

            var planttxtbox = Conditions.AddComboBox("p_plantid", new SqlQuery("GetPlantList", "00001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
               .SetPosition(0.1)
               .SetValidationIsRequired() 
               .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
            ;
           

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
               .SetPosition(1.1);

            // 팝업 조회조건
            processsegmentclassidPopupColumn.Conditions.AddTextBox("MIDDLEPROCESSSEGMENTCLASSNAME")
                .SetLabel("MIDDLEPROCESSSEGMENTCLASS");

            // 팝업 그리드
            processsegmentclassidPopupColumn.GridColumns.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSID", 150);
            processsegmentclassidPopupColumn.GridColumns.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSNAME", 200);

        }

        /// <summary>
        /// 공정 선택팝업
        /// </summary>
        private void InitializeConditionPopup_Processsegmentid()
        {


            var popupProcesssegmentid = Conditions.AddSelectPopup("p_processsegmentid",
                                                               new SqlQuery("GetProcessSegmentListByOsp", "10001"
                                                                               , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                               , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                               ), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
            .SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(500, 600)
            .SetLabel("PROCESSSEGMENTNAME")
            .SetPopupResultCount(1)

            .SetPosition(1.2);

            // 팝업 조회조건
            popupProcesssegmentid.Conditions.AddTextBox("PROCESSSEGMENTNAME")
                 .SetLabel("PROCESSSEGMENT");


            popupProcesssegmentid.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetValidationKeyColumn();
            popupProcesssegmentid.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 250);


        }
        /// <summary>
        ///  외주단가코드
        /// </summary>
        private void InitializeConditionPopup_Ospprice()
        {
            var txtOsppricecode = Conditions.AddSelectPopup("p_osppricecode", new SqlQuery("GetOsppricecodeListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "OSPPRICENAME", "OSPPRICECODE")
               .SetLabel("OSPPRICECODE")
               .SetPopupLayoutForm(400, 600)
               .SetLabel("OSPPRICECODE")
               .SetPopupResultCount(1)
               .SetRelationIds("p_plantid")
               .SetPosition(2.0);
            // 팝업 조회조건
            txtOsppricecode.Conditions.AddTextBox("OSPPRICENAME")
                .SetLabel("OSPPRICENAME");
            txtOsppricecode.GridColumns.AddTextBoxColumn("OSPPRICECODE", 100)
              .SetValidationKeyColumn();
            txtOsppricecode.GridColumns.AddTextBoxColumn("OSPPRICENAME", 150);

        }

        /// <summary>
        /// 자사구분
        /// </summary>
        private void InitializeConditionPopup_Owntype()
        {

            var owntypecbobox = Conditions.AddComboBox("p_owntype", new SqlQuery("GetCodeAllListByOspPrice", "10001", "CODECLASSID=OwnType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetLabel("OWNTYPE")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
               .SetPosition(2.1)
               .SetEmptyItem("", "")
            ;
            //   .SetIsReadOnly(true);

        }
        /// <summary>
        ///  가공단가구분
        /// </summary>
        private void InitializeConditionPopup_Processpricetype()
        {

            var processpricetypecbobox = Conditions.AddComboBox("p_processpricetype", new SqlQuery("GetCodeAllListByOspPrice", "10001", "CODECLASSID=ProcessPriceType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetLabel("PROCESSPRICETYPE")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
               .SetPosition(2.2)
               .SetEmptyItem("", "")
            ;
            //   .SetIsReadOnly(true);

        }

        /// <summary>
        /// 작업장
        /// </summary>
        private void InitializeConditionPopup_OspAreaid()
        {
            // 팝업 컬럼설정
            var popupProduct = Conditions.AddSelectPopup("p_areaid",
                                                                new SqlQuery("GetAreaidListAuthorityByOsp", "10001"
                                                                                , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                                , $"USERID={UserInfo.Current.Id}"
                                                                                , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                ), "AREANAME", "AREAID")
             .SetPopupLayout("AREAID", PopupButtonStyles.Ok_Cancel, true, false)
             .SetPopupLayoutForm(450, 600)
             .SetLabel("AREANAME")
             .SetPopupResultCount(1)
             .SetRelationIds("p_plantid")
              .SetPosition(2.3);
            // 팝업 조회조건
            popupProduct.Conditions.AddTextBox("P_AREANAME")
                .SetLabel("AREANAME");

            // 팝업 그리드
            popupProduct.GridColumns.AddTextBoxColumn("AREAID", 120);
            popupProduct.GridColumns.AddTextBoxColumn("AREANAME", 200);


        }
        /// <summary>
        /// 작업업체
        /// </summary>
        private void InitializeConditionPopup_OspVendorid()
        {
            // 팝업 컬럼설정
            var vendoridPopupColumn = Conditions.AddSelectPopup("p_ospvendorid", new SqlQuery("GetVendorListByOsp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "OSPVENDORNAME", "OSPVENDORID")
               .SetPopupLayout("OSPVENDORID", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("OSPVENDORID")
               .SetPopupResultCount(1)
               .SetRelationIds("p_plantid", "p_areaid")
               .SetPosition(2.4);

            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("OSPVENDORNAME")
                .SetLabel("OSPVENDORNAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);

        }
        /// <summary>
        /// 품목코드 조회조건
        /// </summary>
        private void InitializeConditionPopup_ProductDefId()
        {
            var popupProduct = Conditions.AddSelectPopup("p_productcode",
                                                                new SqlQuery("GetProductdefidlistByOsp", "10001"
                                                                                , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                                , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                ), "PRODUCTDEFNAME", "PRODUCTDEFCODE")
                 .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                 .SetPopupLayoutForm(650, 600)
                 .SetLabel("PRODUCTDEFID")
                 .SetPopupResultCount(1)
                 .SetPosition(3.1);
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
            string strChangerate = this.txtChangerate.Text.ToString();
            double decChangerate = (strChangerate.ToString().Equals("") ? 0 : Convert.ToDouble(strChangerate)); //변경율
            if (decChangerate == 0)
            {
                throw MessageException.Create("InValidOspRequiredField", lblChangerate.Text); //메세지 
              
            }

            ///가공단가 list count 체크
            grdFixedRate.View.CheckValidation();

            DataTable changed = grdFixedRate.DataSource as DataTable;

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
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
        /// 저장시 테이블 생성
        /// </summary>
        /// <returns></returns>
        private DataTable createSaveDatatable()
        {
            DataTable dt = new DataTable();

            dt.TableName = "list";
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("SEQUENCE");
            dt.Columns.Add("STARTDATE");
            dt.Columns.Add("ENDDATE");
            dt.Columns.Add("OSPPRICECODE");
            dt.Columns.Add("PROCESSSEGMENTCLASSID");
            dt.Columns.Add("PROCESSSEGMENTID");
            dt.Columns.Add("PROCESSPRICETYPE");
            dt.Columns.Add("OWNTYPE");
            dt.Columns.Add("PRODUCTDEFVERSION");
            dt.Columns.Add("PRODUCTDEFID");
            dt.Columns.Add("VENDORID");
            dt.Columns.Add("AREAID");
            dt.Columns.Add("PRICECHANGETYPE");
            dt.Columns.Add("CHANGERATE");
            dt.Columns.Add("DECIMALPLACE");
            dt.Columns.Add("CALCULATEDECIAMLPOINTTYPE");
            dt.Columns.Add("DESCRIPTION");
           
            return dt;
        }
        private DataTable createCodeHelpDatatable()
        {
            DataTable dt = new DataTable();

            dt.TableName = "list";
            dt.Columns.Add("CODEID");
            dt.Columns.Add("CODENAME");

            return dt;
        }
        private void ProcSave(string strtitle)
        {
            string strChangerate = this.txtChangerate.Text.ToString();
            double decChangerate = (strChangerate.ToString().Equals("") ? 0 : Convert.ToDouble(strChangerate)); //변경율
            if (decChangerate == 0)
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblChangerate.Text); //메세지 
                return;
            }

            ///가공단가 list count 체크
            grdFixedRate.View.CheckValidation();

            DataTable changed = grdFixedRate.DataSource as DataTable;

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                return;
            }
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    var values = Conditions.GetValues();
                    values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    //기간 처리 
                    #region 기간 검색형 전환 처리 
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
                    #endregion

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
                    else
                    {
                        values.Add("P_PRODUCTDEFID", "");
                        values.Add("P_PRODUCTDEFVERSION", "");
                    }
                    #endregion

                    #region 저장용 data 생성 
                    DataTable dtSave = createSaveDatatable();
                    DataRow drSave = dtSave.NewRow();

                    drSave["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
                    drSave["PLANTID"] = values["P_PLANTID"].ToString();
                    drSave["SEQUENCE"] = "";
                    drSave["STARTDATE"] = values["P_APPLYDATE_PERIODFR"].ToString();
                    drSave["ENDDATE"] = values["P_APPLYDATE_PERIODTO"].ToString();
                    drSave["OSPPRICECODE"] = values["P_OSPPRICECODE"].ToString();
                    drSave["PROCESSSEGMENTCLASSID"] = values["P_PROCESSSEGMENTCLASSID"].ToString();
                    drSave["PROCESSSEGMENTID"] = values["P_PROCESSSEGMENTID"].ToString();
                    drSave["PROCESSPRICETYPE"] = values["P_PROCESSPRICETYPE"].ToString();
                    drSave["OWNTYPE"] = values["P_OWNTYPE"].ToString();
                    drSave["PRODUCTDEFVERSION"] = values["P_PRODUCTDEFVERSION"].ToString();
                    drSave["PRODUCTDEFID"] = values["P_PRODUCTDEFID"].ToString();
                    drSave["VENDORID"] = "";
                    drSave["AREAID"] = values["P_AREAID"].ToString();
                    drSave["PRICECHANGETYPE"] = cboPricechangetype.EditValue.ToString();
                    drSave["CHANGERATE"] = txtChangerate.EditValue.ToString();
                    drSave["DECIMALPLACE"] = cboDecimalplace.EditValue.ToString();
                    drSave["CALCULATEDECIAMLPOINTTYPE"] = cboCalculatedeciamlpointtype.EditValue.ToString();
                    drSave["DESCRIPTION"] = txtDescription.Text.Trim().ToString();
                    dtSave.Rows.Add(drSave);
                    #endregion

                    ExecuteRule("OutsourcedFixedRateDownPrice", dtSave);

                    ShowMessage("SuccessOspProcess");
                    //재조회 
                    OnSaveConfrimSearch();
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();


                }
            }
        }
        private void OnSaveConfrimSearch()
        {

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            //기간 처리 
            #region 기간 검색형 전환 처리 
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
            #endregion

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
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtSpecial = SqlExecuter.Query("GetOutsourcedFixedRateDownPrice", "10001", values);

            if (dtSpecial.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdFixedRate.DataSource = dtSpecial;

        }
        #endregion
    }
}
