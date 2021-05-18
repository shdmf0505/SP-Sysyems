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
using Micube.SmartMES.StandardInfo.Popup;

#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
	/// 프 로 그 램 명  : 기준정보 > UserControl
	/// 업  무  설  명  : 품목/버전/품명 으로 조회및 등록용 
	/// 생    성    자  : 윤성원
	/// 생    성    일  : 2019-06-18
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
    public partial class ucPcsImageid : UserControl
    {

        public string NonCategory { get; set; }


        public string sPopupPage = "";

        public delegate void evecnt_handler(DataRow row);
        public event evecnt_handler write_handler;
        public ucPcsImageid()
        {
            InitializeComponent();
            btnSearch.Click += BtnSearch_Click;
            CODE.KeyDown += TxtItem_KeyDown;
            
            NAME.KeyDown += TxtItem_KeyDown;
            NAME.GotFocus += NAME_GotFocus;
            CODE.Leave += CODE_Leave;
            
        }
      
        private void NAME_GotFocus(object sender, EventArgs e)
        {
            NAME.Visible = false;
            CODE.Visible = true;
            CODE.Focus();
        }

        private void CODE_Leave(object sender, EventArgs e)
        {
            CODE.Visible = false;
            NAME.Visible = true;
        }

        private void TxtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PopupShow();
            }
        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            PopupShow();
        }

        public void Customer_write_handler(DataRow row)
        {

            if(row != null)
            {
                CODE.Text = row["IMAGEID"].ToString();
                NAME.Text = row["IMAGEID"].ToString();
            }
            
            //negero.View()
            if(write_handler != null)
            {
                write_handler(row);
            }
        }

        public void PopupShow()
        {
            string sItemCode = CODE.Text;
          
            string sItemName = NAME.Text;
          
        
                GerverImageCustomerPopup GerverImage = new GerverImageCustomerPopup();
                GerverImage.write_handler += Customer_write_handler;
                GerverImage.ShowDialog();
            

            


        }

    }
}
