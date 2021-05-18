
#region using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.Framework.Net;
using Micube.Framework;
using Micube.Framework.SmartControls;
#endregion

namespace Micube.SmartMES.SystemManagement
{
    /// <summary>
    /// 프로그램명 :  시스템관리 > 요청관리 > 요청정보
    /// 업 무 설명 :  요청정보 메시지 Control
    /// 생  성  자 :  한주석
    /// 생  성  일 :  2019-10-25
    /// 수정 이 력 : 
    /// 
    /// 
    /// </summary>
    public partial class ucMessageInfoNoPopup : UserControl
    {

        public void Clear()
        {
            combostate.EditValue = null;
            comboworktype.EditValue = null;
            txtComment.Rtf = "";
            txtTitle.Text = "";
           comborequesttype.EditValue = null;
        }



        /// <summary>
        /// 상태 콤보 박스
        /// </summary>


        public SmartComboBox Statecombobox
        {
            get
            {

                return combostate;
            }
            set { combostate = value; }
        }



        /// <summary>
        /// 요청 콤보 박스
        /// </summary>


        public SmartComboBox RequestTypecombobox
        {
            get
            {

                return comborequesttype;
            }
            set { comborequesttype = value; }
        }

        /// <summary>
        /// 작업타입 콤보 박스
        /// </summary>


        public SmartComboBox WorkTypecombobox
        {
            get
            {

                return comboworktype;
            }
            set { comboworktype = value; }
        }


        /// <summary>
        /// 제목
        /// </summary>
        public string TitleText
		{
			get { return this.txtTitle.Text; }
			set { txtTitle.Text = value; }
		}

		/// <summary>
		/// 메시지
		/// </summary>
		public string CommentText
		{
			get { return this.txtComment.Rtf; }
			set { txtComment.Rtf = value; }
		}

		#region 생성자
		public ucMessageInfoNoPopup()
		{
			InitializeComponent();
            txtTitle.Text = "";
            if (!this.IsDesignMode())
			{
				InitializeEvent();
				
				FontFamily fontFamily = new FontFamily("Arial");
				Font font = new Font(fontFamily, 15, FontStyle.Regular);
				txtComment.Font = font;

            }
		}
		#endregion

		#region 이벤트
		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{

		}

        #endregion

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void smartLabel1_Click(object sender, EventArgs e)
        {

        }

        private void btnSelectItemDelete_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
