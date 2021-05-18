#region using

using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
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

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품질 기준 정보 > 약품분석 회차관리
    /// 업  무  설  명  : 약품분석의 회차관리를 한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-06-26
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ChemicalAnalysisCycleManagement : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public ChemicalAnalysisCycleManagement()
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
            InitializeGrdCycle();

        }

        #region 회차정의 탭 그리드 초기화
        /// <summary>        
        /// 회차 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrdCycle()
        {
            // TODO : 그리드 초기화 로직 추가
            grdCycle.GridButtonItem = GridButtonItem.Add | GridButtonItem.Export;
            grdCycle.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdCycle.View.AddComboBoxColumn("INSPECTIONCLASSID", 150, new SqlQuery("GetInspectionClass", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONCLASSNAME", "INSPECTIONCLASSID")
               .SetValidationIsRequired()
               .SetIsReadOnly();
           
            grdCycle.View.AddTextBoxColumn("CYCLESEQUENCE", 200)
               .SetIsReadOnly();

            grdCycle.View.AddTextBoxColumn("ENTERPRISEID", 200)
               .SetIsHidden();

            grdCycle.View.AddTextBoxColumn("PLANTID", 200)
               .SetIsHidden();
            
            grdCycle.View.AddTextBoxColumn("TIMECYCLE", 100);

            grdCycle.View.AddTextBoxColumn("TIMECYCLEGAP", 100)
               .SetIsReadOnly();

            grdCycle.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetValidationIsRequired()
               .SetDefault("Valid")
               .SetTextAlignment(TextAlignment.Center);

            grdCycle.View.AddTextBoxColumn("CREATOR", 80)
               .SetIsReadOnly()
              .SetTextAlignment(TextAlignment.Center);

            grdCycle.View.AddTextBoxColumn("CREATEDTIME", 130)
               .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Center);

            grdCycle.View.AddTextBoxColumn("MODIFIER", 80)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Center);

            grdCycle.View.AddTextBoxColumn("MODIFIEDTIME", 130)
               .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Center);

            grdCycle.View.PopulateColumns();

            RepositoryItemTimeEdit edit = new RepositoryItemTimeEdit();

            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            edit.Mask.EditMask = "([0-1]?[0-9]|2[0-3]):[0-5][0-9]";
            edit.Mask.UseMaskAsDisplayFormat = true;
            grdCycle.View.Columns["TIMECYCLE"].ColumnEdit = edit;
            grdCycle.View.Columns["TIMECYCLEGAP"].ColumnEdit = edit;

        }
        #endregion

        #region 회차적용 탭 그리드 초기화

        /// <summary>
        /// 대공정 그리드 초기화
        /// </summary>
        private void InitializeGrdProcessSegmentClass()
        {
            grdProcessSegmentClass.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdProcessSegmentClass.GridButtonItem = GridButtonItem.Export;
            grdProcessSegmentClass.View.SetIsReadOnly();
            
            grdProcessSegmentClass.View.AddTextBoxColumn("RESOURCEID")
                .SetLabel("PROCESSSEGMENTCLASSID");

            grdProcessSegmentClass.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME");

            grdProcessSegmentClass.View.AddTextBoxColumn("INSPECTIONDEFID")
               .SetIsHidden();

            grdProcessSegmentClass.View.AddTextBoxColumn("INSPECTIONDEFVERSION")
               .SetIsHidden();

            grdProcessSegmentClass.View.AddTextBoxColumn("RESOURCETYPE")
               .SetIsHidden();

            grdProcessSegmentClass.View.AddTextBoxColumn("RESOURCEVERSION")
               .SetDefault("*")
               .SetIsHidden();

            grdProcessSegmentClass.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetTextAlignment(TextAlignment.Center);

            grdProcessSegmentClass.View.PopulateColumns();
        }

        /// <summary>
        /// 설비/설비단 그리드 초기화 (INSPITEMCLASS) 
        /// </summary>
        private void InitializeGrdEquipment()
        {
            grdEquipment.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdEquipment.GridButtonItem = GridButtonItem.Export;
            grdEquipment.View.SetIsReadOnly();

            //grdEquipment.View.AddTextBoxColumn("REALINSPITEMCLASSNAME")
            //    .SetLabel("INSPITEMCLASSNAME")
            //    .SetIsHidden();

            grdEquipment.View.AddTextBoxColumn("MAINEQUIPMENTID")
                .SetLabel("EQUIPMENTID");

            grdEquipment.View.AddTextBoxColumn("MAINEQUIPMENTNAME")
                .SetLabel("EQUIPMENTNAME");

            grdEquipment.View.AddTextBoxColumn("SUBEQUIPMENTID")
                .SetLabel("CHILDEQUIPMENTID");

            grdEquipment.View.AddTextBoxColumn("SUBEQUIPMENTNAME")
                .SetLabel("CHILDEQUIPMENTNAME");

            grdProcessSegmentClass.View.AddTextBoxColumn("INSPECTIONDEFID")
                .SetIsHidden();

            grdProcessSegmentClass.View.AddTextBoxColumn("INSPECTIONDEFVERSION")
                .SetIsHidden();

            grdProcessSegmentClass.View.AddTextBoxColumn("RESOURCEID")
                .SetIsHidden();

            grdProcessSegmentClass.View.AddTextBoxColumn("RESOURCETYPE")
                .SetIsHidden();

            grdProcessSegmentClass.View.AddTextBoxColumn("RESOURCEVERSION")
               .SetDefault("*")
               .SetIsHidden();


            grdEquipment.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetTextAlignment(TextAlignment.Center);

            grdEquipment.View.PopulateColumns();
        }


        /// <summary>
        /// 약품 그리드 초기화 (INSPITEM) 
        /// </summary>
        private void InitializeGrdInspItem()
        {
            grdInspItem.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdInspItem.GridButtonItem = GridButtonItem.Export;
            grdInspItem.View.SetIsReadOnly();

            grdInspItem.View.AddTextBoxColumn("INSPITEMID");

            grdInspItem.View.AddTextBoxColumn("INSPITEMNAME");

            grdInspItem.View.AddTextBoxColumn("INSPITEMVERSION")
               .SetIsHidden();

            grdInspItem.View.AddTextBoxColumn("SUBEQUIPMENTID")
              .SetIsHidden();

            grdInspItem.View.AddTextBoxColumn("INSPECTIONDEFID")
              .SetIsHidden();

            //grdInspItem.View.AddTextBoxColumn("INSPECTIONDEFID")
            //    .SetIsHidden();

            //grdInspItem.View.AddTextBoxColumn("INSPECTIONDEFVERSION")
            //    .SetIsHidden();

            grdInspItem.View.AddTextBoxColumn("RESOURCEID")
                .SetIsHidden();

            grdInspItem.View.AddTextBoxColumn("RESOURCETYPE")
                .SetIsHidden();

            grdInspItem.View.AddTextBoxColumn("RESOURCEVERSION")
               .SetDefault("*")
               .SetIsHidden();

            grdInspItem.View.PopulateColumns();
        }


        /// <summary>
        /// 회차적용 그리드 초기화 (INSPITEM) 
        /// </summary>
        private void InitializeGrdCycleApply()
        {
            grdCycleApply.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdCycleApply.GridButtonItem =  GridButtonItem.Export;
            
            grdCycleApply.View.AddTextBoxColumn("CYCLESEQUENCE")
                .SetIsReadOnly();

            grdCycleApply.View.AddTextBoxColumn("TIMECYCLE")
                .SetIsReadOnly();

            grdCycleApply.View.AddComboBoxColumn("ISAPPLY", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=IsApply", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdCycleApply.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetValidationIsRequired()
                .SetDefault("Valid")
                .SetTextAlignment(TextAlignment.Center);

            grdCycleApply.View.AddTextBoxColumn("ENTERPRISEID", 200)
              .SetIsHidden();

            grdCycleApply.View.AddTextBoxColumn("PLANTID", 200)
               .SetIsHidden();

            grdCycleApply.View.PopulateColumns();
        }
        #endregion

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            //new SetGridDeleteButonVisible(grdCycle);
            tabCycle.SelectedPageChanged += TabCycle_SelectedPageChanged;

            //grdCycle 그리드의 새 row 추가 이벤트
            grdCycle.View.AddingNewRow += View_AddingNewRow;
            //grdCycle 그리드의 새 row 추가 이벤트
            grdCycle.ToolbarAddingRow += GrdCycle_ToolbarAddingRow;

            //대공정 그리드의 포커스row 바뀔때 이벤트
            grdProcessSegmentClass.View.FocusedRowChanged += View_GrdProcessSegmentClass_FocusedRowChanged;
            //설비/설비단 그리드의 포커스row 바뀔때 이벤트
            grdEquipment.View.FocusedRowChanged += View_GrdEquipment_FocusedRowChanged;
            //검사항목 그리드의 포커스row 바뀔때 이벤트
            grdInspItem.View.FocusedRowChanged += View_GrdInspItem_FocusedRowChanged;
         
        }

   

        private void View_GrdInspItem_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = grdInspItem.View.GetFocusedDataRow();
            if (row == null) return;
            var values = SetParamenter(row);
            values.Add("SUBEQUIPMENTID", row["SUBEQUIPMENTID"]);
            values.Add("INSPITEMID", row["INSPITEMID"]);
            values.Add("INSPITEMVERSION", row["INSPITEMVERSION"]);
            values.Add("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);


            DataTable dt = SqlExecuter.Query("SelectCycleApply", "10001", values);
            grdCycleApply.View.ClearDatas();
            grdCycleApply.DataSource = dt;
        }

        private void View_GrdEquipment_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = grdEquipment.View.GetFocusedDataRow();
            if (row == null) return;
            var values = SetParamenter(row);

            DataTable dt = SqlExecuter.Query("SelectCycleInspItem", "10001", values);
            grdInspItem.View.ClearDatas();
            grdCycleApply.View.ClearDatas();
            grdInspItem.DataSource = dt;
        }

        private void View_GrdProcessSegmentClass_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = grdProcessSegmentClass.View.GetFocusedDataRow();
            if (row == null) return;
            var values = SetParamenter(row);

            DataTable dt = SqlExecuter.Query("SelectCycleEquipment", "10001", values);
            grdEquipment.View.ClearDatas();
            grdInspItem.View.ClearDatas();
            grdCycleApply.View.ClearDatas();
            grdEquipment.DataSource = dt;
        }

        /// <summary>
        /// 선택된 탭이 변경될 때 이벤트
        /// 회차적용 그리드 초기화, 데이터 바인딩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabCycle_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (tabCycle.SelectedTabPageIndex == 1)
            {
                if (!grdProcessSegmentClass.View.IsInitializeColumns)
                {
                    InitializeGrdProcessSegmentClass();
                    InitializeGrdEquipment();
                    InitializeGrdInspItem();
                    InitializeGrdCycleApply();
                }

                //LoadProcessSegment();
            }
        }

        /// <summary>
        /// 새 Row를 추가 할때 INSPECTIONCLASSID, CYCLESEQUENCE
        /// 자동입력 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["INSPECTIONCLASSID"] = Conditions.GetValue("P_INSPECTIONCLASSID");
            args.NewRow["CYCLESEQUENCE"] = grdCycle.View.DataRowCount;
            args.NewRow["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = Conditions.GetValue("P_PLANTID");
        }

        /// <summary>
        /// 새 Row를 추가 할때 24회 이상은 입력 불가 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdCycle_ToolbarAddingRow(object sender, CancelEventArgs e)
        {
            if (grdCycle.View.DataRowCount > 23)
            {
                e.Cancel = true;
                ShowMessage("CycleSequenceMax");//회차 등록의 가능 회차 수를 초과하였습니다.
            }

            if (string.IsNullOrWhiteSpace(Conditions.GetValue("P_INSPECTIONCLASSID").ToString()))
            {
                e.Cancel = true;
                ShowMessage("MustSelectInspectionClassId");//회차를 등록하기 전에 검사종류를 먼저 선택해야 합니다.
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
            //DataTable changed = null;
            //switch (tabCycle.SelectedTabPageIndex)
            //{
            //    case 0:
            //        changed = grdCycle.GetChangedRows();
            //        ExecuteRule("SaveChemicalCycle", SetDataBeforeSave(changed));
            //        break;

            //    case 1:
            //        changed = grdCycleApply.GetChangedRows();
            //        ExecuteRule("SaveChemicalItemCycle", SaveCycleApply(changed));
            //        break;
            //}
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
            DataTable dt = null;

            var values = Conditions.GetValues();

            switch (tabCycle.SelectedTabPageIndex)
            {
                case 0:
                    dt = await QueryAsync("SelectChemicalAnalysisCycleManagement", "10001", values);
                    CheckData(dt);
                    grdCycle.DataSource = CalculateTimeGap(dt);
                    break;

                case 1:
                    grdProcessSegmentClass.View.ClearDatas();
                    grdEquipment.View.ClearDatas();
                    grdInspItem.View.ClearDatas();
                    grdCycleApply.View.ClearDatas();
                    LoadProcessSegment();
                    break;
            }
        
 
        }
    

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            //this.Conditions.GetControl<SmartTextBox>("P_PLANTID").EditValue = Framework.UserInfo.Current.Plant;
            //this.Conditions.GetControl<SmartTextBox>("P_PLANTID").Enabled = false;
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
            DataTable changed = null; 

            switch (tabCycle.SelectedTabPageIndex)
            {
                case 0:
                    grdCycle.View.CheckValidation();
                    changed = grdCycle.GetChangedRows();
                    break;

                case 1:
                    grdCycleApply.View.CheckValidation();
                    changed = grdCycleApply.GetChangedRows();
                    break;
            }

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
        /// 저장 전에 ENTERPRISEID 와 PLANTID를 로그인 한 유저 정보로 할당해주는 이벤트
        /// </summary>
        /// <param name="table"></param>
        private DataTable SetDataBeforeSave(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                row["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
                row["PLANTID"] = Framework.UserInfo.Current.Plant;
            }

            return table;
        }

        /// <summary>
        /// 검색한 결과 테이블에 데이터가 있는지 확인하는 함수
        /// </summary>
        /// <param name="table"></param>
        private void CheckData(DataTable table)
        {
            if (table.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

        }

        /// <summary>
        /// 검색한 테이블에서 전 회차의 시작시간과 다음회차와의 시간간격을 계산해주는 함수
        /// </summary>
        /// <param name="table"></param>
        private DataTable CalculateTimeGap(DataTable table)
        {
            DataRow rowBefore = null;
            DataRow rowAfter = null;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (i == table.Rows.Count-1)
                {
                    rowBefore = table.Rows[i];
                    rowAfter = table.Rows[0];
                }
                else
                {
                    rowBefore = table.Rows[i];
                    rowAfter = table.Rows[i + 1];
                }

                TimeSpan TimeGap = new TimeSpan();

                if (Convert.ToDateTime(rowAfter["TIMECYCLE"].ToString()) < Convert.ToDateTime(rowBefore["TIMECYCLE"].ToString()))
                {
                    TimeGap = Convert.ToDateTime(rowAfter["TIMECYCLE"].ToString()).AddHours(24) - Convert.ToDateTime(rowBefore["TIMECYCLE"].ToString());
                }
                else
                {
                    TimeGap = Convert.ToDateTime(rowAfter["TIMECYCLE"].ToString()) - Convert.ToDateTime(rowBefore["TIMECYCLE"].ToString());
                }
                    rowBefore["TIMECYCLEGAP"] = TimeGap;
            }

            return table;
        }

        /// <summary>
        /// 저장 하기전,  그리드의 선택된 항목을 추가 해 주는 함수
        /// </summary>
        /// <param name="changed"></param>
        /// <returns></returns>
        private DataTable SaveCycleApply(DataTable table)
        {
            DataRow inspItemRow = grdInspItem.View.GetFocusedDataRow();
            DataRow equipmentRow = grdEquipment.View.GetFocusedDataRow();

            DataTable cycleInputTable = GetCycleApplyTable();
            foreach (DataRow row in table.Rows)
            {
                //:: 김용조 :: 수정
                DataRow newRow = cycleInputTable.NewRow();
                newRow["INSPECTIONCLASSID"] = Conditions.GetValue("P_INSPECTIONCLASSID");   //검색조건의 ClassID를 할당
                newRow["CYCLESEQUENCE"] = row.GetString("CYCLESEQUENCE");                   //해당 그리드에서 시퀀스를 할당
                //newRow["INSPECTIONDEFVERSION"] = inspItemRow["INSPECTIONDEFVERSION"];
                newRow["INSPITEMCLASSID"] = equipmentRow.GetString("SUBEQUIPMENTID");      //설비그리드에서 설비아이디할당
                newRow["INSPITEMID"] = inspItemRow["INSPITEMID"];
                newRow["INSPITEMVERSION"] = inspItemRow["INSPITEMVERSION"];
                newRow["PLANTID"] = Framework.UserInfo.Current.Plant;
                newRow["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
                newRow["VALIDSTATE"] = row.GetString("VALIDSTATE");                         //해당 그리드에서 유효값 할당
                cycleInputTable.Rows.Add(newRow);
            }
            return cycleInputTable;
        }

        private DataTable GetCycleApplyTable()
        {
            DataTable cycleTable = new DataTable();
            cycleTable.TableName = "list";

            cycleTable.Columns.Add("CYCLESEQUENCE");
            cycleTable.Columns.Add("INSPECTIONCLASSID");
            cycleTable.Columns.Add("INSPECTIONDEFID");
            cycleTable.Columns.Add("INSPECTIONDEFVERSION");
            cycleTable.Columns.Add("INSPITEMCLASSID");
            cycleTable.Columns.Add("INSPITEMID");
            cycleTable.Columns.Add("INSPITEMVERSION");
            cycleTable.Columns.Add("PLANTID");
            cycleTable.Columns.Add("ENTERPRISEID");
            cycleTable.Columns.Add("VALIDSTATE");

            return cycleTable;
        }
        /// <summary>
        /// 대공정그리드의 데이터를 조회하는 함수
        /// </summary>
        private void LoadProcessSegment()
        {
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
           
            DataTable dt = SqlExecuter.Query("SelectCycleProcessSegmentClass", "10001", values);

            CheckData(dt);

            grdProcessSegmentClass.DataSource = dt;
        }

        /// <summary>
        /// fucusedRow changed시 focuse된 row의 값을 파라미터로 할당 해주는 함수
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private Dictionary<string, object> SetParamenter(DataRow row)
        {
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("RESOURCEID", row["RESOURCEID"]);
            //values.Add("RESOURCETYPE", row["RESOURCETYPE"]);
            values.Add("RESOURCEVERSION", "*");
            values.Add("INSPECTIONDEFID", row["INSPECTIONDEFID"]);
            values.Add("INSPECTIONDEFVERSION", row["INSPECTIONDEFVERSION"]);

            return values;
        }


        private void ProcSave(string strtitle)
        {

            DataTable changed = null;

            switch (tabCycle.SelectedTabPageIndex)
            {
                case 0:
                    grdCycle.View.FocusedRowHandle = grdCycle.View.FocusedRowHandle;
                    grdCycle.View.FocusedColumn = grdCycle.View.Columns["TIMECYCLEGAP"];
                    grdCycle.View.ShowEditor();
                    grdCycle.View.CheckValidation();
                    changed = grdCycle.GetChangedRows();
                    break;

                case 1:
                    grdCycleApply.View.FocusedRowHandle = grdCycleApply.View.FocusedRowHandle;
                    grdCycleApply.View.FocusedColumn = grdCycleApply.View.Columns["TIMECYCLE"];
                    grdCycleApply.View.ShowEditor();
                    grdCycleApply.View.CheckValidation();
                    changed = grdCycleApply.GetChangedRows();
                    break;
            }

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
                   
                    DataTable dtSave = null;
                    switch (tabCycle.SelectedTabPageIndex)
                    {
                        case 0:
                            dtSave = grdCycle.GetChangedRows();
                            ExecuteRule("SaveChemicalCycle", SetDataBeforeSave(changed));
                            ShowMessage("SuccessOspProcess");
                            OnSaveConfrimSearch();
                            break;

                        case 1:
                            dtSave = grdCycleApply.GetChangedRows();
                            ExecuteRule("SaveChemicalItemCycle", SaveCycleApply(changed));
                            ShowMessage("SuccessOspProcess");
                            OnSaveConfrimSearch();
                            break;
                    }
                   
                    //재조회 
                   
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
            DataTable dt = null;
            var values = Conditions.GetValues();

            switch (tabCycle.SelectedTabPageIndex)
            {
                case 0:
                    dt = SqlExecuter.Query("SelectChemicalAnalysisCycleManagement", "10001", values);
                    CheckData(dt);
                    grdCycle.DataSource = CalculateTimeGap(dt);
                    break;

                case 1:
                    DataRow row = grdInspItem.View.GetFocusedDataRow();
                    if (row == null) return;
                    var valuesPar = SetParamenter(row);
                    valuesPar.Add("SUBEQUIPMENTID", row["SUBEQUIPMENTID"]);
                    valuesPar.Add("INSPITEMID", row["INSPITEMID"]);
                    valuesPar.Add("INSPITEMVERSION", row["INSPITEMVERSION"]);
                    valuesPar.Add("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);


                    dt = SqlExecuter.Query("SelectCycleApply", "10001", valuesPar);
                    grdCycleApply.View.ClearDatas();
                    grdCycleApply.DataSource = dt;
                    break;

            }

        }
        #endregion
    }
}
