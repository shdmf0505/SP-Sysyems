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

namespace Micube.SmartMES.StandardInfo.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품목관리 > 품목등록(사양)
    /// 업  무  설  명  : ifV 등록 팝업
    /// 생    성    자  :  정승원
    /// 생    성    일  : 2019-05-22
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ProductItemIFVPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
        public DataRow CurrentDataRow { get; set; }

        #region Local Variables


        string _MASTERDATACLASSID = "";
        string _ITEMID = "";
        string _ITEMVERSION = "";
        string _ENTERPRISEID = "";
        string _PLANTID = "";


        #endregion

        #region 생성자
        public ProductItemIFVPopup()
        {
            InitializeComponent();
			InitializeEvent();
            InitializeCondition();
        }
        public ProductItemIFVPopup(string MASTERDATACLASSID, string ITEMID, string ITEMVERSION, string ENTERPRISEID, string PLANTID)
        {
            InitializeComponent();

            _MASTERDATACLASSID = MASTERDATACLASSID;
             _ITEMID = ITEMID;
             _ITEMVERSION = ITEMVERSION;
             _ENTERPRISEID = ENTERPRISEID;
             _PLANTID = PLANTID;

            InitializeEvent();
            InitializeCondition();
     
            Search(_MASTERDATACLASSID, _ITEMID, _ITEMVERSION, _ENTERPRISEID, _PLANTID);
        }

        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {
            //Site
            //cboPlant.DisplayMember = "PLANTNAME";
            //cboPlant.ValueMember = "PLANTID";
            //cboPlant.ShowHeader = false;
            //Dictionary<string, object> ParamPlant = new Dictionary<string, object>();
            //ParamPlant.Add("ENTERPRISEID", _ENTERPRISEID);
            //ParamPlant.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //DataTable dtPlant = SqlExecuter.Query("GetPlantList", "00001", ParamPlant);
            //cboPlant.DataSource = dtPlant;

            

        }
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            DataTable dtItemMaster = new DataTable();

            dtItemMaster.Columns.Add("_STATE_");
            dtItemMaster.Columns.Add("IFV");

            dtItemMaster.Columns.Add("MASTERDATACLASSID");
            dtItemMaster.Columns.Add("ITEMID");
            dtItemMaster.Columns.Add("ITEMVERSION");
            dtItemMaster.Columns.Add("ENTERPRISEID");

  

            foreach (Control control in smartSplitTableLayoutPanel4.Controls)
            {
                if (control.ToString() == "DevExpress.XtraEditors.PanelControl")
                {
                    foreach (Control controlc in control.Controls)
                    {
                        switch (controlc.ToString())
                        {
                            case "Micube.Framework.SmartControls.SmartSpinEdit":
                            case "Micube.Framework.SmartControls.SmartTextBox":
                                dtItemMaster.Columns.Add(controlc.Tag.ToString());
                                break;
                            case "Micube.Framework.SmartControls.SmartComboBox":
                                dtItemMaster.Columns.Add(controlc.Tag.ToString());
                                break;

                            case "Micube.SmartMES.StandardInfo.ucCustomerPopup":
                                dtItemMaster.Columns.Add(controlc.Tag.ToString());
                                break;
                            case "Micube.Framework.SmartControls.SmartDateEdit":
                                dtItemMaster.Columns.Add(controlc.Tag.ToString());
                                break;
                            case "Micube.Framework.SmartControls.SmartCheckBox":
                                dtItemMaster.Columns.Add(controlc.Tag.ToString());
                                break;
                        }
                    }
                }
            }

            DataRow row = dtItemMaster.NewRow();
            foreach (Control control in smartSplitTableLayoutPanel4.Controls)
            {
                if (control.ToString() == "DevExpress.XtraEditors.PanelControl")
                {
                    foreach (Control controlc in control.Controls)
                    {
                        switch (controlc.ToString())
                        {
                            case "Micube.Framework.SmartControls.SmartSpinEdit":
                            case "Micube.Framework.SmartControls.SmartTextBox":
                                row[controlc.Tag.ToString()] = controlc.Text;
                                break;
                            case "Micube.Framework.SmartControls.SmartComboBox":
                                Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                combox = (Micube.Framework.SmartControls.SmartComboBox)controlc;
                                row[controlc.Tag.ToString()] = combox.GetDataValue();
                                break;
                            case "Micube.Framework.SmartControls.SmartDateEdit":
                                row[controlc.Tag.ToString()] = controlc.Text;
                                break;
                            case "Micube.Framework.SmartControls.SmartCheckBox":
                                Micube.Framework.SmartControls.SmartCheckBox chkbox = new SmartCheckBox();
                                chkbox = (Micube.Framework.SmartControls.SmartCheckBox)controlc;

                                if(chkbox.Checked)
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

            row["MASTERDATACLASSID"] = _MASTERDATACLASSID;
            row["ITEMID"] = _ITEMID;
            row["ITEMVERSION"] = _ITEMVERSION;
            row["ENTERPRISEID"] = _ENTERPRISEID;

            row["_STATE_"] = "modified";
            row["IFV"] = "IFV";
            
            dtItemMaster.Rows.Add(row);

            ExecuteRule("ItemMaster", dtItemMaster);
            ShowMessage("SuccedSave");

        }

        

        /// <summary>
        /// 조회 클릭 - 메인 grid에 체크 데이터 전달
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Search(string sMASTERDATACLASSID, string sITEMID, string sITEMVERSION, string sENTERPRISEID, string sPLANTID)
        {


            Dictionary<string, object> ParamIfv = new Dictionary<string, object>();
            ParamIfv.Add("MASTERDATACLASSID", sMASTERDATACLASSID);
            ParamIfv.Add("ITEMID", sITEMID);
            ParamIfv.Add("ITEMVERSION", sITEMVERSION);
            ParamIfv.Add("ENTERPRISEID", sENTERPRISEID);

            DataTable dtIfv = SqlExecuter.Query("GetProductItemSpecIfv", "10001", ParamIfv);
            if(dtIfv != null)
            {
                if(dtIfv.Rows.Count !=0)
                {
                    foreach (Control control in smartSplitTableLayoutPanel4.Controls)
                    {

                        if (control.ToString() == "DevExpress.XtraEditors.PanelControl")
                        {
                            foreach (Control controlc in control.Controls)
                            {
                                switch (controlc.ToString())
                                {
                                    case "Micube.Framework.SmartControls.SmartSpinEdit":
                                    case "Micube.Framework.SmartControls.SmartTextBox":
                                        if (dtIfv.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                        {
                                            controlc.Text = dtIfv.Rows[0][controlc.Tag.ToString()].ToString();
                                        }
                                        break;
                                    case "Micube.Framework.SmartControls.SmartComboBox":

                                        Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                        combox = (Micube.Framework.SmartControls.SmartComboBox)controlc;

                                        if (dtIfv.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                        {
                                            combox.EditValue = dtIfv.Rows[0][controlc.Tag.ToString()].ToString();
                                        }
                                        break;
                                    case "Micube.SmartMES.StandardInfo.ucCustomerPopup":
                                        //Micube.SmartMES.StandardInfo.ucPublicPopup ucCp = new ucPublicPopup();
                                        //ucCp = (Micube.SmartMES.StandardInfo.ucPublicPopup)controlc;
                                        //if (dtIfv.Columns.IndexOf(ucCp.CODE.Tag.ToString()) != -1)
                                        //{
                                        //    ucCp.CODE.Text = dtIfv.Rows[0][ucCp.CODE.Tag.ToString()].ToString();
                                        //    ucCp.NAME.Text = dtIfv.Rows[0][ucCp.NAME.Tag.ToString()].ToString();
                                        //}
                                        break;
                                    case "Micube.Framework.SmartControls.SmartDateEdit":
                                        if (dtIfv.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                        {
                                            controlc.Text = dtIfv.Rows[0][controlc.Tag.ToString()].ToString();
                                        }
                                        break;
                                    case "Micube.Framework.SmartControls.SmartCheckBox":
                                        Micube.Framework.SmartControls.SmartCheckBox chkbox = new SmartCheckBox();
                                        chkbox = (Micube.Framework.SmartControls.SmartCheckBox)controlc;
                                        if (dtIfv.Rows[0][controlc.Tag.ToString()].ToString() == "Y")
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
        }

        /// <summary>
        /// 취소 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}

