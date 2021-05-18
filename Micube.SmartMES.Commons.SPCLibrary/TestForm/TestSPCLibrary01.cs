using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Micube.SmartMES.Commons.SPCLibrary
{
    public partial class TestSPCLibrary01 : Form
    {
        public TestSPCLibrary01()
        {
            InitializeComponent();
        }
        private void TestSPCLibrary01_Load(object sender, EventArgs e)
        {
            DefaultSpecDataSetting();
        }
        private void DefaultSpecDataSetting()
        {
            txtUOL.Text = "100.1";
            txtUSL.Text = "80.2";
            txtUCL.Text = "60.3";

            txtLCL.Text = "40.4";
            txtLSL.Text = "20.5";
            txtLOL.Text = "10.6";

        }
        private void btnExcute_Click(object sender, EventArgs e)
        {
            this.txtDisplay.Text = "";
            //입력 Parameter 입력
            SpcLibraryHelper spcHelper = new SpcLibraryHelper();
            SpcRules spcRules = new SpcRules();
            spcRules.xbarr.uol = txtUOL.Text.ToSafeDoubleNaN();
            spcRules.xbarr.usl = txtUSL.Text.ToSafeDoubleNaN();
            spcRules.xbarr.ucl = txtUCL.Text.ToSafeDoubleNaN();
            spcRules.xbarr.lcl = txtLCL.Text.ToSafeDoubleNaN();
            spcRules.xbarr.lsl = txtLSL.Text.ToSafeDoubleNaN();
            spcRules.xbarr.lol = txtLOL.Text.ToSafeDoubleNaN();
            spcRules.nValue = txtValue.Text.ToSafeDoubleNaN();
            spcRules.defaultChartType = SpcChartType.xbarr;

            //Spec Check
            SpcRulesOver returnRulesOver = new SpcRulesOver();
            returnRulesOver = spcHelper.SpcSpecRuleCheck(spcRules);

            //결과 표시
            Console.WriteLine(returnRulesOver.message.value);
            if (returnRulesOver.isResult == true)
            {
                this.txtDisplay.Text = returnRulesOver.message.value;
            }
            else
            {
                if (txtValue.Text.IsDouble())
                {
                    this.txtDisplay.Text = "정상 - SPEC Check";
                }
                else
                {
                    this.txtDisplay.Text = "측정값이 없습니다.";
                }
            }


        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DefaultSpecDataSetting();

        }
    }
}
