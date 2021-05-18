#region using

using Micube.Framework;
using Micube.SmartMES.Commons;
using Micube.Framework.SmartControls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.Framework.Net;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 에칭레이트 SPEC 관리 Popup
    /// 업  무  설  명  : 에치레이트 규격 등록 메뉴에 규격 등록하는 Popup
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2019-06-24
    /// 수  정  이  력  : 2019-07-15 강유라
    /// 
    /// 
    /// </summary>
    public partial class popupSpecValue : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Interface

        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region Local Variables
        public string _controlRange;//관리범위
        public string _specRange;//규격범위
        public DataTable _specDetailTable;//저장 하기전의 SpecDetailData : detailPopup으로 넘겨주어 수정된 row 덮어쓰기 위함.
        public bool isButtonEnable = true;
        #endregion

        #region 생성자

        public popupSpecValue()
        {
            InitializeComponent();

            //InitializeLanguageKey();
            InitializeEvent();
            InitializeControls();
            InitializeGrid();
            CreateDataTable();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Controler 초기화
        /// </summary>
        private void InitializeControls()
        {
            this.AcceptButton = btnOK;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            txtProcessSegmentClassId.Enabled = false;
            txtEquipmentId.Enabled = false;
            txtChildEquipmentId.Enabled = false;

            cboType.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboType.Editor.ShowHeader = false;
        }

        /// <summary>
        /// Grid 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region Type 관리
            grdSpecList.GridButtonItem = GridButtonItem.Add;
            grdSpecList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdSpecList.View.BestFitColumns();
            grdSpecList.View.SetSortOrder("WORKTYPE");

            //specDef를 저장하지 않고 specDetail을 입력 할경우 key로 쓰기위함
            //addRow를 한 시점의 시각 RESOURCEID에 저장 생성일시 대신 보여줌
            grdSpecList.View.AddTextBoxColumn("RESOURCEID", 150)
                .SetTextAlignment(TextAlignment.Left)
                //.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetLabel("CREATEDTIME")
                .SetIsReadOnly();

            grdSpecList.View.AddTextBoxColumn("WORKTYPE", 100)
                .SetTextAlignment(TextAlignment.Left)
                .SetLabel("TYPECONDITION")
                .SetValidationKeyColumn();

            grdSpecList.View.AddComboBoxColumn("DEFAULTCHARTTYPE", 100, new SqlQuery("GetTypeList", "10001", "CODECLASSID=ControlType", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"))
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
                .SetValidationIsRequired();

            grdSpecList.View.AddTextBoxColumn("SPECRANGE", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left);

            grdSpecList.View.AddTextBoxColumn("CONTROLRANGE", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Left);

            grdSpecList.View.AddTextBoxColumn("DESCRIPTION", 250)
                .SetTextAlignment(TextAlignment.Left);

            grdSpecList.View.AddTextBoxColumn("WORKCONDITION", 50)
                .SetDefault("Disable")
                .SetIsHidden();

            grdSpecList.View.AddTextBoxColumn("SPECSEQUENCE", 50)
                .SetIsHidden();

            grdSpecList.View.AddTextBoxColumn("SPECCLASSID", 50)
                .SetIsHidden();

            grdSpecList.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 50)
                .SetIsHidden();

            grdSpecList.View.AddTextBoxColumn("EQUIPMENTID", 50)
                .SetIsHidden();

            grdSpecList.View.AddTextBoxColumn("CHILDEQUIPMENTID", 50)
                .SetIsHidden();

            grdSpecList.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();

            grdSpecList.View.AddTextBoxColumn("PLANTID", 150)
                .SetIsHidden();

            grdSpecList.View.PopulateColumns();

            RepositoryItemTimeEdit edit = new RepositoryItemTimeEdit();

            //edit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
           // edit.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            //edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            //edit.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";


            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            edit.Mask.EditMask = "[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1]) (2[0-3]|[01][0-9]):[0-5][0-9]:[0-5][0-9]";
            edit.Mask.UseMaskAsDisplayFormat = true;
            grdSpecList.View.Columns["RESOURCEID"].ColumnEdit = edit;

            #endregion

            #region History

            grdHistory.GridButtonItem = GridButtonItem.None;
            grdHistory.View.SetIsReadOnly();
            grdHistory.View.BestFitColumns();
            grdHistory.View.SetSortOrder("TXNHISTKEY",DevExpress.Data.ColumnSortOrder.Descending);

            grdHistory.View.AddTextBoxColumn("TXNHISTKEY", 80)
                .SetIsHidden();

            grdHistory.View.AddTextBoxColumn("WORKTYPE", 80)
                .SetTextAlignment(TextAlignment.Left)
                .SetLabel("TYPECONDITION");

            grdHistory.View.AddTextBoxColumn("SPECRANGE", 100)
                .SetTextAlignment(TextAlignment.Left);

            grdHistory.View.AddTextBoxColumn("CONTROLRANGE", 100)
                .SetTextAlignment(TextAlignment.Left);

            grdHistory.View.AddTextBoxColumn("CHANGEREASON", 250)
                .SetTextAlignment(TextAlignment.Left)
                .SetLabel("REASONFORCHANGE");

            grdHistory.View.AddTextBoxColumn("CREATOR", 80)
                .SetTextAlignment(TextAlignment.Center);

            grdHistory.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center);

            grdHistory.View.AddTextBoxColumn("MODIFIER", 80)
                .SetTextAlignment(TextAlignment.Center);

            grdHistory.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center);

            grdHistory.View.PopulateColumns();

            #endregion
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            this.LanguageKey = "ETCHINGRATESPECMGNT";

            tabHistory.Text = Language.Get("TYPEREGISTRATION");
            tabSpec.Text = Language.Get("TYPECHOICEHISTORY");

            txtProcessSegmentClassId.LanguageKey = "PROCESSSEGMENT";
            txtEquipmentId.LanguageKey = "EQUIPMENTUNIT";
            txtChildEquipmentId.LanguageKey = "CHILDEQUIPMENT";
            txtReasons.LanguageKey = "REASONFORCHANGE";

            grdSpecList.LanguageKey = "TYPELIST";
            grdHistory.LanguageKey = "LIST";

            cboType.LanguageKey = "TYPECHOICE";

            btnSave.LanguageKey = "SAVE";
            btnOK.LanguageKey = "OK";
        }

        #endregion

        #region Event

        /// <summary>
        /// Event 초기화 
        /// </summary>
        private void InitializeEvent()
        {

            //Add Row 가 있을 때 Delete버튼 생성
            new SetGridDeleteButonVisibleSimple(grdSpecList);

            Load += (s , e) =>
            {
                SearchSpecDef();
                BindingComboData();
                SearchSpecDefHistory();
                btnOK.Enabled = isButtonEnable;
                btnSave.Enabled = isButtonEnable;
            };

            //새 row 추가 시  plantId, EnterpriseId, resourceId, specClassId 부여
            grdSpecList.View.AddingNewRow += (s, e) =>
            {
                e.NewRow["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;
                e.NewRow["PLANTID"] = Framework.UserInfo.Current.Plant;
                e.NewRow["RESOURCEID"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff");//key로 사용 ms까지구분
                e.NewRow["SPECCLASSID"] = "EtchingRateSpec";
            };

            // Spec 입력하기 위한 이벤트 (popup)
            grdSpecList.View.RowCellClick += View_RowCellClick;
          
            // Type 관리 항목 저장
            btnSave.Click += (s, e) =>
            {
                SaveData();
                BindingComboData();
            };

            // 확인 버튼 이벤트
            btnOK.Click += BtnOK_Click;
            //DefalutChartType 이 바뀔 때 이벤티
            grdSpecList.View.CellValueChanged += View_CellValueChanged;
        }

        /// <summary>
        /// DefalutChartType 에 해당하는 Spec값 있으면 보여주기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "DEFAULTCHARTTYPE")
            {    //저장 하기 전 테이블에 해당 차트타입의 데이터가 있는지 검색          
                var beforeSave = _specDetailTable.Rows.Cast<DataRow>()
                .Where(r => r["CONTROLTYPE"].ToString().Equals(e.Value.ToString()) && r["RESOURCEID"].ToString().Equals(grdSpecList.View.GetFocusedRowCellValue("RESOURCEID").ToString())).ToList();

                if (beforeSave.Count > 0)
                {//데이터가 있다면 수정된 데이터(저장전)로 specRange,controlRange입력
                    DataRow beforeRow = beforeSave.CopyToDataTable().Rows[0];
                    grdSpecList.View.SetFocusedRowCellValue("SPECRANGE", beforeRow["LSL"] + "~" + beforeRow["USL"] + "(" + beforeRow["SL"] + ")");
                    grdSpecList.View.SetFocusedRowCellValue("CONTROLRANGE", beforeRow["LCL"] + "~" + beforeRow["UCL"] + "(" + beforeRow["CL"] + ")");
                }
                else
                {//저장 하기 전 테이블에 수정된 데이터가 없다면 저장되어있는(DB) 데이터 있는지 확인
                    DataRow row = grdSpecList.View.GetFocusedDataRow();

                    Dictionary<string, object> values = new Dictionary<string, object>();
                    values.Add("SPECSEQUENCE", row["SPECSEQUENCE"]);
                    values.Add("DEFAULTCHARTTYPE", e.Value);
                    values.Add("SPECCLASSID", row["SPECCLASSID"]);

                    DataTable dt = SqlExecuter.Query("GetSpecByDefaultChartType", "10001", values);
                    if (dt.Rows.Count > 0)
                    {//저장된 데이터 있을 때
                        grdSpecList.View.SetFocusedRowCellValue("SPECRANGE", dt.Rows[0]["SPECRANGE"]);
                        grdSpecList.View.SetFocusedRowCellValue("CONTROLRANGE", dt.Rows[0]["CONTROLRANGE"]);
                    }
                    else
                    {//저장된 데이터 없을 때
                        grdSpecList.View.SetFocusedRowCellValue("SPECRANGE", null);
                        grdSpecList.View.SetFocusedRowCellValue("CONTROLRANGE", null);
                    }
                }

            }
        }

        /// <summary>
        /// 규격범위, 관리범위 cell 더블클릭 시에만 specDetail을 입력할 수 있는 popup open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Clicks == 2 && (e.Column.FieldName == "SPECRANGE" || e.Column.FieldName == "CONTROLRANGE"))
            {
                DataRow dr = grdSpecList.View.GetFocusedDataRow();

                if (dr == null)
                {
                    return;
                }

                if (string.IsNullOrEmpty(dr["WORKTYPE"].ToString()) || string.IsNullOrEmpty(dr["DEFAULTCHARTTYPE"].ToString()))
                {//WORKTYPE과 DEFAULTCHARTTYPE을 입력하지않으면 specDetail 입력불가
                    return;
                }

                EtchingRateSpecDetailPopup detailPopup = new EtchingRateSpecDetailPopup()
                {
                    StartPosition = FormStartPosition.CenterParent,
                    Owner = this
                };

                detailPopup.CurrentDataRow = dr;
                //DetailPopup의 _beforeSave 데이타테이블에 저장되기전 specDetail 데이타테이블 넘겨주기
                //chartType combo를 바꿀 때 저장 전의 데이타를 덮어씌어 보여주기 위함
                detailPopup._beforeSave = _specDetailTable;
                detailPopup.SearchSpecDetail();

                if (detailPopup.ShowDialog() == DialogResult.OK)
                {
                    if (dr["DEFAULTCHARTTYPE"].Equals(detailPopup._chartType))
                    {//DEFAULTCHARTTYPE과 입력 한 스펙의 CHARTTYPE이 같을때
                        if (!detailPopup._controlRange.Equals("~()"))
                        {//SPEC값이 있을 때
                            dr["CONTROLRANGE"] = detailPopup._controlRange;
                            _controlRange = detailPopup._controlRange;
                        }

                        if (!detailPopup._specRange.Equals("~()"))
                        {//SPEC값이 있을 때
                            dr["SPECRANGE"] = detailPopup._specRange;
                            _specRange = detailPopup._specRange;
                        }

                        if(!dr.RowState.Equals(DataRowState.Added) && !dr.RowState.Equals(DataRowState.Modified))
                        {
                            //관리번위와 규격범위가 바뀌었을때는 rowState를 unchanged로 하기위함
                            (grdSpecList.DataSource as DataTable).AcceptChanges();
                        }

                    }
                    else
                    {
                        _controlRange = null;
                        _specRange = null;
                    }

                    //specDetailPopup의 입력된 row를 _specDetailTable에 입력
                    DataRow newRow = _specDetailTable.NewRow();
                    newRow.ItemArray = detailPopup._specDetailRow.ItemArray.Clone() as object[];
                    DeleteRow(_specDetailTable, newRow);
                    _specDetailTable.Rows.Add(newRow);
                }
            }
        }

        /// <summary>
        /// 팝업 확인 버튼 클릭시 이벤트 
        /// 선택된 Type을 Enable 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            object choiceType = cboType.EditValue;//combo의 선택 한 값


            bool isChanged = false;

            foreach (DataRow row in (grdSpecList.DataSource as DataTable).Rows)
            {//grdSpecList의 데이터중 저장 되지않은 데이터가 있는지 체크
                if (!row.RowState.Equals(DataRowState.Unchanged))
                {
                    isChanged = true;
                    break;
                }
            }

            if (isChanged == true)
            {
                DialogResult result = System.Windows.Forms.DialogResult.No;

                result = this.ShowMessage(MessageBoxButtons.YesNo, "HasNoSavedDataWantToSave");//저장되지않은 데이터가 있습니다. 저장 후 Type을 지정 하시겠습니까?

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveData();
                }
                else
                {
                    return;
                }
            }

            BindingComboData();

            cboType.EditValue = choiceType == null? cboType.EditValue:choiceType;

            if (string.IsNullOrWhiteSpace(cboType.EditValue.ToString()))
            {
                throw MessageException.Create("NoSelectedWorkType");//선택된 Type이 없습니다.
            }


            if (string.IsNullOrWhiteSpace(cboType.Properties.GetDataSourceValue("LSL", cboType.Properties.GetDataSourceRowIndex("SPECSEQUENCE", cboType.EditValue)).ToString())
                && string.IsNullOrWhiteSpace(cboType.Properties.GetDataSourceValue("USL", cboType.Properties.GetDataSourceRowIndex("SPECSEQUENCE", cboType.EditValue)).ToString())
                && string.IsNullOrWhiteSpace(cboType.Properties.GetDataSourceValue("SL", cboType.Properties.GetDataSourceRowIndex("SPECSEQUENCE", cboType.EditValue)).ToString()))
            {//규격 범위가 등록 되지 않은 Type 선택불가
                ShowMessage("NoSpecRangeType");//규격 범위가 없는 Type 입니다
                return;
            }
            
            CurrentDataRow["SPECRANGE"] = cboType.Properties.GetDataSourceValue("SPECRANGE", cboType.Properties.GetDataSourceRowIndex("SPECSEQUENCE", cboType.EditValue));
            CurrentDataRow["CONTROLRANGE"] = cboType.Properties.GetDataSourceValue("CONTROLRANGE", cboType.Properties.GetDataSourceRowIndex("SPECSEQUENCE", cboType.EditValue));
            CurrentDataRow["DESCRIPTION"] = cboType.Properties.GetDataSourceValue("DESCRIPTION", cboType.Properties.GetDataSourceRowIndex("SPECSEQUENCE", cboType.EditValue));
            CurrentDataRow["DEFAULTCHARTTYPE"] = cboType.Properties.GetDataSourceValue("DEFAULTCHARTTYPE", cboType.Properties.GetDataSourceRowIndex("SPECSEQUENCE", cboType.EditValue));

            CurrentDataRow["CHANGEREASON"] = txtReasons.EditValue;
            CurrentDataRow["WORKTYPE"] = cboType.Properties.GetDisplayValueByKeyValue(cboType.EditValue);
            CurrentDataRow["SPECSEQUENCE"] = cboType.EditValue;
            CurrentDataRow["WORKCONDITION"] = "Enable";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        #endregion

        #region Private Function
        /// <summary>
        /// grdSpecList의 데이터를 조회하는 함수
        /// </summary>
        private void SearchSpecDef()
        {
            try
            {
                this.ShowWaitArea();
                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("P_PROCESSSEGMENTCLASSID", CurrentDataRow["PROCESSSEGMENTCLASSID"]);
                values.Add("P_EQUIPMENTID", CurrentDataRow["EQUIPMENTID"]);
                values.Add("P_CHILDEQUIPMENTID", CurrentDataRow["CHILDEQUIPMENTID"]);

                grdSpecList.DataSource = SqlExecuter.Query("SelectEtchingRateSpecDef", "10001", values);
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

        /// <summary>
        /// grdHistoryd의 데이터를 조회하는 함수
        /// </summary>
        private void SearchSpecDefHistory()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("P_PROCESSSEGMENTCLASSID", CurrentDataRow["PROCESSSEGMENTCLASSID"]);
            values.Add("P_EQUIPMENTID", CurrentDataRow["EQUIPMENTID"]);
            values.Add("P_CHILDEQUIPMENTID", CurrentDataRow["CHILDEQUIPMENTID"]);

            grdHistory.DataSource = SqlExecuter.Query("SelectEtchingRateSpecDefHistory", "10001", values);
        }

        /// <summary>
        /// 저장하기 전에 중공정, 설비, 설비단을 dataTable에 할당해주는 함수
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private DataTable SetKeyBeforeSave(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                row["PROCESSSEGMENTCLASSID"] = CurrentDataRow["PROCESSSEGMENTCLASSID"];
                row["EQUIPMENTID"] = CurrentDataRow["EQUIPMENTID"];
                row["CHILDEQUIPMENTID"] = CurrentDataRow["CHILDEQUIPMENTID"];
            }

            return table;
        }
        /// <summary>
        /// cboType의 콤보박스 데이터를 바인딩하는 함수
        /// </summary>
        private void BindingComboData()
        {
            DataTable dt = (grdSpecList.DataSource as DataTable).Copy();
            dt.DefaultView.Sort = "WORKTYPE ASC";
            dt = dt.DefaultView.ToTable();
            cboType.Properties.DataSource = dt;

            cboType.Properties.ValueMember = "SPECSEQUENCE";
            cboType.Properties.DisplayMember = "WORKTYPE";

            //grdSpecList의 DataSource중 콤보에 보여주지 않을 컬럼 설정
            cboType.Properties.PopulateColumns();
            cboType.Properties.Columns["CREATEDTIME"].Visible = false;
            cboType.Properties.Columns["DEFAULTCHARTTYPE"].Visible = false;
            cboType.Properties.Columns["SPECRANGE"].Visible = false;
            cboType.Properties.Columns["CONTROLRANGE"].Visible = false;
            cboType.Properties.Columns["DESCRIPTION"].Visible = false;
            cboType.Properties.Columns["WORKCONDITION"].Visible = false;
            cboType.Properties.Columns["SPECSEQUENCE"].Visible = false;
            cboType.Properties.Columns["SPECCLASSID"].Visible = false;
            cboType.Properties.Columns["PROCESSSEGMENTCLASSID"].Visible = false;
            cboType.Properties.Columns["PROCESSSEGMENTCLASSNAME"].Visible = false;
            cboType.Properties.Columns["EQUIPMENTID"].Visible = false;
            cboType.Properties.Columns["EQUIPMENTNAME"].Visible = false;
            cboType.Properties.Columns["CHILDEQUIPMENTID"].Visible = false;
            cboType.Properties.Columns["CHILDEQUIPMENTNAME"].Visible = false;
            cboType.Properties.Columns["ENTERPRISEID"].Visible = false;
            cboType.Properties.Columns["PLANTID"].Visible = false;
            cboType.Properties.Columns["CHANGEREASON"].Visible = false;
            cboType.Properties.Columns["FLAG"].Visible = false;
            cboType.Properties.Columns["VALIDSTATE"].Visible = false;
            cboType.Properties.Columns["RESOURCEID"].Visible = false;
            cboType.Properties.Columns["ISMODIFY"].Visible = false;
            cboType.Properties.Columns["LSL"].Visible = false;
            cboType.Properties.Columns["USL"].Visible = false;
            cboType.Properties.Columns["SL"].Visible = false;

            if (string.IsNullOrWhiteSpace(CurrentDataRow["SPECSEQUENCE"].ToString()))
            {//이전에 설정된 workType이 없을때
                cboType.Editor.ItemIndex = 0;
            }
            else
            {//이전에 설정된 workType이 있을때
                cboType.EditValue = CurrentDataRow["SPECSEQUENCE"].ToString();
            }

            //Type 콤보박스의 값이 바뀔 때 이벤트 
            //변경사유를 null로 바꿔준다 기존 값을 보여준후 이벤트 실행해야함
            //cboType.Editor.EditValueChanged += (s, e) =>
            //{
            //    if (!cboType.EditValue.Equals(CurrentDataRow["SPECSEQUENCE"].ToString()))
            //    {
            //        txtReasons.EditValue = "";
            //    }
            //};
    
        }

        #endregion

        #region Public Function

        /// <summary>
        /// 화면에 Data를 Setting 한다
        /// </summary>
        /// 
        public void SetData()
        {
            txtProcessSegmentClassId.EditValue = CurrentDataRow["PROCESSSEGMENTCLASSNAME"];
            txtEquipmentId.EditValue = CurrentDataRow["EQUIPMENTNAME"];
            txtChildEquipmentId.EditValue = CurrentDataRow["CHILDEQUIPMENTNAME"];

            cboType.Editor.SelectedText = CurrentDataRow["WORKTYPE"].ToString();
            //txtReasons.EditValue = CurrentDataRow["CHANGEREASON"];
        }

        /// <summary>
        /// SpecDetail을 저장하기 위한 테이블 생성 함수
        /// popup을 열 때 마다 초기화 한다
        /// </summary>
        /// <returns></returns>
        private DataTable CreateDataTable()
        {
            _specDetailTable = new DataTable();
            _specDetailTable.Columns.Add(new DataColumn("CONTROLTYPE", typeof(string)));
            _specDetailTable.Columns.Add(new DataColumn("LSL", typeof(Decimal)));
            _specDetailTable.Columns.Add(new DataColumn("USL", typeof(Decimal)));
            _specDetailTable.Columns.Add(new DataColumn("SL", typeof(Decimal)));
            _specDetailTable.Columns.Add(new DataColumn("LCL", typeof(Decimal)));
            _specDetailTable.Columns.Add(new DataColumn("UCL", typeof(Decimal)));
            _specDetailTable.Columns.Add(new DataColumn("CL", typeof(Decimal)));
            _specDetailTable.Columns.Add(new DataColumn("UOL", typeof(Decimal)));
            _specDetailTable.Columns.Add(new DataColumn("LOL", typeof(Decimal)));
            _specDetailTable.Columns.Add(new DataColumn("HASDATA", typeof(string)));
            _specDetailTable.Columns.Add(new DataColumn("RESOURCEID", typeof(string)));
            _specDetailTable.Columns.Add(new DataColumn("SPECCLASSID", typeof(string)));
            _specDetailTable.Columns.Add(new DataColumn("SPECSEQUENCE", typeof(string)));
            _specDetailTable.Columns.Add(new DataColumn("_STATE_", typeof(string)));
            _specDetailTable.Columns.Add(new DataColumn("PLANTID", typeof(string)));
            _specDetailTable.Columns.Add(new DataColumn("ENTERPRISEID", typeof(string)));

            return _specDetailTable;
        }

        /// <summary>
        /// 수정된 chartType의 데이터를 다시열었다가 수정하지 않고 저장 할 경우
        /// 저장 되기 전의 specDetail 테이블의 중복을 제거 하기 위한 함수
        /// </summary>
        /// <param name="table"></param>
        /// <param name="afterRow"></param>
        /// <returns></returns>
        private DataTable DeleteRow(DataTable table, DataRow afterRow)
        {
            foreach (DataRow BeforeRow in table.Rows)
            {
                if (BeforeRow["RESOURCEID"] == afterRow["RESOURCEID"] &&
                    BeforeRow["CONTROLTYPE"].ToString() == afterRow["CONTROLTYPE"].ToString())
                {
                    table.Rows.Remove(BeforeRow);
                    break;
                }

            }

            return table;
        }

        /// <summary>
        /// 데이터를 저장하는 함수
        /// </summary>
        private void SaveData()
        {
            grdSpecList.View.CheckValidation();

            DataTable dt = grdSpecList.GetChangedRows();

            if (dt.Rows.Count == 0 && _specDetailTable.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
            else
            {
                /*var noSpecDetailCount = dt.AsEnumerable()
                .Where(r => string.IsNullOrWhiteSpace(r["SPECRANGE"].ToString()))
                .ToList().Count;*/

                foreach (DataRow specRow in dt.Rows)
                {
                    
                    //specDef의 resourceid 와 controlType이 동일 한것의 갯수
                    var SpecDetailList = _specDetailTable.Copy().AsEnumerable()
                    .Where(r => Format.GetString(specRow["RESOURCEID"]).Equals(Format.GetString(r["RESOURCEID"]))
                    && Format.GetString(specRow["DEFAULTCHARTTYPE"]).Equals(Format.GetString(r["CONTROLTYPE"])))
                    .ToList();

                    //신규 등록일때
                    if (Format.GetString(specRow["_STATE_"]).Equals("added"))
                    {
                        if (SpecDetailList.Count == 0)
                        {//specDef를 등록했는데 detailRow가 없을때
                            throw MessageException.Create("NoSpecRangeType");//규격 범위가 없는 Type 입니다
                        }
                        else
                        {//detailRow가 있지만, USL,SL,LSL 모두 없는 경우
                            var noSpecDetailCount = SpecDetailList.CopyToDataTable().AsEnumerable()
                                .Where(r => string.IsNullOrWhiteSpace(Format.GetString(r["USL"]))
                            && string.IsNullOrWhiteSpace(Format.GetString(r["SL"])) && string.IsNullOrWhiteSpace(Format.GetString(r["LSL"])))
                            .ToList().Count;

                            if (noSpecDetailCount > 0)
                            {
                                throw MessageException.Create("NoSpecRangeType");//규격 범위가 없는 Type 입니다
                            }
                        }
                    }
                    else if (Format.GetString(specRow["_STATE_"]).Equals("modified"))
                    {
                        if (SpecDetailList.Count == 0)
                        {//specDef를 등록했는데 detailRow가 없을때

                            Dictionary<string, object> values = new Dictionary<string, object>();
                            values.Add("SPECSEQUENCE", specRow["SPECSEQUENCE"]);
                            values.Add("DEFAULTCHARTTYPE", specRow["DEFAULTCHARTTYPE"]);
                            values.Add("SPECCLASSID", specRow["SPECCLASSID"]);

                            DataTable specDetail = SqlExecuter.Query("GetSpecByDefaultChartType", "10001", values);

                            if(specDetail.Rows.Count == 0)
                            throw MessageException.Create("NoSpecRangeType");//규격 범위가 없는 Type 입니다
                        }                       
                    }
                }

            }

            //Send Datasource
            DataSet sendDataSet = new DataSet();

            dt = SetKeyBeforeSave(dt);
            DataTable copySpecDetailTail = _specDetailTable.Copy();

            dt.TableName = "list";
            copySpecDetailTail.TableName = "detail";

            sendDataSet.Tables.Add(dt);
            sendDataSet.Tables.Add(copySpecDetailTail);

            ExecuteRule("SaveEtchingRateSpecDef", sendDataSet);

            SearchSpecDef();
            ShowMessage("SuccessSave");
            _specDetailTable.Clear();//저장완료후 specDetail 데이터 초기화
        }
        #endregion
    }
}