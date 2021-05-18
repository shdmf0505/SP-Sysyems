#region using
using DevExpress.XtraReports.UI;
using DevExpress.XtraTab;
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
#endregion

namespace Micube.SmartMES.StandardInfo.Popup
{
	/// <summary>
	/// 프 로 그 램 명  : 공정관리 > 공정작업 > 입고검사등록
	/// 업  무  설  명  : 사진등록 팝업
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-06-20
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class PrintLabelPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
		public DataRow CurrentDataRow { get; set; }

        private XtraReport view;


        #region 생성자
        public PrintLabelPopup(XtraReport viewList, DataTable labelDataTable)
		{
			InitializeComponent();


            this.view = viewList;

            if (!this.IsDesignMode())
			{
				InitializeEvent();
			}
		}
		#endregion

		#region Event
		private void InitializeEvent()
		{
            this.Load += PrintPackingLabelPopup_Load;
			this.btnPrint.Click += BtnPrint_Click;
			this.btnCancel.Click += BtnCancel_Click;
		}

    
        private void PrintPackingLabelPopup_Load(object sender, EventArgs e)
        {

            XtraTabPage tab = new XtraTabPage();
            tab.Text = this.view.DisplayName;

            ucLabelViewer labelViewer = new ucLabelViewer();
            labelViewer.OnPropertyGridDataChange += LabelViewer_OnPropertyGridDataChange;
            tab.Controls.Add(labelViewer);
            labelViewer.Dock = DockStyle.Fill;
            labelViewer.Tag = this.view.Tag.ToString();
            labelViewer.SetBindingPreview(this.view);


            this.xtraTabControl1.TabPages.Add(tab);

        }

        private void LabelViewer_OnPropertyGridDataChange(ucLabelViewer viewer, string fieldName, string value)
        {

        }

        /// <summary>
        /// Cancel 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Apply 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnPrint_Click(object sender, EventArgs e)
		{

            foreach (XtraTabPage page in this.xtraTabControl1.TabPages)
            {
                ucLabelViewer aa = page.Controls.OfType<ucLabelViewer>().FirstOrDefault();

                XtraReport report = aa.GetLabelReport();

                Commons.CommonFunction.PrintLabel(report);

            }
        }
        #endregion

        #region ◆ Function |



        #endregion

    }
}
