#region using
using Micube.Framework.Net;
using Micube.Framework;
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

using DevExpress.XtraCharts;
using DevExpress.Utils;
using Micube.Framework.SmartControls.Validations;
using Micube.SmartMES.StandardInfo.Popup;
#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램명 : 기준정보 > Setup > 품목규칙 등록
    /// 업 무 설명 : 품목 코드,명,계산식 등록
    /// 생  성  자 : 윤성원
    /// 생  성  일 : 2019-06-27
    /// 수정 이 력 : 
    /// </summary> 
	public partial class InkJet : SmartConditionManualBaseForm
	{
        #region Local Variables
        #endregion

        #region 생성자
        public InkJet()
		{
			InitializeComponent();
            InitializeEvent();
           
        }

        #endregion


        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        protected override void InitializeCondition()
        {

            base.InitializeCondition();
           

            //Conditions.AddComboBox("STATUS", new SqlQuery("GetCodeList", "00001", $"CODECLASSID=Status", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            InitializeCondition_Popup();

            //InitializeCondition_Popup();
            // 버전
            //Conditions.AddComboBox("ITEMVERSION", new SqlQuery("GetItemVersion", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}")).SetValidationIsRequired();


        }
        #endregion


        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 검색조건 팝업 예제
        /// </summary>
        private void InitializeCondition_Popup()
        {
            // 팝업 컬럼 설정
            var parentPopupColumn = Conditions.AddSelectPopup("CUSTOMERID", new SqlQuery("GetCustomerList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
               .SetPopupLayout("CUSTOMER", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
               .SetPopupResultCount(1)  //팝업창 선택가능한 개수
               ;
            // 팝업 그리드
            // 팝업에서 사용할 조회조건 항목 추가
            parentPopupColumn.Conditions.AddTextBox("TXTCUSTOMERID");

            // 팝업 그리드 설정
            parentPopupColumn.GridColumns.AddTextBoxColumn("CUSTOMERID", 80);
            parentPopupColumn.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ADDRESS", 250);
            parentPopupColumn.GridColumns.AddTextBoxColumn("CEONAME", 100);
            parentPopupColumn.GridColumns.AddTextBoxColumn("TELNO", 100);
            parentPopupColumn.GridColumns.AddTextBoxColumn("FAXNO", 100);
        }



        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // 거래처 그리드 초기화
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            grdInkjetcustomer.GridButtonItem = GridButtonItem.All;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdInkjetcustomer.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdInkjetcustomer.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdInkjetcustomer.View.AddTextBoxColumn("VALIDSTATE").SetIsHidden();
            grdInkjetcustomer.View.AddTextBoxColumn("INKJETCODE").SetIsHidden();
            grdInkjetcustomer.View.AddComboBoxColumn("CODETYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=Inkcodetype", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetEmptyItem("", "", true).SetValidationIsRequired();
            
            InitializeGrid_CustomerListPopup();
            grdInkjetcustomer.View.AddTextBoxColumn("CUSTOMERNAME").SetIsReadOnly();
            grdInkjetcustomer.View.AddTextBoxColumn("ENDUSER", 150).SetValidationIsRequired();
            grdInkjetcustomer.View.PopulateColumns();


            //Customer GRID 초기화
            grdCustomer.GridButtonItem = GridButtonItem.All;
            grdCustomer.View.AddTextBoxColumn("IDCLASSID",100).SetIsHidden();
            grdCustomer.View.AddTextBoxColumn("IDDEFID", 150)
                .SetValidationIsRequired()
                .SetValidationKeyColumn();
            grdCustomer.View.AddTextBoxColumn("IDDEFNAME", 200);
            grdCustomer.View.AddComboBoxColumn("ENTERPRISEID", new SqlQuery("GetEnterpriseList", "10001"), "ENTERPRISENAME", "ENTERPRISEID")
                .SetLabel("ENTERPRISE").SetIsHidden();
            grdCustomer.View.AddComboBoxColumn("PLANTID", new SqlQuery("GetPlantList", "10001"), "PLANTNAME", "PLANTID")
                .SetLabel("SITE").SetIsHidden();
            

            grdCustomer.View.AddTextBoxColumn("IDDEFTYPE").SetIsHidden();
            //Alpha ;
            grdCustomer.View.AddSpinEditColumn("LENGTH", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                .SetValueRange(1, decimal.MaxValue)
                .SetDefault("1");

            grdCustomer.View.AddComboBoxColumn("FORMAT", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InkJetCodeType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            //유효상태, 생성자, 수정자...
            grdCustomer.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);


            grdCustomer.View.AddTextBoxColumn("IDDEFCATEGORY").SetIsHidden();
            //Prefix;
            
            grdCustomer.View.AddSpinEditColumn("SEQUENCE", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                .SetValueRange(1, decimal.MaxValue)
                .SetDefault("1")
                .SetValidationIsRequired()
                .SetValidationKeyColumn()
                .SetIsHidden();
            grdCustomer.View.PopulateColumns();


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // 거래처 품목 그리드 초기화
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            grdInkjetproduct.GridButtonItem = GridButtonItem.All;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdInkjetproduct.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdInkjetproduct.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdInkjetproduct.View.AddTextBoxColumn("VALIDSTATE").SetIsHidden();
            grdInkjetproduct.View.AddTextBoxColumn("INKJETCODE").SetIsHidden();
            grdInkjetproduct.View.AddComboBoxColumn("CODETYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=Inkcodetype", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            .SetEmptyItem("", "", true).SetValidationIsRequired();

            InitializeGrid_ProductdefIDListPopup();
            grdInkjetproduct.View.AddTextBoxColumn("PRODUCTDEFNAME",250).SetIsReadOnly();
            grdInkjetproduct.View.AddTextBoxColumn("PRODUCTDEFVERSION",80).SetIsReadOnly();
            grdInkjetproduct.View.AddTextBoxColumn("ENDUSER", 150).SetIsHidden();

            grdInkjetproduct.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            .SetDefault("Valid")
            .SetValidationIsRequired()
            .SetTextAlignment(TextAlignment.Center);


            grdInkjetproduct.View.PopulateColumns();



            //Product GRID 초기화
            grdProduct.GridButtonItem = GridButtonItem.All;
            grdProduct.View.AddTextBoxColumn("IDCLASSID", 100).SetIsHidden();
            grdProduct.View.AddTextBoxColumn("IDDEFID", 150)
                .SetValidationIsRequired()
                .SetValidationKeyColumn();
            grdProduct.View.AddTextBoxColumn("IDDEFNAME", 200);
            grdProduct.View.AddComboBoxColumn("ENTERPRISEID", new SqlQuery("GetEnterpriseList", "10001"), "ENTERPRISENAME", "ENTERPRISEID")
                .SetLabel("ENTERPRISE").SetIsHidden();
            grdProduct.View.AddComboBoxColumn("PLANTID", new SqlQuery("GetPlantList", "10001"), "PLANTNAME", "PLANTID")
                .SetLabel("SITE").SetIsHidden();


            grdProduct.View.AddTextBoxColumn("IDDEFTYPE").SetIsHidden();
            //Alpha ;
            grdProduct.View.AddSpinEditColumn("LENGTH", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                .SetValueRange(1, decimal.MaxValue)
                .SetDefault("1");

            grdProduct.View.AddComboBoxColumn("FORMAT", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=InkJetCodeType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            

            //유효상태, 생성자, 수정자...
            grdProduct.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);


            grdProduct.View.AddTextBoxColumn("IDDEFCATEGORY").SetIsHidden();
            //Prefix;

            grdProduct.View.AddSpinEditColumn("SEQUENCE", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                .SetValueRange(1, decimal.MaxValue)
                .SetDefault("1")
                .SetValidationIsRequired()
                .SetValidationKeyColumn()
                .SetIsHidden();
            grdProduct.View.PopulateColumns();




        }

        // 컬럼이나 숫자 코드관리에서 데이터를 가져온다.
        private void InitializeGrid_CustomerListPopup()
        {
            var parentCodeClassPopupColumn = this.grdInkjetcustomer.View.AddSelectPopupColumn("CUSTOMERID", new SqlQuery("GetCustomerList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("CUSTOMER", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                //.SetPopupResultMapping("CODECLASSID", "CODECLASSID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정
                //.SetPopupAutoFillColumns("CODECLASSNAME")
                // Validation 이 필요한 경우 호출할 Method 지정
                .SetPopupValidationCustom(ValidationCustomerPopup)
                .SetValidationIsRequired()
                ;

            // 팝업에서 사용할 조회조건 항목 추가
            parentCodeClassPopupColumn.Conditions.AddTextBox("TXTCUSTOMERID");
            

            // 팝업 그리드 설정
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("CUSTOMERID", 80);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("ADDRESS", 250);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("CEONAME", 100);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("TELNO", 100);
            parentCodeClassPopupColumn.GridColumns.AddTextBoxColumn("FAXNO", 100);


        }


       



        private void InitializeGrid_ProductdefIDListPopup()
        {
            var parentProductdefIDPopupColumn = this.grdInkjetproduct.View.AddSelectPopupColumn("PRODUCTDEFID", new SqlQuery("GetSpecificationsItemPupop", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"VALIDSTATE={"Valid"}"))
                   // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                   .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, false)
                   // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                   .SetPopupResultCount(1)
                   // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)

                   .SetPopupResultMapping("PRODUCTDEFID", "ITEMID")

                   // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                   .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                   // 그리드의 남은 영역을 채울 컬럼 설정
                   //.SetPopupAutoFillColumns("CODECLASSNAME")
                   // Validation 이 필요한 경우 호출할 Method 지정
                   .SetPopupValidationCustom(ValidationInkjetproductPopup);

            // 팝업에서 사용할 조회조건 항목 추가
            parentProductdefIDPopupColumn.Conditions.AddTextBox("ITEMID");
            parentProductdefIDPopupColumn.Conditions.AddTextBox("ITEMVERSION");
            parentProductdefIDPopupColumn.Conditions.AddTextBox("ITEMNAME");

            // 팝업 그리드 설정
            parentProductdefIDPopupColumn.GridColumns.AddTextBoxColumn("ITEMID", 150);
            parentProductdefIDPopupColumn.GridColumns.AddTextBoxColumn("ITEMVERSION", 200);
            parentProductdefIDPopupColumn.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
        }


        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGridIdDefinitionManagement();
        }

        //고객관리 에서 가져와 등록한다.  
        private ValidationResultCommon ValidationCustomerPopup(DataRow currentRow, IEnumerable<DataRow> popupSelections)
        {

            ValidationResultCommon result = new ValidationResultCommon();
            //string myCodeClassId = currentRow.ToStringNullToEmpty("CODECLASSID");

            foreach (DataRow row in popupSelections)
            {
                currentRow["CUSTOMERNAME"] = row["CUSTOMERNAME"];
            }
            return result;
        }

        //품목관리 에서 가져와 등록한다.  
        private ValidationResultCommon ValidationInkjetproductPopup(DataRow currentRow, IEnumerable<DataRow> popupSelections)
        {

            ValidationResultCommon result = new ValidationResultCommon();
            //string myCodeClassId = currentRow.ToStringNullToEmpty("CODECLASSID");

            foreach (DataRow row in popupSelections)
            {
                currentRow["PRODUCTDEFNAME"] = row["ITEMNAME"];
                currentRow["PRODUCTDEFVERSION"] = row["ITEMVERSION"];
                
            }
            return result;
        }


        #endregion

        #region 이벤트

        private void InitializeEvent()
        {
            // 품목 규칙 대상 그리드 이벤트
            grdInkjetcustomer.View.FocusedRowChanged += grdInkjetcustomer_FocusedRowChanged;

            //grdInkjetproduct.View.FocusedRowChanged += grdInkjetproduct_FocusedRowChanged;
            // 추가
            grdInkjetcustomer.View.AddingNewRow += grdInkjetcustomer_AddingNewRow;
            grdInkjetproduct.View.AddingNewRow += grdInkjetproduct_AddingNewRow;
            grdCustomer.View.AddingNewRow += grdCustomer_AddingNewRow;
            grdProduct.View.AddingNewRow += grdProduct_AddingNewRow;

  

        }

  

        //private void grdInkjetproduct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        //{
        //    if (grdInkjetproduct.View.FocusedRowHandle < 0)
        //        return;

        //    DataRow row = this.grdInkjetproduct.View.GetFocusedDataRow();
        //    Dictionary<string, object> param = new Dictionary<string, object>();
        //    param.Add("P_ENTERPRISEID", row["ENTERPRISEID"].ToString());
        //    param.Add("P_PLANTID", row["PLANTID"].ToString());
        //    param.Add("P_IDCLASSID", row["INKJETCODE"].ToString() + row["CODETYPE"].ToString());
        //    grdProduct.DataSource = SqlExecuter.Query("SelectIdDefList", "10001", param);
        //}

        private void grdProduct_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (grdInkjetcustomer.View.FocusedRowHandle < 0)
                return;

            DataRow row = grdInkjetcustomer.View.GetFocusedDataRow();
            args.NewRow["ENTERPRISEID"] = row["ENTERPRISEID"];
            args.NewRow["PLANTID"] = row["PLANTID"];
            
            args.NewRow["IDDEFTYPE"] = "Alpha";
            args.NewRow["IDDEFCATEGORY"] = "Prefix";
            args.NewRow["IDDEFID"] = "00";

            DataTable dtProduct = (DataTable)grdProduct.DataSource;
            DataTable dtProductCopy = dtProduct.Copy();
            dtProductCopy.Columns.Add("dIDDEFID", typeof(decimal));

            foreach (DataRow rowIDDEFID in dtProductCopy.Rows)
            {
                rowIDDEFID["dIDDEFID"] = rowIDDEFID["IDDEFID"];
            }

            if (dtProductCopy != null)
            {
                if (dtProductCopy.Rows.Count == 0)
                {
                    args.NewRow["IDDEFID"] = "10";
                    
                }
                else
                {
                    args.NewRow["IDDEFID"] = int.Parse(dtProductCopy.Compute("MAX(dIDDEFID)", "1=1").ToString()) + 10;
                }

            }
            else
            {
                args.NewRow["IDDEFID"] = "10";
            }

            args.NewRow["IDCLASSID"] = row["INKJETCODE"].ToString() + row["CODETYPE"].ToString() + "TEXT" + args.NewRow["IDDEFID"].ToString();
        }

        private void grdCustomer_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (grdInkjetcustomer.View.FocusedRowHandle < 0)
                return;

            DataRow row = grdInkjetcustomer.View.GetFocusedDataRow();
            args.NewRow["ENTERPRISEID"] = row["ENTERPRISEID"];
            args.NewRow["PLANTID"] = row["PLANTID"];
            
            args.NewRow["IDDEFTYPE"] = "Alpha";
            args.NewRow["IDDEFCATEGORY"] = "Prefix";
            args.NewRow["IDDEFID"] = "00";

            DataTable dtCustomer = (DataTable)grdCustomer.DataSource;
            DataTable dtCustomerCopy = dtCustomer.Copy();
            dtCustomerCopy.Columns.Add("dIDDEFID", typeof(decimal));
            
            foreach(DataRow rowIDDEFID in dtCustomerCopy.Rows)
            {
                rowIDDEFID["dIDDEFID"] = rowIDDEFID["IDDEFID"];
            }

            if (dtCustomerCopy != null)
            {
                if (dtCustomerCopy.Rows.Count == 0)
                {
                    args.NewRow["IDDEFID"] = "10";
                }
                else
                {
                    args.NewRow["IDDEFID"] = int.Parse(dtCustomerCopy.Compute("MAX(dIDDEFID)", "1=1").ToString()) + 10;
                }

            }
            else
            {
                args.NewRow["IDDEFID"] = "10";
            }
            args.NewRow["IDCLASSID"] = row["INKJETCODE"].ToString() + row["CODETYPE"].ToString()+ "INKJET" + args.NewRow["IDDEFID"].ToString();
        }

        private void grdInkjetproduct_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (grdInkjetproduct.View.FocusedRowHandle < 0)
                return;

            DataRow row = grdInkjetcustomer.View.GetFocusedDataRow();
            args.NewRow["ENTERPRISEID"] = row["ENTERPRISEID"];
            args.NewRow["PLANTID"] = row["PLANTID"];
            
            args.NewRow["ENDUSER"] = row["ENDUSER"];

            //GetNumber number = new GetNumber();
            //DataTable dtDate = SqlExecuter.Query("GetItemId", "10001");
            //string sdate = dtDate.Rows[0]["ITEMID"].ToString().Substring(0, 8);
            //args.NewRow["INKJETCODE"] = number.GetStdNumber("Inkjetcode", sdate);
            args.NewRow["INKJETCODE"] = row["INKJETCODE"];
            args.NewRow["CUSTOMERID"] = row["CUSTOMERID"];
            args.NewRow["CODETYPE"] = row["CODETYPE"];
            args.NewRow["VALIDSTATE"] = "Valid";

        }

        private void grdInkjetcustomer_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = UserInfo.Current.Plant;
            args.NewRow["VALIDSTATE"] = "Valid";

            GetNumber number = new GetNumber();

            DataTable dtDate = SqlExecuter.Query("GetItemId", "10001");
            string sdate = dtDate.Rows[0]["ITEMID"].ToString().Substring(0, 8);

            args.NewRow["INKJETCODE"] = number.GetStdNumber("Inkjetcode", sdate);


            //DataTable dtInkjetcustomer = SqlExecuter.QueryAsync("GetInkJetCustomer", "10001", values);

            //Dictionary<string, object> paramINKJET = new Dictionary<string, object>();
            //paramINKJET.Add("P_ENTERPRISEID", row["ENTERPRISEID"].ToString());
            //paramINKJET.Add("P_PLANTID", row["PLANTID"].ToString());
            //paramINKJET.Add("P_IDCLASSID", row["INKJETCODE"].ToString() + row["CODETYPE"].ToString() + "INKJET");
            //DataTable dtINKJET = SqlExecuter.Query("GetInkJetIdDefList", "10001", paramINKJET);



        }

        private void grdInkjetcustomer_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // 헤더그리드가 없을 경우 리턴
            if (grdInkjetcustomer.View.FocusedRowHandle < 0)
                return;

            DataRow row = this.grdInkjetcustomer.View.GetFocusedDataRow();

            Dictionary<string, object> paramINKJET = new Dictionary<string, object>();
            paramINKJET.Add("P_ENTERPRISEID", row["ENTERPRISEID"].ToString());
            paramINKJET.Add("P_PLANTID", row["PLANTID"].ToString());
            paramINKJET.Add("P_IDCLASSID", row["INKJETCODE"].ToString() + row["CODETYPE"].ToString() + "INKJET");
            grdCustomer.DataSource = SqlExecuter.Query("GetInkJetIdDefList", "10001", paramINKJET);

            Dictionary<string, object> paramTEXT = new Dictionary<string, object>();
            paramTEXT.Add("P_ENTERPRISEID", row["ENTERPRISEID"].ToString());
            paramTEXT.Add("P_PLANTID", row["PLANTID"].ToString());
            paramTEXT.Add("P_IDCLASSID", row["INKJETCODE"].ToString() + row["CODETYPE"].ToString() + "TEXT");
            grdProduct.DataSource = SqlExecuter.Query("GetInkJetIdDefList", "10001", paramTEXT);

            Dictionary<string, object> paramInkjetproduct = new Dictionary<string, object>();
            paramInkjetproduct.Add("CUSTOMERID", row["CUSTOMERID"].ToString());
            paramInkjetproduct.Add("INKJETCODE", row["INKJETCODE"].ToString());
            paramInkjetproduct.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
            paramInkjetproduct.Add("PLANTID", row["PLANTID"].ToString());
            paramInkjetproduct.Add("CODETYPE", row["CODETYPE"].ToString());
            grdInkjetproduct.DataSource = SqlExecuter.Query("GetInkjetproductlist", "10001", paramInkjetproduct);

        }

        #endregion

        #region 툴바


        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Copy"))
            {
                if (grdInkjetcustomer.View.FocusedRowHandle < 0)
                    return;

                DataRow row = grdInkjetcustomer.View.GetFocusedDataRow();

                Dictionary<string, object> paramInkjetcustomer = new Dictionary<string, object>();
                paramInkjetcustomer.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
                paramInkjetcustomer.Add("PLANTID", row["PLANTID"].ToString());
                paramInkjetcustomer.Add("INKJETCODE", row["INKJETCODE"].ToString());
                DataTable dtInkjetcustomer = SqlExecuter.Query("GetInkJetCustomer", "10001", paramInkjetcustomer);

                InkJetCopyPopup InkJetcopy = new InkJetCopyPopup(row);
                InkJetcopy.ShowDialog();

                if (InkJetcopy.txtENDUSERTaget.Text != "")
                {
                    return;
                }



                GetNumber number = new GetNumber();
                DataTable dtDate = SqlExecuter.Query("GetItemId", "10001");
                string sdate = dtDate.Rows[0]["ITEMID"].ToString().Substring(0, 8);


                string sINKJETCODE = number.GetStdNumber("Inkjetcode", sdate);
                // 복사시 추가
                grdInkjetcustomer.View.AddNewRow();
                DataRow rowAddNew = grdInkjetcustomer.View.GetFocusedDataRow();

                sINKJETCODE = rowAddNew["INKJETCODE"].ToString();


                DataTable dtInkjetcustomerNew = (DataTable)grdInkjetcustomer.DataSource;

                foreach (DataRow rowNew in dtInkjetcustomer.Rows)
                {
                    foreach (DataColumn col in dtInkjetcustomerNew.Columns)
                    {
                        switch (col.ColumnName)
                        {
                            case "ENDUSER":
                                rowAddNew["ENDUSER"] = InkJetcopy.txtENDUSERTaget.Text;
                                break;
                            case "INKJETCODE":
                                break;
                            default:
                                rowAddNew[col.ColumnName] = rowNew[col.ColumnName];
                                break;
                        }

                    }
                }

                Dictionary<string, object> paramINKJET = new Dictionary<string, object>();
                paramINKJET.Add("P_ENTERPRISEID", dtInkjetcustomer.Rows[0]["ENTERPRISEID"].ToString());
                paramINKJET.Add("P_PLANTID", dtInkjetcustomer.Rows[0]["PLANTID"].ToString());
                paramINKJET.Add("P_IDCLASSID", dtInkjetcustomer.Rows[0]["INKJETCODE"].ToString() + dtInkjetcustomer.Rows[0]["CODETYPE"].ToString() + "INKJET");

                DataTable dtINKJET = SqlExecuter.Query("GetInkJetIdDefList", "10001", paramINKJET);

                DataTable dtINKJETNew = (DataTable)grdCustomer.DataSource;

                foreach (DataRow rowNew in dtINKJET.Rows)
                {
                    grdCustomer.View.AddNewRow();
                    DataRow rowCustomerAddNew = grdCustomer.View.GetFocusedDataRow();

                    foreach (DataColumn col in dtINKJETNew.Columns)
                    {
                        switch (col.ColumnName)
                        {
                            case "IDCLASSID":

                                rowCustomerAddNew["IDCLASSID"] = sINKJETCODE + rowAddNew["CODETYPE"].ToString() + "INKJET" + rowNew["IDDEFID"].ToString();

                                break;
                            default:
                                rowCustomerAddNew[col.ColumnName] = rowNew[col.ColumnName];
                                break;
                        }
                    }
                }


                Dictionary<string, object> paramTEXT = new Dictionary<string, object>();
                paramTEXT.Add("P_ENTERPRISEID", dtInkjetcustomer.Rows[0]["ENTERPRISEID"].ToString());
                paramTEXT.Add("P_PLANTID", dtInkjetcustomer.Rows[0]["PLANTID"].ToString());
                paramTEXT.Add("P_IDCLASSID", dtInkjetcustomer.Rows[0]["INKJETCODE"].ToString() + dtInkjetcustomer.Rows[0]["CODETYPE"].ToString() + "TEXT");
                DataTable dtTEXT = SqlExecuter.Query("GetInkJetIdDefList", "10001", paramTEXT);


                DataTable dtProductNew = (DataTable)grdProduct.DataSource;

                foreach (DataRow rowNew in dtTEXT.Rows)
                {
                    grdProduct.View.AddNewRow();
                    DataRow rowProductAddNew = grdProduct.View.GetFocusedDataRow();

                    foreach (DataColumn col in dtProductNew.Columns)
                    {
                        switch (col.ColumnName)
                        {
                            case "IDCLASSID":

                                rowProductAddNew["IDCLASSID"] = sINKJETCODE + rowAddNew["CODETYPE"].ToString() + "TEXT" + rowNew["IDDEFID"].ToString();

                                break;

                            default:
                                rowProductAddNew[col.ColumnName] = rowNew[col.ColumnName];
                                break;
                        }

                    }
                }

                Dictionary<string, object> paramInkjetproduct = new Dictionary<string, object>();
                paramInkjetproduct.Add("CUSTOMERID", dtInkjetcustomer.Rows[0]["CUSTOMERID"].ToString());
                paramInkjetproduct.Add("INKJETCODE", dtInkjetcustomer.Rows[0]["INKJETCODE"].ToString());
                paramInkjetproduct.Add("ENTERPRISEID", dtInkjetcustomer.Rows[0]["ENTERPRISEID"].ToString());
                paramInkjetproduct.Add("PLANTID", dtInkjetcustomer.Rows[0]["PLANTID"].ToString());
                paramInkjetproduct.Add("CODETYPE", dtInkjetcustomer.Rows[0]["CODETYPE"].ToString());
                DataTable dtInkjetproduct = SqlExecuter.Query("GetInkjetproductlist", "10001", paramInkjetproduct);

                DataTable dtInkjetproducttNew = (DataTable)grdInkjetproduct.DataSource;

                foreach (DataRow rowNew in dtInkjetproduct.Rows)
                {
                    grdInkjetproduct.View.AddNewRow();
                    DataRow rowInkjetproductAddNew = grdInkjetproduct.View.GetFocusedDataRow();

                    foreach (DataColumn col in dtInkjetproducttNew.Columns)
                    {
                        switch (col.ColumnName)
                        {
                            case "INKJETCODE":
                                rowInkjetproductAddNew["INKJETCODE"] = sINKJETCODE;
                                break;
                            default:
                                rowInkjetproductAddNew[col.ColumnName] = rowNew[col.ColumnName];
                                break;
                        }

                    }
                }



            }

        }
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable changedInkjetcustomer = new DataTable();
            DataTable changedCustomer = new DataTable();
            DataTable changedInkjetproduct = new DataTable();
            DataTable changedProduct = new DataTable();

            // 거래처정보
            changedInkjetcustomer = grdInkjetcustomer.GetChangedRows();
            if (changedInkjetcustomer != null)
            {
                if(changedInkjetcustomer.Rows.Count !=0)
                {
                   
                    ExecuteRule("InkJetCustomer", changedInkjetcustomer);
                }
                
            }
            
            // 거래처정보
            changedCustomer = grdCustomer.GetChangedRows();


            if (changedCustomer != null)
            {
                if (changedCustomer.Rows.Count !=0)
                {
                    DataTable dtCustomeridclassid = new DataTable();
                    dtCustomeridclassid.Columns.Add("IDCLASSID");
                    dtCustomeridclassid.Columns.Add("IDCLASSNAME");
                    dtCustomeridclassid.Columns.Add("ENTERPRISEID");
                    dtCustomeridclassid.Columns.Add("PLANTID");
                    dtCustomeridclassid.Columns.Add("VALIDSTATE");
                    dtCustomeridclassid.Columns.Add("_STATE_");

                    //VALIDSTATE
                    //    SEQUENCE
                    // rowItemserialI["_STATE_"] = "modified";

                    DataTable dtCustomer = (DataTable)grdCustomer.DataSource;
                    DataTable dtCustomerH = dtCustomer.Copy();

                    // 클래스 삭제
                    if (changedCustomer.Select("_STATE_ = 'deleted'").Length != 0)
                    {
                        string sIDCLASSID = "";

                        foreach (DataRow rowCustomer in changedCustomer.Select("_STATE_ = 'deleted'"))
                        {
                            sIDCLASSID = sIDCLASSID + "'" + rowCustomer["IDCLASSID"].ToString() + "'" + ",";
                        }

                      
                        if (dtCustomer.Select("IDCLASSID NOT IN (" + sIDCLASSID + ")").Length == 0)
                        {
                            DataRow rowidclassid = dtCustomeridclassid.NewRow();
                            rowidclassid["IDCLASSID"] = changedCustomer.Rows[0]["IDCLASSID"].ToString();
                            rowidclassid["IDCLASSNAME"] = changedCustomer.Rows[0]["IDCLASSID"].ToString();
                            rowidclassid["ENTERPRISEID"] = changedCustomer.Rows[0]["ENTERPRISEID"].ToString();
                            rowidclassid["PLANTID"] = changedCustomer.Rows[0]["PLANTID"].ToString();
                            rowidclassid["_STATE_"] = "deleted";
                            dtCustomeridclassid.Rows.Add(rowidclassid);
                        }
                       
                    }

                    // 클래스 등록
                    foreach (DataRow rowCustomer in changedCustomer.DefaultView.ToTable(true, new string[] { "IDCLASSID", "ENTERPRISEID", "PLANTID" }).Rows)
                    {

                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("IDCLASSID", rowCustomer["IDCLASSID"]);
                        param.Add("ENTERPRISEID", rowCustomer["ENTERPRISEID"]);
                        param.Add("PLANTID", rowCustomer["PLANTID"]);
                        DataTable dtInkJetIdClassChk = SqlExecuter.Query("GetInkJetIdClassChk", "10001", param);

                        if (dtInkJetIdClassChk != null)
                        {
                            if (dtInkJetIdClassChk.Rows.Count == 0)
                            {
                                DataRow rowidclassid = dtCustomeridclassid.NewRow();
                                rowidclassid["IDCLASSID"] = rowCustomer["IDCLASSID"];
                                rowidclassid["IDCLASSNAME"] = rowCustomer["IDCLASSID"];
                                rowidclassid["ENTERPRISEID"] = rowCustomer["ENTERPRISEID"];
                                rowidclassid["PLANTID"] = rowCustomer["PLANTID"];
                                rowidclassid["_STATE_"] = "added";
                                dtCustomeridclassid.Rows.Add(rowidclassid);
                            }
                        }
                        else
                        {
                            DataRow rowidclassid = dtCustomeridclassid.NewRow();
                            rowidclassid["IDCLASSID"] = rowCustomer["IDCLASSID"];
                            rowidclassid["IDCLASSNAME"] = rowCustomer["IDCLASSID"];
                            rowidclassid["ENTERPRISEID"] = rowCustomer["ENTERPRISEID"];
                            rowidclassid["PLANTID"] = rowCustomer["PLANTID"];
                            rowidclassid["_STATE_"] = "added";
                            dtCustomeridclassid.Rows.Add(rowidclassid);
                        }

                    }

                    DataSet dsCustomer = new DataSet();

                    dtCustomeridclassid.TableName = "idclass";
                    changedCustomer.TableName = "idclassserial";

                    dsCustomer.Tables.Add(dtCustomeridclassid);
                    dsCustomer.Tables.Add(changedCustomer);

                    ExecuteRule("InkJetIdDefinition", dsCustomer);
                }
            }


            // 품목정보       
            changedInkjetproduct = grdInkjetproduct.GetChangedRows();
            if(changedInkjetproduct != null)
            {
                if(changedInkjetproduct.Rows.Count !=0)
                {
                    ExecuteRule("InkJetProduct", changedInkjetproduct);
                }
            }

            changedProduct = grdProduct.GetChangedRows();
            if(changedProduct != null)
            {
                if(changedProduct.Rows.Count !=0)
                {

                    DataTable dtProductclassid = new DataTable();
                    dtProductclassid.Columns.Add("IDCLASSID");
                    dtProductclassid.Columns.Add("IDCLASSNAME");
                    dtProductclassid.Columns.Add("ENTERPRISEID");
                    dtProductclassid.Columns.Add("PLANTID");
                    dtProductclassid.Columns.Add("VALIDSTATE");
                    dtProductclassid.Columns.Add("_STATE_");

                    //VALIDSTATE
                    //    SEQUENCE
                    // rowItemserialI["_STATE_"] = "modified";

                    DataTable dtProduct = (DataTable)grdProduct.DataSource;

                    DataTable dtProductH = dtProduct.Copy();

                    DataView dvProductH = new DataView(dtProductH);

                    // 클래스 삭제

                    if (changedProduct.Select("_STATE_ = 'deleted'").Length != 0)
                    {
                        string sIDCLASSID = "";

                        foreach (DataRow rowProduct in changedProduct.Select("_STATE_ = 'deleted'"))
                        {
                            sIDCLASSID = sIDCLASSID + "'" + rowProduct["IDCLASSID"].ToString() + "'" + ",";
                        }


                        if (dtProduct.Select("IDCLASSID NOT IN (" + sIDCLASSID + ")").Length == 0)
                        {
                            DataRow rowidclassid = dtProductclassid.NewRow();
                            rowidclassid["IDCLASSID"] = changedProduct.Rows[0]["IDCLASSID"].ToString();
                            rowidclassid["IDCLASSNAME"] = changedProduct.Rows[0]["IDCLASSID"].ToString();
                            rowidclassid["ENTERPRISEID"] = changedProduct.Rows[0]["ENTERPRISEID"].ToString();
                            rowidclassid["PLANTID"] = changedProduct.Rows[0]["PLANTID"].ToString();
                            rowidclassid["_STATE_"] = "deleted";
                            dtProductclassid.Rows.Add(rowidclassid);
                        }

                    }



                    // 클래스 등록
                    foreach (DataRow rowProductH in dtProductH.DefaultView.ToTable(true, new string[] { "IDCLASSID", "ENTERPRISEID", "PLANTID" }).Rows)
                    {

                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("IDCLASSID", rowProductH["IDCLASSID"]);
                        param.Add("ENTERPRISEID", rowProductH["ENTERPRISEID"]);
                        param.Add("PLANTID", rowProductH["PLANTID"]);
                        DataTable dtInkJetIdClassChk = SqlExecuter.Query("GetInkJetIdClassChk", "10001", param);

                        if (dtInkJetIdClassChk != null)
                        {
                            if (dtInkJetIdClassChk.Rows.Count == 0)
                            {
                                DataRow rowidclassid = dtProductclassid.NewRow();
                                rowidclassid["IDCLASSID"] = rowProductH["IDCLASSID"];
                                rowidclassid["IDCLASSNAME"] = rowProductH["IDCLASSID"];
                                rowidclassid["ENTERPRISEID"] = rowProductH["ENTERPRISEID"];
                                rowidclassid["PLANTID"] = rowProductH["PLANTID"];
                                rowidclassid["_STATE_"] = "added";
                                dtProductclassid.Rows.Add(rowidclassid);
                            }
                        }
                        else
                        {
                            DataRow rowidclassid = dtProductclassid.NewRow();
                            rowidclassid["IDCLASSID"] = rowProductH["IDCLASSID"];
                            rowidclassid["IDCLASSNAME"] = rowProductH["IDCLASSID"];
                            rowidclassid["ENTERPRISEID"] = rowProductH["ENTERPRISEID"];
                            rowidclassid["PLANTID"] = rowProductH["PLANTID"];
                            rowidclassid["_STATE_"] = "added";
                            dtProductclassid.Rows.Add(rowidclassid);
                        }

                    }

                    DataSet dsProduct = new DataSet();

                    dtProductclassid.TableName = "idclass";
                    changedProduct.TableName = "idclassserial";

                    dsProduct.Tables.Add(dtProductclassid);
                    dsProduct.Tables.Add(changedProduct);


                    ExecuteRule("InkJetIdDefinition", dsProduct);
                }
            }
            



        }
        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

            // 조회조건에 입력된 값들을 Dictionary<string, object> Type으로 가져옴

            var values = this.Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("PLANTID", UserInfo.Current.Plant);
            //TODO : Id를 수정하세요            
            // Stored Procedure 호출
            //this.grdCodeClass.DataSource = await this.ProcedureAsync("usp_com_selectCodeClass", values);
            // Server Xml Query 호출

            DataTable dtInkjetcustomer = await SqlExecuter.QueryAsync("GetInkJetCustomer", "10001", values);

            if (dtInkjetcustomer.Rows.Count < 1) // 
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                this.grdInkjetcustomer.DataSource = dtInkjetcustomer;

                DataRow row = this.grdInkjetcustomer.View.GetFocusedDataRow();

                Dictionary<string, object> paramINKJET = new Dictionary<string, object>();
                paramINKJET.Add("P_ENTERPRISEID", row["ENTERPRISEID"].ToString());
                paramINKJET.Add("P_PLANTID", row["PLANTID"].ToString());
                paramINKJET.Add("P_IDCLASSID", row["INKJETCODE"].ToString() + row["CODETYPE"].ToString() + "INKJET");
                grdCustomer.DataSource = SqlExecuter.Query("GetInkJetIdDefList", "10001", paramINKJET);

                Dictionary<string, object> paramTEXT = new Dictionary<string, object>();
                paramTEXT.Add("P_ENTERPRISEID", row["ENTERPRISEID"].ToString());
                paramTEXT.Add("P_PLANTID", row["PLANTID"].ToString());
                paramTEXT.Add("P_IDCLASSID", row["INKJETCODE"].ToString() + row["CODETYPE"].ToString() + "TEXT");
                grdProduct.DataSource = SqlExecuter.Query("GetInkJetIdDefList", "10001", paramTEXT);
            
                Dictionary<string, object> paramInkjetproduct = new Dictionary<string, object>();
                paramInkjetproduct.Add("CUSTOMERID", row["CUSTOMERID"].ToString());
                //paramInkjetproduct.Add("INKJETCODE", row["INKJETCODE"].ToString());
                paramInkjetproduct.Add("ENTERPRISEID", row["ENTERPRISEID"].ToString());
                paramInkjetproduct.Add("PLANTID", row["PLANTID"].ToString());
                paramInkjetproduct.Add("CODETYPE",row["CODETYPE"].ToString());
                grdInkjetproduct.DataSource = SqlExecuter.Query("GetInkjetproductlist", "10001", paramInkjetproduct);

            }
            

        }
        #endregion

        #region 유효성 검사
        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            DataTable Inkjetcustomerchanged = new DataTable();
            grdInkjetcustomer.View.CheckValidation();
            Inkjetcustomerchanged = grdInkjetcustomer.GetChangedRows();

            DataTable Customerchanged = new DataTable();
            grdCustomer.View.CheckValidation();
            Customerchanged = grdCustomer.GetChangedRows();


            DataTable Inkjetproduct = new DataTable();
            grdInkjetproduct.View.CheckValidation();
            Inkjetproduct = grdInkjetproduct.GetChangedRows();


            DataTable Product = new DataTable();
            grdProduct.View.CheckValidation();
            Product = grdProduct.GetChangedRows();


            if (Inkjetcustomerchanged.Rows.Count == 0 && Customerchanged.Rows.Count == 0 && Inkjetproduct.Rows.Count == 0 && Product.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }
        #endregion

       

    }
}
