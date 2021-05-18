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
    public partial class SpecRegisterDetailPopUp : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables
        public DataRow CurrentDataRow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string _specSequence;//본 그리드에서 넘겨받는 specSequence
        public string _specClassId;//본 그리드에서 넘겨받는 specClassId
        public string _inspectionName;//본 그리드에서 넘겨받는 검사명
        public string  _cboCheck;// 콤보

        // 2019-11-14 윤성원 추가 SPECDEFINITION 등록 
        public string _processsegmentclassid;
        public string _equipmentid;
        public string _childequipmentid;
        public string _inspitemid;
        public string _enterpriseid;
        public string _plantid;

        DataTable _specTable;//Spec정보가 수정되었는지 비교하기 위한 기존 데이터 테이블
        public bool buttonType = true;
        #endregion

        #region 생성자
        public SpecRegisterDetailPopUp()
        {
            InitializeComponent();
            InitializeEvent();
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

            if(_cboCheck == "")
            {
                this.cboChartType.EditValue = "XBARR";
            }
            else
            {
                this.cboChartType.EditValue = _cboCheck;
            }
            
        }

        
        #endregion

        #region Event
        private void InitializeEvent()
        {
            this.Load += SpecRegisterDetailPopUp_Load;
            this.btnCancel.Click += BtnCancel_Click;
            //this.btnSearch.Click += BtnSearch_Click;
            this.btnSave.Click += BtnSave_Click;
            this.cboChartType.EditValueChanged += CboChartType_EditValueChanged;

            //LSL,USL,SL 입력시 이벤트
            txtLsl.Leave += SL_EditValueChanged;
            txtSl.Leave += SL_EditValueChanged;
            txtUsl.Leave += SL_EditValueChanged;
            //LCL,UCL,CL 입력시 이벤트
            txtLcl.Leave += CL_EditValueChanged;
            txtCl.Leave += CL_EditValueChanged;
            txtUcl.Leave += CL_EditValueChanged;
            //LOL,UOL 입력시 이벤트
            txtLol.Leave += OL_EditValueChanged;
            txtUol.Leave += OL_EditValueChanged;

        }

        /// <summary>
        /// LSL,USL,SL 값이 바뀔 때 Validation 하는 이벤트 
        /// LSL < SL < USL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SL_EditValueChanged(object sender, EventArgs e)
        {
            SmartTextBox txtBox = sender as SmartTextBox;
            double lsl = txtLsl.EditValue.ToSafeDoubleNaN();
            double sl = txtSl.EditValue.ToSafeDoubleNaN();
            double usl = txtUsl.EditValue.ToSafeDoubleNaN();

            if (lsl > sl || sl > usl || lsl > usl)
            {
                txtBox.EditValue = DBNull.Value;
                this.ShowMessage("ValidationSL");
            }

        }

        /// <summary>
        /// LCL,UCL,CL 값이 바뀔 때 Validation 하는 이벤트 
        /// LCL < CL < UCL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CL_EditValueChanged(object sender, EventArgs e)
        {
            SmartTextBox txtBox = sender as SmartTextBox;
            double lcl = txtLcl.EditValue.ToSafeDoubleNaN();
            double cl = txtCl.EditValue.ToSafeDoubleNaN();
            double ucl = txtUcl.EditValue.ToSafeDoubleNaN();

            if (lcl > cl || cl > ucl || lcl > ucl)
            {
                txtBox.EditValue = DBNull.Value;
                this.ShowMessage("ValidationCL");
            }
        }

        /// <summary>
        /// LOL,UOL 값이 바뀔 때 Validation 하는 이벤트 
        /// LOL < UOL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OL_EditValueChanged(object sender, EventArgs e)
        {
            SmartTextBox txtBox = sender as SmartTextBox;
            double lol = txtLol.EditValue.ToSafeDoubleNaN();
            double uol = txtUol.EditValue.ToSafeDoubleNaN();

            if (lol > uol)
            {
                txtBox.EditValue = DBNull.Value;
                this.ShowMessage("ValidationOL");
            }

        }
        private void CboChartType_EditValueChanged(object sender, EventArgs e)
        {
            OnSearchClick();
        }

        private void SpecRegisterDetailPopUp_Load(object sender, EventArgs e)
        {
            InitializeComboBox();
            SetTxtControlFormat();
            EnableInput(false);
            if (buttonType == false)
            {
                btnSave.Visible = false;
            }
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

                    this.SaveData(_specTable);
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
        /// <param name="table"></param>
        private void SaveData(DataTable table)
        {
            DataRow beforeValue = null;
            //기존 값
            if (table.Rows.Count > 0)
            {
                beforeValue = table.Rows[0];
            }

            //신규 OR 수정 입력한 값을 데이터 테이블로 만든다
            DataTable change = new DataTable();
            DataRow row = null;

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
            row = change.NewRow();
            row["LSL"] = this.txtLsl.EditValue;
            row["USL"] = this.txtUsl.EditValue;
            row["SL"] = this.txtSl.EditValue;
            row["LCL"] = this.txtLcl.EditValue;
            row["UCL"] = this.txtUcl.EditValue;
            row["CL"] = this.txtCl.EditValue;
            row["LOL"] = this.txtLol.EditValue;
            row["UOL"] = this.txtUol.EditValue;
            row["SPECSEQUENCE"] = _specSequence;
            row["SPECCLASSID"] = _specClassId;
            row["CONTROLTYPE"] = this.cboChartType.EditValue;


            if (table.Rows.Count < 1)
            {//기존 값 없을 때
                row["_STATE_"] = "added";
            }
            else
            {//기존 값 있을 때
                if (beforeValue["LSL"].ToString().Equals(this.txtLsl.EditValue.ToString()) &&
                   beforeValue["USL"].ToString().Equals(this.txtUsl.EditValue.ToString()) &&
                   beforeValue["SL"].ToString().Equals(this.txtSl.EditValue.ToString()) &&
                   beforeValue["LCL"].ToString().Equals(this.txtLcl.EditValue.ToString()) &&
                   beforeValue["UCL"].ToString().Equals(this.txtUcl.EditValue.ToString()) &&
                   beforeValue["CL"].ToString().Equals(this.txtCl.EditValue.ToString()) &&
                   beforeValue["LOL"].ToString().Equals(this.txtLol.EditValue.ToString()) &&
                   beforeValue["UOL"].ToString().Equals(this.txtUol.EditValue.ToString()) &&
                   beforeValue["SPECSEQUENCE"].Equals(_specSequence) &&
                   beforeValue["SPECCLASSID"].Equals(_specClassId) &&
                   beforeValue["CONTROLTYPE"].Equals(this.cboChartType.EditValue.ToString()))
                {//기존 값 있으면서 수정된 내용 없을 때
                    throw MessageException.Create("NoChangeSpecData");
                }
                else
                {//기존 값 있으면서 수정된 내용 있을 때
                    row["_STATE_"] = "modified";
                }
            }

            change.Rows.Add(row);


            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("ENTERPRISEID", this._specSequence);
            values.Add("P_SPECCLASSID", this._specClassId);
            values.Add("P_VALIDSTATE", "Valid");
            values.Add("P_SPECSEQUENCE", this._specSequence);
            
            DataTable dtSd =   SqlExecuter.Query("SelectSpecDefinition", "10001", values);

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
            dtSPECDEFINITION.Columns.Add("CONTROLRANGE");
            

            if (dtSd == null)
            {
                


                //public string _processsegmentclassid;
                //public string _equipmentid;
                //public string _childequipmentid;
                //public string _inspitemid;
                //public string _enterpriseid;
                //public string _plantid;

                DataRow rowSPECDEFINITION = dtSPECDEFINITION.NewRow();
                rowSPECDEFINITION["SPECSEQUENCE"] = _specSequence;
                rowSPECDEFINITION["SPECCLASSID"] = "OperationSpec";
                rowSPECDEFINITION["ENTERPRISEID"] = _enterpriseid;
                rowSPECDEFINITION["PLANTID"] = _plantid;
                rowSPECDEFINITION["PROCESSSEGMENTCLASSID"] = _processsegmentclassid;
                rowSPECDEFINITION["PROCESSSEGMENTID"] = "*";
                rowSPECDEFINITION["EQUIPMENTID"] = "*";
                rowSPECDEFINITION["CHILDEQUIPMENTID"] = "*";
                rowSPECDEFINITION["PRODUCTDEFID"] = "";
                rowSPECDEFINITION["CONSUMABLEDEFID"] = "*";
                rowSPECDEFINITION["CUSTOMERID"] = "*";
                rowSPECDEFINITION["VENDORID"] = "*";
                rowSPECDEFINITION["INSPITEMID"] = _inspitemid;
                rowSPECDEFINITION["WORKTYPE"] = "*";
                rowSPECDEFINITION["WORKCONDITION"] = "*";
                rowSPECDEFINITION["RESOURCEID"] = "*";
                rowSPECDEFINITION["SPECVERSION"] = 0;
                rowSPECDEFINITION["UOMDEFID"] = "";//_row["ITEMUOM"];
                rowSPECDEFINITION["DEFAULTCHARTTYPE"] = this.cboChartType.EditValue;
                rowSPECDEFINITION["VALIDSTATE"] = "Valid";
                rowSPECDEFINITION["_STATE_"] = "added";
                rowSPECDEFINITION["CONTROLRANGE"] = change.Rows[0]["LSL"].ToString() + change.Rows[0]["USL"].ToString() + change.Rows[0]["SL"].ToString();
                dtSPECDEFINITION.Rows.Add(rowSPECDEFINITION);

            }
            else
            {
                if(dtSd.Rows.Count !=0)
                {
                    DataRow rowSPECDEFINITION = dtSPECDEFINITION.NewRow();
                    rowSPECDEFINITION["SPECSEQUENCE"] = _specSequence;
                    rowSPECDEFINITION["SPECCLASSID"] = "OperationSpec";
                    rowSPECDEFINITION["ENTERPRISEID"] = _enterpriseid;
                    rowSPECDEFINITION["PLANTID"] = _plantid;
                    rowSPECDEFINITION["PROCESSSEGMENTCLASSID"] = _processsegmentclassid;
                    rowSPECDEFINITION["PROCESSSEGMENTID"] = "*";
                    rowSPECDEFINITION["EQUIPMENTID"] = "*";
                    rowSPECDEFINITION["CHILDEQUIPMENTID"] = "*";
                    rowSPECDEFINITION["PRODUCTDEFID"] = "";
                    rowSPECDEFINITION["CONSUMABLEDEFID"] = "*";
                    rowSPECDEFINITION["CUSTOMERID"] = "*";
                    rowSPECDEFINITION["VENDORID"] = "*";
                    rowSPECDEFINITION["INSPITEMID"] = _inspitemid;
                    rowSPECDEFINITION["WORKTYPE"] = "*";
                    rowSPECDEFINITION["WORKCONDITION"] = "*";
                    rowSPECDEFINITION["RESOURCEID"] = "*";
                    rowSPECDEFINITION["SPECVERSION"] = 0;
                    rowSPECDEFINITION["UOMDEFID"] = "";//_row["ITEMUOM"];
                    rowSPECDEFINITION["DEFAULTCHARTTYPE"] = this.cboChartType.EditValue;
                    rowSPECDEFINITION["VALIDSTATE"] = "Valid";
                    rowSPECDEFINITION["_STATE_"] = "modified";
                    rowSPECDEFINITION["CONTROLRANGE"] = change.Rows[0]["LSL"].ToString() + change.Rows[0]["USL"].ToString() + change.Rows[0]["SL"].ToString();
                    dtSPECDEFINITION.Rows.Add(rowSPECDEFINITION);
                }
                else
                {
                    DataRow rowSPECDEFINITION = dtSPECDEFINITION.NewRow();
                    rowSPECDEFINITION["SPECSEQUENCE"] = _specSequence;
                    rowSPECDEFINITION["SPECCLASSID"] = "OperationSpec";
                    rowSPECDEFINITION["ENTERPRISEID"] = _enterpriseid;
                    rowSPECDEFINITION["PLANTID"] = _plantid;
                    rowSPECDEFINITION["PROCESSSEGMENTCLASSID"] = _processsegmentclassid;
                    rowSPECDEFINITION["PROCESSSEGMENTID"] = "*";
                    rowSPECDEFINITION["EQUIPMENTID"] = "*";
                    rowSPECDEFINITION["CHILDEQUIPMENTID"] = "*";
                    rowSPECDEFINITION["PRODUCTDEFID"] = "";
                    rowSPECDEFINITION["CONSUMABLEDEFID"] = "*";
                    rowSPECDEFINITION["CUSTOMERID"] = "*";
                    rowSPECDEFINITION["VENDORID"] = "*";
                    rowSPECDEFINITION["INSPITEMID"] = _inspitemid;
                    rowSPECDEFINITION["WORKTYPE"] = "*";
                    rowSPECDEFINITION["WORKCONDITION"] = "*";
                    rowSPECDEFINITION["RESOURCEID"] = "*";
                    rowSPECDEFINITION["SPECVERSION"] = 0;
                    rowSPECDEFINITION["UOMDEFID"] = "";//_row["ITEMUOM"];
                    rowSPECDEFINITION["DEFAULTCHARTTYPE"] = this.cboChartType.EditValue;
                    rowSPECDEFINITION["VALIDSTATE"] = "Valid";
                    rowSPECDEFINITION["_STATE_"] = "added";
                    rowSPECDEFINITION["CONTROLRANGE"] = change.Rows[0]["LSL"].ToString() + change.Rows[0]["USL"].ToString() + change.Rows[0]["SL"].ToString();
                    dtSPECDEFINITION.Rows.Add(rowSPECDEFINITION);
                }
            }

            dtSPECDEFINITION.TableName = "sSPECDEFINITION";
            change.TableName = "sSPECDETAIL";

            DataSet dschange = new DataSet();
            dschange.Tables.Add(change);
            dschange.Tables.Add(dtSPECDEFINITION);

            ExecuteRule("SaveSpecRegister", dschange);
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

            values.Add("P_CONTROLTYPE", this.cboChartType.GetDataValue());
            values.Add("P_SPECCLASSID", this._specClassId);
            values.Add("P_SPECSEQUENCE", this._specSequence);
             _specTable = await SqlExecuter.QueryAsync("SelectSpecDetail", "10001", values);

            if (_specTable.Rows.Count < 1) // 
            {
                //ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.

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
            btnSave.Enabled = false;
            //btnSearch.Enabled = false;
            txtLsl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtLsl.Properties.Mask.EditMask = "###,###,###,0.######";
            txtLsl.Properties.Mask.UseMaskAsDisplayFormat = true;

            txtUsl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtUsl.Properties.Mask.EditMask = "###,###,###,0.######";
            txtUsl.Properties.Mask.UseMaskAsDisplayFormat = true;

            txtSl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtSl.Properties.Mask.EditMask = "###,###,###,0.######";
            txtSl.Properties.Mask.UseMaskAsDisplayFormat = true;

            txtLcl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtLcl.Properties.Mask.EditMask = "###,###,###,0.######";
            txtLcl.Properties.Mask.UseMaskAsDisplayFormat = true;

            txtUcl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtUcl.Properties.Mask.EditMask = "###,###,###,0.######";
            txtUcl.Properties.Mask.UseMaskAsDisplayFormat = true;

            txtCl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtCl.Properties.Mask.EditMask = "###,###,###,0.######";
            txtCl.Properties.Mask.UseMaskAsDisplayFormat = true;

            txtLol.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtLol.Properties.Mask.EditMask = "###,###,###,0.######";
            txtLol.Properties.Mask.UseMaskAsDisplayFormat = true;

            txtUol.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtUol.Properties.Mask.EditMask = "###,###,###,0.######";
            txtUol.Properties.Mask.UseMaskAsDisplayFormat = true;
        }
        #endregion

        #region Public function
        public void SetControlHidden()
        {
            this.btnSave.Hide();
            txtLsl.ReadOnly = true;
            txtUsl.ReadOnly = true;
            txtLcl.ReadOnly = true;
            txtUcl.ReadOnly = true;
            txtSl.ReadOnly = true;
            txtCl.ReadOnly = true;
            txtLol.ReadOnly = true;
            txtUol.ReadOnly = true;
        }
        #endregion
    }
}
