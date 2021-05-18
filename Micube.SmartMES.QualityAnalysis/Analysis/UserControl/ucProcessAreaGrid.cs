using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.Framework.SmartControls;

namespace Micube.SmartMES.QualityAnalysis
{
    public partial class ucProcessAreaGrid : UserControl
    {
        public SmartBandedGrid GridSeqArea
        {
            get { return this.grdProcess; }
        }

        public SmartBandedGrid GridProcessEquip
        {
            get { return this.grdAreaEquipment; }
        }

        public ucProcessAreaGrid()
        {
            InitializeComponent();

            InitializeGrid();
        }

        private void InitializeGrid()
        {
            grdProcess.View.AddTextBoxColumn("NO", 100);
            grdProcess.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 100);

            grdProcess.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdProcess.View.PopulateColumns();

            grdAreaEquipment.View.AddTextBoxColumn("AREA", 150);
            grdAreaEquipment.View.AddTextBoxColumn("EQUIPMENT", 150);
            grdAreaEquipment.View.AddCheckBoxColumn("", 80);
            grdAreaEquipment.View.AddTextBoxColumn("LOTCOUNT", 150);
            grdAreaEquipment.View.AddTextBoxColumn("WORKPCSQTY", 150);
            grdAreaEquipment.View.AddTextBoxColumn("WORKPANELQTY", 150);
            grdAreaEquipment.View.AddTextBoxColumn("DEFECTRATE", 150);

            grdAreaEquipment.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdAreaEquipment.View.PopulateColumns();
        }
    }
}
