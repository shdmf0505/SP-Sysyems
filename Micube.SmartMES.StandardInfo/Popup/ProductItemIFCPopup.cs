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
    /// 업  무  설  명  : ifC 등록 팝업
    /// 생    성    자  :  정승원
    /// 생    성    일  : 2019-05-22
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ProductItemIFCPopup : SmartPopupBaseForm, ISmartCustomPopup
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
        public ProductItemIFCPopup()
        {
            InitializeComponent();
			InitializeEvent();
            InitializeCondition();
        }
        public ProductItemIFCPopup(string MASTERDATACLASSID, string ITEMID, string ITEMVERSION, string ENTERPRISEID, string PLANTID)
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

            //사내 기준 동도금 TYPE
            cboCOPPERTYPE.DisplayMember = "CODENAME";
            cboCOPPERTYPE.ValueMember = "CODEID";
            cboCOPPERTYPE.ShowHeader = false;
            Dictionary<string, object> ParamCopertype = new Dictionary<string, object>();
            ParamCopertype.Add("CODECLASSID", "COPPERTYPE");
            ParamCopertype.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCOPPERTYPE = SqlExecuter.Query("GetCodeList", "00001", ParamCopertype);
            cboCOPPERTYPE.DataSource = dtCOPPERTYPE;

            //사내 기준표면도금TYPE
            cboSURFACETREATMENT.DisplayMember = "CODENAME";
            cboSURFACETREATMENT.ValueMember = "CODEID";
            cboSURFACETREATMENT.ShowHeader = false;
            Dictionary<string, object> ParamSurfacetreatment = new Dictionary<string, object>();
            ParamSurfacetreatment.Add("CODECLASSID", "SURFACETREATMENT");
            ParamSurfacetreatment.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtSURFACETREATMENT = SqlExecuter.Query("GetCodeList", "00001", ParamSurfacetreatment);
            cboSURFACETREATMENT.DataSource = dtSURFACETREATMENT;


            //MIN C/L 쏠림
            cboMINCL.DisplayMember = "CODENAME";
            cboMINCL.ValueMember = "CODEID";
            cboMINCL.ShowHeader = false;
            Dictionary<string, object> ParamMINCL = new Dictionary<string, object>();
            ParamMINCL.Add("CODECLASSID", "MINCL");
            ParamMINCL.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtMINCL = SqlExecuter.Query("GetCodeList", "00001", ParamMINCL);
            cboMINCL.DataSource = dtMINCL;

            //MIN PSR 쏠림
            cboMINPSR.DisplayMember = "CODENAME";
            cboMINPSR.ValueMember = "CODEID";
            cboMINPSR.ShowHeader = false;
            Dictionary<string, object> ParamMINPSR = new Dictionary<string, object>();
            ParamMINPSR.Add("CODECLASSID", "MINPSR");
            ParamMINPSR.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtMINPSR = SqlExecuter.Query("GetCodeList", "00001", ParamMINPSR);
            cboMINPSR.DataSource = dtMINPSR;


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
            DataTable dtproductimspec = new DataTable();

            dtproductimspec.Columns.Add("_STATE_");
            dtproductimspec.Columns.Add("MASTERDATACLASSID");
            dtproductimspec.Columns.Add("ITEMID");
            dtproductimspec.Columns.Add("ITEMVERSION");
            dtproductimspec.Columns.Add("ENTERPRISEID");
            dtproductimspec.Columns.Add("IFC");

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
                                dtproductimspec.Columns.Add(controlc.Tag.ToString());
                                break;
                            case "Micube.Framework.SmartControls.SmartComboBox":
                                dtproductimspec.Columns.Add(controlc.Tag.ToString());
                                break;

                            case "Micube.SmartMES.StandardInfo.ucCustomerPopup":
                                dtproductimspec.Columns.Add(controlc.Tag.ToString());
                                break;
                            case "Micube.Framework.SmartControls.SmartDateEdit":
                                dtproductimspec.Columns.Add(controlc.Tag.ToString());
                                break;
                        }
                    }
                }
            }

            DataRow row = dtproductimspec.NewRow();
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
                        }
                    }
                }
            }

            row["MASTERDATACLASSID"] = _MASTERDATACLASSID;
            row["ITEMID"] = _ITEMID;
            row["ITEMVERSION"] = _ITEMVERSION;
            row["ENTERPRISEID"] = _ENTERPRISEID;
            row["_STATE_"] = "modified";
            row["IFC"] = "IFC";

            dtproductimspec.Rows.Add(row);

            ExecuteRule("CircuitConfiguration", dtproductimspec);
            ShowMessage("SuccedSave");
        }

        

        /// <summary>
        /// 조회 클릭 - 메인 grid에 체크 데이터 전달
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Search(string sMASTERDATACLASSID, string sITEMID, string sITEMVERSION, string sENTERPRISEID, string sPLANTID)
        {


            Dictionary<string, object> ParamYpe = new Dictionary<string, object>();
            ParamYpe.Add("MASTERDATACLASSID", sMASTERDATACLASSID);
            ParamYpe.Add("ITEMID", sITEMID);
            ParamYpe.Add("ITEMVERSION", sITEMVERSION);
            ParamYpe.Add("ENTERPRISEID", sENTERPRISEID);

            DataTable dtype = SqlExecuter.Query("GetProductItemSpecYpe", "10001", ParamYpe);
            if(dtype != null)
            {
                if(dtype.Rows.Count !=0)
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
                                        if (dtype.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                        {
                                            controlc.Text = dtype.Rows[0][controlc.Tag.ToString()].ToString();
                                        }
                                        break;
                                    case "Micube.Framework.SmartControls.SmartComboBox":

                                        Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                        combox = (Micube.Framework.SmartControls.SmartComboBox)controlc;

                                        if (dtype.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                        {
                                            combox.EditValue = dtype.Rows[0][controlc.Tag.ToString()].ToString();
                                        }
                                        break;
                                    case "Micube.SmartMES.StandardInfo.ucPublicPopup":
                                        //Micube.SmartMES.StandardInfo.ucPublicPopup ucCp = new ucPublicPopup();
                                        //ucCp = (Micube.SmartMES.StandardInfo.ucPublicPopup)controlc;
                                        //if (dtype.Columns.IndexOf(ucCp.CODE.Tag.ToString()) != -1)
                                        //{
                                        //    ucCp.CODE.Text = dtype.Rows[0][ucCp.CODE.Tag.ToString()].ToString();
                                        //    ucCp.NAME.Text = dtype.Rows[0][ucCp.NAME.Tag.ToString()].ToString();
                                        //}
                                        break;
                                    case "Micube.Framework.SmartControls.SmartDateEdit":
                                        if (dtype.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                        {
                                            controlc.Text = dtype.Rows[0][controlc.Tag.ToString()].ToString();
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

