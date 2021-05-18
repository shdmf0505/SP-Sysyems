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
    /// 프 로 그 램 명  :  외주관리 > 외주 예산 관리 >외주비 예산 등록
    /// 업  무  설  명  :  외주비 예산 등록한다..
    /// 생    성    자  : choisstar
    /// 생    성    일  : 2019-06-24
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcingExceptBudgetRegister : SmartConditionManualBaseForm
    {
        #region Local Variables

        private string _strPlantid = ""; // plant 변경시 작업 

        #endregion

        #region 생성자

        public OutsourcingExceptBudgetRegister()
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
            // 작업용 plant 셋팅 (조회시 다시 셋팅)
            _strPlantid = UserInfo.Current.Plant.ToString();
            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdOutsourcingExceptBudgetRegister.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdOutsourcingExceptBudgetRegister.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdOutsourcingExceptBudgetRegister.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();                                                          //  회사 ID
            grdOutsourcingExceptBudgetRegister.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();                                                           //  공장 ID
            grdOutsourcingExceptBudgetRegister.View.AddTextBoxColumn("SEQUENCE", 200)
                .SetIsHidden();                                                           // SEQUENCE
            grdOutsourcingExceptBudgetRegister.View.AddComboBoxColumn("EXCEPTTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ExceptType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired()
                // 행추가 시 기본값 지정
                .SetDefault("Process");   //   공정제외 유형 
            this.InitializeGrid_ExceptidListPopup();// 
            grdOutsourcingExceptBudgetRegister.View.AddTextBoxColumn("EXCEPTVERSION",100)
                .SetIsReadOnly();                                                  //  공정명 
            grdOutsourcingExceptBudgetRegister.View.AddTextBoxColumn("EXCEPTNAME", 250)
                .SetIsReadOnly();                                                  //  공정명 

            grdOutsourcingExceptBudgetRegister.View.AddDateEditColumn("STARTDATE", 100)
               // 필수 입력 컬럼 지정
               .SetValidationIsRequired()
               .SetDisplayFormat("yyyy-MM-dd",MaskTypes.DateTime);                 //  시작일
            grdOutsourcingExceptBudgetRegister.View.AddDateEditColumn("ENDDATE", 100)
              .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime);                 //  종료일

            grdOutsourcingExceptBudgetRegister.View.AddTextBoxColumn("DESCRIPTION", 200);// 설명
            grdOutsourcingExceptBudgetRegister.View.AddTextBoxColumn("CREATORNAME", 100)
              .SetIsReadOnly();

            grdOutsourcingExceptBudgetRegister.View.AddTextBoxColumn("CREATEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();
            grdOutsourcingExceptBudgetRegister.View.AddTextBoxColumn("MODIFIERNAME", 100)
                .SetIsReadOnly();
            grdOutsourcingExceptBudgetRegister.View.AddTextBoxColumn("MODIFIEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();
           // grdOutsourcingExceptBudgetRegister.View.SetAutoFillColumn("DESCRIPTION");// 그리드의 남은 영역을 채울 컬럼 설정
            grdOutsourcingExceptBudgetRegister.View.SetKeyColumn("PLANTID", "ENTERPRISEID", "EXCEPTTYPE", "EXCEPTID",  "STARTDATE");

            grdOutsourcingExceptBudgetRegister.View.PopulateColumns();
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
                 .SetPosition(2.3);
            // 팝업 조회조건
            popupProduct.Conditions.AddComboBox("P_PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTDEFTYPE")
                .SetDefault("Product");
           
            popupProduct.Conditions.AddTextBox("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFID");

            // 팝업 그리드
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFCODE", 150)
                .SetIsHidden();
            popupProduct.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150).SetIsReadOnly();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsReadOnly();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();

            var txtProductName = Conditions.AddTextBox("P_PRODUCTDEFNAME")
               .SetLabel("PRODUCTDEFNAME")
               .SetPosition(2.4);

        }


        /// <summary>
        /// ProcessSegment 설정 
        /// </summary>
        private void InitializeConditionPopup_ProcessSegment()
        {
            var ProcessSegmentPopupColumn = Conditions.AddSelectPopup("p_processsegmentid",
                                                               new SqlQuery("GetProcessSegmentListByOsp", "10001"
                                                                               , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                               , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                               ), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
            .SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(500, 600)
            .SetLabel("PROCESSSEGMENTID")
            .SetPopupResultCount(1)
            .SetPosition(2.3);

            // 팝업 조회조건
            ProcessSegmentPopupColumn.Conditions.AddTextBox("PROCESSSEGMENTNAME")
                  .SetLabel("PROCESSSEGMENT");

            // 팝업 그리드
            ProcessSegmentPopupColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetValidationKeyColumn();
            ProcessSegmentPopupColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

        }

        /// <summary>
        /// grid ExceptidListPopup
        /// </summary>
        private void InitializeGrid_ExceptidListPopup()
        {
            var popupGridExceptid = this.grdOutsourcingExceptBudgetRegister.View.AddSelectPopupColumn("EXCEPTID", new SqlQuery("GetExcepttypeExceptidByOsp", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("EXCEPTID", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(0)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                //.SetPopupResultMapping("EXCEPTID", "EXCEPTID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정
                .SetValidationIsRequired()
                .SetRelationIds("EXCEPTTYPE", "ENTERPRISEID","PLANTID")
                .SetPopupAutoFillColumns("EXCEPTNAME")
                .SetPopupQueryPopup((DataRow currentrow) =>
                   {
                       if (string.IsNullOrWhiteSpace(currentrow.ToStringNullToEmpty("EXCEPTTYPE")))
                       {
                           this.ShowMessage("NoSelectSite");
                           return false;
                       }

                       return true;
                   })
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    int irow = 0;
                    int crow = 0;
                    DataRow classRow = grdOutsourcingExceptBudgetRegister.View.GetFocusedDataRow();
                    crow = grdOutsourcingExceptBudgetRegister.View.FocusedRowHandle;
                    string Excepttype = classRow["EXCEPTTYPE"].ToString();
                  
                    foreach (DataRow row in selectedRows)
                    {
                        if (irow == 0)
                        {
                            string sProduct = row["EXCEPTID"].ToString();
                            string sProductVer = row["EXCEPTVERSION"].ToString();
                            int icheck = checkGridProduct(sProduct, sProductVer);
                            if (icheck == -1)
                            {
                                classRow["EXCEPTID"] = row["EXCEPTID"];
                                classRow["EXCEPTVERSION"] = row["EXCEPTVERSION"];
                                classRow["EXCEPTNAME"] = row["EXCEPTNAME"];
                                grdOutsourcingExceptBudgetRegister.View.RaiseValidateRow(crow);
                            }
                            else
                            {
                                classRow["EXCEPTID"] ="";
                                irow = irow - 1;

                            }
                        }
                        else
                        {
                            popAddGrid(Excepttype, row);
                        }
                        irow = irow + 1;

                       
                    }
                    
                })
           
            ;
            // 팝업에서 사용할 조회조건 항목 추가   .SetRelationIds("EXCEPTTYPE", "ENTERPRISEID","PLANTID")
            popupGridExceptid.Conditions.AddTextBox("EXCEPTNAME");
            popupGridExceptid.Conditions.AddTextBox("ENTERPRISEID")
                .SetPopupDefaultByGridColumnId("ENTERPRISEID")
                .SetLabel("")
                .SetIsHidden();
            popupGridExceptid.Conditions.AddTextBox("PLANTID")
                .SetPopupDefaultByGridColumnId("PLANTID")
                .SetLabel("")
                .SetIsHidden();
            popupGridExceptid.Conditions.AddTextBox("EXCEPTTYPE")
               .SetPopupDefaultByGridColumnId("EXCEPTTYPE")
               .SetLabel("")
               .SetIsHidden();
            // 팝업 그리드 설정
            popupGridExceptid.GridColumns.AddTextBoxColumn("EXCEPTID", 150)
                .SetValidationKeyColumn();
            popupGridExceptid.GridColumns.AddTextBoxColumn("EXCEPTVERSION", 50)
                .SetValidationKeyColumn();
            popupGridExceptid.GridColumns.AddTextBoxColumn("EXCEPTNAME", 200);


        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdOutsourcingExceptBudgetRegister.View.AddingNewRow += View_AddingNewRow;

            grdOutsourcingExceptBudgetRegister.View.CellValueChanged += View_CellValueChanged;

           
        }

        /// <summary>
        /// 예산일자 그리드 포맷 변경 처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.Column.FieldName == "STARTDATE")
            {
                grdOutsourcingExceptBudgetRegister.View.CellValueChanged -= View_CellValueChanged;

                DataRow row = grdOutsourcingExceptBudgetRegister.View.GetFocusedDataRow();
                if (row["STARTDATE"].ToString().Equals(""))
                {
                    grdOutsourcingExceptBudgetRegister.View.CellValueChanged += View_CellValueChanged;
                    return;
                }
                DateTime dateBudget = Convert.ToDateTime(row["STARTDATE"].ToString());
                grdOutsourcingExceptBudgetRegister.View.SetFocusedRowCellValue("STARTDATE", dateBudget.ToString("yyyy-MM-dd"));// 예산일자
                grdOutsourcingExceptBudgetRegister.View.CellValueChanged += View_CellValueChanged;
            }
            if (e.Column.FieldName == "ENDDATE")
            {
                grdOutsourcingExceptBudgetRegister.View.CellValueChanged -= View_CellValueChanged;

                DataRow row = grdOutsourcingExceptBudgetRegister.View.GetFocusedDataRow();
                if (row["ENDDATE"].ToString().Equals(""))
                {
                    grdOutsourcingExceptBudgetRegister.View.CellValueChanged += View_CellValueChanged;
                    return;
                }
                DateTime dateBudget = Convert.ToDateTime(row["ENDDATE"].ToString());
                grdOutsourcingExceptBudgetRegister.View.SetFocusedRowCellValue("ENDDATE", dateBudget.ToString("yyyy-MM-dd"));// 예산일자
                grdOutsourcingExceptBudgetRegister.View.CellValueChanged += View_CellValueChanged;
            }

        }
        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
           
            // 화사id  ,site id 추가  "PLANTID", "ENTERPRISEID",
            DateTime dateNow = DateTime.Now;
            var values = Conditions.GetValues();
            string _strPlantid = values["P_PLANTID"].ToString();
            grdOutsourcingExceptBudgetRegister.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdOutsourcingExceptBudgetRegister.View.SetFocusedRowCellValue("PLANTID", _strPlantid);// plantid
            grdOutsourcingExceptBudgetRegister.View.SetFocusedRowCellValue("STARTDATE", dateNow.ToString("yyyy-MM-dd"));// 시작일
            grdOutsourcingExceptBudgetRegister.View.SetFocusedRowCellValue("ENDDATE", "9999-12-31");// 종료일
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            
            ////base.OnToolbarSaveClick();

            ////// TODO : 저장 Rule 변경
            ////DataTable changed = grdOutsourcingExceptBudgetRegister.GetChangedRows();

            ////ExecuteRule("OutsourcingExceptBudgetRegister", changed);
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

            // TODO : 조회 SP 변경 p_exceptperiod
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
            if (!(values["P_EXCEPTDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_EXCEPTDATE_PERIODFR"]);
                values.Remove("P_EXCEPTDATE_PERIODFR");
                values.Add("P_EXCEPTDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_EXCEPTDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_EXCEPTDATE_PERIODTO"]);
                values.Remove("P_EXCEPTDATE_PERIODTO");
                values.Add("P_EXCEPTDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }

            #endregion

            _strPlantid = values["P_PLANTID"].ToString();
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtExceptBudgetEexpenses = await SqlExecuter.QueryAsync("GetOutsourcingExceptBudgetRegister", "10001", values);

            if (dtExceptBudgetEexpenses.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdOutsourcingExceptBudgetRegister.DataSource = dtExceptBudgetEexpenses;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            //plant
            //InitializeConditionPopup_Plant();
            //ProcessSegment
            InitializeConditionPopup_ProcessSegment();
            // 제품코드
            InitializeConditionPopup_ProductDefId();

        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();


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
            grdOutsourcingExceptBudgetRegister.View.CheckValidation();

          
            DataTable changed = grdOutsourcingExceptBudgetRegister.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
            for (int irow = 0; irow < changed.Rows.Count; irow++)
            {
                DataRow row = changed.Rows[irow];
                DateTime StartDate = Convert.ToDateTime(row["STARTDATE"]);
                DateTime EndDate = Convert.ToDateTime(row["ENDDATE"]);
                if (StartDate > EndDate)
                {
                    // 다국어 메세지 처리 (종료일 보다 시작일이 자료가 존재합니다.)
                    throw MessageException.Create("OspCheckStartEnd");
                }

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
        private void popAddGrid(string strExcepttype, DataRow dr)
        {
            DateTime dateNow = DateTime.Now;
            if (strExcepttype.Equals("Process"))
            {
                string sProcessd = dr["EXCEPTID"].ToString();
                int icheck = checkGridProcess(sProcessd);
                if (icheck == -1)
                {
                    grdOutsourcingExceptBudgetRegister.View.AddNewRow();
                    int irow = grdOutsourcingExceptBudgetRegister.View.RowCount - 1;
                    DataRow classRow = grdOutsourcingExceptBudgetRegister.View.GetDataRow(irow);
                   
                    classRow["EXCEPTTYPE"] = strExcepttype;
                    classRow["EXCEPTID"] = dr["EXCEPTID"];
                    classRow["EXCEPTVERSION"] = dr["EXCEPTVERSION"];
                    classRow["EXCEPTNAME"] = dr["EXCEPTNAME"];
                    grdOutsourcingExceptBudgetRegister.View.RaiseValidateRow(irow);
                }
            }
            else
            {
                string sProduct = dr["EXCEPTID"].ToString();
                string sProductVer = dr["EXCEPTVERSION"].ToString();
                int icheck = checkGridProduct(sProduct , sProductVer);
                if (icheck == -1)
                {
                    grdOutsourcingExceptBudgetRegister.View.AddNewRow();
                    int irow = grdOutsourcingExceptBudgetRegister.View.RowCount - 1;
                    DataRow classRow = grdOutsourcingExceptBudgetRegister.View.GetDataRow(irow);
                   
                    classRow["EXCEPTTYPE"] = strExcepttype;
                    classRow["EXCEPTID"] = dr["EXCEPTID"];
                    classRow["EXCEPTVERSION"] = dr["EXCEPTVERSION"];
                    classRow["EXCEPTNAME"] = dr["EXCEPTNAME"];
                    grdOutsourcingExceptBudgetRegister.View.RaiseValidateRow(irow);
                }
            }
            //유무 체크 
           
        }
        /// <summary>
        /// Process 체크 처리 
        /// </summary>
        /// <param name="sLotId"></param>
        /// <returns></returns>
        private int checkGridProcess(string sProcessd)
        {

            for (int i = 0; i < grdOutsourcingExceptBudgetRegister.View.DataRowCount; i++)
            {
                if (grdOutsourcingExceptBudgetRegister.View.GetRowCellValue(i, "EXCEPTID").ToString().Equals(sProcessd))
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Process 체크 처리 
        /// </summary>
        /// <param name="sLotId"></param>
        /// <returns></returns>
        private int checkGridProduct(string sProduct ,string sProductVer)
        {

            for (int i = 0; i < grdOutsourcingExceptBudgetRegister.View.DataRowCount; i++)
            {
                if (grdOutsourcingExceptBudgetRegister.View.GetRowCellValue(i, "EXCEPTID").ToString().Equals(sProduct) && grdOutsourcingExceptBudgetRegister.View.GetRowCellValue(i, "EXCEPTVERSION").ToString().Equals(sProductVer))
                {
                    return i;
                }
            }
            return -1;
        }

        private void ProcSave(string strtitle)
        {
            grdOutsourcingExceptBudgetRegister.View.FocusedRowHandle = grdOutsourcingExceptBudgetRegister.View.FocusedRowHandle;
            grdOutsourcingExceptBudgetRegister.View.FocusedColumn = grdOutsourcingExceptBudgetRegister.View.Columns["EXCEPTVERSION"];
            grdOutsourcingExceptBudgetRegister.View.ShowEditor();
            // TODO : 유효성 로직 변경
            grdOutsourcingExceptBudgetRegister.View.CheckValidation();

            grdOutsourcingExceptBudgetRegister.View.CheckValidation();


            DataTable changed = grdOutsourcingExceptBudgetRegister.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                return;
            }
            for (int irow = 0; irow < changed.Rows.Count; irow++)
            {
                DataRow row = changed.Rows[irow];
                DateTime StartDate = Convert.ToDateTime(row["STARTDATE"]);
                DateTime EndDate = Convert.ToDateTime(row["ENDDATE"]);
                if (StartDate > EndDate)
                {
                    // 다국어 메세지 처리 (종료일 보다 시작일이 자료가 존재합니다.)
                    this.ShowMessage(MessageBoxButtons.OK, "OspCheckStartEnd");
                    return;
                }

            }
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    DataTable dtSave = grdOutsourcingExceptBudgetRegister.GetChangedRows();

                    ExecuteRule("OutsourcingExceptBudgetRegister", dtSave);
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
            if (!(values["P_EXCEPTDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_EXCEPTDATE_PERIODFR"]);
                values.Remove("P_EXCEPTDATE_PERIODFR");
                values.Add("P_EXCEPTDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_EXCEPTDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_EXCEPTDATE_PERIODTO"]);
                values.Remove("P_EXCEPTDATE_PERIODTO");
                values.Add("P_EXCEPTDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }

            #endregion

            _strPlantid = values["P_PLANTID"].ToString();
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtExceptBudgetEexpenses =  SqlExecuter.Query("GetOutsourcingExceptBudgetRegister", "10001", values);

            if (dtExceptBudgetEexpenses.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdOutsourcingExceptBudgetRegister.DataSource = dtExceptBudgetEexpenses;

        }
        #endregion
    }
}
