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
    public partial class OutsideFinishingEexpensePopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        public DataRow CurrentDataRow { get; set; }
        #region Local Variables

        string _sPlantid = "";

        #endregion

        #region 생성자
        public OutsideFinishingEexpensePopup()
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
            //else
            //{
            //    PopupVendorid.Enabled = false;
            //}
            selectOspVendoridPopup(_sPlantid);
            selectOspAreaidPopup(_sPlantid);
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
            cboISERROR.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboISERROR.ValueMember = "CODEID";
            cboISERROR.DisplayMember = "CODENAME";

            cboISERROR.DataSource = SqlExecuter.Query("GetCodeList", "00001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "YesNo" } });
            cboISERROR.ShowHeader = false;

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
            btnCloseProcess.Click += BtnCloseProcess_Click; ;

            btnOspworkstatus.Click += BtnOspworkstatus_Click;
            popupProcesssegmentclassid.EditValueChanged += popupProcesssegmentclassid_EditValueChanged;
            selectProcesssegmentclassid();
            selectProcesssegmentidPopup("");
        }
        /// <summary>
        /// Areaid 제한 로직 추가 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void popupProcesssegmentclassid_EditValueChanged(object sender, EventArgs e)
        {


            if (popupProcesssegmentclassid.EditValue.ToString().Equals(""))
            {
                selectProcesssegmentidPopup("");
                return;
            }
            selectProcesssegmentidPopup(popupProcesssegmentclassid.GetValue().ToString());
        }



        private void BtnCloseProcess_Click(object sender, EventArgs e)
        {

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

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnCloseProcess.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnCloseProcess.Enabled = false;
                    btnClose.Enabled = false;

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
                        // strEnddate = dtEnddat.AddDays(1).ToString(strDateFormat);
                        strEnddate = dtEnddat.ToString(strDateFormat);
                    }
                    DataTable dtSave = createSaveDatatable();
                    DataRow dr = dtSave.NewRow();

                    dr["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
                    dr["PLANTID"] = UserInfo.Current.Plant.ToString();
                    dr["EXPSETTLEDATE_PERIODFR"] = strStartdate;
                    dr["EXPSETTLEDATE_PERIODTO"] = strEnddate;
                    dr["PERIODTYPEOSP"] = cboPERIODTYPEOSP.EditValue;
                    dr["VENDORID"] = PopupVendorid.GetValue().ToString();
                    dr["AREAID"] = PopupAreaid.GetValue().ToString();
                    dr["PROCESSSEGMENTCLASSID"] = popupProcesssegmentclassid.GetValue().ToString();
                    dr["PROCESSSEGMENTID"] = popupProcesssegmentid.GetValue().ToString();
                    dr["PRODUCTDEFID"] = Productdefid.CODE.EditValue;
                    dr["PRODUCTDEFVERSION"] = Productdefid.VERSION.EditValue;
                    dr["ISERROR"] = cboISERROR.EditValue;
                    dr["USERID"] = UserInfo.Current.Id.ToString();
                    dr["FUNCTIONID"] = "OSPReCalculate";
                    strParametervalue = strParametervalue + "Plantid :" + UserInfo.Current.Plant.ToString() + " ,";
                    if (!strStartdate.Equals(""))
                    {
                        strParametervalue = strParametervalue + lblEXPSETTLEDATE.Text + " :" + strStartdate + " ~ " + strEnddate + " ,";
                    }
                    strParametervalue = strParametervalue + lblPERIODTYPEOSP.Text + " :" + cboPERIODTYPEOSP.Text + " ,";

                    if (!(PopupAreaid.Text.Equals("")))
                    {
                        strParametervalue = strParametervalue + lblVendorid.Text + " :" + PopupVendorid.Text + " ,";
                    }
                    if (!(PopupAreaid.Text.Equals("")))
                    {
                        strParametervalue = strParametervalue + lblAreaid.Text + " :" + PopupAreaid.Text + " ,";
                    }
                    if (!(popupProcesssegmentclassid.Text.Equals("")))
                    {
                        strParametervalue = strParametervalue + lblProcesssegmentclassid.Text + " :" + popupProcesssegmentclassid.Text + " ,";
                    }
                    if (!(popupProcesssegmentid.Text.Equals("")))
                    {
                        strParametervalue = strParametervalue + lblProcesssegmentid.Text + " :" + popupProcesssegmentid.Text + " ,";
                    }
                    if (!(Productdefid.CODE.Text.Equals("")))
                    {
                        strParametervalue = strParametervalue + lblProductdefid.Text + " :" + Productdefid.CODE.EditValue + " ,";
                        strParametervalue = strParametervalue + "Ver" + " :" + Productdefid.VERSION.EditValue + " ,";
                        strParametervalue = strParametervalue + "Name" + " :" + Productdefid.NAME.EditValue + " ,";
                    }
                    strParametervalue = strParametervalue + lblISERROR.Text + " :" + cboISERROR.Text + " ,";
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
                    dr["REQUESTNO"] = strtempRequestno;

                    MessageWorker worker = new MessageWorker("OutsideFinishingEexpensePopup");
                    worker.SetBody(new MessageBody()
                        {
                            { "list", dtSave }
                        });
                    //worker.ExecuteWithTimeout(300);
                    //this.ExecuteRuleAsync("OutsideFinishingEexpensePopup", dtSave);
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


                }
            }
        }

        private void BtnOspworkstatus_Click(object sender, EventArgs e)
        {

           
            OspConCurRequestPopup itemPopup = new OspConCurRequestPopup(txtrequsetno.Text, "OSPReCalculate");
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
        /// <summary>
        /// 중공정 설정 
        /// </summary>
        private void selectProcesssegmentclassid()
        {

            // 팝업 컬럼설정
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(500, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("MIDDLEPROCESSSEGMENTCLASS", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "MIDDLEPROCESSSEGMENTCLASS";
            popup.LabelText = "MIDDLEPROCESSSEGMENTCLASS";
            popup.SearchQuery = new SqlQuery("GetProcesssegmentclassidListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                                 , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                 );
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "MIDDLEPROCESSSEGMENTCLASSNAME";

            popup.ValueFieldName = "MIDDLEPROCESSSEGMENTCLASSID";
            popup.LanguageKey = "MIDDLEPROCESSSEGMENTCLASSID";

            popup.Conditions.AddTextBox("MIDDLEPROCESSSEGMENTCLASSNAME")
                .SetLabel("MIDDLEPROCESSSEGMENTCLASSNAME");

            popup.GridColumns.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSID", 150)
                .SetLabel("MIDDLEPROCESSSEGMENTCLASSID");
            popup.GridColumns.AddTextBoxColumn("MIDDLEPROCESSSEGMENTCLASSNAME", 200)
                .SetLabel("MIDDLEPROCESSSEGMENTCLASSNAME");

            popupProcesssegmentclassid.SelectPopupCondition = popup;

        }
        /// <summary>
        /// 공정 선택팝업
        /// </summary>
        private void selectProcesssegmentidPopup(string strprocesssegmentclassid)
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(500, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "PROCESSSEGMENTID";
            popup.LabelText = "PROCESSSEGMENTID";
            popup.SearchQuery = new SqlQuery("GetProcessSegmentListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                                  , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                  , $"P_PROCESSSEGMENTCLASSID={strprocesssegmentclassid}"
                                                                                  );
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "PROCESSSEGMENTNAME";

            popup.ValueFieldName = "PROCESSSEGMENTID";
            popup.LanguageKey = "PROCESSSEGMENTID";

            popup.Conditions.AddTextBox("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT");

            popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetLabel("PROCESSSEGMENTID");
            popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200)
                .SetLabel("PROCESSSEGMENTNAME");

            popupProcesssegmentid.SelectPopupCondition = popup;
        }


        // 공정 
        //중공정??

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

            dt.Columns.Add("EXPSETTLEDATE_PERIODFR");
            dt.Columns.Add("EXPSETTLEDATE_PERIODTO");
            dt.Columns.Add("PERIODTYPEOSP");
            dt.Columns.Add("VENDORID");
            dt.Columns.Add("AREAID");
            dt.Columns.Add("PROCESSSEGMENTCLASSID");
            dt.Columns.Add("PROCESSSEGMENTID");
            dt.Columns.Add("PRODUCTDEFID");
            dt.Columns.Add("PRODUCTDEFVERSION");
            dt.Columns.Add("ISERROR");
            dt.Columns.Add("USERID");

            dt.Columns.Add("REQUESTNO");
            dt.Columns.Add("FUNCTIONID");
            dt.Columns.Add("PARAMETERVALUE");
            dt.Columns.Add("_STATE_");
            return dt;
        }
    }
}
