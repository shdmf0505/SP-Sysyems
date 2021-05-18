using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.Framework;
using Micube.Framework.SmartControls;

namespace Micube.SmartMES.StandardInfo
{
    public partial class ProductSpecManangementEtcInfo_YPE : UserControl
    {
		#region 생성자 
		public ProductSpecManangementEtcInfo_YPE()
        {
            InitializeComponent();
            InitializeTextControl();
        }

		#endregion

		#region 컨텐츠 초기화

		private void InitializeTextControl()
        {

        }

		#endregion

		#region 저장

		/// <summary>
		/// 저장
		/// </summary>
		/// <returns></returns>
		public Dictionary<string, object> Save()
        {
			//필수 입력 체크
			List<string> requiredList = new List<string>();
			CommonFunctionProductSpec.GetRequiredValidationList(tlpEtcInfo, requiredList);

            // 사양변경 내용, 특이사항은 하나만 입력되더라도 저장 진행될수 있도록 Validation 에서 제외후 다시 확인  
            if (requiredList.Contains("SPECCHANGE"))
                requiredList.Remove("SPECCHANGE");

            if (requiredList.Contains("SPECIALNOTE"))
                requiredList.Remove("SPECIALNOTE");


            CommonFunctionProductSpec.RequiredListNullOrEmptyCheck(tlpEtcInfo, requiredList);


            //사양변경 내용, 특이사항은 하나만 입력되더라도 저장 진행될수 있도록 Validation 에서 제외후 다시 확인  
            /******* 임시로 막음 2019/12/27 이상진
            if (string.IsNullOrEmpty(txtSpecChange1.Text)
                && string.IsNullOrEmpty(txtSpecChange2.Text)
                && string.IsNullOrEmpty(txtSpecChange3.Text)
                && string.IsNullOrEmpty(txtSpecChange4.Text)
                && string.IsNullOrEmpty(txtSpecChange5.Text))
                throw MessageException.Create("InValidOspRequiredField", smartGroupBox1.Text + " - " + smartLabel2.Text);

            if (string.IsNullOrEmpty(txtSpecNotice1.Text)
                && string.IsNullOrEmpty(txtSpecNotice2.Text)
                && string.IsNullOrEmpty(txtSpecNotice3.Text)
                && string.IsNullOrEmpty(txtSpecNotice4.Text)
                && string.IsNullOrEmpty(txtSpecNotice5.Text))
                throw MessageException.Create("InValidOspRequiredField", smartGroupBox1.Text + " - " + smartLabel3.Text);
            *******************/

            Dictionary<string, object> dataDictionary = new Dictionary<string, object>();
			CommonFunctionProductSpec.GetSaveDataDictionary(tlpEtcInfo, dataDictionary);

			return dataDictionary;
		}

		#endregion

		#region 데이터 바인드

		/// <summary>
		/// 조회 데이터 바인드
		/// </summary>
		/// <param name="dt"></param>
		public void DataBind(DataTable dt)
		{
			if (dt.Rows.Count <= 0) return;

			DataRow r = dt.Rows[0];

			CommonFunctionProductSpec.SearchDataBind(r, tlpEtcInfo);
		}
        public object comment1()
        {
            return txtSpecNotice1.EditValue;
        }
        public object comment2()
        {
            return txtSpecNotice2.EditValue;
        }
        public object comment3()
        {
            return txtSpecNotice3.EditValue;
        }
        public object comment4()
        {
            return txtSpecNotice4.EditValue;
        }
        public object comment5()
        {
            return txtSpecNotice5.EditValue;
        }

        public object change1()
        {
            return txtSpecChange1.EditValue;
        }
        public object change2()
        {
            return txtSpecChange2.EditValue;
        }
        public object change3()
        {
            return txtSpecChange3.EditValue;
        }
        public object change4()
        {
            return txtSpecChange4.EditValue;
        }
        public object change5()
        {
            return txtSpecChange5.EditValue;
        }

        /// <summary>
        /// 데이터 초기화
        /// </summary>
        public void ClearData()
        {
            //데이터 초기화
            CommonFunctionProductSpec.ClearData(tlpEtcInfo);
        }
        #endregion
    }
}
