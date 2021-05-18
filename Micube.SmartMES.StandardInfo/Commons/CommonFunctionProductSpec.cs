using Micube.Framework;
using Micube.Framework.SmartControls;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 품목 사양 공통 함수
    /// 업  무  설  명  :
    /// 생    성    자  : 정승원
    /// 생    성    일  : 2019-12-12
    /// 수  정  이  력  :
    ///
    ///
    /// </summary>
    internal class CommonFunctionProductSpec
    {
        /// <summary>
        /// 저장 데이터 Dictionary GET
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="pairs"></param>
        public static void GetSaveDataDictionary(Control ctl, Dictionary<string, object> pairs)
        {
            switch (ctl.GetType().Name)
            {
                case "SmartTextBox":
                    SmartTextBox txtCtl = ctl as SmartTextBox;
                    if (!string.IsNullOrEmpty(Format.GetString(txtCtl.Tag)))
                    {
                        string strValue = Format.GetString(txtCtl.EditValue);
                        if (!Format.GetString(txtCtl.Tag).Equals("PRODUCTDIMENSIONS") && !Format.GetString(txtCtl.Tag).Equals("XOUT"))
                        {
                            strValue = strValue.Replace("%", "");
                            strValue = strValue.Replace("㎛", "");
                            strValue = strValue.Replace("㎟/PNL", "");
                            strValue = strValue.Replace("sq/mm", "");
                            strValue = strValue.Replace("㎟", "");
                            strValue = strValue.Replace("PCS", "");
                            strValue = strValue.Replace("MM", "");
                            strValue = strValue.Replace("X :", "");
                            strValue = strValue.Replace("Y :", "");
                            strValue = strValue.Replace("Left:", "");
                            strValue = strValue.Replace("Right:", "");
                            strValue.TrimStart('±');
                        }
                        pairs.Add(Format.GetString(txtCtl.Tag), strValue.Trim());
                    }
                    break;

                case "SmartComboBox":
                    SmartComboBox cboCtl = ctl as SmartComboBox;
                    if (!string.IsNullOrEmpty(Format.GetString(cboCtl.Tag)))
                    {
                        pairs.Add(Format.GetString(cboCtl.Tag), cboCtl.GetDataValue());
                    }
                    break;

                case "SmartSelectPopupEdit":
                    SmartSelectPopupEdit popCtl = ctl as SmartSelectPopupEdit;
                    if (!string.IsNullOrEmpty(Format.GetString(popCtl.Tag)))
                    {
                        pairs.Add(Format.GetString(popCtl.Tag), popCtl.GetValue());
                    }
                    break;

                case "SmartDateEdit":
                    SmartDateEdit date = ctl as SmartDateEdit;
                    if (!string.IsNullOrEmpty(Format.GetString(date.Tag)))
                    {
                        pairs.Add(date.Tag.ToString(), date.Text);
                    }
                    break;

                case "SmartSpinEdit":
                    SmartSpinEdit spin = ctl as SmartSpinEdit;
                    if (!string.IsNullOrEmpty(Format.GetString(spin.Tag)))
                    {
                        pairs.Add(Format.GetString(spin.Tag), spin.EditValue);
                    }
                    break;

                case "SmartMemoEdit":
                    SmartMemoEdit memo = ctl as SmartMemoEdit;
                    if (!string.IsNullOrEmpty(Format.GetString(memo.Tag)))
                    {
                        pairs.Add(Format.GetString(memo.Tag), memo.EditValue);
                    }
                    break;
            }

            foreach (Control c in ctl.Controls)
            {
                GetSaveDataDictionary(c, pairs);
            }
        }

        /// <summary>
        /// 조회 데이터 바인드
        /// </summary>
        /// <param name="dt"></param>
        public static void SearchDataBind(DataRow r, Control ctl)
        {
            switch (ctl.GetType().Name)
            {
                case "SmartComboBox":
                    SmartComboBox cbo = ctl as SmartComboBox;

                    if (!string.IsNullOrEmpty(Format.GetString(cbo.Tag)))
                    {
                        if (r.Table.Columns.Contains(Format.GetString(cbo.Tag)))
                        {
                            cbo.EditValue = r[Format.GetString(cbo.Tag)];
                        }
                    }
                    break;

                case "SmartTextBox":
                    SmartTextBox txt = ctl as SmartTextBox;

                    if (!string.IsNullOrEmpty(Format.GetString(txt.Tag)))
                    {
                        if (r.Table.Columns.Contains(Format.GetString(txt.Tag)))
                        {
                            txt.EditValue = r[Format.GetString(txt.Tag)].ToString();
                            //pcsary(panel당 블럭수량)은 0일 경우 1로 셋팅
                            if(txt.Tag.ToString().Equals("PCSARY") && (txt.EditValue.ToString().Equals("0") || txt.EditValue.ToString().Equals(string.Empty)))
                            {
                                txt.EditValue = "1";
                            }
                        }
                    }
                    break;

                case "SmartSelectPopupEdit":
                    SmartSelectPopupEdit pop = ctl as SmartSelectPopupEdit;

                    if (!string.IsNullOrEmpty(Format.GetString(pop.Tag)))
                    {
                        if (r.Table.Columns.Contains(Format.GetString(pop.Tag)))
                        {
                            pop.SetValue(r[Format.GetString(pop.Tag)]);
                            pop.EditValue = r[Format.GetString(pop.Tag) + "NAME"].ToString();
                        }
                    }
                    break;

                case "SmartDateEdit":
                    SmartDateEdit date = ctl as SmartDateEdit;

                    if (!string.IsNullOrEmpty(Format.GetString(date.Tag)))
                    {
                        if (r.Table.Columns.Contains(Format.GetString(date.Tag)))
                        {
                            date.EditValue = r[Format.GetString(date.Tag)].ToString();
                        }
                    }
                    break;

                case "SmartSpinEdit":
                    SmartSpinEdit spin = ctl as SmartSpinEdit;

                    if (!string.IsNullOrEmpty(Format.GetString(spin.Tag)))
                    {
                        if (r.Table.Columns.Contains(Format.GetString(spin.Tag)))
                        {
                            spin.EditValue = r[Format.GetString(spin.Tag)];
                        }
                    }
                    break;

                case "SmartMemoEdit":
                    SmartMemoEdit memo = ctl as SmartMemoEdit;

                    if (!string.IsNullOrEmpty(Format.GetString(memo.Tag)))
                    {
                        if (r.Table.Columns.Contains(Format.GetString(memo.Tag)))
                        {
                            memo.EditValue = r[Format.GetString(memo.Tag)];
                        }
                    }
                    break;
            }

            foreach (Control c in ctl.Controls)
            {
                SearchDataBind(r, c);
            }
        }

        /// <summary>
        /// 조회 데이터 바인드
        /// </summary>
        /// <param name="pairs"></param>
        /// <param name="ctl"></param>
        public static void SearchDataBind(Dictionary<string, object> pairs, Control ctl)
        {
            switch (ctl.GetType().Name)
            {
                case "SmartComboBox":
                    SmartComboBox cbo = ctl as SmartComboBox;
                    if (!string.IsNullOrEmpty(Format.GetString(cbo.Tag)))
                    {
                        if (pairs.Keys.Contains(Format.GetString(cbo.Tag)))
                        {
                            cbo.EditValue = pairs[Format.GetString(cbo.Tag)];
                        }
                    }
                    break;

                case "SmartTextBox":
                    SmartTextBox txt = ctl as SmartTextBox;
                    if (!string.IsNullOrEmpty(Format.GetString(txt.Tag)))
                    {
                        if (pairs.Keys.Contains(Format.GetString(txt.Tag)))
                        {
                            txt.EditValue = pairs[Format.GetString(txt.Tag)].ToString();
                        }
                    }
                    break;

                case "SmartSelectPopupEdit":
                    SmartSelectPopupEdit pop = ctl as SmartSelectPopupEdit;
                    if (!string.IsNullOrEmpty(Format.GetString(pop.Tag)))
                    {
                        if (pairs.Keys.Contains(Format.GetString(pop.Tag)))
                        {
                            pop.Text = pairs[Format.GetString(pop.Tag)].ToString();
                        }
                    }
                    break;

                case "SmartDateEdit":
                    SmartDateEdit date = ctl as SmartDateEdit;
                    if (!string.IsNullOrEmpty(Format.GetString(date.Tag)))
                    {
                        if (pairs.Keys.Contains(Format.GetString(date.Tag)))
                        {
                            date.EditValue = pairs[Format.GetString(date.Tag)];
                        }
                    }
                    break;

                case "SmartSpinEdit":
                    SmartSpinEdit spin = ctl as SmartSpinEdit;
                    if (!string.IsNullOrEmpty(Format.GetString(spin.Tag)))
                    {
                        if (pairs.Keys.Contains(Format.GetString(spin.Tag)))
                        {
                            spin.EditValue = pairs[Format.GetString(spin.Tag)];
                        }
                    }
                    break;
            }

            foreach (Control c in ctl.Controls)
            {
                SearchDataBind(pairs, c);
            }
        }

        /// <summary>
        /// 필수 입력 리스트 GET
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="str"></param>
        public static void GetRequiredValidationList(Control ctl, List<string> requiredList)
        {
            if (ctl.GetType() == typeof(SmartLabel))
            {
                SmartLabel lbl = ctl as SmartLabel;
                if (lbl.ForeColor.Equals(Color.Red))
                {
                    requiredList.Add(lbl.LanguageKey);
                }
            }

            foreach (Control c in ctl.Controls)
            {
                GetRequiredValidationList(c, requiredList);
            }
        }

        /// <summary>
        /// 필수 입력 체크
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="str"></param>
        public static void RequiredListNullOrEmptyCheck(Control ctl, List<string> requiredList, string strGroupKey = "")
        {
            string tag = string.Empty;
            string groupName = string.Empty;
            if (!string.IsNullOrEmpty(groupName))
                groupName = Language.Get(strGroupKey) + "-";

            switch (ctl.GetType().Name)
            {
                case "SmartTextBox":
                    SmartTextBox txt = ctl as SmartTextBox;
                    if (!string.IsNullOrEmpty(Format.GetString(txt.Tag)))
                    {
                        tag = Regex.Replace(Format.GetString(txt.Tag), @"\d+", "");

                        if (tag.StartsWith("FROM"))
                        {
                            tag = tag.Substring(4);
                        }

                        if (tag.StartsWith("TO"))
                        {
                            tag = tag.Substring(2);
                        }

                        if (requiredList.Contains(tag))
                        {
                            if (string.IsNullOrWhiteSpace(Format.GetString(txt.EditValue)))
                            {
                                throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get(tag));
                            }
                        }
                    }

                    break;

                case "SmartComboBox":
                    SmartComboBox cbo = ctl as SmartComboBox;
                    if (!string.IsNullOrEmpty(Format.GetString(cbo.Tag)))
                    {
                        tag = Regex.Replace(Format.GetString(cbo.Tag), @"\d+", "");

                        if (tag.StartsWith("FROM"))
                        {
                            tag = tag.Substring(4);
                        }

                        if (tag.StartsWith("TO"))
                        {
                            tag = tag.Substring(2);
                        }

                        if (requiredList.Contains(tag))
                        {
                            if (string.IsNullOrWhiteSpace(Format.GetString(cbo.GetDataValue())))
                            {
                                throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get(tag));
                            }
                        }
                    }

                    break;

                case "SmartSelectPopupEdit":
                    SmartSelectPopupEdit pop = ctl as SmartSelectPopupEdit;
                    if (!string.IsNullOrEmpty(Format.GetString(pop.Tag)))
                    {
                        tag = Regex.Replace(Format.GetString(pop.Tag), @"\d+", "");

                        if (tag.StartsWith("FROM"))
                        {
                            tag = tag.Substring(4);
                        }

                        if (tag.StartsWith("TO"))
                        {
                            tag = tag.Substring(2);
                        }

                        if (requiredList.Contains(tag))
                        {
                            if (string.IsNullOrWhiteSpace(Format.GetString(pop.GetValue())))
                            {
                                throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get(tag));
                            }
                        }
                    }

                    break;

                case "SmartDateEdit":
                    SmartDateEdit date = ctl as SmartDateEdit;
                    if (!string.IsNullOrEmpty(Format.GetString(date.Tag)))
                    {
                        tag = Regex.Replace(Format.GetString(date.Tag), @"\d+", "");

                        if (tag.StartsWith("FROM"))
                        {
                            tag = tag.Substring(4);
                        }

                        if (tag.StartsWith("TO"))
                        {
                            tag = tag.Substring(2);
                        }

                        if (requiredList.Contains(tag))
                        {
                            if (string.IsNullOrWhiteSpace(Format.GetString(date.EditValue)))
                            {
                                throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get(tag));
                            }
                        }
                    }

                    break;

                case "SmartSpinEdit":
                    SmartSpinEdit spin = ctl as SmartSpinEdit;
                    if (!string.IsNullOrEmpty(Format.GetString(spin.Tag)))
                    {
                        tag = Regex.Replace(Format.GetString(spin.Tag), @"\d+", "");

                        if (tag.StartsWith("FROM"))
                        {
                            tag = tag.Substring(4);
                        }

                        if (tag.StartsWith("TO"))
                        {
                            tag = tag.Substring(2);
                        }

                        if (requiredList.Contains(tag))
                        {
                            if (string.IsNullOrWhiteSpace(Format.GetString(spin.EditValue)))
                            {
                                throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get(tag));
                            }
                        }
                    }

                    break;
            }

            foreach (Control c in ctl.Controls)
            {
                RequiredListNullOrEmptyCheck(c, requiredList, strGroupKey);
            }
        }

        /// <summary>
        /// 데이터 clear
        /// </summary>
        /// <param name="dt"></param>
        public static void ClearData(Control ctl)
        {
            switch (ctl.GetType().Name)
            {
                case "SmartComboBox":
                    SmartComboBox cbo = ctl as SmartComboBox;
                    if (!string.IsNullOrEmpty(Format.GetString(cbo.Tag)))
                    {
                        cbo.EditValue = string.Empty;
                    }
                    break;

                case "SmartTextBox":
                    SmartTextBox txt = ctl as SmartTextBox;

                    if (!string.IsNullOrEmpty(Format.GetString(txt.Tag)))
                    {
                        txt.EditValue = string.Empty;
                    }
                    break;

                case "SmartSelectPopupEdit":
                    SmartSelectPopupEdit pop = ctl as SmartSelectPopupEdit;
                    if (!string.IsNullOrEmpty(Format.GetString(pop.Tag)))
                    {
                        pop.SetValue(null);
                        pop.Text = string.Empty;
                    }
                    break;

                case "SmartDateEdit":
                    SmartDateEdit date = ctl as SmartDateEdit;
                    if (!string.IsNullOrEmpty(Format.GetString(date.Tag)))
                    {
                        date.EditValue = string.Empty;
                    }
                    break;

                case "SmartSpinEdit":
                    SmartSpinEdit spin = ctl as SmartSpinEdit;
                    if (!string.IsNullOrEmpty(Format.GetString(spin.Tag)))
                    {
                        spin.EditValue = string.Empty;
                    }
                    break;

                case "SmartMemoEdit":
                    SmartMemoEdit memo = ctl as SmartMemoEdit;
                    if (!string.IsNullOrEmpty(Format.GetString(memo.Tag)))
                    {
                        memo.EditValue = string.Empty;
                    }
                    break;
            }

            foreach (Control c in ctl.Controls)
            {
                ClearData(c);
            }
        }
    }
}