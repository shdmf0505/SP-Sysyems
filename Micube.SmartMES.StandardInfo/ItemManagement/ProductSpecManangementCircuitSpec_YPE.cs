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
    public partial class ProductSpecManangementCircuitSpec_YPE : UserControl
    {
        public DataTable DataSource
        {

            get; private set;
        }
        private int _seqIndex = 0;
        public ProductSpecManangementCircuitSpec_YPE()
        {
            InitializeComponent();

			InitializeEvent();
            InitializeGrid();
            InitializeTextControl();
        }

		#region Event

		/// <summary>
		/// 이벤트 초기화
		/// </summary>
		private void InitializeEvent()
		{
			grdCircuitSpec.View.AddingNewRow += View_AddingNewRow;

            tbElongation1.EditValueChanged += TbElongation_EditValueChanged;
            tbElongation2.EditValueChanged += TbElongation_EditValueChanged;
            tbElongation3.EditValueChanged += TbElongation_EditValueChanged;

            tbElongation1_Before.EditValueChanged += TbBeforePitch_EditValueChanged;
            tbElongation2_Before.EditValueChanged += TbBeforePitch_EditValueChanged;
            tbElongation3_Before.EditValueChanged += TbBeforePitch_EditValueChanged;
        }

        //자동계산 
        private void TbElongation_EditValueChanged(object sender, EventArgs e)
        {
            SmartTextBox txtElongation = (sender as SmartTextBox);
            string strElongationName = txtElongation.Name;

            SmartTextBox txtAuto = this.Controls.Find(strElongationName + "_Auto", true).FirstOrDefault() as SmartTextBox;

            string strValue = txtElongation.EditValue.ToString().Trim().Replace("%", "");
            decimal dValue;
            if (Decimal.TryParse(strValue, out dValue) == false)
            {
                if (txtAuto != null)
                    txtAuto.EditValue = null;

                return;
            }

            //% 환산
            if (txtAuto != null)
            {
                txtAuto.EditValue = dValue.ToDouble() * 0.01;

                // 적용전 값이 있을 경우 자동계산 
                SmartTextBox txtBefore = this.Controls.Find(strElongationName + "_Before", true).FirstOrDefault() as SmartTextBox;
                if (txtBefore != null && txtBefore.EditValue != null)
                    TbBeforePitch_EditValueChanged(txtBefore, null);
            }
        }


        private void TbBeforePitch_EditValueChanged(object sender, EventArgs e)
        {
            SmartTextBox txtBefore = (sender as SmartTextBox);
            string strElonValue = string.Empty;
            string strElongationName = txtBefore.Name.Replace("_Before", "");

            SmartTextBox txtElon = this.Controls.Find(strElongationName + "_Auto", true).FirstOrDefault() as SmartTextBox;

            SmartTextBox txtAuto = this.Controls.Find(txtBefore.Name.Replace("Before", "After"), true).FirstOrDefault() as SmartTextBox;

            if (txtElon != null && txtElon.EditValue != null)
                strElonValue = txtElon.EditValue.ToString().Trim().Replace("㎛", "");

            if (string.IsNullOrEmpty(strElonValue))
            {
                if (txtAuto != null)
                    txtAuto.EditValue = null;

                return;
            }

            //자동계산 처리
            string strValue = txtBefore.EditValue.ToString().Trim().Replace("㎛", "");
            decimal dValue, dElonValue;

            if (Decimal.TryParse(strValue, out dValue) == false)
                return;
            if (Decimal.TryParse(strElonValue, out dElonValue) == false)
                return;

            if (txtAuto != null)
                txtAuto.EditValue = dValue + dElonValue;
        }
        /// <summary>
        /// SEQUENCE 채번
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
		{
			args.NewRow["SEQUENCE"] = ++_seqIndex;
		}

		#endregion

		#region 컨텐츠 초기화

		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitializeGrid()
        {
            grdCircuitSpec.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            grdCircuitSpec.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            // 내/외층(사용층)
            grdCircuitSpec.View.AddTextBoxColumn("INNEROUTERLAYER", 200).SetValidationKeyColumn();
            // 폭/간격(원본)
            grdCircuitSpec.View.AddTextBoxColumn("BREADTHINTERVALFROM", 100);
            // 폭/간격(작업본)
            grdCircuitSpec.View.AddTextBoxColumn("BREADTHINTERVALTO", 150);
			// SEQUENCE
			grdCircuitSpec.View.AddTextBoxColumn("SEQUENCE").SetIsHidden();

			grdCircuitSpec.View.PopulateColumns();
        }

		/// <summary>
		/// TEXTBOX 컨트롤 초기화
		/// </summary>
        private void InitializeTextControl()
        {
            TextBoxHelper.SetUnitMask(Unit.percentWithminus, tbElongation1);
            TextBoxHelper.SetUnitMask(Unit.percentWithminus, tbElongation2);
            TextBoxHelper.SetUnitMask(Unit.percentWithminus, tbElongation3);
            TextBoxHelper.SetUnitMask(Unit.um, tbElongation1_Before);
            TextBoxHelper.SetUnitMask(Unit.um, tbElongation2_Before);
            TextBoxHelper.SetUnitMask(Unit.um, tbElongation3_Before);
            TextBoxHelper.SetUnitMask(Unit.um, tbElongation1_After);
            TextBoxHelper.SetUnitMask(Unit.um, tbElongation2_After);
            TextBoxHelper.SetUnitMask(Unit.um, tbElongation3_After);
            TextBoxHelper.SetUnitMask(Unit.um, tbElongation1_Auto);
            TextBoxHelper.SetUnitMask(Unit.um, tbElongation2_Auto);
            TextBoxHelper.SetUnitMask(Unit.um, tbElongation3_Auto);

            TextBoxHelper.SetUnitMask(Unit.umWithplusminus, tbPSR);
        }

        #endregion


        public DataTable Gridcircuitreturn()
        {
            DataSource = new DataTable();
            DataTable dt = grdCircuitSpec.DataSource as DataTable;
            int count = 0;
            
            while (dt.Rows.Count>count && count<7)
            {
                string inner = dt.Columns[0].ToString() + count.ToString();
                string breadfrom = dt.Columns[1].ToString() + count.ToString();
                string breadto = dt.Columns[2].ToString() + count.ToString();

                DataSource.Columns.Add(new DataColumn(inner, typeof(string)));
                DataSource.Columns.Add(new DataColumn(breadfrom, typeof(string))); 
                DataSource.Columns.Add(new DataColumn(breadto, typeof(string)));
                count = count + 1;
            }

            DataRow dr = DataSource.NewRow();
            for(int i=0;i<dt.Rows.Count;i++)
            {
                if(i>=7)
                {
                    break;
                }
                string inner = dt.Columns[0].ToString() + i.ToString();
                string breadfrom = dt.Columns[1].ToString() + i.ToString();
                string breadto = dt.Columns[2].ToString() + i.ToString();
                dr[inner] = dt.Rows[i][dt.Columns[0]];
                dr[breadfrom] = dt.Rows[i][dt.Columns[1]];
                dr[breadto] = dt.Rows[i][dt.Columns[2]];

            }



            DataSource.Rows.Add(dr);

            return DataSource;

        }
        public DataTable PSRreturn()

        {

            DataSource = new DataTable();
            DataSource.Columns.Add(new DataColumn("ELONGATION1", typeof(string))); // 연신율1
            DataSource.Columns.Add(new DataColumn("PITCHBEFORE1", typeof(string))); // 적용전 PITCH1
            DataSource.Columns.Add(new DataColumn("PITCHAFTER1", typeof(string))); // 적용후 PITCH1
            DataSource.Columns.Add(new DataColumn("ELONGATION2", typeof(string))); // 연신율2
            DataSource.Columns.Add(new DataColumn("PITCHBEFORE2", typeof(string))); // 적용전 PITCH2
            DataSource.Columns.Add(new DataColumn("PITCHAFTER2", typeof(string))); // 적용후 PITCH2
            DataSource.Columns.Add(new DataColumn("ELONGATION3", typeof(string))); // 연신율3
            DataSource.Columns.Add(new DataColumn("PITCHBEFORE3", typeof(string))); // 적용전 PITCH3
            DataSource.Columns.Add(new DataColumn("PITCHAFTER3", typeof(string))); // 적용후 PITCH3
            DataSource.Columns.Add(new DataColumn("PSRTOLERANCE", typeof(string))); // PSR공차
            DataSource.Columns.Add(new DataColumn("PSRX", typeof(string))); // PSR X좌표
            DataSource.Columns.Add(new DataColumn("PSRY", typeof(string))); // PSR Y좌표

            DataRow row = DataSource.NewRow();

            row["ELONGATION1"] = tbElongation1.EditValue;
            row["PITCHBEFORE1"] = tbElongation1_Before.EditValue;
            row["PITCHAFTER1"] = tbElongation1_After.EditValue;
            row["ELONGATION2"] = tbElongation2.EditValue;
            row["PITCHBEFORE2"] = tbElongation2_Before.EditValue;
            row["PITCHAFTER2"] = tbElongation2_After.EditValue;
            row["ELONGATION3"] = tbElongation3.EditValue;
            row["PITCHBEFORE3"] = tbElongation3_Before.EditValue;
            row["PITCHAFTER3"] = tbElongation3_After.EditValue;
            row["PSRTOLERANCE"] = tbPSR.EditValue;
            row["PSRX"] = txtPsrXaxis.EditValue;
            row["PSRY"] = txtPsrYaxis.EditValue;

            DataSource.Rows.Add(row);
            return DataSource;

        }
        #region 저장

        /// <summary>
        /// 저장
        /// </summary>
        /// <returns></returns>
        public DataTable Save(out Dictionary<string, object> psrInfo)
        {
			DataTable dt = new DataTable();
            grdCircuitSpec.View.CheckValidation();

            //필수 입력 체크
            List<string> requiredList = new List<string>();
            CommonFunctionProductSpec.GetRequiredValidationList(tlpCircuitSpec, requiredList);
            CommonFunctionProductSpec.RequiredListNullOrEmptyCheck(tlpCircuitSpec, requiredList);

            Dictionary<string, object> dataDictionary = new Dictionary<string, object>();
            CommonFunctionProductSpec.GetSaveDataDictionary(tlpCircuitSpec, dataDictionary);

			//필수 입력
            dt = (grdCircuitSpec.DataSource as DataTable).Clone();
			/*
            for (int i = 0; i < requiredList.Count; i++)
            {
                DataRow row = dt.NewRow();
                string detailName = requiredList[i];

                row["INNEROUTERLAYER"] = detailName;

                var query = from p in dataDictionary.AsEnumerable()
                            where p.Key.Contains(detailName)
                            select p;

                List<KeyValuePair<string, object>> pl = query.ToList();
                for (int k = 0; k < pl.Count; k++)
                {
                    if (pl[k].Key.StartsWith("FROM"))
                    {
                        row["BREADTHINTERVALFROM"] = pl[k].Value;
                    }

                    if (pl[k].Key.StartsWith("TO"))
                    {
                        row["BREADTHINTERVALTO"] = pl[k].Value;
                    }
                }//for

                if (!detailName.Equals("PITCHBEFORE"))
                {
                    dt.Rows.Add(row);
                }
            }
			*/

			dt.Rows.Add("ELONGATION1", tbElongation1.EditValue, tbElongation1_Before.EditValue, 1);
			dt.Rows.Add("ELONGATION2", tbElongation2.EditValue, tbElongation2_Before.EditValue, 2);
			dt.Rows.Add("ELONGATION3", tbElongation3.EditValue, tbElongation3_Before.EditValue, 3);

			//공차
			psrInfo = new Dictionary<string, object>();
			psrInfo.Add("PSRTOLERANCE", tbPSR.EditValue);
			psrInfo.Add("PSRXAXIS", txtPsrXaxis.EditValue);
			psrInfo.Add("PSRYAXIS", txtPsrYaxis.EditValue);
			

			//회로사양 Grid
			DataTable dtDelete = grdCircuitSpec.GetChangesDeleted();
			DataTable dtCircuit = grdCircuitSpec.DataSource as DataTable;			
			
			for(int i = 0; i < dtCircuit.Rows.Count; i++)
			{
				bool isDeleted = false;
				for(int k = 0; k < dtDelete.Rows.Count; k++)
				{
					if(Format.GetString(dtDelete.Rows[k]["INNEROUTERLAYER"]) == Format.GetString(dtCircuit.Rows[i]["INNEROUTERLAYER"])
					&& Format.GetString(dtDelete.Rows[k]["SEQUENCE"]) == Format.GetString(dtCircuit.Rows[i]["SEQUENCE"]))
					{
						isDeleted = true;
						break;
					}
				}
				if(!isDeleted)
				{
					dt.ImportRow(dtCircuit.Rows[i]);
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
		public void DataBind(DataTable dt, DataTable specDt)
		{
			if (dt.Rows.Count <= 0) return;

			_seqIndex = dt.Rows.Count;

			List<DataRow> rowView = new List<DataRow>();
			DataTable dtBind = new DataTable();
			DataRow row = dtBind.NewRow();
			foreach(DataRow r in dt.Rows)
			{
				if(Format.GetString(r["INNEROUTERLAYER"]).Contains("ELONGATION"))
				{
					rowView.Add(r);

					string tag = Format.GetString(r["INNEROUTERLAYER"]);
					if (!string.IsNullOrEmpty(tag))
					{
						dtBind.Columns.Add("FROM" + tag, typeof(string));
						dtBind.Columns.Add("TO" + tag, typeof(string));

						row["FROM" + tag] = r["BREADTHINTERVALFROM"];
						row["TO" + tag] = r["BREADTHINTERVALTO"];
					}				
				}//if
			}//foreach
			dtBind.Rows.Add(row);
			CommonFunctionProductSpec.SearchDataBind(dtBind.Rows[0], tlpCircuitSpec);

			for (int i = 0; i < rowView.Count; i++)
			{
				dt.Rows.Remove(rowView[i]);
			}

			//공차
			tbPSR.EditValue = specDt.Rows[0]["PSRTOLERANCE"];
			txtPsrXaxis.EditValue = specDt.Rows[0]["PSRXAXIS"];
			txtPsrYaxis.EditValue = specDt.Rows[0]["PSRYAXIS"];

			grdCircuitSpec.DataSource = dt;
		}

        /// <summary>
        /// 데이터 초기화
        /// </summary>
        public void ClearData()
        {
            //데이터 초기화
            CommonFunctionProductSpec.ClearData(tlpCircuitSpec);
			grdCircuitSpec.View.ClearDatas();
            //grdCircuitSpec.DataSource = null;
        }

        #endregion

    }
}
