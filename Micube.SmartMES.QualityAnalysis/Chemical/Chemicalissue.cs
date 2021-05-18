#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.QualityAnalysis;
using Micube.SmartMES.QualityAnalysis.Popup;
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
using DevExpress.XtraGrid.Views.Grid;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 약품관리 > 약품분석 이상발생
    /// 업  무  설  명  : 정기건이면서 Spec Out이거나 재분석건이면서 Spec Out이 발생한 약품분석항목들을 관리한다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-05-30
    /// 수  정  이  력  : 2019-08-06 강유라 
    /// 
    /// 
    /// </summary>
    public partial class Chemicalissue : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public Chemicalissue()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeGrid();
            InitializeEvent();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdChemicalissue.GridButtonItem = GridButtonItem.Export;
            grdChemicalissue.View.SetIsReadOnly(); 

            grdChemicalissue.View.AddTextBoxColumn("STATENAME", 180)
                .SetTextAlignment(TextAlignment.Center); // 진행현황
            grdChemicalissue.View.AddTextBoxColumn("REANALYSISTYPE", 100)
                .SetTextAlignment(TextAlignment.Center); // 재분석구분
            grdChemicalissue.View.AddTextBoxColumn("ANALYSISDATE", 120)
                .SetTextAlignment(TextAlignment.Center); // 분석일
            grdChemicalissue.View.AddTextBoxColumn("DEGREE", 80)
                .SetTextAlignment(TextAlignment.Center); // 차수
            grdChemicalissue.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 100)
                .SetLabel("LARGEPROCESSSEGMENTNAME"); // 대공정명
            grdChemicalissue.View.AddTextBoxColumn("FACTORYNAME", 80); // 공장명 
            grdChemicalissue.View.AddTextBoxColumn("EQUIPMENTNAME", 150); // 설비명
            grdChemicalissue.View.AddTextBoxColumn("CHILDEQUIPMENTNAME", 150); // 설비단명
            grdChemicalissue.View.AddTextBoxColumn("CHEMICALNAME", 120); // 약품명
            grdChemicalissue.View.AddTextBoxColumn("CHEMICALLEVEL", 80)
                .SetTextAlignment(TextAlignment.Center); // 약품등급
            grdChemicalissue.View.AddTextBoxColumn("MANAGEMENTSCOPE", 140); // 관리범위
            grdChemicalissue.View.AddTextBoxColumn("SPECSCOPE", 140); // 규격범위
            grdChemicalissue.View.AddSpinEditColumn("TITRATIONQTY", 100)
                .SetDisplayFormat("0.000", MaskTypes.Numeric); // 적정량
            grdChemicalissue.View.AddSpinEditColumn("ANALYSISVALUE", 100)
                .SetDisplayFormat("0.000", MaskTypes.Numeric); // 분석량
            grdChemicalissue.View.AddTextBoxColumn("ISCLOSE", 100)
                .SetTextAlignment(TextAlignment.Center); // 마감여부

            grdChemicalissue.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 100)
                .SetIsHidden(); // 대공정ID
            grdChemicalissue.View.AddTextBoxColumn("EQUIPMENTID", 100)
                .SetIsHidden(); // 설비ID
            grdChemicalissue.View.AddTextBoxColumn("CHILDEQUIPMENTID", 100)
                .SetIsHidden(); // 설비단ID
            grdChemicalissue.View.AddTextBoxColumn("SEQUENCE", 100)
                .SetIsHidden(); // 일련번호
            grdChemicalissue.View.AddTextBoxColumn("ANALYSISTYPE", 100)
                .SetIsHidden(); // 약품수질구분
            grdChemicalissue.View.AddTextBoxColumn("INSPITEMID", 100)
                .SetIsHidden(); // 약품ID
            grdChemicalissue.View.AddTextBoxColumn("PMTYPE", 100)
                .SetIsHidden(); // PM 구분
            grdChemicalissue.View.AddTextBoxColumn("REANALYSISTYPEID", 100)
                .SetIsHidden(); // 재분석구분
            grdChemicalissue.View.AddTextBoxColumn("ABNOCRNO", 100)
                .SetIsHidden(); // 이상발생번호
            grdChemicalissue.View.AddTextBoxColumn("ABNOCRTYPE", 100)
                .SetIsHidden(); // 이상발생유형
            grdChemicalissue.View.AddTextBoxColumn("STATE", 100)
                .SetIsHidden(); // 진행현황
            grdChemicalissue.View.AddTextBoxColumn("ISMODIFY")
                .SetIsHidden(); // 작업장 통제 권한에 따른 수정가능여부
            grdChemicalissue.View.AddTextBoxColumn("AREAID")
                .SetIsHidden(); // 작업장 ID
            grdChemicalissue.View.AddTextBoxColumn("AREANAME")
                .SetIsHidden(); // 작업장명

            grdChemicalissue.View.PopulateColumns();
            grdChemicalissue.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // Row DoubleClick시 팝업 호출
            grdChemicalissue.View.RowClick += View_RowClick;
            grdChemicalissue.View.RowStyle += View_RowStyle;
        }

        /// <summary>
        /// 포커스받은행 Row 색깔변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            // 포커스 받은 Row 색깔표시
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.HighPriority = true;
                e.Appearance.BackColor = Color.Yellow;             
            }
        }

        /// <summary>
        /// Row DoubleClick시 팝업 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                DataRow row = grdChemicalissue.View.GetFocusedDataRow();

                pnlContent.ShowWaitArea();

                ChemicalissuePopup popup = new ChemicalissuePopup(row);
                popup.Owner = this;
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.ParentControl = this;
                popup.btnSave.Enabled = btnFlag.Enabled;
                popup.SearchAutoAffectLot();

                popup.ShowDialog();

                pnlContent.CloseWaitArea();
            }
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            //DataTable changed = grdCodeClass.GetChangedRows();

            //ExecuteRule("SaveCodeClass", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();

            DateTime analysisDateFr = Convert.ToDateTime(values["P_PERIOD_PERIODFR"]);
            values["P_PERIOD_PERIODFR"] = string.Format("{0:yyyy-MM-dd}", analysisDateFr);
            DateTime analysisDateTo = Convert.ToDateTime(values["P_PERIOD_PERIODTO"]);
            values["P_PERIOD_PERIODTO"] = string.Format("{0:yyyy-MM-dd}", analysisDateTo);
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = await SqlExecuter.QueryAsync("GetChemicalIssue", "10001", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData");
            }

            grdChemicalissue.DataSource = dt;
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //this.Conditions.AddComboBox("p_plantId", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PLANTNAME", "PLANTID")
            //    .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
            //    .SetDefault(UserInfo.Current.Plant)
            //    .SetValidationIsRequired()
            //    .SetLabel("PLANT")
            //    .SetPosition(1.1); // Site (기본값 Login 유저의 Site)

            this.Conditions.AddComboBox("p_chemicalWaterType", new SqlQuery("GetChemicalWaterType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONCLASSNAME", "INSPECTIONCLASSID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetDefault("ChemicalInspection")
                .SetRelationIds("p_plantId")
                .SetValidationIsRequired()
                .SetLabel("CHEMICALWATERTYPE")
                .SetPosition(1.2); // 약품수질구분 (기본값 Login 유저의 Site에 해당하는 InspectionClassId)

            this.Conditions.AddComboBox("p_round", new SqlQuery("GetRoundListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CYCLESEQUENCETIME", "CYCLESEQUENCE")
                .SetRelationIds("p_plantId", "p_chemicalWaterType", "p_division")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetValidationIsRequired()
                .SetEmptyItem()
                .SetLabel("ROUND")
                .SetPosition(2.1); // 회차

            this.Conditions.AddComboBox("p_processsegmentclassId", new SqlQuery("GetLargeProcesssegmentListByQcm", "10002", "CODECLASSID=ChemicalAnalyRound", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                //.SetRelationIds("p_plantId")
                .SetRelationIds("p_chemicalWaterType")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetEmptyItem()
                .SetLabel("LARGEPROCESSSEGMENT")
                .SetPosition(2.3);

            InitializeConditionPopup_Equipment();
            InitializeConditionPopup_EquipmentStage();
            InitializeConditionPopup_Chemical();
        }

        /// <summary>
        /// Site에 따른 권한처리
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // 조회조건에 구성된 Control에 대한 처리가 필요한 경우
            // SmartComboBox plantCombo = Conditions.GetControl<SmartComboBox>("p_plantId");
        }

        /// <summary>
        /// 공장 조회조건
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {
            // 팝업 컬럼설정
            var plantPopupColumn = Conditions.AddSelectPopup("p_plantId", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
               .SetPopupLayout("PLANT", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("PLANT")
               .SetPopupResultCount(1)
               .SetPosition(2.2)
               .SetDefault(UserInfo.Current.Plant, UserInfo.Current.Plant)
               .SetValidationIsRequired();

            // 팝업 조회조건
            plantPopupColumn.Conditions.AddTextBox("PLANTIDNAME")
                .SetLabel("PLANTIDNAME");

            // 팝업 그리드
            plantPopupColumn.GridColumns.AddTextBoxColumn("PLANTID", 150)
                .SetValidationKeyColumn();
            plantPopupColumn.GridColumns.AddTextBoxColumn("PLANTNAME", 200);
        }

        /// <summary>
        /// 대공정 조회조건
        /// </summary>
        private void InitializeConditionPopup_Process()
        {
            // 팝업 컬럼설정
            var processPopupColumn = Conditions.AddSelectPopup("p_processsegmentclassId", new SqlQuery("GetLargeProcesssegmentListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
               .SetPopupLayout("LARGEPROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(400, 600)
               .SetLabel("LARGEPROCESSSEGMENT")
               .SetPopupResultCount(1)
               .SetPosition(2.3)
               .SetRelationIds("p_plantId");

            // 팝업 조회조건
            processPopupColumn.Conditions.AddTextBox("PROCESSSEGMENTCLASSIDNAME")
                .SetLabel("LARGEPROCESSSEGMENTIDNAME");

            // 팝업 그리드
            processPopupColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 150)
                .SetLabel("LARGEPROCESSSEGMENTID")
                .SetValidationKeyColumn();
            processPopupColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200)
                .SetLabel("LARGEPROCESSSEGMENTNAME");
        }

        /// <summary>
        /// 설비 조회조건
        /// </summary>
        private void InitializeConditionPopup_Equipment()
        {
            // 팝업 컬럼설정
            var equipmentPopupColumn = Conditions.AddSelectPopup("p_equipmentId", new SqlQuery("GetEquipmentListByChemicalAnalysis", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTNAME", "EQUIPMENTID")
               .SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("EQUIPMENT")
               .SetPopupResultCount(1)
               .SetPosition(3.1)
               .SetRelationIds("p_plantId", "p_processsegmentclassId");

            // 팝업 조회조건
            equipmentPopupColumn.Conditions.AddTextBox("EQUIPMENTIDNAME")
                .SetLabel("EQUIPMENTIDNAME");

            // 팝업 그리드
            equipmentPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetValidationKeyColumn();
            equipmentPopupColumn.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);
        }

        /// <summary>
        /// 설비단 조회조건
        /// </summary>
        private void InitializeConditionPopup_EquipmentStage()
        {
            // 팝업 컬럼설정
            var equipmentStagePopupColumn = Conditions.AddSelectPopup("p_childEquipmentId", new SqlQuery("GetChildEquipmentListByChemicalAnalysis", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CHILDEQUIPMENTNAME", "CHILDEQUIPMENTID")
               .SetPopupLayout("CHILDEQUIPMENT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("CHILDEQUIPMENT")
               .SetPopupResultCount(1)
               .SetPosition(3.2)
               .SetRelationIds("p_plantId", "p_equipmentId");

            // 팝업 조회조건
            equipmentStagePopupColumn.Conditions.AddTextBox("CHILDEQUIPMENTIDNAME")
                .SetLabel("CHILDEQUIPMENTIDNAME");

            // 팝업 그리드
            equipmentStagePopupColumn.GridColumns.AddTextBoxColumn("CHILDEQUIPMENTID", 150)
                .SetValidationKeyColumn();
            equipmentStagePopupColumn.GridColumns.AddTextBoxColumn("CHILDEQUIPMENTNAME", 200);
        }

        /// <summary>
        /// 약품 조회조건
        /// </summary>
        private void InitializeConditionPopup_Chemical()
        {
            // 팝업 컬럼설정
            var chemicalPopupColumn = Conditions.AddSelectPopup("p_chemicalId", new SqlQuery("GetChemicalListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMNAME", "INSPITEMID")
               .SetPopupLayout("CHEMICAL", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("CHEMICAL")
               .SetPopupResultCount(1)
               .SetPosition(3.3)
               .SetRelationIds("p_processsegmentclassId", "p_equipmentId", "p_childEquipmentId");

            // 팝업 조회조건
            chemicalPopupColumn.Conditions.AddTextBox("INSPITEMIDNAME")
                .SetLabel("INSPITEMIDNAME");

            // 팝업 그리드
            chemicalPopupColumn.GridColumns.AddTextBoxColumn("INSPITEMID", 150)
                .SetValidationKeyColumn();
            chemicalPopupColumn.GridColumns.AddTextBoxColumn("INSPITEMNAME", 200);
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            //grdCodeClass.View.CheckValidation();

            //DataTable changed = grdCodeClass.GetChangedRows();

            //if (changed.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}
        }

        #endregion

        #region Private Function

        #endregion

        #region Global Function

        /// <summary>
        /// Popup 닫혔을때 재검색하기 위한 함수
        /// </summary>
        public void Search()
        {
            OnSearchAsync();
        }

        #endregion
    }
}
