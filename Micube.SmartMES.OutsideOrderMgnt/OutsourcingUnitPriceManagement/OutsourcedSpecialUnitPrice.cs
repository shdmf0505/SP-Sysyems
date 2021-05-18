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
    /// 프 로 그 램 명  :  외주관리> 외주 단가 관리>Special 단가 등록
    /// 업  무  설  명  : Special 단가 등록한다.
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-07-05
    /// 수  정  이  력  : 
    /// 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcedSpecialUnitPrice : SmartConditionManualBaseForm
    {
        #region Local Variables

        private string _strPlantid = ""; // plant 변경시 작업 


        #endregion

        #region 생성자

        public OutsourcedSpecialUnitPrice()
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
            _strPlantid = UserInfo.Current.Plant.ToString();
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
          
            grdSpecial.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdSpecial.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdSpecial.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();                                                          //  회사 ID
            grdSpecial.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();                                                      //  공장 ID
            grdSpecial.View.AddTextBoxColumn("OSPVENDORID", 200)
                .SetIsHidden();
            grdSpecial.View.AddTextBoxColumn("MODELID", 200)
              .SetIsHidden();
          
            InitializeGrid_ProcesssegmentidListPopup(); //공정코드
            grdSpecial.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200)
                .SetIsReadOnly();
            InitializeGrid_AreaPopup();//협력사
            grdSpecial.View.AddTextBoxColumn("AREANAME", 200)
                .SetIsReadOnly();
            InitializeGrid_ProductdefidListPopup(); //품목코드 
            grdSpecial.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();
            grdSpecial.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                .SetIsReadOnly();
            grdSpecial.View.AddDateEditColumn("STARTDATE", 100)
                 // 필수 입력 컬럼 지정
                .SetValidationIsRequired()
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime);                              //시작일 
            grdSpecial.View.AddDateEditColumn("ENDDATE", 100)
               // 필수 입력 컬럼 지정
                .SetValidationIsRequired()
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime);                              //종료일  
            grdSpecial.View.AddSpinEditColumn("REDUCERATE", 100)
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);                                //인하율
            grdSpecial.View.AddTextBoxColumn("DESCRIPTION", 200);
            grdSpecial.View.AddTextBoxColumn("CREATORNAME", 100)
                .SetIsReadOnly();
            grdSpecial.View.AddTextBoxColumn("CREATEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();
            grdSpecial.View.AddTextBoxColumn("MODIFIERNAME", 100)
                .SetIsReadOnly();
            grdSpecial.View.AddTextBoxColumn("MODIFIEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();
            ///grdSpecial.View.SetAutoFillColumn("DESCRIPTION");

            grdSpecial.View.SetKeyColumn("PLANTID", "ENTERPRISEID", "STARTDATE", "PROCESSSEGMENTID", "AREAID", "PRODUCTDEFID","PRODUCTDEFVERSION");
            grdSpecial.View.PopulateColumns();
        }


        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializeGrid_ProcesssegmentidListPopup()
        {
            var popupGridProcessSegments = this.grdSpecial.View.AddSelectPopupColumn("PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("PROCESSSEGMENTID", "PROCESSSEGMENTID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정

                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    DataRow classRow = grdSpecial.View.GetFocusedDataRow();

                    foreach (DataRow row in selectedRows)
                    {
                        classRow["PROCESSSEGMENTNAME"] = row["PROCESSSEGMENTNAME"];
                    }
                })
           
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            popupGridProcessSegments.Conditions.AddTextBox("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT");

            // 팝업 그리드 설정
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetValidationKeyColumn();
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);


        }
        /// <summary>
        /// InitializeGrid_AreaPopup
        /// </summary>
        private void InitializeGrid_AreaPopup()
        {
            var popupGridProcessSegments = this.grdSpecial.View.AddSelectPopupColumn("AREAID",
                new SqlQuery("GetAreaidPopupListByOsp", "10001"
                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                , $"P_PLANTID={_strPlantid}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)

                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("AREAID", "AREAID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정


                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    DataRow classRow = grdSpecial.View.GetFocusedDataRow();

                    foreach (DataRow row in selectedRows)
                    {
                        classRow["AREANAME"] = row["AREANAME"];
                    }
                })
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            popupGridProcessSegments.Conditions.AddTextBox("P_AREANAME")
                .SetLabel("AREANAME");

            // 팝업 그리드 설정
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetValidationKeyColumn();
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }
        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializeGrid_OspVendoridListPopup()
        {
            // 팝업 컬럼설정
            var popupGridOspVendorid = this.grdSpecial.View.AddSelectPopupColumn("OSPVENDORID", new SqlQuery("GetVendorListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("OSPVENDORID", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("OSPVENDORID", "OSPVENDORID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정
                .SetRelationIds("ENTERPRISEID", "PLANTID")
                .SetPopupAutoFillColumns("OSPVENDORNAME")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    DataRow classRow = grdSpecial.View.GetFocusedDataRow();

                    foreach (DataRow row in selectedRows)
                    {
                        classRow["OSPVENDORNAME"] = row["OSPVENDORNAME"];
                    }
                })
           
            ;
            // 팝업 조회조건
            popupGridOspVendorid.Conditions.AddTextBox("OSPVENDORNAME")
                .SetLabel("OSPVENDORNAME");
            popupGridOspVendorid.Conditions.AddTextBox("PLANTID")
              .SetPopupDefaultByGridColumnId("PLANTID")
              .SetLabel("")
              .SetIsHidden();
            // 팝업 그리드
            popupGridOspVendorid.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            popupGridOspVendorid.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);

           

        }
        /// <summary>
        /// grid InitializeGrid_ProductdefidListPopup
        /// </summary>
        private void InitializeGrid_ProductdefidListPopup()
        {
            var popupGridProductdefid = this.grdSpecial.View.AddSelectPopupColumn("PRODUCTDEFID",120, new SqlQuery("GetProductdefidlistByOsp", "10001"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("PRODUCTDEFID", "PRODUCTDEFID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(650, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정
                .SetValidationIsRequired()
                .SetRelationIds("ENTERPRISEID", "PLANTID")
                .SetPopupAutoFillColumns("PRODUCTDEFNAME")
          
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    DataRow classRow = grdSpecial.View.GetFocusedDataRow();

                    foreach (DataRow row in selectedRows)
                    {
                        classRow["PRODUCTDEFVERSION"] = row["PRODUCTDEFVERSION"];
                        classRow["PRODUCTDEFNAME"] = row["PRODUCTDEFNAME"];
                    }
                })

            ;
            popupGridProductdefid.Conditions.AddComboBox("P_PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTDEFTYPE")
                .SetDefault("Product");

            popupGridProductdefid.Conditions.AddTextBox("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFID");
            popupGridProductdefid.GridColumns.AddTextBoxColumn("PRODUCTDEFCODE", 150)
                .SetLabel("")
                .SetIsHidden();
            popupGridProductdefid.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            popupGridProductdefid.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150).SetIsReadOnly();
            popupGridProductdefid.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsReadOnly();
            popupGridProductdefid.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();

            


        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdSpecial.View.AddingNewRow += View_AddingNewRow;

            grdSpecial.View.CellValueChanged += View_CellValueChanged;
        }

        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            var values = Conditions.GetValues();
            string sPlantid = values["P_PLANTID"].ToString();
            grdSpecial.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdSpecial.View.SetFocusedRowCellValue("PLANTID", sPlantid);// plantid
            grdSpecial.View.SetFocusedRowCellValue("PROCESSSEGMENTID", "*");// 
            grdSpecial.View.SetFocusedRowCellValue("PROCESSSEGMENTNAME", "*");// 
            grdSpecial.View.SetFocusedRowCellValue("AREAID", "*");// 
            grdSpecial.View.SetFocusedRowCellValue("AREANAME", "*");// 
            grdSpecial.View.SetFocusedRowCellValue("PRODUCTDEFID", "*");// 
            grdSpecial.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", "*");// 
            grdSpecial.View.SetFocusedRowCellValue("PRODUCTDEFNAME", "*");// 
            grdSpecial.View.SetFocusedRowCellValue("OSPVENDORID", "*");//   
            grdSpecial.View.SetFocusedRowCellValue("MODELID", "*");// 
            DateTime dateNow = DateTime.Now;
            grdSpecial.View.SetFocusedRowCellValue("STARTDATE", dateNow.ToString("yyyy-MM-dd"));//시작일자
            grdSpecial.View.SetFocusedRowCellValue("ENDDATE", "9999-12-31");//시작일자
        }

        /// <summary>
        /// 일자 그리드 포맷 변경 처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.Column.FieldName == "STARTDATE")
            {
                grdSpecial.View.CellValueChanged -= View_CellValueChanged;

                DataRow row = grdSpecial.View.GetFocusedDataRow();

                if (row["STARTDATE"].ToString().Equals(""))
                {
                    grdSpecial.View.CellValueChanged += View_CellValueChanged;
                    return;
                }
                DateTime dateBudget = Convert.ToDateTime(row["STARTDATE"].ToString());
                grdSpecial.View.SetFocusedRowCellValue("STARTDATE", dateBudget.ToString("yyyy-MM-dd"));// 예산일자
                grdSpecial.View.CellValueChanged += View_CellValueChanged;
            }
            if (e.Column.FieldName == "ENDDATE")
            {
                grdSpecial.View.CellValueChanged -= View_CellValueChanged;

                DataRow row = grdSpecial.View.GetFocusedDataRow();

                if (row["ENDDATE"].ToString().Equals(""))
                {
                    grdSpecial.View.CellValueChanged += View_CellValueChanged;
                    return;
                }
                DateTime dateBudget = Convert.ToDateTime(row["ENDDATE"].ToString());
                grdSpecial.View.SetFocusedRowCellValue("ENDDATE", dateBudget.ToString("yyyy-MM-dd"));// 예산일자
                grdSpecial.View.CellValueChanged += View_CellValueChanged;
            }

        }
        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            //// TODO : 저장 Rule 변경
            //DataTable changed = grdSpecial.GetChangedRows();

            //ExecuteRule("OutsourcedSpecialUnitPrice", changed);
        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
            {

                ProcSave(btn.Text);
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
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtSpecial = await SqlExecuter.QueryAsync("GetOutsourcedSpecialUnitPrice", "10001", values);

            if (dtSpecial.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdSpecial.DataSource = dtSpecial;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //site
           // InitializeConditionPopup_Plant();
            //공정
            InitializeConditionPopup_ProcessSegment();

            InitializeConditionPopup_OspAreaid();
            //// 작업업체 
            //InitializeConditionPopup_OspVendorid();

            // 품목코드 
            InitializeConditionPopup_ProductDefId();

            //site
            //공정
            //협력사
            //품목코드

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
               .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
            ;
           
        }

        /// <summary>
        /// ProcessSegment 설정 
        /// </summary>
        private void InitializeConditionPopup_ProcessSegment()
        {
            var popupProcessSegment = Conditions.AddSelectPopup("p_processsegmentid",
                                                               new SqlQuery("GetProcessSegmentListByOsp", "10001"
                                                                               , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                               , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                               ), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
            .SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(500, 600)
            .SetLabel("PROCESSSEGMENTID")
            .SetPopupResultCount(1)
            .SetPosition(1.0);

            // 팝업 조회조건
            popupProcessSegment.Conditions.AddTextBox("PROCESSSEGMENTNAME")
               .SetLabel("PROCESSSEGMENT");

            // 팝업 그리드
            popupProcessSegment.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetValidationKeyColumn();
            popupProcessSegment.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

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
               .SetPosition(2.0);

            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("OSPVENDORNAME")
                .SetLabel("OSPVENDORNAME");
           
            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);

            var txtOspVendor = Conditions.AddTextBox("P_OSPVENDORNAME")
               .SetLabel("OSPVENDORNAME")
               .SetPosition(2.1);

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

            var txtProductName = Conditions.AddTextBox("P_PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFNAME")
                .SetPosition(3.2);
        }
        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            SmartComboBox cboPlaint = Conditions.GetControl<SmartComboBox>("p_plantid");
            cboPlaint.EditValueChanged += cboPlaint_EditValueChanged;
        }
        private void cboPlaint_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();

            _strPlantid = values["P_PLANTID"].ToString();

        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            grdSpecial.View.CheckValidation();

            DataTable changed = grdSpecial.GetChangedRows();

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
        private void ProcSave(string strtitle)
        {
            grdSpecial.View.FocusedRowHandle = grdSpecial.View.FocusedRowHandle;
            grdSpecial.View.FocusedColumn = grdSpecial.View.Columns["OSPPRICECODE"];
            grdSpecial.View.ShowEditor();
          
            grdSpecial.View.CheckValidation();
            //품목코드 null 값 대체(*) 처리
            SetNullReplaceCombination();
            DataTable changed = grdSpecial.GetChangedRows();
            

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
                    DataTable dtSave = grdSpecial.GetChangedRows();

                    ExecuteRule("OutsourcedSpecialUnitPrice", dtSave);
                 
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
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtSpecial =  SqlExecuter.Query("GetOutsourcedSpecialUnitPrice", "10001", values);

            if (dtSpecial.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdSpecial.DataSource = dtSpecial;

        }

        /// <summary>
        /// 외주 단가 기준에 협력사 품목코드 null 값 대체(*) 처리 
        /// </summary>
        /// <param name="strRequestno"></param>
        private void SetNullReplaceCombination()
        {

            if (grdSpecial.View.DataRowCount == 0)
            {
                return;
            }
            for (int irow = 0; irow < grdSpecial.View.DataRowCount; irow++)
            {
                string sproductdefid = grdSpecial.View.GetRowCellValue(irow, "PRODUCTDEFID").ToString();

                if (sproductdefid.Equals(""))
                {
                    grdSpecial.View.SetRowCellValue(irow, "PRODUCTDEFID", "*");// 
                    grdSpecial.View.SetRowCellValue(irow, "PRODUCTDEFVERSION", "*");//
                    grdSpecial.View.SetRowCellValue(irow, "PRODUCTDEFNAME", "*");//

                }
                string strareaid = grdSpecial.View.GetRowCellValue(irow, "AREAID").ToString();

                if (strareaid.Equals(""))
                {
                    grdSpecial.View.SetFocusedRowCellValue("AREAID", "*");// 
                    grdSpecial.View.SetFocusedRowCellValue("AREANAME", "*");//
                }

            }
        }
        #endregion
    }
}
