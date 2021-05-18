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
using System.Text.RegularExpressions;

namespace Micube.SmartMES.StandardInfo
{
    public partial class ProductSpecificationCircuitSpec2 : UserControl
    {
        DataTable DataSource = new DataTable();
        #region 생성자
        public ProductSpecificationCircuitSpec2()
        {
            InitializeComponent();

            if (!this.IsDesignMode())
            {
                InitializeTextControl();
                InitializeEvent();
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
            TextBoxHelper.SetUnitMask(Unit.um, tbElongation1_Before);
            TextBoxHelper.SetUnitMask(Unit.um, tbElongation2_Before);
            TextBoxHelper.SetUnitMask(Unit.um, tbElongation3_Before);
            TextBoxHelper.SetUnitMask(Unit.um, tbAfterPitch1);
            TextBoxHelper.SetUnitMask(Unit.um, tbAfterPitch2);
            TextBoxHelper.SetUnitMask(Unit.um, tbAfterPitch3);
            TextBoxHelper.SetUnitMask(Unit.um, tbAutoElongation1);
            TextBoxHelper.SetUnitMask(Unit.um, tbAutoElongation2);
            TextBoxHelper.SetUnitMask(Unit.um, tbAutoElongation3);


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
            cbClass1.DataSource = dtImpedencetype1.Copy();

            Dictionary<string, object> Impedencetype2 = new Dictionary<string, object>();
            Impedencetype2.Add("CODECLASSID", "ImpedanceType");
            Impedencetype2.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtImpedencetype2 = SqlExecuter.Query("GetTypeList", "10001", Impedencetype2);

            SetSmartComboBox(cbClass2);
            cbClass2.DataSource = dtImpedencetype2.Copy();

            Dictionary<string, object> Impedencetype3 = new Dictionary<string, object>();
            Impedencetype3.Add("CODECLASSID", "ImpedanceType");
            Impedencetype3.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtImpedencetype3 = SqlExecuter.Query("GetTypeList", "10001", Impedencetype3);

            SetSmartComboBox(cbClass3);
            cbClass3.DataSource = dtImpedencetype3.Copy();



            Dictionary<string, object> ImpedenceLayer1 = new Dictionary<string, object>();
            ImpedenceLayer1.Add("CODECLASSID", "ImpedanceLayer");
            ImpedenceLayer1.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtImpedenceLayer1 = SqlExecuter.Query("GetTypeList", "10001", ImpedenceLayer1);

            SetSmartComboBox(cbAppliedLayer1);
            cbAppliedLayer1.DataSource = dtImpedenceLayer1.Copy();

            Dictionary<string, object> ImpedenceLayer2 = new Dictionary<string, object>();
            ImpedenceLayer2.Add("CODECLASSID", "ImpedanceLayer");
            ImpedenceLayer2.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtImpedenceLayer2 = SqlExecuter.Query("GetTypeList", "10001", ImpedenceLayer2);

            SetSmartComboBox(cbAppliedLayer2);
            cbAppliedLayer2.DataSource = dtImpedenceLayer2.Copy();

            Dictionary<string, object> ImpedenceLayer3 = new Dictionary<string, object>();
            ImpedenceLayer3.Add("CODECLASSID", "ImpedanceLayer");
            ImpedenceLayer3.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtImpedenceLayer3 = SqlExecuter.Query("GetTypeList", "10001", ImpedenceLayer3);

            SetSmartComboBox(cbAppliedLayer3);
            cbAppliedLayer3.DataSource = dtImpedenceLayer3.Copy();

            Dictionary<string, object> ImpedenceCheck1 = new Dictionary<string, object>();
            ImpedenceCheck1.Add("CODECLASSID", "YesNo");
            ImpedenceCheck1.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtImpedenceCheck1 = SqlExecuter.Query("GetTypeList", "10001", ImpedenceCheck1);

            SetSmartComboBox(cbimpedence1);
            cbimpedence1.DataSource = dtImpedenceCheck1.Copy();

            Dictionary<string, object> ImpedenceCheck2 = new Dictionary<string, object>();
            ImpedenceCheck2.Add("CODECLASSID", "YesNo");
            ImpedenceCheck2.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtImpedenceCheck2 = SqlExecuter.Query("GetTypeList", "10001", ImpedenceCheck2);

            SetSmartComboBox(cbimpedence2);
            cbimpedence2.DataSource = dtImpedenceCheck2.Copy();

            Dictionary<string, object> ImpedenceCheck3 = new Dictionary<string, object>();
            ImpedenceCheck3.Add("CODECLASSID", "YesNo");
            ImpedenceCheck3.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtImpedenceCheck3 = SqlExecuter.Query("GetTypeList", "10001", ImpedenceCheck3);

            SetSmartComboBox(cbimpedence3);
            cbimpedence3.DataSource = dtImpedenceCheck3.Copy();


            SetSmartComboBox(cbCLFLAG1);
            SetSmartComboBox(cbCLFLAG2);
            SetSmartComboBox(cbCLFLAG3);
            SetSmartComboBox(cbCLFLAG4);
            SetSmartComboBox(cbCLFLAG5);
            SetSmartComboBox(cbCLFLAG6);
            SetSmartComboBox(cbCLFLAG7);

            cbCLFLAG1.DataSource = dtImpedenceCheck3.Copy();
            cbCLFLAG2.DataSource = dtImpedenceCheck3.Copy();
            cbCLFLAG3.DataSource = dtImpedenceCheck3.Copy();
            cbCLFLAG4.DataSource = dtImpedenceCheck3.Copy();
            cbCLFLAG5.DataSource = dtImpedenceCheck3.Copy();
            cbCLFLAG6.DataSource = dtImpedenceCheck3.Copy();
            cbCLFLAG7.DataSource = dtImpedenceCheck3.Copy();



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

        #region event

        
        
        private void InitializeEvent()
        {
            txtlaser1.EditValueChanged += Txtlaser1_EditValueChanged;
            txtmvhland1.EditValueChanged += Txtlaser1_EditValueChanged;
            txtlaser2.EditValueChanged += Txtlaser2_EditValueChanged;
            txtmvhland2.EditValueChanged += Txtlaser2_EditValueChanged;
            txtlaser3.EditValueChanged += Txtlaser3_EditValueChanged;
            txtmvhland3.EditValueChanged += Txtlaser3_EditValueChanged;
            txtlaser4.EditValueChanged += Txtlaser4_EditValueChanged;
            txtmvhland4.EditValueChanged += Txtlaser4_EditValueChanged;
            txtlaser5.EditValueChanged += Txtlaser5_EditValueChanged;
            txtmvhland5.EditValueChanged += Txtlaser5_EditValueChanged;
            txtlaser6.EditValueChanged += Txtlaser6_EditValueChanged;
            txtmvhland6.EditValueChanged += Txtlaser6_EditValueChanged;
            txtlaser7.EditValueChanged += Txtlaser7_EditValueChanged;
            txtmvhland7.EditValueChanged += Txtlaser7_EditValueChanged;
            txtlaser8.EditValueChanged += Txtlaser8_EditValueChanged;
            txtmvhland8.EditValueChanged += Txtlaser8_EditValueChanged;

            txtmvhdepth1.EditValueChanged += Txtmvhdepth1_EditValueChanged;
            txtlaser1.EditValueChanged += Txtmvhdepth1_EditValueChanged;
            txtmvhdepth2.EditValueChanged += Txtmvhdepth2_EditValueChanged;
            txtlaser2.EditValueChanged += Txtmvhdepth2_EditValueChanged;
            txtmvhdepth3.EditValueChanged += Txtmvhdepth3_EditValueChanged;
            txtlaser3.EditValueChanged += Txtmvhdepth3_EditValueChanged;
            txtmvhdepth4.EditValueChanged += Txtmvhdepth4_EditValueChanged;
            txtlaser4.EditValueChanged += Txtmvhdepth4_EditValueChanged;
            txtmvhdepth5.EditValueChanged += Txtmvhdepth5_EditValueChanged;
            txtlaser5.EditValueChanged += Txtmvhdepth5_EditValueChanged;
            txtmvhdepth6.EditValueChanged += Txtmvhdepth6_EditValueChanged;
            txtlaser6.EditValueChanged += Txtmvhdepth6_EditValueChanged;
            txtmvhdepth7.EditValueChanged += Txtmvhdepth7_EditValueChanged;
            txtlaser7.EditValueChanged += Txtmvhdepth7_EditValueChanged;
            txtmvhdepth8.EditValueChanged += Txtmvhdepth8_EditValueChanged;
            txtlaser8.EditValueChanged += Txtmvhdepth8_EditValueChanged;


            tbElongation1.EditValueChanged += TbElongation1_EditValueChanged;
            tbElongation1_Before.EditValueChanged += TbElongation1_EditValueChanged;
            tbElongation3.EditValueChanged += TbElongation3_EditValueChanged;
            tbElongation3_Before.EditValueChanged += TbElongation3_EditValueChanged;
            tbElongation2.EditValueChanged += TbElongation2_EditValueChanged;
            tbElongation2_Before.EditValueChanged += TbElongation2_EditValueChanged;


        }


        #region Aspect Ratio
        private void Txtmvhdepth1_EditValueChanged(object sender, EventArgs e)
        {
            if (txtmvhdepth1.EditValue != null && txtlaser1.EditValue != null && !txtlaser1.EditValue.Equals("")
             && !txtmvhdepth1.EditValue.Equals(""))
            {
                string strValue1 = txtlaser1.EditValue.ToString().Trim();
                string strValue2 = txtmvhdepth1.EditValue.ToString().Trim();

                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                txtratio1.EditValue = (Convert.ToDouble(strValue2)*100 / (Convert.ToDouble(strValue1) * 1000)).ToString("F1");

                if ((Convert.ToDouble(strValue2) * 100 / (Convert.ToDouble(strValue1) * 1000))>=80)
                {
                    txtratio1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                }
                else
                {
                    txtratio1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                }

            }
            else
            {
                txtratio1.EditValue = "";
                txtratio1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            }
        }

        private void Txtmvhdepth2_EditValueChanged(object sender, EventArgs e)
        {
            if (txtmvhdepth2.EditValue != null && txtlaser2.EditValue != null && !txtlaser2.EditValue.Equals("")
             && !txtmvhdepth2.EditValue.Equals(""))
            {
                string strValue1 = txtlaser2.EditValue.ToString().Trim();
                string strValue2 = txtmvhdepth2.EditValue.ToString().Trim();

                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                txtratio2.EditValue = (Convert.ToDouble(strValue2) * 100 / (Convert.ToDouble(strValue1) * 1000)).ToString("F1");

                if ((Convert.ToDouble(strValue2) * 100 / (Convert.ToDouble(strValue1) * 1000)) >= 80)
                {
                    txtratio2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                }
                else
                {
                    txtratio2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                }

            }
            else
            {
                txtratio2.EditValue = "";
                txtratio2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            }
        }

        private void Txtmvhdepth3_EditValueChanged(object sender, EventArgs e)
        {
            if (txtmvhdepth3.EditValue != null && txtlaser3.EditValue != null && !txtlaser3.EditValue.Equals("")
             && !txtmvhdepth3.EditValue.Equals(""))
            {
                string strValue1 = txtlaser3.EditValue.ToString().Trim();
                string strValue2 = txtmvhdepth3.EditValue.ToString().Trim();

                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                txtratio3.EditValue = (Convert.ToDouble(strValue2) * 100 / (Convert.ToDouble(strValue1) * 1000)).ToString("F1");

                if ((Convert.ToDouble(strValue2) * 100 / (Convert.ToDouble(strValue1) * 1000)) >= 80)
                {
                    txtratio3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                }
                else
                {
                    txtratio3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                }

            }
            else
            {
                txtratio3.EditValue = "";
                txtratio3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            }
        }

        private void Txtmvhdepth4_EditValueChanged(object sender, EventArgs e)
        {
            if (txtmvhdepth4.EditValue != null && txtlaser4.EditValue != null && !txtlaser4.EditValue.Equals("")
             && !txtmvhdepth4.EditValue.Equals(""))
            {
                string strValue1 = txtlaser4.EditValue.ToString().Trim();
                string strValue2 = txtmvhdepth4.EditValue.ToString().Trim();

                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                txtratio4.EditValue = (Convert.ToDouble(strValue2) * 100 / (Convert.ToDouble(strValue1) * 1000)).ToString("F1");

                if ((Convert.ToDouble(strValue2) * 100 / (Convert.ToDouble(strValue1) * 1000)) >= 80)
                {
                    txtratio4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                }
                else
                {
                    txtratio4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                }

            }
            else
            {
                txtratio4.EditValue = "";
                txtratio4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            }
        }

        private void Txtmvhdepth5_EditValueChanged(object sender, EventArgs e)
        {
            if (txtmvhdepth5.EditValue != null && txtlaser5.EditValue != null && !txtlaser5.EditValue.Equals("")
             && !txtmvhdepth5.EditValue.Equals(""))
            {
                string strValue1 = txtlaser5.EditValue.ToString().Trim();
                string strValue2 = txtmvhdepth5.EditValue.ToString().Trim();

                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                txtratio5.EditValue = (Convert.ToDouble(strValue2) * 100 / (Convert.ToDouble(strValue1) * 1000)).ToString("F1");

                if ((Convert.ToDouble(strValue2) * 100 / (Convert.ToDouble(strValue1) * 1000)) >= 80)
                {
                    txtratio5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                }
                else
                {
                    txtratio5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                }

            }
            else
            {
                txtratio5.EditValue = "";
                txtratio5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            }
        }

        private void Txtmvhdepth6_EditValueChanged(object sender, EventArgs e)
        {
            if (txtmvhdepth6.EditValue != null && txtlaser6.EditValue != null && !txtlaser6.EditValue.Equals("")
             && !txtmvhdepth6.EditValue.Equals(""))
            {
                string strValue1 = txtlaser6.EditValue.ToString().Trim();
                string strValue2 = txtmvhdepth6.EditValue.ToString().Trim();

                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                txtratio6.EditValue = (Convert.ToDouble(strValue2) * 100 / (Convert.ToDouble(strValue1) * 1000)).ToString("F1");

                if ((Convert.ToDouble(strValue2) * 100 / (Convert.ToDouble(strValue1) * 1000)) >= 80)
                {
                    txtratio6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                }
                else
                {
                    txtratio6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                }

            }
            else
            {
                txtratio6.EditValue = "";
                txtratio6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            }
        }

        private void Txtmvhdepth7_EditValueChanged(object sender, EventArgs e)
        {
            if (txtmvhdepth7.EditValue != null && txtlaser7.EditValue != null && !txtlaser7.EditValue.Equals("")
             && !txtmvhdepth7.EditValue.Equals(""))
            {
                string strValue1 = txtlaser7.EditValue.ToString().Trim();
                string strValue2 = txtmvhdepth7.EditValue.ToString().Trim();

                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                txtratio7.EditValue = (Convert.ToDouble(strValue2) * 100 / (Convert.ToDouble(strValue1) * 1000)).ToString("F1");

                if ((Convert.ToDouble(strValue2) * 100 / (Convert.ToDouble(strValue1) * 1000)) >= 80)
                {
                    txtratio7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                }
                else
                {
                    txtratio7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                }

            }
            else
            {
                txtratio7.EditValue = "";
                txtratio7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            }
        }

        private void Txtmvhdepth8_EditValueChanged(object sender, EventArgs e)
        {
            if (txtmvhdepth8.EditValue != null && txtlaser8.EditValue != null && !txtlaser8.EditValue.Equals("")
             && !txtmvhdepth8.EditValue.Equals(""))
            {
                string strValue1 = txtlaser8.EditValue.ToString().Trim();
                string strValue2 = txtmvhdepth8.EditValue.ToString().Trim();

                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                txtratio8.EditValue = (Convert.ToDouble(strValue2) * 100 / (Convert.ToDouble(strValue1) * 1000)).ToString("F1");

                if ((Convert.ToDouble(strValue2) * 100 / (Convert.ToDouble(strValue1) * 1000)) >= 80)
                {
                    txtratio8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                }
                else
                {
                    txtratio8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                }

            }
            else
            {
                txtratio8.EditValue = "";
                txtratio8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            }
        }

        #endregion

        #region Annula-ring
        private void Txtlaser1_EditValueChanged(object sender, EventArgs e)
        {
            if (txtmvhland1.EditValue != null && txtlaser1.EditValue != null && !txtlaser1.EditValue.Equals("")
             && !txtmvhland1.EditValue.Equals(""))
            {
                string strValue1 = txtlaser1.EditValue.ToString().Trim();
                string strValue2 = txtmvhland1.EditValue.ToString().Trim();

                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                txtannula1.EditValue = Convert.ToInt64(Convert.ToDouble(strValue2) - Convert.ToDouble(strValue1) / 2);
                
                if (Convert.ToInt64(Convert.ToDouble(strValue2) - Convert.ToDouble(strValue1) / 2)>=0.075)
                {
                    txtannula1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                }
                else
                {
                    txtannula1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                }

            }
            else
            {
                txtannula1.EditValue = "";
                txtannula1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            }

        }

        private void Txtlaser2_EditValueChanged(object sender, EventArgs e)
        {
            if (txtmvhland2.EditValue != null && txtlaser2.EditValue != null && !txtmvhland2.EditValue.Equals("")
             && !txtlaser2.EditValue.Equals(""))
            {
                string strValue1 = txtlaser2.EditValue.ToString().Trim();
                string strValue2 = txtmvhland2.EditValue.ToString().Trim();

                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                txtannula2.EditValue = Convert.ToInt64(Convert.ToDouble(strValue2) - Convert.ToDouble(strValue1) / 2);

                if (Convert.ToInt64(Convert.ToDouble(strValue2) - Convert.ToDouble(strValue1) / 2) >= 0.075)
                {
                    txtannula2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                }
                else
                {
                    txtannula2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                }

            }
            else
            {
                txtannula2.EditValue = "";
                txtannula2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            }

        }

        private void Txtlaser3_EditValueChanged(object sender, EventArgs e)
        {
            if (txtmvhland3.EditValue != null && txtlaser3.EditValue != null && !txtmvhland3.EditValue.Equals("")
             && !txtlaser3.EditValue.Equals(""))
            {
                string strValue1 = txtlaser3.EditValue.ToString().Trim();
                string strValue2 = txtmvhland3.EditValue.ToString().Trim();

                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                txtannula3.EditValue = Convert.ToInt64(Convert.ToDouble(strValue2) - Convert.ToDouble(strValue1) / 2);

                if (Convert.ToInt64(Convert.ToDouble(strValue2) - Convert.ToDouble(strValue1) / 2) >= 0.075)
                {
                    txtannula3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                }
                else
                {
                    txtannula3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                }

            }
            else
            {
                txtannula3.EditValue = "";
                txtannula3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            }

        }

        private void Txtlaser4_EditValueChanged(object sender, EventArgs e)
        {
            if (txtmvhland4.EditValue != null && txtlaser4.EditValue != null && !txtmvhland4.EditValue.Equals("")
             && !txtlaser4.EditValue.Equals(""))
            {
                string strValue1 = txtlaser4.EditValue.ToString().Trim();
                string strValue2 = txtmvhland4.EditValue.ToString().Trim();

                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                txtannula4.EditValue = Convert.ToInt64(Convert.ToDouble(strValue2) - Convert.ToDouble(strValue1) / 2);

                if (Convert.ToInt64(Convert.ToDouble(strValue2) - Convert.ToDouble(strValue1) / 2) >= 0.075)
                {
                    txtannula4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                }
                else
                {
                    txtannula4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                }

            }
            else
            {
                txtannula4.EditValue = "";
                txtannula4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            }

        }

        private void Txtlaser5_EditValueChanged(object sender, EventArgs e)
        {
            if (txtmvhland5.EditValue != null && txtlaser5.EditValue != null && !txtmvhland5.EditValue.Equals("")
             && !txtlaser5.EditValue.Equals(""))
            {
                string strValue1 = txtlaser5.EditValue.ToString().Trim();
                string strValue2 = txtmvhland5.EditValue.ToString().Trim();

                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                txtannula5.EditValue = Convert.ToInt64(Convert.ToDouble(strValue2) - Convert.ToDouble(strValue1) / 2);

                if (Convert.ToInt64(Convert.ToDouble(strValue2) - Convert.ToDouble(strValue1) / 2) >= 0.075)
                {
                    txtannula5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                }
                else
                {
                    txtannula5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                }

            }
            else
            {
                txtannula5.EditValue = "";
                txtannula5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            }
        }

        private void Txtlaser6_EditValueChanged(object sender, EventArgs e)
        {
            if (txtmvhland6.EditValue != null && txtlaser6.EditValue != null && !txtmvhland6.EditValue.Equals("")
             && !txtlaser6.EditValue.Equals(""))
            {
                string strValue1 = txtlaser6.EditValue.ToString().Trim();
                string strValue2 = txtmvhland6.EditValue.ToString().Trim();

                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                txtannula6.EditValue = Convert.ToInt64(Convert.ToDouble(strValue2) - Convert.ToDouble(strValue1) / 2);

                if (Convert.ToInt64(Convert.ToDouble(strValue2) - Convert.ToDouble(strValue1) / 2) >= 0.075)
                {
                    txtannula6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                }
                else
                {
                    txtannula6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                }

            }
            else
            {
                txtannula6.EditValue = "";
                txtannula6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            }
        }

        private void Txtlaser7_EditValueChanged(object sender, EventArgs e)
        {
            if (txtmvhland7.EditValue != null && txtlaser7.EditValue != null && !txtmvhland7.EditValue.Equals("")
             && !txtlaser7.EditValue.Equals(""))
            {
                string strValue1 = txtlaser7.EditValue.ToString().Trim();
                string strValue2 = txtmvhland7.EditValue.ToString().Trim();

                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                txtannula7.EditValue = Convert.ToInt64(Convert.ToDouble(strValue2) - Convert.ToDouble(strValue1) / 2);

                if (Convert.ToInt64(Convert.ToDouble(strValue2) - Convert.ToDouble(strValue1) / 2) >= 0.075)
                {
                    txtannula7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                }
                else
                {
                    txtannula7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                }

            }
            else
            {
                txtannula7.EditValue = "";
                txtannula7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            }
        }

        private void Txtlaser8_EditValueChanged(object sender, EventArgs e)
        {
            if (txtmvhland8.EditValue != null && txtlaser8.EditValue != null && !txtmvhland8.EditValue.Equals("")
             && !txtlaser8.EditValue.Equals(""))
            {
                string strValue1 = txtlaser8.EditValue.ToString().Trim();
                string strValue2 = txtmvhland8.EditValue.ToString().Trim();

                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                txtannula8.EditValue = Convert.ToInt64(Convert.ToDouble(strValue2) - Convert.ToDouble(strValue1) / 2);

                if (Convert.ToInt64(Convert.ToDouble(strValue2) - Convert.ToDouble(strValue1) / 2) >= 0.075)
                {
                    txtannula8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                }
                else
                {
                    txtannula8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                }

            }
            else
            {
                txtannula8.EditValue = "";
                txtannula8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            }
        }


        #endregion


        private void TbElongation3_EditValueChanged(object sender, EventArgs e)
        {
            if (tbElongation3.EditValue != null && tbElongation3_Before.EditValue != null && !tbElongation3.EditValue.Equals("") &&
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
                tbAutoElongation3.EditValue = Convert.ToInt64(Convert.ToDouble(strValue2) * Convert.ToDouble(strValue1) * 10);
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
                tbAutoElongation2.EditValue = Convert.ToInt64(Convert.ToDouble(strValue2) * Convert.ToDouble(strValue1) * 10);
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
                tbAutoElongation1.EditValue = Convert.ToInt64(Convert.ToDouble(strValue2) * Convert.ToDouble(strValue1) * 10);
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

 
    


        #region 저장


        #endregion

        #endregion

        #region 저장

        /// <summary>
        /// 저장 
        /// </summary>
        public DataSet Save()
        {
            //필수 입력 체크
            DataSet ds = new DataSet();
            List<string> requiredList = new List<string>();
            GetRequiredValidationList2(tlpCircuitSpec, requiredList);
         

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

                if (!detailName.Equals("PITCHBEFORE"))
                {
                    dt.Rows.Add(row);
                    count++;
                }

            }

            ds.Tables.Add(dt);
            List<string> mvhlist = new List<string>();
            mvhlist.Add("MVHTYPE"); //MVH TYPE
            mvhlist.Add("LASERSIZE"); // LASER SIZE
            mvhlist.Add("LANDSIZE");// LAND SIZE
            mvhlist.Add("CLFLAG");// CLFLAG
            mvhlist.Add("ANNULARING");// ANNULARING
            mvhlist.Add("CONFORMALSIZE");// CONFORMALSIZE
            mvhlist.Add("FORMINGDEPTH");// FORMINGDEPTH
            mvhlist.Add("ASPECTRATIO");// ASEPECTRATIO
            mvhlist.Add("SEQUENCE");// SEQUENCE

            DataTable dt2 = new DataTable();

            dt2.Columns.Add("MVHTYPE", typeof(string));
            dt2.Columns.Add("LASERSIZE", typeof(string));
            dt2.Columns.Add("LANDSIZE", typeof(string));
            dt2.Columns.Add("CLFLAG", typeof(string));
            dt2.Columns.Add("ANNULARING", typeof(string));
            dt2.Columns.Add("CONFORMALSIZE", typeof(string));
            dt2.Columns.Add("FORMINGDEPTH", typeof(string));
            dt2.Columns.Add("ASPECTRATIO", typeof(string));
            dt2.Columns.Add("SEQUENCE", typeof(string));

            DataRow row1 = dt2.NewRow();
            DataRow row2 = dt2.NewRow();
            DataRow row3 = dt2.NewRow();
            DataRow row4 = dt2.NewRow();
            DataRow row5 = dt2.NewRow();
            DataRow row6 = dt2.NewRow();
            DataRow row7 = dt2.NewRow();
            DataRow row8 = dt2.NewRow();

            row1["SEQUENCE"] = "1";
            row2["SEQUENCE"] = "2";
            row3["SEQUENCE"] = "3";
            row4["SEQUENCE"] = "4";
            row5["SEQUENCE"] = "5";
            row6["SEQUENCE"] = "6";
            row7["SEQUENCE"] = "7";
            row8["SEQUENCE"] = "8";


            for (int i = 0; i < mvhlist.Count; i++)
            {
                var query = from p in dataDictionary.AsEnumerable()
                            where p.Key.Contains(mvhlist[i])
                            select p;

                List<KeyValuePair<string, object>> pl = query.ToList();
                for (int k = 0; k < pl.Count; k++)
                {
                    string no = Regex.Replace(pl[k].Key, @"[^\d+]", "");
                    string column = Regex.Replace(pl[k].Key, @"\d+", "").Trim();

                    switch (no)
                    {
                        case "1":
                            row1[column] = pl[k].Value;
                            break;
                        case "2":
                            row2[column] = pl[k].Value;
                            break;
                        case "3":
                            row3[column] = pl[k].Value;
                            break;
                        case "4":
                            row4[column] = pl[k].Value;
                            break;
                        case "5":
                            row5[column] = pl[k].Value;
                            break;
                        case "6":
                            row6[column] = pl[k].Value;
                            break;
                        case "7":
                            row7[column] = pl[k].Value;
                            break;
                        case "8":
                            row8[column] = pl[k].Value;
                            break;
                    }//switch
                }//for
            }//for


            dt2.Rows.Add(row1);
            dt2.Rows.Add(row2);
            dt2.Rows.Add(row3);
            dt2.Rows.Add(row4);
            dt2.Rows.Add(row5);
            dt2.Rows.Add(row6);
            dt2.Rows.Add(row7);
            dt2.Rows.Add(row8);
            ds.Tables.Add(dt2);
            return ds;

        }




        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, object> Save2()
        {
            //필수 입력 체크
            List<string> requiredList = new List<string>();

            Dictionary<string, object> dataDictionary = new Dictionary<string, object>();
            CommonFunctionProductSpec.GetSaveDataDictionary(tlpCircuitSpec, dataDictionary);

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
            dt = DataPitchAfter(dt);
            dt = DataPitchBefore(dt);
            DataTable dtBind = new DataTable();
            DataRow row = dtBind.NewRow();
            foreach (DataRow r in dt.Rows)
            {
                string tag = Format.GetString(r["DETAILNAME"]);
                if(tag.Equals("MINSIZEINPUT"))
                {
                    continue;
                }
 

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
        /// 조회 데이터 바인드
        /// </summary>
        /// <param name="dt"></param>
        public DataTable DataPitchAfter(DataTable dt)
        {
            double pitchbefore = 0;
            double elongation = 0;



            for (int k = 1; k <= 3; k++)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string tag = Format.GetString(dt.Rows[i]["DETAILNAME"]);
                    if (tag.Equals("ELONGATION-" + k) && !dt.Rows[i]["SPECDETAILFROM"].ToString().Equals(""))
                    {
                        elongation = Convert.ToDouble(dt.Rows[i]["SPECDETAILFROM"]);
                    }
                    if (tag.Equals("PITCHBEFORE" + k) && !dt.Rows[i]["SPECDETAILFROM"].ToString().Equals(""))
                    {
                        pitchbefore = Convert.ToDouble(dt.Rows[i]["SPECDETAILFROM"]);
                    }

                }



                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string tag = Format.GetString(dt.Rows[i]["DETAILNAME"]);
                    if (tag.Equals("PITCHAFTER" + k))
                    {
                        double pitchafter2 = pitchbefore - (elongation * pitchbefore * 0.01);
                        if (dt.Rows[i]["DETAILNAME"].ToString().Equals("PITCHAFTER" + k))
                        {
                            dt.Rows[i]["SPECDETAILFROM"] = pitchafter2;
                        }
                        if (elongation != null && pitchbefore != null)
                     { 
                        if (k == 1)
                        {
                            tbAutoElongation1.EditValue = elongation * pitchbefore * 0.01;
                        }
                        if (k == 2)
                        {
                            tbAutoElongation2.EditValue = elongation * pitchbefore * 0.01;
                        }
                        if (k == 3)
                        {
                            tbAutoElongation3.EditValue = elongation * pitchbefore * 0.01;
                        }
                    }
                    }
                }


                pitchbefore = 0;
                elongation = 0;
            

            }
         return dt;
     }


        /// <summary>
        /// 조회 데이터 바인드
        /// </summary>
        /// <param name="dt"></param>
        public DataTable DataPitchBefore(DataTable dt)
        {
            double pitchbefore = 0;
            double pitchafter = 0;
            for (int k = 1; k <= 3; k++)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string tag = Format.GetString(dt.Rows[i]["DETAILNAME"]);
                    if (tag.Equals("PITCHAFTER" + k) && !dt.Rows[i]["SPECDETAILFROM"].ToString().Equals(""))
                    {
                        pitchafter = Convert.ToDouble(dt.Rows[i]["SPECDETAILFROM"]);
                    }
                    if (tag.Equals("PITCHBEFORE" + k) && !dt.Rows[i]["SPECDETAILFROM"].ToString().Equals(""))
                    {
                        pitchbefore = Convert.ToDouble(dt.Rows[i]["SPECDETAILFROM"]);
                    }
                }

                    if (k == 1)
                    {

                        tbPitchBefore1.EditValue = pitchafter / pitchbefore;
                    }
                    if (k == 2)
                    {
                        tbPitchBefore2.EditValue = pitchafter / pitchbefore;
                    }
                    if (k == 3)
                    {
                        tbPitchBefore3.EditValue = pitchafter / pitchbefore;
                    }
                
                pitchbefore = 0;
                pitchafter = 0;
            }
            return dt;
        }




        /// <summary>
        /// 조회 데이터 바인드
        /// </summary>
        /// <param name="dt"></param>
        public void DataBind2(DataTable dt)
        {

                if (dt.Rows.Count <= 0) return;

                DataRow r = dt.Rows[0];

                CommonFunctionProductSpec.SearchDataBind(r, tlpCircuitSpec);
            

        }

        /// <summary>
        /// 조회 데이터 바인드
        /// </summary>
        /// <param name="dt"></param>
        public void DataBind(DataTable dt, bool isAnother)
        {
            if (dt.Rows.Count <= 0) return;

            foreach (DataRow r in dt.Rows)
            {
                DataTable dtBind = new DataTable();
                DataRow row = dtBind.NewRow();

                int a = int.Parse(r["SEQUENCE"].ToString()) + 1;

                string no = Format.GetString(a);
                if (!string.IsNullOrEmpty(no))
                {
                    dtBind.Columns.Add("MVHTYPE" + no, typeof(string));
                    dtBind.Columns.Add("LASERSIZE" + no, typeof(string));
                    dtBind.Columns.Add("LANDSIZE" + no, typeof(string));
                    dtBind.Columns.Add("CLFLAG" + no, typeof(string));
                    dtBind.Columns.Add("CONFORMALSIZE" + no, typeof(string));
                    dtBind.Columns.Add("FORMINGDEPTH" + no, typeof(string));
                    dtBind.Columns.Add("ANNULARING" + no, typeof(string));
                    dtBind.Columns.Add("ASPECTRATIO" + no, typeof(string));

                    row["MVHTYPE" + no] = r["MVHTYPE"];
                    row["LASERSIZE" + no] = r["LASERSIZE"];
                    row["LANDSIZE" + no] = r["LANDSIZE"];
                    row["CLFLAG" + no] = r["CLFLAG"];
                    row["CONFORMALSIZE" + no] = r["CONFORMALSIZE"];
                    row["FORMINGDEPTH" + no] = r["FORMINGDEPTH"];
                    row["ANNULARING" + no] = r["ANNULARING"];
                    row["ASPECTRATIO" + no] = r["ASPECTRATIO"];
                }
                dtBind.Rows.Add(row);
                CommonFunctionProductSpec.SearchDataBind(row, tlpCircuitSpec);

            }

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


        private void smartLabel9_Click(object sender, EventArgs e)
        {

        }

        private void smartLabel19_Click(object sender, EventArgs e)
        {

        }

        private void tbInnerCircuitBreadth_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cbClass3_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void smartTextBox11_EditValueChanged(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 조회 데이터 바인드
        /// </summary>
        /// <param name="dt"></param>
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



        public DataTable PSRreturn()

        {

            DataSource = new DataTable();
            ReportTableReturn.GetLabelDataTable(tlpCircuitSpec, DataSource);


            DataRow row = DataSource.NewRow();
            ReportTableReturn.GetDataRow(row, tlpCircuitSpec);
            DataSource.Rows.Add(row);
            return DataSource;

        }

        private void smartTextBox43_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void smartTextBox35_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void smartTextBox56_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void smartTextBox10_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void smartTextBox30_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void tlpCircuitSpec_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
