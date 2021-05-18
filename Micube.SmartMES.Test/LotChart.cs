using DevExpress.XtraCharts;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Micube.SmartMES.Test
{
    public partial class LotChart : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public LotChart()
        {
            InitializeComponent();
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

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            DataTable dataSource = GetData();

            SetChartData(dataSource);
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            DataTable type = new DataTable();
            type.Columns.Add("CODEID", typeof(string));
            type.Columns.Add("CODENAME", typeof(string));

            type.Rows.Add("LINE", "LINE");
            type.Rows.Add("BAR", "BAR");
            type.Rows.Add("POINT", "POINT");

            type.AcceptChanges();


            SqlQueryAdapter sqlQuery = new SqlQueryAdapter(type);
            Conditions.AddComboBox("P_CHARTTYPE", sqlQuery)
                .SetDefault("LINE")
                .SetLabel("차트타입")
                .SetValidationIsRequired();
        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
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

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            chartLot.Validated += ChartLot_Validated;
        }

        /// <summary>
        /// Series Point 선택 시 발생 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChartLot_Validated(object sender, EventArgs e)
        {
            SmartChart chart = sender as SmartChart;

            List<object> listPoint = chart.GetSelectionPoints("");

            if (listPoint.Count > 0)
            {
                try
                {
                    DialogManager.ShowWaitArea(pnlContent);

                    DataTable dataSource = GetData().Clone();
                    chartLot.SelectedItems.Clear();

                    listPoint.ForEach(point =>
                    {
                        SeriesPoint seriesPoint = point as SeriesPoint;

                        DataRow[] rows = GetData().Select("LOTID = '" + seriesPoint.Argument + "'");

                        if (rows.Length > 0)
                        {
                            DataRow newRow = dataSource.NewRow();
                            newRow.ItemArray = rows[0].ItemArray.Clone() as object[];

                            dataSource.Rows.Add(newRow);
                        }
                    });

                    dataSource.AcceptChanges();

                    //LotChartPopup popup = new LotChartPopup(Conditions.GetValue("P_CHARTTYPE").ToString(), dataSource);
                    //popup.ShowDialog();
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                }
                finally
                {
                    //chart.ClearSelectionPoints();

                    DialogManager.CloseWaitArea(pnlContent);
                }
            }
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        private void SetChartData(DataTable dataSource)
        {
            chartLot.ClearSeries();

            string chartType = Conditions.GetValue("P_CHARTTYPE").ToString();

            if (chartType == "LINE")
            {
                chartLot.AddLineSeries("Lot", dataSource)
                    .SetX_DataMember(ScaleType.Qualitative, "LOTID")
                    .SetY_DataMember(ScaleType.Numerical, "DEFECTRATE");
            }
            else if (chartType == "BAR")
            {
                chartLot.AddBarSeries("Lot", dataSource)
                    .SetX_DataMember(ScaleType.Qualitative, "LOTID")
                    .SetY_DataMember(ScaleType.Numerical, "DEFECTQTY");
            }
            else if (chartType == "POINT")
            {
                chartLot.AddPointSeries("Lot", dataSource)
                    .SetX_DataMember(ScaleType.Qualitative, "LOTID")
                    .SetY_DataMember(ScaleType.Numerical, "DEFECTRATE");
            }

            chartLot.SetUseRectangleSelection()
                .SetUseSelectionHighlight();

            chartLot.PopulateSeries();

            //chartLot.SelectionMode = ElementSelectionMode.Extended;
            //chartLot.SeriesSelectionMode = SeriesSelectionMode.Point;
        }

        private DataTable GetData()
        {
            DataTable result = new DataTable();

            result.Columns.Add("LOTID", typeof(string));
            result.Columns.Add("LOTNAME", typeof(string));
            result.Columns.Add("DESCRIPTION", typeof(string));
            result.Columns.Add("LOCATIONID", typeof(string));
            result.Columns.Add("LOCATIONNAME", typeof(string));
            result.Columns.Add("RECIPEDEFID", typeof(string));
            result.Columns.Add("RECIPEDEFVERSION", typeof(string));
            result.Columns.Add("ROOTLOTID", typeof(string));
            result.Columns.Add("DEFECTQTY", typeof(int));
            result.Columns.Add("PRODUCTIONQTY", typeof(int));
            result.Columns.Add("DEFECTRATE", typeof(decimal));

            result.Rows.Add("LOT-V-201905-0001", "LOT-V-201905-0001", "", "IFC", "안산공장", "RECIPE-2019-07-2S", "3", "LOT-V-201905-0001", "17", "1000");
            result.Rows.Add("LOT-V-201905-0002", "LOT-V-201905-0002", "", "IFC", "안산공장", "RECIPE-2019-01-6A", "2", "LOT-V-201905-0002", "11", "1200");
            result.Rows.Add("LOT-V-201905-0003", "LOT-V-201905-0003", "", "IFC", "안산공장", "RECIPE-2018-09-7W", "5", "LOT-V-201905-0003", "36", "800");
            result.Rows.Add("LOT-V-201905-0004", "LOT-V-201905-0004", "", "IFC", "안산공장", "RECIPE-2019-01-5Z", "4", "LOT-V-201905-0004", "19", "500");
            result.Rows.Add("LOT-V-201905-0005", "LOT-V-201905-0005", "", "IFC", "안산공장", "RECIPE-2019-01-6J", "9", "LOT-V-201905-0005", "89", "1500");
            result.Rows.Add("LOT-V-201905-0006", "LOT-V-201905-0006", "", "IFC", "안산공장", "RECIPE-2019-01-77", "1", "LOT-V-201905-0006", "135", "3000");
            result.Rows.Add("LOT-V-201905-0007", "LOT-V-201905-0007", "", "IFC", "안산공장", "RECIPE-2019-02-38", "3", "LOT-V-201905-0007", "77", "1700");
            result.Rows.Add("LOT-V-201905-0008", "LOT-V-201905-0008", "", "IFC", "안산공장", "RECIPE-2019-02-1H", "7", "LOT-V-201905-0008", "63", "600");
            result.Rows.Add("LOT-V-201905-0009", "LOT-V-201905-0009", "", "IFC", "안산공장", "RECIPE-2019-02-4Q", "1", "LOT-V-201905-0009", "137", "2000");
            result.Rows.Add("LOT-V-201905-0010", "LOT-V-201905-0010", "", "IFC", "안산공장", "RECIPE-2019-03-5Y", "5", "LOT-V-201905-0010", "88", "1700");
            result.Rows.Add("LOT-V-201905-0011", "LOT-V-201905-0011", "", "IFC", "안산공장", "RECIPE-2019-03-6X", "2", "LOT-V-201905-0011", "53", "1100");
            result.Rows.Add("LOT-V-201905-0012", "LOT-V-201905-0012", "", "IFC", "안산공장", "RECIPE-2019-03-4G", "6", "LOT-V-201905-0012", "121", "1850");
            result.Rows.Add("LOT-V-201905-0013", "LOT-V-201905-0013", "", "IFC", "안산공장", "RECIPE-2019-04-3K", "1", "LOT-V-201905-0013", "251", "2300");
            result.Rows.Add("LOT-V-201905-0014", "LOT-V-201905-0014", "", "IFC", "안산공장", "RECIPE-2019-04-18", "4", "LOT-V-201905-0014", "19", "850");
            result.Rows.Add("LOT-V-201905-0015", "LOT-V-201905-0015", "", "IFC", "안산공장", "RECIPE-2019-04-22", "2", "LOT-V-201905-0015", "44", "1300");

            foreach (DataRow row in result.Rows)
            {
                row["DEFECTRATE"] = Format.GetDecimal(row["DEFECTQTY"]) / Format.GetDecimal(row["PRODUCTIONQTY"]);
            }

            result.AcceptChanges();


            return result;
        }

        #endregion
    }
}