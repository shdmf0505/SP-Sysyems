using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.SmartMES.OutsideOrderMgnt.Popup;
using Micube.Framework.Net;

namespace Micube.SmartMES.OutsideOrderMgnt
{
    public partial class ucItemPopup : UserControl
    {
        /// <summary>
        /// 프 로 그 램 명  : 제품코드 사용자 컨트롤 
        /// 업  무  설  명  : 제품코드 사용자 컨트롤 
        /// 생    성    자  : choisstar
        /// 생    성    일  : 2019-06-13
        /// 수  정  이  력  : 
        /// 
        /// 
        /// </summary>
        #region public Variables
        public string strTempPlantid
        {
            get
            {
                return _Plantid;
            }
            set
            {
                _Plantid = value;
            }
        }
        public bool blReadOnly
        {
            get
            {
                return _ReadOnly;
            }
            set
            {
                _ReadOnly = value;
            }
        }
        #endregion
        #region Local Variables
        private string _Plantid = "";
        private bool _ReadOnly =false;
        #endregion

        public ucItemPopup()
        {
            InitializeComponent();

           
            btnSearch.Click += BtnSearch_Click;
            CODE.KeyDown += TxtItem_KeyDown;
            CODE.EditValueChanged += TxtItem_EditValueChanged;
            NAME.KeyDown += TxtItem_KeyDown;
            CODE.Leave += TxtItem_Leave;
            
           

        }

        /// <summary>
        /// 기준 투입 컨트롤 값 변경 후 기준로스 계산 결과가 0 보다 큰지 체크한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtItem_Leave(object sender, EventArgs e)
        {
            if (CODE.ReadOnly == true) return;
            if (!(CODE.Text.Trim().ToString().Equals("")) && !(CODE.Text.Trim().ToString().Equals(CODE.Tag.ToString()))) 
            {

                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("PRODUCTDEFID", CODE.Text.Trim().ToString());
                Param.Add("PLANTID", _Plantid);
                Param = Commons.CommonFunction.ConvertParameter(Param);
                DataTable dt = SqlExecuter.Query("GetProductdefidPoplistByOsp", "10001", Param);

                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    CODE.Text = dr["PRODUCTDEFID"].ToString();
                    CODE.Tag = CODE.Text;
                    VERSION.Text = dr["PRODUCTDEFVERSION"].ToString();
                    NAME.Text = dr["PRODUCTDEFNAME"].ToString();
                    txtProdcutCode.Text = dr["PRODUCTDEFCODE"].ToString();
                    return;
                }
                else if (dt.Rows.Count > 1)
                {
                    PopupShowDatable(dt);
                }
                else
                {
                    PopupShow();
                }

            }
        }


       

        /// <summary>
        /// 제품코드 값 체크 ""시 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtItem_EditValueChanged(object sender, EventArgs e)
        {
            if (CODE.ReadOnly == true) return;
            if (CODE.Text.Trim().ToString().Equals(""))
            {
                NAME.Text = "";
                VERSION.Text = "";
                txtProdcutCode.Text = "";
                CODE.Tag = CODE.Text;
            }
        }

        /// <summary>
        /// 제품코드 enter 시  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (CODE.ReadOnly == true) return;
            if (e.KeyCode == Keys.Enter)
            {
                if (CODE.Text.Trim().ToString().Equals("")) return;

                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("PRODUCTDEFID", CODE.Text.Trim().ToString());
                Param.Add("PLANTID", _Plantid);
                Param = Commons.CommonFunction.ConvertParameter(Param);
                DataTable dt = SqlExecuter.Query("GetProductdefidPoplistByOsp", "10001", Param);

                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    CODE.Text = dr["PRODUCTDEFID"].ToString();
                    CODE.Tag = CODE.Text;
                    VERSION.Text = dr["PRODUCTDEFVERSION"].ToString();
                    NAME.Text = dr["PRODUCTDEFNAME"].ToString();
                    txtProdcutCode.Text = dr["PRODUCTDEFCODE"].ToString();
                    return;
                }
                else if (dt.Rows.Count > 1)
                {
                    PopupShowDatable(dt);
                }
                else
                {
                    PopupShow();
                }
               
            }
        }

        /// <summary>
        /// 버튼이벤트 처리 (조회)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (CODE.ReadOnly == true) return;
            PopupShow();
        }


        private void ItemPopup_write_handler(DataRow row)
        {
            if (row == null) return;
            CODE.Text = row["PRODUCTDEFID"].ToString();
            VERSION.Text = row["PRODUCTDEFVERSION"].ToString();
            NAME.Text = row["PRODUCTDEFNAME"].ToString();
            txtProdcutCode.Text = row["PRODUCTDEFCODE"].ToString();
            CODE.Tag = CODE.Text;
        }


        /// <summary>
        /// 제품코드 팝업창 조회 결과값 이동 처리 
        /// </summary>
        /// <param name="dtResult"></param>
        private void PopupShowDatable(DataTable dtResult)
        {
            string sItemCode = CODE.Text;
            string sItemVer = VERSION.Text;
            string sItemName = NAME.Text;

            productdefidPopup itemPopup = new productdefidPopup(sItemCode, _Plantid, dtResult);
            itemPopup.write_handler += ItemPopup_write_handler;
            if (itemPopup.CurrentDataRow != null)
            {
                if (itemPopup.CurrentDataRow.Table.Rows.Count == 1)
                {
                    itemPopup.Close();
                    CODE.Text = itemPopup.CurrentDataRow["PRODUCTDEFID"].ToString();
                    VERSION.Text = itemPopup.CurrentDataRow["PRODUCTDEFVERSION"].ToString();
                    NAME.Text = itemPopup.CurrentDataRow["PRODUCTDEFNAME"].ToString();
                    txtProdcutCode.Text = itemPopup.CurrentDataRow["PRODUCTDEFCODE"].ToString();
                    CODE.Tag = CODE.Text;
                }
                else
                {
                    itemPopup.ShowDialog(this);
                }
            }
            else
            {
                itemPopup.ShowDialog(this);
            }
        }

        /// <summary>
        /// 제품 코드 조회창 호출 
        /// </summary>
        private void PopupShow()
        {
            string sItemCode = CODE.Text;
            string sItemVer = VERSION.Text;
            string sItemName = NAME.Text;

            productdefidPopup itemPopup = new productdefidPopup(sItemCode, sItemVer, sItemName ,_Plantid);
            itemPopup.write_handler += ItemPopup_write_handler;
            if (itemPopup.CurrentDataRow != null)
            {
                if (itemPopup.CurrentDataRow.Table.Rows.Count == 1)
                {
                    itemPopup.Close();
                    CODE.Text = itemPopup.CurrentDataRow["PRODUCTDEFID"].ToString();
                    VERSION.Text = itemPopup.CurrentDataRow["PRODUCTDEFVERSION"].ToString();
                    NAME.Text = itemPopup.CurrentDataRow["PRODUCTDEFNAME"].ToString();
                    txtProdcutCode.Text = itemPopup.CurrentDataRow["PRODUCTDEFCODE"].ToString();
                    CODE.Tag = CODE.Text;
                }
                else
                {
                    itemPopup.ShowDialog(this);
                }
            }
            else
            {
                itemPopup.ShowDialog(this);
            }
        }

        
    }
}
