#region using

using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Repository;
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
    /// 프 로 그 램 명  : 품질관리 > 검사원 관리 > 검사원 평가문제지 관리
    /// 업  무  설  명  : 검사원에 대한 평가문제지를 파일로 관리한다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-07-30
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class InspectionPaper : SmartConditionManualBaseForm
    {
        #region Local Variables

        private string _enterpriseId = "";
        private string _plantId = "";
        private string _productionYear = "";
        private string _inspectionClassId = "";
        private string _sequence = "";

        #endregion

        #region 생성자

        public InspectionPaper()
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
            InitializeGrid();
        }

        /// <summary>        
        /// 검사등급 관리현황 그리드
        /// </summary>
        private void InitializeGrid()
        {
            grdInspectionPaper.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdInspectionPaper.GridButtonItem = GridButtonItem.Export|GridButtonItem.Add|GridButtonItem.Delete;

            grdInspectionPaper.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetValidationKeyColumn(); // Enterprise ID

            grdInspectionPaper.View.AddTextBoxColumn("PLANTID", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetValidationKeyColumn(); // Site ID

            grdInspectionPaper.View.AddTextBoxColumn("PRODUCTYEAR", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("yyyy")
                .SetValidationKeyColumn(); // 제작년도

            grdInspectionPaper.View.AddTextBoxColumn("VENDORNAME", 200)
                .SetTextAlignment(TextAlignment.Left); // 협력사

            grdInspectionPaper.View.AddComboBoxColumn("INSPECTIONCLASSID", 250, new SqlQuery("GetCapacityType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "INSPECTIONCLASSNAME", "INSPECTIONCLASSID")
                .SetLabel("CAPACITYTYPE")
                .SetTextAlignment(TextAlignment.Left)
                .SetValidationKeyColumn(); // 자격구분

            grdInspectionPaper.View.AddTextBoxColumn("DESCRIPTION", 250)
                .SetTextAlignment(TextAlignment.Left); // 설명

            grdInspectionPaper.View.AddTextBoxColumn("SEQUENCE")
                .SetIsHidden(); // Sequence

            grdInspectionPaper.View.PopulateColumns();

            RepositoryItemDateEdit edit = new RepositoryItemDateEdit();
            edit.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;
            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            edit.Mask.EditMask = "yyyy";
            //edit.Mask.UseMaskAsDisplayFormat = true;

            grdInspectionPaper.View.Columns["PRODUCTYEAR"].ColumnEdit = edit;

            fileInspectionPaper.LanguageKey = "INSPECTIONFILEEXAMPLE";

            grdInspectionPaper.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdInspectionPaper.View.AddingNewRow += View_AddingNewRow;

            grdInspectionPaper.View.FocusedRowChanged += View_FocusedRowChanged;
            grdInspectionPaper.View.RowClick += View_RowClick;

            grdInspectionPaper.View.CellValueChanged += View_CellValueChanged;
        }

        /// <summary>
        /// 자격연도값을 yyyy형식으로 지정한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = grdInspectionPaper.View.GetFocusedDataRow();

            if (e.Column.FieldName == "PRODUCTYEAR")
            {
                row["PRODUCTYEAR"] = e.Value.ToString().Substring(0, 4);
            }
        }

        /// <summary>
        /// 제작년도, 자격구분별로 첨부된 파일정보를 검색한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (grdInspectionPaper.View.GetFocusedDataRow() == null) return;

            if (grdInspectionPaper.View.GetFocusedDataRow().RowState == DataRowState.Unchanged)
            {
                pnlContent.ShowWaitArea();
                SearchInspectionFile();
                pnlContent.CloseWaitArea();

                fileInspectionPaper.Resource.Id = _enterpriseId + _plantId + _productionYear + _inspectionClassId + _sequence;
                fileInspectionPaper.Resource.Type = "EvaluationPaper";
                fileInspectionPaper.Resource.Version = "1";
                fileInspectionPaper.UploadPath = "InspectionMgnt/InspectionPaper";
            }
            else
            {
                fileInspectionPaper.ClearData();
            }
        }

        /// <summary>
        /// 제작년도, 자격구분별로 첨부된 파일정보를 검색한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdInspectionPaper.View.GetFocusedDataRow() == null) return;

            if (grdInspectionPaper.View.GetFocusedDataRow().RowState == DataRowState.Unchanged)
            {
                pnlContent.ShowWaitArea();
                SearchInspectionFile();
                pnlContent.CloseWaitArea();

                fileInspectionPaper.Resource.Id = _enterpriseId + _plantId + _productionYear + _inspectionClassId + _sequence;
                fileInspectionPaper.Resource.Type = "EvaluationPaper";
                fileInspectionPaper.Resource.Version = "1";
                fileInspectionPaper.UploadPath = "InspectionMgnt/InspectionPaper";
            }
            else
            {
                fileInspectionPaper.ClearData();
            }
        }

        /// <summary>
        /// Row 추가시 Enterprise와 Plant를 세팅한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = UserInfo.Current.Plant;
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable changed = grdInspectionPaper.GetChangedRows();
            DataTable fileChaned = fileInspectionPaper.GetChangedRows();

            fileChaned.TableName = "fileList";

            // 저장할 파일데이터가 있다면 서버에 업로드한다.
            if (fileChaned.Rows.Count != 0)
            {
                int chkAdded = 0;

                foreach (DataRow row in fileChaned.Rows)
                {
                    if (row["_STATE_"].ToString() == "added")
                    {
                        chkAdded++;
                    }

                    row["RESOURCEID"] = _enterpriseId + _plantId + _productionYear + _inspectionClassId + _sequence;
                    row["RESOURCETYPE"] = "EvaluationPaper";
                    row["RESOURCEVERSION"] = "1";
                    row["FILEPATH"] = "InspectionMgnt/InspectionPaper";
                }

                if (chkAdded > 0)
                {
                    fileInspectionPaper.SaveChangedFiles();
                }
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(changed.Copy());
            ds.Tables.Add(fileChaned.Copy());

            this.ExecuteRule("SaveInspectionPaper", ds);
            fileInspectionPaper.BeginInvoke(new MethodInvoker(() => { SearchInspectionFile(); }));
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            values.Add("p_languageType", UserInfo.Current.LanguageType);
            //values["P_PRODUCTYEAR"] = values["P_PRODUCTYEAR"].ToString().Substring(0, 4);

            DataTable dt = await SqlExecuter.QueryAsync("GetInspectionPaper", "10001", values);

            if (dt.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                this.ShowMessage("NoSelectData");
                grdInspectionPaper.DataSource = null;
                fileInspectionPaper.ClearData();
                return;
            }
            
            grdInspectionPaper.DataSource = dt;
            fileInspectionPaper.BeginInvoke(new MethodInvoker(() => { SearchInspectionFile(); }));
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //Conditions.AddDateEdit("p_productYear")
            //    .SetLabel("PRODUCTYEAR")
            //    .SetPosition(1.1);

            Conditions.AddComboBox("p_capacityType", new SqlQuery("GetCapacityType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "INSPECTIONCLASSNAME", "INSPECTIONCLASSID")
                .SetLabel("CAPACITYTYPE")
                .SetEmptyItem()
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetPosition(1.2);
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // 조회조건에 구성된 Control에 대한 처리가 필요한 경우
            //SmartDateEdit dateEdit = Conditions.GetControl<SmartDateEdit>("p_productYear");
            //dateEdit.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;
            //dateEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            //dateEdit.Properties.Mask.EditMask = "yyyy";
            //dateEdit.EditValue = DateTime.Now;
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdInspectionPaper.View.CheckValidation();

            DataTable changed = grdInspectionPaper.GetChangedRows();
            DataTable fileChaned = fileInspectionPaper.GetChangedRows();

            if (changed.Rows.Count == 0 && fileChaned.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 검사원별로 등록된 파일정보를 조회한다.
        /// </summary>
        private void SearchInspectionFile()
        {
            if ((grdInspectionPaper.DataSource as DataTable).Rows.Count == 0) return;

            _enterpriseId = grdInspectionPaper.View.GetFocusedRowCellValue("ENTERPRISEID").ToString();
            _plantId = grdInspectionPaper.View.GetFocusedRowCellValue("PLANTID").ToString();
            _productionYear = grdInspectionPaper.View.GetFocusedRowCellValue("PRODUCTYEAR").ToString();
            _inspectionClassId = grdInspectionPaper.View.GetFocusedRowCellValue("INSPECTIONCLASSID").ToString();
            _sequence = grdInspectionPaper.View.GetFocusedRowCellValue("SEQUENCE").ToString();

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "RESOURCEID", _enterpriseId + _plantId + _productionYear + _inspectionClassId + _sequence},
                { "RESOURCETYPE", "EvaluationPaper"},
                { "RESOURCEVERSION", "1"}
            };

            DataTable dt = SqlExecuter.Query("GetInspectionPaperFile", "10001", param);

            if (dt.Rows.Count == 0)
            {
                fileInspectionPaper.ClearData();
                return;
            }

            fileInspectionPaper.DataSource = dt;
        }

        #endregion
    }
}
