#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.Net.Data;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리 > 외주가공비마감 > 기타 외주 비용 등록
    /// 업  무  설  명  : 기타외주 작업의뢰 내역을 등록한다.
    /// 생    성    자  : choisstar
    /// 생    성    일  : 2019-06-13
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OtherOutsourcedClaimCostRegistration : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        private bool _blchang = false; //임시작업그리드 통제용 
        private string _strPlantid = ""; // plant 변경시 작업 
        private string _strOspVendorid = ""; //
        private bool _blSaveAuth = false;   //저장버튼 권한 체크 처리 
        #endregion

        #region 생성자

        public OtherOutsourcedClaimCostRegistration()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {

            base.InitializeContent();
            // TODO : 컨트롤 초기화 로직 구성
            InitializeComboBox();  // 콤보박스 셋팅 

            _strPlantid = UserInfo.Current.Plant.ToString();  // 작업용 plant 셋팅 (조회시 다시 셋팅)
            _blSaveAuth = true;
            InitializeEvent();    // 이벤트 

            selectOspAreaidPopup(_strPlantid);

            InitializeGrid();// 기타외주작업목록 grid 셋팅

            BtnClear_Click(null, null);
        }

        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
            // 작업구분값 정의 
            cboClaimType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboClaimType.ValueMember = "CODEID";
            cboClaimType.DisplayMember = "CODENAME";
            cboClaimType.EditValue = "1";
            DataTable dtClaimType = SqlExecuter.Query("GetCodeList", "00001"
                                                  , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "ClaimType" } });
            DataRow[] filteredDataRows = dtClaimType.Select("CODEID NOT IN ('DefectScrap' )");
            DataTable filteredDataTable = new DataTable();

            if (filteredDataRows.Length != 0)
            {
                filteredDataTable = filteredDataRows.CopyToDataTable();
                cboClaimType.DataSource = filteredDataTable;
            }
            else
            {
                cboClaimType.DataSource = dtClaimType;
            }
            cboClaimType.ShowHeader = false;
        }

        #endregion

        #region Popup

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

                    _strOspVendorid = row["OSPVENDORID"].ToString();
                    txtOspVendorName.Text = row["OSPVENDORNAME"].ToString();
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
            popupOspAreaid.SelectPopupCondition = popup;
        }

       

        #endregion

        #region 외주작업의뢰 목록 리스트 그리드
        /// <summary>        
        /// 외주작업의뢰 목록 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdOtherOutsourcedClaimCostRegistration.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;
            grdOtherOutsourcedClaimCostRegistration.View.SetIsReadOnly();
            grdOtherOutsourcedClaimCostRegistration.View.AddTextBoxColumn("ENTERPRISEID", 120)
                .SetIsHidden();                                                               //  회사 ID
            grdOtherOutsourcedClaimCostRegistration.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();
            grdOtherOutsourcedClaimCostRegistration.View.AddTextBoxColumn("PERIODNAME")
                .SetIsHidden();
            grdOtherOutsourcedClaimCostRegistration.View.AddTextBoxColumn("PERIODSTATE")
                .SetIsHidden();
            grdOtherOutsourcedClaimCostRegistration.View.AddTextBoxColumn("RELKEYNO")
                .SetIsHidden();
            grdOtherOutsourcedClaimCostRegistration.View.AddTextBoxColumn("CLAIMDATE", 120)
               .SetDisplayFormat("yyyy-MM-dd");                                                //  정산일자
            grdOtherOutsourcedClaimCostRegistration.View.AddTextBoxColumn("CLAIMSEQUENCE", 120);              // 정산번호
            grdOtherOutsourcedClaimCostRegistration.View.AddComboBoxColumn("CLAIMTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ClaimType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));          //  작업구분           
            grdOtherOutsourcedClaimCostRegistration.View.AddTextBoxColumn("CLAIMDEPARTMENT", 120);              // 정산부서
            grdOtherOutsourcedClaimCostRegistration.View.AddTextBoxColumn("CLAIMUSER", 120);                 // 정산자ID
            grdOtherOutsourcedClaimCostRegistration.View.AddTextBoxColumn("CLAIMUSERNAME", 120);               // 정산자명
            grdOtherOutsourcedClaimCostRegistration.View.AddTextBoxColumn("AREAID", 80);                     // 협력사
            grdOtherOutsourcedClaimCostRegistration.View.AddTextBoxColumn("AREANAME", 120);                   // 협력사
            grdOtherOutsourcedClaimCostRegistration.View.AddTextBoxColumn("OSPVENDORID", 120);                     // 협력사
            grdOtherOutsourcedClaimCostRegistration.View.AddTextBoxColumn("OSPVENDORNAME", 120);                   // 협력사
          
            grdOtherOutsourcedClaimCostRegistration.View.AddTextBoxColumn("PRODUCTDEFID", 120);                    // 제품 정의 ID
            grdOtherOutsourcedClaimCostRegistration.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);               // 제품 정의 Version
            grdOtherOutsourcedClaimCostRegistration.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);                  // 제품명
            grdOtherOutsourcedClaimCostRegistration.View.AddSpinEditColumn("CLAIMAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                                   // 정산금액
            grdOtherOutsourcedClaimCostRegistration.View.AddTextBoxColumn("DESCRIPTION", 120);                  //  설명
            grdOtherOutsourcedClaimCostRegistration.View.AddTextBoxColumn("PERIODID", 80)
                .SetIsHidden();       // PERIODID
         
            grdOtherOutsourcedClaimCostRegistration.View.PopulateColumns();

        }
        #endregion

        #region 외주작업의뢰 작업용 임시 그리드
        /// <summary>        
        /// 외주작업의뢰 목록 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializegOtherSubcontractWorkTempGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdWorkTemp.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;
            grdWorkTemp.View.AddTextBoxColumn("ENTERPRISEID", 120)
              .SetIsHidden();                                                               //  회사 ID
            grdWorkTemp.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();
            grdWorkTemp.View.AddTextBoxColumn("PERIODNAME")
               .SetIsHidden();
            grdWorkTemp.View.AddTextBoxColumn("PERIODSTATE")
                .SetIsHidden();
            grdWorkTemp.View.AddTextBoxColumn("RELKEYNO")
               .SetIsHidden();
            grdWorkTemp.View.AddTextBoxColumn("CLAIMDATE", 120)
               .SetDisplayFormat("yyyy-MM-dd");                                                //  정산일자
            grdWorkTemp.View.AddTextBoxColumn("CLAIMSEQUENCE", 120);              // 정산번호
            grdWorkTemp.View.AddComboBoxColumn("CLAIMTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ClaimType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));          //  작업구분           
            grdWorkTemp.View.AddTextBoxColumn("CLAIMDEPARTMENT", 120);              // 정산부서
            grdWorkTemp.View.AddTextBoxColumn("CLAIMUSER", 120);                 // 정산자ID
            grdWorkTemp.View.AddTextBoxColumn("CLAIMUSERNAME", 120);               // 정산자명
            grdWorkTemp.View.AddTextBoxColumn("AREAID", 120);                     // 협력사
            grdWorkTemp.View.AddTextBoxColumn("AREANAME", 120);                   // 협력사
            grdWorkTemp.View.AddTextBoxColumn("OSPVENDORID", 120);                     // 협력사
            grdWorkTemp.View.AddTextBoxColumn("OSPVENDORNAME", 120);                   // 협력사

            grdWorkTemp.View.AddTextBoxColumn("PRODUCTDEFID", 120);                    // 제품 정의 ID
            grdWorkTemp.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);               // 제품 정의 Version
            grdWorkTemp.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);                  // 제품명
            grdWorkTemp.View.AddSpinEditColumn("CLAIMAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                                   // 정산금액
            grdWorkTemp.View.AddTextBoxColumn("DESCRIPTION", 200);                  //  설명
            grdWorkTemp.View.AddTextBoxColumn("PERIODID", 80)
                .SetIsHidden();       // PERIODID
            grdWorkTemp.View.PopulateColumns();
        }
        #endregion

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가

            // 버튼 이벤트 처리 
            btnClear.Click += BtnClear_Click;
            btnSave.Click += BtnSave_Click;
            btnDelete.Click += BtnDelete_Click;
            // 그리드 클릭 이벤트 
            grdOtherOutsourcedClaimCostRegistration.View.FocusedRowChanged += View_FocusedRowChanged;

            grdOtherOutsourcedClaimCostRegistration.View.RowCellClick += View_RowCellClick;
            // 화면컨트롤 이벤트 처리 
            txtClaimAmount.EditValueChanged += DetailControl_EditValueChanged;

        }

        /// <summary>
        /// 기타외주작업목록의 수정여부및 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailControl_EditValueChanged(object sender, EventArgs e)
        {

            if (_blchang == false) return;
            DataRow dr = null;
            if (grdWorkTemp.View.DataRowCount != 1)
            {
                grdWorkTemp.View.ClearDatas();
                DataTable dt = createSaveDatatable();
                grdWorkTemp.DataSource = dt;
                grdWorkTemp.View.AddNewRow();
                dr = grdWorkTemp.View.GetDataRow(grdWorkTemp.View.FocusedRowHandle);
                var values = Conditions.GetValues();
                string _strPlantid = values["P_PLANTID"].ToString();
                dr["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
                dr["PLANTID"] = _strPlantid;
                dr["CLAIMUSER"] = UserInfo.Current.Id.ToString();
                dr["CLAIMDEPARTMENT"] = UserInfo.Current.Department.ToString();
                dr["_STATE_"] = "added";


            }
            else if (!(grdWorkTemp.View.GetRowCellValue(0, "_STATE_").ToString().Equals("added")))
            {
                dr = grdWorkTemp.View.GetDataRow(grdWorkTemp.View.FocusedRowHandle);
                //grdWorkTemp.View.SetRowCellValue(0, "_STATE_", "modified");
                dr["_STATE_"] = "modified";
            }
            else
            {
                dr = grdWorkTemp.View.GetDataRow(grdWorkTemp.View.FocusedRowHandle);
                dr["_STATE_"] = "added";
            }

            DateTime dateClaimDate = Convert.ToDateTime(dtpClaimDate.EditValue);
            dr["CLAIMDATE"] = dateClaimDate.ToString("yyyy-MM-dd");
            
            if (txtClaimSequence.Text.Equals(""))
            {
                dr["CLAIMSEQUENCE"] = 0;
            }
            else
            {
                dr["CLAIMSEQUENCE"] = txtClaimSequence.Text;
            }

            dr["CLAIMTYPE"] = cboClaimType.EditValue;
            dr["PRODUCTDEFID"] = usrProductdefid.CODE.EditValue;
            dr["PRODUCTDEFVERSION"] = usrProductdefid.VERSION.EditValue;
            dr["AREAID"] = popupOspAreaid.GetValue().ToString();
            dr["OSPVENDORID"] = _strOspVendorid;
            dr["DESCRIPTION"] = txtDescription.EditValue;
            dr["CLAIMAMOUNT"] = txtClaimAmount.EditValue;

        }

        /// <summary>
        /// 그리드의 Row Click시 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            focusedRowChanged();
        }

        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 코드그룹 리스트 그리드의 포커스 행 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focusedRowChanged();
        }

        /// <summary>
        /// 초기화 - 기타 외주 작업 내역을 초기화 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            _blchang = false;

            DateTime dateNow = DateTime.Now;
            dtpClaimDate.EditValue = dateNow.ToString("yyyy-MM-dd");

            
            txtClaimDepartment.EditValue = UserInfo.Current.Department.ToString();//현재로그인 부서
            txtClaimUsername.EditValue = UserInfo.Current.Name.ToString();//  현재로그인 유저정보
            txtClaimSequence.EditValue = "";
            cboClaimType.EditValue = "";
            usrProductdefid.CODE.EditValue = "";
            usrProductdefid.VERSION.EditValue = "";
            usrProductdefid.NAME.EditValue = "";

            popupOspAreaid.SetValue("");
            popupOspAreaid.EditValue = "";

            _strOspVendorid = "";
            txtOspVendorName.EditValue = "";
            txtClaimAmount.EditValue = 0;
            txtDescription.EditValue = "";
            controlEnableProcess(true);

            grdWorkTemp.View.ClearDatas();
            _blchang = true;

        }

        /// <summary>
        /// 저장 -기타 외주 작업 내역을 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {

            ////// 필수값 체크 로직 추가 처리 
            /// 
            if (cboClaimType.EditValue.ToString().Equals(""))
            {

                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField",lblClaimType.Text); //메세지 
                usrProductdefid.Focus();
                return;
            }
           
            //2.정산금액
            if (txtClaimAmount.Text.ToString().Equals("") || txtClaimAmount.Text.ToString().Equals("0"))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblClaimAmount.Text); //메세지 
                txtClaimAmount.Focus();
                return;
            }

            //3.협력사 필수값 체크 여부 
            if (popupOspAreaid.GetValue().ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblOspAreaid.Text); //메세지 
                popupOspAreaid.Focus();
                return;
            }

            DataRow drTemp = grdWorkTemp.View.GetDataRow(grdWorkTemp.View.FocusedRowHandle);
            string periodid = drTemp["PERIODID"].ToString();
            if (!(periodid.Equals("")))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspData014"); //메세지 
                return;
            }
            //상태값 체크 여부 추가 (의뢰만)
            DialogResult result = System.Windows.Forms.DialogResult.No;
            string strSeq = txtClaimSequence.Text;
            string strExpsettledate = dtpClaimDate.Text.ToString();

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnSave.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    btnClear.Enabled = false;
                    btnDelete.Enabled = false;
                    //datatable 생성 
                    DataTable dt = createSaveDatatable();
                    _blchang = true;
                    DetailControl_EditValueChanged(null, null);
                    DataRow dr = grdWorkTemp.View.GetDataRow(grdWorkTemp.View.FocusedRowHandle);

                    dr["VALIDSTATE"] = "Valid";
                    dt.ImportRow(dr);

                    DataTable saveResult = this.ExecuteRule<DataTable>("OtherOutsourcedClaimCostRegistration", dt);
                    DataRow resultData = saveResult.Rows[0];
                    string strtempSettlesequence = resultData["CLAIMSEQUENCE"].ToString();
                    txtClaimSequence.EditValue = strtempSettlesequence;

                    DateTime dateSettledate = Convert.ToDateTime(resultData["CLAIMDATE"].ToString());
                    dtpClaimDate.EditValue = dateSettledate.ToString("yyyy-MM-dd");
                    dtpClaimDate.Enabled = false;
                    ShowMessage("SuccessOspProcess");

                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();

                    btnSave.Enabled = true;
                    btnClear.Enabled = true;
                    btnDelete.Enabled = true;
                    //// 의뢰번호 삭제 처리된 목록 가져오기 
                    if (!(strSeq.Equals("")))
                    {
                        //재조회하기..
                        OnSaveConfrimSearch();

                        //해당row 값 가져오기 
                        int irow = GetGridRowSearch(strExpsettledate, strSeq);
                        if (irow >= 0)
                        {
                            grdOtherOutsourcedClaimCostRegistration.View.FocusedRowHandle = irow;
                            grdOtherOutsourcedClaimCostRegistration.View.SelectRow(irow);
                            focusedRowChanged();
                        }
                        else if (grdOtherOutsourcedClaimCostRegistration.View.DataRowCount > 0)
                        {
                            grdOtherOutsourcedClaimCostRegistration.View.FocusedRowHandle = 0;
                            grdOtherOutsourcedClaimCostRegistration.View.SelectRow(0);
                            focusedRowChanged();
                        }
                       
                    }
                }
            }

        }

        /// <summary>
        ///  삭제 -기타 외주 작업 내역을 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            //삭제 작업 처리 
            //의뢰번호 유무에 따른 처리 
            if (txtClaimSequence.EditValue.ToString().Equals(""))
            {
                BtnClear_Click(null, null);
                return;

            }

            DataRow drTemp = grdWorkTemp.View.GetDataRow(grdWorkTemp.View.FocusedRowHandle);
            string periodid = drTemp["PERIODID"].ToString();
            if (!(periodid.Equals("")))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspData014"); //메세지 
                return;
            }
            DialogResult result = System.Windows.Forms.DialogResult.No;
            string strSeq = txtClaimSequence.Text;
            string strExpsettledate = dtpClaimDate.Text.ToString();

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnDelete.Text);//삭제하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnDelete.Enabled = false;
                    btnClear.Enabled = false;
                    btnSave.Enabled = false;
                    //삭제여부 메세지 포함 
                    DataTable dt = createSaveDatatable();

                    _blchang = true;
                    DetailControl_EditValueChanged(null, null);
                    DataRow dr = grdWorkTemp.View.GetDataRow(grdWorkTemp.View.FocusedRowHandle);
                    _blchang = false;

                    dr["_STATE_"] = "deleted";
                    dr["VALIDSTATE"] = "Invalid";

                    dt.ImportRow(dr);

                    this.ExecuteRule("OtherOutsourcedClaimCostRegistration", dt);

                    ShowMessage("SuccessOspProcess"); //삭제 처리 하고 
                
                    BtnClear_Click(null, null);  //초기화

                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    btnSave.Enabled = true;
                    btnClear.Enabled = true;
                    btnDelete.Enabled = true;
                    //// 의뢰번호 삭제 처리된 목록 가져오기 
                    if (!(strSeq.Equals("")))
                    {
                        //재조회하기..
                        OnSaveConfrimSearch();
                        if (grdOtherOutsourcedClaimCostRegistration.View.DataRowCount > 0)
                        {
                            grdOtherOutsourcedClaimCostRegistration.View.FocusedRowHandle = 0;
                            grdOtherOutsourcedClaimCostRegistration.View.SelectRow(0);
                            focusedRowChanged();
                        }

                    }
                }
            }

        }
        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //  base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            //  DataTable changed = grdList.GetChangedRows();

            // ExecuteRule("SaveCodeClass", changed);
        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Initialization"))
            {

                BtnClear_Click(null, null);
            }
            if (btn.Name.ToString().Equals("Save"))
            {

                BtnSave_Click(null, null);
            }
            if (btn.Name.ToString().Equals("Delete"))
            {

                BtnDelete_Click(null, null);
            }

        }
        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            #region 기간 검색형 전환 처리 

            if (!(values["P_CLAIMDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime cliamDateFr = Convert.ToDateTime(values["P_CLAIMDATE_PERIODFR"]);
                values.Remove("P_CLAIMDATE_PERIODFR");
                values.Add("P_CLAIMDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", cliamDateFr));
            }
            if (!(values["P_CLAIMDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime cliamDateTo = Convert.ToDateTime(values["P_CLAIMDATE_PERIODTO"]);
                values.Remove("P_CLAIMDATE_PERIODTO");
                values.Add("P_CLAIMDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", cliamDateTo));
            }

         
            #endregion

            _strPlantid = values["P_PLANTID"].ToString();
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dt = await SqlExecuter.QueryAsync("GetOtherOutsourcedClaimCostRegistration", "10001", values);

            if (dt.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");

            }
            grdOtherOutsourcedClaimCostRegistration.DataSource = dt;
            if (grdOtherOutsourcedClaimCostRegistration.View.DataRowCount > 0)
            {
                grdOtherOutsourcedClaimCostRegistration.View.FocusedRowHandle = 0;
                grdOtherOutsourcedClaimCostRegistration.View.SelectRow(0);
                focusedRowChanged();
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //InitializeConditionPopup_Plant();

            InitializeConditionPopup_OspAreaid();
            // 작업업체 
            InitializeConditionPopup_OspVendorid();

            InitializeCondition_YesNo();

        }
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {
            var planttxtbox = Conditions.AddComboBox("p_plantid", new SqlQuery("GetPlantList", "00001", $"ENTERPRISEID={UserInfo.Current.Enterprise}",$"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
              .SetLabel("PLANT")
              .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
              .SetPosition(0.1)
              .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
              .SetIsReadOnly(true);
        }
        /// <summary>
        /// 작업장
        /// </summary>
        private void InitializeConditionPopup_OspAreaid()
        {
            // 팝업 컬럼설정
            var vendoridPopupColumn = Conditions.AddSelectPopup("p_areaid", new SqlQuery("GetAreaidPopupListByOsp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(520, 600)
               .SetLabel("AREANAME")
               .SetRelationIds("p_plantId")
               .SetPopupResultCount(1)
               .SetPosition(0.2);
            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("P_AREANAME")
                .SetLabel("AREANAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("AREAID", 120);
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("AREANAME", 200);


        }
        /// <summary>
        /// 작업업체 .고객 조회조건
        /// </summary>
        private void InitializeConditionPopup_OspVendorid()
        {
            // 팝업 컬럼설정
            var vendoridPopupColumn = Conditions.AddSelectPopup("p_ospvendorid", new SqlQuery("GetVendorListByOsp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "OSPVENDORNAME", "OSPVENDORID")
               .SetPopupLayout("OSPVENDORID", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("OSPVENDORID")
              .SetRelationIds("p_plantid", "p_areaid")
               .SetPopupResultCount(1)
               .SetPosition(0.4);
            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("OSPVENDORNAME")
                .SetLabel("OSPVENDORNAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);


            var txtOspVendor = Conditions.AddTextBox("P_OSPVENDORNAME")
             .SetLabel("OSPVENDORNAME")
             .SetPosition(0.5);
        }
        
        /// <summary>
        /// 확인여부
        /// </summary>
        private void InitializeCondition_YesNo()
        {
            var YesNobox = Conditions.AddComboBox("p_Closeyesno", new SqlQuery("GetCodeList", "00001", $"CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetLabel("CLOSEYESNO")
                 .SetPosition(2.8)
                 .SetEmptyItem("전체");

        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            SmartComboBox cboPlaint = Conditions.GetControl<SmartComboBox>("p_plantid");
            cboPlaint.EditValueChanged += cboPlaint_EditValueChanged;
        }
        private void cboPlaint_EditValueChanged(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();

            _strPlantid = values["P_PLANTID"].ToString();
            selectOspAreaidPopup(_strPlantid);
            if (btnSave.Enabled == false)
            {
                _blSaveAuth = false;
            }
            else
            {
                _blSaveAuth = true;
            }
            BtnClear_Click(null, null);
        }
        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경

        }

        #endregion

        #region Private Function
        /// <summary>
        /// 그리드의 포커스 행 변경시 행의 작업목록의 내용을 매핑처리 함.
        /// </summary>
        private void focusedRowChanged()
        {
            //포커스 행 체크 
            if (grdOtherOutsourcedClaimCostRegistration.View.FocusedRowHandle < 0) return;

            var row = grdOtherOutsourcedClaimCostRegistration.View.GetDataRow(grdOtherOutsourcedClaimCostRegistration.View.FocusedRowHandle);
            _blchang = false;
            txtClaimSequence.EditValue = row["CLAIMSEQUENCE"].ToString();
            DateTime dateClaimdate = Convert.ToDateTime(row["CLAIMDATE"].ToString());
            dtpClaimDate.EditValue = dateClaimdate.ToString("yyyy-MM-dd");
            txtClaimDepartment.EditValue = row["CLAIMDEPARTMENT"].ToString();
            txtClaimUsername.EditValue = row["CLAIMUSERNAME"].ToString();
            cboClaimType.EditValue = row["CLAIMTYPE"].ToString();
           
            usrProductdefid.CODE.EditValue = row["PRODUCTDEFID"].ToString();
            usrProductdefid.VERSION.EditValue = row["PRODUCTDEFVERSION"].ToString();
            usrProductdefid.NAME.EditValue = row["PRODUCTDEFNAME"].ToString();

            popupOspAreaid.SetValue(row["AREAID"].ToString());
            popupOspAreaid.EditValue = row["AREANAME"].ToString();

            _strOspVendorid  =row["OSPVENDORID"].ToString();
            txtOspVendorName.EditValue = row["OSPVENDORNAME"].ToString();

            txtEtcLotId.EditValue = row["ETCLOTID"].ToString();
            txtClaimAmount.EditValue = row["CLAIMAMOUNT"].ToString();
            txtDescription.EditValue = row["DESCRIPTION"].ToString();
            //DateTime dateClaimYM = Convert.ToDateTime(row["PERIODNAME"].ToString());
            //dtpClaimDate.EditValue = dateClaimYM.ToString("yyyy-MM");
            string strPeriodstate = row["PERIODSTATE"].ToString();
            string strrelkeyno = row["RELKEYNO"].ToString();  
            DataTable dtWorkTemp = createSaveDatatable();
            dtWorkTemp.ImportRow(row);
            grdWorkTemp.DataSource = dtWorkTemp;
            if (strPeriodstate.Equals("Open"))
            {
                if (strrelkeyno.Equals(""))
                {
                    controlEnableProcess(true);
                }
                else
                {
                    controlEnableProcess(false);
                }
               
            }
            else
            {
                controlEnableProcess(false);
            }
           
           
            _blchang = true;
        }
        /// <summary>
        /// 저장 후 재조회용 
        /// </summary>

        private void OnSaveConfrimSearch()
        {
           
            var values = Conditions.GetValues();

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            #region 기간 검색형 전환 처리 

            if (!(values["P_CLAIMDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime cliamDateFr = Convert.ToDateTime(values["P_CLAIMDATE_PERIODFR"]);
                values.Remove("P_CLAIMDATE_PERIODFR");
                values.Add("P_CLAIMDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", cliamDateFr));
            }
            if (!(values["P_CLAIMDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime cliamDateTo = Convert.ToDateTime(values["P_CLAIMDATE_PERIODTO"]);
                values.Remove("P_CLAIMDATE_PERIODTO");
                values.Add("P_CLAIMDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", cliamDateTo));
            }

            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dt = SqlExecuter.Query("GetOtherOutsourcedClaimCostRegistration", "10001", values);

            grdOtherOutsourcedClaimCostRegistration.DataSource = dt;

           
        }
        /// <summary>
        /// 그리드 이동에 필요한 row 찾기
        /// </summary>
        /// <param name="strExpsettledate"></param>  
        /// <param name="strSeq"></param>
        private int GetGridRowSearch(string strExpsettledate, string strSeq)
        {
            int iRow = -1;
            if (grdOtherOutsourcedClaimCostRegistration.View.DataRowCount == 0)
            {
                return iRow;
            }
            for (int i = 0; i < grdOtherOutsourcedClaimCostRegistration.View.DataRowCount; i++)
            {
                if (grdOtherOutsourcedClaimCostRegistration.View.GetRowCellValue(i, "CLAIMDATE").ToString().Equals(strExpsettledate)
                    && grdOtherOutsourcedClaimCostRegistration.View.GetRowCellValue(i, "CLAIMSEQUENCE").ToString().Equals(strSeq)
                    )
                {
                    return i;
                }
            }
            return iRow;
        }
        //
        /// <summary>
        /// 진행상태값에 따른 입력 항목 lock 처리
        /// </summary>
        /// <param name="blProcess"></param>
        private void controlEnableProcess(bool blProcess)
        {

            dtpClaimDate.Enabled = blProcess;
            txtClaimDepartment.Enabled = false;
            txtClaimUsername.Enabled = false;

            cboClaimType.Enabled = blProcess;  // 작업구분
            popupOspAreaid.Enabled = blProcess;

            txtClaimAmount.Enabled = blProcess;
            txtDescription.Enabled = blProcess;
            if (_blSaveAuth == true)
            {
                btnSave.Enabled = blProcess;
                btnDelete.Enabled = blProcess;
            }
            
        }

        /// <summary>
        /// 저장및  삭제용 data table 생성 
        /// </summary>
        /// <returns></returns>
        private DataTable createSaveDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "list";
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("CLAIMDATE", typeof(DateTime));
            dt.Columns.Add("CLAIMSEQUENCE");
            dt.Columns.Add("CLAIMTYPE");
            dt.Columns.Add("CLAIMDEPARTMENT");
            dt.Columns.Add("CLAIMUSER");
            dt.Columns.Add("AREAID");
            dt.Columns.Add("OSPVENDORID");
            dt.Columns.Add("PRODUCTDEFID");
            dt.Columns.Add("PRODUCTDEFVERSION");
            dt.Columns.Add("PERIODID");
            dt.Columns.Add("CLAIMAMOUNT");
            dt.Columns.Add("ETCLOTID");
            dt.Columns.Add("DESCRIPTION");
            dt.Columns.Add("VALIDSTATE");
            dt.Columns.Add("_STATE_");
            return dt;
        }


        #endregion

    }
}
