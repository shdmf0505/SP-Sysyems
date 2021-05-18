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
    /// 프 로 그 램 명  : 외주관리>외주 단가 관리>단가코드등록
    /// 업  무  설  명  : 단가코드등록등록한다.
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-07-05
    /// 수  정  이  력  : 
    /// 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcedBasedUnitPriceRegistration : SmartConditionManualBaseForm
    {
        #region Local Variables

       

        #endregion

        #region 생성자

        public OutsourcedBasedUnitPriceRegistration()
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
            grdospprice.GridButtonItem = GridButtonItem.Add| GridButtonItem.Delete | GridButtonItem.Export|GridButtonItem.Expand | GridButtonItem.Restore;
            grdospprice.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdospprice.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();                                                          //  회사 ID
            grdospprice.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();                                                           //  공장 ID
           
            
            this.InitializeGrid_ProcesssegmentclassidPopup();// 
            grdospprice.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 250)
               .SetIsReadOnly();
            //표준공정 추가 ...
            InitializeGrid_ProcesssegmentidListPopup();
            grdospprice.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 250)
             .SetIsReadOnly();
            grdospprice.View.AddTextBoxColumn("OSPPRICECODE", 100)
               .SetValidationIsRequired();
            grdospprice.View.AddTextBoxColumn("OSPPRICENAME", 150);
            grdospprice.View.AddComboBoxColumn("SPECUNIT", 100, new SqlQuery("GetUomDefinitionMapListByOsp", "10001",
                "UOMCATEGORY=OSPSPEC", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFID")
                .SetEmptyItem("","");
            grdospprice.View.AddComboBoxColumn("CALCULATEUNIT", 100, new SqlQuery("GetUomDefinitionMapListByOsp", "10001", "UOMCATEGORY=OSP", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFID")
                .SetDefault("PNL")
                .SetValidationIsRequired();
            grdospprice.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid");
            grdospprice.View.AddTextBoxColumn("DESCRIPTION", 200);
                        
            grdospprice.View.AddTextBoxColumn("CREATORNAME", 100)
                .SetIsReadOnly();
            grdospprice.View.AddTextBoxColumn("CREATEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();
            grdospprice.View.AddTextBoxColumn("MODIFIERNAME", 100)
                .SetIsReadOnly();
            grdospprice.View.AddTextBoxColumn("MODIFIEDTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetDefault("")
                .SetIsReadOnly();
            grdospprice.View.SetKeyColumn("PLANTID", "ENTERPRISEID", "OSPPRICECODE");
            //grdospprice.View.SetAutoFillColumn("DESCRIPTION");
            grdospprice.View.PopulateColumns();
        }
        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializeGrid_ProcesssegmentidListPopup()
        {
            var popupGridProcessSegments = this.grdospprice.View.AddSelectPopupColumn("PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("PROCESSSEGMENTID", "PROCESSSEGMENTID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정
                //.SetRelationIds("PROCESSSEGMENTCLASSID")
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    DataRow classRow = grdospprice.View.GetFocusedDataRow();

                    foreach (DataRow row in selectedRows)
                    {
                        classRow["PROCESSSEGMENTNAME"] = row["PROCESSSEGMENTNAME"];
                    }
                })
            //.SetPopupValidationCustom(ValidationParentAreaIdPopup);
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            popupGridProcessSegments.Conditions.AddTextBox("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT");
            popupGridProcessSegments.Conditions.AddTextBox("PROCESSSEGMENTCLASSID")
               .SetPopupDefaultByGridColumnId("PROCESSSEGMENTCLASSID")
               .SetLabel("")
               .SetIsHidden();
            // 팝업 그리드 설정
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetValidationKeyColumn();
            popupGridProcessSegments.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);


        }
        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializeGrid_ProcesssegmentclassidPopup()
        {
            var popupGridProcessSegmentclassid = this.grdospprice.View.AddSelectPopupColumn("PROCESSSEGMENTCLASSID", 
                new SqlQuery("GetProcesssegmentclassidGridListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("PROCESSSEGMENTCLASSID", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("PROCESSSEGMENTCLASSID", "PROCESSSEGMENTCLASSID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정
                .SetValidationIsRequired()
                .SetPopupAutoFillColumns("PROCESSSEGMENTCLASSNAME")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    DataRow classRow = grdospprice.View.GetFocusedDataRow();

                    foreach (DataRow row in selectedRows)
                    {
                        classRow["PROCESSSEGMENTCLASSNAME"] = row["PROCESSSEGMENTCLASSNAME"];
                    }
                })
            ;
            // 팝업에서 사용할 조회조건 항목 추가
            popupGridProcessSegmentclassid.Conditions.AddTextBox("PROCESSSEGMENTCLASSNAME");

            // 팝업 그리드 설정
            popupGridProcessSegmentclassid.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150);
            popupGridProcessSegmentclassid.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200);


        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdospprice.View.AddingNewRow += View_AddingNewRow;
            grdospprice.View.CellValueChanged += View_CellValueChanged;
        }
        /// <summary>
        /// 거래유형 코드 그리드  유형코드 대문자로 처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "OSPPRICECODE")
            {
                grdospprice.View.CellValueChanged -= View_CellValueChanged;

                DataRow row = grdospprice.View.GetFocusedDataRow();

                string strTransactioncode = row["OSPPRICECODE"].ToString();

                grdospprice.View.SetFocusedRowCellValue("OSPPRICECODE", strTransactioncode.ToUpper());// 
                grdospprice.View.CellValueChanged += View_CellValueChanged;

            }
        }
        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            var values = Conditions.GetValues();
            string _strPlantid = values["P_PLANTID"].ToString();
            grdospprice.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdospprice.View.SetFocusedRowCellValue("PLANTID", _strPlantid);// plantid
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
            //DataTable changed = grdospprice.GetChangedRows();

            //ExecuteRule("OutsourcedBasedUnitPriceRegistration", changed);
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
            values.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise);
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtBasedPrice = await SqlExecuter.QueryAsync("GetOutsourcedBasedUnitPriceRegistration", "10001", values);

            if (dtBasedPrice.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdospprice.DataSource = dtBasedPrice;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // site
            // InitializeConditionPopup_Plant();
            // 중공정 
            InitializeConditionPopup_Processsegmentclassid();
            // 표준공정
            InitializeConditionPopup_Processsegmentid();
            //외주단가코드 ,외주단가명 
            InitializeConditionPopup_Ospprice();
            //유효상태 
            InitializeConditionPopup_ValidState();
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
            var processsegmentclassidPopupColumn = Conditions.AddSelectPopup("p_processsegmentclassid", new SqlQuery("GetProcesssegmentclassidListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}",$"ENTERPRISEID={UserInfo.Current.Enterprise}"), "MIDDLEPROCESSSEGMENTCLASSNAME", "MIDDLEPROCESSSEGMENTCLASSID")
               .SetPopupLayout("MIDDLEPROCESSSEGMENTCLASS", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("MIDDLEPROCESSSEGMENTCLASS")
               .SetPopupResultCount(1)
               .SetPosition(0.3);

            // 팝업 조회조건
            processsegmentclassidPopupColumn.Conditions.AddTextBox("MIDDLEPROCESSSEGMENTCLASSNAME")
                .SetLabel("MIDDLEPROCESSSEGMENTCLASSNAME");

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
                .SetRelationIds("P_PROCESSSEGMENTCLASSID")
                .SetPosition(0.4);

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
               .SetPopupResultCount(1)
               .SetRelationIds("p_plantid","P_PROCESSSEGMENTCLASSID", "p_processsegmentid") 
               .SetPosition(0.6);
            // 팝업 조회조건
            txtOsppricecode.Conditions.AddTextBox("OSPPRICENAME")
                .SetLabel("OSPPRICENAME");
            txtOsppricecode.GridColumns.AddTextBoxColumn("OSPPRICECODE", 100)
              .SetValidationKeyColumn();
            txtOsppricecode.GridColumns.AddTextBoxColumn("OSPPRICENAME", 150);

        }
        /// <summary>
        ///  유효상태
        /// </summary>
        private void InitializeConditionPopup_ValidState()
        {
            var plantCbobox = Conditions.AddComboBox("p_validstate", new SqlQuery("GetCodeList", "00001", new Dictionary<string, object>() { { "CODECLASSID", "ValidState" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "CODENAME", "CODEID")
              .SetLabel("VALIDSTATE")
              .SetPosition(0.5)
              .SetEmptyItem("","");

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

            // TODO : 유효성 로직 변경
            grdospprice.View.CheckValidation();
            
            DataTable changed = grdospprice.GetChangedRows();

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
            grdospprice.View.FocusedRowHandle = grdospprice.View.FocusedRowHandle;
            grdospprice.View.FocusedColumn = grdospprice.View.Columns["OSPPRICECODE"];
            grdospprice.View.ShowEditor();
            // TODO : 유효성 로직 변경
            grdospprice.View.CheckValidation();

            DataTable changed = grdospprice.GetChangedRows();

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
                    DataTable dtSave = grdospprice.GetChangedRows();

                    ExecuteRule("OutsourcedBasedUnitPriceRegistration", dtSave);
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
            values.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise);
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtBasedPrice = SqlExecuter.Query("GetOutsourcedBasedUnitPriceRegistration", "10001", values);

            if (dtBasedPrice.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdospprice.DataSource = dtBasedPrice;

        }
        #endregion
    }
}
