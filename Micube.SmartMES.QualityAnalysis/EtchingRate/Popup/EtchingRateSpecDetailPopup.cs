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

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질 > 레칭레이트 > 에칭레이트 규격 등록
    /// 업  무  설  명  : SPEC 등록 화면의 그리드를 더블 클릭하여 상세 스펙을 관리한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-05-14
    /// 수  정  이  력  : 윤성원 2019-07-05 using 에 #region #endregion 추가
    /// 
    /// 
    /// </summary>
    public partial class EtchingRateSpecDetailPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables
        public DataRow CurrentDataRow  { get; set; }
        public string _controlRange;//관리범위 부모창으로 전달
        public string _specRange;//규격범위 부모창으로 전달
        public string _chartType;//차트타입 부모창으로 전달 
        public DataRow _specDetailRow;//조회한(DB Select) 테이블 중 선택된 ChartType의 DataRow
        private DataTable _specTable;//조회한(DB Select) SpecDetail
        public DataTable _beforeSave;//저장 전의 SpecDetail (부모 팝업으로 부터 넘겨받음) / 저장전 데이터 있을 때 덮어쓰기 위함

        #endregion

        #region 생성자
        public EtchingRateSpecDetailPopup()
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
            this.cboChartType.Properties.PopulateColumns();
            this.cboChartType.Properties.Columns["CODEID"].Visible = false;
        }

        
        #endregion

        #region Event
        private void InitializeEvent()
        {
            this.Load += EtchingRateSpecDetailPopup_Load;
            this.btnCancel.Click += BtnCancel_Click;
            this.btnOK.Click += BtnOK_Click;
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

            if (lsl >= sl || sl >= usl || lsl >= usl)
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

            if (lcl >= cl || cl >= ucl || lcl >= ucl)
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

            if (lol >= uol)
            {
                txtBox.EditValue = DBNull.Value;
                this.ShowMessage("ValidationOL");
            }

        }

        /// <summary>
        /// 팝업 로드시 chartType콤보박스 초기화, 선택된 row의 defaultChartType을 기본 값으로 설정
        /// textBox format설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EtchingRateSpecDetailPopup_Load(object sender, EventArgs e)
        {
            InitializeComboBox();
            SetTxtControlFormat();
            this.cboChartType.EditValue = CurrentDataRow["DEFAULTCHARTTYPE"];
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }
        /// <summary>
        /// charType의 값이 바뀔 때 선택된 ChartType의 spec 값을 보여주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboChartType_EditValueChanged(object sender, EventArgs e)
        {
            FilterSpecData(cboChartType.EditValue.ToString());
            updateDataBeforeSave(_beforeSave, cboChartType.EditValue.ToString(), CurrentDataRow["RESOURCEID"].ToString());
        }

        /// <summary>
        /// 저장 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            //DialogResult result = System.Windows.Forms.DialogResult.No;

            //result = this.ShowMessage(MessageBoxButtons.YesNo, "SpecRegisterConfirmNote");

            //if (result == System.Windows.Forms.DialogResult.Yes)
            //{
                try
                {
                    this.ShowWaitArea();

                    Comparedata(_specDetailRow);

                    _chartType = cboChartType.EditValue.ToString();
                    _controlRange = txtLcl.EditValue + "~" +txtUcl.EditValue + "("+ txtCl.EditValue + ")";
                    _specRange = txtLsl.EditValue + "~" + txtUsl.EditValue + "(" + txtSl.EditValue + ")";

                    //팝업 확인버튼 클릭시 DialogResult를 OK로 설정 부모 창에서 쓰기 위함
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    this.Close();
                }
            //}

        }

        /// <summary>
        /// 취소버튼 눌렀을 때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region 검색
        //protected async void OnSearchClick()
        //{
        //    try
        //    {
        //        DialogManager.ShowWaitArea(this.smartPanel1);
        //        await OnSearchAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowError(ex);
        //    }
        //    finally
        //    {
        //        DialogManager.CloseWaitArea(this.smartPanel1);
        //        btnSave.Enabled = true;
        //    }
        //}
        //protected async Task OnSearchAsync()
        //{
        //    Dictionary<string, object> values = new Dictionary<string, object>();
        //    values.Add("SPECSEQUENCE", CurrentDataRow["SPECSEQUENCE"]);
        //     _specTable = await SqlExecuter.QueryAsync("GetEtchingRateSpecDetail", "10001", values);

        //    FilterSpecData();

        //    EnableInput(true);
        //}

        #endregion

        #region Public Function
        /// <summary>
        /// 팝업을 열때 저장된 specDetail을 조회하는 함수
        /// </summary>
        public void SearchSpecDetail()
        {        
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("SPECSEQUENCE", CurrentDataRow["SPECSEQUENCE"]);
            _specTable = SqlExecuter.Query("GetEtchingRateSpecDetail", "10001", values);

            FilterSpecData(CurrentDataRow["DEFAULTCHARTTYPE"].ToString());
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
            txtLsl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtLsl.Properties.Mask.EditMask = "###,###,0.##";
            txtLsl.Properties.Mask.UseMaskAsDisplayFormat = true;

            txtUsl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtUsl.Properties.Mask.EditMask = "###,###,0.##";
            txtUsl.Properties.Mask.UseMaskAsDisplayFormat = true;

            txtSl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtSl.Properties.Mask.EditMask = "###,###,0.##";
            txtSl.Properties.Mask.UseMaskAsDisplayFormat = true;

            txtLcl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtLcl.Properties.Mask.EditMask = "###,###,0.##";
            txtLcl.Properties.Mask.UseMaskAsDisplayFormat = true;

            txtUcl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtUcl.Properties.Mask.EditMask = "###,###,0.##";
            txtUcl.Properties.Mask.UseMaskAsDisplayFormat = true;

            txtCl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtCl.Properties.Mask.EditMask = "###,###,0.##";
            txtCl.Properties.Mask.UseMaskAsDisplayFormat = true;

            txtLol.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtLol.Properties.Mask.EditMask = "###,###,0.##";
            txtLol.Properties.Mask.UseMaskAsDisplayFormat = true;

            txtUol.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtUol.Properties.Mask.EditMask = "###,###,0.##";
            txtUol.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        /// <summary>
        /// 팝업을 열때 조회해 온 데이타(DB Select)를 chartType에 따라 필터링하여 
        /// 해당 chartType spec값을 보여주는 함수 
        /// </summary>
        /// <param name="controlType"></param>
        private void FilterSpecData(string controlType)
        {
            var controlTypeData = _specTable.Rows.Cast<DataRow>()
                .Where(r => r["CONTROLTYPE"].ToString() == controlType)
                .CopyToDataTable();

            _specDetailRow = controlTypeData.Rows[0];

            _specDetailRow["RESOURCEID"] = CurrentDataRow["RESOURCEID"];
            _specDetailRow["SPECSEQUENCE"] = CurrentDataRow["SPECSEQUENCE"];
            _specDetailRow["PLANTID"] = Framework.UserInfo.Current.Plant;
            _specDetailRow["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;

            this.txtLsl.EditValue = _specDetailRow["LSL"];
            this.txtUsl.EditValue = _specDetailRow["USL"];
            this.txtSl.EditValue = _specDetailRow["SL"];
            this.txtLcl.EditValue = _specDetailRow["LCL"];
            this.txtUcl.EditValue = _specDetailRow["UCL"];
            this.txtCl.EditValue = _specDetailRow["CL"];
            this.txtLol.EditValue = _specDetailRow["LOL"];
            this.txtUol.EditValue = _specDetailRow["UOL"];
        }
        /// <summary>
        /// specDetail을 입력할 때 기존의 Spec 데이터(DB에 저장된 값)와 입력된 정보를 비교한 후
        /// 변경사항이 있을 때만 저장하는 함수
        /// </summary>
        /// <param name="selectedRow"></param>
        private void Comparedata(DataRow selectedRow)
        {
            //기존값
            //DataRow beforeValue = beforeData;
        
            //신규 OR 수정 입력한 값을 데이터 테이블로 만든다
            //DataTable change = new DataTable();
            //DataRow row = null;


            //change.Columns.Add(new DataColumn("LSL", typeof(Decimal)));
            //change.Columns.Add(new DataColumn("USL", typeof(Decimal)));
            //change.Columns.Add(new DataColumn("SL", typeof(Decimal)));
            //change.Columns.Add(new DataColumn("LCL", typeof(Decimal)));
            //change.Columns.Add(new DataColumn("UCL", typeof(Decimal)));
            //change.Columns.Add(new DataColumn("CL", typeof(Decimal)));
            //change.Columns.Add(new DataColumn("LOL", typeof(Decimal)));
            //change.Columns.Add(new DataColumn("UOL", typeof(Decimal)));
            //change.Columns.Add(new DataColumn("SPECSEQUENCE", typeof(string)));
            //change.Columns.Add(new DataColumn("SPECCLASSID", typeof(string)));
            //change.Columns.Add(new DataColumn("CONTROLTYPE", typeof(string)));
            //change.Columns.Add(new DataColumn("RESOURCEID", typeof(string)));
            //change.Columns.Add(new DataColumn("_STATE_", typeof(string)));

            //입력한 값
            //row = change.NewRow();
            //row["LSL"] = this.txtLsl.EditValue;
            //row["USL"] = this.txtUsl.EditValue;
            //row["SL"] = this.txtSl.EditValue;
            //row["LCL"] = this.txtLcl.EditValue;
            //row["UCL"] = this.txtUcl.EditValue;
            //row["CL"] = this.txtCl.EditValue;
            //row["LOL"] = this.txtLol.EditValue;
            //row["UOL"] = this.txtUol.EditValue;
            //row["SPECSEQUENCE"] = CurrentDataRow["SPECSEQUENCE"];
            //row["SPECCLASSID"] = CurrentDataRow["SPECCLASSID"];
            //row["CONTROLTYPE"] = this.cboChartType.EditValue;


            if (selectedRow["HASDATA"].ToString().Equals("FALSE"))
            {//기존 값 없을 때 (DB에 저장된 값이 없었을 때)
                selectedRow["_STATE_"] = "added";
            }
            else
            {//기존 값 있을 때 (DB에 저장된 값이 있었을 때)
                if (selectedRow["LSL"].ToString().Equals(this.txtLsl.EditValue.ToString()) &&
                   selectedRow["USL"].ToString().Equals(this.txtUsl.EditValue.ToString()) &&
                   selectedRow["SL"].ToString().Equals(this.txtSl.EditValue.ToString()) &&
                   selectedRow["LCL"].ToString().Equals(this.txtLcl.EditValue.ToString()) &&
                   selectedRow["UCL"].ToString().Equals(this.txtUcl.EditValue.ToString()) &&
                   selectedRow["CL"].ToString().Equals(this.txtCl.EditValue.ToString()) &&
                   selectedRow["LOL"].ToString().Equals(this.txtLol.EditValue.ToString()) &&
                   selectedRow["UOL"].ToString().Equals(this.txtUol.EditValue.ToString()))
                {//기존 값 있으면서 수정된 내용 없을 때
                    throw MessageException.Create("NoChangeSpecData");
                }
                else
                {//기존 값 있으면서 수정된 내용 있을 때
                    selectedRow["_STATE_"] = "modified";
                }
            }

            selectedRow["LSL"] = txtLsl.EditValue;
            selectedRow["USL"] = txtUsl.EditValue;
            selectedRow["SL"] = txtSl.EditValue;
            selectedRow["LCL"] = txtLcl.EditValue;
            selectedRow["UCL"] = txtUcl.EditValue;
            selectedRow["CL"] = txtCl.EditValue;
            selectedRow["LOL"] = txtLol.EditValue;
            selectedRow["UOL"] = txtUol.EditValue;

            //change.Rows.Add(row);
            //ExecuteRule("SaveSpecRegister", change);
        }

        /// <summary>
        /// 저장하기전의 SpecDetail을 재 입력하기위해 팝업을 열때 입력 했던 내용이 있다면 덮어씌워서 보여주는 함수 
        /// </summary>
        /// <param name="beforeSaveTable"></param>
        /// <param name="controlType"></param>
        /// <param name="resourceId"></param>
        private void updateDataBeforeSave(DataTable beforeSaveTable, string controlType, string resourceId)
        {
            var updateData = beforeSaveTable.Rows.Cast<DataRow>()
                .Where(r => r["CONTROLTYPE"].ToString() == controlType && r["RESOURCEID"].ToString() == resourceId).ToList();

            if (updateData.Count > 0)
            {
                DataTable dt = updateData.CopyToDataTable();
                DataRow updateRow = dt.Rows[0];

                this.txtLsl.EditValue = updateRow["LSL"];
                this.txtUsl.EditValue = updateRow["USL"];
                this.txtSl.EditValue = updateRow["SL"];
                this.txtLcl.EditValue = updateRow["LCL"];
                this.txtUcl.EditValue = updateRow["UCL"];
                this.txtCl.EditValue = updateRow["CL"];
                this.txtLol.EditValue = updateRow["LOL"];
                this.txtUol.EditValue = updateRow["UOL"];
            }

        }

        #endregion

    }
}
