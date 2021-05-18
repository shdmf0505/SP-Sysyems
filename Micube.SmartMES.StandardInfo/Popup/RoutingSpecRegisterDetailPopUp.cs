#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
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
    /// 프 로 그 램 명  : 기준정보 > 품질 기준 정보 > SPEC 등록 화면내의 팝업
    /// 업  무  설  명  : SPEC 등록 화면의 그리드를 더블 클릭하여 상세 스펙을 관리한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-05-14
    /// 수  정  이  력  : 윤성원 2019-07-05 using 에 #region #endregion 추가
    /// 
    /// 
    /// </summary>
    public partial class RoutingSpecRegisterDetailPopUp : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables
        public DataRow CurrentDataRow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        DataTable _specTable;//Spec정보가 수정되었는지 비교하기 위한 기존 데이터 테이블

        public DataRow _row;



        #endregion

        #region 생성자
        public RoutingSpecRegisterDetailPopUp(DataRow row)
        {
            InitializeComponent();
            InitializeEvent();

            _row = row;

        }
        #endregion

        #region 컨텐츠 영역 초기화


        /// <summary>
        /// chartType 콤보박스 초기화 함수
        /// </summary>
        private void InitializeComboBox()
        {
            SqlQuery chartTypeList = new SqlQuery("GetTypeList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=ControlType");
            DataTable chartTypeTable = chartTypeList.Execute();
            this.cboChartType.Properties.DataSource = chartTypeTable;
            this.cboChartType.Properties.ValueMember = "CODEID";
            this.cboChartType.Properties.DisplayMember = "CODENAME";
            this.cboChartType.Properties.ShowHeader = false;
            this.cboChartType.EditValue = "XBARR"; // 공정스펙은 XBARR가 필수이다. 

        }

        
        #endregion

        #region Event
        private void InitializeEvent()
        {
            this.Load += SpecRegisterDetailPopUp_Load;
            this.btnCancel.Click += BtnCancel_Click;
            this.btnSearch.Click += BtnSearch_Click;
            this.btnSave.Click += BtnSave_Click;
            this.btnDelete.Click += BtnDelete_Click;
            //this.cboChartType.EditValueChanged += CboChartType_EditValueChanged;
           
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {

            Dictionary<string, object> ParamInspectionddef = new Dictionary<string, object>();
            ParamInspectionddef.Add("INSPECTIONDEFID", _row["INSPECTIONDEFID"].ToString());
            ParamInspectionddef.Add("INSPITEMID", _row["INSPITEMID"].ToString());
            ParamInspectionddef.Add("PRODUCTDEFID", _row["RESOURCEID"].ToString());
            ParamInspectionddef.Add("PRODUCTDEFVERSION", _row["RESOURCEVERSION"].ToString());
            ParamInspectionddef.Add("PROCESSSEGMENTID", _row["PROCESSSEGMENTID"].ToString());

            DataTable dtInspectionddef = SqlExecuter.Query("GetInspectionItemResultChk", "10001", ParamInspectionddef);
            if(dtInspectionddef != null)
            {
                if(dtInspectionddef.Rows.Count !=0)
                {
                    ShowMessage("InspectionItemresultCheck");
                    return;
                }
            }


            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "SpecRegisterDeleteNote");

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnDelete.Enabled = false;
                    this.DeleteSaveData();
                    this.DialogResult = DialogResult.OK;
                    ShowMessage("SuccessSave");

                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {

                    this.CloseWaitArea();
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = true;

                    //OnSearchClick();
                    this.Close();
                }
            }
        }

        //private void CboChartType_EditValueChanged(object sender, EventArgs e)
        //{
        //    btnSearch.Enabled = true;
        //}

        private void SpecRegisterDetailPopUp_Load(object sender, EventArgs e)
        {
            InitializeComboBox();
            SetTxtControlFormat();
            //this.txtSpecName.EditValue = "";// _inspectionName;
            //EnableInput(false);
            OnSearchClick();
            //this.txtSpecName.Enabled = false;
        }

        /// <summary>
        /// 검색버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            OnSearchClick();
        }

        /// <summary>
        /// 저장 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "SpecRegisterConfirmNote");

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;

                    this.SaveData();
                    this.DialogResult = DialogResult.OK;
                    ShowMessage("SuccessSave");

                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    
                    this.CloseWaitArea();
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    //OnSearchClick();
                    this.Close();
                }
            }

        }

        /// <summary>
        /// 저장 할 때 기존의 Spec 데이터와 입력된 정보를 비교한 후
        /// 변경사항이 있을 때만 저장하는 함수
        /// </summary>
        /// <param name="row"></param>
        private void SaveData()
        {
            DataTable dtINSPECTIONITEMREL = new DataTable();

            dtINSPECTIONITEMREL.Columns.Add("SPECSEQUENCE");
            dtINSPECTIONITEMREL.Columns.Add("INSPITEMID");
            dtINSPECTIONITEMREL.Columns.Add("INSPITEMVERSION");
            dtINSPECTIONITEMREL.Columns.Add("INSPECTIONDEFID");
            dtINSPECTIONITEMREL.Columns.Add("INSPECTIONDEFVERSION");
            dtINSPECTIONITEMREL.Columns.Add("RESOURCETYPE");
            dtINSPECTIONITEMREL.Columns.Add("RESOURCEID");
            dtINSPECTIONITEMREL.Columns.Add("RESOURCEVERSION");
          //  dtINSPECTIONITEMREL.Columns.Add("INSPITEMCLASSID");
            dtINSPECTIONITEMREL.Columns.Add("ENTERPRISEID");
            dtINSPECTIONITEMREL.Columns.Add("PLANTID");
            dtINSPECTIONITEMREL.Columns.Add("UNIT");
            dtINSPECTIONITEMREL.Columns.Add("VALIDSTATE");
            dtINSPECTIONITEMREL.Columns.Add("_STATE_");

           // dtINSPECTIONITEMREL.Columns.Add("PROCESSSEGMENTTYPE");
            dtINSPECTIONITEMREL.Columns.Add("PROCESSSEGID");
            dtINSPECTIONITEMREL.Columns.Add("PROCESSEGVERSION");
            dtINSPECTIONITEMREL.Columns.Add("SPECCLASSID");

            DataTable dtSPECDEFINITION = new DataTable();
            dtSPECDEFINITION.Columns.Add("SPECSEQUENCE");
            dtSPECDEFINITION.Columns.Add("SPECCLASSID");
            dtSPECDEFINITION.Columns.Add("ENTERPRISEID");
            dtSPECDEFINITION.Columns.Add("PLANTID");
            dtSPECDEFINITION.Columns.Add("PROCESSSEGMENTCLASSID");
            dtSPECDEFINITION.Columns.Add("PROCESSSEGMENTID");
            dtSPECDEFINITION.Columns.Add("EQUIPMENTID");
            dtSPECDEFINITION.Columns.Add("CHILDEQUIPMENTID");
            dtSPECDEFINITION.Columns.Add("PRODUCTDEFID");
            dtSPECDEFINITION.Columns.Add("CONSUMABLEDEFID");
            dtSPECDEFINITION.Columns.Add("CUSTOMERID");
            dtSPECDEFINITION.Columns.Add("VENDORID");
            dtSPECDEFINITION.Columns.Add("INSPITEMID");
            dtSPECDEFINITION.Columns.Add("WORKTYPE");
            dtSPECDEFINITION.Columns.Add("WORKCONDITION");
            dtSPECDEFINITION.Columns.Add("RESOURCEID");
            dtSPECDEFINITION.Columns.Add("SPECVERSION");
            dtSPECDEFINITION.Columns.Add("UOMDEFID");

            dtSPECDEFINITION.Columns.Add("DEFAULTCHARTTYPE");
            dtSPECDEFINITION.Columns.Add("DESCRIPTION");
            dtSPECDEFINITION.Columns.Add("VALIDSTATE");
            dtSPECDEFINITION.Columns.Add("_STATE_");

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("RESOURCEID", _row["RESOURCEID"]);
            param.Add("RESOURCEVERSION", _row["RESOURCEVERSION"]);
            param.Add("INSPITEMID", _row["INSPITEMID"]);
            param.Add("INSPECTIONDEFID", _row["INSPECTIONDEFID"]);
            //param.Add("INSPITEMCLASSID", _row["INSPITEMCLASSID"]);
            param.Add("PROCESSSEGMENTID", _row["PROCESSSEGMENTID"]);
            param.Add("CONTROLTYPE", "XBARR"); // 공정스펙 XBARR 는 필수데이터 입니다.
            DataTable specXBARRTable = SqlExecuter.Query("GetSpecDetail", "10001", param);


            GetNumber number = new GetNumber();
            DataTable dtDate = SqlExecuter.Query("GetItemId", "10001");
            string sdate = dtDate.Rows[0]["ITEMID"].ToString().Substring(0, 8);
            string sSPECSEQUENCE = "";

            // 공정스펙 XBARR 없는경우 채번생성
            if (specXBARRTable.Rows.Count == 0)
            {
                sSPECSEQUENCE = number.GetStdNumber("OperationInspection", "OS" + sdate);
            }
            else
            {
                // 공정스펙 XBARR 있는경우 채번가져오기
                sSPECSEQUENCE = specXBARRTable.Rows[0]["SPECSEQUENCE"].ToString();
            }

    
            if (specXBARRTable.Rows.Count == 0)
            {

               


                DataRow rowINSPECTIONITEMREL = dtINSPECTIONITEMREL.NewRow();

                rowINSPECTIONITEMREL["SPECSEQUENCE"] = sSPECSEQUENCE;
                rowINSPECTIONITEMREL["INSPITEMID"] = _row["INSPITEMID"];
                rowINSPECTIONITEMREL["INSPITEMVERSION"] = "*";
                rowINSPECTIONITEMREL["INSPECTIONDEFID"] = _row["INSPECTIONDEFID"];
                rowINSPECTIONITEMREL["INSPECTIONDEFVERSION"] = "*";
                rowINSPECTIONITEMREL["RESOURCETYPE"] = _row["RESOURCETYPE"];
                rowINSPECTIONITEMREL["RESOURCEID"] = _row["RESOURCEID"];
                rowINSPECTIONITEMREL["RESOURCEVERSION"] = _row["RESOURCEVERSION"];
               // rowINSPECTIONITEMREL["INSPITEMCLASSID"] = _row["INSPITEMCLASSID"];
                rowINSPECTIONITEMREL["ENTERPRISEID"] = _row["ENTERPRISEID"];
                rowINSPECTIONITEMREL["PLANTID"] = _row["PLANTID"];
                rowINSPECTIONITEMREL["UNIT"] = _row["ITEMUOM"];
                rowINSPECTIONITEMREL["VALIDSTATE"] = "Valid";
                //rowINSPECTIONITEMREL["PROCESSSEGMENTTYPE"] = "ProcessSegmentID";
                rowINSPECTIONITEMREL["PROCESSSEGID"] = _row["PROCESSSEGMENTID"];
                rowINSPECTIONITEMREL["PROCESSEGVERSION"] = "*";
                rowINSPECTIONITEMREL["SPECCLASSID"] = "OperationSpec";

                dtINSPECTIONITEMREL.Rows.Add(rowINSPECTIONITEMREL);

              


                DataRow rowSPECDEFINITION = dtSPECDEFINITION.NewRow();
                rowSPECDEFINITION["SPECSEQUENCE"] = sSPECSEQUENCE;
                rowSPECDEFINITION["SPECCLASSID"] = "OperationSpec";
                rowSPECDEFINITION["ENTERPRISEID"] = _row["ENTERPRISEID"];
                rowSPECDEFINITION["PLANTID"] = _row["PLANTID"];
                rowSPECDEFINITION["PROCESSSEGMENTCLASSID"] = "*";

                rowSPECDEFINITION["PROCESSSEGMENTID"] = _row["PROCESSSEGMENTID"];
                rowSPECDEFINITION["EQUIPMENTID"] = "*";
                rowSPECDEFINITION["CHILDEQUIPMENTID"] = "*";

                rowSPECDEFINITION["PRODUCTDEFID"] = _row["RESOURCEID"];

                rowSPECDEFINITION["CONSUMABLEDEFID"] = "*";
                rowSPECDEFINITION["CUSTOMERID"] = "*";
                rowSPECDEFINITION["VENDORID"] = "*";

                rowSPECDEFINITION["INSPITEMID"] = _row["INSPITEMID"];

                rowSPECDEFINITION["WORKTYPE"] = "*";
                rowSPECDEFINITION["WORKCONDITION"] = "*";
                rowSPECDEFINITION["RESOURCEID"] = "*";
                rowSPECDEFINITION["SPECVERSION"] = 0;
                rowSPECDEFINITION["UOMDEFID"] = _row["ITEMUOM"];
                rowSPECDEFINITION["DEFAULTCHARTTYPE"] = "XBARR";
                rowSPECDEFINITION["VALIDSTATE"] = "Valid";

                dtSPECDEFINITION.Rows.Add(rowSPECDEFINITION);
            }


            Dictionary<string, object> paramspec = new Dictionary<string, object>();
            paramspec.Add("RESOURCEID", _row["RESOURCEID"]);
            paramspec.Add("RESOURCEVERSION", _row["RESOURCEVERSION"]);
            paramspec.Add("INSPITEMID", _row["INSPITEMID"]);
            paramspec.Add("INSPECTIONDEFID", _row["INSPECTIONDEFID"]);
            paramspec.Add("INSPITEMCLASSID", _row["INSPITEMCLASSID"]);
            paramspec.Add("PROCESSSEGMENTID", _row["PROCESSSEGMENTID"]);
            paramspec.Add("CONTROLTYPE", this.cboChartType.EditValue); // 공정스펙 XBARR 는 필수데이터 입니다.
            _specTable = SqlExecuter.Query("GetSpecDetail", "10001", paramspec);


            DataRow beforeValue = null;
            //기존 값
            if (_specTable.Rows.Count > 0)
            {
                beforeValue = _specTable.Rows[0];
            }

            //신규 OR 수정 입력한 값을 데이터 테이블로 만든다
            DataTable change = new DataTable();
            

            change.Columns.Add(new DataColumn("LSL", typeof(Decimal)));
            change.Columns.Add(new DataColumn("USL", typeof(Decimal)));
            change.Columns.Add(new DataColumn("SL", typeof(Decimal)));
            change.Columns.Add(new DataColumn("LCL", typeof(Decimal)));
            change.Columns.Add(new DataColumn("UCL", typeof(Decimal)));
            change.Columns.Add(new DataColumn("CL", typeof(Decimal)));
            change.Columns.Add(new DataColumn("LOL", typeof(Decimal)));
            change.Columns.Add(new DataColumn("UOL", typeof(Decimal)));
            change.Columns.Add(new DataColumn("SPECSEQUENCE", typeof(string)));
            change.Columns.Add(new DataColumn("SPECCLASSID", typeof(string)));
            change.Columns.Add(new DataColumn("CONTROLTYPE", typeof(string)));
            change.Columns.Add(new DataColumn("_STATE_", typeof(string)));


            //입력한 값
            DataRow row = change.NewRow();

            if(this.txtLsl.EditValue.ToString() == "")
            {
                row["LSL"] = DBNull.Value;
            }
            else
            {
                decimal numberlsl = 0;
                if (decimal.TryParse(this.txtLsl.EditValue.ToString(), out numberlsl))
                {
                    row["LSL"] = numberlsl;
                }
                else
                {
                    ShowMessage("OnlyDecimal");
                    return;
                }
            }


            //row["USL"] = this.txtUsl.EditValue = this.txtUsl.EditValue.ToString() == "" ? null: this.txtUsl.EditValue;

            if (this.txtUsl.EditValue.ToString() == "")
            {
                row["USL"] = DBNull.Value;
            }
            else
            {
                decimal numberlsl = 0;
                if (decimal.TryParse(this.txtUsl.EditValue.ToString(), out numberlsl))
                {
                    row["USL"] = numberlsl;
                }
                else
                {
                    ShowMessage("OnlyDecimal");
                    return;
                }
            }

            //row["SL"] = this.txtSl.EditValue = this.txtSl.EditValue.ToString() == "" ? null: this.txtSl.EditValue;
            if (this.txtSl.EditValue.ToString() == "")
            {
                row["SL"] = DBNull.Value;
            }
            else
            {
                decimal numberlsl = 0;
                if (decimal.TryParse(this.txtSl.EditValue.ToString(), out numberlsl))
                {
                    row["SL"] = numberlsl;
                }
                else
                {
                    ShowMessage("OnlyDecimal");
                    return;
                }
            }
            //row["LCL"] = this.txtLcl.EditValue = this.txtLcl.EditValue.ToString() == "" ? null: this.txtLcl.EditValue;
            if (this.txtLcl.EditValue.ToString() == "")
            {
                row["LCL"] = DBNull.Value;
            }
            else
            {
                decimal numberlsl = 0;
                if (decimal.TryParse(this.txtLcl.EditValue.ToString(), out numberlsl))
                {
                    row["LCL"] = numberlsl;
                }
                else
                {
                    ShowMessage("OnlyDecimal");
                    return;
                }
            }
            //row["UCL"] = this.txtUcl.EditValue = this.txtUcl.EditValue.ToString() == "" ? null : this.txtUcl.EditValue;
            if (this.txtUcl.EditValue.ToString() == "")
            {
                row["UCL"] = DBNull.Value;
            }
            else
            {
                decimal numberlsl = 0;
                if (decimal.TryParse(this.txtUcl.EditValue.ToString(), out numberlsl))
                {
                    row["UCL"] = numberlsl;
                }
                else
                {
                    ShowMessage("OnlyDecimal");
                    return;
                }
            }
            //row["CL"] = this.txtCl.EditValue = this.txtCl.EditValue.ToString() == "" ? null : this.txtCl.EditValue;
            if (this.txtCl.EditValue.ToString() == "")
            {
                row["CL"] = DBNull.Value;
            }
            else
            {
                decimal numberlsl = 0;
                if (decimal.TryParse(this.txtCl.EditValue.ToString(), out numberlsl))
                {
                    row["CL"] = numberlsl;
                }
                else
                {
                    ShowMessage("OnlyDecimal");
                    return;
                }
            }
            //row["LOL"] = this.txtLol.EditValue = this.txtLol.EditValue.ToString() == "" ? null : this.txtLol.EditValue;
            if (this.txtLol.EditValue.ToString() == "")
            {
                row["LOL"] = DBNull.Value;
            }
            else
            {
                decimal numberlsl = 0;
                if (decimal.TryParse(this.txtLol.EditValue.ToString(), out numberlsl))
                {
                    row["LOL"] = numberlsl;
                }
                else
                {
                    ShowMessage("OnlyDecimal");
                    return;
                }
            }
            //row["UOL"] = this.txtUol.EditValue = this.txtUol.EditValue.ToString() == "" ? null : this.txtUol.EditValue;

            if (this.txtUol.EditValue.ToString() == "")
            {
                row["UOL"] = DBNull.Value;
            }
            else
            {
                decimal numberlsl = 0;
                if (decimal.TryParse(this.txtUol.EditValue.ToString(), out numberlsl))
                {
                    row["UOL"] = numberlsl;
                }
                else
                {
                    ShowMessage("OnlyDecimal");
                    return;
                }
            }


            row["SPECSEQUENCE"] = sSPECSEQUENCE;
            row["SPECCLASSID"] = "OperationSpec";
            row["CONTROLTYPE"] = this.cboChartType.EditValue;


          

            if (_specTable.Rows.Count == 0)
            {//기존 값 없을 때
                row["_STATE_"] = "added";
            }
            else
            {//기존 값 있을 때
                row["_STATE_"] = "modified";
            }

            change.Rows.Add(row);

            DataSet dschange = new DataSet();


            //dtINSPECTIONITEMATTRIBUTE.TableName = "sINSPECTIONITEMATTRIBUTE";
            dtINSPECTIONITEMREL.TableName = "sINSPECTIONITEMREL";
            dtSPECDEFINITION.TableName = "sSPECDEFINITION";
            change.TableName = "sSPECDETAIL";
            //dtOPERATIONSPECVALUE.TableName = "sOPERATIONSPECVALUE";

            //dtINSPECTIONITEMATTRIBUTE.Rows[0]["_STATE_"] = change.Rows[0]["_STATE_"];
            if(dtINSPECTIONITEMREL != null)
            {
                if(dtINSPECTIONITEMREL.Rows.Count !=0)
                {
                    dtINSPECTIONITEMREL.Rows[0]["_STATE_"] = change.Rows[0]["_STATE_"];
                    
                }
            }

            if (dtSPECDEFINITION != null)
            {
                if (dtSPECDEFINITION.Rows.Count != 0)
                {
                    dtSPECDEFINITION.Rows[0]["_STATE_"] = change.Rows[0]["_STATE_"];
                }
            }

            //dtOPERATIONSPECVALUE.Rows[0]["_STATE_"] = change.Rows[0]["_STATE_"];


            //dschange.Tables.Add(dtINSPECTIONITEMATTRIBUTE);
            dschange.Tables.Add(dtINSPECTIONITEMREL);
            dschange.Tables.Add(dtSPECDEFINITION);
            dschange.Tables.Add(change);
            //dschange.Tables.Add(dtOPERATIONSPECVALUE);

            ExecuteRule("RoutingSaveSpecRegister", dschange);
        }

        /// <summary>
        /// 저장 할 때 기존의 Spec 데이터와 입력된 정보를 비교한 후
        /// 변경사항이 있을 때만 저장하는 함수
        /// </summary>
        /// <param name="row"></param>
        private void DeleteSaveData()
        {
            Dictionary<string, object> paramspec = new Dictionary<string, object>();
            paramspec.Add("RESOURCEID", _row["RESOURCEID"]);
            paramspec.Add("RESOURCEVERSION", _row["RESOURCEVERSION"]);
            paramspec.Add("INSPITEMID", _row["INSPITEMID"]);
            paramspec.Add("INSPECTIONDEFID", _row["INSPECTIONDEFID"]);
            paramspec.Add("INSPITEMCLASSID", _row["INSPITEMCLASSID"]);
            paramspec.Add("PROCESSSEGMENTID", _row["PROCESSSEGMENTID"]);
            paramspec.Add("CONTROLTYPE", this.cboChartType.EditValue); // 공정스펙 XBARR 는 필수데이터 입니다.
            _specTable = SqlExecuter.Query("GetSpecDetail", "10001", paramspec);

            string sSPECSEQUENCE = "";
            sSPECSEQUENCE = _specTable.Rows[0]["SPECSEQUENCE"].ToString();

            DataTable dtINSPECTIONITEMREL = new DataTable();
            dtINSPECTIONITEMREL.Columns.Add("SPECSEQUENCE");
            dtINSPECTIONITEMREL.Columns.Add("INSPITEMID");
            dtINSPECTIONITEMREL.Columns.Add("INSPITEMVERSION");
            dtINSPECTIONITEMREL.Columns.Add("INSPECTIONDEFID");
            dtINSPECTIONITEMREL.Columns.Add("INSPECTIONDEFVERSION");
            dtINSPECTIONITEMREL.Columns.Add("RESOURCETYPE");
            dtINSPECTIONITEMREL.Columns.Add("RESOURCEID");
            dtINSPECTIONITEMREL.Columns.Add("RESOURCEVERSION");
            //dtINSPECTIONITEMREL.Columns.Add("INSPITEMCLASSID");
            dtINSPECTIONITEMREL.Columns.Add("ENTERPRISEID");
            dtINSPECTIONITEMREL.Columns.Add("PLANTID");
            dtINSPECTIONITEMREL.Columns.Add("UNIT");
            dtINSPECTIONITEMREL.Columns.Add("VALIDSTATE");
            dtINSPECTIONITEMREL.Columns.Add("_STATE_");
            //dtINSPECTIONITEMREL.Columns.Add("PROCESSSEGMENTTYPE");
            dtINSPECTIONITEMREL.Columns.Add("PROCESSSEGID");
            dtINSPECTIONITEMREL.Columns.Add("PROCESSEGVERSION");
            dtINSPECTIONITEMREL.Columns.Add("ISINSPECTIONREQUIRED");

            DataTable dtSPECDEFINITION = new DataTable();
            dtSPECDEFINITION.Columns.Add("SPECSEQUENCE");
            dtSPECDEFINITION.Columns.Add("SPECCLASSID");
            dtSPECDEFINITION.Columns.Add("ENTERPRISEID");
            dtSPECDEFINITION.Columns.Add("PLANTID");
            dtSPECDEFINITION.Columns.Add("PROCESSSEGMENTCLASSID");
            dtSPECDEFINITION.Columns.Add("PROCESSSEGMENTID");
            dtSPECDEFINITION.Columns.Add("EQUIPMENTID");
            dtSPECDEFINITION.Columns.Add("CHILDEQUIPMENTID");
            dtSPECDEFINITION.Columns.Add("PRODUCTDEFID");
            dtSPECDEFINITION.Columns.Add("CONSUMABLEDEFID");
            dtSPECDEFINITION.Columns.Add("CUSTOMERID");
            dtSPECDEFINITION.Columns.Add("VENDORID");
            dtSPECDEFINITION.Columns.Add("INSPITEMID");
            dtSPECDEFINITION.Columns.Add("WORKTYPE");
            dtSPECDEFINITION.Columns.Add("WORKCONDITION");
            dtSPECDEFINITION.Columns.Add("RESOURCEID");
            dtSPECDEFINITION.Columns.Add("SPECVERSION");
            dtSPECDEFINITION.Columns.Add("UOMDEFID");
            dtSPECDEFINITION.Columns.Add("DEFAULTCHARTTYPE");
            dtSPECDEFINITION.Columns.Add("DESCRIPTION");
            dtSPECDEFINITION.Columns.Add("VALIDSTATE");
            dtSPECDEFINITION.Columns.Add("_STATE_");

            if (this.cboChartType.EditValue.ToString() == "XBARR")
            {
                DataRow rowINSPECTIONITEMREL = dtINSPECTIONITEMREL.NewRow();
                rowINSPECTIONITEMREL["SPECSEQUENCE"] = sSPECSEQUENCE;
                rowINSPECTIONITEMREL["INSPITEMID"] = _row["INSPITEMID"];
                rowINSPECTIONITEMREL["INSPITEMVERSION"] = "*";
                rowINSPECTIONITEMREL["INSPECTIONDEFID"] = _row["INSPECTIONDEFID"];
                rowINSPECTIONITEMREL["INSPECTIONDEFVERSION"] = "*";
                rowINSPECTIONITEMREL["RESOURCETYPE"] = "Product";
                rowINSPECTIONITEMREL["RESOURCEID"] = _row["RESOURCEID"];
                rowINSPECTIONITEMREL["RESOURCEVERSION"] = _row["RESOURCEVERSION"];
               // rowINSPECTIONITEMREL["INSPITEMCLASSID"] = _row["INSPITEMCLASSID"];
                rowINSPECTIONITEMREL["ENTERPRISEID"] = _row["ENTERPRISEID"];
                rowINSPECTIONITEMREL["PLANTID"] = _row["PLANTID"];
                rowINSPECTIONITEMREL["UNIT"] = _row["ITEMUOM"];
                rowINSPECTIONITEMREL["VALIDSTATE"] = "Valid";
                rowINSPECTIONITEMREL["PROCESSSEGMENTTYPE"] = "ProcessSegmentID";
                rowINSPECTIONITEMREL["PROCESSSEGID"] = _row["PROCESSSEGMENTID"];
                rowINSPECTIONITEMREL["PROCESSEGVERSION"] = "*";
                rowINSPECTIONITEMREL["ISINSPECTIONREQUIRED"] = "Y";
                dtINSPECTIONITEMREL.Rows.Add(rowINSPECTIONITEMREL);

                DataRow rowSPECDEFINITION = dtSPECDEFINITION.NewRow();
                rowSPECDEFINITION["SPECSEQUENCE"] = sSPECSEQUENCE;
                rowSPECDEFINITION["SPECCLASSID"] = "OperationSpec";
                rowSPECDEFINITION["ENTERPRISEID"] = _row["ENTERPRISEID"];
                rowSPECDEFINITION["PLANTID"] = _row["PLANTID"];
                rowSPECDEFINITION["PROCESSSEGMENTCLASSID"] = "*";
                rowSPECDEFINITION["PROCESSSEGMENTID"] = _row["PROCESSSEGMENTID"];
                rowSPECDEFINITION["EQUIPMENTID"] = "*";
                rowSPECDEFINITION["CHILDEQUIPMENTID"] = "*";
                rowSPECDEFINITION["PRODUCTDEFID"] = _row["RESOURCEID"];
                rowSPECDEFINITION["CONSUMABLEDEFID"] = "*";
                rowSPECDEFINITION["CUSTOMERID"] = "*";
                rowSPECDEFINITION["VENDORID"] = "*";
                rowSPECDEFINITION["INSPITEMID"] = _row["INSPITEMID"];
                rowSPECDEFINITION["WORKTYPE"] = "*";
                rowSPECDEFINITION["WORKCONDITION"] = "*";
                rowSPECDEFINITION["RESOURCEID"] = "*";
                rowSPECDEFINITION["SPECVERSION"] = 0;
                rowSPECDEFINITION["UOMDEFID"] = _row["ITEMUOM"];
                rowSPECDEFINITION["DEFAULTCHARTTYPE"] = this.cboChartType.EditValue;
                rowSPECDEFINITION["VALIDSTATE"] = "Valid";
                dtSPECDEFINITION.Rows.Add(rowSPECDEFINITION);

            }



            DataRow beforeValue = null;
            //기존 값
            if (_specTable.Rows.Count > 0)
            {
                beforeValue = _specTable.Rows[0];
            }

            //신규 OR 수정 입력한 값을 데이터 테이블로 만든다
            DataTable change = new DataTable();
            DataRow row = null;


            change.Columns.Add(new DataColumn("SPECSEQUENCE", typeof(string)));
            change.Columns.Add(new DataColumn("SPECCLASSID", typeof(string)));
            change.Columns.Add(new DataColumn("CONTROLTYPE", typeof(string)));
            change.Columns.Add(new DataColumn("_STATE_", typeof(string)));


            //공정스펙 기준 타입 XBARR 일경우 삭제 스펙 전체 삭제
            if (this.cboChartType.EditValue.ToString() == "XBARR")
            {
                Dictionary<string, object> paramDel = new Dictionary<string, object>();
                paramDel.Add("RESOURCEID", _row["RESOURCEID"]);
                paramDel.Add("RESOURCEVERSION", _row["RESOURCEVERSION"]);
                paramDel.Add("INSPITEMID", _row["INSPITEMID"]);
                paramDel.Add("INSPECTIONDEFID", _row["INSPECTIONDEFID"]);
              //  paramDel.Add("INSPITEMCLASSID", _row["INSPITEMCLASSID"]);
                paramDel.Add("PROCESSSEGMENTID", _row["PROCESSSEGMENTID"]);
                //paramDel.Add("CONTROLTYPE", this.cboChartType.EditValue); // 공정스펙 XBARR 는 필수데이터 입니다.
                _specTable = SqlExecuter.Query("GetSpecDetail", "10001", paramDel);

                foreach(DataRow rowDel in _specTable.Rows)
                {
                    row = change.NewRow();
                    row["SPECSEQUENCE"] = rowDel["SPECSEQUENCE"];
                    row["SPECCLASSID"] = "OperationSpec";
                    row["CONTROLTYPE"] = rowDel["CONTROLTYPE"];
                    row["_STATE_"] = "deleted";
                    change.Rows.Add(row);
                }
                
            }
            else
            {
                row = change.NewRow();
                row["SPECSEQUENCE"] = sSPECSEQUENCE;
                row["SPECCLASSID"] = "OperationSpec";
                row["CONTROLTYPE"] = this.cboChartType.EditValue;
                row["_STATE_"] = "deleted";
                change.Rows.Add(row);
            }


            DataSet dschange = new DataSet();


            //dtINSPECTIONITEMATTRIBUTE.TableName = "sINSPECTIONITEMATTRIBUTE";
            dtINSPECTIONITEMREL.TableName = "sINSPECTIONITEMREL";
            dtSPECDEFINITION.TableName = "sSPECDEFINITION";
            change.TableName = "sSPECDETAIL";
            //dtOPERATIONSPECVALUE.TableName = "sOPERATIONSPECVALUE";

            //dtINSPECTIONITEMATTRIBUTE.Rows[0]["_STATE_"] = change.Rows[0]["_STATE_"];

            // 공정스펙 기준 타입 XBARR 일경우 삭제
            if (this.cboChartType.EditValue.ToString() == "XBARR")
            {
                dtINSPECTIONITEMREL.Rows[0]["_STATE_"] = change.Rows[0]["_STATE_"];
                dtSPECDEFINITION.Rows[0]["_STATE_"] = change.Rows[0]["_STATE_"];
            }
            //dtOPERATIONSPECVALUE.Rows[0]["_STATE_"] = change.Rows[0]["_STATE_"];


            //dschange.Tables.Add(dtINSPECTIONITEMATTRIBUTE);
            dschange.Tables.Add(dtINSPECTIONITEMREL);
            dschange.Tables.Add(dtSPECDEFINITION);
            dschange.Tables.Add(change);
            //dschange.Tables.Add(dtOPERATIONSPECVALUE);

            ExecuteRule("RoutingSaveSpecRegister", dschange);
        }

        void fnnumber()
        {

        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region 검색
        protected async void OnSearchClick()
        {
            try
            {
                DialogManager.ShowWaitArea(this.smartPanel1);
                await OnSearchAsync();
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                DialogManager.CloseWaitArea(this.smartPanel1);
                btnSave.Enabled = true;
            }
        }
        protected async Task OnSearchAsync()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();

            values.Add("RESOURCEID", _row["RESOURCEID"]);
            values.Add("RESOURCEVERSION", _row["RESOURCEVERSION"]);
            values.Add("INSPITEMID", _row["INSPITEMID"]);

            values.Add("INSPECTIONDEFID", _row["INSPECTIONDEFID"]);
            values.Add("INSPITEMCLASSID", _row["INSPITEMCLASSID"]);
            values.Add("PROCESSSEGMENTID", _row["PROCESSSEGMENTID"]);
            values.Add("CONTROLTYPE", this.cboChartType.EditValue);

            _specTable = await SqlExecuter.QueryAsync("GetSpecDetail", "10001", values);

            if (_specTable.Rows.Count < 1) // 
            {
                //ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                   this.cboChartType.EditValue = "XBARR";
                   cboChartType.Enabled = false;
                   ClearInput();
            }
            else
            {
                this.txtLsl.EditValue = _specTable.Rows[0]["LSL"];
                this.txtUsl.EditValue = _specTable.Rows[0]["USL"];
                this.txtSl.EditValue = _specTable.Rows[0]["SL"];
                this.txtLcl.EditValue = _specTable.Rows[0]["LCL"];
                this.txtUcl.EditValue = _specTable.Rows[0]["UCL"];
                this.txtCl.EditValue = _specTable.Rows[0]["CL"];
                this.txtLol.EditValue = _specTable.Rows[0]["LOL"];
                this.txtUol.EditValue = _specTable.Rows[0]["UOL"];
                //this.cboaoiqclayer.EditValue = _specTable.Rows[0]["AOIQCLAYER"];

            }

            EnableInput(true);
        }

        #endregion   

        #region Private Function
        /// <summary>
        /// 입력컨트롤 Enabled 처리를 하는 함수
        /// </summary>
        /// <param name="isUse"></param>
        private void EnableInput (bool isUse)
        {
            this.txtLsl.Enabled = isUse;
            this.txtUsl.Enabled = isUse;
            this.txtSl.Enabled = isUse;
            this.txtLcl.Enabled = isUse;
            this.txtUcl.Enabled = isUse;
            this.txtCl.Enabled = isUse;
            this.txtLol.Enabled = isUse;
            this.txtUol.Enabled = isUse;

        }

        /// <summary>
        /// 입력컨트롤을 null로 초기화 하는 함수
        /// </summary>
        private void ClearInput()
        {
            this.txtLsl.EditValue = DBNull.Value;
            this.txtUsl.EditValue = DBNull.Value;
            this.txtSl.EditValue = DBNull.Value;
            this.txtLcl.EditValue = DBNull.Value;
            this.txtUcl.EditValue = DBNull.Value;
            this.txtCl.EditValue = DBNull.Value;
            this.txtLol.EditValue = DBNull.Value;
            this.txtUol.EditValue = DBNull.Value;
        }

        /// <summary>
        /// TxtControlFormat을 주는 함수
        /// </summary>
        private void SetTxtControlFormat()
        {
            //btnSave.Enabled = false;
            //btnSearch.Enabled = false;
            //txtLsl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //txtLsl.Properties.Mask.EditMask = "#,###,##0.######";
            //txtLsl.Properties.Mask.UseMaskAsDisplayFormat = true;

            //txtUsl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //txtUsl.Properties.Mask.EditMask = "#,###,##0.######";
            //txtUsl.Properties.Mask.UseMaskAsDisplayFormat = true;

            //txtSl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //txtSl.Properties.Mask.EditMask = "#,###,##0.######";
            //txtSl.Properties.Mask.UseMaskAsDisplayFormat = true;

            //txtLcl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //txtLcl.Properties.Mask.EditMask = "#,###,##0.######";
            //txtLcl.Properties.Mask.UseMaskAsDisplayFormat = true;

            //txtUcl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //txtUcl.Properties.Mask.EditMask = "#,###,##0.######";
            //txtUcl.Properties.Mask.UseMaskAsDisplayFormat = true;

            //txtCl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //txtCl.Properties.Mask.EditMask = "#,###,##0.######";
            //txtCl.Properties.Mask.UseMaskAsDisplayFormat = true;

            //txtLol.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //txtLol.Properties.Mask.EditMask = "#,###,##0.######";
            //txtLol.Properties.Mask.UseMaskAsDisplayFormat = true;

            //txtUol.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //txtUol.Properties.Mask.EditMask = "#,###,##0.######";
            //txtUol.Properties.Mask.UseMaskAsDisplayFormat = true;
        }
        #endregion
    }
}
