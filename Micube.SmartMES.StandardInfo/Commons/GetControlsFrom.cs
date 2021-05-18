#region using

using Micube.Framework.SmartControls;
using System.Data;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 테이블레이아웃
    /// 업  무  설  명  : 테이블레이아웃 데이터를 테이블에 등록
    /// 생    성    자  : 윤성원
    /// 생    성    일  : 2019-07-05
    /// 수  정  이  력  :
    ///  2021.02.15 전우성 GetControlsPanelControlDelGrid 함수 static 변경
    ///                    SetControlsEmpty 함수 추가
    /// </summary>
    ///
    public class GetControlsFrom
    {
        public GetControlsFrom()
        {
        }

        /// <summary>
        /// 컨트롤 테이블 바인딩
        /// </summary>
        /// <param name="panl">테이블레이아웃판넬</param>

        /// <returns></returns>
        public DataTable GetControlsFromTable(Micube.Framework.SmartControls.SmartSplitTableLayoutPanel panl)
        {
            DataTable dt = new DataTable();
            DataRow row = dt.NewRow();
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

                                dt.Columns.Add(controlc.Tag.ToString());

                                row[controlc.Tag.ToString()] = controlc.Text;
                                break;

                            case "Micube.Framework.SmartControls.SmartMemoEdit": // 메모
                                dt.Columns.Add(controlc.Tag.ToString());

                                row[controlc.Tag.ToString()] = controlc.Text;
                                break;

                            case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                                Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();

                                combox = (Micube.Framework.SmartControls.SmartComboBox)controlc;
                                dt.Columns.Add(controlc.Tag.ToString());
                                row[controlc.Tag.ToString()] = combox.GetDataValue();
                                break;

                            case "Micube.SmartMES.StandardInfo.ucItemPopup": //유저컨트롤 팝업
                                Micube.SmartMES.StandardInfo.ucItemPopup ucIitem = new StandardInfo.ucItemPopup();
                                ucIitem = (Micube.SmartMES.StandardInfo.ucItemPopup)controlc;

                                dt.Columns.Add(ucIitem.CODE.Tag.ToString());
                                dt.Columns.Add(ucIitem.VERSION.Tag.ToString());

                                row[ucIitem.CODE.Tag.ToString()] = ucIitem.CODE.Text;
                                row[ucIitem.VERSION.Tag.ToString()] = ucIitem.VERSION.Text;

                                break;

                            case "Micube.Framework.SmartControls.SmartDateEdit": //날짜

                                dt.Columns.Add(controlc.Tag.ToString());

                                row[controlc.Tag.ToString()] = controlc.Text;
                                break;

                            case "Micube.Framework.SmartControls.SmartCheckBox": //체크박스
                                Micube.Framework.SmartControls.SmartCheckBox chkbox = new SmartCheckBox();
                                chkbox = (Micube.Framework.SmartControls.SmartCheckBox)controlc;
                                dt.Columns.Add(controlc.Tag.ToString());
                                if (chkbox.Checked)
                                {
                                    row[controlc.Tag.ToString()] = "Y";
                                }
                                else
                                {
                                    row[controlc.Tag.ToString()] = "N";
                                }
                                break;
                        }
                    }
                }
            }
            dt.Rows.Add(row);
            return dt;
        }

        /// <summary>
        /// 컨트롤 테이블 바인딩
        /// </summary>
        /// <param name="panl">테이블레이아웃판넬</param>

        /// <returns></returns>
        public void GetControlsFromPanelControlDelGrid(Micube.Framework.SmartControls.SmartSplitTableLayoutPanel panl, Micube.Framework.SmartControls.SmartBandedGrid grid)
        {
            object obj = grid.View.DataSource;
            DataTable dt = ((System.Data.DataView)obj).Table;
            DataRow row = grid.View.GetFocusedDataRow();
            if (row != null)
            {
                foreach (Control control in panl.Controls)
                {
                    switch (control.ToString())
                    {
                        case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                        case "Micube.Framework.SmartControls.SmartTextBox": // 에디터

                            if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                            {
                                if (row[control.Tag.ToString()].ToString() != control.Text)
                                {
                                    row[control.Tag.ToString()] = control.Text;
                                }
                            }
                            break;

                        case "Micube.Framework.SmartControls.SmartMemoEdit": // 메모
                            if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                            {
                                if (row[control.Tag.ToString()].ToString() != control.Text)
                                {
                                    row[control.Tag.ToString()] = control.Text;
                                }
                            }
                            break;

                        case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                            Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();

                            combox = (Micube.Framework.SmartControls.SmartComboBox)control;

                            if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                            {
                                if (combox.GetDataValue() != null)
                                {
                                    if (row[control.Tag.ToString()].ToString() != combox.GetDataValue().ToString())
                                    {
                                        row[control.Tag.ToString()] = combox.GetDataValue();
                                    }
                                }
                            }

                            break;

                        case "Micube.SmartMES.StandardInfo.ucItemPopup": //유저컨트롤 팝업
                            Micube.SmartMES.StandardInfo.ucItemPopup ucIitem = new StandardInfo.ucItemPopup();
                            ucIitem = (Micube.SmartMES.StandardInfo.ucItemPopup)control;

                            if (dt.Columns.IndexOf(ucIitem.CODE.Tag.ToString()) != -1)
                            {
                                if (row[ucIitem.CODE.Tag.ToString()].ToString() != ucIitem.CODE.Text)
                                {
                                    row[ucIitem.CODE.Tag.ToString()] = ucIitem.CODE.Text;
                                }
                                if (row[ucIitem.VERSION.Tag.ToString()].ToString() != ucIitem.VERSION.Text)
                                {
                                    row[ucIitem.VERSION.Tag.ToString()] = ucIitem.VERSION.Text;
                                }
                            }

                            break;

                        case "Micube.Framework.SmartControls.SmartDateEdit": //날짜

                            if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                            {
                                if (row[control.Tag.ToString()].ToString() != control.Text)
                                {
                                    row[control.Tag.ToString()] = control.Text;
                                }
                            }

                            break;

                        case "Micube.Framework.SmartControls.SmartCheckBox": //체크박스
                            Micube.Framework.SmartControls.SmartCheckBox chkbox = new SmartCheckBox();
                            chkbox = (Micube.Framework.SmartControls.SmartCheckBox)control;

                            if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                            {
                                if (chkbox.Checked)
                                {
                                    row[control.Tag.ToString()] = "Y";
                                }
                                else
                                {
                                    row[control.Tag.ToString()] = "N";
                                }
                            }
                            break;

                        case "Micube.Framework.SmartControls.SmartSelectPopupEdit":
                            Micube.Framework.SmartControls.SmartSelectPopupEdit SelectPopup = new SmartSelectPopupEdit();
                            SelectPopup = (Micube.Framework.SmartControls.SmartSelectPopupEdit)control;

                            if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                            {
                                if (row[control.Tag.ToString()].ToString() != SelectPopup.GetValue().ToString())
                                {
                                    row[control.Tag.ToString()] = SelectPopup.GetValue();
                                }
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 화면 컨트롤러 초기화
        /// </summary>
        /// <param name="pnl"></param>
        public static void SetControlsEmpty(TableLayoutPanel pnl)
        {
            foreach (Control control in pnl.Controls)
            {
                switch (control.ToString())
                {
                    case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                    case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                    case "Micube.Framework.SmartControls.SmartMemoEdit": // 메모
                    case "Micube.Framework.SmartControls.SmartDateEdit": //날짜
                        control.Text = string.Empty;
                        break;

                    case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                        (control as SmartComboBox).EditValue = string.Empty;
                        break;

                    case "Micube.SmartMES.StandardInfo.ucItemPopup": //유저컨트롤 팝업
                        (control as ucItemPopup).CODE.Text = string.Empty;
                        (control as ucItemPopup).VERSION.Text = string.Empty;
                        break;

                    case "Micube.Framework.SmartControls.SmartCheckBox": //체크박스
                        (control as SmartCheckBox).Checked = false;
                        break;

                    case "Micube.Framework.SmartControls.SmartSelectPopupEdit":
                        (control as SmartSelectPopupEdit).EditValue = string.Empty;
                        break;
                }
            }
        }

        /// <summary>
        /// 컨트롤 테이블 바인딩
        /// </summary>
        /// <param name="panl">테이블레이아웃판넬</param>

        /// <returns></returns>
        public static void GetControlsPanelControlDelGrid(TableLayoutPanel panl, Micube.Framework.SmartControls.SmartBandedGrid grid)
        {
            object obj = grid.View.DataSource;
            DataTable dt = ((DataView)obj).Table;
            DataRow row = grid.View.GetFocusedDataRow();

            if (row != null)
            {
                foreach (Control control in panl.Controls)
                {
                    switch (control.ToString())
                    {
                        case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                        case "Micube.Framework.SmartControls.SmartTextBox": // 에디터
                            if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                            {
                                if (row[control.Tag.ToString()].ToString() != control.Text)
                                {
                                    row[control.Tag.ToString()] = control.Text;
                                }
                            }
                            break;

                        case "Micube.Framework.SmartControls.SmartMemoEdit": // 메모
                            if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                            {
                                if (row[control.Tag.ToString()].ToString() != control.Text)
                                {
                                    row[control.Tag.ToString()] = control.Text;
                                }
                            }
                            break;

                        case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                            SmartComboBox combox = new SmartComboBox();
                            combox = (SmartComboBox)control;

                            if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                            {
                                if (combox.GetDataValue() != null)
                                {
                                    if (row[control.Tag.ToString()].ToString() != combox.GetDataValue().ToString())
                                    {
                                        row[control.Tag.ToString()] = combox.GetDataValue();
                                    }
                                }
                            }
                            break;

                        case "Micube.SmartMES.StandardInfo.ucItemPopup": //유저컨트롤 팝업
                            ucItemPopup ucIitem = new ucItemPopup();
                            ucIitem = (ucItemPopup)control;

                            if (dt.Columns.IndexOf(ucIitem.CODE.Tag.ToString()) != -1)
                            {
                                if (row[ucIitem.CODE.Tag.ToString()].ToString() != ucIitem.CODE.Text)
                                {
                                    row[ucIitem.CODE.Tag.ToString()] = ucIitem.CODE.Text;
                                }

                                if (row[ucIitem.VERSION.Tag.ToString()].ToString() != ucIitem.VERSION.Text)
                                {
                                    row[ucIitem.VERSION.Tag.ToString()] = ucIitem.VERSION.Text;
                                }
                            }
                            break;

                        case "Micube.Framework.SmartControls.SmartDateEdit": //날짜
                            if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                            {
                                if (row[control.Tag.ToString()].ToString() != control.Text)
                                {
                                    row[control.Tag.ToString()] = control.Text;
                                }
                            }
                            break;

                        case "Micube.Framework.SmartControls.SmartCheckBox": //체크박스
                            SmartCheckBox chkbox = new SmartCheckBox();
                            chkbox = (SmartCheckBox)control;

                            if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                            {
                                row[control.Tag.ToString()] = chkbox.Checked ? "Y" : "N";
                            }
                            break;

                        case "Micube.Framework.SmartControls.SmartSelectPopupEdit":
                            SmartSelectPopupEdit SelectPopup = new SmartSelectPopupEdit();
                            SelectPopup = (SmartSelectPopupEdit)control;

                            if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                            {
                                if (row[control.Tag.ToString()].ToString() != SelectPopup.GetValue().ToString())
                                {
                                    row[control.Tag.ToString()] = SelectPopup.GetValue();
                                }
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 컨트롤 테이블 바인딩
        /// </summary>
        /// <param name="panl">테이블레이아웃판넬</param>

        /// <returns></returns>
        public void GetControlsFromBoxControlDelGrid(Micube.Framework.SmartControls.SmartSplitTableLayoutPanel panl, Micube.Framework.SmartControls.SmartBandedGrid grid)
        {
            object obj = grid.View.DataSource;
            DataTable dt = ((System.Data.DataView)obj).Table;
            DataRow row = grid.View.GetFocusedDataRow();
            if (row != null)
            {
                foreach (Control control in panl.Controls)
                {
                    switch (control.ToString())
                    {
                        case "Micube.Framework.SmartControls.SmartSpinEdit": // 숫자에디터
                        case "Micube.Framework.SmartControls.SmartTextBox": // 에디터

                            if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                            {
                                if (row[control.Tag.ToString()].ToString() != control.Text)
                                {
                                    row[control.Tag.ToString()] = control.Text;
                                }
                            }
                            break;

                        case "Micube.Framework.SmartControls.SmartMemoEdit": // 메모
                            if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                            {
                                if (row[control.Tag.ToString()].ToString() != control.Text)
                                {
                                    row[control.Tag.ToString()] = control.Text;
                                }
                            }
                            break;

                        case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                            Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();

                            combox = (Micube.Framework.SmartControls.SmartComboBox)control;

                            if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                            {
                                if (combox.GetDataValue() != null)
                                {
                                    if (row[control.Tag.ToString()].ToString() != combox.GetDataValue().ToString())
                                    {
                                        row[control.Tag.ToString()] = combox.GetDataValue();
                                    }
                                }
                            }

                            break;

                        case "Micube.SmartMES.StandardInfo.ucItemPopup": //유저컨트롤 팝업
                            Micube.SmartMES.StandardInfo.ucItemPopup ucIitem = new StandardInfo.ucItemPopup();
                            ucIitem = (Micube.SmartMES.StandardInfo.ucItemPopup)control;

                            if (dt.Columns.IndexOf(ucIitem.CODE.Tag.ToString()) != -1)
                            {
                                if (row[ucIitem.CODE.Tag.ToString()].ToString() != ucIitem.CODE.Text)
                                {
                                    row[ucIitem.CODE.Tag.ToString()] = ucIitem.CODE.Text;
                                }
                                if (row[ucIitem.VERSION.Tag.ToString()].ToString() != ucIitem.VERSION.Text)
                                {
                                    row[ucIitem.VERSION.Tag.ToString()] = ucIitem.VERSION.Text;
                                }
                            }

                            break;

                        case "Micube.Framework.SmartControls.SmartDateEdit": //날짜

                            if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                            {
                                if (row[control.Tag.ToString()].ToString() != control.Text)
                                {
                                    row[control.Tag.ToString()] = control.Text;
                                }
                            }

                            break;

                        case "Micube.Framework.SmartControls.SmartCheckBox": //체크박스
                            Micube.Framework.SmartControls.SmartCheckBox chkbox = new SmartCheckBox();
                            chkbox = (Micube.Framework.SmartControls.SmartCheckBox)control;

                            if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                            {
                                if (chkbox.Checked)
                                {
                                    row[control.Tag.ToString()] = "Y";
                                }
                                else
                                {
                                    row[control.Tag.ToString()] = "N";
                                }
                            }
                            break;

                        case "Micube.Framework.SmartControls.SmartSelectPopupEdit":
                            Micube.Framework.SmartControls.SmartSelectPopupEdit SelectPopup = new SmartSelectPopupEdit();
                            SelectPopup = (Micube.Framework.SmartControls.SmartSelectPopupEdit)control;

                            if (dt.Columns.IndexOf(control.Tag.ToString()) != -1)
                            {
                                if (row[control.Tag.ToString()].ToString() != SelectPopup.GetValue().ToString())
                                {
                                    row[control.Tag.ToString()] = SelectPopup.GetValue();
                                }
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 컨트롤 테이블 바인딩
        /// </summary>
        /// <param name="panl">테이블레이아웃판넬</param>

        /// <returns></returns>
        public void GetControlsFromGrid(Micube.Framework.SmartControls.SmartSplitTableLayoutPanel panl, Micube.Framework.SmartControls.SmartBandedGrid grid)
        {
            object obj = grid.View.DataSource;
            DataTable dt = ((System.Data.DataView)obj).Table;
            DataRow row = grid.View.GetFocusedDataRow();
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

                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        if (row[controlc.Tag.ToString()].ToString() != controlc.Text)
                                        {
                                            row[controlc.Tag.ToString()] = controlc.Text;
                                        }
                                    }
                                    break;

                                case "Micube.Framework.SmartControls.SmartMemoEdit": // 메모
                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        if (row[controlc.Tag.ToString()].ToString() != controlc.Text)
                                        {
                                            row[controlc.Tag.ToString()] = controlc.Text;
                                        }
                                    }
                                    break;

                                case "Micube.Framework.SmartControls.SmartComboBox": //콤보
                                    Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();

                                    combox = (Micube.Framework.SmartControls.SmartComboBox)controlc;

                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        if (combox.GetDataValue() != null)
                                        {
                                            if (row[controlc.Tag.ToString()].ToString() != combox.GetDataValue().ToString())
                                            {
                                                row[controlc.Tag.ToString()] = combox.GetDataValue();
                                            }
                                        }
                                    }

                                    break;

                                case "Micube.SmartMES.StandardInfo.ucItemPopup": //유저컨트롤 팝업
                                    Micube.SmartMES.StandardInfo.ucItemPopup ucIitem = new StandardInfo.ucItemPopup();
                                    ucIitem = (Micube.SmartMES.StandardInfo.ucItemPopup)controlc;

                                    if (dt.Columns.IndexOf(ucIitem.CODE.Tag.ToString()) != -1)
                                    {
                                        if (row[ucIitem.CODE.Tag.ToString()].ToString() != ucIitem.CODE.Text)
                                        {
                                            row[ucIitem.CODE.Tag.ToString()] = ucIitem.CODE.Text;
                                        }
                                        if (row[ucIitem.VERSION.Tag.ToString()].ToString() != ucIitem.VERSION.Text)
                                        {
                                            row[ucIitem.VERSION.Tag.ToString()] = ucIitem.VERSION.Text;
                                        }
                                    }

                                    break;

                                case "Micube.Framework.SmartControls.SmartDateEdit": //날짜

                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        if (row[controlc.Tag.ToString()].ToString() != controlc.Text)
                                        {
                                            row[controlc.Tag.ToString()] = controlc.Text;
                                        }
                                    }

                                    break;

                                case "Micube.Framework.SmartControls.SmartCheckBox": //체크박스
                                    Micube.Framework.SmartControls.SmartCheckBox chkbox = new SmartCheckBox();
                                    chkbox = (Micube.Framework.SmartControls.SmartCheckBox)controlc;

                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        if (chkbox.Checked)
                                        {
                                            row[controlc.Tag.ToString()] = "Y";
                                        }
                                        else
                                        {
                                            row[controlc.Tag.ToString()] = "N";
                                        }
                                    }
                                    break;

                                case "Micube.Framework.SmartControls.SmartSelectPopupEdit":
                                    Micube.Framework.SmartControls.SmartSelectPopupEdit SelectPopup = new SmartSelectPopupEdit();
                                    SelectPopup = (Micube.Framework.SmartControls.SmartSelectPopupEdit)controlc;

                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        if (row[controlc.Tag.ToString()].ToString() != SelectPopup.GetValue().ToString())
                                        {
                                            row[controlc.Tag.ToString()] = SelectPopup.GetValue();
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}