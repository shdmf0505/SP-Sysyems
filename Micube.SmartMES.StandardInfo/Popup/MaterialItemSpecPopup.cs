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
    /// 프 로 그 램 명  : 기준정보 > 품목관리 > 품목등록(자재)
    /// 업  무  설  명  : 자재품목등록
    /// 생    성    자  :  윤성원
    /// 생    성    일  : 2019-06-28
    /// 수  정  이  력  : 
    /// </summary>
    public partial class MaterialItemSpecPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
        #region Local Variables
        public DataRow CurrentDataRow { get; set; }
        string _sITEMID = "";   //품목id
        string _sITEMVERSION = ""; // 품목버전
        string _sMASTERDATACLASSID = ""; // 품목마스터클래스
        string _sENTERPRISEID = ""; // 회사


		private DataTable _resourceData = new DataTable(); //resource 데이터 Set 변수

		///  선택한 설비 list를 보내기 위한 Handler
		/// </summary>
		/// <param name="dt"></param>
		public delegate void ResultDataHandler(DataTable dt);
		public event ResultDataHandler ResultDataEvent;
		#endregion
        
		#region 생성자
		public MaterialItemSpecPopup()
        {
            InitializeComponent();
            InitializeEvent();
            InitializeCondition();
        }
        public MaterialItemSpecPopup(string sITEMID, string sITEMVERSION, string sMASTERDATACLASSID, string sENTERPRISEID)
        {
            InitializeComponent();
            _sITEMID = sITEMID;
            _sITEMVERSION = sITEMVERSION;
            _sMASTERDATACLASSID = sMASTERDATACLASSID;
            _sENTERPRISEID = sENTERPRISEID;
            InitializeEvent();
            InitializeCondition();
            InitializeGridIdDefinitionManagement();

        }

        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {

            // 품목구분
            cboMATERIALCLASS.DisplayMember = "CODENAME";
            cboMATERIALCLASS.ValueMember = "CODEID";
            cboMATERIALCLASS.ShowHeader = false;
            Dictionary<string, object> ParamRt = new Dictionary<string, object>();
            ParamRt.Add("CODECLASSID", "MaterialClass");
            ParamRt.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtMATERIALCLASS = SqlExecuter.Query("GetCodeList", "00001", ParamRt);
            cboMATERIALCLASS.DataSource = dtMATERIALCLASS;

            // 세부구분
            cboSUBCLASS.DisplayMember = "CODENAME";
            cboSUBCLASS.ValueMember = "CODEID";
            cboSUBCLASS.ShowHeader = false;
            Dictionary<string, object> ParamCPT = new Dictionary<string, object>();
            ParamCPT.Add("CODECLASSID", "SubClass");
            ParamCPT.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtSUBCLASS = SqlExecuter.Query("GetCodeList", "00001", ParamCPT);
            cboSUBCLASS.DataSource = dtSUBCLASS;



            ConditionItemSelectPopup cisiPURCHASEMAN = new ConditionItemSelectPopup();
            cisiPURCHASEMAN.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisiPURCHASEMAN.SetPopupLayout("PURCHASEMAN", PopupButtonStyles.Ok_Cancel);
            cisiPURCHASEMAN.Id = "PURCHASEMAN";
            cisiPURCHASEMAN.LabelText = "PURCHASEMAN";
            cisiPURCHASEMAN.SearchQuery = new SqlQuery("GetUserAreaPerson", "10001");
            cisiPURCHASEMAN.IsMultiGrid = false;
            cisiPURCHASEMAN.DisplayFieldName = "USERNAME";
            cisiPURCHASEMAN.ValueFieldName = "USERID";
            cisiPURCHASEMAN.LanguageKey = "PURCHASEMAN";
            cisiPURCHASEMAN.Conditions.AddTextBox("USERNAME");
            cisiPURCHASEMAN.GridColumns.AddTextBoxColumn("USERID", 150);
            cisiPURCHASEMAN.GridColumns.AddTextBoxColumn("USERNAME", 200);
            sspPURCHASEMAN.SelectPopupCondition = cisiPURCHASEMAN;

            ConditionItemSelectPopup cisidvendorCode = new ConditionItemSelectPopup();
            cisidvendorCode.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisidvendorCode.SetPopupLayout("VENDORCODE", PopupButtonStyles.Ok_Cancel);
            cisidvendorCode.Id = "VENDORCODE";
            cisidvendorCode.LabelText = "VENDORCODE";
            cisidvendorCode.SearchQuery = new SqlQuery("GetVendorList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            cisidvendorCode.IsMultiGrid = false;
            cisidvendorCode.DisplayFieldName = "VENDORNAME";
            cisidvendorCode.ValueFieldName = "VENDORID";
            cisidvendorCode.LanguageKey = "VENDORCODE";
            cisidvendorCode.Conditions.AddTextBox("VENDORID");
            cisidvendorCode.GridColumns.AddTextBoxColumn("VENDORID", 150);
            cisidvendorCode.GridColumns.AddTextBoxColumn("VENDORNAME", 200);
            sspVendorId.SelectPopupCondition = cisidvendorCode;

            //ConditionItemSelectPopup cisidMaker = new ConditionItemSelectPopup();
            //cisidMaker.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            //cisidMaker.SetPopupLayout("VENDORCODE", PopupButtonStyles.Ok_Cancel);
            //cisidMaker.Id = "VENDORCODE";
            //cisidMaker.LabelText = "VENDORCODE";
            //cisidMaker.SearchQuery = new SqlQuery("GetVendorList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            //cisidMaker.IsMultiGrid = false;
            //cisidMaker.DisplayFieldName = "VENDORNAME";
            //cisidMaker.ValueFieldName = "VENDORID";
            //cisidMaker.LanguageKey = "VENDORCODE";
            //cisidMaker.Conditions.AddTextBox("VENDORID");
            //cisidMaker.GridColumns.AddTextBoxColumn("VENDORID", 150);
            //cisidMaker.GridColumns.AddTextBoxColumn("VENDORNAME", 200);
            //sspMaker.SelectPopupCondition = cisidMaker;

            grdMaterialItemSpec.Hide();

        }
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            
        }

		/// <summary>
		/// 확인 클릭 - 메인 grid에 체크 데이터 전달
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSave_Click(object sender, EventArgs e)
		{
            object obj = this.grdMaterialItemSpec.DataSource;
            DataTable dt = (DataTable)obj;

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
                                if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                {
                                    dt.Rows[0][controlc.Tag.ToString()] = controlc.Text;
                                }


                                break;
                            case "Micube.Framework.SmartControls.SmartDateEdit":
                                if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                {
                                    dt.Rows[0][controlc.Tag.ToString()] = controlc.Text;
                                }

                                break;
                            case "Micube.Framework.SmartControls.SmartSelectPopupEdit":
                                Micube.Framework.SmartControls.SmartSelectPopupEdit SelectPopup = new SmartSelectPopupEdit();
                                SelectPopup = (Micube.Framework.SmartControls.SmartSelectPopupEdit)controlc;

                                if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                {
                                    if (dt.Rows[0][controlc.Tag.ToString()].ToString() != controlc.Text)
                                    {
                                        dt.Rows[0][controlc.Tag.ToString()] = SelectPopup.GetValue();
                                    }

                                }
                                break;

                        }
                    }
                }
            }

            
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("MASTERDATACLASSID", _sMASTERDATACLASSID);
            dicParam.Add("ENTERPRISEID", _sENTERPRISEID);
            DataTable dtResult = SqlExecuter.Query("GetItemDescriptionRule", "10001", dicParam);


            // 품목명 중복체크룰
            Dictionary<string, object> dDrParam = new Dictionary<string, object>();
            dDrParam.Add("MASTERDATACLASSID", _sMASTERDATACLASSID);
            dDrParam.Add("ENTERPRISEID", _sENTERPRISEID);
            DataTable dtDr = SqlExecuter.Query("GetItemDuplicateRule", "10001", dDrParam);

            String sAddChk = "";
            foreach (DataRow row in dtDr.DefaultView.ToTable(true, new string[] { "TARGETATTRIBUTE" }).Rows)
            {

                foreach (DataRow rowAdd in dtResult.Select("TARGETATTRIBUTE = '" + row["TARGETATTRIBUTE"].ToString() + "'"))
                {
                    sAddChk = sAddChk + rowAdd["CODEID"].ToString() + rowAdd["SEPARATORCODE"].ToString();
                }

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
                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        if ("CUSTOMERNAME" == controlc.Tag.ToString())
                                        {
                                            sAddChk = sAddChk.Replace(controlc.Tag.ToString(), controlc.Text);
                                        }

                                    }


                                    break;
                                case "Micube.Framework.SmartControls.SmartComboBox":

                                    Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                    combox = (Micube.Framework.SmartControls.SmartComboBox)controlc;

                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        if (combox.GetDataValue() != null)
                                        {
                                            sAddChk = sAddChk.Replace(controlc.Tag.ToString(), combox.GetDataValue().ToString());
                                        }
                                    }

                                    break;

                                case "Micube.SmartMES.StandardInfo.ucCustomerPopup":

                                    //Micube.SmartMES.StandardInfo.ucPublicPopup ucCp = new ucPublicPopup();
                                    //ucCp = (Micube.SmartMES.StandardInfo.ucPublicPopup)controlc;

                                    //if (dt.Columns.IndexOf(ucCp.CODE.Tag.ToString()) != -1)
                                    //{
                                    //    sAddChk = sAddChk.Replace(ucCp.CODE.Tag.ToString(), controlc.Text);
                                    //}

                                    break;

                                case "Micube.Framework.SmartControls.SmartDateEdit":
                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        sAddChk = sAddChk.Replace(controlc.Tag.ToString(), controlc.Text);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            Dictionary<string, object> dDrChkParam = new Dictionary<string, object>();
            dDrChkParam.Add("ENTERPRISEID", _sENTERPRISEID);
            dDrChkParam.Add("ITEMNAME", sAddChk);
            dDrChkParam.Add("ITEMID", _sITEMID);
            dDrChkParam.Add("MASTERDATACLASSID", _sMASTERDATACLASSID);

           
            DataTable dtDrChk = SqlExecuter.Query("GetItemNameDuplicate", "10001", dDrChkParam);
            if (dtDrChk.Rows.Count != 0)
            {
                MSGBox.Show(MessageBoxType.Information, "DuplicateItemName");
            }

            DataTable dtItemMaster = new DataTable();
            dtItemMaster.Columns.Add("ITEMID");
            dtItemMaster.Columns.Add("ITEMVERSION");
            dtItemMaster.Columns.Add("MASTERDATACLASSID");
            dtItemMaster.Columns.Add("_STATE_");

            foreach (DataRow row in dtResult.DefaultView.ToTable(true, new string[] { "TARGETATTRIBUTE" }).Rows)
            {
                dtItemMaster.Columns.Add(row["TARGETATTRIBUTE"].ToString());

                String sAdd = "";
                DataRow rowItemMaster = dtItemMaster.NewRow();
                rowItemMaster["ITEMID"] = _sITEMID;
                rowItemMaster["ITEMVERSION"] = _sITEMVERSION;
                rowItemMaster["MASTERDATACLASSID"] = _sMASTERDATACLASSID;
                rowItemMaster["_STATE_"] = "modified";


                foreach (DataRow rowAdd in dtResult.Select("TARGETATTRIBUTE = '" + row["TARGETATTRIBUTE"].ToString() + "'"))
                {
                    sAdd = sAdd + rowAdd["CODEID"].ToString() + rowAdd["SEPARATORCODE"].ToString();
                }


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
                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        sAdd = sAdd.Replace(controlc.Tag.ToString(), controlc.Text);
                                    }


                                    break;
                                case "Micube.Framework.SmartControls.SmartComboBox":

                                    Micube.Framework.SmartControls.SmartComboBox combox = new SmartComboBox();
                                    combox = (Micube.Framework.SmartControls.SmartComboBox)controlc;

                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        if (combox.GetDataValue() != null)
                                        {
                                            sAdd = sAdd.Replace(controlc.Tag.ToString(), combox.GetDataValue().ToString());
                                        }
                                    }

                                    break;

                              

                                case "Micube.Framework.SmartControls.SmartDateEdit":
                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        sAdd = sAdd.Replace(controlc.Tag.ToString(), controlc.Text);
                                    }
                                    break;
                            }
                        }
                    }
                }

                rowItemMaster[row["TARGETATTRIBUTE"].ToString()] = sAdd;

                dtItemMaster.Rows.Add(rowItemMaster);
            }

            DataTable changed = new DataTable();

            grdMaterialItemSpec.View.CheckValidation();
            changed = grdMaterialItemSpec.GetChangedRows();


            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            MessageWorker saveWorker = new MessageWorker("MaterialItemSpec");
            saveWorker.SetBody(new MessageBody()
                {
                    { "materialItemSpec", changed }
                    ,{ "itemmaster", dtItemMaster }
                });

            saveWorker.Execute();

            MSGBox.Show(MessageBoxType.Information, "SuccedSave");

            dt.AcceptChanges();

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

        

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            // GRID 초기화
            grdMaterialItemSpec.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            //grdProductItemSpec.View.AddTextBoxColumn("ENTERPIRSEID").SetIsHidden();

            grdMaterialItemSpec.View.AddTextBoxColumn("ITEMID", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("ITEMVERSION", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("MASTERDATACLASSID", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("ENTERPRISEID", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("PLANTID", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("MATERIALTYPE", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("MATERIALCLASS", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("SUBCLASS", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("MINORDERQTY", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("PROCUREMENT", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("LMECLASS", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("PURCHASEMAN", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("ORDERPOLICY", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("SAFETYSTOCK", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("RECEIPTWAREHOUSEID", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("RECEIPTLOCATOR", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("RECEIPTROUTE", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("VENDORID", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("MAKER", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("ORIGIN", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("NORMALLEADTIME", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("URGENCYLEADTIME", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("TARIFFRATE", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("BASEBANDRATE", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("DOUBLESINGLESIDED", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("PITHICKNESS", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("THICKNESSCOPPER", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("COPPERSPEC", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("MATERIALWIDTH", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("MATERIALLENGTH", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("EXPIRATIONDATE", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("MAKERECEIPTTYPE", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("COLOR", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("ISCONTAINHALOGEN", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("DESCRIPTION", 200);
            grdMaterialItemSpec.View.AddTextBoxColumn("VALIDSTATE", 200);
            grdMaterialItemSpec.View.PopulateColumns();


            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ITEMID", _sITEMID);
            Param.Add("ITEMVERSION", _sITEMVERSION);
          
            Param.Add("ENTERPRISEID", _sENTERPRISEID);

            DataTable dt = SqlExecuter.Query("GetMateritalitemspec", "10001", Param);
            if (dt.Rows.Count == 0)
            {
                grdMaterialItemSpec.View.AddNewRow();

                object objNew = this.grdMaterialItemSpec.DataSource;
                DataTable dtNew = (DataTable)objNew;

                dtNew.Rows[0]["ITEMID"] = _sITEMID;
                dtNew.Rows[0]["ITEMVERSION"] = _sITEMVERSION;
                dtNew.Rows[0]["MASTERDATACLASSID"] = _sMASTERDATACLASSID;
                dtNew.Rows[0]["ENTERPRISEID"] = _sENTERPRISEID;
            }
            else
            {
                grdMaterialItemSpec.DataSource = dt;
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
                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        controlc.Text = dt.Rows[0][controlc.Tag.ToString()].ToString();
                                    }
                                    break;
                                case "Micube.Framework.SmartControls.SmartComboBox":

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
                                            SelectPopup.Text = dt.Rows[0][controlc.Tag.ToString().Replace("ID", "NAME")].ToString();
                                        }
                                        else
                                        {
                                            SelectPopup.Text = dt.Rows[0][controlc.Tag.ToString() + "NAME"].ToString();
                                        }
                                        //txtCustomerName.Text = dt.Rows[0]["CUSTOMERNAME"].ToString();
                                    }
                                    break;
                                case "Micube.Framework.SmartControls.SmartDateEdit":
                                    if (dt.Columns.IndexOf(controlc.Tag.ToString()) != -1)
                                    {
                                        controlc.Text = dt.Rows[0][controlc.Tag.ToString()].ToString();
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }
        #endregion


    }
}
