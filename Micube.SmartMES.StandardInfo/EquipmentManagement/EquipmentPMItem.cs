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

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 설비기준정보 > 설비별 점검
    /// 업  무  설  명  : 설비별 점검 정보를 관리한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-05-22
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class EquipmentPMItem : SmartConditionManualBaseForm
    {
        #region Local Variables

        private DataTable _searchData;//저장,수정,삭제 시 비교할 기존 데이터 테이블
        private DataTable _saveData;//저장,수정,삭제 시 바뀐 내용 데이터 테이블
        private DataTable _realSaveData;//비교 후 실제로 저장할 데이터 테이블
        private DataTable _noMappintData;//mapping 되지 않은 항목 데이터 테이블
        string _equipmentId = "";

        #endregion

        #region 생성자

        public EquipmentPMItem()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
        }

        /// <summary>
        /// CboPMList 콤보박스를 초기화 하는 함수
        /// CboPMList 콤보박스를 초기화 하는 함수
        /// </summary>
        private void InitializeComboBox()
        {
            SqlQuery pmList = new SqlQuery("GetMaintItemClassList", "10001", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            DataTable pmListTable = pmList.Execute();

            this.cboPMList.Properties.DataSource = pmListTable;
            this.cboPMList.Properties.ValueMember = "MAINTITEMCLASSID";
            this.cboPMList.Properties.DisplayMember = "MAINTITEMCLASSNAME";
            this.cboPMList.Properties.ShowHeader = false;
            this.cboPMList.Enabled = false;

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("P_MAINTITEMCLASSID", "");
            values.Add("P_EQUIPMENTID", _equipmentId);

            _noMappintData = SqlExecuter.Query("SelectEquipmentPMItem_NoMapping", "10001", values);
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridEquipment()
        {
            // TODO : 그리드 초기화 로직 추가
            grdEquipment.GridButtonItem = GridButtonItem.None;
            grdEquipment.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdEquipment.View.SetIsReadOnly();

            grdEquipment.View.AddTextBoxColumn("EQUIPMENTID", 150);

            grdEquipment.View.AddTextBoxColumn("PLANTID", 150)
                        .SetIsHidden();

            grdEquipment.View.AddTextBoxColumn("EQUIPMENTNAME", 200);

            grdEquipment.View.AddTextBoxColumn("EQUIPMENTCLASSID", 200)
                        .SetIsHidden();

            grdEquipment.View.AddTextBoxColumn("EQUIPMENTCLASSNAME", 200);

            grdEquipment.View.AddTextBoxColumn("AREAID", 150);

            grdEquipment.View.AddTextBoxColumn("AREANAME", 150);

            grdEquipment.View.AddTextBoxColumn("PROCESSSEGMENTID", 150);

            grdEquipment.View.AddTextBoxColumn("VENDORID", 150);

            grdEquipment.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                        .SetDefault("Valid")
                        .SetValidationIsRequired()
                        .SetTextAlignment(TextAlignment.Center);

            grdEquipment.View.PopulateColumns();
        }

        private void InitializeGridPMItem()
        {
            // TODO : 그리드 초기화 로직 추가
            grdPMItemNotMapping.View.SetIsReadOnly();
            grdPMItemNotMapping.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdPMItemNotMapping.GridButtonItem = GridButtonItem.Refresh;

            grdPMItemNotMapping.View.AddTextBoxColumn("EQUIPMENTID", 200)
                               .SetIsHidden();

            grdPMItemNotMapping.View.AddTextBoxColumn("MAINTITEMCLASSID", 200);

            grdPMItemNotMapping.View.AddTextBoxColumn("MAINTITEMCLASSNAME", 200);

            grdPMItemNotMapping.View.AddTextBoxColumn("MAINTITEMID", 150);

            grdPMItemNotMapping.View.AddTextBoxColumn("MAINTITEMNAME", 150);

            grdPMItemNotMapping.View.AddTextBoxColumn("DESCRIPTION", 200);

            grdPMItemNotMapping.View.AddTextBoxColumn("PLANTID", 200)
                               .SetIsHidden();

            grdPMItemNotMapping.View.AddTextBoxColumn("ENTERPRISEID", 200)
                               .SetIsHidden();

            grdPMItemNotMapping.View.AddSpinEditColumn("MAINTDURATION", 150)
                               .SetIsHidden();
            //테이블 변경 후 수정 필!!
            grdPMItemNotMapping.View.AddTextBoxColumn("MAINTDURATIONUNIT", 80)
                               .SetIsHidden();

            grdPMItemNotMapping.View.AddSpinEditColumn("MAINTSEQUENCE", 150)
                               .SetIsHidden();

            grdPMItemNotMapping.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                               .SetDefault("Valid")
                               .SetTextAlignment(TextAlignment.Center);

            grdPMItemNotMapping.View.PopulateColumns();
        }

        private void InitializeGridPMItemList()
        {
            grdPMItemMapping.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdPMItemMapping.GridButtonItem = GridButtonItem.None;

            grdPMItemMapping.View.AddSpinEditColumn("MAINTSEQUENCE", 100);
            grdPMItemMapping.View.SetSortOrder("MAINTSEQUENCE");

            grdPMItemMapping.View.AddTextBoxColumn("EQUIPMENTID", 150);

            grdPMItemMapping.View.AddTextBoxColumn("MAINTITEMCLASSID", 200)
                            .SetIsReadOnly();

            grdPMItemMapping.View.AddTextBoxColumn("MAINTITEMCLASSNAME", 200)
                            .SetIsReadOnly();

            grdPMItemMapping.View.AddTextBoxColumn("MAINTITEMID", 150)
                            .SetIsReadOnly();

            grdPMItemMapping.View.AddTextBoxColumn("MAINTITEMNAME", 150)
                            .SetIsReadOnly();

            grdPMItemMapping.View.AddTextBoxColumn("DESCRIPTION", 200)
                            .SetIsHidden();

            grdPMItemMapping.View.AddTextBoxColumn("PLANTID", 150)
                            .SetIsHidden();

            grdPMItemMapping.View.AddTextBoxColumn("ENTERPRISEID", 200)
                        .SetIsHidden();

            grdPMItemMapping.View.AddComboBoxColumn("MAINTTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=MaintType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdPMItemMapping.View.AddSpinEditColumn("MAINTDURATION", 150);

            //CODECLASSID 수정 필!!
            grdPMItemMapping.View.AddComboBoxColumn("MAINTDURATIONUNIT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=MaintdurationUnit", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdPMItemMapping.View.AddComboBoxColumn("MAINTPLANTYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=MaintPlanType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdPMItemMapping.View.AddTextBoxColumn("MAINTPLANDAY", 100);

            grdPMItemMapping.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                            .SetDefault("Valid")
                            .SetValidationIsRequired()
                            .SetTextAlignment(TextAlignment.Center);

            grdPMItemMapping.View.PopulateColumns();

        }
        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            this.Load += EquipmentPMItem_Load;
            this.cboPMList.EditValueChanged += CboPMList_EditValueChanged;
            this.grdEquipment.View.FocusedRowChanged += View_FocusedRowChanged;
            this.grdPMItemNotMapping.View.RowStyle += grdPMItemNotMapping_View_RowStyle;
            this.grdPMItemMapping.View.RowStyle += grdPMItemMapping_View_RowStyle;
            this.btnSelectItemAdd.Click += BtnSelectItemAdd_Click;
            this.btnSelectedItemDelete.Click += BtnSelectedItemDelete_Click;
            this.grdPMItemNotMapping.HeaderButtonClickEvent += GrdPMItemNotMapping_HeaderButtonClickEvent;
        }
        /// <summary>
        /// 매핑 되지 않은 항목을 새로고침 해주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GrdPMItemNotMapping_HeaderButtonClickEvent(object sender, HeaderButtonClickArgs args)
        {
            if (args.Button.Caption == "GridRefresh")
            {
                InitializeComboBox();
                cboPMList.Enabled = true;
            }
        }

        /// <summary>
        /// 선택된 Row가 바뀔 때마다 설비에 매핑된 리스트를 재조회 하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.cboPMList.Enabled = true;
            DataRow row = grdEquipment.View.GetDataRow(e.FocusedRowHandle);
            if (row == null) return;

            _equipmentId = row["EQUIPMENTID"].ToString();

            EquipmentGridFocusedRowChanged();
            NoMappingGridDataBinding();
            RemoveOverlap();
        }

        /// <summary>
        /// 매핑되지 않은 항목 -> 매핑항목으로 이동하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectItemAdd_Click(object sender, EventArgs e)
        {
            SelectItemAdd();
        }

        /// <summary>
        /// 매핑항목 -> 매핑되지 않은 항목으로 이동하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectedItemDelete_Click(object sender, EventArgs e)
        {
            SelectItemDelete();
        }

        /// <summary>
        /// 수정 된 Row색상을 바꿔주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPMItemNotMapping_View_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            e.Appearance.BackColor = Color.Transparent;
        }
        private void grdPMItemMapping_View_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            e.Appearance.BackColor = Color.Transparent;
        }

        /// <summary>
        /// CboPMList 값이 바뀔때 점검항목 재조회하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboPMList_EditValueChanged(object sender, EventArgs e)
        {
            NoMappingGridDataBinding();
        }

        private void EquipmentPMItem_Load(object sender, EventArgs e)
        {
            InitializeComboBox();
            InitializeGridEquipment();
            InitializeGridPMItem();
            InitializeGridPMItemList();

            btnSelectItemAdd.Enabled = false;
            btnSelectedItemDelete.Enabled = false;
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            InitializeDataTable();
         
            DataTable dtMapping = (DataTable)grdPMItemMapping.DataSource;

            DataView dvMapping = dtMapping.DefaultView;

            _saveData = dvMapping.ToTable();

            for (int i = 0; i < _searchData.Rows.Count; i++)
            {
                bool bIsHave = false;

                string strMaintItemBefore = _searchData.Rows[i]["MAINTITEMID"].ToString();
                string strMaintTypeBefore = _searchData.Rows[i]["MAINTTYPE"].ToString();
                int intMaintDurationBefore = Format.GetInteger(_searchData.Rows[i]["MAINTDURATION"]);
                string strMaintDurationUnitBefore = _searchData.Rows[i]["MAINTDURATIONUNIT"].ToString();
                string strDisplaySequenceBefore = _searchData.Rows[i]["MAINTSEQUENCE"].ToString();
                string strValidStateBefore = _searchData.Rows[i]["VALIDSTATE"].ToString();

                for (int j = 0; j < _saveData.Rows.Count; j++)
                {
                    string strMaintItemAfter = _saveData.Rows[j]["MAINTITEMID"].ToString();
                    string strMaintTypeAfter = _saveData.Rows[j]["MAINTTYPE"].ToString();
                    int intMaintDurationAfter = Format.GetInteger(_saveData.Rows[j]["MAINTDURATION"]);
                    string strMaintDurationUnitAfter = _saveData.Rows[j]["MAINTDURATIONUNIT"].ToString();
                    string strDisplaySequenceAfter = _saveData.Rows[j]["MAINTSEQUENCE"].ToString();
                    string strValidStateAfter = _saveData.Rows[j]["VALIDSTATE"].ToString();

                    if (strMaintItemBefore == strMaintItemAfter)
                    {
                        if (strMaintTypeBefore != strMaintTypeAfter || intMaintDurationBefore != intMaintDurationAfter || strMaintDurationUnitBefore != strMaintDurationUnitAfter
                           || strDisplaySequenceBefore != strDisplaySequenceAfter || strValidStateBefore != strValidStateAfter)
                        {
                            DataRow addRow = _realSaveData.NewRow();
                            addRow.ItemArray = _saveData.Rows[j].ItemArray.Clone() as object[];
                            addRow["_STATE_"] = "modified";

                            _realSaveData.Rows.Add(addRow);
                        }
                        else
                        {
                            DataRow addRow = _realSaveData.NewRow();
                            addRow.ItemArray = _saveData.Rows[j].ItemArray.Clone() as object[];
                            addRow["_STATE_"] = "unchanged";

                            _realSaveData.Rows.Add(addRow);
                        }

                        bIsHave = true;
                        break;
                    }
                    else
                    {
                        if (j == _saveData.Rows.Count - 1)
                            bIsHave = false;
                    }
                }

                if (!bIsHave)
                {
                    DataRow addRow = _realSaveData.NewRow();
                    //addRow["CONDITIONITEMGROUPID"] = grdConditionItemGroupList.View.GetFocusedRowCellValue("CONDITIONITEMGROUPID");
                    addRow["_STATE_"] = "deleted";

                    foreach (DataColumn col in _searchData.Columns)
                    {
                        if (col.ColumnName != "_STATE_")
                            addRow[col.ColumnName] = _searchData.Rows[i][col];
                    }

                    _realSaveData.Rows.Add(addRow);

                }
            }

            for (int i = 0; i < _saveData.Rows.Count; i++)
            {
                bool bIsHave = false;

                string strMaintItemBefore = _saveData.Rows[i]["MAINTITEMID"].ToString();

                for (int j = 0; j < _searchData.Rows.Count; j++)
                {
                    string strMaintItemAfter = _searchData.Rows[j]["MAINTITEMID"].ToString();

                    if (strMaintItemBefore == strMaintItemAfter)
                    {
                        bIsHave = true;
                        break;
                    }
                    else
                    {
                        if (j == _searchData.Rows.Count - 1)
                            bIsHave = false;
                    }
                }

                if (!bIsHave)
                {
                    DataRow addRow = _realSaveData.NewRow();
                    addRow.ItemArray = _saveData.Rows[i].ItemArray.Clone() as object[];
                    addRow["_STATE_"] = "added";

                    _realSaveData.Rows.Add(addRow);
                }
            }

            if (_realSaveData == null || _realSaveData.Rows.Count == 0)
                throw MessageException.Create("NoSaveData");

            ExecuteRule("SaveEquipmentPMItemMapping", _realSaveData);

            EquipmentGridFocusedRowChanged();   
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
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtEquipment = await SqlExecuter.QueryAsync("SelectEquipmentPMItem_Equipment", "10001", values);

            if (dtEquipment.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

             grdEquipment.DataSource = dtEquipment;
            NoMappingGridDataBinding();

        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            InitializeCondition_Popup();
        }
        /// <summary>
        /// 공장 조회조건을 user의 plant정보로 설정 및 수정 불가
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            this.Conditions.GetControl<SmartTextBox>("P_PLANTID").EditValue = Framework.UserInfo.Current.Plant;
            this.Conditions.GetControl<SmartTextBox>("P_PLANTID").Enabled = false;
        }

        /// <summary>
        /// 설비그룹 선택하는 팝업
        /// </summary>
        private void InitializeCondition_Popup()
        {
            //팝업 컬럼 설정
            var EquipmentClassPopup = this.Conditions.AddSelectPopup("P_EQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassList", "10001", "EQUIPMENTCLASSTYPE=Production", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                           .SetPopupLayout("EQUIPMENTCLASS", PopupButtonStyles.Ok_Cancel, true, false)
                                           .SetPopupResultCount(1)  //팝업창 선택가능한 개수                 
                                           .SetPopupLayoutForm(800, 500, FormBorderStyle.FixedToolWindow)
                                           .SetLabel("EQUIPMENTCLASS")
                                           .SetPopupAutoFillColumns("EQUIPMENTCLASSNAME")
                                           .SetPosition(2.5);
            //팝업 조회조건
            EquipmentClassPopup.Conditions.AddComboBox("LARGEEQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassListRelationCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                          .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                                          .SetLabel("LARGECLASS"); ;

            EquipmentClassPopup.Conditions.AddComboBox("MIDDLEEQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassListRelationCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "EQUIPMENTCLASSNAME", "EQUIPMENTCLASSID")
                                          .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                                          .SetValidationIsRequiredCondition("LARGEEQUIPMENTCLASSID")
                                          .SetRelationIds("LARGEEQUIPMENTCLASSID")
                                          .SetLabel("MIDDLECLASS");

            EquipmentClassPopup.Conditions.AddTextBox("SMALLEQUIPMENTCLASSID")
                                          .SetLabel("SMALLCLASS");
            //팝업 그리드
            EquipmentClassPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSID", 90);
            EquipmentClassPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSNAME", 130);
            EquipmentClassPopup.GridColumns.AddTextBoxColumn("DESCRIPTION", 90);
            EquipmentClassPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSTYPE", 130);
            EquipmentClassPopup.GridColumns.AddTextBoxColumn("LEQUIPMENTCLASSNAME", 100)
                                           .SetLabel("LARGECLASS");
            EquipmentClassPopup.GridColumns.AddTextBoxColumn("MEQUIPMENTCLASSNAME", 100)
                                           .SetLabel("MIDDLECLASS");
            EquipmentClassPopup.GridColumns.AddTextBoxColumn("VALIDSTATE", 100);       
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
            grdPMItemMapping.View.CheckValidation();

            //DataTable changed = grdPMItemMapping.GetChangedRows();

            //if (changed.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// cboPMList 콤보박스 값이 바뀔 때 마다 매핑되지 않은 항목 데이터 바인딩 하는 함수
        /// </summary>
        private void NoMappingGridDataBinding()
        {
            if (this.cboPMList.EditValue == null) return;


            string maintClassId = this.cboPMList.EditValue.ToString();

            if (_noMappintData == null) return;

           

            var hasTempID = ((DataTable)grdPMItemMapping.DataSource).Rows.Cast<DataRow>()
                        .Where(r => r["MAINTITEMCLASSID"].Equals(maintClassId))
                        .Select(r => r["MAINTITEMID"].ToString())
                        .ToList();

            DataTable realPmItem = ((DataTable)grdPMItemNotMapping.DataSource).Clone();
            realPmItem.Rows.Clear();

            if (hasTempID.Count == 0)
            {// pm유형에 해당하는 저장된 점검항목없을때
                var tmep2 = _noMappintData
                                  .AsEnumerable()
                                  .Where(r => r["MAINTITEMCLASSID"].Equals(maintClassId));
                if (tmep2.ToList().Count == 0)
                {// pm유형에 해당하는 저장된 점검항목없고, pm유형에 해당하는 점검항목없을때

                }
                else
                {// pm유형에 해당하는 저장된 점검항목없고, pm유형에 해당하는 점검항목 있을때
                    realPmItem = tmep2.CopyToDataTable();
                }
            }
            else
            {
                var temp = _noMappintData.AsEnumerable()
                                  .Where(r => r["MAINTITEMCLASSID"].Equals(maintClassId) && !hasTempID.Contains(r["MAINTITEMID"].ToString()));

                if (temp.ToList().Count == 0)
                {// pm유형에 해당하는 저장된 점검항목 제외하고 DataRow없을때

                }
                else
                {
                    realPmItem = temp.CopyToDataTable();
                }

            }

            if (realPmItem.Rows.Count < 1 && string.IsNullOrEmpty(maintClassId))
            {
                btnSelectItemAdd.Enabled = false;
                btnSelectedItemDelete.Enabled = false;

            }
            else
            {
                btnSelectItemAdd.Enabled = true;
                btnSelectedItemDelete.Enabled = true;

            }

            this.grdPMItemNotMapping.DataSource = realPmItem;

        }

        /// <summary>
        ///저장, 추가, 삭제 시 비교한 데이터 테이블 초기화 함수 
        /// </summary>
        private void InitializeDataTable()
        {
            _realSaveData = new DataTable();

            foreach (DataColumn col in ((DataTable)grdPMItemMapping.DataSource).Columns)
            {
                _realSaveData.Columns.Add(col.ColumnName, col.DataType);
            }

            _realSaveData.Columns.Add("_STATE_", typeof(string));

            //_searchData = ((DataTable)this.grdPMItemMapping.DataSource).Clone();
        }


        /// <summary>
        /// 매핑되지 않은 항목 -> 매핑항목으로 이동하는 함수
        /// </summary>
        private void SelectItemAdd()
        {


            DataTable dtMapping = (DataTable)grdPMItemMapping.DataSource;
            DataTable dtNotMapping = (DataTable)grdPMItemNotMapping.DataSource;
            DataTable equipmentdt = grdEquipment.View.GetCheckedRows();

            if (equipmentdt.Rows.Count==0)
            {
                throw MessageException.Create("NoSelectedEquipmentRow");

            }

            List<DataRowView> listAddRows = new List<DataRowView>();

            for (int i = grdPMItemNotMapping.View.RowCount - 1; i >= 0; i--)
            {
                if (!grdPMItemNotMapping.View.IsRowChecked(i))
                    continue;

                DataRowView row = grdPMItemNotMapping.View.GetRow(i) as DataRowView;

                listAddRows.Add(row);
            }
            
            int sequence = (dtMapping.Rows.Count+1) * 10;

         
                foreach (DataRowView row in listAddRows)
                {
                    foreach (DataRow equipmentRow in equipmentdt.Rows)
                     {
                    row["PLANTID"] = equipmentRow["PLANTID"].ToString();
                    row["EQUIPMENTID"] = equipmentRow["EQUIPMENTID"].ToString();
                    row["MAINTSEQUENCE"] = sequence;
                    row["VALIDSTATE"] = "Valid";
                    DataRowView newRow = (grdPMItemMapping.View.DataSource as DataView).AddNew();
                    newRow.BeginEdit();
                    newRow.Row.ItemArray = row.Row.ItemArray.Clone() as object[];
                    newRow.EndEdit();
                    
                }


                sequence += 10;
                row.Delete();
   

               
            }
            dtMapping.AcceptChanges();
            dtNotMapping.AcceptChanges();
 
        }

        /// <summary>
        /// 매핑항목 -> 매핑되지 않은 항목으로 이동하는 함수 
        /// </summary>
        private void SelectItemDelete()
        {
            List<DataRowView> listDeleteRows = new List<DataRowView>();

            DataTable dtMapping = (DataTable)grdPMItemMapping.DataSource;
            DataTable dtNotMapping = (DataTable)grdPMItemNotMapping.DataSource;

            DataRow equipmentRow = grdEquipment.View.GetFocusedDataRow();

            if (_equipmentId.Equals(""))
            {
                throw MessageException.Create("NoSelectedEquipmentRow");
            }

            for (int i = grdPMItemMapping.View.RowCount - 1; i >= 0; i--)
            {
                if (!grdPMItemMapping.View.IsRowChecked(i))
                    continue;

                DataRowView row = grdPMItemMapping.View.GetRow(i) as DataRowView;
     
                listDeleteRows.Add(row);
            }

            foreach (DataRowView row in listDeleteRows)
            {
                row["MAINTDURATION"] = DBNull.Value;
                row["MAINTDURATIONUNIT"] = DBNull.Value;
                row["MAINTSEQUENCE"] = DBNull.Value;

                DataRowView newRow = (grdPMItemNotMapping.View.DataSource as DataView).AddNew();
                newRow.BeginEdit();
                //newRow.Row.ItemArray = row.Row.ItemArray.Clone() as object[];
                newRow["EQUIPMENTID"] = row["EQUIPMENTID"];
                newRow["MAINTITEMCLASSID"] = row["MAINTITEMCLASSID"];
                newRow["MAINTITEMCLASSNAME"] = row["MAINTITEMCLASSNAME"];
                newRow["MAINTITEMID"] = row["MAINTITEMID"];
                newRow["MAINTITEMNAME"] = row["MAINTITEMNAME"];
                newRow["DESCRIPTION"] = row["DESCRIPTION"];





                row.Delete();
                newRow.EndEdit();
            }

            dtMapping.AcceptChanges();
            dtNotMapping.AcceptChanges();

        }

        /// <summary>
        /// 포커스된 설비가 바뀔 때 매핑항목 재조회 함수
        /// </summary>
        private void EquipmentGridFocusedRowChanged()
        {          
            pnlContent.ShowWaitArea();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            param.Add("P_MAINTITEMCLASSID", "");
            param.Add("P_EQUIPMENTID", _equipmentId);
            grdPMItemMapping.DataSource = SqlExecuter.Query("SelectEquipmentPMItem_EquipmentMapping", "10001", param);

            _searchData = ((DataTable)grdPMItemMapping.DataSource).Clone();

            foreach (DataRow row in ((DataTable)grdPMItemMapping.DataSource).Rows)
            {
                DataRow addRow = _searchData.NewRow();
                addRow.ItemArray = row.ItemArray.Clone() as object[];
                _searchData.Rows.Add(addRow);
            }

            pnlContent.CloseWaitArea(); 
        }

        /// <summary>
        /// 포커스된 설비가 바뀔 때 매핑항목 재조회 함수
        /// </summary>
        private void RemoveOverlap()
        {
            DataTable noitem = grdPMItemNotMapping.DataSource as DataTable;
            DataTable item = grdPMItemMapping.DataSource as DataTable;
            if (noitem.Rows.Count == 0 || item.Rows.Count == 0)
                return;          
 
            DataTable itemCopy = item.Copy();



            foreach (DataRow dr in noitem.Rows)
            {
                    
                for(int i= itemCopy.Rows.Count-1;i>=0;i--)
                {

                    if(dr["MAINTITEMCLASSID"].ToString().Equals(itemCopy.Rows[i]["MAINTITEMCLASSID"].ToString()) && 
                        dr["MAINTITEMID"].ToString().Equals(itemCopy.Rows[i]["MAINTITEMID"].ToString()))
                     {
                        item.Rows.RemoveAt(i);
                        break;

                    }


                }



            }




        }
        #endregion
    }
}
