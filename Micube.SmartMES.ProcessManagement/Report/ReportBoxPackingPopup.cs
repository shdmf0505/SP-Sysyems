#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.SmartMES.Commons;

using DevExpress.XtraReports.UI;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
#endregion

namespace Micube.SmartMES.ProcessManagement.Report
{
    public partial class ReportBoxPackingPopup : SmartPopupBaseForm
    {
        #region ◆ Local Variables |

        // TODO : 화면에서 사용할 내부 변수 추가
        private XtraReport report;
        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public ReportBoxPackingPopup()
        {
            InitializeComponent();

            this.Load += Form_Load;
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Form_Load(object sender, EventArgs e)
        {
            InitializeControls();
            InitializeEvent();
        }
        #endregion

        #region ◆ 컨텐츠 영역 초기화 |
        private void InitializeControls()
        {
            Assembly assembly = Assembly.GetAssembly(this.GetType());
            report = XtraReport.FromStream(assembly.GetManifestResourceStream("Micube.SmartMES.ProcessManagement.Report.BoxPackingPrint.repx"), true);

            this.documentViewer1.DocumentSource = report;

            report.CreateDocument();
        }

        #endregion

        #region ◆ Event |

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가

        }
        #endregion
    }
}
