#region using

using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.QualityAnalysis;

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

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 약품관리 > 약품분석현황
    /// 업  무  설  명  : 정기건으로 등록된 약품분석의 회차별 적정량값을 조회한다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-06-12
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ChemicalAnalysisStatus : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ChemicalAnalysisStatus()
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
            grdChemicalAnalysisStatus.GridButtonItem = GridButtonItem.Export;
            grdChemicalAnalysisStatus.View.SetIsReadOnly(); 

            var standard = grdChemicalAnalysisStatus.View.AddGroupColumn("STANDARD");

            standard.AddTextBoxColumn("ANALYSISDATE", 120)
                .SetTextAlignment(TextAlignment.Center); // 분석일
            standard.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 100)
                .SetLabel("LARGEPROCESSSEGMENTNAME"); // 대공정명
            standard.AddTextBoxColumn("FACTORYNAME", 80); // 공장명
            standard.AddTextBoxColumn("EQUIPMENTNAME", 150); // 설비명
            standard.AddTextBoxColumn("CHILDEQUIPMENTNAME", 150); // 설비단명
            standard.AddTextBoxColumn("CHEMICALNAME", 120); // 약품명
            standard.AddTextBoxColumn("MANAGEMENTSCOPE", 140); // 관리범위
            standard.AddTextBoxColumn("SPECSCOPE", 140); // 규격범위

            var degree = grdChemicalAnalysisStatus.View.AddGroupColumn("ANALYSISRESULT");

            degree.AddSpinEditColumn("0", 80)
                .SetLabel("NOTPERIOD")
                .SetDisplayFormat("0.000", MaskTypes.Numeric); // 정기외
            degree.AddSpinEditColumn("1", 80)
                .SetLabel("ONEROUND")
                .SetDisplayFormat("0.000", MaskTypes.Numeric); // 1회차
            degree.AddSpinEditColumn("2", 80)
                .SetLabel("TWOROUND")
                .SetDisplayFormat("0.000", MaskTypes.Numeric); // 2회차
            degree.AddSpinEditColumn("3", 80)
                .SetLabel("THREEROUND")
                .SetDisplayFormat("0.000", MaskTypes.Numeric); // 3회차
            degree.AddSpinEditColumn("4", 80)
                .SetLabel("FOURROUND")
                .SetDisplayFormat("0.000", MaskTypes.Numeric); // 4회차
            degree.AddSpinEditColumn("5", 80)
                .SetLabel("FIVEROUND")
                .SetDisplayFormat("0.000", MaskTypes.Numeric); // 5회차
            degree.AddSpinEditColumn("6", 80)
                .SetLabel("SIXROUND")
                .SetDisplayFormat("0.000", MaskTypes.Numeric); // 6회차
            degree.AddSpinEditColumn("7", 80)
                .SetLabel("SEVENROUND")
                .SetDisplayFormat("0.000", MaskTypes.Numeric); // 7회차
            degree.AddSpinEditColumn("8", 80)
                .SetLabel("EIGHTROUND")
                .SetDisplayFormat("0.000", MaskTypes.Numeric); // 8회차
            degree.AddSpinEditColumn("9", 80)
                .SetLabel("NINEROUND")
                .SetDisplayFormat("0.000", MaskTypes.Numeric); // 9회차
            degree.AddSpinEditColumn("10", 80)
                .SetLabel("TENROUND")
                .SetDisplayFormat("0.000", MaskTypes.Numeric); // 10회차
            degree.AddSpinEditColumn("11", 80)
                .SetLabel("ELEVENROUND")
                .SetDisplayFormat("0.000", MaskTypes.Numeric); // 11회차
            degree.AddSpinEditColumn("12", 80)
                .SetLabel("TWELVEROUND")
                .SetDisplayFormat("0.000", MaskTypes.Numeric); // 12회차
            degree.AddTextBoxColumn("SPECOUTDEGREE", 100)
                .SetIsHidden(); // 스펙아웃난 차수

            grdChemicalAnalysisStatus.View.PopulateColumns();
            grdChemicalAnalysisStatus.View.OptionsView.AllowCellMerge = true; // CellMerge
            grdChemicalAnalysisStatus.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            grdChemicalAnalysisStatus.View.CellMerge += View_CellMerge;
            grdChemicalAnalysisStatus.View.RowCellStyle += View_RowCellStyle;
        }

        /// <summary>
        /// Spec Out 발생한 데이터는 글씨를 빨간색으로 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (IsSpecOut(e.Column.FieldName, e.RowHandle)) 
            {
                e.Appearance.ForeColor = Color.Red;
            }

            // 포커스 받은 Row 색깔표시
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                if (e.Column.FieldName == "ANALYSISDATE" || e.Column.FieldName == "PROCESSSEGMENTCLASSNAME"
                    || e.Column.FieldName == "FACTORYNAME" || e.Column.FieldName == "EQUIPMENTNAME")
                    //|| e.Column.FieldName == "CHILDEQUIPMENTNAME")
                {
                    e.Appearance.BackColor = Color.White;
                }
                else
                {
                    e.Appearance.BackColor = Color.Yellow;
                }         
            }
        }

        /// <summary>
        /// 사용자 지정 Cell Merge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (view == null)
            {
                return;
            }

            if (e.Column.FieldName == "ANALYSISDATE" || e.Column.FieldName == "PROCESSSEGMENTCLASSNAME"
                || e.Column.FieldName == "EQUIPMENTNAME" //|| e.Column.FieldName == "CHILDEQUIPMENTNAME"
                || e.Column.FieldName == "FACTORYNAME")
            {
                string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);
                e.Merge = (str1 == str2);
            }
            else
            {
                e.Merge = false;
            }
            e.Handled = true;
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

            DataTable dt = await SqlExecuter.QueryAsync("GetChemicalAnalysisStatus", "10001", values);

            if (dt.Rows.Count < 1) 
            {
                ShowMessage("NoSelectData"); 
            }

            grdChemicalAnalysisStatus.DataSource = dt;
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //this.Conditions.AddComboBox("p_plantId", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
            //    .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
            //    .SetDefault(UserInfo.Current.Plant)
            //    .SetValidationIsRequired()
            //    .SetLabel("PLANT")
            //    .SetPosition(1.1);

            this.Conditions.AddComboBox("p_chemicalWaterType", new SqlQuery("GetChemicalWaterType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONCLASSNAME", "INSPECTIONCLASSID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetDefault("ChemicalInspection")
                .SetRelationIds("p_plantId")
                .SetValidationIsRequired()
                .SetLabel("CHEMICALWATERTYPE")
                .SetPosition(1.2); // 약품수질구분 (기본값 Login 유저의 Site에 해당하는 InspectionClassId)
                

            this.Conditions.AddComboBox("p_processsegmentclassId", new SqlQuery("GetLargeProcesssegmentListByQcm", "10002", "CODECLASSID=ChemicalAnalyRound", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                //.SetRelationIds("p_plantId")
                .SetRelationIds("p_chemicalWaterType")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetEmptyItem()
                .SetLabel("LARGEPROCESSSEGMENT")
                .SetPosition(2.2);

            InitializeConditionPopup_Equipment();
            InitializeConditionPopup_EquipmentStage();
            InitializeConditionPopup_Chemical();
        }

        /// <summary>
        /// Site 조회조건
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {
            // 팝업 컬럼설정
            var plantPopupColumn = Conditions.AddSelectPopup("p_plantId", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
               .SetPopupLayout("PLANT", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("PLANT")
               .SetPopupResultCount(1)
               .SetPosition(2.1)
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
               .SetPopupLayoutForm(500, 600)
               .SetLabel("LARGEPROCESSSEGMENT")
               .SetPopupResultCount(1)
               .SetPosition(2.2)
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
               .SetPopupLayoutForm(400, 600)
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

        /// <summary>
        /// NG 발생 포인트 컬럼 추출 함수
        /// </summary>
        /// <param name="col"></param>
        /// <param name="handle"></param>
        /// <returns></returns>
        private bool IsSpecOut(string col, int handle)
        {
            bool flag = false;

            try
            {
                string specOutDegree = Format.GetString(grdChemicalAnalysisStatus.View.GetRowCellValue(handle, "SPECOUTDEGREE"));
                string[] specOutSplitDegree = specOutDegree.Split(',');

                foreach (string str in specOutSplitDegree)
                {
                    if (str.Equals(col))
                    {
                        flag = true;
                        break;
                    }
                }

                return flag;
            }
            catch
            {
                return flag;
            }
        }

        #endregion
    }
}
