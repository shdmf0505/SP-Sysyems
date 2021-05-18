using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Micube.SmartMES.Commons.SPCLibrary.DataSets.SpcDataSet;

namespace Micube.SmartMES.SPC
{
    public partial class TestSpcBaseChart0301Raw : Form
    {
        public ParPIDataTable RawData = new ParPIDataTable();
        public TestSpcBaseChart0301Raw()
        {
            InitializeComponent();
        }

        private void TestSpcBaseChart0301Raw_Load(object sender, EventArgs e)
        {
            ///this.grdViewRowData.DataSource = RawData;
        }

        private void TestSpcBaseChart0301Raw_Resize(object sender, EventArgs e)
        {
            FormResize();
        }

        /// <summary>
        /// Form Resize
        /// </summary>
        private void FormResize()
        {
            try
            {
                int mainPanel1position = 390;
                int rate = 2;
                //this.ucChartDetail1.splMain.SplitterPosition = position;
                this.ucChartDetail1.splMain.SplitterPosition = this.Height - (this.Height / rate);
                this.ucChartDetail1.splMainPanel1.SplitterPosition = mainPanel1position;
                //this.ucChartDetail1.splMainPanel1.SplitterPosition = this.Height - (this.Height / rate);
            }
            catch (Exception)
            {
                //throw;
            }
        }


    }//end class
}//end namespace
