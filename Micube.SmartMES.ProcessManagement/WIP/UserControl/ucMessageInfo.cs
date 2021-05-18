
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
using Micube.SmartMES.ProcessManagement;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
	/// <summary>
	/// 프로그램명 :  공정관리 > 공정작업 > LOT 메시지 관리
	/// 업 무 설명 :  메시지 정보 UserControl
	/// 생  성  자 :  정승원
	/// 생  성  일 :  2019-09-04
	/// 수정 이 력 : 
	/// 
	/// 
	/// </summary>
	public partial class ucMessageInfo : UserControl
	{
		/// <summary>
		/// 팝업으로 보여주기 체크
		/// </summary>
		public bool CheckedShowType
		{
			get { return chkPopupView.Checked; }
			set { chkPopupView.Checked = value; }
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
		public ucMessageInfo()
		{
			InitializeComponent();

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
	}
}
