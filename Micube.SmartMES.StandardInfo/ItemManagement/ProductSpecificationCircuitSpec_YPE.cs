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
using Micube.Framework.Net;

namespace Micube.SmartMES.StandardInfo
{
    public partial class ProductSpecificationCircuitSpec_YPE : UserControl
    {
        public DataTable DataSource
        {

            get; private set;
        }
        private int _seqIndex = 0;
        public ProductSpecificationCircuitSpec_YPE()
        {
            InitializeComponent();
            InitializeTextControl();
            InitializeGrid();
           
            InitializeEvent();
        }

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            tbElongation1.EditValueChanged += TbElongation1_EditValueChanged;
            tbElongation1_Before.EditValueChanged += TbElongation1_Before_EditValueChanged;
            tbElongation2.EditValueChanged += TbElongation2_EditValueChanged;
            tbElongation2_Before.EditValueChanged += TbElongation2_Before_EditValueChanged;
            tbElongation3.EditValueChanged += TbElongation3_EditValueChanged;
            tbElongation3_Before.EditValueChanged += TbElongation3_Before_EditValueChanged;
        }

        private void TbElongation3_Before_EditValueChanged(object sender, EventArgs e)
        {


            if (tbElongation3.EditValue != null && tbElongation3_Before.EditValue != null && !tbElongation3.EditValue.Equals("") &&
                !tbElongation3.EditValue.Equals(" %") && !tbElongation3_Before.EditValue.Equals("") && !tbElongation3_Before.EditValue.Equals(" ㎛"))
            {

                string strValue1 = tbElongation3_Before.EditValue.ToString().Trim().Replace("㎛", "");
                string strValue2 = tbElongation3.EditValue.ToString().Trim().Replace("%", "");
                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                tbAutoElongation3.EditValue = Convert.ToInt32(Convert.ToDouble(strValue2) * Convert.ToDouble(strValue1) * 10);
                tbAfterPitch3.EditValue = Convert.ToDouble(strValue1) + Convert.ToDouble(tbAutoElongation3.EditValue) * 0.001;
                if (double.IsNaN((Convert.ToDouble(tbAfterPitch3.EditValue) / Convert.ToDouble(strValue1) * 100)))
                {
                    tbPitchBefore3.EditValue = "";
                }
                else
                {
                    tbPitchBefore3.EditValue = (Convert.ToDouble(tbAfterPitch3.EditValue) / Convert.ToDouble(strValue1) * 100).ToString("F2");
                }
            }
            else
            {
                tbAutoElongation3.EditValue = "";
                tbAfterPitch3.EditValue = "";
                tbPitchBefore3.EditValue = "";
            }
        }


        private void TbElongation3_EditValueChanged(object sender, EventArgs e)
        {
            if (tbElongation3.EditValue != null && tbElongation3_Before.EditValue != null &&  !tbElongation3.EditValue.Equals("") && 
                !tbElongation3.EditValue.Equals(" %") && !tbElongation3_Before.EditValue.Equals("") && !tbElongation3_Before.EditValue.Equals(" ㎛"))
            {

                string strValue1 = tbElongation3_Before.EditValue.ToString().Trim().Replace(" ㎛", "");
                string strValue2 = tbElongation3.EditValue.ToString().Trim().Replace(" %", "");
                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                tbAutoElongation3.EditValue = Convert.ToInt32(Convert.ToDouble(strValue2) * Convert.ToDouble(strValue1) * 10);
                tbAfterPitch3.EditValue = Convert.ToDouble(strValue1) + Convert.ToDouble(tbAutoElongation3.EditValue) * 0.001;
                if (double.IsNaN((Convert.ToDouble(tbAfterPitch3.EditValue) / Convert.ToDouble(strValue1) * 100)))
                {
                    tbPitchBefore3.EditValue = "";
                }
                else
                {
                    tbPitchBefore3.EditValue = (Convert.ToDouble(tbAfterPitch3.EditValue) / Convert.ToDouble(strValue1) * 100).ToString("F2");
                }
            }
            else
            {
                tbAutoElongation3.EditValue = "";
                tbAfterPitch3.EditValue = "";
                tbPitchBefore3.EditValue = "";
            }

        }

        private void TbElongation2_Before_EditValueChanged(object sender, EventArgs e)
        {

            if (tbElongation2.EditValue != null && tbElongation2_Before.EditValue != null && !tbElongation2.EditValue.Equals("")
                && !tbElongation2.EditValue.Equals(" %") && !tbElongation2_Before.EditValue.Equals("") && !tbElongation2_Before.EditValue.Equals(" ㎛"))
            {

                string strValue1 = tbElongation2_Before.EditValue.ToString().Trim().Replace(" ㎛", "");
                string strValue2 = tbElongation2.EditValue.ToString().Trim().Replace(" %", "");
                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                tbAutoElongation2.EditValue = Convert.ToInt32(Convert.ToDouble(strValue2) * Convert.ToDouble(strValue1) * 10);
                tbAfterPitch2.EditValue = Convert.ToDouble(strValue1) + Convert.ToDouble(tbAutoElongation2.EditValue) * 0.001;
                if (double.IsNaN((Convert.ToDouble(tbAfterPitch2.EditValue) / Convert.ToDouble(strValue1) * 100)))
                {
                    tbPitchBefore2.EditValue = "";
                }
                else
                {
                    tbPitchBefore2.EditValue = (Convert.ToDouble(tbAfterPitch2.EditValue) / Convert.ToDouble(strValue1) * 100).ToString("F2");
                }
            }
            else
            {
                tbAutoElongation2.EditValue = "";
                tbAfterPitch2.EditValue = "";
                tbPitchBefore2.EditValue = "";
            }


        }

        private void TbElongation2_EditValueChanged(object sender, EventArgs e)
        {
            if (tbElongation2.EditValue != null && tbElongation2_Before.EditValue != null && !tbElongation2.EditValue.Equals("")
                    && !tbElongation2.EditValue.Equals(" %") && !tbElongation2_Before.EditValue.Equals("") && !tbElongation2_Before.EditValue.Equals(" ㎛"))
            {

                string strValue1 = tbElongation2_Before.EditValue.ToString().Trim().Replace(" ㎛", "");
                string strValue2 = tbElongation2.EditValue.ToString().Trim().Replace(" %", "");
                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                tbAutoElongation2.EditValue = Convert.ToInt32(Convert.ToDouble(strValue2) * Convert.ToDouble(strValue1) * 10);
                tbAfterPitch2.EditValue = Convert.ToDouble(strValue1) + Convert.ToDouble(tbAutoElongation2.EditValue) * 0.001;
                if (double.IsNaN((Convert.ToDouble(tbAfterPitch2.EditValue) / Convert.ToDouble(strValue1) * 100)))
                {
                    tbPitchBefore2.EditValue = "";
                }
                else
                {
                    tbPitchBefore2.EditValue = (Convert.ToDouble(tbAfterPitch2.EditValue) / Convert.ToDouble(strValue1) * 100).ToString("F2");
                }
            }
            else
            {
                tbAutoElongation2.EditValue = "";
                tbAfterPitch2.EditValue = "";
                tbPitchBefore2.EditValue = "";
            }
                
            
        }

        private void TbElongation1_EditValueChanged(object sender, EventArgs e)
        {
            if (tbElongation1.EditValue != null && tbElongation1_Before.EditValue != null && !tbElongation1.EditValue.Equals("")
                && !tbElongation1.EditValue.Equals(" %") && !tbElongation1_Before.EditValue.Equals("") && !tbElongation1_Before.EditValue.Equals(" ㎛"))
            {
                string strValue1 = tbElongation1_Before.EditValue.ToString().Trim().Replace(" ㎛", "");
                string strValue2 = tbElongation1.EditValue.ToString().Trim().Replace(" %", "");
                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                tbAutoElongation1.EditValue = Convert.ToInt32(Convert.ToDouble(strValue2) * Convert.ToDouble(strValue1) * 10);
                tbAfterPitch1.EditValue = Convert.ToDouble(strValue1) + Convert.ToDouble(tbAutoElongation1.EditValue) * 0.001;
                if (double.IsNaN((Convert.ToDouble(tbAfterPitch1.EditValue) / Convert.ToDouble(strValue1) * 100)))
                {
                    tbPitchBefore1.EditValue = "";
                }
                else
                {
                    tbPitchBefore1.EditValue = (Convert.ToDouble(tbAfterPitch1.EditValue) / Convert.ToDouble(strValue1) * 100).ToString("F2");
                }
            }
            else
            {
                tbAutoElongation1.EditValue = "";
                tbAfterPitch1.EditValue = "";
                tbPitchBefore1.EditValue = "";
            }
        }

        private void TbElongation1_Before_EditValueChanged(object sender, EventArgs e)
        {

            if (tbElongation1.EditValue != null && tbElongation1_Before.EditValue != null && !tbElongation1.EditValue.Equals("") 
                && !tbElongation1.EditValue.Equals(" %") && !tbElongation1_Before.EditValue.Equals("") && !tbElongation1_Before.EditValue.Equals(" ㎛"))
            {
                string strValue1 = tbElongation1_Before.EditValue.ToString().Trim().Replace(" ㎛", "");
                string strValue2 = tbElongation1.EditValue.ToString().Trim().Replace(" %", "");

                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                tbAutoElongation1.EditValue = Convert.ToInt32(Convert.ToDouble(strValue2) * Convert.ToDouble(strValue1) * 10);
                tbAfterPitch1.EditValue = Convert.ToDouble(strValue1) + Convert.ToDouble(tbAutoElongation1.EditValue) * 0.001 ;

                if (double.IsNaN((Convert.ToDouble(tbAfterPitch1.EditValue) / Convert.ToDouble(strValue1) * 100)))
                {
                    tbPitchBefore1.EditValue = "";
                }
                else
                {
                    tbPitchBefore1.EditValue = (Convert.ToDouble(tbAfterPitch1.EditValue) / Convert.ToDouble(strValue1) * 100).ToString("F2");
                }
            }
            else
            {
                tbAutoElongation1.EditValue = "";
                tbAfterPitch1.EditValue = "";
                tbPitchBefore1.EditValue = "";
            }
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
        
        }

        /// <summary>
        /// TEXTBOX 컨트롤 초기화
        /// </summary>

        /// <summary>
        /// TextBox 컨트롤 초기화
        /// </summary>
        private void InitializeTextControl()
        {


            TextBoxHelper.SetUnitMask(Unit.percentWithminus, tbElongation1);
            TextBoxHelper.SetUnitMask(Unit.percentWithminus, tbElongation2);
            TextBoxHelper.SetUnitMask(Unit.percentWithminus, tbElongation3);
            TextBoxHelper.SetUnitMask(Unit.um, tbPSR);
            TextBoxHelper.SetUnitMask(Unit.um, tbElongation1_Before);
            TextBoxHelper.SetUnitMask(Unit.um, tbElongation2_Before);
            TextBoxHelper.SetUnitMask(Unit.um, tbElongation3_Before);
            TextBoxHelper.SetUnitMask(Unit.um, tbAfterPitch1);
            TextBoxHelper.SetUnitMask(Unit.um, tbAfterPitch2);
            TextBoxHelper.SetUnitMask(Unit.um, tbAfterPitch3);
            TextBoxHelper.SetUnitMask(Unit.um, tbAutoElongation1);
            TextBoxHelper.SetUnitMask(Unit.um, tbAutoElongation2);
            TextBoxHelper.SetUnitMask(Unit.um, tbAutoElongation3);
            TextBoxHelper.SetMarkMask(Mark.Scale, tbPitchBefore1);
            TextBoxHelper.SetMarkMask(Mark.Scale, tbPitchBefore2);
            TextBoxHelper.SetMarkMask(Mark.Scale, tbPitchBefore3);



            Dictionary<string, object> Impedence1 = new Dictionary<string, object>();
            Impedence1.Add("CODECLASSID", "YesNo");
            Impedence1.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtImpedence1 = SqlExecuter.Query("GetTypeList", "10001", Impedence1);

            SetSmartComboBox(cbImpidence);
            cbImpidence.DataSource = dtImpedence1.Copy();

            Dictionary<string, object> Impedencetype1 = new Dictionary<string, object>();
            Impedencetype1.Add("CODECLASSID", "ImpedanceType");
            Impedencetype1.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtImpedencetype1 = SqlExecuter.Query("GetTypeList", "10001", Impedencetype1);

            SetSmartComboBox(cbClass1);
            SetSmartComboBox(cbClass2);
            SetSmartComboBox(cbClass3);

            cbClass1.DataSource = dtImpedencetype1.Copy();
            cbClass2.DataSource = dtImpedencetype1.Copy();
            cbClass3.DataSource = dtImpedencetype1.Copy();


            Dictionary<string, object> ImpedenceLayer1 = new Dictionary<string, object>();
            ImpedenceLayer1.Add("CODECLASSID", "ImpedanceLayer");
            ImpedenceLayer1.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtImpedenceLayer1 = SqlExecuter.Query("GetTypeList", "10001", ImpedenceLayer1);

            SetSmartComboBox(cbAppliedLayer1);
            cbAppliedLayer1.DataSource = dtImpedenceLayer1.Copy();
            SetSmartComboBox(cbAppliedLayer2);
            cbAppliedLayer2.DataSource = dtImpedenceLayer1.Copy();
            SetSmartComboBox(cbAppliedLayer3);
            cbAppliedLayer3.DataSource = dtImpedenceLayer1.Copy();
   
            Dictionary<string, object> ImpedenceCheck1 = new Dictionary<string, object>();
            ImpedenceCheck1.Add("CODECLASSID", "YesNo");
            ImpedenceCheck1.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtImpedenceCheck1 = SqlExecuter.Query("GetTypeList", "10001", ImpedenceCheck1);

            SetSmartComboBox(cbimpedence1);
            cbimpedence1.DataSource = dtImpedenceCheck1.Copy();
            SetSmartComboBox(cbimpedence2);
            cbimpedence2.DataSource = dtImpedenceCheck1.Copy();
            SetSmartComboBox(cbimpedence3);
            cbimpedence3.DataSource = dtImpedenceCheck1.Copy();

        }


        /// <summary>
        /// 조회 데이터 바인드
        /// </summary>
        /// <param name="dt"></param>
        public void DataBind(DataTable dt)
        {

            if (dt.Rows.Count <= 0) return;

            DataRow r = dt.Rows[0];

            CommonFunctionProductSpec.SearchDataBind(r, tlpCircuitSpec);
        }


        /// <summary>
        /// Combo 초기화 공통
        /// </summary>
        /// <param name="comboBox"></param>
        private void SetSmartComboBox(SmartComboBox comboBox)
        {
            comboBox.DisplayMember = "CODENAME";
            comboBox.ValueMember = "CODEID";
            comboBox.ShowHeader = false;
            comboBox.UseEmptyItem = true;
            comboBox.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
        }
        #endregion



        public DataTable PSRreturn()

        {

            DataSource = new DataTable();
            ReportTableReturn.GetLabelDataTable(tlpCircuitSpec, DataSource);


            DataRow row = DataSource.NewRow();
            ReportTableReturn.GetDataRow(row, tlpCircuitSpec);
            DataSource.Rows.Add(row);
            return DataSource;

        }
        #region 저장


        /// <summary>
        /// 저장 
        /// </summary>
        public DataTable Save()
        {
            //필수 입력 체크
        
            List<string> requiredList = new List<string>();
            GetRequiredValidationList2(tlpCircuitSpec, requiredList);
            cbimpedence3.EditValue = cbimpedence1.GetDataValue();
            cbimpedence2.EditValue = cbimpedence1.GetDataValue();

            Dictionary<string, object> dataDictionary = new Dictionary<string, object>();
            CommonFunctionProductSpec.GetSaveDataDictionary(tlpCircuitSpec, dataDictionary);

            DataTable dt = new DataTable();

            dt.Columns.Add("DETAILNAME", typeof(string));
            dt.Columns.Add("SPECDETAILFROM", typeof(string));
            dt.Columns.Add("SPECDETAILTO", typeof(string));
            dt.Columns.Add("SEQUENCE", typeof(string));


            int count = 1;
            for (int i = 0; i < requiredList.Count; i++)
            {
                DataRow row = dt.NewRow();
                string detailName = requiredList[i];

                row["DETAILNAME"] = detailName;
                row["SEQUENCE"] = count;

                var query = from p in dataDictionary.AsEnumerable()
                            where p.Key.Contains(detailName)
                            select p;

                List<KeyValuePair<string, object>> pl = query.ToList();
                for (int k = 0; k < pl.Count; k++)
                {

                    if (pl[k].Key.StartsWith("FROM"))
                    {

                        row["SPECDETAILFROM"] = pl[k].Value;
                    }

                    if (pl[k].Key.StartsWith("TO"))
                    {
                        row["SPECDETAILTO"] = pl[k].Value;
                    }
                }


                    dt.Rows.Add(row);
                    count++;
                

            }
            return dt;
        }

            public Dictionary<string, object> Save2()
        {
            //필수 입력 체크
            List<string> requiredList = new List<string>();

            Dictionary<string, object> dataDictionary = new Dictionary<string, object>();
            CommonFunctionProductSpec.GetSaveDataDictionary(tlpCircuitSpec, dataDictionary);

            return dataDictionary;
        }

        public static void GetRequiredValidationList2(Control ctl, List<string> requiredList)
        {
            if (ctl.GetType() == typeof(SmartLabel))
            {
                SmartLabel lbl = ctl as SmartLabel;

                requiredList.Add(lbl.LanguageKey);

            }

            foreach (Control c in ctl.Controls)
            {
                GetRequiredValidationList2(c, requiredList);
            }
        }





        #endregion

        #region 데이터 바인드

        /// <summary>
        /// 조회 데이터 바인드
        /// </summary>
        /// <param name="dt"></param>
        public void DataBind2(DataTable dt)
        {
            if (dt.Rows.Count <= 0) return;
            DataTable dtBind = new DataTable();
            DataRow row = dtBind.NewRow();
            foreach (DataRow r in dt.Rows)
            {
                string tag = Format.GetString(r["DETAILNAME"]);

                if (!string.IsNullOrEmpty(tag))
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
            CommonFunctionProductSpec.ClearData(tlpCircuitSpec);
        }

        #endregion

        private void tlpCircuitSpec_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
