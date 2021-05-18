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

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : ex> 시스템관리 > 코드 관리 > 코드그룹 정보
    /// 업  무  설  명  : ex> 시스템에서 공통으로 사용되는 코드그룹 정보를 관리한다.
    /// 생    성    자  : 홍길동
    /// 생    성    일  : 2019-05-14
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LOTAnalysisPivot : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        string _Text;                                       // 설명
        SmartBandedGrid grdList = new SmartBandedGrid();    // Main Grid

        /// <summary>
        /// 선택된 불량코드 DataTable
        /// </summary>
        private DataTable _DefectList;

        /// <summary>
        /// 조회 조건 품목에서 찾은 정보 Row
        /// </summary>
        private DataRow _selectedRow;

        #endregion

        #region 생성자

        public LOTAnalysisPivot()
        {
            InitializeComponent();

            //InitializeContent();
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
            gridConsumable.View.AddTextBoxColumn("CONSUMABLE", 100);
            gridConsumable.View.AddTextBoxColumn("LOTID", 150);
            gridConsumable.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            gridConsumable.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 150);
            gridConsumable.View.AddTextBoxColumn("CONSUMABLELOTID", 150);
            gridConsumable.View.AddTextBoxColumn("SUBCONSUMABLELOTID", 150);
            gridConsumable.View.AddTextBoxColumn("SUBCONSUMABLEWORKDATE", 150);

            gridConsumable.View.PopulateColumns();

            gridDurable.View.AddTextBoxColumn("TOOLCHANGETYPE", 100);
            gridDurable.View.AddTextBoxColumn("LOTID", 150);
            gridDurable.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            gridDurable.View.AddTextBoxColumn("DURABLEDEFID", 150);
            gridDurable.View.AddTextBoxColumn("DURABLEDEFVERSION", 150);
            gridDurable.View.AddTextBoxColumn("WORKDATE", 150);

            gridDurable.View.PopulateColumns();

            //grdList.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;
            //grdList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            //// 코드그룹ID
            //grdList.View.AddTextBoxColumn("CODECLASSID", 150)
            //    .SetValidationIsRequired();
            //// 코드그룹명
            //grdList.View.AddTextBoxColumn("CODECLASSNAME", 200);

            //grdList.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            //grdList.View.AddingNewRow += View_AddingNewRow;
            ContextMenuStrip context = chartResourceTime.contextMenuStrip;
            context.Items.Clear();
        }

        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable changed = grdList.GetChangedRows();

            ExecuteRule("SaveCodeClass", changed);
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

            DataTable dtCodeClass = await ProcedureAsync("usp_com_selectCodeClass", values);

            if (dtCodeClass.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdList.DataSource = dtCodeClass;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            //base.InitializeCondition();
            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            #region 품목 

            var productDefID = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductListByYieldStatus", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFID", "PRODUCTDEFID")
                                         .SetLabel("PRODUCTDEFID")
                                         .SetPopupLayout("GRIDPRODUCTLIST", PopupButtonStyles.Ok_Cancel, true, false)
                                         .SetPopupLayoutForm(600, 400, FormBorderStyle.FixedToolWindow)
                                         .SetPosition(1.1)
                                         .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                                         {
                                             foreach (DataRow row in selectedRow)
                                             {
                                                 SetCondition(row);
                                             }
                                         });

            SmartTextBox tbProductID = Conditions.GetControl<SmartTextBox>("PRODUCTDEFID");

            productDefID.Conditions.AddTextBox("PRODUCTDEFID")
                                   .SetLabel("PRODUCTDEFID");

            //productDefID.Conditions.GetControl<SmartTextBox>("PRODUCTDEFID").EditValue = tbProductID.EditValue;

            productDefID.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120)
                                    .SetLabel("PRODUCTDEFID");

            productDefID.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 250)
                                    .SetLabel("PRODUCTDEFNAME");

            #endregion

            #region 품목명

            Conditions.GetCondition<ConditionItemTextBox>("P_PRODUCTNAME").SetIsReadOnly();

            #endregion

            #region 불량선택
            // DefectSelectPopup
            DefectSelectPopup defectPopup = new DefectSelectPopup();
            var defectCodePopup = Conditions.AddSelectPopup("P_DEFECTCODE", (ISmartCustomPopup)defectPopup, "DEFECTNAME", "DEFECTNAME")
                 .SetPopupLayoutForm(500, 600)
                 .SetLabel("DEFECTCODE")
                 .SetPopupCustomParameter((popup, dataRow) =>
                 {
                     (popup as DefectSelectPopup).SelectedDefectHandlerEvent += (dt) => { _DefectList = dt; };
                 });
            ;

            // 팝업 조회조건
            defectCodePopup.Conditions.AddTextBox("DEFECTCODE");

            // 팝업 그리드
            defectCodePopup.GridColumns.AddTextBoxColumn("DEFECTCODE", 150)
                .SetValidationKeyColumn();
            defectCodePopup.GridColumns.AddTextBoxColumn("DEFECTNAME", 200);


            #endregion

            #region 불량명

            //var defectName = Conditions.AddTextBox("DEFECTNAME").SetPosition(5.2).SetIsReadOnly();

            #endregion
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            if (_parameters.Count > 0)
            {
                if (_parameters["P_PERIOD_PERIODFR"] != null && _parameters["P_PERIOD_PERIODTO"] != null)
                {
                    SmartPeriodEdit condPeriod = Conditions.GetControl<SmartPeriodEdit>("P_PERIOD");
                    condPeriod.datePeriodFr.EditValue = _parameters["P_PERIOD_PERIODFR"];
                    condPeriod.datePeriodTo.EditValue = _parameters["P_PERIOD_PERIODTO"];
                }

                if (_parameters["P_PRODUCTDEFID"] != null)
                    Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(_parameters["P_PRODUCTDEFID"]);

                if (_parameters["P_PRODUCTNAME"] != null)
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = _parameters["P_PRODUCTNAME"];

                if (_parameters["P_SUMMARYTYPE"] != null)
                    Conditions.GetControl<SmartComboBox>("P_SUMMARYTYPE").EditValue = _parameters["P_SUMMARYTYPE"];

                if (_parameters["P_PLANTID"] != null)
                    Conditions.GetControl<SmartComboBox>("P_PLANTID").EditValue = _parameters["P_PLANTID"];


                if (_parameters["P_LOTSELECTION"] != null)
                    Conditions.GetControl<SmartComboBox>("P_LOTSELECTION").EditValue = _parameters["P_LOTSELECTION"];

                if (_parameters["P_DEFECTCODE"] != null)
                    Conditions.GetControl<SmartSelectPopupEdit>("P_DEFECTCODE").SetValue(_parameters["P_DEFECTCODE"]);

                ////Conditions.GetControl<SmartTextBox>("DEFECTNAME").Text = dr["DEFECTNAME"].ToString();
 
                //Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(dr["PRODUCTDEFID"]);

                //Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = dr["PRODUCTDEFNAME"].ToString(); ;
            }
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
            grdList.View.CheckValidation();

            DataTable changed = grdList.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        private void SetCondition(DataRow dr)
        {
            if (!DefectMapHelper.IsNull(dr.GetObject("DEFECTCODE")))
            {
                Conditions.GetControl<SmartSelectPopupEdit>("P_DEFECTCODE").SetValue(dr["DEFECTCODE"]);

                //Conditions.GetControl<SmartTextBox>("DEFECTNAME").Text = dr["DEFECTNAME"].ToString();
            }
            else
            {
                Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(dr["PRODUCTDEFID"]);

                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = dr["PRODUCTDEFNAME"].ToString();
            }
            _selectedRow = dr;
        }

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }

        #endregion

        #region Override 

        public override void LoadForm(Dictionary<string, object> parameters)
        {
            if (parameters == null) return;

            //base.HideCondition();

            base.LoadForm(parameters);

            this.itemSmartLabelTextBox.EditValue = parameters["P_PRODUCTNAME"];

            string txtPeriod = parameters["P_PERIOD_PERIODFR"].ToString().Substring(0, 10) + " ~ " + parameters["P_PERIOD_PERIODTO"].ToString().Substring(0, 10);

            this.periodSmartLabelTextBox.EditValue = txtPeriod;
        }

        #endregion
    }
}
