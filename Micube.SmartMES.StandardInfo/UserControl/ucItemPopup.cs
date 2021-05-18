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
    public partial class ucItemPopup : UserControl
    {
        #region Local Variables
        public delegate void room_evecnt_handler(DataRow row);
        public event room_evecnt_handler write_handler;
        #endregion

        #region 생성자
        public ucItemPopup()
        {
            InitializeComponent();
            btnSearch.Click += BtnSearch_Click;
            CODE.KeyDown += TxtItem_KeyDown;
            VERSION.KeyDown += TxtItem_KeyDown;
            NAME.KeyDown += TxtItem_KeyDown;
        }
        #endregion

        #region Event
        // 코드명 포커스 일경우 코드show 코드명 hide
        private void NAME_GotFocus(object sender, EventArgs e)
        {
            NAME.Visible = false; 
            CODE.Visible = true;
            CODE.Focus();
        }
        // 코드 포커스 아닐경우 코드hide코드명 show
        private void CODE_Leave(object sender, EventArgs e)
        {
            CODE.Visible = false;
            NAME.Visible = true;
        }
        //텍스트 박스에서 엔터키 다운일경우 팝업 호출
        private void TxtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PopupShow();
            }
        }
        //검색 버튼 클릭시 팝업 호출
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            PopupShow();
        }

        //팝업이 닫이고 값을 반환하여 컨트롤에 입력
        private void ItemPopup_write_handler(DataRow row)
        {
            CODE.Text = row["ITEMID"].ToString();
            VERSION.Text = row["ITEMVERSION"].ToString();
            NAME.Text = row["ITEMNAME"].ToString();

            //negero.View()
            if (write_handler != null)
            {
                write_handler(row);
            }

        }
        #endregion

        #region 컨텐츠 영역 초기화
        //팝업 호출
        public void PopupShow()
        {
            string sItemCode = CODE.Text; // 코드
            string sItemVer = VERSION.Text; // 버전
            string sItemName = NAME.Text; // 코드명

            ProductItemPopup itemPopup = new ProductItemPopup(sItemCode);
            itemPopup.write_handler += ItemPopup_write_handler;

            // 반환된 데이터가 없을 경우 
            if(itemPopup.CurrentDataRow != null)
            {
                // 한개의 행을 반환시 팝업 자동으로 닫고 값을 반환
                if (itemPopup.CurrentDataRow.Table.Rows.Count == 1)
                {
                    itemPopup.Close();
                    CODE.Text = itemPopup.CurrentDataRow["ITEMID"].ToString();
                    VERSION.Text = itemPopup.CurrentDataRow["ITEMVERSION"].ToString();
                    NAME.Text = itemPopup.CurrentDataRow["ITEMNAME"].ToString();
                }
                else
                {
                    itemPopup.ShowDialog();
                }
            }
            else
            {
                itemPopup.ShowDialog();
            }
        }
        #endregion
    }
}
