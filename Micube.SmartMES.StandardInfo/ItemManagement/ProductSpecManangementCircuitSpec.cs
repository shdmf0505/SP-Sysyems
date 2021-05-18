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
    public partial class ProductSpecManangementCircuitSpec : UserControl
    {
		#region 생성자
		public ProductSpecManangementCircuitSpec()
        {
            InitializeComponent();

			if (!this.IsDesignMode())
			{
				InitializeTextControl();
			}
        }

		#endregion

		#region 컨트롤 초기화

		/// <summary>
		/// TextBox 컨트롤 초기화
		/// </summary>
		private void InitializeTextControl()
		{
			TextBoxHelper.SetUnitMask(Unit.percent, tbElongation1);
			TextBoxHelper.SetUnitMask(Unit.percent, tbElongation2);
			TextBoxHelper.SetUnitMask(Unit.percent, tbElongation3);
			TextBoxHelper.SetUnitMask(Unit.um, tbBeforePitch1);
			TextBoxHelper.SetUnitMask(Unit.um, tbBeforePitch2);
			TextBoxHelper.SetUnitMask(Unit.um, tbBeforePitch3);
			TextBoxHelper.SetUnitMask(Unit.um, tbAfterPitch1);
			TextBoxHelper.SetUnitMask(Unit.um, tbAfterPitch2);
			TextBoxHelper.SetUnitMask(Unit.um, tbAfterPitch3);
			TextBoxHelper.SetUnitMask(Unit.um, tbAutoElongation1);
			TextBoxHelper.SetUnitMask(Unit.um, tbAutoElongation2);
			TextBoxHelper.SetUnitMask(Unit.um, tbAutoElongation3);

			TextBoxHelper.SetMarkMask(Mark.LEFT, tbConnectorLeft);
			TextBoxHelper.SetMarkMask(Mark.RIGHT, tbConnectorRight);
		}

		#endregion

		#region event

		#endregion

		#region 저장

		/// <summary>
		/// 저장 
		/// </summary>
		public DataTable Save()
        {
			//필수 입력 체크
			List<string> requiredList = new List<string>();
			CommonFunctionProductSpec.GetRequiredValidationList(tlpCircuitSpec, requiredList);
			CommonFunctionProductSpec.RequiredListNullOrEmptyCheck(tlpCircuitSpec, requiredList, smartGroupBox1.LanguageKey);

			Dictionary<string, object> dataDictionary = new Dictionary<string, object>();
			CommonFunctionProductSpec.GetSaveDataDictionary(tlpCircuitSpec, dataDictionary);

			DataTable dt = new DataTable();
			
			dt.Columns.Add("DETAILNAME", typeof(string));
			dt.Columns.Add("SPECDETAILFROM", typeof(string));
			dt.Columns.Add("SPECDETAILTO", typeof(string));
			dt.Columns.Add("SEQUENCE", typeof(string));


			int count = 1;
			for(int i = 0; i < requiredList.Count; i++)
			{
				DataRow row = dt.NewRow();
				string detailName = requiredList[i];

				row["DETAILNAME"] = detailName;
				row["SEQUENCE"] = count;

				var query = from p in dataDictionary.AsEnumerable()
							where p.Key.Contains(detailName)
							select p;

				List<KeyValuePair<string, object>> pl = query.ToList();
				for(int k = 0; k < pl.Count; k++)
				{
					if(pl[k].Key.StartsWith("FROM"))
					{
						row["SPECDETAILFROM"] = pl[k].Value;
					}

					if (pl[k].Key.StartsWith("TO"))
					{
						row["SPECDETAILTO"] = pl[k].Value;
					}
				}

				if(!detailName.Equals("PITCHBEFORE"))
				{
					dt.Rows.Add(row);
					count++;
				}
				
			}

			return dt;

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

			DataTable dtBind = new DataTable();
			DataRow row = dtBind.NewRow();
			foreach (DataRow r in dt.Rows)
			{
				string tag = Format.GetString(r["DETAILNAME"]);

				if(!string.IsNullOrEmpty(tag))
				{
					dtBind.Columns.Add("FROM" + tag, typeof(string));
					dtBind.Columns.Add("TO" + tag, typeof(string));

					row["FROM" + tag] = r["SPECDETAILFROM"];
					row["TO" + tag] = r["SPECDETAILTO"];

				}
				
			}
			dtBind.Rows.Add(row);
			CommonFunctionProductSpec.SearchDataBind(dtBind.Rows[0], tlpCircuitSpec);
		}


        /// <summary>
        /// 데이터 초기화
        /// </summary>
        public void ClearData()
        {
            //데이터 초기화
            CommonFunctionProductSpec.ClearData(tlpCircuitSpec);
        }

        #endregion


    }
}
