#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 테이블
    /// 업  무  설  명  : 테이블 데이터를 테이블레이아웃 판넬에 등록
    /// 생    성    자  : 윤성원
    /// 생    성    일  : 2019-07-05
    /// 수  정  이  력  : 
    /// </summary>
    /// 
    public class SetControlsFrom
    {
        public SetControlsFrom()
        {

        }

        /// <summary>
        /// 컨트롤 로우 바인딩
        /// </summary>
        /// <param name="panl">로우레이아웃판넬</param>
        /// <param name="row">로우</param>
        /// <returns></returns>
        public DataRow SetControlsFromPanelControlDelRow(Micube.Framework.SmartControls.SmartSplitTableLayoutPanel panl, DataRow row)
        {
            if (row == null)
            {
                return null;
            }

            foreach (Control control in panl.Controls)
            {
                switch (control.ToString())
                {
                    case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                    case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                        if (control.Tag.ToString() != "")
                        {
                            control.Text = row[control.Tag.ToString()].ToString();
                        }
                        break;
                    case "Micube.Framework.SmartControls.SmartMemoEdit": // 메모
                        if (control.Tag.ToString() != "")
                        {
                            control.Text = row[control.Tag.ToString()].ToString();
                        }
                        break;

                    case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                        if (control.Tag.ToString() != "")
                        {
                            //Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                            Micube.Framework.SmartControls.SmartComboBox combox = (Micube.Framework.SmartControls.SmartComboBox)control;
                            combox.EditValue = row[control.Tag.ToString()].ToString();
                        }
                        break;
                    case "Micube.Framework.SmartControls.SmartSelectPopupEdit":

                        //Micube.Framework.SmartControls.SmartSelectPopupEdit SelectPopup = new SmartSelectPopupEdit();
                        Micube.Framework.SmartControls.SmartSelectPopupEdit SelectPopup = (Micube.Framework.SmartControls.SmartSelectPopupEdit)control;

                        if (control.Tag.ToString() != "")
                        {
                            SelectPopup.SetValue(row[control.Tag.ToString()].ToString());
                            if (control.Tag.ToString().IndexOf("ID") != -1)
                            {
                                if (SelectPopup.SelectPopupCondition.DisplayFieldName == control.Tag.ToString())
                                {
                                    SelectPopup.Text = row[control.Tag.ToString()].ToString();
                                }
                                else
                                {
                                    SelectPopup.Text = row[control.Tag.ToString().Replace("ID", "NAME")].ToString();
                                }
                            }
                            else
                            {
                                if (SelectPopup.SelectPopupCondition.DisplayFieldName == control.Tag.ToString())
                                {
                                    SelectPopup.Text = row[control.Tag.ToString()].ToString();
                                }
                                else
                                {
                                    SelectPopup.Text = row[control.Tag.ToString() + "NAME"].ToString();
                                }
                            }
                            //SelectPopup.Refresh(); // TODO : 주석처리?
                            //txtCustomerName.Text = dataRow.Rows[0]["CUSTOMERNAME"].ToString();
                        }
                        break;
                    case "Micube.Framework.SmartControls.SmartDateEdit":  //날짜
                        if (control.Tag.ToString() != "")
                        {
                            control.Text = row[control.Tag.ToString()].ToString();
                        }
                        break;
                    case "Micube.Framework.SmartControls.SmartCheckBox": //체크박스
                        if (control.Tag.ToString() != "")
                        {
                            //Micube.Framework.SmartControls.SmartCheckBox chkbox = new SmartCheckBox();
                            Micube.Framework.SmartControls.SmartCheckBox chkbox = (Micube.Framework.SmartControls.SmartCheckBox)control;
                            if (row[control.Tag.ToString()].ToString() == "Y")
                            {
                                chkbox.Checked = true;
                            }
                            else
                            {
                                chkbox.Checked = false;
                            }
                        }
                        break;
                }
            }
            return row;
        }

     

        /// <summary>
        /// 컨트롤 로우 바인딩
        /// </summary>
        /// <param name="panl">로우레이아웃판넬</param>
        /// <param name="row">로우</param>
        /// <returns></returns>
        public DataRow SetControlsFromBoxControlDelRow(Micube.Framework.SmartControls.SmartSplitTableLayoutPanel box, DataRow row)
        {
            if (row == null)
            {
                return null;
            }

            foreach (Control control in box.Controls)
            {
                switch (control.ToString())
                {
                    case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                    case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                        if (control.Tag.ToString() != "")
                        {
                            control.Text = row[control.Tag.ToString()].ToString();
                        }
                        break;
                    case "Micube.Framework.SmartControls.SmartMemoEdit": // 메모
                        if (control.Tag.ToString() != "")
                        {
                            control.Text = row[control.Tag.ToString()].ToString();
                        }
                        break;

                    case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                        if (control.Tag.ToString() != "")
                        {
                            //Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                            Micube.Framework.SmartControls.SmartComboBox combox = (Micube.Framework.SmartControls.SmartComboBox)control;
 

                            combox.EditValue = row[control.Tag.ToString()].ToString();
                        }
                        break;
                    case "Micube.Framework.SmartControls.SmartSelectPopupEdit":

                        //Micube.Framework.SmartControls.SmartSelectPopupEdit SelectPopup = new SmartSelectPopupEdit();
                        Micube.Framework.SmartControls.SmartSelectPopupEdit SelectPopup = (Micube.Framework.SmartControls.SmartSelectPopupEdit)control;

                        if (control.Tag.ToString() != "")
                        {
                            SelectPopup.SetValue(row[control.Tag.ToString()].ToString());

                            SelectPopup.Text = row[control.Tag.ToString()+"NAME"].ToString();


                            //SelectPopup.Refresh(); // TODO : 주석처리?
                            //txtCustomerName.Text = dataRow.Rows[0]["CUSTOMERNAME"].ToString();
                        }
                        break;
                    case "Micube.Framework.SmartControls.SmartDateEdit":  //날짜
                        if (control.Tag.ToString() != "")
                        {
                            control.Text = row[control.Tag.ToString()].ToString();
                        }
                        break;
                    case "Micube.Framework.SmartControls.SmartCheckBox": //체크박스
                        if (control.Tag.ToString() != "")
                        {
                            //Micube.Framework.SmartControls.SmartCheckBox chkbox = new SmartCheckBox();
                            Micube.Framework.SmartControls.SmartCheckBox chkbox = (Micube.Framework.SmartControls.SmartCheckBox)control;
                            if (row[control.Tag.ToString()].ToString() == "Y")
                            {
                                chkbox.Checked = true;
                            }
                            else
                            {
                                chkbox.Checked = false;
                            }
                        }
                        break;
                    case "Micube.Framework.SmartControls.SmartLabelComboBox":
                        if (control.Tag.ToString() != "")
                            if (control.Tag != null)
                            {
                                {
                                    Micube.Framework.SmartControls.SmartLabelComboBox combox = (Micube.Framework.SmartControls.SmartLabelComboBox)control;
                                    combox.EditValue = row[control.Tag.ToString()].ToString();
                                }
                            }
                        break;
                    case "Micube.Framework.SmartControls.SmartLabelTextBox":
                        if (control.Tag != null)
                        {
                            if (control.Tag.ToString() != "")
                            {
                                control.Text = row[control.Tag.ToString()].ToString();
                            }
                        }
                        break;
                    case "Micube.Framework.SmartControls.SmartLabelCheckEdit":
                        if (control.Tag != null)
                        {
                            if (control.Tag.ToString() != "")
                            {
                                Micube.Framework.SmartControls.SmartLabelCheckEdit chkbox = new SmartLabelCheckEdit();
                                chkbox = (Micube.Framework.SmartControls.SmartLabelCheckEdit)control;
                                if (row[control.Tag.ToString()].ToString() == "True")
                                {
                                    chkbox.EditValue = true;
                                }
                                else
                                {
                                    chkbox.EditValue = false;
                                }
                            }
                        }
                        break;
                    case "Micube.Framework.SmartControls.SmartLabelSelectPopupEdit":

                        //Micube.Framework.SmartControls.SmartSelectPopupEdit SelectPopup = new SmartSelectPopupEdit();
                        Micube.Framework.SmartControls.SmartLabelSelectPopupEdit SelectLabelPopup = (Micube.Framework.SmartControls.SmartLabelSelectPopupEdit)control;

                        if (control.Tag.ToString() != "")
                        {
                            SelectLabelPopup.Text = row[control.Tag.ToString()].ToString();

                            //SelectPopup.Refresh(); // TODO : 주석처리?
                            //txtCustomerName.Text = dataRow.Rows[0]["CUSTOMERNAME"].ToString();
                        }
                        break;
                }

                
            }
            return row;
        }


        /// <summary>
        /// 컨트롤 로우 바인딩
        /// </summary>
        /// <param name="panl">로우레이아웃판넬</param>
        /// <param name="row">로우</param>
        /// <returns></returns>
        public DataRow SetControlsFromRow(Micube.Framework.SmartControls.SmartSplitTableLayoutPanel panl, DataRow row)
        {

            if (row != null)
            {
                
                    foreach (Control control in panl.Controls)
                    {

                        if (control.ToString() == "DevExpress.XtraEditors.PanelControl")
                        {
                            foreach (Control controlc in control.Controls)
                            {
                                switch (controlc.ToString())
                                {
                                    case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                                    case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                                        if (controlc.Tag.ToString() != "")
                                        {
                                            controlc.Text = row[controlc.Tag.ToString()].ToString();
                                        }
                                        break;
                                    case "Micube.Framework.SmartControls.SmartMemoEdit": // 메모
                                        if (controlc.Tag.ToString() != "")
                                        {
                                            controlc.Text = row[controlc.Tag.ToString()].ToString();
                                        }
                                        break;

                                    case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                                            if (controlc.Tag.ToString() != "")
                                            {
                                                Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                                combox = (Micube.Framework.SmartControls.SmartComboBox)controlc;
                                                combox.EditValue = row[controlc.Tag.ToString()].ToString();
                                            }
                                        
                                        
                                        break;
                                    case "Micube.Framework.SmartControls.SmartSelectPopupEdit":

                                        Micube.Framework.SmartControls.SmartSelectPopupEdit SelectPopup = new SmartSelectPopupEdit();

                                        SelectPopup = (Micube.Framework.SmartControls.SmartSelectPopupEdit)controlc;

                                        if (controlc.Tag.ToString() != "")
                                        {
                                            SelectPopup.SetValue(row[controlc.Tag.ToString()].ToString());
                                            if (controlc.Tag.ToString().IndexOf("ID") != -1)
                                            {
                                                if(SelectPopup.SelectPopupCondition.DisplayFieldName == controlc.Tag.ToString())
                                                {
                                                    SelectPopup.Text = row[controlc.Tag.ToString()].ToString();
                                                }
                                                else
                                                {
                                                    SelectPopup.Text = row[controlc.Tag.ToString().Replace("ID", "NAME")].ToString();
                                                }
                                            }
                                            else
                                            {
                                                if (SelectPopup.SelectPopupCondition.DisplayFieldName == controlc.Tag.ToString())
                                                {
                                                    SelectPopup.Text = row[controlc.Tag.ToString()].ToString();
                                                }
                                                else
                                                {
                                                    SelectPopup.Text = row[controlc.Tag.ToString() + "NAME"].ToString();
                                                }
                                            }
                                            SelectPopup.Refresh();
                                        //txtCustomerName.Text = dataRow.Rows[0]["CUSTOMERNAME"].ToString();
                                        }
                                        break;
                                    case "Micube.Framework.SmartControls.SmartDateEdit":  //날짜
                                            if (controlc.Tag.ToString() != "")
                                            {
                                                controlc.Text = row[controlc.Tag.ToString()].ToString();
                                            }
                                            break;
                                    case "Micube.Framework.SmartControls.SmartCheckBox": //체크박스
                                        if (controlc.Tag.ToString() != "")
                                        {
                                            Micube.Framework.SmartControls.SmartCheckBox chkbox = new SmartCheckBox();
                                            chkbox = (Micube.Framework.SmartControls.SmartCheckBox)controlc;
                                            if (row[controlc.Tag.ToString()].ToString() == "Y")
                                            {
                                                chkbox.Checked = true;
                                            }
                                            else
                                            {
                                                chkbox.Checked = false;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }
             
            }
            return row;
        }

        /// <summary>
        /// 컨트롤 테이블 바인딩
        /// </summary>
        /// <param name="panl">테이블레이아웃판넬</param>
        /// <param name="dt">테이터테이블</param>
        /// <returns></returns>
        public DataTable SetControlsFromPanelControlDelTable(Micube.Framework.SmartControls.SmartSplitTableLayoutPanel panl, DataTable dt)
        {
            foreach (Control control in panl.Controls)
            {

                switch (control.ToString())
                {
                    case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                    case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                            control.Text = "";
                        break;
                    case "Micube.Framework.SmartControls.SmartMemoEdit": // 메모
                        control.Text = "";
                        break;
                    case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                        Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                        combox = (Micube.Framework.SmartControls.SmartComboBox)control;
                        combox.EditValue = "";
                        break;
                    case "Micube.Framework.SmartControls.SmartSelectPopupEdit":
                        Micube.Framework.SmartControls.SmartSelectPopupEdit SelectPopup = new SmartSelectPopupEdit();
                        SelectPopup = (Micube.Framework.SmartControls.SmartSelectPopupEdit)control;
                        if (SelectPopup.EditValue != null)
                        {
                            if (SelectPopup.EditValue.ToString() != "")
                            {
                                SelectPopup.SetValue("");
                                SelectPopup.Text = "";
                            }
                        }
                        break;
                    case "Micube.Framework.SmartControls.SmartDateEdit":  //날짜
                        control.Text = "";
                        break;
                    case "Micube.Framework.SmartControls.SmartCheckBox": //체크박스
                        Micube.Framework.SmartControls.SmartCheckBox chkbox = new SmartCheckBox();
                        chkbox = (Micube.Framework.SmartControls.SmartCheckBox)control;
                        chkbox.Checked = false;
                        break;
                }

            }
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    foreach (Control control in panl.Controls)
                    {
                        switch (control.ToString())
                        {
                            case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                            case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                                if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                                {
                                    control.Text = dt.Rows[0][control.Tag.ToString()].ToString();
                                }
                                break;
                            case "Micube.Framework.SmartControls.SmartMemoEdit": // 메모
                                if (control.Tag.ToString() != "")
                                {
                                        control.Text = dt.Rows[0][control.Tag.ToString()].ToString();
                                }
                                break;

                            case "Micube.Framework.SmartControls.SmartComboBox": //콤보

                                Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                combox = (Micube.Framework.SmartControls.SmartComboBox)control;

                                if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                                {
                                    combox.EditValue = dt.Rows[0][control.Tag.ToString()].ToString();
                                }
                                break;
                            case "Micube.Framework.SmartControls.SmartSelectPopupEdit":

                                Micube.Framework.SmartControls.SmartSelectPopupEdit SelectPopup = new SmartSelectPopupEdit();

                                SelectPopup = (Micube.Framework.SmartControls.SmartSelectPopupEdit)control;

                                if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                                {

                                    SelectPopup.SetValue(dt.Rows[0][control.Tag.ToString()].ToString());
                                    if (control.Tag.ToString().IndexOf("ID") != -1)
                                    {
                                        if (SelectPopup.SelectPopupCondition.ValueFieldName != SelectPopup.SelectPopupCondition.DisplayFieldName)
                                        {
                                            SelectPopup.Text = dt.Rows[0][control.Tag.ToString().Replace("ID", "NAME")].ToString();
                                        }
                                        else
                                        {
                                            SelectPopup.Text = dt.Rows[0][control.Tag.ToString()].ToString();
                                        }

                                    }
                                    else
                                    {
                                        if (SelectPopup.SelectPopupCondition.ValueFieldName != SelectPopup.SelectPopupCondition.DisplayFieldName)
                                        {
                                            SelectPopup.Text = dt.Rows[0][control.Tag.ToString() + "NAME"].ToString();
                                        }
                                        else
                                        {
                                            SelectPopup.Text = dt.Rows[0][control.Tag.ToString()].ToString();
                                        }
                                    }
                                    SelectPopup.Refresh();
                                    //txtCustomerName.Text = dataRow.Rows[0]["CUSTOMERNAME"].ToString();
                                }
                                break;
                            case "Micube.Framework.SmartControls.SmartDateEdit":  //날짜
                                if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                                {
                                    if (dt.Rows[0][control.Tag.ToString()].ToString() != "")
                                    {
                                        control.Text = DateTime.Parse(dt.Rows[0][control.Tag.ToString()].ToString()).ToString("yyyy-MM-dd");
                                    }
                                }
                                break;
                            case "Micube.Framework.SmartControls.SmartCheckBox": //체크박스
                                Micube.Framework.SmartControls.SmartCheckBox chkbox = new SmartCheckBox();
                                chkbox = (Micube.Framework.SmartControls.SmartCheckBox)control;
                                if (dt.Rows[0][control.Tag.ToString()].ToString() == "Y")
                                {
                                    chkbox.Checked = true;
                                }
                                else
                                {
                                    chkbox.Checked = false;
                                }

                                break;
                        }
                   }
                }
            }
            return dt;
        }

        /// <summary>
        /// 컨트롤 테이블 바인딩
        /// </summary>
        /// <param name="panl">테이블레이아웃판넬</param>
        /// <param name="dt">테이터테이블</param>
        /// <returns></returns>
        public DataTable SetControlsFromTable(Micube.Framework.SmartControls.SmartSplitTableLayoutPanel panl, DataTable dt)
        {
            foreach (Control control in panl.Controls)
            {
                if (control.ToString() == "DevExpress.XtraEditors.PanelControl")
                {
                    foreach (Control controlc in control.Controls)
                    {
                        switch (controlc.ToString())
                        {
                            case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                            case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                                controlc.Text ="";
                                break;
                            case "Micube.Framework.SmartControls.SmartMemoEdit": // 메모
                                controlc.Text = "";
                                break;
                            case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                                Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                combox = (Micube.Framework.SmartControls.SmartComboBox)controlc;
                                combox.EditValue = "";
                                break;
                            case "Micube.Framework.SmartControls.SmartSelectPopupEdit":
                                Micube.Framework.SmartControls.SmartSelectPopupEdit SelectPopup = new SmartSelectPopupEdit();
                                SelectPopup = (Micube.Framework.SmartControls.SmartSelectPopupEdit)controlc;
                                if(SelectPopup.EditValue != null)
                                {
                                    if (SelectPopup.EditValue.ToString() != "")
                                    {
                                        SelectPopup.SetValue("");
                                        SelectPopup.Text = "";
                                    }
                                }
                                break;
                            case "Micube.Framework.SmartControls.SmartDateEdit":  //날짜
                                controlc.Text = "";
                                break;
                            case "Micube.Framework.SmartControls.SmartCheckBox": //체크박스
                                Micube.Framework.SmartControls.SmartCheckBox chkbox = new SmartCheckBox();
                                chkbox = (Micube.Framework.SmartControls.SmartCheckBox)controlc;
                                chkbox.Checked = false;
                                break;
                        }
                    }
                }
            }
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    foreach (Control control in panl.Controls)
                    {

                        if (control.ToString() == "DevExpress.XtraEditors.PanelControl")
                        {
                            foreach (Control controlc in control.Controls)
                            {
                                switch (controlc.ToString())
                                {
                                    case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                                    case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                                        if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                        {
                                            controlc.Text = dt.Rows[0][controlc.Tag.ToString()].ToString();
                                        }
                                        break;
                                    case "Micube.Framework.SmartControls.SmartMemoEdit": // 메모
                                        if (controlc.Tag.ToString() != "")
                                        {
                                            controlc.Text = dt.Rows[0][controlc.Tag.ToString()].ToString();
                                        }
                                        break;

                                    case "Micube.Framework.SmartControls.SmartComboBox": //콤보

                                        Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                        combox = (Micube.Framework.SmartControls.SmartComboBox)controlc;

                                        if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                        {
                                            combox.EditValue = dt.Rows[0][controlc.Tag.ToString()].ToString();
                                        }
                                        break;
                                    case "Micube.Framework.SmartControls.SmartSelectPopupEdit":

                                        Micube.Framework.SmartControls.SmartSelectPopupEdit SelectPopup = new SmartSelectPopupEdit();

                                        SelectPopup = (Micube.Framework.SmartControls.SmartSelectPopupEdit)controlc;

                                        if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                        {
                                            
                                            SelectPopup.SetValue(dt.Rows[0][controlc.Tag.ToString()].ToString());
                                            if (controlc.Tag.ToString().IndexOf("ID") != -1)
                                            {
                                                if(SelectPopup.SelectPopupCondition.ValueFieldName != SelectPopup.SelectPopupCondition.DisplayFieldName)
                                                {
                                                    SelectPopup.Text = dt.Rows[0][controlc.Tag.ToString().Replace("ID", "NAME")].ToString();
                                                }
                                                else
                                                {
                                                    SelectPopup.Text = dt.Rows[0][controlc.Tag.ToString()].ToString();
                                                }
                                                
                                            }
                                            else
                                            {
                                                if (SelectPopup.SelectPopupCondition.ValueFieldName != SelectPopup.SelectPopupCondition.DisplayFieldName)
                                                {
                                                    SelectPopup.Text = dt.Rows[0][controlc.Tag.ToString() + "NAME"].ToString();
                                                }
                                                else
                                                {
                                                    SelectPopup.Text = dt.Rows[0][controlc.Tag.ToString()].ToString();
                                                }
                                            }
                                            SelectPopup.Refresh();
                                            //txtCustomerName.Text = dataRow.Rows[0]["CUSTOMERNAME"].ToString();
                                        }
                                        break;
                                    case "Micube.Framework.SmartControls.SmartDateEdit":  //날짜
                                        if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                        {
                                            if (dt.Rows[0][controlc.Tag.ToString()].ToString() !="")
                                            {
                                                controlc.Text = DateTime.Parse(dt.Rows[0][controlc.Tag.ToString()].ToString()).ToString("yyyy-MM-dd");
                                            }
                                        }
                                        break;
                                    case "Micube.Framework.SmartControls.SmartCheckBox": //체크박스
                                        Micube.Framework.SmartControls.SmartCheckBox chkbox = new SmartCheckBox();
                                        chkbox = (Micube.Framework.SmartControls.SmartCheckBox)controlc;
                                        if (dt.Rows[0][controlc.Tag.ToString()].ToString() == "Y")
                                        {
                                            chkbox.Checked = true;
                                        }
                                        else
                                        {
                                            chkbox.Checked = false;
                                        }

                                        break;
                                }
                            }
                        }
                    }
                }
            }
            return dt;
        }

        //table laqyout 변경으로 인한 메소드 수정_191213_JHI
        public void SetControl(Control control, DataRow dataRow)
        {
            try
            {
                if (control.GetType() == typeof(SmartSplitTableLayoutPanel))
                {
                    foreach (var controlItem in ((SmartSplitTableLayoutPanel)control).Controls)
                    {
                        if (controlItem.GetType() == typeof(SmartTextBox))
                        {
                            if (!string.IsNullOrEmpty(((SmartTextBox) controlItem).Tag.ToString()))
                            {
                                if (!string.IsNullOrEmpty(dataRow[((SmartTextBox)controlItem).Tag.ToString()].ToString()))
                                {
                                    ((SmartTextBox)controlItem).Text =
                                        dataRow[((SmartTextBox)controlItem).Tag.ToString()].ToString();
                                }
                            }
                        }
                        else if (controlItem.GetType() == typeof(SmartComboBox))
                        {
                            if (!string.IsNullOrEmpty(((SmartComboBox) controlItem).Tag.ToString()))
                            {
                                if (!string.IsNullOrEmpty(dataRow[((SmartComboBox)controlItem).Tag.ToString()].ToString()))
                                {
                                    ((SmartComboBox)controlItem).EditValue =
                                        dataRow[((SmartComboBox)controlItem).Tag.ToString()].ToString();
                                }
                            }
                            
                        }
                        else if (controlItem.GetType() == typeof(SmartSpinEdit))
                        {
                            if (!string.IsNullOrEmpty(((SmartSpinEdit) controlItem).Tag.ToString()))
                            {
                                if (!string.IsNullOrEmpty(dataRow[((SmartSpinEdit)controlItem).Tag.ToString()].ToString()))
                                {
                                    ((SmartSpinEdit)controlItem).Value =
                                        decimal.Parse(dataRow[((SmartSpinEdit)controlItem).Tag.ToString()].ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageException.Create(ex.ToString());
            }
        }

        public void SetControl(Control control, DataTable dataTable)
        {
            try
            {
                if (control.GetType() == typeof(SmartSplitTableLayoutPanel))
                {
                    foreach (var controlItem in ((SmartSplitTableLayoutPanel)control).Controls)
                    {
                        if (controlItem.GetType() == typeof(SmartTextBox))
                        {
                            if (!string.IsNullOrEmpty(dataTable.Rows[0][((SmartTextBox)controlItem).Tag.ToString()].ToString()))
                            {
                                ((SmartTextBox)controlItem).Text =
                                    dataTable.Rows[0][((SmartTextBox)controlItem).Tag.ToString()].ToString();
                            }
                        }
                        else if (controlItem.GetType() == typeof(SmartComboBox))
                        {
                            if (!string.IsNullOrEmpty(dataTable.Rows[0][((SmartComboBox)controlItem).Tag.ToString()].ToString()))
                            {
                                ((SmartComboBox)controlItem).EditValue =
                                    dataTable.Rows[0][((SmartComboBox)controlItem).Tag.ToString()].ToString();
                            }
                        }
                        else if (controlItem.GetType() == typeof(SmartSpinEdit))
                        {
                            if (!string.IsNullOrEmpty(dataTable.Rows[0][((SmartSpinEdit)controlItem).Tag.ToString()].ToString()))
                            {
                                ((SmartSpinEdit)controlItem).Value =
                                    int.Parse(dataTable.Rows[0][((SmartSpinEdit)controlItem).Tag.ToString()].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageException.Create(ex.ToString());
            }
        }

        //InitializeControl_jhi_1217
        public void InitializeControl(Control control)
        {
            try
            {
                if (control.GetType() == typeof(SmartSplitTableLayoutPanel))
                {
                    foreach (var controlItem in ((SmartSplitTableLayoutPanel)control).Controls)
                    {
                        if (controlItem.GetType() == typeof(SmartTextBox))
                        {
                            if (!string.IsNullOrEmpty(((SmartTextBox)controlItem).Tag.ToString()))
                            {
                                var controlBox = (SmartTextBox) controlItem;
                                controlBox.Text = string.Empty;
                            }
                        }
                        else if (controlItem.GetType() == typeof(SmartComboBox))
                        {
                            if (!string.IsNullOrEmpty(((SmartComboBox)controlItem).Tag.ToString()))
                            {
                                var controlBox = (SmartComboBox)controlItem;
                                controlBox.ItemIndex = -1;
                            }

                        }
                        else if (controlItem.GetType() == typeof(SmartSpinEdit))
                        {
                            if (!string.IsNullOrEmpty(((SmartSpinEdit)controlItem).Tag.ToString()))
                            {
                                var controlBox = (SmartSpinEdit)controlItem;
                                controlBox.Value = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
