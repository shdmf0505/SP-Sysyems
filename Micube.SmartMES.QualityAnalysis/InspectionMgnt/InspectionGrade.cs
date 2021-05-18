#region using

using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
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
    /// 프 로 그 램 명  : 품질관리 > 검사원 관리 > 검사원 등급 관리
    /// 업  무  설  명  : 검사원에 대한 점수와 해당되는 등급에 대한 관리를 한다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-07-29
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class InspectionGrade : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public InspectionGrade()
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
            grdInspectionGrade.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdInspectionGrade.GridButtonItem = GridButtonItem.Export|GridButtonItem.Add|GridButtonItem.Delete;

            grdInspectionGrade.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetTextAlignment(TextAlignment.Center)
                .SetValidationKeyColumn()
                .SetIsReadOnly()
                .SetIsHidden(); // 회사 ID

            grdInspectionGrade.View.AddTextBoxColumn("PLANTID", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetValidationKeyColumn()
                .SetIsReadOnly(); // Site ID

            grdInspectionGrade.View.AddComboBoxColumn("INSPECTIONCLASSID", 250, new SqlQuery("GetCapacityType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "INSPECTIONCLASSNAME", "INSPECTIONCLASSID")
                .SetLabel("CAPACITYTYPE")
                .SetTextAlignment(TextAlignment.Left)
                .SetValidationKeyColumn(); // 자격구분

            grdInspectionGrade.View.AddSpinEditColumn("LOWERSCORE", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetValidationLessThen("UPPERSCORE")
                .SetValidationCustom(SameScoreValidation); // 점수 하한값

            grdInspectionGrade.View.AddSpinEditColumn("UPPERSCORE", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetValidationGreatThen("LOWERSCORE")
                .SetValidationCustom(SameScoreValidation); // 점수 상한값

            grdInspectionGrade.View.AddComboBoxColumn("GRADE", 100, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=InspectionGrade"))
                .SetTextAlignment(TextAlignment.Center)
                .SetValidationKeyColumn(); // 등급          

            grdInspectionGrade.View.AddTextBoxColumn("INSPECTIONCLASSNAME")
                .SetIsHidden();

            grdInspectionGrade.View.AddTextBoxColumn("DESCRIPTION", 150);

            grdInspectionGrade.View.PopulateColumns();

            grdInspectionGrade.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdInspectionGrade.View.AddingNewRow += View_AddingNewRow;
            grdInspectionGrade.View.RowCellClick += View_RowCellClick;

            grdInspectionGrade.View.CellValueChanged += View_CellValueChanged;

            grdInspectionGrade.View.ShowingEditor += View_ShowingEditor;
        }

        /// <summary>
        /// 자격구분이 입력되기 전까지 점수 하한값과 점수 상한값 ReadOnly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            DataRow row = grdInspectionGrade.View.GetFocusedDataRow();
            string fieldName = grdInspectionGrade.View.FocusedColumn.FieldName;

            if (string.IsNullOrEmpty(row["INSPECTIONCLASSID"].ToString()))
            {
                if (fieldName == "LOWERSCORE" || fieldName == "UPPERSCORE")
                {
                    this.ShowMessage("EnteredFirstInspection");
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 자격구분의 등급에따라 점수가 겹치는것을 방지
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow currentRow = grdInspectionGrade.View.GetDataRow(e.RowHandle);
            //DataTable currentDt = grdInspectionGrade.GetChangedRows();

            if (currentRow == null) return;
            if (currentRow.RowState == DataRowState.Deleted) return;
            
            // Delete 상태인 행은 삭제
            //foreach (DataRow row in currentDt.Rows)
            //{
            //    if (row["_STATE_"].Equals("deleted"))
            //    {
            //        row.Delete();
            //    }
            //}

            // 점수 상하한값일때
            if (e.Column.FieldName == "LOWERSCORE" || e.Column.FieldName == "UPPERSCORE")
            {
                string inspectionId = currentRow["INSPECTIONCLASSID"].ToString(); // 현재 입력중인 자격구분 ID
                string enterpriseId = currentRow["ENTERPRISEID"].ToString(); // 현재 입력중인 회사 ID
                string plantId = currentRow["PLANTID"].ToString(); // 현재 입력중인 Site ID

                DataTable dt = (grdInspectionGrade.DataSource as DataTable).AsEnumerable().Where(r => r["ENTERPRISEID"].Equals(enterpriseId)
                                                                                 && r["PLANTID"].Equals(plantId)
                                                                                 && r["INSPECTIONCLASSID"].Equals(inspectionId))
                                                                          .CopyToDataTable();
                // 현재 입력된 등급의 점수가 데이터베이스에 존재한다면 제외
                if (dt.AsEnumerable().Where(r => r["GRADE"].Equals(currentRow["GRADE"])).Count() > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["GRADE"].Equals(currentRow["GRADE"]))
                        {
                            dt.Rows.Remove(dt.Rows[i]);
                        }
                    }
                }

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("INSPECTIONCLASSID", inspectionId);
                param.Add("ENTERPRISEID", enterpriseId);
                param.Add("PLANTID", plantId);
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                //DataTable dt = SqlExecuter.Query("GetInspectionGradeStatus", "10001", param);

                // 현재 입력된 등급의 점수가 데이터베이스에 존재한다면 제외
                //if (dt.AsEnumerable().Where(r => r["GRADE"].Equals(currentRow["GRADE"])).Count() > 0)
                //{
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        if (dt.Rows[i]["GRADE"].Equals(currentRow["GRADE"]))
                //        {
                //            dt.Rows[i].Delete();
                //        }
                //    }
                //}

                //dt.Merge(currentDt, true, MissingSchemaAction.Ignore);
                //dt = dt.DefaultView.ToTable(true);
                
                // 하한값 Check
                if (e.Column.FieldName == "LOWERSCORE")
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        //if (row["LOWERSCORE"] == DBNull.Value || row["UPPERSCORE"] == DBNull.Value || e.Value == DBNull.Value) continue;
                        if (e.Value == DBNull.Value) continue;

                        int lowerScore = row["LOWERSCORE"] == DBNull.Value ? 0 : Convert.ToInt32(row["LOWERSCORE"]);
                        int upperScore = row["UPPERSCORE"] == DBNull.Value ? 999999999 : Convert.ToInt32(row["UPPERSCORE"]);
                        int inputScore = Convert.ToInt32(e.Value);

                        if (lowerScore <= inputScore && inputScore <= upperScore)
                        {
                            this.ShowMessage("ScoreIsAlready");
                            grdInspectionGrade.View.SetRowCellValue(e.RowHandle, "LOWERSCORE", null);
                            break;                        
                        }
                    }
                }
                // 상한값 Check
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        //if (row["LOWERSCORE"] == DBNull.Value || row["UPPERSCORE"] == DBNull.Value || e.Value == DBNull.Value) continue;
                        if (e.Value == DBNull.Value) continue;

                        int lowerScore = row["LOWERSCORE"] == DBNull.Value ? 0 : Convert.ToInt32(row["LOWERSCORE"]);
                        int upperScore = row["UPPERSCORE"] == DBNull.Value ? 999999999 : Convert.ToInt32(row["UPPERSCORE"]);
                        int inputScore = Convert.ToInt32(e.Value);

                        if (lowerScore <= inputScore && inputScore <= upperScore)
                        {
                            this.ShowMessage("ScoreIsAlready");
                            grdInspectionGrade.View.SetRowCellValue(e.RowHandle, "UPPERSCORE", null);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 자격구분의 등급에따라 점수가 겹치는것을 방지
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    DataRow currentRow = grdInspectionGrade.View.GetDataRow(e.RowHandle);

        //    if (currentRow == null) return;

        //    string inspectionId = currentRow["INSPECTIONCLASSID"].ToString(); // 자격구분 ID
        //    string enterpriseId = currentRow["ENTERPRISEID"].ToString(); // Enterprise ID
        //    string plantId = currentRow["PLANTID"].ToString(); // Plant ID

        //    // 하한값 입력 Check
        //    if (e.Column.FieldName == "LOWERSCORE")
        //    {
        //        if (currentRow["LOWERSCORE"] == DBNull.Value) return;

        //        int standardLower = Convert.ToInt32(currentRow["LOWERSCORE"]); // 사용자가 입력하고 있는 점수 하한값

        //        DataTable dt = (grdInspectionGrade.DataSource as DataTable);

        //        foreach (DataRow row in dt.Rows.Cast<DataRow>().Where(r => r["INSPECTIONCLASSID"].ToString() == inspectionId 
        //                                                                && r["ENTERPRISEID"].ToString() == enterpriseId
        //                                                                && r["PLANTID"].ToString() == plantId))
        //        {
        //            if (row.RowState != DataRowState.Deleted)
        //            {
        //                if (row["LOWERSCORE"] != DBNull.Value && row["UPPERSCORE"] != DBNull.Value)
        //                {                            
        //                    int lower = Convert.ToInt32(row["LOWERSCORE"]);
        //                    int upper = Convert.ToInt32(row["UPPERSCORE"]);

        //                    if (lower <= standardLower && standardLower <= upper)
        //                    {
        //                        // 비교하다가 현재 내 행이면 비교를 건너뜀
        //                        if (Convert.ToInt32(row["LOWERSCORE"]) == standardLower) continue;

        //                        this.ShowMessage("ScoreIsAlready");
        //                        grdInspectionGrade.View.SetRowCellValue(e.RowHandle, "LOWERSCORE", null);
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    // 상한값 입력 Check
        //    else if (e.Column.FieldName == "UPPERSCORE")
        //    {
        //        if (currentRow["UPPERSCORE"] == DBNull.Value) return;

        //        int standardUpper = Convert.ToInt32(currentRow["UPPERSCORE"]); // 사용자가 입력하고 있는 점수 상한값

        //        DataTable dt = (grdInspectionGrade.DataSource as DataTable);

        //        foreach (DataRow row in dt.Rows.Cast<DataRow>().Where(r => r["INSPECTIONCLASSID"].ToString() == inspectionId
        //                                                                && r["ENTERPRISEID"].ToString() == enterpriseId
        //                                                                && r["PLANTID"].ToString() == plantId))
        //        {
        //            if (row.RowState != DataRowState.Deleted)
        //            {
        //                if (row["LOWERSCORE"] != DBNull.Value && row["UPPERSCORE"] != DBNull.Value)
        //                {
        //                    // 비교하다가 현재 내 행이면 비교를 건너뜀
        //                    if (Convert.ToInt32(row["UPPERSCORE"]) == standardUpper) continue;

        //                    int lower = Convert.ToInt32(row["LOWERSCORE"]);
        //                    int upper = Convert.ToInt32(row["UPPERSCORE"]);

        //                    if (lower <= standardUpper && standardUpper <= upper)
        //                    {
        //                        this.ShowMessage("ScoreIsAlready");
        //                        grdInspectionGrade.View.SetRowCellValue(e.RowHandle, "UPPERSCORE", null);
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Row DoubleClick시 검사원 등급 이력팝업 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (grdInspectionGrade.View.GetFocusedDataRow().RowState != DataRowState.Added)
            {
                if (e.Clicks == 2)
                {
                    if (e.Column.FieldName.Equals("INSPECTIONCLASSID") || e.Column.FieldName.Equals("GRADE"))
                    {
                        pnlContent.ShowWaitArea();

                        InspectionGradeHistoryPopup popup = new InspectionGradeHistoryPopup();

                        popup.Owner = this;
                        popup.CurrentDataRow = grdInspectionGrade.View.GetFocusedDataRow();
                        popup.ShowDialog();

                        pnlContent.CloseWaitArea();
                    }
                }
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

            DataTable changed = grdInspectionGrade.GetChangedRows();

            ExecuteRule("SaveInspectionGrade", changed);
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

            DataTable dt = await SqlExecuter.QueryAsync("GetInspectionGrade", "10001", values);

            if (dt.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                this.ShowMessage("NoSelectData");
                grdInspectionGrade.DataSource = null;
                return;
            }

            grdInspectionGrade.DataSource = dt;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            Conditions.AddComboBox("p_capacityType", new SqlQuery("GetCapacityType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "INSPECTIONCLASSNAME", "INSPECTIONCLASSID")
                .SetLabel("CAPACITYTYPE")
                .SetEmptyItem()
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetPosition(1.1);
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

            grdInspectionGrade.View.CheckValidation();

            DataTable changed = grdInspectionGrade.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 점수 하한값과 상한값은 같을수 없다.
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <returns></returns>
        private IEnumerable<ValidationResult> SameScoreValidation(int rowHandle)
        {
            var currentRow = grdInspectionGrade.View.GetDataRow(rowHandle);

            List<ValidationResult> result = new List<ValidationResult>();
            ValidationResult lowerScore = new ValidationResult();
            ValidationResult upperScore = new ValidationResult();

            if (currentRow["LOWERSCORE"] == DBNull.Value || currentRow["UPPERSCORE"] == DBNull.Value)
            {
                return result;
            }

            if (Convert.ToInt32(currentRow["LOWERSCORE"]) == Convert.ToInt32(currentRow["UPPERSCORE"]))
            {
                lowerScore.Caption = "Message";
                lowerScore.FailMessage = Language.GetMessage("NotSameLowScoreUpperScore").Message;
                lowerScore.Id = "LOWERSCORE";
                lowerScore.IsSucced = false;

                upperScore.Caption = "Message";
                upperScore.FailMessage = Language.GetMessage("NotSameLowScoreUpperScore").Message;
                upperScore.Id = "UPPERSCORE";
                upperScore.IsSucced = false;

                result.Add(lowerScore);
                result.Add(upperScore);
            }

            return result;
        }

        #endregion
    }
}
