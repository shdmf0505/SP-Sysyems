#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraEditors.Repository;
using System.ComponentModel;
#endregion

namespace Micube.SmartMES.MaterialsManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 자재관리 > 자재재고관리 > 자재 불출 요청
    /// 업  무  설  명  : 자재 불출 요청 등록한다.
    /// 생    성    자  : 최별
    /// 생    성    일  : 2019-07-30
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class MaterialOutflowRequest : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        //bool blSave = true;  //저장 여부 확인 처리 
        private string _strPlantid = "";
        bool blIsuseplantauthority = true;
        #endregion

        #region 생성자

        public MaterialOutflowRequest()
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

            InitializeEvent();

           
            // 콤보박스 셋팅 
            InitializeComboBox();
          
            // popup 
            InitializePopup();
            if (pnlToolbar.Controls["layoutToolbar"] != null)
            {
               
                if (pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"] != null)
                    pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"].Enabled = true;
            }
            InitializeGridSearch();
            InitializeGridRequest();
            _strPlantid = UserInfo.Current.Plant.ToString();
            BtnClear_Click(null, null);
        }
        #region POPUP  

        /// <summary>
        /// popup 컨트롤 추가
        /// </summary>
        private void InitializePopup()
        {
            // 1.공정 
            selectProcesssegmentidPopup(); 
        }
        /// <summary>
        /// 공정 선택팝업
        /// </summary>
        private void selectProcesssegmentidPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(500, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "PROCESSSEGMENTID";
            popup.LabelText = "PROCESSSEGMENTID";
            popup.SearchQuery = new SqlQuery("GetProcessSegmentListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                                  , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                  );
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "PROCESSSEGMENTNAME";

            popup.ValueFieldName = "PROCESSSEGMENTID";
            popup.LanguageKey = "PROCESSSEGMENTID";

            popup.Conditions.AddTextBox("PROCESSSEGMENTNAME");

            popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetLabel("PROCESSSEGMENTID");
            popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200)
                .SetLabel("PROCESSSEGMENTNAME");

            popupProcesssegmentid.SelectPopupCondition = popup;
        }
        #endregion

        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
            // 1.작업장 
            selectOspAreaidPopup(UserInfo.Current.Plant.ToString());
            // 2.요청유형
            cboRequesttype.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboRequesttype.ValueMember = "CODEID";
            cboRequesttype.DisplayMember = "CODENAME";
            cboRequesttype.EditValue = "RQ0010";
            DataTable dtRequesttype = SqlExecuter.Query("GetCodeListByCsmConsumableRequestType", "10001"
                                      , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            cboRequesttype.DataSource = dtRequesttype;
            cboRequesttype.ShowHeader = false;

        }

        #region 작업장 combox
        /// <summary>
        /// 작업장 combox 
        /// </summary>
        /// <param name="sPlantid"></param>
        private void selectOspAreaidPopup(string sPlantid)
        {

            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(520, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "AREANAME";
            popup.LabelText = "AREANAME";
            popup.SearchQuery = new SqlQuery("GetAreaidListAuthorityByCsm", "10001", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                           , $"USERID={UserInfo.Current.Id}"
                                                                           , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                           , $"P_PLANTID={sPlantid}");
            popup.IsMultiGrid = false;

            popup.DisplayFieldName = "AREANAME";
            popup.ValueFieldName = "AREAID";
            popup.LanguageKey = "AREAID";
            popup.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                foreach (DataRow row in selectedRow)
                {
                    if (selectedRow.Count() > 0)
                    {
                        txtwarehouseid.Text = row["WAREHOUSEID"].ToString();
                        txtwarehousename.Text = row["WAREHOUSENAME"].ToString();
                        if (row["ISMODIFY"].ToString().Equals("Y"))
                        {
                            if (blIsuseplantauthority == true)
                            {
                                if (pnlToolbar.Controls["layoutToolbar"] != null)
                                {
                                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = true;
                                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"] != null)
                                        pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"].Enabled = true;
                                }
                            }
                            else
                            {
                                if (pnlToolbar.Controls["layoutToolbar"] != null)
                                {
                                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = false;
                                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"] != null)
                                        pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"].Enabled = true;
                                }
                            }
                        }
                        else
                        {


                            if (pnlToolbar.Controls["layoutToolbar"] != null)
                            {
                                if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                                    pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = false;
                                if (pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"] != null)
                                    pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"].Enabled = true;
                            }

                        }

                    }

                }
            });
            popup.Conditions.AddTextBox("P_AREANAME")
                .SetLabel("AREANAME");
            popup.GridColumns.AddTextBoxColumn("AREAID", 120)
                .SetLabel("AREAID");
            popup.GridColumns.AddTextBoxColumn("AREANAME", 200)
                .SetLabel("AREANAME");
            popup.GridColumns.AddTextBoxColumn("ISMODIFY", 200)
                .SetLabel("ISMODIFY")
                .SetIsHidden();
            popupOspAreaid.SelectPopupCondition = popup;
        }
        #endregion
        #endregion
        /// <summary>        
        /// 자재불출요청 목록 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGridSearch()
        {
            // TODO : 그리드 초기화 로직 추가
            grdSearch.GridButtonItem = GridButtonItem.Export| GridButtonItem.Expand | GridButtonItem.Restore;
            grdSearch.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
          
            grdSearch.View.SetIsReadOnly();
            grdSearch.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();
            grdSearch.View.AddTextBoxColumn("PLANTID", 150)
                .SetIsHidden();
            grdSearch.View.AddTextBoxColumn("CSMREQUESTDEPARTMENT", 150)
                .SetIsHidden();
            grdSearch.View.AddTextBoxColumn("DESCRIPTIONMAIN", 150)
                .SetIsHidden();
            grdSearch.View.AddTextBoxColumn("CSMREQUESTNO", 120);

            grdSearch.View.AddTextBoxColumn("CSMREQUESTDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly();
            grdSearch.View.AddTextBoxColumn("CSMREQUESTUSERNAME", 120);
            grdSearch.View.AddTextBoxColumn("ISMODIFY", 120)               .SetIsHidden();
            grdSearch.View.AddTextBoxColumn("WAREHOUSEID", 120)
               .SetIsHidden();
            grdSearch.View.AddTextBoxColumn("WAREHOUSENAME", 120);
            grdSearch.View.AddTextBoxColumn("AREAID", 120)
                .SetIsHidden();
            grdSearch.View.AddTextBoxColumn("AREANAME", 120);
  
            grdSearch.View.AddComboBoxColumn("CSMREQUESTTYPE", 150,
                new SqlQuery("GetCodeList", "00001", "CODECLASSID=ConsumableRequestType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));   //  
            grdSearch.View.AddTextBoxColumn("PROCESSSEGMENTID", 100);
            grdSearch.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120);
            grdSearch.View.AddTextBoxColumn("CSMREQUESTSEQUENCE", 120);
            grdSearch.View.AddTextBoxColumn("CONSUMABLEDEFID", 120);
            grdSearch.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80);
            grdSearch.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
            grdSearch.View.AddSpinEditColumn("QTY", 100)
                .SetDisplayFormat("#,###,##0.#####", MaskTypes.Numeric);
            grdSearch.View.AddTextBoxColumn("UNIT", 80);
            grdSearch.View.AddTextBoxColumn("MATREMARK", 300);
          
            grdSearch.View.PopulateColumns();
        }

        /// <summary>        
        /// 자재불출요청  그리드를 초기화한다.
        /// </summary>
        private void InitializeGridRequest()
        {
            // TODO : 그리드 초기화 로직 추가
            grdMaterial.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdMaterial.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdMaterial.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();
            grdMaterial.View.AddTextBoxColumn("PLANTID", 150)
                .SetIsHidden();
            grdMaterial.View.AddTextBoxColumn("REQUESTNO", 150)
                .SetIsHidden();
            grdMaterial.View.AddTextBoxColumn("REQUESTSEQUENCE", 150)
                .SetIsHidden();
            InitializeGrid_ConsumabledefidPopup();
            grdMaterial.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80)
                .SetIsReadOnly();
            grdMaterial.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200)
                .SetIsReadOnly();

            grdMaterial.View.AddTextBoxColumn("UNIT", 80)
                .SetIsReadOnly();
            grdMaterial.View.AddSpinEditColumn("QTY", 100)
                .SetValidationIsRequired()
                .SetTextAlignment(Micube.Framework.SmartControls.TextAlignment.Right)
                .SetDisplayFormat("#,###,##0.#####", MaskTypes.Numeric)
                .IsFloatValue = true;
            grdMaterial.View.AddTextBoxColumn("MATREMARK", 300);
            
            grdMaterial.View.SetKeyColumn("PLANTID", "ENTERPRISEID", "CONSUMABLEDEFID");

            grdMaterial.View.PopulateColumns();

            RepositoryItemTextEdit edit = new RepositoryItemTextEdit();
            edit.Mask.EditMask = "#,###,##0.#####";
            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

            grdMaterial.View.Columns["QTY"].ColumnEdit = edit;

        }
        /// <summary>
        /// grid 자재 정보 가져오기 
        /// </summary>
        private void InitializeGrid_ConsumabledefidPopup()
        {
            var popupGridConsumabledefid = this.grdMaterial.View.AddSelectPopupColumn("CONSUMABLEDEFID",120, new SqlQuery("GetConsumabledefinitionListByCsm", "10001"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("CONSUMABLEIDPOPUP", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("CONSUMABLEDEFID", "CONSUMABLEDEFID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(900, 600)
                // 그리드의 남은 영역을 채울 컬럼 설정
                .SetValidationIsRequired()
                .SetRelationIds("PLANTID", "ENTERPRISEID")
                //.SetPopupAutoFillColumns("CONSUMABLEDEFNAME")

                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    DataRow classRow = grdMaterial.View.GetFocusedDataRow();

                    foreach (DataRow row in selectedRows)
                    {
                        classRow["CONSUMABLEDEFNAME"] = row["CONSUMABLEDEFNAME"];
                        classRow["CONSUMABLEDEFVERSION"] = row["CONSUMABLEDEFVERSION"];
                        classRow["UNIT"] = row["UNIT"];
                      
                    }
                })

            ;

            // 팝업 조회조건


            popupGridConsumabledefid.Conditions.AddComboBox("P_CONSUMABLECLASSID",
                     new SqlQuery("GetConsumableclassListByCsm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CONSUMABLECLASSNAME", "CONSUMABLECLASSID")
                .SetLabel("CONSUMABLECLASSID")
                .SetEmptyItem("", "");
            

            popupGridConsumabledefid.Conditions.AddTextBox("CONSUMABLEDEFNAME")
                .SetLabel("CONSUMABLEDEFNAME");

            popupGridConsumabledefid.Conditions.AddTextBox("P_ENTERPRISEID")
                .SetPopupDefaultByGridColumnId("ENTERPRISEID")
                .SetLabel("")
                .SetIsHidden();
           
            popupGridConsumabledefid.Conditions.AddTextBox("LANGUAGETYPE")
                .SetLabel("")
                .SetDefault(UserInfo.Current.LanguageType)
                .SetIsHidden();
            // 팝업 그리드

            popupGridConsumabledefid.GridColumns.AddComboBoxColumn("CONSUMABLECLASSID", 100, new SqlQuery("GetConsumableclassListByCsm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CONSUMABLECLASSNAME", "CONSUMABLECLASSID")
                .SetIsReadOnly();
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150).SetIsReadOnly();
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80).SetIsReadOnly();
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 300).SetIsReadOnly();
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("UNIT", 80).SetIsHidden();
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("ISLOTMNG", 80).SetIsHidden();


        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
           
            // 버튼 이벤트 처리 
            btnClear.Click += BtnClear_Click;
            btnSave.Click += BtnSave_Click;
            btnDelete.Click += BtnDelete_Click;
            btnSlip.Click += BtnSlip_Click;
            // TODO : 화면에서 사용할 이벤트 추가
            popupOspAreaid.EditValueChanged += popupOspAreaid_EditValueChanged;
            grdMaterial.View.AddingNewRow += View_AddingNewRow;
            grdMaterial.View.ShowingEditor += View_ShowingEditor;
            grdSearch.View.FocusedRowChanged += View_FocusedRowChanged;
        }
        /// <summary>
		/// 셀 ReadOnly 처리
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (!(grdMaterial.View.GetFocusedRowCellValue("REQUESTNO").ToString().Equals("")))
            {
                e.Cancel = true;
            }
            //if (grdMaterial.View.FocusedColumn.FieldName.Equals("QTY") && grdMaterial.View.GetFocusedRowCellValue("ISLOTMNG").ToString().Equals("Y"))
            //{
            //    e.Cancel = true;
            //}

        }
        /// <summary>
        /// Areaid 제한 로직 추가 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupOspAreaid_EditValueChanged(object sender, EventArgs e)
        {


            if (popupOspAreaid.EditValue.ToString().Equals(""))
            {
                txtwarehouseid.Text = "";
                txtwarehousename.Text = "";
                return;
            }
           
        }
        /// <summary>
        /// 초기화 - 기타 외주 작업 내역을 초기화 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, EventArgs e)
        {
          
            txtRequestno.EditValue = ""; //의뢰번호 
            DateTime dateNow = DateTime.Now;
            
            txtRequestdepartment.EditValue = UserInfo.Current.Department.ToString();//현재로그인 부서
            txtRequestusername.EditValue = UserInfo.Current.Name.ToString();//  현재로그인 유저정보
            popupOspAreaid.SetValue("");
            popupOspAreaid.Text = "";
            popupOspAreaid.EditValue = "";

            popupProcesssegmentid.SetValue("");
            popupProcesssegmentid.Text = "";
            popupProcesssegmentid.EditValue = "";
            txtDescription.Text = "";
            txtwarehouseid.Text = "";
            txtwarehousename.Text = "";
            grdMaterial.View.ClearDatas();
            //blSave = true;
            controlReadOnlyProcess(false);
        }

        /// <summary>
        /// 저장 -기타 외주 작업 내역을 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            //작업장
            if (!(txtRequestno.Text.ToString().Equals("")))
            {
               
                return;
            }
            if (popupOspAreaid.GetValue().ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblAreaid.Text); //메세지 
                popupOspAreaid.Focus();
                return;
            }
            //요청유형
            if (cboRequesttype.EditValue.ToString().Equals(""))
            {

                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblRequesttype.Text); //메세지 
                cboRequesttype.Focus();
                return;
            }
            if (popupProcesssegmentid.GetValue().ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblProcesssegmentid.Text); //메세지 
                popupProcesssegmentid.Focus();
                return;
            }
            
            if (grdMaterial.View.DataRowCount ==0 )
            {
                //자재 불출 요청 내역이 존재하지 않습니다. 대체 (다국어)
                this.ShowMessage(MessageBoxButtons.OK, "InValidCsmData003", grdMaterial.Caption.ToString()); //메세지 
                return;
            }

            //자재불출 목록 리스트 체크 
            grdMaterial.View.FocusedRowHandle = grdMaterial.View.FocusedRowHandle;
            grdMaterial.View.FocusedColumn = grdMaterial.View.Columns["CONSUMABLEDEFNAME"];
            grdMaterial.View.ShowEditor();

            for (int i = 0; i < grdMaterial.View.DataRowCount; i++)
            {

                string strConsumabledefid = grdMaterial.View.GetRowCellValue(i, "CONSUMABLEDEFID").ToString();
                if (strConsumabledefid.Equals(""))
                {
                    string lblConsumabledefid = grdMaterial.View.Columns["CONSUMABLEDEFID"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblConsumabledefid); //메세지
                    return;
                }
                string strConsumabledefversion = grdMaterial.View.GetRowCellValue(i, "CONSUMABLEDEFVERSION").ToString();
                if (strConsumabledefversion.Equals(""))
                {
                    string lblConsumabledefid = grdMaterial.View.Columns["CONSUMABLEDEFVERSION"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblConsumabledefid); //메세지
                    return;
                }
                string strQty = grdMaterial.View.GetRowCellValue(i, "QTY").ToString();
                double dblQty = (strQty.ToString().Equals("") ? 0 : Convert.ToDouble(strQty)); //
                if (dblQty.Equals(0))
                {
                    string lblQty = grdMaterial.View.Columns["QTY"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                    return;
                }


            }

            //상태값 체크 여부 추가 (의뢰만)
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnSave.Text);//저장하시겠습니까? 
            string strRequestno = txtRequestno.Text;
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    btnClear.Enabled = false;
                    btnDelete.Enabled = false;
                    //datatable 생성 
                    DataTable dt = createSaveHeaderDatatable();
                    DataRow dr = dt.NewRow();
                    dr["PLANTID"] = _strPlantid;
                    dr["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
                    dr["REQUESTNO"] = txtRequestno.Text.ToString();
                    dr["AREAID"] = popupOspAreaid.GetValue().ToString();
                    dr["WAREHOUSEID"] = txtwarehouseid.Text.ToString();
                    dr["PROCESSSEGMENTID"] = popupProcesssegmentid.GetValue().ToString();
                    dr["PROCESSSEGMENTVERSION"] = "*";
                    dr["DESCRIPTION"] =txtDescription.Text.ToString();
                    dr["REQUESTTYPE"] =cboRequesttype.EditValue.ToString();
                    if (txtRequestno.Text.ToString().Equals(""))
                    {
                      
                        dr["_STATE_"] = "added";
                        dr["REQUESTUSER"] = UserInfo.Current.Id.ToString();
                        dr["REQUESTDEPARTMENT"] = UserInfo.Current.Department.ToString();
                    }
                    else
                    {
                        dr["_STATE_"] = "modified";
                    }
                    dr["VALIDSTATE"] = "Valid";
                   
                    dt.Rows.Add(dr);
                    DataTable dtSave = (grdMaterial.GetChangedRows() as DataTable).Clone();
                    dtSave.TableName = "listDetail";
                    dtSave.Columns["CONSUMABLEDEFID"].DataType = typeof(string);
                    dtSave.Columns["QTY"].DataType = typeof(double);
                    DataTable dtSavechang = grdMaterial.GetChangedRows();

                    for (int irow = 0; irow < dtSavechang.Rows.Count; irow++)
                    {
                        dr = dtSavechang.Rows[irow];
                        dtSave.ImportRow(dr);

                    }

                    dtSave.DefaultView.Sort = "_STATE_ desc ";
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    ds.Tables.Add(dtSave);
                    DataTable saveResult = this.ExecuteRule<DataTable>("MaterialOutflowRequest", ds);
                    DataRow resultData = saveResult.Rows[0];
                    string strtempRequestno = resultData.ItemArray[0].ToString();
                    //의뢰번호 셋팅 처리 
                    txtRequestno.EditValue = strtempRequestno;
                    ShowMessage("SuccessOspProcess");
                  
                    //// 수정된 작업 리스트 가져오기및 행 이동 처리 
                    if (!(strtempRequestno.Equals("")))
                    {
                        //재조회하기..
                        OnSaveConfrimSearch();
                        //해당row 값 가져오기 
                        int irow = GetGridRowSearch(strtempRequestno);
                        if (irow >= 0)
                        {
                            grdSearch.View.FocusedRowHandle = irow;
                            grdSearch.View.SelectRow(irow);
                            focusedRowChanged();
                        }
                        else if (grdSearch.View.DataRowCount > 0)
                        {
                            grdSearch.View.FocusedRowHandle = 0;
                            grdSearch.View.SelectRow(0);
                            focusedRowChanged();
                        }
                        else
                        {
                            BtnClear_Click(null, null);
                        }
                    }
                   
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
                    
                }
            }

        }
        /// <summary>
        ///  삭제 -기타 외주 작업 내역을 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSlip_Click(object sender, EventArgs e)
        {


            DataTable dtcheck = grdSearch.View.GetCheckedRows();

            if (dtcheck.Rows.Count == 0)
            {
                ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                return;
            }

          

            // 건수 재비교 처리해야함.
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnSlip.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    //this.ShowWaitArea();
                    //btnSlip.Enabled = false;
                    //btnSave.Enabled = false;
                    //btnClear.Enabled = false;
                    //btnDelete.Enabled = false;
                    DataView dvMapping = dtcheck.DefaultView;
                    dvMapping.Sort = "CSMREQUESTNO ASC";

                    DataTable _printData = dvMapping.ToTable();
                    DataRow dr = null;
                    string Requestno = "";
                    DataTable printSendData = _printData.Clone();

                    for (int irow = 0; irow < _printData.Rows.Count; irow++)
                    {
                        dr = dtcheck.Rows[irow];
                        if (!(Requestno.Equals(dr["CSMREQUESTNO"].ToString())))
                        {
                            Requestno = dr["CSMREQUESTNO"].ToString();
                            printSendData.ImportRow(dr);
                        }

                    }
                    if (printSendData.Rows.Count >0 )
                    {
                        PrintMain(printSendData);
                    }

                    // XtraReport report = XtraReport.FromStream(stream);
                    //owMessage("SuccessOspProcess");
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    //is.CloseWaitArea();
                    btnSlip.Enabled = true;
                    btnSave.Enabled = true;
                    btnClear.Enabled = true;
                    btnDelete.Enabled = true;

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
            if (txtRequestno.EditValue.ToString().Equals(""))
            {
                BtnClear_Click(null, null);
                return;

            }
           
            DialogResult result = System.Windows.Forms.DialogResult.No;
            string strRequestno = txtRequestno.Text;
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
                    DataTable dt = createSaveHeaderDatatable();
                    DataRow dr = dt.NewRow();
                    dr["PLANTID"] = _strPlantid;
                    dr["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
                    dr["REQUESTNO"] = txtRequestno.Text.ToString();
                    dr["AREAID"] = popupOspAreaid.GetValue().ToString();
                    dr["PROCESSSEGMENTID"] = popupProcesssegmentid.GetValue().ToString();
                    dr["PROCESSSEGMENTVERSION"] = "*";
                    dr["REQUESTTYPE"] = cboRequesttype.EditValue.ToString();
                    dr["_STATE_"] = "deleted";
                    dr["VALIDSTATE"] = "Invalid";
                    dt.Rows.Add(dr);
                    DataTable changed = (grdMaterial.DataSource as DataTable).Copy();
                    DataSet ds = new DataSet();

                    ds.Tables.Add(dt);
                    
                    changed.TableName = "listDetail";
                    ds.Tables.Add(changed);
                    this.ExecuteRule("MaterialOutflowRequest", ds);

                    ShowMessage("SuccessOspProcess"); //삭제 처리 하고 
                    ///this.OnSearchAsync();  //재조회 
                   // 의뢰번호 삭제 처리된 목록 가져오기 
                    if (!(strRequestno.Equals("")))
                    {
                        //재조회하기..
                        OnSaveConfrimSearch();
                        if (grdSearch.View.DataRowCount > 0)
                        {
                            grdSearch.View.FocusedRowHandle = 0;
                            grdSearch.View.SelectRow(0);
                            focusedRowChanged();
                        }
                        else
                        {
                            BtnClear_Click(null, null);
                        }
                    }
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
                   
                }
            }


        }
        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (!(txtRequestno.Text.ToString().Equals("")))
            {
                int intfocusRow = grdMaterial.View.FocusedRowHandle;
                grdMaterial.View.DeleteRow(intfocusRow);
                return;
            }
            var values = Conditions.GetValues();
            string _strPlantid = values["P_PLANTID"].ToString();
            grdMaterial.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdMaterial.View.SetFocusedRowCellValue("PLANTID", _strPlantid);// plantid
            grdMaterial.View.SetFocusedRowCellValue("REQUESTNO",txtRequestno.Text);
        }
        /// <summary>
        /// 그리드의 포커스 행 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focusedRowChanged();
        }
        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Save"))
            {

                BtnSave_Click(null, null);
            }
            if (btn.Name.ToString().Equals("Initialization"))
            {

                BtnClear_Click(null, null);
            }
            if (btn.Name.ToString().Equals("Delete"))
            {

                BtnDelete_Click(null, null);
            }
            if (btn.Name.ToString().Equals("Outputslip"))
            {

                BtnSlip_Click(null, null);
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
            values.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("USERID", UserInfo.Current.Id.ToString());
            #region 기간 검색형 전환 처리 
            if (!(values["P_CSMREQUESTDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_CSMREQUESTDATE_PERIODFR"]);
                values.Remove("P_CSMREQUESTDATE_PERIODFR");
                values.Add("P_CSMREQUESTDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_CSMREQUESTDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_CSMREQUESTDATE_PERIODTO"]);
                values.Remove("P_CSMREQUESTDATE_PERIODTO");
                values.Add("P_CSMREQUESTDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }
            #endregion

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dt = await SqlExecuter.QueryAsync("GetMaterialOutflowRequest", "10001", values);

            if (dt.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");

            }
            grdSearch.DataSource = dt;
           
            if (dt.Rows.Count > 0)
            {
                grdSearch.View.FocusedRowHandle = 0;
                grdSearch.View.SelectRow(0);
                focusedRowChanged();
            }
            string strPlantid = values["P_PLANTID"].ToString();

            if (!(_strPlantid.Equals(strPlantid)))
            {
                selectOspAreaidPopup(strPlantid);
                _strPlantid = strPlantid;
            }

        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //1.site
            //InitializeCondition_Plant();
            //2.작업장
            InitializeConditionPopup_Areaid();
            //3.요청기간

            //4.요청유형
            InitializeCondition_Requesttype();
            //5.자재코드
            InitializeConditionPopup_Consumabledefid();
        }
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeCondition_Plant()
        {

            var planttxtbox = Conditions.AddComboBox("p_plantid", new SqlQuery("GetPlantList", "00001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
               .SetPosition(0.1)
               .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
            ;
         

        }
        /// <summary>
        /// 작업장 조회조건
        /// </summary>
        private void InitializeConditionPopup_Areaid()
        {
            var popupProduct = Conditions.AddSelectPopup("p_areaid",
                                                              new SqlQuery("GetAreaidListAuthorityByCsm", "10001"
                                                                              , $"USERID={UserInfo.Current.Id}"
                                                                              , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                              , $"P_ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                              ), "AREANAME", "AREAID")
           .SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false)
           .SetPopupLayoutForm(520, 600)
           .SetLabel("AREANAME")
           .SetPopupResultCount(1)
           .SetRelationIds("p_plantid")
           .SetPosition(0.2);
            // 팝업 조회조건
            popupProduct.Conditions.AddTextBox("P_AREANAME")
                .SetLabel("AREANAME");
            popupProduct.GridColumns.AddTextBoxColumn("AREAID", 100)
                .SetValidationKeyColumn();
            popupProduct.GridColumns.AddTextBoxColumn("AREANAME", 200);

        }
       
        /// <summary>
        /// 4.요청유형
        /// </summary>
        private void InitializeCondition_Requesttype()
        {
            
            var Requesttypetxtbox = Conditions.AddComboBox("p_Requesttype", new SqlQuery("GetCodeListByCsmConsumableRequestType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetLabel("CSMREQUESTTYPE")
                .SetPosition(1.1)
                .SetEmptyItem("","")
            ;
           

        }
        /// <summary>
        /// 자재코드 조회조건
        /// </summary>
        private void InitializeConditionPopup_Consumabledefid()
        {
            var popupConsumabledef = Conditions.AddSelectPopup("p_Consumabledefid",
                                                                new SqlQuery("GetConsumabledefinitionListByCsm", "10001"
                                                                                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                                , $"P_ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                ), "CONSUMABLEDEFNAME", "CONSUMABLEDEFID")
                 .SetPopupLayout("CONSUMABLEDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                 .SetPopupLayoutForm(900, 600)
                 .SetLabel("CONSUMABLEDEFNAME")
                 .SetPopupResultCount(1)
                 .SetPosition(1.3);
            // 팝업 조회조건

            popupConsumabledef.Conditions.AddComboBox("P_CONSUMABLECLASSID",
                               new SqlQuery("GetConsumableclassListByCsm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CONSUMABLECLASSNAME", "CONSUMABLECLASSID")
                          .SetLabel("CONSUMABLECLASSID")
                          .SetEmptyItem("","");
                          //.SetDefault("RawMaterial");
            popupConsumabledef.Conditions.AddTextBox("P_CONSUMABLEDEFNAME")
                .SetLabel("CONSUMABLEDEFNAME");


            popupConsumabledef.GridColumns.AddComboBoxColumn("CONSUMABLECLASSID", 100, 
                new SqlQuery("GetConsumableclassListByCsm", "10001",  $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CONSUMABLECLASSNAME", "CONSUMABLECLASSID")
                .SetIsReadOnly();
            popupConsumabledef.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150).SetIsReadOnly();
            popupConsumabledef.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80).SetIsReadOnly();
            
            popupConsumabledef.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 300).SetIsReadOnly();

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
            if (pnlToolbar.Controls["layoutToolbar"] != null)
            {

                if (pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"] != null)
                    pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"].Enabled = true;
            }
            if (btnSave.Enabled==true )
            {
                blIsuseplantauthority = true;
            }
            else
            {
                blIsuseplantauthority = false;
            }

           
        }
        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            ////base.OnValidateContent();

        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }

        /// <summary>
        /// 그리드의 포커스 행 변경시 행의 작업목록의 내용을 매핑처리 함.
        /// </summary>
        private void focusedRowChanged()
        {
            //포커스 행 체크 
            if (grdSearch.View.FocusedRowHandle < 0) return;

            var row = grdSearch.View.GetDataRow(grdSearch.View.FocusedRowHandle);
            string strtempPlantid = row["PLANTID"].ToString();
            txtRequestno.EditValue = row["CSMREQUESTNO"].ToString();
          
            txtRequestdepartment.EditValue = row["CSMREQUESTDEPARTMENT"].ToString();
            txtRequestusername.EditValue = row["CSMREQUESTUSERNAME"].ToString();
           
            popupOspAreaid.SetValue(row["AREAID"].ToString());
            popupOspAreaid.Text = row["AREANAME"].ToString(); ;
            popupOspAreaid.EditValue = row["AREANAME"].ToString();

            popupProcesssegmentid.SetValue(row["PROCESSSEGMENTID"].ToString());
            popupProcesssegmentid.Text = row["PROCESSSEGMENTNAME"].ToString(); ;
            popupProcesssegmentid.EditValue = row["PROCESSSEGMENTNAME"].ToString();
            cboRequesttype.EditValue = row["CSMREQUESTTYPE"].ToString();
            txtDescription.EditValue = row["DESCRIPTIONMAIN"].ToString();
            txtwarehousename.EditValue = row["WAREHOUSENAME"].ToString();
            txtwarehouseid.EditValue = row["WAREHOUSEID"].ToString();
            if (row["ISMODIFY"].ToString().Equals("Y"))
            {
                if (blIsuseplantauthority == true)
                {
                    if (pnlToolbar.Controls["layoutToolbar"] != null)
                    {
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = true;
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"].Enabled = true;
                    }
                }
                else
                {
                    if (pnlToolbar.Controls["layoutToolbar"] != null)
                    {
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = false;
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"].Enabled = true;
                    }
                }
            }
            else
            {


                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = false;
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"] != null)
                        pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"].Enabled = true;
                }

            }

        
        //하단 그리드 조회용 
        OnDetailRequestnoSearch(strtempPlantid, row["CSMREQUESTNO"].ToString());
            //blSave = false;
            controlReadOnlyProcess(true);
            
        }

        /// <summary>
        /// 저장 후 재조회용 
        /// </summary>

        private void OnSaveConfrimSearch()
        {
           
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("USERID", UserInfo.Current.Id.ToString());
            #region 기간 검색형 전환 처리 
            if (!(values["P_CSMREQUESTDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_CSMREQUESTDATE_PERIODFR"]);
                values.Remove("P_CSMREQUESTDATE_PERIODFR");
                values.Add("P_CSMREQUESTDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_CSMREQUESTDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_CSMREQUESTDATE_PERIODTO"]);
                values.Remove("P_CSMREQUESTDATE_PERIODTO");
                values.Add("P_CSMREQUESTDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dt = SqlExecuter.Query("GetMaterialOutflowRequest", "10001", values);
            grdSearch.DataSource = dt;


           
        }


        /// <summary>
        /// 자재불출 요청 내역 하단 그리드 조회용 
        /// </summary>

        private void OnDetailRequestnoSearch(string strPlantid ,string strRequestno)
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();
               
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param.Add("P_PLANTID", strPlantid);
            Param.Add("P_REQUESTNO", strRequestno);
            Param.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dt = SqlExecuter.Query("GetMaterialOutflowRequestNoSearch", "10001", Param);
            grdMaterial.DataSource = dt;

        }

        /// <summary>
        /// 그리드 이동에 필요한 row 찾기
        /// </summary>
        /// <param name="strRequestno"></param>
        private int GetGridRowSearch(string strRequestno)
        {
            int iRow = -1;
            if (grdSearch.View.DataRowCount == 0)
            {
                return iRow;
            }
            for (int i = 0; i < grdSearch.View.DataRowCount; i++)
            {
                if (grdSearch.View.GetRowCellValue(i, "CSMREQUESTNO").ToString().Equals(strRequestno))
                {
                    return i;
                }
            }
            return iRow;
        }
        /// <summary>
        /// 복사시 기본테이블 생성
        /// </summary>
        /// <returns></returns>
        private DataTable createSaveHeaderDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "listMain";
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("AREAID");
            dt.Columns.Add("WAREHOUSEID");
            dt.Columns.Add("REQUESTNO");
            dt.Columns.Add("PROCESSSEGMENTID");
            dt.Columns.Add("PROCESSSEGMENTVERSION");
            dt.Columns.Add("REQUESTUSER");
            dt.Columns.Add("REQUESTDEPARTMENT");
            dt.Columns.Add("REQUESTTYPE");
            dt.Columns.Add("VALIDSTATE");
            dt.Columns.Add("DESCRIPTION");
            dt.Columns.Add("_STATE_");
            return dt;
        }
        /// <summary>
        /// 자재기타 입고 (header) 입력 항목 lock 처리
        /// </summary>
        /// <param name="blProcess"></param>
        private void controlReadOnlyProcess(bool blProcess)
        {
            
            if (blProcess == true)
            {
                popupOspAreaid.Enabled = false;
                popupProcesssegmentid.Enabled = false;
                cboRequesttype.Enabled = false;
            }
            else
            {
                popupOspAreaid.Enabled = true;
                popupProcesssegmentid.Enabled = true;
                cboRequesttype.Enabled = true;
            }
            popupOspAreaid.ReadOnly = blProcess;
            popupProcesssegmentid.ReadOnly = blProcess;
        }
        private void PrintMain(DataTable dtPrint)
        {
           
            XtraReport printReport = new XtraReport();
            string requestNo = "";
            Assembly assembly = Assembly.GetAssembly(this.GetType());
            Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.MaterialsManagement.ReportCsm.RequestConsumableRelease.repx");
            for (int irow = 0; irow < dtPrint.Rows.Count; irow++)
            {
                DataRow row = dtPrint.Rows[irow];
                requestNo = row["CSMREQUESTNO"].ToString();
                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("REQUESTNO", row["CSMREQUESTNO"].ToString());
                Param.Add("PLANTID", row["PLANTID"].ToString());
                Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                Param = Commons.CommonFunction.ConvertParameter(Param);
                DataTable dtHeaderInfo2 = SqlExecuter.Query("SelectPrintChitHeaderInfo", "10001", Param);
                DataTable dtTableInfo2 = SqlExecuter.Query("SelectPrintChitDetailTable", "10001", Param);
                
                XtraReport[] reports = null;
                DataTable[] tableInfoByPage = null;

                int cnt = dtTableInfo2.Rows.Count;
                int rest = cnt % 9;
                int val = cnt / 9;

                reports = val.Equals(0) ? new XtraReport[1] : (rest.Equals(0) ? new XtraReport[val] : new XtraReport[val + 1]);
                tableInfoByPage = val.Equals(0) ? new DataTable[1] : (rest.Equals(0) ? new DataTable[val] : new DataTable[val + 1]);
                int virtualVal = val.Equals(0) ? 1 : (rest.Equals(0) ? val : val + 1);

                for (int i = 0; i < virtualVal; i++)
                {
                    reports[i] = XtraReport.FromStream(stream);
                    tableInfoByPage[i] = dtTableInfo2.Clone();

                    if ((i < val) || (virtualVal.Equals(1) && rest.Equals(0)))
                    {
                        tableInfoByPage[i] = TableDataDivide(dtTableInfo2, tableInfoByPage[i], i * 9, 9);
                    }
                    else if (!val.Equals(virtualVal))
                    {
                        tableInfoByPage[i] = TableDataDivide(dtTableInfo2, tableInfoByPage[i], i * 9, rest);
                    }

                    //빈 row 채우기
                    if (rest > 0 && (i + 1) == virtualVal)
                    {
                        for (int k = 0; k < (9 - rest); k++)
                        {
                            DataRow newRow = tableInfoByPage[i].NewRow();
                            newRow["REQUESTNO"] = requestNo;

                            tableInfoByPage[i].Rows.Add(newRow);
                        }
                    }

                    DataTable headerInfoByPage = dtHeaderInfo2.Copy();

                    DataSet dsReport = new DataSet();
                    headerInfoByPage.TableName = "HeaderInfo";
                    tableInfoByPage[i].TableName = "TableInfo";
                    dsReport.Tables.Add(headerInfoByPage);
                    dsReport.Tables.Add(tableInfoByPage[i]);

                    DataRelation relation = new DataRelation("RelationRequestNo", headerInfoByPage.Columns["REQUESTNO"], tableInfoByPage[i].Columns["REQUESTNO"]);
                    dsReport.Relations.Add(relation);

                    printReport.Pages.Add(SetReortPageData(reports[i], dsReport).FirstOrDefault());
                }

            }
            printReport.ShowPreviewDialog();
        }

        private DataTable TableDataDivide(DataTable target, DataTable input, int stdCnt, int interval)
        {
            for (int i = stdCnt; i < (stdCnt + interval); i++)
            {
                DataRow newRow = input.NewRow();
                newRow.ItemArray = target.Rows[i].ItemArray;
                input.Rows.Add(newRow);
            }

            return input;
        }

        /// <summary>
		/// 자재불출요청 page별 데이터 바인드
		/// </summary>
		/// <param name="report"></param>
		/// <param name="ds"></param>
		/// <returns></returns>
		private  PageList SetReortPageData(XtraReport report, DataSet ds)
        {
            report.DataSource = ds;
            report.DataMember = "HeaderInfo";

            //회수용			
            DetailReportBand detailReport = report.Bands["DetailReport"] as DetailReportBand;
            detailReport.DataSource = ds;
            detailReport.DataMember = "RelationRequestNo";

            Band groupHeader = detailReport.Bands["GroupHeader1"];
            SetReportControlDataBinding(groupHeader.Controls, ds, "");

            Band detailBand = detailReport.Bands["Detail1"];
            SetReportControlDataBinding(detailBand.Controls, ds, "RelationRequestNo");

            setLabelLaungage(detailReport, "GroupHeader1");

            //불출용
            DetailReportBand detailReport1 = report.Bands["DetailReport1"] as DetailReportBand;
            detailReport1.DataSource = ds;
            detailReport1.DataMember = "RelationRequestNo";

            Band groupHeader1 = detailReport1.Bands["GroupHeader2"];
            SetReportControlDataBinding(groupHeader1.Controls, ds, "");

            Band detailBand1 = detailReport1.Bands["Detail2"];
            SetReportControlDataBinding(detailBand1.Controls, ds, "RelationRequestNo");

            setLabelLaungage(detailReport1, "GroupHeader2");

            report.CreateDocument();

            return report.Pages;
        }

        /// <summary>
        /// Report 파일의 컨트롤 중 Tag(FieldName) 값이 있는 컨트롤에 DataBinding(Text)를 추가한다.
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="dataSource"></param>
        private void SetReportControlDataBinding(XRControlCollection controls, DataSet dataSource, string relationId)
        {
            if (controls.Count > 0)
            {
                foreach (XRControl control in controls)
                {
                    if (!string.IsNullOrWhiteSpace(control.Tag.ToString()) && !control.Name.Substring(0, 3).Equals("lbl"))
                    {
                        string fieldName = "";

                        if (!string.IsNullOrWhiteSpace(relationId))
                            fieldName = string.Join(".", relationId, control.Tag.ToString());
                        else
                            fieldName = control.Tag.ToString();

                        control.DataBindings.Add("Text", dataSource, fieldName);
                    }

                    SetReportControlDataBinding(control.Controls, dataSource, relationId);
                }
            }
        }

        /// <summary>
        /// 다국어 명 적용
        /// </summary>
        /// <param name="band"></param>
        private  void setLabelLaungage(object band, string strGroupHeader = "GroupHeader1")
        {
            if (band is DetailReportBand)
            {
                DetailReportBand detailReport = band as DetailReportBand;
                Band groupHeader = detailReport.Bands[strGroupHeader];

                foreach (XRControl control in groupHeader.Controls)
                {
                    if (control is DevExpress.XtraReports.UI.XRLabel)
                    {
                        if (!string.IsNullOrEmpty(control.Tag.ToString()))
                        {
                            if (control.Name.Substring(0, 3).Equals("lbl"))
                            {
                                string bindText = Language.Get(control.Tag.ToString());
                                Font ft = BestSizeEstimator.GetFontToFitBounds(control as XRLabel, bindText);
                                if (ft.Size < control.Font.Size)
                                {
                                    control.Font = ft;
                                }

                                control.Text = bindText;
                            }
                        }
                    }
                    else if (control is DevExpress.XtraReports.UI.XRTable)
                    {
                        XRTable xt = control as XRTable;

                        foreach (XRTableRow tr in xt.Rows)
                        {
                            for (int i = 0; i < tr.Cells.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(tr.Cells[i].Tag.ToString()))
                                {
                                    tr.Cells[i].Text = Language.Get(tr.Cells[i].Tag.ToString());

                                }

                            }
                        }

                    }
                }

            }

        }
        #endregion
    }
}
