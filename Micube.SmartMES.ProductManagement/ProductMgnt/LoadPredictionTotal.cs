#region using

using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
#endregion

namespace Micube.SmartMES.ProductManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 생산관리 > 공정부하 > 공정부하량 종합현황
    /// 업  무  설  명  : 공정부하량 종합현황
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2020-01-06
    /// 수  정  이  력  :
    /// 
    /// 
    /// </summary>
    public partial class LoadPredictionTotal : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |
        private const string LOADTYPE_SIXDAY = "SixDay";
        private const string LOADTYPE_THIRTYDAY = "ThirtyDay";
        private const string LOADTYPE_NINETYDAY = "NinetyDay";

        private const string PRODUCTIONTYPE_PRODUCTION = "Production";
        private const string PRODUCTIONTYPE_SAMPLE = "Sample";

        private string actionDate;
        private string loadType;

        private Font underlined;
        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public LoadPredictionTotal()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Form_Load(object sender, EventArgs e)
        {
            InitializeComboBox();
            InitializeGrid();
        }
        #endregion

        #region ◆ 컨텐츠 영역 초기화 |
        /// <summary>
        /// Control 설정
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();
            InitializeEvent();
        }

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // 부하일
            Conditions.AddDateEdit("P_ACTIONDATE")
                .SetPosition(1.1)
                .SetValidationIsRequired()
                .SetDefault(DateTime.Today)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetLabel("LOADDATE");

            // 부하 타입
            Conditions.AddComboBox("P_LOADTYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=LoadType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetPosition(1.2).SetLabel("LOADTYPE").SetValidationIsRequired();
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        } 
        #endregion

        #region ▶ ComboBox 초기화 |
        /// <summary>
        /// 콤보박스를 초기화 하는 함수
        /// </summary>
        private void InitializeComboBox()
        {
            
        }
        #endregion

        #region ▶ Grid 설정 |
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            InitializeTotalGrid();
            InitializeDetailGrid();
        }

        /// <summary>
        /// 종합 그리드 초기화
        /// </summary>
        private void InitializeTotalGrid()
        {
            #region - 공정 부하 Grid 설정
            grdLoad.View.ClearColumns();
            grdLoad.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdLoad.View.OptionsView.AllowCellMerge = true;
            grdLoad.GridButtonItem = GridButtonItem.Export;
            grdLoad.View.SetIsReadOnly();

            var groupProcess = grdLoad.View.AddGroupColumn("");

            groupProcess.AddTextBoxColumn("LOADTOPSEGMENTCLASSNAME", 150).SetIsReadOnly();
            groupProcess.AddTextBoxColumn("LOADMIDDLESEGMENTCLASSNAME", 150).SetIsReadOnly();
            groupProcess.AddTextBoxColumn("LOADSMALLSEGMENTCLASSNAME", 150).SetIsReadOnly();

            this.grdLoad.View.OptionsCustomization.AllowFilter = false;
            this.grdLoad.View.OptionsCustomization.AllowSort = false;
            this.grdLoad.View.OptionsCustomization.AllowGroup = false;
            this.grdLoad.View.OptionsCustomization.AllowQuickHideColumns = false;
            this.grdLoad.View.OptionsCustomization.AllowBandMoving = false;
            this.grdLoad.View.OptionsCustomization.AllowColumnMoving = false;
            this.grdLoad.View.OptionsCustomization.AllowChangeBandParent = false;
            #endregion
        }

        /// <summary>
        /// 상세 그리드 초기화
        /// </summary>
        private void InitializeDetailGrid()
        {
            #region - 공정 부하 Grid 설정
            grdLoadDetail.View.ClearColumns();
            grdLoadDetail.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdLoadDetail.View.OptionsView.AllowCellMerge = true;
            grdLoadDetail.GridButtonItem = GridButtonItem.Export;
            grdLoadDetail.View.SetIsReadOnly();

            var groupProcess = grdLoadDetail.View.AddGroupColumn("");

            groupProcess.AddTextBoxColumn("LOADTOPSEGMENTCLASSNAME", 100).SetIsReadOnly();
            groupProcess.AddTextBoxColumn("LOADMIDDLESEGMENTCLASSNAME", 100).SetIsReadOnly();
            groupProcess.AddTextBoxColumn("LOADSMALLSEGMENTCLASSNAME", 150).SetIsReadOnly();
            groupProcess.AddTextBoxColumn("PROCESSSEGMENTNAME", 200).SetIsReadOnly();
            groupProcess.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetIsReadOnly();
            groupProcess.AddTextBoxColumn("PRODUCTDEFID", 130).SetIsReadOnly();

            this.grdLoadDetail.View.OptionsCustomization.AllowFilter = false;
            this.grdLoadDetail.View.OptionsCustomization.AllowSort = false;
            this.grdLoadDetail.View.OptionsCustomization.AllowGroup = false;
            this.grdLoadDetail.View.OptionsCustomization.AllowQuickHideColumns = false;
            this.grdLoadDetail.View.OptionsCustomization.AllowBandMoving = false;
            this.grdLoadDetail.View.OptionsCustomization.AllowColumnMoving = false;
            this.grdLoadDetail.View.OptionsCustomization.AllowChangeBandParent = false;
            #endregion
        }

        #endregion

        #endregion

        #region ◆ Event |

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdLoad.View.CellMerge += grdLoadView_CellMerge;
            grdLoad.View.RowStyle += grdLoadView_RowStyle;
            grdLoad.View.RowCellStyle += grdLoadView_RowCellStyle;
            grdLoad.View.RowCellClick += grdLoadView_RowCellClick;

            grdLoadDetail.View.CellMerge += grdLoadDetailView_CellMerge;
            grdLoadDetail.View.RowStyle += grdLoadDetailView_RowStyle;
        }

        /// <summary>
        /// 사용자 Cell Merge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdLoadView_CellMerge(object sender, CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (view == null)
            {
                return;
            }

            if (e.Column.FieldName == "LOADTOPSEGMENTCLASSNAME" || e.Column.FieldName == "LOADMIDDLESEGMENTCLASSNAME")
            {
                string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);

                string top1 = view.GetRowCellValue(e.RowHandle1, "LOADTOPSEGMENTCLASSID").ToString();
                string top2 = view.GetRowCellValue(e.RowHandle2, "LOADTOPSEGMENTCLASSID").ToString();

                e.Merge = (str1 == str2 && top1 == top2);
            }
            else
            {
                e.Merge = false;
            }
            e.Handled = true;
        }

        /// <summary>
        /// 사용자 Cell Merge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdLoadDetailView_CellMerge(object sender, CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (view == null)
            {
                return;
            }

            if (e.Column.FieldName == "LOADTOPSEGMENTCLASSNAME" || e.Column.FieldName == "LOADMIDDLESEGMENTCLASSNAME")
            {
                string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);

                string top1 = view.GetRowCellValue(e.RowHandle1, "LOADTOPSEGMENTCLASSID").ToString();
                string top2 = view.GetRowCellValue(e.RowHandle2, "LOADTOPSEGMENTCLASSID").ToString();

                e.Merge = (str1 == str2 && top1 == top2);
            }
            else if(e.Column.FieldName == "LOADSMALLSEGMENTCLASSNAME")
            {
                string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);

                string mid1 = view.GetRowCellValue(e.RowHandle1, "LOADMIDDLESEGMENTCLASSID").ToString();
                string mid2 = view.GetRowCellValue(e.RowHandle2, "LOADMIDDLESEGMENTCLASSID").ToString();

                e.Merge = (str1 == str2 && mid1 == mid2);
            }
            else if(e.Column.FieldName == "PROCESSSEGMENTNAME")
            {
                string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);

                string small1 = view.GetRowCellValue(e.RowHandle1, "LOADSMALLSEGMENTCLASSID").ToString();
                string small2 = view.GetRowCellValue(e.RowHandle2, "LOADSMALLSEGMENTCLASSID").ToString();

                e.Merge = (str1 == str2 && small1 == small2);
            }
            else
            {
                e.Merge = false;
            }
            e.Handled = true;
        }

        /// <summary>
        /// 합계 셀 색상 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdLoadView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow row = (DataRow)view.GetDataRow(e.RowHandle);

            if (Format.GetString(row["LOADSMALLSEGMENTCLASSID"]) == "TOTAL")
            {
                if(e.Column.FieldName.EndsWith("_TOTAL") || e.Column.FieldName.EndsWith("_ETC"))
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else if(e.Column.FieldName.EndsWith("_PRODUCTION") || e.Column.FieldName.EndsWith("_SAMPLE"))
                {
                    e.Appearance.ForeColor = Color.Blue;
                    if (underlined == null)
                    {
                        underlined = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Underline);
                    }
                    e.Appearance.Font = underlined;
                }
            }
        }

        /// <summary>
        /// 소계, 합계 행 색상 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdLoadView_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow row = (DataRow)view.GetDataRow(e.RowHandle);

            if (Format.GetString(row["LOADSMALLSEGMENTCLASSID"]) == "TOTAL")
            {
                e.Appearance.BackColor = Color.LightBlue;
            }
            else if (Format.GetString(row["LOADSMALLSEGMENTCLASSID"]) == "SUBTOTAL")
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
        }

        /// <summary>
        /// 소계, 합계 행 색상 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdLoadDetailView_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow row = (DataRow)view.GetDataRow(e.RowHandle);

            if (Format.GetString(row["PRODUCTDEFID"]) == "TOTAL")
            {
                e.Appearance.BackColor = Color.LightBlue;
            }
            else if (Format.GetString(row["PRODUCTDEFID"]) == "SUBTOTAL")
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
        }

        /// <summary>
        /// 합계 셀 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdLoadView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow row = (DataRow)view.GetDataRow(e.RowHandle);

            if (Format.GetString(row["LOADSMALLSEGMENTCLASSID"]) == "TOTAL")
            {
                if (e.Column.FieldName.EndsWith("_PRODUCTION") || e.Column.FieldName.EndsWith("_SAMPLE"))
                {
                    tabControl.SelectedTabPage = pagDetail;
                    string productionType = "";
                    if (e.Column.FieldName.EndsWith("_PRODUCTION"))
                    {
                        productionType = "Production";
                    }
                    else if(e.Column.FieldName.EndsWith("_SAMPLE"))
                    {
                        productionType = "Sample";
                    }

                    var values = Conditions.GetValues();

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("P_ACTIONDATE", this.actionDate);
                    param.Add("P_LOADTYPE", this.loadType);
                    param.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
                    param.Add("P_TOPSEGMENTCLASSID", row["LOADTOPSEGMENTCLASSID"]);
                    param.Add("P_PLANTID", values["P_PLANTID"].ToString());
                    param.Add("P_PRODUCTIONTYPE", productionType);

                    int gapDays = 0;
                    switch (this.loadType)
                    {
                        case LOADTYPE_SIXDAY:
                            gapDays = 5;
                            break;
                        case LOADTYPE_THIRTYDAY:
                            gapDays = 29;
                            break;
                        case LOADTYPE_NINETYDAY:
                            gapDays = 89;
                            break;
                    }
                    DateTime fromDate = DateTime.Parse(this.actionDate);
                    DateTime toDate = fromDate.AddDays(gapDays);

                    // Grid Data 초기화
                    InitializeDetailGrid();
                    AddDateColumnsToDetailGrid(fromDate, toDate);

                    grdLoadDetail.View.PopulateColumns();
                    grdLoadDetail.View.FixColumn(new string[] { "LOADTOPSEGMENTCLASSNAME", "LOADMIDDLESEGMENTCLASSNAME", "LOADSMALLSEGMENTCLASSNAME"
                        , "PROCESSSEGMENTNAME", "PRODUCTDEFNAME" });

                    DataTable dtResult = this.Procedure("usp_wip_selectdaysegmentloadtotaldetail", param);
                    if (dtResult.Rows.Count < 1)
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }
                    grdLoadDetail.DataSource = dtResult;
                }
            }
        }
        #endregion

        #region ◆ 툴바 |

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
        }

        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            tabControl.SelectedTabPage = pagMain;

            var values = Conditions.GetValues();
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
            values["P_ACTIONDATE"] = values["P_ACTIONDATE"].ToString().Substring(0, 10);

            this.actionDate = values["P_ACTIONDATE"].ToString();
            this.loadType = values["P_LOADTYPE"].ToString();

            int gapDays = 0;
            switch(values["P_LOADTYPE"].ToString())
            {
                case LOADTYPE_SIXDAY:
                    gapDays = 5;
                    break;
                case LOADTYPE_THIRTYDAY:
                    gapDays = 29;
                    break;
                case LOADTYPE_NINETYDAY:
                    gapDays = 89;
                    break;
            }
            DateTime fromDate = DateTime.Parse(values["P_ACTIONDATE"].ToString());
            DateTime toDate = fromDate.AddDays(gapDays);

            // 상세 Grid Data 초기화
            InitializeDetailGrid();
            grdLoadDetail.View.PopulateColumns();
            grdLoadDetail.DataSource = null;

            // 종합 Grid Data 초기화
            InitializeTotalGrid();
            AddDateColumnsToTotalGrid(fromDate, toDate);
            grdLoad.View.PopulateColumns();
            grdLoad.View.FixColumn(new string[] { "LOADTOPSEGMENTCLASSNAME", "LOADMIDDLESEGMENTCLASSNAME", "LOADSMALLSEGMENTCLASSNAME" });

            DataTable dtResult = this.Procedure("usp_wip_selectdaysegmentloadtotal", values);
            if (dtResult.Rows.Count < 1)
            {   
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            grdLoad.DataSource = dtResult;
        }
        #endregion

        #region ◆ 유효성 검사 |

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #endregion

        #region ◆ Private Function |
        /// <summary>
        /// 종합 그리드에 날짜별 필드 추가
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        private void AddDateColumnsToTotalGrid(DateTime from, DateTime to)
        {
            foreach (DateTime day in EachDay(from, to))
            {
                var groupEachDay = grdLoad.View.AddGroupColumn(day.ToString("yyyy-MM-dd"));

                // 합계
                ConditionItemTextBox total = groupEachDay.AddTextBoxColumn(day.ToString("yyyy-MM-dd") + "_TOTAL", 80)
                    .SetDisplayFormat("#,##0")
                    .SetTextAlignment(TextAlignment.Right);
                total.LanguageKey = "SUM";

                // 양산
                ConditionItemTextBox production = groupEachDay.AddTextBoxColumn(day.ToString("yyyy-MM-dd") + "_PRODUCTION", 80)
                    .SetDisplayFormat("#,##0")
                    .SetTextAlignment(TextAlignment.Right);
                production.LanguageKey = "PRODACTUALAMOUNT";

                // 샘플
                ConditionItemTextBox sample = groupEachDay.AddTextBoxColumn(day.ToString("yyyy-MM-dd") + "_SAMPLE", 80)
                    .SetDisplayFormat("#,##0")
                    .SetTextAlignment(TextAlignment.Right);
                sample.LanguageKey = "SAMACTUALAMOUNT";

                // 기타
                ConditionItemTextBox etc = groupEachDay.AddTextBoxColumn(day.ToString("yyyy-MM-dd") + "_ETC", 80)
                    .SetDisplayFormat("#,##0")
                    .SetTextAlignment(TextAlignment.Right);
                etc.LanguageKey = "ETCSPEC";
            }
        }

        /// <summary>
        /// 상세 그리드에 날짜별 컬럼 추가
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        private void AddDateColumnsToDetailGrid(DateTime from, DateTime to)
        {
            foreach (DateTime day in EachDay(from, to))
            {
                var groupEachDay = grdLoadDetail.View.AddGroupColumn(day.ToString("yyyy-MM-dd"));

                // 합계
                ConditionItemTextBox total = groupEachDay.AddTextBoxColumn(day.ToString("yyyy-MM-dd"), 80)
                    .SetDisplayFormat("#,##0")
                    .SetTextAlignment(TextAlignment.Right);
                total.LanguageKey = "QTY";
            }
        }

        /// <summary>
        /// from 부터 to까지의 일자들의 컬렉션을 반환한다.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private IEnumerable<DateTime> EachDay(DateTime from, DateTime to)
        {
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                yield return day;
            }
        }
        #endregion
    }
}
