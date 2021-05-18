using DevExpress.XtraEditors;
using Micube.SmartMES.Commons.SPCLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Micube.SmartMES.SPC
{
    public partial class TestSpcBaseChart04CpkFrame : Form
    {
        public TestSpcBaseChart04CpkFrame()
        {
            InitializeComponent();
            this.ucCpkFrame01.splMain.PanelVisibility = SplitPanelVisibility.Both;
        }

        private void TestSpcBaseChart04CpkFrame_Resize(object sender, EventArgs e)
        {
            this.ucCpkFrame01.splMain.SplitterPosition = 170;
            this.ucCpkFrame01.splMainSub.SplitterPosition = this.Height - (this.Height / 5);
        }

        private void TestSpcBaseChart04CpkFrame_Load(object sender, EventArgs e)
        {
            SpcFunction.isPublicTestMode = 2;
            AnalysisExecutionParameter para = new AnalysisExecutionParameter();
            this.ucCpkFrame01.TestAnalysisExecution(para);
        }
    }//end class
}//end namespace
