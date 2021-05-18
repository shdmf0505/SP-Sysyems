using DevExpress.XtraCharts;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

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
    public partial class LotChartPopup : SmartPopupBaseForm
    {
        private string _ChartType;
        private DataTable _DataSource;

        public LotChartPopup(string chartType, DataTable dataSource)
        {
            InitializeComponent();

            _ChartType = chartType;
            _DataSource = dataSource;

            InitializeContent();
        }

        private void InitializeContent()
        {
            InitializeChart();
            InitializeGrid();
        }

        private void InitializeChart()
        {
            if (_ChartType == "LINE")
            {
                chartLot.AddLineSeries("Lot", _DataSource)
                    .SetX_DataMember(ScaleType.Qualitative, "LOTID")
                    .SetY_DataMember(ScaleType.Numerical, "DEFECTRATE")
                    .SetCrosshairPattern(true, "{A} : {V:P2}");
            }
            else if (_ChartType == "BAR")
            {
                chartLot.AddBarSeries("Lot", _DataSource)
                    .SetX_DataMember(ScaleType.Qualitative, "LOTID")
                    .SetY_DataMember(ScaleType.Numerical, "DEFECTQTY")
                    .SetCrosshairPattern(true, "{A} : {V}");
            }

            chartLot.PopulateSeries();
        }

        private void InitializeGrid()
        {
            grdLot.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore;
            grdLot.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdLot.View.SetIsReadOnly();

            grdLot.View.AddTextBoxColumn("LOTID", 150);
            grdLot.View.AddTextBoxColumn("LOTNAME", 200);
            grdLot.View.AddTextBoxColumn("DESCRIPTION", 200);
            grdLot.View.AddTextBoxColumn("LOCATIONID", 150);
            grdLot.View.AddTextBoxColumn("LOCATIONNAME", 200);
            grdLot.View.AddTextBoxColumn("RECIPEDEFID", 150);
            grdLot.View.AddTextBoxColumn("RECIPEDEFVERSION", 70);
            grdLot.View.AddTextBoxColumn("ROOTLOTID", 150);
            grdLot.View.AddSpinEditColumn("DEFECTQTY", 80);
            grdLot.View.AddSpinEditColumn("PRODUCTIONQTY", 80);
            grdLot.View.AddSpinEditColumn("DEFECTRATE", 80)
                .SetDisplayFormat("#,##0.##");

            grdLot.View.PopulateColumns();

            grdLot.DataSource = _DataSource;
        }
    }
}
