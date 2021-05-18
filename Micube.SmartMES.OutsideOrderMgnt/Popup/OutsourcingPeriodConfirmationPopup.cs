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

namespace Micube.SmartMES.OutsideOrderMgnt.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 
    /// 업  무  설  명  : 
    /// 생    성    자  : 최별
    /// 생    성    일  : 2019-07-05
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcingPeriodConfirmationPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        public DataRow CurrentDataRow { get; set; }
        #region Local Variables
        
        string _sPlantid = "";
        string _Functionid = "";
        #endregion

        #region 생성자
        public OutsourcingPeriodConfirmationPopup()
        {
            InitializeComponent();

            InitializeEvent();

            InitializeCondition();
            InitializeComboBox();
            _sPlantid = UserInfo.Current.Plant.ToString();
            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
            {
                lblAreaid.Visible = false;
                PopupAreaid.Visible = false;
            }
            else
            {
                PopupVendorid.Enabled = false;
            }
            selectOspPeriodidPopup(_sPlantid, "OutSourcing");
            selectOspAreaidPopup(_sPlantid);
            selectOspVendoridPopup(_sPlantid);
        }
      

        #endregion
        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
        

            cboPERIODTYPEOSP.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboPERIODTYPEOSP.ValueMember = "CODEID";
            cboPERIODTYPEOSP.DisplayMember = "CODENAME";
            cboPERIODTYPEOSP.EditValue = "OutSourcing";
            cboPERIODTYPEOSP.DataSource = SqlExecuter.Query("GetCodeList", "00001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "PeriodTypeOSP" } });
            cboPERIODTYPEOSP.ShowHeader = false;
            //OSPAmountType
            cboOSPAMOUNTTYPE.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboOSPAMOUNTTYPE.ValueMember = "CODEID";
            cboOSPAMOUNTTYPE.DisplayMember = "CODENAME";
            cboOSPAMOUNTTYPE.EditValue = "Total";
            cboOSPAMOUNTTYPE.DataSource = SqlExecuter.Query("GetCodeList", "00001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "OSPAmountType" } });
            cboOSPAMOUNTTYPE.ShowHeader = false;

        }

        #endregion
        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {
            // 계획년월
            DateTime dateNow = DateTime.Now;
            dtpStartDate.EditValue = dateNow.ToString("yyyy-MM-01");
            dtpEndDate.EditValue = dateNow.ToString("yyyy-MM-dd");

        }
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
           
            // 닫기
            btnClose.Click += BtnClose_Click;
            btnCloseProcess.Click += BtnCloseProcess_Click;
            btnCloseCancel.Click += BtnCloseCancel_Click;
            btnOspworkstatus.Click += BtnOspworkstatus_Click;
          
            cboPERIODTYPEOSP.EditValueChanged += cboPERIODTYPEOSP_EditValueChanged;
        }
        private void cboPERIODTYPEOSP_EditValueChanged(object sender, EventArgs e)
        {
            _sPlantid = UserInfo.Current.Plant.ToString();
            string strPeriodtype = cboPERIODTYPEOSP.EditValue.ToString();
            selectOspPeriodidPopup(_sPlantid, strPeriodtype);
            
                PopupPeriodid.SetValue("");
            PopupPeriodid.EditValue = "";
        }
        private void BtnCloseProcess_Click(object sender, EventArgs e)
        {
           
            if (PopupPeriodid.GetValue().ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblCLOSEYM.Text); //메세지 
                PopupPeriodid.Focus();
                return ;
            }
            if (!(txtPeriodstate.Text.Equals("Open")))
            {
                //다국어 처리 (마감되어 있는 월입니다. 마감 작업이 불가능합니다)
                this.ShowMessage("InValidOspData012");
                return;
            }
            if (dtpStartDate.Text.ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblEXPSETTLEDATE.Text); //메세지 
                return;

            }
            if (dtpEndDate.Text.ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblEXPSETTLEDATE.Text); //메세지 
                return;

            }
            DateTime StartDate = Convert.ToDateTime(dtpStartDate.Text);
            DateTime EndDate = Convert.ToDateTime(dtpEndDate.Text);
            if (StartDate > EndDate)
            {
                // 다국어 메세지 처리 (종료일 보다 시작일이 자료가 존재합니다.)

                this.ShowMessage(MessageBoxButtons.OK, "InValidCsmData006");
                return;
            }
            string strParametervalue = "";

            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnCloseProcess.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnCloseProcess.Enabled = false;
                    btnClose.Enabled = false;
                    btnCloseCancel.Enabled = false;
                    DataTable dtSave = createSaveDatatable();
                    DataRow dr = dtSave.NewRow();
                    string strStartdate = "";
                    string strEnddate = "";
                    string strDateFormat = "";
                    strDateFormat = "yyyy-MM-dd";
                    if (!dtpStartDate.Text.Equals(""))
                    {
                        DateTime dtStartdat = Convert.ToDateTime(dtpStartDate.Text.ToString());
                        strStartdate = dtStartdat.ToString(strDateFormat);
                    }
                    if (!dtpEndDate.Text.Equals(""))
                    {

                        DateTime dtEnddat = Convert.ToDateTime(dtpEndDate.Text.ToString());
                        strEnddate = dtEnddat.AddDays(0).ToString(strDateFormat);
                    }
                    dr["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
                    dr["PLANTID"] = UserInfo.Current.Plant.ToString();
                    dr["EXPSETTLEDATE_PERIODFR"] = strStartdate;
                    dr["EXPSETTLEDATE_PERIODTO"] = strEnddate;
                    dr["PERIODTYPEOSP"] = cboPERIODTYPEOSP.EditValue;
                    dr["PERIODID"] = PopupPeriodid.GetValue().ToString();
                    dr["OSPAMOUNTTYPE"] = cboOSPAMOUNTTYPE.EditValue;
                    dr["VENDORID"] =PopupVendorid.GetValue().ToString();
                    dr["AREAID"] = PopupAreaid.GetValue().ToString();
                    dr["ISCLOSE"] = "Y";
                    dr["USERID"] = UserInfo.Current.Id.ToString();
                    dr["FUNCTIONID"] = "OSPCloseBatch";
                    strParametervalue = strParametervalue + "Plantid :" + UserInfo.Current.Plant.ToString() +" ,";
                    if (!strStartdate.Equals(""))
                    {
                        strParametervalue = strParametervalue + lblEXPSETTLEDATE.Text +" :" + strStartdate + " ~ " + strEnddate + " ,";
                    }
                    strParametervalue = strParametervalue + lblPERIODTYPEOSP.Text + " :" + cboPERIODTYPEOSP.Text + " ,";
                    strParametervalue = strParametervalue + lblCLOSEYM.Text + " :" + PopupPeriodid.Text + " ,";
                    strParametervalue = strParametervalue + lblOSPAMOUNTTYPE.Text + " :" + cboOSPAMOUNTTYPE.Text + " ,";
                    if (!(PopupVendorid.Text.Equals("")))
                    {
                        strParametervalue = strParametervalue + lblVendorid.Text + " :" + PopupVendorid.Text + " ,";
                    }
                    if (!(PopupAreaid.Text.Equals("")))
                    {
                        strParametervalue = strParametervalue + lblAreaid.Text + " :" + PopupAreaid.Text + " ,";
                    }

                   
                    dr["PARAMETERVALUE"] =  strParametervalue;
                    dr["_STATE_"] = "added";


                    dtSave.Rows.Add(dr);
                   
                    DataTable saveResult = this.ExecuteRule<DataTable>("OspConCurRequestSave", dtSave);
                    DataRow resultData = saveResult.Rows[0];
                    string strtempRequestno = resultData.ItemArray[0].ToString();
                    lblrequsetno.Visible = true;
                    txtrequsetno.Visible = true;
                    txtrequsetno.EditValue = strtempRequestno;

                    dr["REQUESTNO"] = strtempRequestno;
                    _Functionid = "";
                    MessageWorker worker = new MessageWorker("OutsourcingPeriodConfirmationPopup");
                    worker.SetBody(new MessageBody()
                        {
                            { "list", dtSave }
                        });
                    worker.ExecuteNoResponse();
                    // this.ExecuteRuleAsync("OutsourcingPeriodConfirmationPopup", dtSave);

                    ShowMessage("SuccessOspRequest");

                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    btnCloseProcess.Enabled = true;
                    btnClose.Enabled = true;
                    btnCloseCancel.Enabled = true;

                }
            }
        }

        private void BtnCloseCancel_Click(object sender, EventArgs e)
        {
            if (PopupPeriodid.GetValue().ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblCLOSEYM.Text); //메세지 
                PopupPeriodid.Focus();
                return;
            }
            if (!(txtPeriodstate.Text.Equals("Open")))
            {
                //다국어 처리 (마감되어 있는 월입니다. 마감 작업이 불가능합니다)
                this.ShowMessage("InValidOspData012");
                return;
            }
            if (dtpStartDate.Text.ToString().Equals("") && !(dtpEndDate.Text.ToString().Equals("")))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblEXPSETTLEDATE.Text); //메세지 
                return;

            }
            if (dtpEndDate.Text.ToString().Equals("") && !(dtpStartDate.Text.ToString().Equals("")))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblEXPSETTLEDATE.Text); //메세지 
                return;

            }
            string strParametervalue = "";

            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnCloseCancel.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnCloseProcess.Enabled = false;
                    btnClose.Enabled = false;
                    btnCloseCancel.Enabled = false;
                    string strStartdate = "";
                    string strEnddate = "";
                    string strDateFormat = "";
                    strDateFormat = "yyyy-MM-dd";
                    if (!dtpStartDate.Text.Equals(""))
                    {
                        DateTime dtStartdat = Convert.ToDateTime(dtpStartDate.Text.ToString());
                        strStartdate = dtStartdat.ToString(strDateFormat);
                    }
                    if (!dtpEndDate.Text.Equals(""))
                    {

                        DateTime dtEnddat = Convert.ToDateTime(dtpEndDate.Text.ToString());
                        //strEnddate = dtEnddat.AddDays(1).ToString(strDateFormat);
                        strEnddate = dtEnddat.ToString(strDateFormat);
                    }
                    DataTable dtSave = createSaveDatatable();
                    DataRow dr = dtSave.NewRow();
                    
                    dr["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
                    dr["PLANTID"] = UserInfo.Current.Plant.ToString();
                    dr["EXPSETTLEDATE_PERIODFR"] = strStartdate;
                    dr["EXPSETTLEDATE_PERIODTO"] = strEnddate;
                    dr["PERIODTYPEOSP"] = cboPERIODTYPEOSP.EditValue;
                    dr["PERIODID"] = PopupPeriodid.GetValue().ToString();
                    dr["OSPAMOUNTTYPE"] = cboOSPAMOUNTTYPE.EditValue;
                    dr["VENDORID"] = PopupVendorid.GetValue().ToString();
                    dr["AREAID"] = PopupAreaid.GetValue().ToString();
                    dr["ISCLOSE"] = "N";
                    dr["FUNCTIONID"] = "OSPCloseCancelBatch";
                    strParametervalue = strParametervalue + "Plantid :" + UserInfo.Current.Plant.ToString() + " ,";
                    if (!strStartdate.Equals(""))
                    {
                        strParametervalue = strParametervalue + lblEXPSETTLEDATE.Text + " :" + strStartdate + " ~ " + strEnddate + " ,";
                    }
                    strParametervalue = strParametervalue + lblPERIODTYPEOSP.Text + " :" + cboPERIODTYPEOSP.Text + " ,";
                    strParametervalue = strParametervalue + lblCLOSEYM.Text + " :" + PopupPeriodid.Text + " ,";
                    strParametervalue = strParametervalue + lblOSPAMOUNTTYPE.Text + " :" + cboOSPAMOUNTTYPE.Text + " ,";
                  
                    if (!(PopupVendorid.Text.Equals("")))
                    {
                        strParametervalue = strParametervalue + lblVendorid.Text + " :" + PopupVendorid.Text + " ,";
                    }
                    if (!(PopupAreaid.Text.Equals("")))
                    {
                        strParametervalue = strParametervalue + lblAreaid.Text + " :" + PopupAreaid.Text + " ,";
                    }
                     dr["PARAMETERVALUE"] = strParametervalue;
                   

                    dr["USERID"] = UserInfo.Current.Id.ToString();

                    dr["_STATE_"] = "added";


                    dtSave.Rows.Add(dr);
                    DataTable saveResult = this.ExecuteRule<DataTable>("OspConCurRequestSave", dtSave);
                    DataRow resultData = saveResult.Rows[0];
                    string strtempRequestno = resultData.ItemArray[0].ToString();
                    lblrequsetno.Visible = true;
                    txtrequsetno.Visible = true;
                    txtrequsetno.EditValue = strtempRequestno;
                    _Functionid = "";
                    dr["REQUESTNO"] = strtempRequestno;

                    //this.ExecuteRuleAsync("OutsourcingPeriodConfirmationPopup", dtSave);
                    MessageWorker worker = new MessageWorker("OutsourcingPeriodConfirmationPopup");
                    worker.SetBody(new MessageBody()
                        {
                            { "list", dtSave }
                        });
                    worker.ExecuteNoResponse();
                    ShowMessage("SuccessOspRequest");

                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    btnCloseProcess.Enabled = true;
                    btnClose.Enabled = true;
                    btnCloseCancel.Enabled = true;

                }
            }
        }
        
        private void BtnOspworkstatus_Click(object sender, EventArgs e)
        {

          
            OspConCurRequestPopup itemPopup = new OspConCurRequestPopup(txtrequsetno.Text, _Functionid);
            itemPopup.ShowDialog(this);
        }

        /// <summary>
        /// 닫기 클릭시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {

            this.Close();
        }


        private void selectOspPeriodidPopup(string sPlantid , string strPeriodtype)
        {

            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(520, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("PERIODNAME", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "PERIODNAME";
            popup.LabelText = "PERIODNAME";
            popup.SearchQuery = new SqlQuery("GetPeriodidListByOsp", "10001", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                        , $"USERID={UserInfo.Current.Id}"
                                                                        , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                        , $"P_PLANTID={sPlantid}"
                                                                         , $"P_PERIODTYPE={strPeriodtype}");
            popup.IsMultiGrid = false;
            popup.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                foreach (DataRow row in selectedRow)
                {
                    if (selectedRow.Count() > 0)
                    {
                        txtPeriodstate.Text = row["PERIODSTATE"].ToString();

                    }

                }
            });
            popup.DisplayFieldName = "PERIODNAME";
            popup.ValueFieldName = "PERIODID";
            popup.LanguageKey = "PERIODID";

            popup.Conditions.AddTextBox("P_PERIODNAME")
                .SetLabel("PERIODNAME");
            popup.GridColumns.AddTextBoxColumn("PERIODID", 120)
                .SetLabel("PERIODID")
                .SetIsHidden();
            popup.GridColumns.AddTextBoxColumn("PERIODNAME", 120)
               .SetLabel("PERIODNAME");
            popup.GridColumns.AddTextBoxColumn("PERIODSTATE", 200)
                .SetLabel("PERIODSTATE");

            PopupPeriodid.SelectPopupCondition = popup;
        }
        /// <summary>
        /// 작업장 
        /// </summary>
        /// <param name="sPlantid"></param>
        private void selectOspAreaidPopup(string sPlantid)
        {

            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(820, 700, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "AREANAME";
            popup.LabelText = "AREANAME";
            popup.SearchQuery = new SqlQuery("GetAreaidPopupListByOsp", "10001", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                           , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                           , $"P_PLANTID={sPlantid}");
            popup.IsMultiGrid = false;

            popup.DisplayFieldName = "AREANAME";
            popup.ValueFieldName = "AREAID";
            popup.LanguageKey = "AREAID";
            popup.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //작업장 변경 시 작업자 조회
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    PopupVendorid.SetValue(row["AREAID"].ToString());
                    PopupVendorid.Text = row["AREANAME"].ToString();
                    PopupVendorid.EditValue = row["AREANAME"].ToString();
                });

            });

            popup.Conditions.AddTextBox("P_AREANAME")
                .SetLabel("AREANAME");
            popup.GridColumns.AddTextBoxColumn("AREAID", 120)
                .SetLabel("AREAID");
            popup.GridColumns.AddTextBoxColumn("AREANAME", 200)
                .SetLabel("AREANAME");
            popup.GridColumns.AddTextBoxColumn("OSPVENDORID", 120)
                .SetLabel("OSPVENDORID");
            popup.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200)
                .SetLabel("OSPVENDORNAME");
            PopupAreaid.SelectPopupCondition = popup;
        }
        private void selectOspVendoridPopup(string sPlantid)
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(550, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("OSPVENDORID", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "OSPVENDORID";
            popup.LabelText = "OSPVENDORID";
            popup.SearchQuery = new SqlQuery("GetVendorListAuthorityByOsp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                , $"USERID={UserInfo.Current.Id}"
                , $"P_PLANTID={sPlantid}");
            popup.IsMultiGrid = false;

            popup.DisplayFieldName = "OSPVENDORNAME";
            popup.ValueFieldName = "OSPVENDORID";
            popup.LanguageKey = "OSPVENDORID";

            popup.Conditions.AddTextBox("OSPVENDORNAME");

            popup.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetLabel("OSPVENDORID");
            popup.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200)
                .SetLabel("OSPVENDORNAME");

            PopupVendorid.SelectPopupCondition = popup;
        }
        #endregion

        /// <summary>
        /// 저장시 기본테이블 생성
        /// </summary>
        /// <returns></returns>
        private DataTable createSaveDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "list";
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("PERIODTYPEOSP");
            dt.Columns.Add("EXPSETTLEDATE_PERIODFR");
            dt.Columns.Add("EXPSETTLEDATE_PERIODTO");
            dt.Columns.Add("PERIODID");
            dt.Columns.Add("OSPAMOUNTTYPE");
            dt.Columns.Add("VENDORID");
            dt.Columns.Add("AREAID");
            dt.Columns.Add("USERID");
            dt.Columns.Add("ISCLOSE");
            dt.Columns.Add("REQUESTNO");
            dt.Columns.Add("FUNCTIONID");
            dt.Columns.Add("PARAMETERVALUE");
            dt.Columns.Add("_STATE_");
            return dt;
        }
    }
}
