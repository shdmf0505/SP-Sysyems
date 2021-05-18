using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.Framework.SmartControls.Grid.Conditions;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using Micube.SmartMES.QualityAnalysis.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 수입 검사 관리 > 공정 수입검사 결과등록
    /// 업  무  설  명  : 공정수입검사 결과등록하는 팝업
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-09-05
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ProcessImportInspectionResultPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Interface

        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region Local Variables
        public DataTable _exteriorTable;
        public DataTable _measureTable;
        public DataTable _defectTable;
        private DataTable _fileDt;
        private bool _hasImage = true;
        private SmartBandedGrid _grid;//선택된 탭에 따라 다른 grd객체
        private SmartPictureEdit _commonPic;//선택된 탭에 따라 다른 picBox객체 
        private SmartPictureEdit _commonPic2;//선택된 탭에 따라 다른 picBox객체 
        public string _type;//insert or update인지 여부
        private string _isLocking = "N";//NCR 자동 locking 처리 여부
        private DataRow _lotRow;
        private int _raidOldSelected;
        private DataSet measureDs;
        private DataTable _selectMeasureValueDt;
        private double _inspectionQty;
        private string _toResourceId = "";
        private string _toAreaId = "";
        //bool _autoChange = false;
        private double _lotQty = 0.0;
        public bool isEnable = true;
        private ProcessImportInspectionResult _parent; // 2020.02.24-유석진-(결과등록)외주입고품 현황 화면 인자로 받음


        //2020-02-19 강유라 이메일 첨부용
        DataTable _FileToSendEmail;

        //2020-03-24 강유라
        private string processDefType = "";
        private string lastRework = "";

        #endregion

        #region 생성자
        public ProcessImportInspectionResultPopup(string type, ProcessImportInspectionResult parent)
        {
            _type = type;
            _parent = parent; // 2020.02.24-유석진-(결과등록)외주입고품 현황 화면 인자로 받음
            InitializeComponent();
            SettingLotgrd();
        }
        #endregion

        #region 컨텐츠 영역 초기화

        #region 그리드 초기화
        private void InitializeExteriorGrid(string _type)
        {
            grdInspectionItem.View.ClearColumns();

            #region 외관검사 탭 초기화
            if (_type.Equals("insertData"))
            {
                grdInspectionItem.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
                //grdInspectionItem.View.OptionsView.AllowCellMerge = true;
            }
            else if (_type.Equals("updateData"))
            {
                grdInspectionItem.GridButtonItem = GridButtonItem.None;
            }
            //구분 등 검사항목
            var item = grdInspectionItem.View.AddGroupColumn("");

            //불량코드
            item.AddTextBoxColumn("DEFECTCODE", 150)
                .SetIsHidden();

            //불량코드명
            InitializeGrid_DefectCodeListPopup(item);

            //품질공정
            item.AddTextBoxColumn("QCSEGMENTID", 100)
                .SetIsHidden();

            item.AddTextBoxColumn("QCSEGMENTNAME", 250)
                .SetIsReadOnly();

            //검사 수량
            var groupInspQty = grdInspectionItem.View.AddGroupColumn("INSPECTIONQTY");

            if (cboStandardType.Editor.EditValue.ToString().Equals("AQL"))
            {
                //PCS
                groupInspQty.AddSpinEditColumn("INSPECTIONQTY", 150)
                    .SetTextAlignment(TextAlignment.Right)
                    .SetLabel("PCS")
                    .SetIsReadOnly();

                //PNL
                groupInspQty.AddSpinEditColumn("INSPECTIONQTYPNL", 150)
                    .SetTextAlignment(TextAlignment.Right)
                    .SetIsReadOnly()
                    .SetLabel("PNL");
            }
            else if (cboStandardType.Editor.EditValue.ToString().Equals("NCR"))
            {
                //PCS
                groupInspQty.AddSpinEditColumn("INSPECTIONQTY", 150)
                    .SetTextAlignment(TextAlignment.Right)
                    .SetLabel("PCS");

                //PNL
                groupInspQty.AddSpinEditColumn("INSPECTIONQTYPNL", 150)
                    .SetTextAlignment(TextAlignment.Right)
                    .SetLabel("PNL");

            }

            //불량수량
            var groupSpecOutQty = grdInspectionItem.View.AddGroupColumn("DEFECTQTY");
            //PCS
            groupSpecOutQty.AddSpinEditColumn("DEFECTQTY", 150)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("PCS");
            //PNL
            groupSpecOutQty.AddSpinEditColumn("DEFECTQTYPNL", 150)
                .SetTextAlignment(TextAlignment.Right)
                //.SetIsReadOnly()
                .SetLabel("PNL");

            //결과
            var result = grdInspectionItem.View.AddGroupColumn("");

            result.AddTextBoxColumn("DEFECTRATE", 150)
                .SetIsReadOnly()
               // .SetDisplayFormat("{###.#:P0}", MaskTypes.Numeric);
               .SetDisplayFormat("###.#", MaskTypes.Numeric);


            result.AddComboBoxColumn("INSPECTIONRESULT", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            result.AddTextBoxColumn("TXNHISTKEY", 300)
                .SetIsHidden();

            result.AddTextBoxColumn("RESOURCEID", 300)
                .SetIsHidden();

            result.AddTextBoxColumn("RESOURCETYPE", 300)
                .SetIsHidden();

            result.AddTextBoxColumn("PROCESSRELNO", 300)
                .SetIsHidden();

            grdInspectionItem.View.PopulateColumns();
            #endregion

        }

        #region
        /// <summary>
        /// 불량 코드 팝업
        /// </summary>
        private void InitializeGrid_DefectCodeListPopup(ConditionItemGroup item)
        {
            var defectCodePopupColumn = item.AddSelectPopupColumn("DEFECTCODENAME", 250, new SqlQuery("GetOSPInspectionDefectCodeToInsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("SELECTDEFECTCODEID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(1000, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("DEFECTCODENAME")
                .SetLabel("DEFECTCODENAME")
                .SetPopupApplySelection((selectedRows, dataGridRows) =>
                {
                        // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                        // dataGridRow : 현재 Focus가 있는 그리드의 DataRow


                    DataTable dt = grdInspectionItem.DataSource as DataTable;
                    DataRow dr = selectedRows.FirstOrDefault();

                    if (dr != null)
                    {
                        int icnt = dt.Select("DEFECTCODE = '" + dr["DEFECTCODE"].ToString() + "'").Count();
                        int icntQCId = dt.Select("QCSEGMENTID = '" + dr["QCSEGMENTID"].ToString() + "'").Count();
                        if (icnt > 0 && icntQCId > 0)

                        {
                            throw MessageException.Create("DuplicationDefectCode");
                        }

                        dataGridRows["DEFECTCODE"] = dr["DEFECTCODE"].ToString();
                        dataGridRows["QCSEGMENTID"] = dr["QCSEGMENTID"].ToString();
                        dataGridRows["QCSEGMENTNAME"] = dr["QCSEGMENTNAME"].ToString();
                        dataGridRows["DECISIONDEGREE"] = dr["DECISIONDEGREE"].ToString();

                        dataGridRows["AQLINSPECTIONLEVEL"] = dr["AQLINSPECTIONLEVEL"].ToString();
                        dataGridRows["AQLDEFECTLEVEL"] = dr["AQLDEFECTLEVEL"].ToString();
                        dataGridRows["AQLDECISIONDEGREE"] = dr["AQLDECISIONDEGREE"].ToString();
                    }

                        /*
                        foreach (DataRow row in selectedRows)
                        {
                            dataGridRows["DEFECTCODE"] = row["DEFECTCODE"].ToString();
                            dataGridRows["QCSEGMENTID"] = row["QCSEGMENTID"].ToString();
                            dataGridRows["QCSEGMENTNAME"] = row["QCSEGMENTNAME"].ToString();
                        }
                        */

                });

            // 팝업의 검색조건 항목 추가 (불량코드/명)
            defectCodePopupColumn.Conditions.AddTextBox("TXTDEFECTCODE").
                SetLabel("DEFECTCODE");
            // 팝업의 검색조건 항목 추가 (품질공정id/명)
            defectCodePopupColumn.Conditions.AddTextBox("TXTQCSEGMENT")
                .SetLabel("QCSEGMENT");

            defectCodePopupColumn.GridColumns.AddTextBoxColumn("DEFECTCODE", 100)
                .SetIsReadOnly();
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("DEFECTCODENAME", 150)
                .SetIsReadOnly();
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("QCSEGMENTID", 100)
                .SetIsReadOnly();
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("QCSEGMENTNAME", 150)
                .SetIsReadOnly();

            defectCodePopupColumn.GridColumns.AddTextBoxColumn("DECISIONDEGREE", 80)
                .SetIsReadOnly();
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("AQLINSPECTIONLEVEL", 80)
                .SetIsReadOnly();
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("AQLDEFECTLEVEL", 80)
                .SetIsReadOnly();
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("AQLDECISIONDEGREE", 80)
                .SetIsReadOnly();
        }
        #endregion
        private void InitializeGrid(string type)
        { 
            #region 측정검사 탭 초기화

            #region 검사 항목 그리드 초기화

            grdInspectionItemSpec.GridButtonItem = GridButtonItem.None;
            grdInspectionItemSpec.View.OptionsView.AllowCellMerge = true;

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMID", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMNAME", 250)
                .SetIsReadOnly();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPECTIONSTANDARD", 250)
                .SetIsReadOnly();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPECTIONRESULT", 250)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMVERSION", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMTYPE", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("UPPERSPECLIMIT", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("LOWERSPECLIMIT", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("LOWERCONTROLLIMIT", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("UPPERCONTROLLIMIT", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.PopulateColumns();

            #endregion

            #region 측정값 그리드 초기화

            if (type.Equals("updateData"))
            {
                grdMeasuredValue.GridButtonItem = GridButtonItem.None;
            }
            else
            {
                grdMeasuredValue.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            }
            grdMeasuredValue.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMeasuredValue.View.AddTextBoxColumn("NO", 200)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("INSPITEMID", 200)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("INSPITEMNAME", 200)
                .SetIsReadOnly();

            grdMeasuredValue.View.AddTextBoxColumn("INSPITEMVERSION", 200)
                .SetIsHidden();

            grdMeasuredValue.View.AddSpinEditColumn("MEASUREVALUE", 150)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,0.######", MaskTypes.Numeric);

            grdMeasuredValue.View.AddTextBoxColumn("LOWERSPECLIMIT", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("UPPERSPECLIMIT", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("LOWERCONTROLLIMIT", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("UPPERCONTROLLIMIT", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("TARGETVALUE", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("UPPERSCREENLIMIT", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("LOWERSCREENLIMIT", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddComboBoxColumn("INSPECTIONRESULT", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdMeasuredValue.View.AddTextBoxColumn("TXNHISTKEY", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("RESOURCEID", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("RESOURCETYPE", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("PROCESSRELNO", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("NCRDECISIONDEGREE", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("QCGRADE", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("PRIORITY", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("DEGREE", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("ENTERPRISEID", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("PLANTID", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("ISDELETE", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("INSPECTIONSTANDARD", 300)
              .SetIsHidden();
      

            grdMeasuredValue.View.PopulateColumns();

            #endregion

            #endregion
        }

        /// <summary>
		/// 합계 Row 초기화
		/// </summary>
		private void InitializationSummaryRow()
        {
            grdInspectionItem.View.Columns["DEFECTCODE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInspectionItem.View.Columns["DEFECTCODE"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdInspectionItem.View.Columns["INSPECTIONQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdInspectionItem.View.Columns["INSPECTIONQTY"].SummaryItem.DisplayFormat = "{0}";
            grdInspectionItem.View.Columns["INSPECTIONQTYPNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average;
            grdInspectionItem.View.Columns["INSPECTIONQTYPNL"].SummaryItem.DisplayFormat = "{0}";
            grdInspectionItem.View.Columns["DEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInspectionItem.View.Columns["DEFECTQTY"].SummaryItem.DisplayFormat = "{0}";
            grdInspectionItem.View.Columns["DEFECTQTYPNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInspectionItem.View.Columns["DEFECTQTYPNL"].SummaryItem.DisplayFormat = "{0}";
            grdInspectionItem.View.Columns["DEFECTRATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;//***불량율 합계는 100넘을 수있음 다시계산?***
            grdInspectionItem.View.Columns["DEFECTRATE"].SummaryItem.DisplayFormat = "{0:f2} %";

            grdInspectionItem.View.OptionsView.ShowFooter = true;
            grdInspectionItem.ShowStatusBar = false;
        }
        #endregion

        #region 컨트롤 초기화
        /// <summary>
        /// 콤보박스에 데이터를 바인딩 하는 함수
        /// </summary>
        private void InitializeCombo()
        {
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                {"LANGUAGETYPE", UserInfo.Current.LanguageType},
                { "CODECLASSID","ProcessingStatus"}
            };

            DataTable dt = SqlExecuter.Query("GetTypeList", "10001", param);

            cboProcessingStatus.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboProcessingStatus.Editor.ShowHeader = false;

            cboProcessingStatus.Editor.DisplayMember = "CODENAME";
            cboProcessingStatus.Editor.ValueMember = "CODEID";
            cboProcessingStatus.Editor.DataSource = dt;

            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"LANGUAGETYPE", UserInfo.Current.LanguageType},
                { "CODECLASSID","InspectionDecisionClass"}
            };

            DataTable dt2 = SqlExecuter.Query("GetTypeList", "10001", values);

            cboStandardType.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboStandardType.Editor.ShowHeader = false;
            cboStandardType.Editor.DisplayMember = "CODENAME";
            cboStandardType.Editor.ValueMember = "CODEID";
            cboStandardType.Editor.UseEmptyItem = false;
            cboStandardType.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboStandardType.Editor.DataSource = dt2;
            cboStandardType.Editor.EditValue = "AQL";


        }
        #endregion

        #region 컨트롤 초기화
        private void SettingLotgrd()
        {
            // LOT 정보 GRID
            ucLotInfo.ColumnCount = 5;
            ucLotInfo.LabelWidthWeight = "40%";
            ucLotInfo.SetInvisibleFields("PROCESSPATHID", "PROCESSDEFID", "PROCESSDEFVERSION", "PROCESSSEGMENTVERSION", "NEXTPROCESSSEGMENTVERSION",
                "ISLOCKING", "PANELPERQTY", "PROCESSSEGMENTTYPE", "STEPTYPE");
        }
        #endregion

        #region 팝업 초기화

        /// <summary>
        /// userPopup
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup CreateUserPopup()
        {
            ConditionItemSelectPopup conditionItem = new ConditionItemSelectPopup();
            conditionItem.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            conditionItem.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, true);
            conditionItem.Id = "INSPECTIONUSER";
            conditionItem.LabelText = "INSPECTIONUSER";
            conditionItem.SearchQuery = new SqlQuery("GetUserList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            conditionItem.IsMultiGrid = false;
            conditionItem.DisplayFieldName = "USERNAME";
            conditionItem.ValueFieldName = "USERID";
            conditionItem.LanguageKey = "INSPECTIONUSER";

            conditionItem.Conditions.AddTextBox("USERIDNAME");

            conditionItem.GridColumns.AddTextBoxColumn("USERID", 150);
            conditionItem.GridColumns.AddTextBoxColumn("USERNAME", 200);

            return conditionItem;
        }

        /// <summary>
        /// AreaPopup
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup CreateAreaPopup(DataRow row)
        {
            //areaList 확인
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LOTID", row["LOTID"]);
            param.Add("PROCESSSEGMENTID", row["NEXTPROCESSSEGMENTID"]);
            param.Add("PROCESSSEGMENTVERSION", row["NEXTPROCESSSEGMENTVERSION"]);
            param.Add("RESOURCETYPE", "Resource");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", CurrentDataRow["PLANTID"]);
            //2020-03-01 추가
            param.Add("PRODUCTDEFID", row["PRODUCTDEFID"]);
            param.Add("PRODUCTDEFVERSION", row["PRODUCTDEFVERSION"]);
            //2020-03-24 추가
            param.Add("LASTREWORK", lastRework);

            ConditionItemSelectPopup conditionItem = new ConditionItemSelectPopup();
            conditionItem.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            conditionItem.SetPopupLayout("RESOURCELIST", PopupButtonStyles.Ok_Cancel, true, true);
            conditionItem.Id = "RESOURCEID";
            conditionItem.LabelText = "RESOURCE";
            //conditionItem.SearchQuery = new SqlQuery("GetTransitAreaList", "10031", param);
            //2020-03-01 변경
            //conditionItem.SearchQuery = new SqlQuery("GetOSPInspectionNextAreaList", "10001", param);
            conditionItem.SearchQuery = new SqlQuery("GetTransitAreaList", lastRework == "Y" ? "10032" : "10031", param);
            conditionItem.IsMultiGrid = false;
            conditionItem.DisplayFieldName = "RESOURCENAME";
            conditionItem.ValueFieldName = "RESOURCEID";
            conditionItem.LanguageKey = "RESOURCE";
            conditionItem.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                foreach (DataRow resourceRow in selectedRow)
                {
                    _toResourceId = resourceRow["RESOURCEID"].ToString();
                    _toAreaId = resourceRow["AREAID"].ToString();
                }
            });

            conditionItem.Conditions.AddTextBox("TXTAREA");

            conditionItem.GridColumns.AddTextBoxColumn("RESOURCEID", 150);
            conditionItem.GridColumns.AddTextBoxColumn("RESOURCENAME", 200);
            conditionItem.GridColumns.AddTextBoxColumn("AREAID", 150);
            conditionItem.GridColumns.AddTextBoxColumn("AREANAME", 200);

            return conditionItem;
        }
        #endregion

        #endregion

        #region Event
        private void InitializeEvent()
        {
            //팝업 로드 이벤트
            this.Load += ProcessInspectionResultPopup_Load;
            //탭체인지 이벤트
            tabInspection.SelectedPageChanged += TabInspection_SelectedPageChanged;

            //특정 컬럼만 Merge하는 이벤트 
            //grdInspectionItem.View.CellMerge += View_CellMerge;

            //검사수량 및 불량수량을 입력하면 불량율을계산 해주는 이벤트
            grdInspectionItem.View.CellValueChanged += GrdDefectQTY_CellValueChanged;
            //불량율이 바뀔 때  판정결과를 자동판정하는 이벤트
            grdInspectionItem.View.CellValueChanged += GrdDefectRate_CellValueChanged;
            //불량율 합계 이벤트
            grdInspectionItem.View.CustomSummaryCalculate += View_CustomSummaryCalculate;

            //측정값을 입력하면 스펙체크를 하여 판정결과를 자동판정하는 이벤트
            grdMeasuredValue.View.CellValueChanged += View_CellValueChangedSpecCheck;

            //측정검사 AddingNewRowEvent
            grdMeasuredValue.ToolbarAddingRow += GrdMeasuredValue_ToolbarAddingRow;
            grdMeasuredValue.View.AddingNewRow += View_AddingNewRow;

            //판정 결과에 따라 전체 검사결과 판정
            grdInspectionItem.View.CellValueChanged += AllResult_CellValueChanged;
            //grdInspectionItemSpec.View.CellValueChanged += AllResult_CellValueChanged;
            grdMeasuredValue.View.CellValueChanged += AllResult_CellValueChanged;
            //2020-01-16 측정검사 ROW삭제시 측정항목 값 변경 후 전체 결과 판정
            grdInspectionItemSpec.View.CellValueChanged += AllResult_CellValueChanged;

            //팝업저장버튼을 클릭시 이벤트
            btnSave.Click += BtnSave_Click;
            //팝업닫기버튼을 클릭시 이벤트
            btnClose.Click += BtnClose_Click;

            //grdInspectionItem에 입력된 값은 수정하지 못하게 하는 이벤트
            grdInspectionItem.View.ShowingEditor += View_CantChangeResult;
            grdInspectionItemSpec.View.ShowingEditor += View_CantChangeResult;


            //이미지 추가 버튼 클릭 이벤트
            btnAddImageExterior.Click += BtnAddImage_Click;
            btnAddImageMeasure.Click += BtnAddImage_Click;
            

            //이미지 삭제 이벤트
            picExterior.KeyDown += PicDefect_KeyDown;
            picExterior2.KeyDown += PicDefect_KeyDown;

            picMeasure.KeyDown += PicDefect_KeyDown;
            picMeasure.KeyDown += PicDefect_KeyDown;

            //외관검사 그리드 새로 row 추가 이벤트
            grdInspectionItem.View.AddingNewRow += View_AddingNewRowDefect;
            grdInspectionItem.ToolbarAddingRow += (s, e) =>
              {
                  if (rdoTakeOver.SelectedIndex == 1)
                  {//2020-03-23 강유라 무검사 선택시 입력 불가
                    ShowMessage("CantInputDefectQtyNoInspection"); //인계처리(무검사) 선택시 검사결과를 입력 할 수 없습니다.
                    e.Cancel = true;
                  }
              };

            //외관검사 그리드 row Delete 이벤트
            grdInspectionItem.ToolbarDeletingRow += GrdInspectionItem_ToolbarDeletingRow;

            //처리여부 선택시 이벤트
            rdoTakeOver.SelectedIndexChanged += RdoTakeOver_SelectedIndexChanged;

            //DELETE하는 경우
            grdMeasuredValue.ToolbarDeletingRow += (s, e) =>
            {
                DataRow deleteRow = grdMeasuredValue.View.GetFocusedDataRow();
                if (deleteRow == null) return;
                 deleteRow["ISDELETE"] = "Y";
                //e.Cancel = true;

                //int NGCount = GetNGCount(grdMeasuredValue.DataSource as DataTable);
            };

            //2020-01-16 측정검사 ROW삭제시 측정항목 결과 판정
            grdMeasuredValue.ToolbarDeleteRow += (s, e) =>
             {
                 DataRow specItemRow = grdInspectionItemSpec.View.GetFocusedDataRow();

                 if (specItemRow == null) return;

                 int NGCount = GetNGCount(grdMeasuredValue.DataSource as DataTable);

                 if (NGCount > 0)
                 {//NG 한건이라도 있을 때
                     grdInspectionItemSpec.View.SetFocusedRowCellValue("INSPECTIONRESULT", "NG");
                 }
                 else
                 {
                     grdInspectionItemSpec.View.SetFocusedRowCellValue("INSPECTIONRESULT", "OK");
                 }
             };

            btnSelfInspLink.Click += BtnSelfInspLink_Click;

        }

        /// <summary>
        /// 자주검사 화면 연계 버튼 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void BtnSelfInspLink_Click(object sender, EventArgs e)
        {
            // 2020.02.24-유석진-자주검사(입고,출하)결과 조회 화면 호출
            _parent.setOpenMenu();
        }

        /// <summary>
        /// 외관검사 그리드 삭제 버튼 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GrdInspectionItem_ToolbarDeletingRow(object sender, CancelEventArgs e)
        {
            DataRow row = grdInspectionItem.View.GetFocusedDataRow();

            if (row == null) return;

            if (row["CANDELETE"].ToString().Equals("N"))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 외관검사 그리드 추가 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRowDefect(SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataRow row = (grdInspectionItem.DataSource as DataTable).Rows[0];
            args.NewRow["ISAQL"] = row["ISAQL"];
            args.NewRow["AQLINSPECTIONLEVEL"] = row["AQLINSPECTIONLEVEL"];
            args.NewRow["AQLDEFECTLEVEL"] = row["AQLDEFECTLEVEL"];
            args.NewRow["AQLDECISIONDEGREE"] = row["AQLDECISIONDEGREE"];
            args.NewRow["AQLCYCLE"] = row["AQLCYCLE"];
            args.NewRow["ISNCR"] = row["ISNCR"];
            args.NewRow["NCRINSPECTIONQTY"] = row["NCRINSPECTIONQTY"];
            args.NewRow["NCRCYCLE"] = row["NCRCYCLE"];
            args.NewRow["NCRDECISIONDEGREE"] = row["NCRDECISIONDEGREE"];
            args.NewRow["NCRDEFECTRATE"] = row["NCRDEFECTRATE"];
            args.NewRow["NCRLOTSIZE"] = row["NCRLOTSIZE"];
            args.NewRow["INSPECTIONQTY"] = row["INSPECTIONQTY"];
            args.NewRow["DEFECTQTY"] = 0;
            args.NewRow["DEFECTRATE"] = "0%";
            args.NewRow["INSPECTIONQTYPNL"] = row["INSPECTIONQTYPNL"];
            args.NewRow["DEFECTQTYPNL"] = 0;
            args.NewRow["INSPECTIONRESULT"] = "OK";
            args.NewRow["CANDELETE"] = "Y";
        }

        private void GrdMeasuredValue_ToolbarAddingRow(object sender, CancelEventArgs e)
        {
            DataRow row = grdInspectionItemSpec.View.GetFocusedDataRow();
            if (row == null)
            {
                e.Cancel = true;
            }
            else if (rdoTakeOver.SelectedIndex == 1)
            {//2020-03-23 강유라 무검사 선택시 입력 불가
                ShowMessage("CantInputDefectQtyNoInspection"); //인계처리(무검사) 선택시 검사결과를 입력 할 수 없습니다.
                e.Cancel = true;
            }
        }

        /// <summary>
        /// grdMeasuredValue 검사 결과에 따라 전체 결과 자동 판정하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllResult_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if(e.Column.FieldName.Equals("INSPECTIONRESULT"))
            {              
                if (e.Value.Equals("NG") || txtInspectionResult.EditValue == null)
                {//판정결과 NG일때 -> NG
                    txtInspectionResult.EditValue = e.Value;
                }
                else
                {//판정결과 OK일때

                 //이전 검사 들로 인한 전체 판정 결과
                    string result = txtInspectionResult.EditValue.ToString();

                    if (result.Equals("NG"))
                    {//이전 검사 결과 NG일경우

                        DataTable grdEx = grdInspectionItem.DataSource as DataTable;

                        int NGCount = 0;
                        NGCount = GetNGCount(grdEx);

                        if (NGCount == 0)
                        {//외관검사 결과 NGCount 일때 -> 측정검사 확인
                            DataTable itemDt = grdInspectionItemSpec.DataSource as DataTable;
                            NGCount = GetNGCount(itemDt);

                            if (NGCount == 0)
                            {
                                txtInspectionResult.EditValue = e.Value;
                            }
                            else
                            {
                                txtInspectionResult.EditValue = "NG";
                            }
                        }
                        else
                        {
                            txtInspectionResult.EditValue = "NG";
                        }

                    }
                    else
                    {//이전 검사 결과 없거나 OK 일경우 -> OK
                        txtInspectionResult.EditValue = e.Value;
                    }
                }
            }
        }

        /// <summary>
        /// grdMeasuredValue 새 row 추가 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataRow row = grdInspectionItemSpec.View.GetFocusedDataRow();
            if (row == null) return;

            args.NewRow["INSPITEMID"] = row["INSPITEMID"];
            args.NewRow["INSPITEMNAME"] = row["INSPITEMNAME"];
            args.NewRow["INSPITEMVERSION"] = row["INSPITEMVERSION"];
            args.NewRow["DEGREE"] = CurrentDataRow["DEGREE"];

            args.NewRow["INSPECTIONSTANDARD"] = row["INSPECTIONSTANDARD"];
            args.NewRow["LOWERSPECLIMIT"] = row["LOWERSPECLIMIT"];
            args.NewRow["UPPERSPECLIMIT"] = row["UPPERSPECLIMIT"];
            args.NewRow["LOWERCONTROLLIMIT"] = row["LOWERCONTROLLIMIT"];
            args.NewRow["UPPERCONTROLLIMIT"] = row["UPPERCONTROLLIMIT"];

            args.NewRow["TARGETVALUE"] = row["TARGETVALUE"];
            args.NewRow["UPPERSCREENLIMIT"] = row["UPPERSCREENLIMIT"];
            args.NewRow["NCRDECISIONDEGREE"] = row["NCRDECISIONDEGREE"];

            args.NewRow["LOWERSCREENLIMIT"] = row["LOWERSCREENLIMIT"];

            args.NewRow["RESOURCEID"] = CurrentDataRow["LOTID"];
            args.NewRow["RESOURCETYPE"] = "ProcessInspection";
            args.NewRow["PROCESSRELNO"] = CurrentDataRow["LOTHISTKEY"] + CurrentDataRow["DEGREE"].ToString();

            args.NewRow["ENTERPRISEID"] = row["ENTERPRISEID"];
            args.NewRow["PLANTID"] = row["PLANTID"];
        }

        /// <summary>
        /// 무검사(인계처리) 시 메세지박스 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RdoTakeOver_SelectedIndexChanged(object sender, EventArgs e)
        {/*
            rdoTakeOver.SelectedIndex == 0 -> 인계처리
            rdoTakeOver.SelectedIndex == 1 -> 인계처리(무검사)
            rdoTakeOver.SelectedIndex == 2 -> 의뢰취소
             */

            string finalResult = Format.GetString(txtInspectionResult.EditValue);

            if (rdoTakeOver.SelectedIndex == 1)
            {//인계처리(무검사)
                CheckFinalInspResult(finalResult);

                DialogResult result = System.Windows.Forms.DialogResult.No;

                result = this.ShowMessage(MessageBoxButtons.YesNo, "DeleteAllDataConfirm");//인계처리(무검사) 선택시 입력한 검사결과가 지워집니다. 선택하시겠습니까?

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {/*
                        DataTable defectCodeDt = grdInspectionItem.DataSource as DataTable;
                        foreach (DataRow defectRow in defectCodeDt.Rows)
                        {
                            defectRow["INSPECTIONQTY"] = DBNull.Value;
                            defectRow["INSPECTIONQTYPNL"] = DBNull.Value;
                            defectRow["DEFECTQTY"] = DBNull.Value;
                            defectRow["DEFECTQTYPNL"] = DBNull.Value;
                            defectRow["DEFECTRATE"] = string.Empty;
                            defectRow["INSPECTIONRESULT"] = string.Empty;            
                        }

                        defectCodeDt.AcceptChanges();

                        DataTable measureDt = grdInspectionItemSpec.DataSource as DataTable;
                        foreach (DataRow measureRow in measureDt.Rows)
                        {
                            measureRow["MEASUREVALUE"] = string.Empty;
                            measureRow["INSPECTIONRESULT"] = string.Empty;              
                        }

                        measureDt.AcceptChanges();

                        ucProcessInspDefect.ClearUserControlGrd();

                        txtInspectionResult.EditValue = null;
                        */

                        //검사항목 조회
                        OnSearch();
                        picExterior.Image = null;
                        picExterior2.Image = null;
                        picMeasure.Image = null;
                        picMeasure2.Image = null;

                        ucProcessInspDefect.ClearUserControlGrd();
                        popArea.Enabled = true;
                        txtInspectionResult.Text = "OK";
                        measureDs = new DataSet();
                    }
                    catch (Exception ex)
                    {
                        this.ShowError(ex);
                    }

                }
                else
                {
                    rdoTakeOver.SelectedIndex = _raidOldSelected;
                }
            }
            else if (rdoTakeOver.SelectedIndex == 2)
            {//의뢰취소
                popArea.EditValue = string.Empty;
                popArea.Enabled = false;
                _toAreaId = "";
                _toResourceId = "";
            }
            else if (rdoTakeOver.SelectedIndex == 0)
            {//인계처리
                CheckFinalInspResult(finalResult);
                popArea.Enabled = true;
            }

            _raidOldSelected = rdoTakeOver.SelectedIndex;
            ucProcessInspDefect.SetSelectedIndex(_raidOldSelected);
        }

        private void TabInspection_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (tabInspection.SelectedTabPageIndex)
            {
                case 0:
                    _grid = grdInspectionItem;
                    _commonPic = picExterior;
                    _commonPic2 = picExterior2;
                    break;

                case 1:
                    _grid = grdInspectionItemSpec;
                    _commonPic = picMeasure;
                    _commonPic2 = picMeasure2;
                    break;

            }
        }

        /// <summary>
        /// 로드 이벤트
        /// 팝업내의 팝업초기화 및 데이터로드
        /// 1.컨트롤 설정(팝업타입 지정/그리드 확대 / 축소 버튼 안보이게 등)
        /// 2.팝업타입에 따라 다른 이벤트 연결
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessInspectionResultPopup_Load(object sender, EventArgs e)
        {
            SettingControl();

            btnSave.Enabled = isEnable;

            if (_type.Equals("insertData"))
            {
                _lotQty = _lotRow["PCSQTY"].ToSafeDoubleZero();
                grdInspectionItem.View.FocusedRowChanged += FocusedRowChangedBeforeSave;
                grdInspectionItemSpec.View.FocusedRowChanged += FocusedRowChangedBeforeSave;
                cboStandardType.Control.EditValueChanged += Control_EditValueChanged;
                txtInspectionResult.Editor.EditValue = "OK";

            }
            else if(_type.Equals("updateData"))
            {
                grdInspectionItem.View.FocusedRowChanged += View_FocusedRowChangedExterior;
                grdInspectionItemSpec.View.FocusedRowChanged += View_FocusedRowChangedMeasure;
   
            }

            InitializePopup();

            ucProcessInspDefect userControl = tpgDefect.Controls[0] as ucProcessInspDefect;
            userControl.SetType(_type);
            userControl.InitializeGrid();
            userControl.InitializationSummaryRow();
            userControl.InitializeEvent();
            userControl.SetConsumableDefComboBox(CurrentDataRow["LOTID"].ToString());

            //검사항목 조회
            OnSearch();

            this.MinimizeBox = true;

        }

        /// <summary>
        /// AQL, NCR 기준 변경시 초기화
        /// </summary>
        private void Control_EditValueChanged(object sender, EventArgs e)
        {
            InitializeExteriorGrid(_type);

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);
            values.Add("PLANTID", CurrentDataRow["PLANTID"]);
            values.Add("P_RESOURCEID", CurrentDataRow["LOTID"]);
            values.Add("RESOURCETYPE", "ProcessInspection");
            values.Add("P_PROCESSRELNO", CurrentDataRow["LOTHISTKEY"] + CurrentDataRow["DEGREE"].ToString());
            values.Add("P_RELRESOURCEID", CurrentDataRow["PRODUCTDEFID"]);
            values.Add("P_RELRESOURCEVERSION", CurrentDataRow["PRODUCTDEFVERSION"]);
            values.Add("P_RELRESOURCETYPE", "Product");
            //values.Add("P_INSPITEMTYPE", "OK_NG");
            values.Add("P_INSPECTIONDEFID", "OSPInspection");
            values.Add("P_RESULTTXNHISTKEY", CurrentDataRow["TXNHISTKEY"].ToString());
            values.Add("P_RESULTTXNGROUPHISTKEY", CurrentDataRow["TXNGROUPHISTKEY"].ToString());
            //측정검사 검사항목 가져오기위한 파라미터
            values.Add("P_PROCESSSEGMENTID", CurrentDataRow["PROCESSSEGMENTID"]);
            values.Add("P_PROCESSSEGMENTVERSION", CurrentDataRow["PROCESSSEGMENTVERSION"]);

            //외관검사 - 불량코드
            DataTable exteriorDefectCode = SqlExecuter.Query("SelectOSPInspectionExterior", "10001", values);
        
            if (cboStandardType.Editor.EditValue.ToString().Equals("AQL"))
            {
                grdInspectionItem.DataSource = SetAQLQTY(exteriorDefectCode);
            }
            else
            {
                grdInspectionItem.DataSource = InspectionHelper.SetProcessRelNo(CurrentDataRow["LOTHISTKEY"] + CurrentDataRow["DEGREE"].ToString(), exteriorDefectCode);
                txtAQLInspectionQty.Editor.EditValue = "";
                txtAQLInspectionLevel.Editor.EditValue = "";

            }

            picExterior.Image = null;
            picExterior2.Image = null;
        }
        /// <summary>
        /// 등록된 검사 결과가 있을 때 결과 수정 못함 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CantChangeResult(object sender, CancelEventArgs e)
        {
            if (_type.Equals("updateData"))
            {//이미등록된 결과가 있을 때
                e.Cancel = true;
            }
            else
            {

                if (rdoTakeOver.SelectedIndex == 1)
                {//2020-03-23 강유라 무검사 선택시 입력 불가
                    ShowMessage("CantInputDefectQtyNoInspection"); //인계처리(무검사) 선택시 검사결과를 입력 할 수 없습니다.
                    e.Cancel = true;
                }

                if (grdInspectionItem.View.FocusedColumn.FieldName == "DEFECTQTY" || grdInspectionItem.View.FocusedColumn.FieldName == "DEFECTQTYPNL")
                {
                    var inspectionQty = grdInspectionItem.View.GetRowCellValue(grdInspectionItem.View.FocusedRowHandle, "INSPECTIONQTY");
                    if (inspectionQty == null || string.IsNullOrWhiteSpace(inspectionQty.ToString()) || inspectionQty.ToSafeInt32() == 0)
                    {
                        e.Cancel = true;
                    }                               
                }       
            }
        }

        /// <summary>
        /// 포커스된 row에 등록된 이미지를 보여주는 이벤트(저장전)
        /// inspItem dt에 파일정보 저장하여 이미지 보여주기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FocusedRowChangedBeforeSave(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SmartBandedGridView view = sender as SmartBandedGridView;
            DataRow beforeRow = view.GetDataRow(e.PrevFocusedRowHandle);
            DataRow row = view.GetDataRow(e.FocusedRowHandle);
            if (row == null) return;

            if (beforeRow != null && tabInspection.SelectedTabPageIndex == 1)
            {
                TempSaveMeasureValueBeforeSave(beforeRow);
                BindingMeasureValueBeforeSave(row);
            }

            //검사결과가 N이고, 대분류가 아닐 때
            if (row["INSPECTIONRESULT"].ToString().Equals("NG"))
            {
                _commonPic.Image = null;
                _commonPic2.Image = null;

                if (row["FILEFULLPATH1"].ToString() == string.Empty && row["FILENAME1"].ToString() == string.Empty
                        && row["FILEFULLPATH2"].ToString() == string.Empty && row["FILENAME2"].ToString() == string.Empty)
                {
                    return;
                }

                ImageConverter converter = new ImageConverter();

                if (!string.IsNullOrWhiteSpace(row["FILEFULLPATH1"].ToString()))
                {
                    _commonPic.Image = QcmImageHelper.GetImageFromFile(row["FILEFULLPATH1"].ToString());//(Image)converter.ConvertFrom(row["FILEFULLPATH1"]);
                }

                if (!string.IsNullOrWhiteSpace(row["FILEFULLPATH2"].ToString()))
                {
                    _commonPic2.Image = QcmImageHelper.GetImageFromFile(row["FILEFULLPATH2"].ToString());//(Image)converter.ConvertFrom(row["FILEFULLPATH2"]);
                }
            }
            else
            {
                _commonPic.Image = null;
                _commonPic2.Image = null;
            }

        }

        /// <summary>
        /// 포커스된 row에 등록된 이미지를 보여주는 이벤트(저장후)
        /// lotid+inspItem 으로 파일정보 셀렉트하여 이미지 보여주기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChangedExterior(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SmartBandedGridView view = sender as SmartBandedGridView;
            DataRow row = view.GetDataRow(e.FocusedRowHandle);

            string resourceType = "DefectCode";
            _commonPic = picExterior;
            _commonPic2 = picExterior2;

            picExterior.Image = null;
            picExterior2.Image = null;
            
            //검사결과가 NG이고, 대분류가 아닐 때
            if (row["INSPECTIONRESULT"].ToString().Equals("NG"))
            {
                SearchImageByRow(row, resourceType, picExterior, picExterior2);
            }
        }

        /// <summary>
        /// 포커스된 row에 등록된 이미지를 보여주는 이벤트(저장후)
        /// lotid+inspItem 으로 파일정보 셀렉트하여 이미지 보여주기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChangedMeasure(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SmartBandedGridView view = sender as SmartBandedGridView;
            DataRow row = view.GetDataRow(e.FocusedRowHandle);

            string resourceType = "InspItemId";
      
            BindingMeasureValueAfterSave(row);
            resourceType = "InspItemId";

            picMeasure.Image = null;
            picMeasure2.Image = null;

            SearchImageByRow(row, resourceType, picMeasure, picMeasure2);
           
        }
        /// <summary>
        /// PictureBox에 delete버튼 클릭시 사진지우는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicDefect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && _type.Equals("insertData"))
            {
                DataRow row = _grid.View.GetFocusedDataRow();
                if (row == null) return;
                SmartPictureEdit picBox = sender as SmartPictureEdit;
                picBox.Image = null;

                if (picBox.Name.Equals(_commonPic.Name))
                {
                    row["FILEDATA1"] = null;
                    row["FILECOMMENTS1"] = null;
                    row["FILESIZE1"] = DBNull.Value;
                    row["FILEEXT1"] = null;
                    row["FILENAME1"] = null;

                    //===========================================================================
                    //YJKIM : File의 FullPath를 저장할 필드를 설정
                    row["FILEFULLPATH1"] = null;
                    row["FILEID1"] = null;
                    //---------------------------------------------------------------------------

                }
                else if (picBox.Name.Equals(_commonPic2.Name))
                {
                    row["FILEDATA2"] = null;
                    row["FILECOMMENTS2"] = null;
                    row["FILESIZE2"] = DBNull.Value;
                    row["FILEEXT2"] = null;
                    row["FILENAME2"] = null;

                    //============================================================================
                    //YJKIM : File의 FullPath를 저장할 필드를 설정
                    row["FILEFULLPATH2"] = null;
                    row["FILEID2"] = null;
                    //----------------------------------------------------------------------------
                }
            }
        }

        /// <summary>
        /// 이미지 추가버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddImage_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row = _grid.View.GetFocusedDataRow();
                if (row == null) return;

                if (_type.Equals("updateData"))
                {
                    ShowMessage("CanAddImageBeforeResultSave");//검사 결과가 저장된 후에는 이미지를 추가 할 수 없습니다.
                    return;
                }

                if (!row["INSPECTIONRESULT"].ToString().Equals("NG"))
                {
                    ShowMessage("CanAddImageOnlyNG");//판정결과가 NG인 경우만 사진을 등록할수 있습니다.
                    return;
                }

                DialogManager.ShowWaitArea(this);

                string imageFile = string.Empty;

                OpenFileDialog dialog = new OpenFileDialog
                {
                    Multiselect = false,
                    Filter = "Image Files (*.bmp, *.jpg, *.jpeg, *.png)|*.BMP;*.JPG;*.JPEG;*.PNG",
                    FilterIndex = 0
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string fileResoureceId = "";

                    if (tabInspection.SelectedTabPageIndex == 0)
                    {
                        fileResoureceId = CurrentDataRow["LOTID"] + row["DEFECTCODE"].ToString() + row["QCSEGMENTID"].ToString() + "O"+ CurrentDataRow["DEGREE"].ToString();
                    }
                    else if (tabInspection.SelectedTabPageIndex == 1)
                    {
                        fileResoureceId = CurrentDataRow["LOTID"] + row["INSPITEMID"].ToString() + CurrentDataRow["DEGREE"].ToString();
                    }

                    row["FILEINSPECTIONTYPE"] = "ProcessInspection";
                    row["FILERESOURCEID"] = fileResoureceId;
                    row["PROCESSRELNO"] = CurrentDataRow["LOTHISTKEY"] + CurrentDataRow["DEGREE"].ToString();

                    imageFile = dialog.FileName;
                    FileInfo fileInfo = new FileInfo(dialog.FileName);
                    using (FileStream fs = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        byte[] data = new byte[fileInfo.Length];
                        fs.Read(data, 0, (int)fileInfo.Length);

                        MemoryStream ms = new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(data).ToString()));
                        if (_commonPic.Image == null)
                        {
                            row["FILENAME1"] = dialog.SafeFileName;
                            row["FILEID1"] = "FILE-" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            //=========================================================================================================================
                            //YJKIM : binary파일을 저장하지 않고 File을 Upload하는 형태로 변경
                            ////2020-02-19 강유라 이메일 첨부용
                            row["FILEDATA1"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(dialog.FileName), typeof(byte[]));
                            row["FILEFULLPATH1"] = dialog.FileName; //파일의 전체경로 저장
                            //-------------------------------------------------------------------------------------------------------------------------

                            row["FILECOMMENTS1"] = "InspectionResult/ProcessInspection";
                            row["FILESIZE1"] = Math.Round(Convert.ToDouble(fileInfo.Length) / 1024);
                            row["FILEEXT1"] = fileInfo.Extension.Substring(1); ;

                            _commonPic.Image = Image.FromStream(ms);
                        }
                        else
                        {
                            row["FILENAME2"] = dialog.SafeFileName;
                            row["FILEID2"] = "FILE-" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            //=========================================================================================================================
                            //YJKIM : binary파일을 저장하지 않고 File을 Upload하는 형태로 변경
                            //2020-02-19 강유라 이메일 첨부용
                            row["FILEDATA2"] = (byte[])new ImageConverter().ConvertTo(new Bitmap(dialog.FileName), typeof(byte[]));
                            row["FILEFULLPATH2"] = dialog.FileName; //파일의 전체경로 저장
                            //-------------------------------------------------------------------------------------------------------------------------

                            row["FILECOMMENTS2"] = "InspectionResult/ProcessInspection";
                            row["FILESIZE2"] = Math.Round(Convert.ToDouble(fileInfo.Length) / 1024);
                            row["FILEEXT2"] = fileInfo.Extension.Substring(1);
                            _commonPic2.Image = Image.FromStream(ms);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw Framework.MessageException.Create(ex.ToString());
            }
            finally
            {
                DialogManager.CloseWaitArea(this);
            }
        }


        /// <summary>
        /// 검사수량, 불량수량을 입력 했을때 불량율을 계산해준다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdDefectQTY_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            
            if (e.Column.FieldName == "INSPECTIONQTY" || e.Column.FieldName == "DEFECTQTY" 
                            || e.Column.FieldName == "DEFECTQTYPNL" 
                            || (e.Column.FieldName == "INSPECTIONQTYPNL" && cboStandardType.Editor.EditValue.ToString().Equals("NCR")))
            {
                grdInspectionItem.View.CellValueChanged -= GrdDefectQTY_CellValueChanged;

                DataRow row = grdInspectionItem.View.GetDataRow(e.RowHandle);
                DataTable dt = grdInspectionItem.DataSource as DataTable;

                if (e.Column.FieldName == "INSPECTIONQTY" || (e.Column.FieldName == "INSPECTIONQTYPNL" && cboStandardType.Editor.EditValue.ToString().Equals("NCR")))
                {
                    //음수를 입력했을 때 0으로 바꿔줌
                    if (e.Value.ToSafeInt32() <= 0)
                    {
                        ShowMessage("InspectionQtyCount");//검사수량은 0 또는 음수가 될 수 없습니다.                  
                        grdInspectionItem.View.SetFocusedRowCellValue(e.Column.FieldName, 0);
                        grdInspectionItem.View.CellValueChanged += GrdDefectQTY_CellValueChanged;
                        return;
                    }

                    if (e.Column.FieldName == "INSPECTIONQTY")
                    {
                        if (_lotQty < e.Value.ToSafeDoubleZero() || e.Value.ToSafeDoubleZero() == 0)
                        {

                            if (_lotQty < e.Value.ToSafeDoubleZero())
                            {
                                ShowMessage("InvalidSampleQtyOverQty");//샘플 수량은 전체 수량보다 클 수 없습니다
                            }
                            else if (e.Value.ToSafeDoubleZero() == 0)
                            {
                                ShowMessage("InspectionQtyCount");//검사수량은 0이 될 수 없습니다.
                            }
                            /*
                            foreach (DataRow dtRow in dt.Rows)
                            {
                                dtRow["INSPECTIONQTY"] = DBNull.Value;
                                dtRow["INSPECTIONQTYPNL"] = DBNull.Value;
                                //grdInspectionItem.View.SetFocusedRowCellValue("INSPECTIONQTYPNL", inspectionQtyPNL);
                            }*/

                            grdInspectionItem.View.SetFocusedRowCellValue(e.Column.FieldName, DBNull.Value);
                            grdInspectionItem.View.SetFocusedRowCellValue("INSPECTIONQTYPNL", DBNull.Value);
                            grdInspectionItem.View.CellValueChanged += GrdDefectQTY_CellValueChanged;
                            return;
                        }
                        else
                        {
                            foreach (DataRow dtRow in dt.Rows)
                            {
                                dtRow["INSPECTIONQTY"] = e.Value;
                                //NSPECTIONQTYPNL = INSPECTIONQTY/PANELPERQTY
                 
                                double inspectionQtyPNL = CurrentDataRow["PANELPERQTY"].ToSafeDoubleZero() == 0 ? 0: Math.Ceiling(e.Value.ToSafeDoubleZero() / CurrentDataRow["PANELPERQTY"].ToSafeDoubleZero());
                                dtRow["INSPECTIONQTYPNL"] = inspectionQtyPNL;
                                //grdInspectionItem.View.SetFocusedRowCellValue("INSPECTIONQTYPNL", inspectionQtyPNL);
                            }

                            _inspectionQty = Format.GetDouble(e.Value,0);
                        }

                    }
                    else if (e.Column.FieldName == "INSPECTIONQTYPNL" && cboStandardType.Editor.EditValue.ToString().Equals("NCR"))
                    {
                        //입력된 INSPECTIONQTYPNL로 계산된 INSPECTIONQTY
                        double inspectionQtyPCS = CurrentDataRow["PANELPERQTY"].ToSafeDoubleZero() == 0? e.Value.ToSafeDoubleZero() : e.Value.ToSafeDoubleZero() * CurrentDataRow["PANELPERQTY"].ToSafeDoubleZero();

                        if (_lotQty < inspectionQtyPCS)
                        {
                            ShowMessage("InvalidSampleQtyOverQty");//샘플 수량은 전체 수량보다 클 수 없습니다
                            grdInspectionItem.View.SetFocusedRowCellValue(e.Column.FieldName, DBNull.Value);
                            grdInspectionItem.View.SetFocusedRowCellValue("INSPECTIONQTY", DBNull.Value);
                            grdInspectionItem.View.CellValueChanged += GrdDefectQTY_CellValueChanged;
                            return;
                        }

                        foreach (DataRow dtRow in dt.Rows)
                        {
                            dtRow["INSPECTIONQTYPNL"] = e.Value;
                            //INSPECTIONQTY = INSPECTIONQTYPNL * PANELPERQTY
                            dtRow["INSPECTIONQTY"] = inspectionQtyPCS;
 
                        }

                    }
                }


                if (e.Column.FieldName == "DEFECTQTY" || e.Column.FieldName == "DEFECTQTYPNL")
                {
                    //검사수량을 입력하지 않고 불량수량입력 할 때
                    if (string.IsNullOrWhiteSpace(row["INSPECTIONQTY"].ToString()) || string.IsNullOrWhiteSpace(row["INSPECTIONQTYPNL"].ToString()))
                    {
                        ShowMessage("NoInspectionQtyAndPnl");//검사 수량을 먼저 입력하세요
                        grdInspectionItem.View.SetFocusedRowCellValue(e.Column.FieldName, 0);
                        grdInspectionItem.View.CellValueChanged += GrdDefectQTY_CellValueChanged;
                        return;
                    }

                    if (e.Column.FieldName == "DEFECTQTY")
                    {
                        if (row["INSPECTIONQTY"].ToSafeInt32() < e.Value.ToSafeInt32())
                        {//검사 수량보다 불량수가 많을 때
                            ShowMessage("PcsQtyRangeOver");//검사수량보다 불량수량이 초과하였습니다.
                            grdInspectionItem.View.SetFocusedRowCellValue(e.Column.FieldName, 0);
                            grdInspectionItem.View.SetFocusedRowCellValue("DEFECTQTYPNL", 0);
                            grdInspectionItem.View.CellValueChanged += GrdDefectQTY_CellValueChanged;
                            return;
                        }

                        var specOutQtySum = dt.AsEnumerable()
                        .Select(r => r["DEFECTQTY"].ToSafeInt32())
                        .Sum().ToSafeInt32();

                        if (row["INSPECTIONQTY"].ToSafeInt32() < specOutQtySum)
                        {
                            ShowMessage("PcsQtyRangeOver");//검사수량보다 불량수량이 초과하였습니다.
                            grdInspectionItem.View.SetFocusedRowCellValue("DEFECTQTY", 0);
                            grdInspectionItem.View.CellValueChanged += GrdDefectQTY_CellValueChanged;
                            return;
                        }

                        row["DEFECTQTY"] = e.Value;
                        //DEFECTQTYPNL = DEFECTQTY/PANELPERQTY
                        double defectQtyPnl = CurrentDataRow["PANELPERQTY"].ToSafeDoubleZero() == 0? 0 :Math.Ceiling(e.Value.ToSafeDoubleZero() / CurrentDataRow["PANELPERQTY"].ToSafeDoubleZero());
                        row["DEFECTQTYPNL"] = defectQtyPnl;
                        
                    }
                    else if (e.Column.FieldName == "DEFECTQTYPNL")
                    {
                      
                        if (row["INSPECTIONQTYPNL"].ToSafeInt32() < e.Value.ToSafeInt32())
                        {//검사 수량보다 불량수가 많을 때
                            ShowMessage("PcsQtyRangeOver");//검사수량보다 불량수량이 초과하였습니다.
                            grdInspectionItem.View.SetFocusedRowCellValue(e.Column.FieldName, 0);
                            grdInspectionItem.View.SetFocusedRowCellValue("DEFECTQTY", 0);
                            grdInspectionItem.View.CellValueChanged += GrdDefectQTY_CellValueChanged;
                            return;
                        }

                        var specOutQtySum = dt.AsEnumerable()
                        .Select(r => r["DEFECTQTY"].ToSafeInt32())
                        .Sum().ToSafeInt32();

                        //입력한 DEFECTQTYPNL로 계산된 DEFECTQTY
                        double defectQtyPCS = CurrentDataRow["PANELPERQTY"].ToSafeDoubleZero() == 0 ? e.Value.ToSafeDoubleZero() :e.Value.ToSafeDoubleZero() * CurrentDataRow["PANELPERQTY"].ToSafeDoubleZero();

                        //이미 입력되어있는 DEFECTQTY + 지금 계산된 DEFECTQTY 의 합이 INSPECTIONQTY 초과하면안됨
                        if (row["INSPECTIONQTY"].ToSafeInt32() < specOutQtySum + defectQtyPCS)
                        {
                            ShowMessage("PcsQtyRangeOver");//검사수량보다 불량수량이 초과하였습니다.
                            grdInspectionItem.View.SetFocusedRowCellValue(e.Column.FieldName, 0);
                            grdInspectionItem.View.SetFocusedRowCellValue("DEFECTQTY", 0);
                            grdInspectionItem.View.CellValueChanged += GrdDefectQTY_CellValueChanged;
                            return;
                        }

                        row["DEFECTQTYPNL"] = e.Value;
                        //DEFECTQTY = DEFECTQTYPNL * PANELPERQTY
                        row["DEFECTQTY"] = defectQtyPCS;                        
                    }

                }

                if (string.IsNullOrWhiteSpace(row["DEFECTQTY"].ToString()))
                {
                    grdInspectionItem.View.SetFocusedRowCellValue("DEFECTRATE", null);
                }
                else
                {
                    //DEFECTQTYPNL = DEFECTQTY/PANELPERQTY
                    decimal defectRate = Math.Round((row["DEFECTQTY"].ToSafeDecimal() / row["INSPECTIONQTY"].ToSafeDecimal() * 100).ToSafeDecimal(), 1);
                    string per = defectRate.ToString() + "%";
                    grdInspectionItem.View.SetFocusedRowCellValue("DEFECTRATE", per);

                }

                grdInspectionItem.View.CellValueChanged += GrdDefectQTY_CellValueChanged;
            }
        }

        /// <summary>
        /// 불량율 값이 바뀔 경우 판정결과를 판정하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdDefectRate_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //불량율이 바뀌었을때
            if (e.Column.FieldName == "DEFECTRATE" && e.Value !=null)
            {
                DataRow row = grdInspectionItem.View.GetFocusedDataRow();
                //불량 기준을 찾기위한 파라미터가 없는경우 return
                /*if ((string.IsNullOrWhiteSpace(row["AQLINSPECTIONLEVEL"].ToString()) && string.IsNullOrWhiteSpace(row["AQLDEFECTLEVEL"].ToString()))
                   || string.IsNullOrWhiteSpace(row["NCRDECISIONDEGREE"].ToString()))
                    return;*/

                if (string.IsNullOrWhiteSpace(cboStandardType.Editor.EditValue.ToString()))
                {
                    throw MessageException.Create("SelectInspStandard");//판정기준(외관검사) 선택하세요.
                }

                string result = "";
                string messageId = null;

                if (cboStandardType.Editor.EditValue.ToString().Equals("AQL"))
                {//AQL기준
                    result = InspectionHelper.SetQcGradeAndResultAQLType(row, "OSPInspection", row["AQLDECISIONDEGREE"].ToString(), _lotRow["PCSQTY"].ToString(), true ,out messageId);
                }
                else if (cboStandardType.Editor.EditValue.ToString().Equals("NCR"))
                {//NCR기준
                    result = InspectionHelper.SetQcGradeAndResultNCRQtyRateType(row, "OSPInspection", row["DECISIONDEGREE"].ToString(), true, out messageId);
                }

                //불량 기준에 의해 판정결과 값 입력
                grdInspectionItem.View.SetFocusedRowCellValue("INSPECTIONRESULT", result);

                if (messageId != null)
                {
                    ShowMessage(messageId);
                }
            }
        }


        /// <summary>
        /// 불량률 계산
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                GridSummaryItem item = e.Item as GridSummaryItem;
                if (item.FieldName == "DEFECTRATE")
                {
                    double inspectionQty = 0;
                    double defectQty = 0;
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        inspectionQty = (sender as GridView).Columns["INSPECTIONQTY"].SummaryItem.SummaryValue.ToSafeDoubleZero();
                        _inspectionQty = inspectionQty;
                        defectQty = (sender as GridView).Columns["DEFECTQTY"].SummaryItem.SummaryValue.ToSafeDoubleNaN();
                        if (inspectionQty != 0 && defectQty != 0)
                        {
                            decimal defectRate = Math.Round((defectQty / inspectionQty * 100).ToSafeDecimal(), 1);
                            e.TotalValue = defectRate;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Spec 값에 따라 판정결과를 입력 해 주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChangedSpecCheck(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //측정값이 바뀌었을때
            if (e.Column.FieldName == "MEASUREVALUE")
            {
                DataRow row = grdMeasuredValue.View.GetDataRow(e.RowHandle);

                if (row == null)
                    return;
                //SmartBandedGridView view = sender as SmartBandedGridView;
                //스펙을 찾기위한 파라미터가 없는경우 return
                if (string.IsNullOrWhiteSpace(row["LOWERSPECLIMIT"].ToString()) && string.IsNullOrWhiteSpace(row["UPPERSPECLIMIT"].ToString()))
                {
                    this.ShowMessage("NoSpecDetail");//유효성 검사를 할 Spec정보가 등록되어있지 않습니다.
                }
                else
                {
                    //스펙체크 결과에 의해 판정결과 값 입력
                    grdMeasuredValue.View.SetFocusedRowCellValue("INSPECTIONRESULT", CheckValidationSpecOut(row));

                    if (CheckValidationSpecOut(row).Equals("NG"))
                    { 
                        if (row["NCRDECISIONDEGREE"] == null)
                        {
                            ShowMessage("NoActionStandardData");//판정등급이 없습니다.
                            return;
                        }

                        string sequence = "";
                        string qcgrade = InspectionHelper.GetQcGradeAndSequenceNCRAndSpecType("OperationInspection", row["NCRDECISIONDEGREE"].ToString(), out sequence);

                        row["QCGRADE"] = qcgrade;
                        row["PRIORITY"] = sequence;
                    }
                }
            }
        }
       
        /// <summary>
        /// 저장버튼을 클릭했을때 검사 결과를 저장하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.DialogResult.No;

            if (_type.Equals("insertData"))
            { //검사 결과 최초 등록일때 결과 저장여부 평가
                if (txtLotId.EditValue == null)
                {
                    return;
                }

                //2020-01-17 결과 NG일때 입고 의뢰 취소만 가능
                CheckFinalInspResult(Format.GetString(txtInspectionResult.EditValue));

                if (!rdoTakeOver.EditValue.ToString().Equals("HandOverProcessNon") && (string.IsNullOrWhiteSpace(cboProcessingStatus.Editor.EditValue.ToString()) || string.IsNullOrWhiteSpace(rdoTakeOver.EditValue.ToString())))
                {
                    this.ShowMessage("NeedToInputResultItemAndTakeOver");//처리사항과 인계처리여부를 입력해야합니다.
                    return;
                }

                if (rdoTakeOver.EditValue.ToString().Equals("HandOverProcess") || rdoTakeOver.EditValue.ToString().Equals("HandOverProcessNon"))
                {
                    if (string.IsNullOrWhiteSpace(popArea.EditValue.ToString()))
                    {
                        this.ShowMessage("NeedToInputAreaWhenTakeOver");//인계처리시 인계작업장을 입력해야합니다.
                        return;
                    }

                }
                // 결과 입력 VALIDATION
                if (CheckAllResultAndImageInput(grdInspectionItem, true) == false || CheckAllResultInput(grdMeasuredValue) == false)
                {
                    //2020-03-03 강유라 영풍만 이미지 저장 필수 풀어달라함
                    if (UserInfo.Current.Enterprise.Equals("INTERFLEX"))
                    {
                        if (_hasImage == false)
                        {
                            this.ShowMessage("NeedToAddImage");//판정 결과가 NG 인 항목은 적어도 하나의 이미지를 추가해야합니다.
                        }
                        else
                        {
                            this.ShowMessage("NeedToAllInspectionResult");//모든 검사 결과를 입력해야 합니다.
                        }
                    }
                    else if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                    {
                        this.ShowMessage("NeedToAllInspectionResult");//모든 검사 결과를 입력해야 합니다.
                    }

                    return;

                }

                if ( _type.Equals("insertData") && grdInspectionItem.View.RowCount > 0 && (_inspectionQty == 0 || string.IsNullOrWhiteSpace(Format.GetString(_inspectionQty))))
                {
                    this.ShowMessage("NoInspectionQtyAndPnl");//검사 수량을 먼저 입력하세요.
                    return;
                }

                var defectPNLQtyZero = ucProcessInspDefect.GetGridDataSource().AsEnumerable()
                    .Where(r => r["DEFECTQTYPNL"].ToSafeInt32() == 0).ToList().Count;

                if (defectPNLQtyZero > 0)
                {
                    this.ShowMessage("DefectQtyInputZero");//불량(폐기)처리 불량수량은 0을 입력할수 없습니다.
                    return;
                }     

                txtInspectionDate.EditValue = DateTime.Now;
            }


            result = this.ShowMessage(MessageBoxButtons.YesNo, "ResultRegisterConfirmNote");//검사 결과를 저장하시겠습니까?

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    btnClose.Enabled = false;

                    //DataSet rullSet = new DataSet();

                    //inspectionItemResult Table에 저장 될 Data
                    DataTable changed = (grdInspectionItem.DataSource as DataTable).Clone();

                    var tempDefect = (grdInspectionItem.DataSource as DataTable).AsEnumerable()
                       .Where(r => !string.IsNullOrWhiteSpace(r["INSPECTIONQTY"].ToString()))
                       .ToList();

                    if (tempDefect.Count > 0)
                    {
                        changed = tempDefect.CopyToDataTable();
                    }

                    changed.TableName = "list2";

                    //3. 파일을 저장하는 로직을 작성
                    //=====================================================================================================
                    //YJKIM : Server에 파일을 Upload하는 로직을 작성
                    //______________________________________________________________________________________________________
                    //    ColumnName      TableName -> |     SF_INSPECTIONFILE             |    SF_OBJECTFILEMAP
                    //______________________________________________________________________________________________________
                    //         리소스타입               |     FILEINSPECTIONTYPE            |       RESOURCETYPE
                    //         리소스아이디             |     FILERESOURCEID                |       RESOURCEID
                    //         파일아이디               |     Generate.createID("FILE-")    |       FILEID
                    //         리소스버전               |     *                             |       RESOURCEVERSION
                    //_______________________________________________________________________________________________________
                    DataTable fileUploadTableExterior = QcmImageHelper.GetImageFileTable();
                    int totalFileSizeExterior = 0;

                    //2020-02-19 강유라 이메일 첨부용
                    _FileToSendEmail = QcmImageHelper.GetImageFileTableToSendEmail();

                    foreach (DataRow originRow in changed.Rows)
                    {
                        if (!originRow.IsNull("FILENAME1"))
                        {
                            if (!originRow.GetString("FILEFULLPATH1").Equals(""))
                            {
                                DataRow newRow = fileUploadTableExterior.NewRow();
                                newRow["FILEID"] = originRow["FILEID1"];         //Server에서 FileID를 생성하여서 가져와야 한다.
                                newRow["FILENAME"] = originRow["FILEID1"];
                                newRow["FILEEXT"] = originRow.GetString("FILEEXT1").Replace(".", "");      //파일업로드시에는 확장자에서 . 을 빼야 한다.
                                newRow["FILEPATH"] = originRow["FILEINSPECTIONTYPE"];// originRow["FILEFULLPATH1"];
                                newRow["SAFEFILENAME"] = originRow["FILENAME1"];
                                newRow["FILESIZE"] = originRow["FILESIZE1"];
                                newRow["SEQUENCE"] = 1;
                                newRow["LOCALFILEPATH"] = originRow["FILEFULLPATH1"];
                                newRow["RESOURCETYPE"] = originRow["FILEINSPECTIONTYPE"];
                                newRow["RESOURCEID"] = originRow["FILERESOURCEID"];
                                newRow["RESOURCEVERSION"] = "*";
                                newRow["PROCESSINGSTATUS"] = "Wait";


                                //YJKIM : 서버에서 데이타베이스를 입력하기 위해 파일아이디를 전달해야 한다.


                                totalFileSizeExterior += originRow.GetInteger("FILESIZE1");

                                fileUploadTableExterior.Rows.Add(newRow);

                                //2020-02-19 강유라 이메일 첨부용
                                DataRow emailNewRow = _FileToSendEmail.NewRow();
                                emailNewRow["FILEDATA"] = originRow["FILEDATA1"];
                                emailNewRow["FILENAME"] = originRow["FILENAME1"];

                                _FileToSendEmail.Rows.Add(emailNewRow);
                            }
                        }

                        if (!originRow.IsNull("FILENAME2"))
                        {
                            if (!originRow.GetString("FILEFULLPATH2").Equals(""))
                            {
                                DataRow newRow2 = fileUploadTableExterior.NewRow();
                                newRow2["FILEID"] = originRow["FILEID2"];          //Server에서 FileID를 생성하여서 가져와야 한다.
                                newRow2["FILENAME"] = originRow["FILEID2"];
                                newRow2["FILEEXT"] = originRow.GetString("FILEEXT2").Replace(".", "");
                                newRow2["FILEPATH"] = originRow["FILEINSPECTIONTYPE"];//originRow["FILEFULLPATH2"];
                                newRow2["SAFEFILENAME"] = originRow["FILENAME2"];
                                newRow2["FILESIZE"] = originRow["FILESIZE2"];
                                newRow2["SEQUENCE"] = 2;
                                newRow2["LOCALFILEPATH"] = originRow["FILEFULLPATH2"];
                                newRow2["RESOURCETYPE"] = originRow["FILEINSPECTIONTYPE"];
                                newRow2["RESOURCEID"] = originRow["FILERESOURCEID"];
                                newRow2["RESOURCEVERSION"] = "*";
                                newRow2["PROCESSINGSTATUS"] = "Wait";

                                totalFileSizeExterior += originRow.GetInteger("FILESIZE2");

                                fileUploadTableExterior.Rows.Add(newRow2);

                                //2020-02-19 강유라 이메일 첨부용
                                DataRow emailNewRow = _FileToSendEmail.NewRow();
                                emailNewRow["FILEDATA"] = originRow["FILEDATA2"];
                                emailNewRow["FILENAME"] = originRow["FILENAME2"];

                                _FileToSendEmail.Rows.Add(emailNewRow);
                            }
                        }
                    }

                    //inspectionItemResult Table에 저장 될 Data - Measure
                    DataTable toRealSaveMeasure = (grdMeasuredValue.DataSource as DataTable).Clone();

                    TempSaveMeasureValueBeforeSave(grdInspectionItemSpec.View.GetFocusedDataRow());

                    if (measureDs.Tables.Count != 0)
                    {
                        foreach (DataTable dt in measureDs.Tables)
                        {
                            toRealSaveMeasure.Merge(dt);
                        }
                    }
                    else
                    {
                        toRealSaveMeasure = (grdMeasuredValue.DataSource as DataTable).Copy();
                    }
                    toRealSaveMeasure.TableName = "list3";//측정값 저장 데이터

                    DataTable measureImage = (grdInspectionItemSpec.DataSource as DataTable).Copy();
                    measureImage.TableName = "list5";//측정값이미지 데이터

                    DataTable fileUploadTableMeasure = QcmImageHelper.GetImageFileTable();
                    int totalFileSizeMeasure = 0;

                    foreach (DataRow originRow in measureImage.Rows)
                    {
                        if (!originRow.IsNull("FILENAME1"))
                        {
                            if (!originRow.GetString("FILEFULLPATH1").Equals(""))
                            {
                                DataRow newRow = fileUploadTableMeasure.NewRow();
                                newRow["FILEID"] = originRow["FILEID1"];         //Server에서 FileID를 생성하여서 가져와야 한다.
                                newRow["FILENAME"] = originRow["FILEID1"];
                                newRow["FILEEXT"] = originRow.GetString("FILEEXT1").Replace(".", "");      //파일업로드시에는 확장자에서 . 을 빼야 한다.
                                newRow["FILEPATH"] = originRow["FILEINSPECTIONTYPE"];// originRow["FILEFULLPATH1"];
                                newRow["SAFEFILENAME"] = originRow["FILENAME1"];
                                newRow["FILESIZE"] = originRow["FILESIZE1"];
                                newRow["SEQUENCE"] = 1;
                                newRow["LOCALFILEPATH"] = originRow["FILEFULLPATH1"];
                                newRow["RESOURCETYPE"] = originRow["FILEINSPECTIONTYPE"];
                                newRow["RESOURCEID"] = originRow["FILERESOURCEID"];
                                newRow["RESOURCEVERSION"] = "*";
                                newRow["PROCESSINGSTATUS"] = "Wait";

                                //YJKIM : 서버에서 데이타베이스를 입력하기 위해 파일아이디를 전달해야 한다.

                                totalFileSizeMeasure += originRow.GetInteger("FILESIZE1");

                                fileUploadTableMeasure.Rows.Add(newRow);


                                //2020-02-19 강유라 이메일 첨부용
                                DataRow emailNewRow = _FileToSendEmail.NewRow();
                                emailNewRow["FILEDATA"] = originRow["FILEDATA1"];
                                emailNewRow["FILENAME"] = originRow["FILENAME1"];

                                _FileToSendEmail.Rows.Add(emailNewRow);
                            }
                        }

                        if (!originRow.IsNull("FILENAME2"))
                        {
                            if (!originRow.GetString("FILEFULLPATH2").Equals(""))
                            {
                                DataRow newRow2 = fileUploadTableMeasure.NewRow();
                                newRow2["FILEID"] = originRow["FILEID2"];          //Server에서 FileID를 생성하여서 가져와야 한다.
                                newRow2["FILENAME"] = originRow["FILEID2"];
                                newRow2["FILEEXT"] = originRow.GetString("FILEEXT2").Replace(".", "");
                                newRow2["FILEPATH"] = originRow["FILEINSPECTIONTYPE"];//originRow["FILEFULLPATH2"];
                                newRow2["SAFEFILENAME"] = originRow["FILENAME2"];
                                newRow2["FILESIZE"] = originRow["FILESIZE2"];
                                newRow2["SEQUENCE"] = 2;
                                newRow2["LOCALFILEPATH"] = originRow["FILEFULLPATH2"];
                                newRow2["RESOURCETYPE"] = originRow["FILEINSPECTIONTYPE"];
                                newRow2["RESOURCEID"] = originRow["FILERESOURCEID"];
                                newRow2["RESOURCEVERSION"] = "*";
                                newRow2["PROCESSINGSTATUS"] = "Wait";

                                totalFileSizeMeasure += originRow.GetInteger("FILESIZE2");

                                fileUploadTableMeasure.Rows.Add(newRow2);

                                //2020-02-19 강유라 이메일 첨부용
                                DataRow emailNewRow = _FileToSendEmail.NewRow();
                                emailNewRow["FILEDATA"] = originRow["FILEDATA2"];
                                emailNewRow["FILENAME"] = originRow["FILENAME2"];

                                _FileToSendEmail.Rows.Add(emailNewRow);
                            }
                        }
                    }

                    //inspectionItemResult Table에 저장 될 Data - 불량처리
                    DataTable defectChanged = ucProcessInspDefect.GetGridDataSource();
                    defectChanged.TableName = "list4";

                    DataTable fileUploadTableDefect = QcmImageHelper.GetImageFileTable();
                    int totalFileSizeDefect = 0;
                    foreach (DataRow originRow in defectChanged.Rows)
                    {
                        if (!originRow.IsNull("FILENAME1"))
                        {
                            if (!originRow.GetString("FILEFULLPATH1").Equals(""))
                            {
                                DataRow newRow = fileUploadTableDefect.NewRow();
                                newRow["FILEID"] = originRow["FILEID1"];         //Server에서 FileID를 생성하여서 가져와야 한다.
                                newRow["FILENAME"] = originRow["FILEID1"];
                                newRow["FILEEXT"] = originRow.GetString("FILEEXT1").Replace(".", "");      //파일업로드시에는 확장자에서 . 을 빼야 한다.
                                newRow["FILEPATH"] = originRow["FILEINSPECTIONTYPE"];// originRow["FILEFULLPATH1"];
                                newRow["SAFEFILENAME"] = originRow["FILENAME1"];
                                newRow["FILESIZE"] = originRow["FILESIZE1"];
                                newRow["SEQUENCE"] = 1;
                                newRow["LOCALFILEPATH"] = originRow["FILEFULLPATH1"];
                                newRow["RESOURCETYPE"] = originRow["FILEINSPECTIONTYPE"];
                                newRow["RESOURCEID"] = originRow["FILERESOURCEID"];
                                newRow["RESOURCEVERSION"] = "*";
                                newRow["PROCESSINGSTATUS"] = "Wait";


                                //YJKIM : 서버에서 데이타베이스를 입력하기 위해 파일아이디를 전달해야 한다.


                                totalFileSizeDefect += originRow.GetInteger("FILESIZE1");

                                fileUploadTableDefect.Rows.Add(newRow);
                            }
                        }

                        if (!originRow.IsNull("FILENAME2"))
                        {
                            if (!originRow.GetString("FILEFULLPATH2").Equals(""))
                            {
                                DataRow newRow2 = fileUploadTableDefect.NewRow();
                                newRow2["FILEID"] = originRow["FILEID2"];          //Server에서 FileID를 생성하여서 가져와야 한다.
                                newRow2["FILENAME"] = originRow["FILEID2"];
                                newRow2["FILEEXT"] = originRow.GetString("FILEEXT2").Replace(".", "");
                                newRow2["FILEPATH"] = originRow["FILEINSPECTIONTYPE"];//originRow["FILEFULLPATH2"];
                                newRow2["SAFEFILENAME"] = originRow["FILENAME2"];
                                newRow2["FILESIZE"] = originRow["FILESIZE2"];
                                newRow2["SEQUENCE"] = 2;
                                newRow2["LOCALFILEPATH"] = originRow["FILEFULLPATH2"];
                                newRow2["RESOURCETYPE"] = originRow["FILEINSPECTIONTYPE"];
                                newRow2["RESOURCEID"] = originRow["FILERESOURCEID"];
                                newRow2["RESOURCEVERSION"] = "*";
                                newRow2["PROCESSINGSTATUS"] = "Wait";

                                totalFileSizeDefect += originRow.GetInteger("FILESIZE2");

                                fileUploadTableDefect.Rows.Add(newRow2);
                            }
                        }
                    }

                    //외관, 측정 사진 Merge
                    fileUploadTableExterior.Merge(fileUploadTableMeasure);
                    fileUploadTableExterior.Merge(fileUploadTableDefect);
                    //총 파일 사이즈 합
                    int totlaFileSize = totalFileSizeExterior + totalFileSizeMeasure + totalFileSizeDefect;

                    if (fileUploadTableExterior.Rows.Count > 0)
                    {
                        FileProgressDialog fileProgressDialog = new FileProgressDialog(fileUploadTableExterior, UpDownType.Upload, "", totlaFileSize);
                        fileProgressDialog.ShowDialog(this);

                        if (fileProgressDialog.DialogResult == DialogResult.Cancel)
                            throw MessageException.Create("CancelMessageToUploadFile");  //파일업로드를 취소하였습니다.

                        ProgressingResult fileResult = fileProgressDialog.Result;

                        if (!fileResult.IsSuccess)
                            throw MessageException.Create("FailMessageToUploadFile"); //파일업로드를 실패하였습니다.
                    }
                    //------------------------------------------------------------------------------------------------------

                    //inspectionResult Table에 저장 될 Data 
                    DataTable inspectionResult;
                    if (_type.Equals("insertData"))
                    {//sf_inspectionResult에 Insert
                        inspectionResult = GetInspectionResultTableToInsert(CurrentDataRow, toRealSaveMeasure);
                    }
                    else
                    {//sf_inspectionResult에 Update
                        inspectionResult = GetInspectionResultTableToUpdate(CurrentDataRow);
                    }

                    inspectionResult.TableName = "list";

 
                    CheckChanged(inspectionResult.Rows[0]["_STATE_"].ToString(), changed, toRealSaveMeasure, defectChanged);

                    /*
                    rullSet.Tables.Add(inspectionResult);
                    rullSet.Tables.Add(changed);
                    rullSet.Tables.Add(toRealSaveMeasure);
                    rullSet.Tables.Add(measureImage);
                    rullSet.Tables.Add(defectChanged.Copy());

                    ExecuteRule("SaveOSPInspectionResult", rullSet);
                    */

                    MessageWorker worker = new MessageWorker("SaveOSPInspectionResult");
                    worker.SetBody(new MessageBody()
                    {

                        { "list", inspectionResult},
                        { "list2", changed},
                        { "list3", toRealSaveMeasure},
                        { "list4", defectChanged.Copy()},
                        { "list5", measureImage},
                        { "ENTERPRISEID", UserInfo.Current.Enterprise},
                        { "PLANTID", UserInfo.Current.Plant},
                        { "inspectionclassId", "OSPInspection"}//외관/측정 구분 필요?

                    });

                    var isSendEmailDt = worker.Execute<DataTable>();
                    var isSendEmailRS = isSendEmailDt.GetResultSet();

                    if (isSendEmailRS.Rows.Count > 0)
                    {
                        DataRow responseRow = isSendEmailRS.Rows[0];

                        if (responseRow["ISSENDEMAIL"].ToString().Equals("True"))
                        {
                            string exteriorNG = responseRow["EXTERIORNG"].ToString();
                            string measureNG  = responseRow["MEASURENG"].ToString();
                            
                            DataTable toSendDt = CommonFunction.CreateProcessAbnormalEmailDt();

                            DataRow newRow = toSendDt.NewRow();
                            newRow["PRODUCTDEFNAME"] = CurrentDataRow["PRODUCTDEFNAME"];
                            newRow["PRODUCTDEFID"] = CurrentDataRow["PRODUCTDEFID"];
                            newRow["PRODUCTDEFVERSION"] = CurrentDataRow["PRODUCTDEFVERSION"];
                            newRow["LOTID"] = CurrentDataRow["LOTID"];
                            newRow["PROCESSSEGMENTCLASSNAME"] = CurrentDataRow["PROCESSSEGMENTCLASSNAME"];
                            newRow["PROCESSSEGMENTCLASSID"] = CurrentDataRow["PROCESSSEGMENTCLASSIDTOP"];
                            newRow["PROCESSSEGMENTNAME"] = CurrentDataRow["PROCESSSEGMENTNAME"];
                            newRow["PROCESSSEGMENTID"] = CurrentDataRow["PROCESSSEGMENTID"];
                            newRow["AREANAME"] = CurrentDataRow["AREANAME"];
                            newRow["AREAID"] = CurrentDataRow["AREAID"];
                            newRow["INSPECTIONRESULT"] = "NG";
                            newRow["DEFECTNAME"] = exteriorNG;
                            newRow["MEASUREVALUE"] = measureNG;
                            newRow["REMARK"] = "";
                            newRow["USERID"] = UserInfo.Current.Id;
                            newRow["TITLE"] = Language.Get("OSPABNORMALTITLE");
                            newRow["INSPECTION"] = "ProcessInspection";
                            newRow["LANGUAGETYPE"] = UserInfo.Current.LanguageType;

                            toSendDt.Rows.Add(newRow);

                            //2020-02-19 강유라 이메일 첨부용
                            //CommonFunction.ShowSendEmailPopupDataTable(toSendDt, _FileToSendEmail);
                            CommonFunction.ShowSendEmailPopupDataTable(toSendDt);
                        }
                    }
                    this.DialogResult = DialogResult.OK;
                    ShowMessage("SuccessSave");
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    btnSave.Enabled = true;
                    btnClose.Enabled = true;
                    this.Close();

                    _parent.SearchMainGrd(); // 2020.02.21 유석진 현황조회 추가
                }
            }
        }

        /// <summary>
        /// 닫기버튼을 클릭했을때 팝업을 닫는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// CellMerge이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (view == null) return;

            if (e.Column.FieldName == "TYPE" || e.Column.FieldName == "INSPECTIONSTANDARD")
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

        #region Public Function
        /// <summary>
        /// popup을 로드할때 조회된 데이터를 각각의 컨트롤에 
        /// 할당해주는 함수
        /// </summary>
        public int SetControlSearchData(DataRow row)
        {
            int rowCount = 0;

            InitializeCombo();
            InitializeGrid(_type);
            InitializeExteriorGrid(_type);
            InitializationSummaryRow();
            InitializeEvent();
            

            if (row == null) return rowCount;

            txtLotId.Editor.EditValue = row["LOTID"];
            cboProcessingStatus.Editor.EditValue = row["RESULTITEM"];
            txtInspectionDate.Editor.EditValue = row["INSPECTIONDATE"];
            popInspector.Editor.EditValue = UserInfo.Current.Name;
            txtDegree.Editor.EditValue = row["DEGREE"];   
            rdoTakeOver.EditValue = row["ISSEND"];

            if (_type.Equals("updateData"))
            {
                cboStandardType.Editor.EditValue = row["JUDGMENTCRITERIA"];
                txtInspectionResult.Editor.EditValue = row["INSPECTIONRESULT"];
                popInspector.Editor.EditValue = row["INSPECTIONUSER"];
                popInspector.Editor.Text = Format.GetString(row["INSPECTIONUSER"]);
            }

            //인계할 작업장
            //popArea.SetValue(row["TOAREAID"]);
            popArea.Text = row["TRANSITAREANAME"].ToString();

            //lotinfoSelect
            ucLotInfo.ClearData();
            rowCount = SearchLotInfo();

            return rowCount;
        }
        #endregion

        #region Private Function
        /*
        /// <summary>
        /// 대분류에 해당하는 소분류 나열 순서를 정해주는 함수
        /// ex) 대분류 10,20,30..
        ///     소분류 10-1,10-2...
        /// </summary>
        private DataTable SetDataTableOrder(DataTable table1, DataTable table2)
        {
            int inspItemclassNo = 10;

            DataTable bindingTable = table2.Clone();
            DataRow addRow;

            foreach (DataRow row1 in table1.Rows)
            {
                int inspItemNo = 1;
                row1["SORT"] = inspItemclassNo;
                row1["PROCESSRELNO"] = CurrentDataRow["LOTHISTKEY"] + CurrentDataRow["DEGREE"].ToString();

                foreach (DataRow row2 in table2.Rows)
                {
                    if (row1["INSPITEMID"].Equals(row2["INSPITEMCLASSID"]))
                    {
                        row2["SORT"] = inspItemclassNo + "_" + inspItemNo;
                        row2["PROCESSRELNO"] = CurrentDataRow["LOTHISTKEY"] + CurrentDataRow["DEGREE"].ToString();

                        addRow = bindingTable.NewRow();
                        addRow.ItemArray = row2.ItemArray.Clone() as object[];
                        bindingTable.Rows.Add(addRow);
                        inspItemNo++;
                    }
                }

                addRow = bindingTable.NewRow();
                addRow.ItemArray = row1.ItemArray.Clone() as object[];
                bindingTable.Rows.Add(addRow);
                inspItemclassNo += 10;
            }

            return bindingTable;
        }
        */
        /// <summary>
        /// sf_inspectionresult에 insert할 테이블 생성 함수
        /// </summary>
        private DataTable GetInspectionResultTableToInsert(DataRow searchedRow, DataTable measureDt)
        {

            string priotiryQCGrade = InspectionHelper.GetPriorityQCGradeOSP(grdInspectionItem, measureDt, cboStandardType.Editor.EditValue.ToString());

            //inspectionResult Table에 저장될 Data
            DataTable inspectionTable = new DataTable();
            DataRow row = null;

            inspectionTable.Columns.Add(new DataColumn("TXNHISTKEY", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("RESOURCETYPE", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("RESOURCEID", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("LOTHISTKEY", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("PROCESSRELNO", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("ENTERPRISEID", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("PLANTID", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("AREAID", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("TORESOURCEID", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("PRODUCTDEFID", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("PRODUCTDEFVERSION", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("PROCESSSEGMENTID", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("PROCESSSEGMENTVERSION", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("USERSEQUENCE", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("INSPECTIONDATE", typeof(DateTime)));
            inspectionTable.Columns.Add(new DataColumn("INSPECTIONUSER", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("INSPECTIONRESULT", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("ISSEND", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("DEGREE", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("REASONCODEID", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("RESULTITEM", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("DEFECTQTYSUM", typeof(double)));
            inspectionTable.Columns.Add(new DataColumn("DEFECTQTYSUMPNL", typeof(double)));
            inspectionTable.Columns.Add(new DataColumn("ISLOCKING", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("_STATE_", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("INSPECTIONQTY", typeof(Int16)));
            inspectionTable.Columns.Add(new DataColumn("JUDGMENTCRITERIA", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("PROCESSDEFID", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("PROCESSDEFVERSION", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("WORKCOUNT", typeof(double)));
            inspectionTable.Columns.Add(new DataColumn("INSPECTIONCLASSID", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("QCGRADE", typeof(string)));

            row = inspectionTable.NewRow();
            row["TXNHISTKEY"] = searchedRow["TXNHISTKEY"];//검사정보
            row["RESOURCETYPE"] = "ProcessInspection";
            row["RESOURCEID"] = searchedRow["LOTID"];
            row["LOTHISTKEY"] = searchedRow["LOTHISTKEY"];//Lot
            row["PROCESSRELNO"] = searchedRow["LOTHISTKEY"] + searchedRow["DEGREE"].ToString();
            row["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            row["PLANTID"] = UserInfo.Current.Plant;

            /*
            string resourceId = null;
            string transitAreaId = null;

            if (popArea.GetValue() != null && !string.IsNullOrWhiteSpace(popArea.GetValue().ToString()))
            {
                resourceId = popArea.GetValue().ToString().Split('|')[0];
                transitAreaId = popArea.GetValue().ToString().Split('|')[1];   
            }*/

            row["TORESOURCEID"] = _toResourceId;
            row["AREAID"] = _toAreaId;

            row["PRODUCTDEFID"] = searchedRow["PRODUCTDEFID"];
            row["PRODUCTDEFVERSION"] = searchedRow["PRODUCTDEFVERSION"];
            row["PROCESSSEGMENTID"] = searchedRow["PROCESSSEGMENTID"];
            row["PROCESSSEGMENTVERSION"] = searchedRow["PROCESSSEGMENTVERSION"];
            row["USERSEQUENCE"] = searchedRow["USERSEQUENCE"];
            row["INSPECTIONDATE"] = txtInspectionDate.Text;
            row["INSPECTIONUSER"] = popInspector.Text;// 이름으로
            //row["INSPECTIONUSER"] = popInspector.GetValue();// 아이디로
            row["INSPECTIONRESULT"] = txtInspectionResult.Editor.EditValue;
            row["ISSEND"] = rdoTakeOver.EditValue;
            row["DEGREE"] = searchedRow["DEGREE"];
            row["REASONCODEID"] = "LockProcessNonconfirm";
            row["RESULTITEM"] = cboProcessingStatus.Editor.EditValue;
            row["DEFECTQTYSUM"] = ucProcessInspDefect.SetDefectQtySum();
            row["DEFECTQTYSUMPNL"] = ucProcessInspDefect.SetDefectQtyPNLSum();
            row["ISLOCKING"] = _isLocking;
            row["_STATE_"] = "added";
            row["INSPECTIONQTY"] = _inspectionQty;
            row["JUDGMENTCRITERIA"] = cboStandardType.Editor.EditValue;
            row["PROCESSDEFID"] = searchedRow["PROCESSDEFID"];
            row["PROCESSDEFVERSION"] = searchedRow["PROCESSDEFVERSION"];
            row["WORKCOUNT"] = searchedRow["WORKCOUNT"];
            row["INSPECTIONCLASSID"] = "OSPInspection";
            row["QCGRADE"] = priotiryQCGrade;

            inspectionTable.Rows.Add(row);

            return inspectionTable;
        }

        /// <summary>
        /// sf_inspectionresult에 update할 테이블 생성 함수
        /// </summary>
        private DataTable GetInspectionResultTableToUpdate(DataRow searchedRow)
        {
            //inspectionResult Table에 저장될 Data
            DataTable inspectionTable = new DataTable();
            DataRow row = null;

            //key
            inspectionTable.Columns.Add(new DataColumn("TXNHISTKEY", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("PROCESSRELNO", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("RESOURCEID", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("RESOURCETYPE", typeof(string)));
           

            //내용
            inspectionTable.Columns.Add(new DataColumn("INSPECTIONUSER", typeof(string)));
            inspectionTable.Columns.Add(new DataColumn("_STATE_", typeof(string)));

            row = inspectionTable.NewRow();
            row["TXNHISTKEY"] = searchedRow["TXNHISTKEY"];
            row["PROCESSRELNO"] = searchedRow["PROCESSRELNO"];
            row["RESOURCEID"] = searchedRow["LOTID"];
            row["RESOURCETYPE"] = "ProcessInspection";
  

            row["INSPECTIONUSER"] = popInspector.Text; //이름
           // row["INSPECTIONUSER"] = popInspector.GetValue();//아이디

            if (searchedRow["INSPECTIONUSER"].ToString().Equals(popInspector.GetValue().ToString()))
            {
                row["_STATE_"] = "unchanged";
            }
            else
            {
                row["_STATE_"] = "modified";
            }
            inspectionTable.Rows.Add(row);

            return inspectionTable;
        }


        /// <summary>
        /// 입력된 값이 spec값을 벗어나지 않았는지 체크하여 결과를 return 하는 함수
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String CheckValidationSpecOut(DataRow row)
        {
            string result = "OK";
            if (!string.IsNullOrWhiteSpace(row["MEASUREVALUE"].ToString()))
            {
                DataRow itemRow = grdInspectionItemSpec.View.GetFocusedDataRow();
     
                //LSL, USL 
                double lsl = row["LOWERSPECLIMIT"].ToString().ToSafeDoubleNaN();
                double usl = row["UPPERSPECLIMIT"].ToString().ToSafeDoubleNaN();

                //입력된 값
                double measureValue = row["MEASUREVALUE"].ToSafeDoubleNaN();

                //입력된 값이 Spec범위를 벗어났을 경우
                if (lsl > measureValue || usl < measureValue)
                {
                    result = "NG";
                    itemRow["INSPECTIONRESULT"] = "NG";
                }
                else
                {
                    DataTable toCheck = grdMeasuredValue.DataSource as DataTable;
                    int NGCount = GetNGCount(toCheck);

                    if (NGCount == 0)
                    {
                        itemRow["INSPECTIONRESULT"] = "OK";
                    }
                    else
                    {
                        itemRow["INSPECTIONRESULT"] = "NG";
                    }
                  
                }

                return result;
            }
            else
            {
                return "";
            }

        }


        /// <summary>
        /// 모든 검사 결과를 입력 했는지, 사진을 첨부했는지 확인하는 함수
        /// </summary>
        /// <returns></returns>
        private Boolean CheckAllResultAndImageInput(SmartBandedGrid grid, bool isCheckQTY = false)
        {
            DataTable changed = grid.DataSource as DataTable;

            bool inputAllResult = true;
            _hasImage = true;

            foreach (DataRow row in changed.Rows)
            {
                if (string.IsNullOrWhiteSpace(row["INSPECTIONRESULT"].ToString()) || (isCheckQTY == true && string.IsNullOrWhiteSpace(row["INSPECTIONQTY"].ToString())))
                {
                    inputAllResult = false;
                    break;
                }

                if (row["INSPECTIONRESULT"].ToString().Equals("NG"))
                {
                    if (string.IsNullOrWhiteSpace(row["FILEFULLPATH1"].ToString()) && string.IsNullOrWhiteSpace(row["FILEFULLPATH2"].ToString()))
                    {
                        if (UserInfo.Current.Enterprise.Equals("INTERFLEX"))
                        inputAllResult = false;

                        _hasImage = false;
                        break;
                    }
                }
            }

            return inputAllResult;
        }

        /// <summary>
        /// 모든 검사 결과를 입력 했는지 확인하는 함수
        /// </summary>
        /// <returns></returns>
        private Boolean CheckAllResultInput(SmartBandedGrid grid)
        {
            DataTable changed = grid.DataSource as DataTable;

            bool inputAllResult = true;
            _hasImage = true;

            foreach (DataRow row in changed.Rows)
            {
                if (string.IsNullOrWhiteSpace(row["INSPECTIONRESULT"].ToString()))
                {
                    inputAllResult = false;
                    break;
                }
            }

            return inputAllResult;
        }

        /// <summary>
        /// 저장 할 데이터가 있는지 체크하는 함수
        /// </summary>
        /// <param name="state"></param>
        /// <param name="table"></param>
        /// <param name="table2"></param>
        /// <param name="table3"></param>
        private void CheckChanged(string state, DataTable table, DataTable table2, DataTable table3)
        {
            if (state.Equals("unchanged")/* && table.Rows.Count == 0 && table2.Rows.Count == 0 && table3.Rows.Count == 0*/)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        /// <summary>
        /// 컨트롤 세팅을 하는 함수
        /// </summary>
        private void SettingControl()
        {
            grpLotInfo.GridButtonItem = GridButtonItem.None;
            grpInspInfo.GridButtonItem = GridButtonItem.None;
            grpTakeOverInfo.GridButtonItem = GridButtonItem.None;
            Dictionary<string, object> radioParam = new Dictionary<string, object>()
            {
                {"CODECLASSID", "IsHandOverProcess" },
                {"LANGUAGETYPE", UserInfo.Current.LanguageType}
            };
 
            DataTable radioDt = SqlExecuter.Query("GetCodeList", "00001", radioParam);

            foreach (DataRow dr in radioDt.Rows)
                rdoTakeOver.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(dr["CODEID"], dr["CODENAME"].ToString()));

           // rdoTakeOver.Properties.Items[0].Description = Language.Get("인계처리");
           // rdoTakeOver.Properties.Items[1].Description = Language.Get("입고의뢰취소");

            _grid = grdInspectionItem;
            _commonPic = picExterior;
            _commonPic2 = picExterior2;

            picExterior.Properties.ShowMenu = false;
            picExterior2.Properties.ShowMenu = false;
            picMeasure.Properties.ShowMenu = false;
            picMeasure2.Properties.ShowMenu = false;

            measureDs = new DataSet();
            SetReadOnlyControl();
        }

        /// <summary>
        /// lotInfo를 조회하는 함수
        /// </summary>
        private int SearchLotInfo()
        {
            int rowCount = 0;

            //2020-03-24 강유라 / 마지막 재작업인지  아닌지 판단
            Dictionary<string, object> param1 = new Dictionary<string, object>();
            param1.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param1.Add("PLANTID", UserInfo.Current.Plant);
            param1.Add("LOTID", txtLotId.Text);
            param1.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable processDefTypeInfo = SqlExecuter.Query("GetProcessDefTypeByProcess", "10001", param1);

            if (processDefTypeInfo.Rows.Count > 0)
            {
                DataRow processDefTypeRow = processDefTypeInfo.AsEnumerable().FirstOrDefault();

                processDefType = Format.GetString(processDefTypeRow["PROCESSDEFTYPE"]);
                lastRework = Format.GetString(processDefTypeRow["LASTREWORK"]);
            }

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", CurrentDataRow["PLANTID"]);
            param.Add("LOTID", txtLotId.Text);
            param.Add("LOTHISTKEY", CurrentDataRow["LOTHISTKEY"]);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            //2019 12-14 재작업일 경우 다른 쿼리
            //string queryVersion = CurrentDataRow["ISREWORK"].ToString().Equals("Y") ? "10002" : "10001";

            //DataTable lotInfo = SqlExecuter.Query("SelectLotInfoByLotIDOSP", queryVersion, param);
            DataTable lotInfo = SqlExecuter.Query("SelectLotInfoByLotIDOSP", "10002", param);

            rowCount = lotInfo.Rows.Count;

            if (rowCount > 0)
            {
                ucLotInfo.DataSource = lotInfo;
                _lotRow = lotInfo.Rows[0];
            }
            else
            {
                ucLotInfo.DataSource = lotInfo;
                // TODO : Inner Join 조건 확인(존재하지만 공정이 없는 LOT도 조회 안됨)
                // 해당 Lot이 존재하지 않습니다. {0}
                this.ShowMessage("NotExistLot", string.Format("LotId = {0}", txtLotId.Text));
                this.Close();
            }

            return rowCount;
        }

        /// <summary>
        /// _type 에따라 컨트롤 readOnly처리 함수
        /// </summary>
        private void SetReadOnlyControl()
        {
            if (_type.Equals("updateData"))
            {
                cboProcessingStatus.Editor.ReadOnly = true;
                rdoTakeOver.ReadOnly = true;
                popArea.ReadOnly = true;
                popArea.Enabled = false;
                cboStandardType.Editor.ReadOnly = true;
             
            }
        }
        /*
        // <summary>
        /// 입력된 검사수량, 불량수량을 통한 불량율 계산값이 기준에 벗어나지 않았는지 체크하여 결과를 return 하는 함수********불량율 기준 정해지면 수정******
        /// ***********************************************************불량NCR로 자동 Locking 걸릴시 인계처리 불가 의뢰취소만 가능
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String CheckValidationDefectRate(DataRow row)
        {
            if (string.IsNullOrWhiteSpace(cboStandardType.Editor.EditValue.ToString()))
            {
                throw MessageException.Create("SelectInspStandard");//판정기준(외관검사) 선택하세요.
            }

            string result = "";
            if (cboStandardType.Editor.EditValue.ToString().Equals("AQL"))
            {
                if (string.IsNullOrWhiteSpace(row["AQLINSPECTIONLEVEL"].ToString()) ||
                    string.IsNullOrWhiteSpace(row["AQLDEFECTLEVEL"].ToString()) ||
                    string.IsNullOrWhiteSpace(_lotRow["PCSQTY"].ToString()))
                {
                    ShowMessage("NoStandardData");//판정 기준이 없습니다.
                        return "NG";
                }
      

                //spec값을 찾기 위한 currentRow의 파라미터
                Dictionary<string, object> specParam = new Dictionary<string, object>();
                specParam.Add("P_SPCLEVEL", row["AQLINSPECTIONLEVEL"]);
                specParam.Add("P_DEFLEVEL", row["AQLDEFECTLEVEL"]);
                specParam.Add("P_LOTQTY", _lotRow["PCSQTY"]);

                //spec값을 가져오는 쿼리 실행
                SqlQuery AQLQuery = new SqlQuery("SelectAQLCheckBasis", "10001", specParam);
                DataTable AQLDt = AQLQuery.Execute();

                //spec값
                DataRow AQLRow = AQLDt.Rows[0];

                //스펙기준 값
                string standardDefectQty = AQLRow["DEFECTRATE"].ToString();

                if (!string.IsNullOrWhiteSpace(standardDefectQty))
                {
                    decimal defectQty = standardDefectQty.ToSafeDecimal();

                    //불량수량이 기준을 벗어났을 경우
                    if (row["DEFECTQTY"].ToSafeDecimal() > defectQty)//specLimitRow["DEFECTRATE"]
                    {
                        result = "NG";
                    }
                    else
                    {
                        result = "OK";
                    }
                }
            }
            else if (cboStandardType.Editor.EditValue.ToString().Equals("NCR"))
            {
                if (string.IsNullOrWhiteSpace(row["NCRDECISIONDEGREE"].ToString()))
                {
                    ShowMessage("NoStandardData");//판정 기준이 없습니다.
                    return "NG";
                }

                //spec값을 찾기 위한 currentRow의 파라미터
                Dictionary<string, object> specParam = new Dictionary<string, object>();
                specParam.Add("P_INSPECTIONCLASSID", "OSPInspection");
                specParam.Add("P_NCRDECISIONDEGREE", row["NCRDECISIONDEGREE"]);

                //spec값을 가져오는 쿼리 실행
                SqlQuery NCRQuery = new SqlQuery("SelectNCRCheckBasis", "10001", specParam);
                DataTable NCRDt = NCRQuery.Execute();

                //spec값
                DataRow NCRRow = NCRDt.Rows[0];

                //입력된 값
                string defectRateString = NCRRow["NGRATE"].ToString();
                string defectQuantityString = NCRRow["NGQUANTITY"].ToString();

                if (!string.IsNullOrWhiteSpace(defectRateString))
                {
                    decimal defectRate = defectRateString.ToSafeDecimal();

                    //불량율이 기준을 벗어났을 경우
                    decimal inputedDefectRate = row["DEFECTRATE"].ToString().Replace("%", "").ToSafeDecimal();
                    if (inputedDefectRate > defectRate)
                    {
                        result = "NG";
                    }
                    else
                    {
                        result = "OK";
                    }
                }
                else if (!string.IsNullOrWhiteSpace(defectQuantityString))
                {
                    decimal defectQuantity = defectQuantityString.ToSafeDecimal();

                    //불량율이 기준을 벗어났을 경우
                    if (row["DEFECTQTY"].ToSafeDecimal() >= defectQuantity)
                    {
                        result = "NG";
                    }
                    else
                    {
                        result = "OK";
                    }
                }
            }

            return result;
        }
        */
        #region 검색
        /// <summary>
        /// 조회 함수
        /// </summary>
        private void OnSearch()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);
            values.Add("PLANTID", CurrentDataRow["PLANTID"]);
            values.Add("P_RESOURCEID", CurrentDataRow["LOTID"]);
            values.Add("RESOURCETYPE", "ProcessInspection");
            values.Add("P_PROCESSRELNO", CurrentDataRow["LOTHISTKEY"] + CurrentDataRow["DEGREE"].ToString());
            values.Add("P_RELRESOURCEID", CurrentDataRow["RESULTPRODUCTDEFID"]);
            values.Add("P_RELRESOURCEVERSION", CurrentDataRow["RESULTPRODUCTDEFVERSION"]);
            values.Add("P_INSPECTIONCLASSID", "OSPInspection");
            values.Add("P_RESULTTXNHISTKEY", CurrentDataRow["TXNHISTKEY"].ToString());
            values.Add("P_RESULTTXNGROUPHISTKEY", CurrentDataRow["TXNGROUPHISTKEY"].ToString());
            //측정검사 검사항목 가져오기위한 파라미터
            values.Add("P_PROCESSSEGMENTID", CurrentDataRow["RESULTPROCESSSEGMENTID"]);
            values.Add("P_PROCESSSEGMENTVERSION", CurrentDataRow["RESULTPROCESSSEGMENTVERSION"]);

            //외관검사
            //ItemClassTable
            //DataTable exteriorItemClassTable = SqlExecuter.Query("SelectItemClassToInspection", "10002", values);
            //ItemTable
            //DataTable exteriorItemTable = SqlExecuter.Query("SelectItemToInspection", "10002", values);

            //values.Remove("P_INSPITEMTYPE");
   
            //외관검사 - 불량코드
            DataTable exteriorDefectCode = SqlExecuter.Query("SelectOSPInspectionExterior", "10001", values);

            if (cboStandardType.Editor.EditValue.ToString().Equals("AQL"))
            {
                grdInspectionItem.DataSource = SetAQLQTY(exteriorDefectCode);
            }
            else
            {
                grdInspectionItem.DataSource = InspectionHelper.SetProcessRelNo(CurrentDataRow["LOTHISTKEY"] + CurrentDataRow["DEGREE"].ToString(), exteriorDefectCode);
            }

            //SearchImageByRow(grdInspectionItem.View.GetFocusedDataRow(), "DefectCode", picExterior, picExterior2);

            values.Remove("P_INSPECTIONCLASSID");
            values.Add("P_INSPECTIONCLASSID", "OperationInspection");

            //측정검사
            //Item
            DataTable itemDt = SqlExecuter.Query("SelectOSPInspectionMeasure", "10001", values);

            SelectMeasureValueAfterSave();

            grdInspectionItemSpec.DataSource = InspectionHelper.SetProcessRelNo(CurrentDataRow["LOTHISTKEY"] + CurrentDataRow["DEGREE"].ToString(), itemDt);
            //SearchImageByRow(grdInspectionItemSpec.View.GetFocusedDataRow() , "InspItemId", picMeasure,picMeasure2);


            DataRow itemRow = grdInspectionItemSpec.View.GetFocusedDataRow();
            BindingMeasureValueAfterSave(itemRow);

            //ItemTable
            //DataTable measureDt = SqlExecuter.Query("SelectItemToInspectionOSP", "10001", values);

            //불량처리
            DataTable defectTable = SqlExecuter.Query("SelectOSPInspDefect", "10001", values);
            ucProcessInspDefect.SetLotIdAndData(CurrentDataRow, _lotRow, defectTable, _type);

            //grdInspectionItem - 합불판정
            //grdInspectionItem.DataSource = SetDataTableOrder(exteriorItemClassTable, exteriorItemTable);//대분류, 소분류를 정렬시켜 그리드 바인딩


            //grdInspectionItemSpec - 측정검사
            //grdInspectionItemSpec.DataSource = SetDataTableOrder(measureItemClassTable, measureItemTable);//대분류, 소분류를 정렬시켜 그리드 바인딩
        }

        #endregion

        /// <summary>
        /// 검사자를 선택하는 팝업
        /// </summary>
        private void InitializePopup()
        {
            popInspector.Editor.SelectPopupCondition = CreateUserPopup();
            
            popArea.SelectPopupCondition = CreateAreaPopup(_lotRow);

        }
        /// <summary>
        /// 측정검사의 검사항목이 바뀔때 이미 입력된 데이터를 임시저장하는 함수
        /// </summary>
        /// <param name="beforeRow"></param>
        private void TempSaveMeasureValueBeforeSave(DataRow beforeRow)
        {
            if ((grdMeasuredValue.DataSource as DataTable).Rows.Count == 0) return;

            var toSaveCount = (grdMeasuredValue.DataSource as DataTable).AsEnumerable()
                    .Where(r => !r["ISDELETE"].ToString().Equals("Y"))
                    .ToList();

            if (toSaveCount.Count == 0) return;

            DataTable toSaveDt = toSaveCount.CopyToDataTable();

            string tableName = beforeRow["INSPITEMID"].ToString();
            if (measureDs == null)
            {
                toSaveDt.TableName = tableName;
                measureDs.Tables.Add(toSaveDt);
            }
            else
            {
                DataTable dt = measureDs.Tables[tableName];

                if (dt == null)
                {//DataSet에 임시저장 데이터 없을때
                    toSaveDt.TableName = tableName;
                    measureDs.Tables.Add(toSaveDt);
                }
                else
                {//DataSet에 임시저장 데이터 있을때
                    measureDs.Tables.Remove(dt);
                    dt = toSaveDt;
                    dt.TableName = tableName;
                    measureDs.Tables.Add(dt);
                }
            }
        }

        /// <summary>
        /// 저장 전 검사항목의 포커스가 바뀔 때 임시저장한 테이블을 바인딩 시켜주는 함수
        /// </summary>
        /// <param name="row"></param>
        private void BindingMeasureValueBeforeSave(DataRow row)
        {
            if (row == null) return;
            DataTable tempDt = measureDs.Tables[row["INSPITEMID"].ToString()];
            if (tempDt == null)
            {
                grdMeasuredValue.View.ClearDatas();
            }
            else
            {
                grdMeasuredValue.DataSource = tempDt;
            }

        }

        /// <summary>
        /// 측정검사의 검사항목이 바뀔때 다른 측정값을 검색하는 함수
        /// </summary>
        private void SelectMeasureValueAfterSave()
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"ENTERPRISEID", UserInfo.Current.Enterprise},
                {"PLANTID", CurrentDataRow["PLANTID"]},
                {"P_TXNGROUPHISTKEY", CurrentDataRow["TXNGROUPHISTKEY"]},
                {"P_RESOURCEID",CurrentDataRow["LOTID"]},
                {"P_PROCESSRELNO",CurrentDataRow["LOTHISTKEY"] + CurrentDataRow["DEGREE"].ToString()},
                { "RESOURCETYPE","ProcessInspection"}
            };

            _selectMeasureValueDt = SqlExecuter.Query("SelectOSPMeasureByInspItem", "10001", values);
        }

        /// <summary>
        /// Select한 측정값중 inspitemId와 version 으로 해당 내역만 바인딩하는 함수
        /// </summary>
        /// <param name="row"></param>
        private void BindingMeasureValueAfterSave(DataRow row)
        {
            if (row == null)
            {
                grdMeasuredValue.View.ClearDatas();
                return;
            }

            var toBiding = _selectMeasureValueDt.AsEnumerable()
                .Where(r => r["INSPITEMID"].ToString().Equals(row["INSPITEMID"]) && r["INSPITEMVERSION"].ToString().Equals(row["INSPITEMVERSION"]))
                .ToList();

            if (toBiding.Count > 0)
            {
                grdMeasuredValue.DataSource = toBiding.CopyToDataTable();
            }
            else
            {
                grdMeasuredValue.View.ClearDatas();
            }
        }

        /// <summary>
        /// 판정 기준이 AQL일때 
        /// </summary>
        /// <param name="row"></param>
        private DataTable SetAQLQTY(DataTable exteriorDefectCode)
        {
            if (exteriorDefectCode.Rows.Count != 0)
            { 
                DataRow paramRow = exteriorDefectCode.Rows[0];

                if (string.IsNullOrWhiteSpace(paramRow["AQLINSPECTIONLEVEL"].ToString()) ||
                  string.IsNullOrWhiteSpace(paramRow["AQLDEFECTLEVEL"].ToString()) ||
                  string.IsNullOrWhiteSpace(_lotRow["PCSQTY"].ToString()))
                {
                    ShowMessage("NoStandardData");//판정 기준이 없습니다.
                    return exteriorDefectCode;
                }

                //spec값을 찾기 위한 currentRow의 파라미터
                Dictionary<string, object> specParam = new Dictionary<string, object>();
                specParam.Add("P_SPCLEVEL", paramRow["AQLINSPECTIONLEVEL"]);
                specParam.Add("P_DEFLEVEL", paramRow["AQLDEFECTLEVEL"]);
                specParam.Add("P_LOTQTY", _lotRow["PCSQTY"]);

                //spec값을 가져오는 쿼리 실행
                SqlQuery AQLQuery = new SqlQuery("SelectAQLCheckBasis", "10001", specParam);
                DataTable AQLDt = AQLQuery.Execute();

                if (AQLDt.Rows.Count != 0)
                {//spec값
                    DataRow AQLRow = AQLDt.Rows[0];

                    //2020-03-26 강유라 
                    // 영풍 - AQL 기준 숫자보다 LOT수량 적으면 검사 수량  => lot 수량
                    // 인터 - 기존
                    decimal AqlSize = 0;
                    AqlSize = Format.GetDecimal(AQLRow["AQLSIZE"]);

                    if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
                    {               
                        AqlSize = Format.GetDecimal(_lotRow["PCSQTY"]) < AqlSize ? Format.GetDecimal(_lotRow["PCSQTY"]) : AqlSize;                        
                    }

                    txtAQLInspectionQty.Editor.EditValue = AqlSize;
                    txtAQLInspectionLevel.Editor.EditValue = paramRow["AQLINSPECTIONLEVEL"];

                    foreach (DataRow row in exteriorDefectCode.Rows)
                    {// 검사수량
                        row["INSPECTIONQTY"] = AqlSize;
                        row["PROCESSRELNO"] = CurrentDataRow["LOTHISTKEY"] + CurrentDataRow["DEGREE"].ToString();
                        row["INSPECTIONQTYPNL"] = _lotRow["PANELPERQTY"].ToSafeDecimal() == 0 ? 0 :Math.Ceiling(AqlSize / _lotRow["PANELPERQTY"].ToSafeDecimal());

                    }
                }
            }
            return exteriorDefectCode;
        }

        /// <summary>
        /// dt의 판정결과 NG갯수 반환 함수
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private int GetNGCount(DataTable table)
        {
            var ngCount = table.AsEnumerable()
                .Where(r => r["INSPECTIONRESULT"].ToString().Equals("NG"))
                .ToList().Count;

            return ngCount;
        }

        /// <summary>
        /// focuse된 Row로 이미지 찾는 함수
        /// </summary>
        /// <param name="row"></param>
        private void SearchImageByRow(DataRow row, string resourceType, SmartPictureEdit _commonPic, SmartPictureEdit _commonPic2)
        {
            if (row == null) return;
            string fileResoureceId = "";

            if (resourceType.Equals("DefectCode"))
            {
                fileResoureceId = CurrentDataRow["LOTID"] + row["DEFECTCODE"].ToString()+ row["QCSEGMENTID"].ToString() + "O"+ CurrentDataRow["DEGREE"].ToString();
            }
            else if (resourceType.Equals("InspItemId"))
            {
                fileResoureceId = CurrentDataRow["LOTID"] + row["INSPITEMID"].ToString()+ CurrentDataRow["DEGREE"].ToString();
            }

            Dictionary<string, object> values = new Dictionary<string, object>()
                {
                    {"RESOURCETYPE","ProcessInspection" },
                    {"RESOURCEID",fileResoureceId},
                    {"RESOURCEVERSION", "*"}
                };

            _fileDt = SqlExecuter.Query("GetFileHttpPathFromObjectFileByStandardInfo", "10001", values);

            foreach (DataRow fileRow in _fileDt.Rows)
            {
                string filenameAndExt = fileRow.GetString("FILENAME") + "." + fileRow.GetString("FILEEXT");

                if (_commonPic.Image == null)
                {   //2020-01-28 파일 경로변경
                    _commonPic.Image = CommonFunction.GetFtpImageFileToBitmap(fileRow.GetString("FILEPATH"), filenameAndExt);
                }
                else
                {
                    _commonPic2.Image = CommonFunction.GetFtpImageFileToBitmap(fileRow.GetString("FILEPATH"), filenameAndExt);
                }

            }
        }
        /// <summary>
        /// 검사 결과 NG이면 무조건 입고의뢰 취소
        /// </summary>
        /// <param name="finalResult"></param>
        private void CheckFinalInspResult(string finalResult)
        {
            if (finalResult.Equals("NG") && rdoTakeOver.SelectedIndex != 2)
            {
                rdoTakeOver.SelectedIndexChanged -= RdoTakeOver_SelectedIndexChanged;
                rdoTakeOver.SelectedIndex = 2;
                popArea.EditValue = string.Empty;
                popArea.Enabled = false;
                _toAreaId = "";
                _toResourceId = "";
                rdoTakeOver.SelectedIndexChanged += RdoTakeOver_SelectedIndexChanged;
                throw MessageException.Create("ResultNGCantProcessOSP");
            }
        }
        #endregion


    }
}
