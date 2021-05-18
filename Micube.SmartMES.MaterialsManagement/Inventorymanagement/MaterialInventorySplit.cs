#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

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
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using Micube.SmartMES.MaterialsManagement.Popup;
#endregion

namespace Micube.SmartMES.MaterialsManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 자재관리 > 자재재고관리 > 자재 재고 조정
    /// 업  무  설  명  : 자재 재고 조정를 관리한다.
    /// 생    성    자  : 최별
    /// 생    성    일  : 2019-07-30
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class MaterialInventorySplit : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        string _sWorkTime = "";
        bool blSave = true;  //저장 여부 확인 처리 
        bool blSavePeriodid = true;  //재고마감여부
        bool blIsuseplantauthority = true;
        DataTable _dtSite = null;
        string _strDateChangeAuth = "";
        #endregion

        #region 생성자

        public MaterialInventorySplit()
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
            _strDateChangeAuth = "false";
            if (pnlToolbar.Controls["layoutToolbar"] != null)
            {
                if (pnlToolbar.Controls["layoutToolbar"].Controls["DateChangeAuth"] != null)
                {
                    //if (pnlToolbar.Controls["layoutToolbar"].Controls["DateChangeAuth"].Visible == true)
                    {
                        pnlToolbar.Controls["layoutToolbar"].Controls["DateChangeAuth"].Visible = false;
                        _strDateChangeAuth = "true";
                    }
                }

            }
            InitializeEvent();
            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
            // SmartComboBox 컨트롤 추가
            InitializeComboBox();
            //WORKTIME 정보 가져오기 
            OnPlantidInformationSearch(cboPlantid.EditValue.ToString());
            //화면 clear
            BtnClear_Click(null, null);
        }

        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
            // plantid
            cboPlantid.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboPlantid.ValueMember = "PLANTID";
            cboPlantid.DisplayMember = "PLANTID";
            cboPlantid.EditValue = UserInfo.Current.Plant.ToString();
            _dtSite = SqlExecuter.Query("GetPlantListAuthorityBycms", "10001"
                                                       , new Dictionary<string, object>() { { "USERID", UserInfo.Current.Id }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });
            cboPlantid.DataSource = _dtSite;
            cboPlantid.ShowHeader = false;
            DataRow[] rowsPlant = _dtSite.Select("PLANTID = '" + UserInfo.Current.Plant.ToString() + "'");

            if (rowsPlant.Length > 0)
            {

                if (rowsPlant[0]["ISUSEPLANTAUTHORITY"].ToString().Equals("Y"))
                {
                    blIsuseplantauthority = true;
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
                    blIsuseplantauthority = false;
                    if (pnlToolbar.Controls["layoutToolbar"] != null)
                    {
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = false;
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"].Enabled = false;
                    }
                }

            }
            selectOspAreaidPopup(UserInfo.Current.Plant.ToString());


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
            //popup.SearchQuery = new SqlQuery("GetAreaidListByCsm", "10001", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}"
            //                                                               , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
            //                                                               , $"P_PLANTID={sPlantid}");
            popup.SearchQuery = new SqlQuery("GetAreaidListAuthorityByCsm", "10001", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                        , $"USERID={UserInfo.Current.Id}"
                                                                        , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                        , $"P_PLANTID={sPlantid}");
            popup.IsMultiGrid = false;
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
            popup.DisplayFieldName = "AREANAME";
            popup.ValueFieldName = "AREAID";
            popup.LanguageKey = "AREAID";

            popup.Conditions.AddTextBox("P_AREANAME")
                .SetLabel("AREANAME");
            popup.GridColumns.AddTextBoxColumn("AREAID", 120)
                .SetLabel("AREAID");
            popup.GridColumns.AddTextBoxColumn("AREANAME", 200)
                .SetLabel("AREANAME");

            popupOspAreaid.SelectPopupCondition = popup;
        }
        #endregion

        #endregion
        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdMaterial.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
            grdMaterial.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdMaterial.View.IsUsePasteReadOnlyColumn = true;
            grdMaterial.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();
            grdMaterial.View.AddTextBoxColumn("PLANTID", 150)
                .SetIsHidden();
            grdMaterial.View.AddTextBoxColumn("TRANSACTIONTYPE", 150)
                .SetIsHidden();
            grdMaterial.View.AddTextBoxColumn("TRANSACTIONNO", 150)
                .SetIsHidden();
            grdMaterial.View.AddTextBoxColumn("TRANSACTIONDATE", 150)
                .SetIsHidden();
            grdMaterial.View.AddTextBoxColumn("TRANSACTIONSEQUENCE", 80)
                .SetIsHidden();
            InitializeGrid_ConsumabledefidPopup();
            grdMaterial.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80)
                .SetIsReadOnly();
            grdMaterial.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200)
                .SetIsReadOnly()
                ;

            grdMaterial.View.AddTextBoxColumn("CONSUMABLELOTID", 150)
                .SetIsReadOnly()
                .SetValidationIsRequired();
            grdMaterial.View.AddSpinEditColumn("STOCKQTY", 100)
                .SetDisplayFormat("#,###,##0.#####", MaskTypes.Numeric)
                .SetIsReadOnly()
                .IsFloatValue = true
                ;
            grdMaterial.View.AddTextBoxColumn("AVAILABLEQTY", 100)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,###,##0.#####", MaskTypes.Numeric)
               .SetIsReadOnly();
            grdMaterial.View.AddSpinEditColumn("QTY", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###,##0.#####", MaskTypes.Numeric)
                .SetLabel("SPLITQTY")
                .IsFloatValue = true
                ;
            grdMaterial.View.AddSpinEditColumn("ADJSTOCKQTY", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###,##0.#####", MaskTypes.Numeric)
                .SetIsReadOnly();
            grdMaterial.View.AddTextBoxColumn("UNIT", 80)
                .SetIsReadOnly();
            grdMaterial.View.AddTextBoxColumn("FROMAREAID", 150)
                .SetIsHidden();
            grdMaterial.View.AddTextBoxColumn("FROMWAREHOUSEID", 150)
               .SetIsHidden();
            grdMaterial.View.AddTextBoxColumn("FROMWAREHOUSENAME", 150)
               .SetIsHidden();
            grdMaterial.View.AddTextBoxColumn("FROMAREANAME", 150)
                .SetIsHidden();
            grdMaterial.View.AddTextBoxColumn("TRANSACTIONCODE", 150)
                .SetIsHidden();
            //this.InitializeGrid_ReasonCodePopup();
            //grdMaterial.View.AddTextBoxColumn("TRANSACTIONREASONCODENAME", 150);
            grdMaterial.View.AddTextBoxColumn("RELCONSUMABLELOTID",200)
                .SetValidationIsRequired(); 
            
            grdMaterial.View.AddTextBoxColumn("MATREMARK", 300);
            grdMaterial.View.AddTextBoxColumn("ISLOTMNG", 150)
                .SetIsHidden();

            grdMaterial.View.AddTextBoxColumn("YEARMONTH", 150)
                .SetIsHidden();
            grdMaterial.View.AddTextBoxColumn("STARTDATE", 150)
                .SetIsHidden();
            grdMaterial.View.AddTextBoxColumn("ENDDATE", 150)
                .SetIsHidden();
            grdMaterial.View.AddTextBoxColumn("WORKTIME", 150)
                .SetIsHidden();

            grdMaterial.View.SetKeyColumn("PLANTID", "ENTERPRISEID", "CONSUMABLEDEFID", "CONSUMABLELOTID");
            grdMaterial.View.PopulateColumns();
            RepositoryItemTextEdit edit = new RepositoryItemTextEdit();
            edit.Mask.EditMask = "#,###,##0.#####";
            edit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

            grdMaterial.View.Columns["QTY"].ColumnEdit = edit;
            grdMaterial.View.Columns["ADJSTOCKQTY"].ColumnEdit = edit;

        }

        /// grid_Costcentercod 정보 가져오기 
        /// </summary>
        private void InitializeGrid_CostcentercodePopup()
        {
            var popupGridReasonCode = this.grdMaterial.View.AddSelectPopupColumn("COSTCENTERCODE", new SqlQuery("GetCostcentercodeByCsm", "10001"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("COSTCENTERNAME", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("COSTCENTERCODE", "COSTCENTERCODE")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정

                .SetRelationIds("PLANTID", "FROMAREAID")

                .SetValidationIsRequired()
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    DataRow classRow = grdMaterial.View.GetFocusedDataRow();

                    foreach (DataRow row in selectedRows)
                    {
                        classRow["COSTCENTERNAME"] = row["COSTCENTERNAME"];

                    }
                })

            ;
            // 팝업에서 사용할 조회조건 항목 추가 
            popupGridReasonCode.Conditions.AddTextBox("COSTCENTERNAME");

            popupGridReasonCode.Conditions.AddTextBox("P_AREAID")
                .SetPopupDefaultByGridColumnId("FROMAREAID")
                .SetLabel("")
                .SetIsHidden();


            // 팝업 그리드 설정
            popupGridReasonCode.GridColumns.AddTextBoxColumn("COSTCENTERCODE", 100);
            popupGridReasonCode.GridColumns.AddTextBoxColumn("COSTCENTERNAME", 250);


        }
        /// <summary>
        /// grid 자재 정보 가져오기 
        /// </summary>
        private void InitializeGrid_ConsumabledefidPopup()
        {
            var popupGridConsumabledefid = this.grdMaterial.View.AddSelectPopupColumn("CONSUMABLEDEFID", 120, new SqlQuery("GetConsumabledefinitionStockQtyOnlyListByCsm", "10001"
                                                                            , $"ISLOTMNG={"Y"}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("CONSUMABLEIDPOPUP", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(0)
                .SetNotUseMultiColumnPaste()
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                // .SetPopupResultMapping("CONSUMABLEDEFID", "CONSUMABLEDEFID")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(1200, 600)
                // 그리드의 남은 영역을 채울 컬럼 설정
                .SetValidationIsRequired()
                .SetRelationIds("PLANTID", "ENTERPRISEID", "FROMWAREHOUSEID", "YEARMONTH", "STARTDATE", "ENDDATE", "ISLOTMNG")
                //.SetPopupAutoFillColumns("CONSUMABLEDEFNAME")

                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {

                    int irow = 0;
                    int crow = 0;
                    DataRow classRow = grdMaterial.View.GetFocusedDataRow();
                    crow = grdMaterial.View.FocusedRowHandle;

                    foreach (DataRow row in selectedRows)
                    {
                        if (irow == 0)
                        {
                            string sConsumabledefid = row["CONSUMABLEDEFID"].ToString();
                            string sConsumabledefversion = row["CONSUMABLEDEFVERSION"].ToString();
                            string sConsumablelotid = row["CONSUMABLELOTID"].ToString();

                            int icheck = checkGridConsumabledefid(sConsumabledefid, sConsumabledefversion, sConsumablelotid);
                            if (icheck == -1)
                            {
                                classRow["CONSUMABLEDEFID"] = row["CONSUMABLEDEFID"];
                                classRow["CONSUMABLEDEFNAME"] = row["CONSUMABLEDEFNAME"];
                                classRow["CONSUMABLEDEFVERSION"] = row["CONSUMABLEDEFVERSION"];
                                classRow["CONSUMABLELOTID"] = row["CONSUMABLELOTID"];
                                classRow["ISLOTMNG"] = row["ISLOTMNG"];
                                classRow["UNIT"] = row["UNIT"];
                                classRow["STOCKQTY"] = row["STOCKQTY"];
                                classRow["AVAILABLEQTY"] = row["AVAILABLEQTY"];
                                classRow["QTY"] = 0d;
                                classRow["ADJSTOCKQTY"] = row["STOCKQTY"];
                                grdMaterial.View.RaiseValidateRow(crow);
                            }
                            else
                            {
                                classRow["CONSUMABLEDEFID"] = "";
                                classRow["CONSUMABLEDEFNAME"] ="";
                                classRow["CONSUMABLEDEFVERSION"] = "";
                                classRow["CONSUMABLELOTID"] ="";
                                classRow["UNIT"] ="";
                                classRow["STOCKQTY"] = 0d;
                                classRow["AVAILABLEQTY"] = 0d;
                                classRow["QTY"] = 0d;
                                classRow["ADJSTOCKQTY"] = 0d;
                                irow = irow - 1;
                            }
                        }
                        else
                        {
                            popAddGrid(row);
                        }
                        irow = irow + 1;
                    }
                })

            ;

            // 팝업 조회조건


            popupGridConsumabledefid.Conditions.AddComboBox("P_CONSUMABLECLASSID",
                     new SqlQuery("GetConsumableclassListByCsm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CONSUMABLECLASSNAME", "CONSUMABLECLASSID")
                .SetLabel("CONSUMABLECLASSID")
                .SetEmptyItem("", "");

            popupGridConsumabledefid.Conditions.AddTextBox("P_CONSUMABLEDEFID")
               .SetLabel("CONSUMABLEDEFID");

            popupGridConsumabledefid.Conditions.AddTextBox("P_CONSUMABLEDEFNAME")
                .SetLabel("CONSUMABLEDEFNAME");

            popupGridConsumabledefid.Conditions.AddTextBox("P_CONSUMABLELOTID")
                .SetLabel("CONSUMABLELOTID");

            popupGridConsumabledefid.Conditions.AddTextBox("P_ENTERPRISEID")
                .SetPopupDefaultByGridColumnId("ENTERPRISEID")
                .SetLabel("")
                .SetIsHidden();
            popupGridConsumabledefid.Conditions.AddTextBox("P_PLANTID")
                .SetPopupDefaultByGridColumnId("PLANTID")
                .SetLabel("")
                .SetIsHidden();
            popupGridConsumabledefid.Conditions.AddTextBox("P_FROMWAREHOUSEID")
                .SetPopupDefaultByGridColumnId("FROMWAREHOUSEID")
                .SetLabel("")
                .SetIsHidden();
            popupGridConsumabledefid.Conditions.AddTextBox("P_YEARMONTH")
                .SetPopupDefaultByGridColumnId("YEARMONTH")
                .SetIsHidden();
            popupGridConsumabledefid.Conditions.AddTextBox("P_STARTDATE")
                .SetPopupDefaultByGridColumnId("STARTDATE")
                .SetLabel("")
                .SetIsHidden();
            popupGridConsumabledefid.Conditions.AddTextBox("P_ENDDATE")
                .SetPopupDefaultByGridColumnId("ENDDATE")
                .SetLabel("")
                .SetIsHidden();
            popupGridConsumabledefid.Conditions.AddTextBox("LANGUAGETYPE")
                .SetLabel("")
                .SetDefault(UserInfo.Current.LanguageType)
                .SetIsHidden();
            ////// 팝업 그리드

            popupGridConsumabledefid.GridColumns.AddComboBoxColumn("CONSUMABLECLASSID", 100, new SqlQuery("GetConsumableclassListByCsm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CONSUMABLECLASSNAME", "CONSUMABLECLASSID")
                .SetIsReadOnly();
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 100).SetIsReadOnly();
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80).SetIsReadOnly();
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 270).SetIsReadOnly();
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("CONSUMABLELOTID", 200).SetIsReadOnly();
            popupGridConsumabledefid.GridColumns.AddSpinEditColumn("STOCKQTY",100)
                .SetDisplayFormat("#,###,##0.#####", MaskTypes.Numeric)
                .SetIsReadOnly();
            popupGridConsumabledefid.GridColumns.AddSpinEditColumn("AVAILABLEQTY", 100)
               .SetDisplayFormat("#,###,##0.#####", MaskTypes.Numeric)
               .SetIsReadOnly();
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("UNIT", 80).SetIsHidden();
            popupGridConsumabledefid.GridColumns.AddTextBoxColumn("ISLOTMNG", 80).SetIsHidden();


        }
        /// <summary>
        /// grid _ReasonCode 정보 가져오기 
        /// </summary>
        private void InitializeGrid_ReasonCodePopup()
        {
            var popupGridReasonCode = this.grdMaterial.View.AddSelectPopupColumn("TRANSACTIONREASONCODE", new SqlQuery("GetTransactionreasoncodeByCsm", "10001"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("TRANSACTIONREASONCODE", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("TRANSACTIONREASONCODE", "TRANSACTIONREASONCODE")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정
                .SetValidationIsRequired()
                .SetRelationIds("PLANTID", "ENTERPRISEID", "TRANSACTIONCODE")
                .SetPopupAutoFillColumns("TRANSACTIONREASONCODENAME")

                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    DataRow classRow = grdMaterial.View.GetFocusedDataRow();

                    foreach (DataRow row in selectedRows)
                    {
                        classRow["TRANSACTIONREASONCODENAME"] = row["TRANSACTIONREASONCODENAME"];

                    }
                })

            ;
            // 팝업에서 사용할 조회조건 항목 추가 

            popupGridReasonCode.Conditions.AddTextBox("TRANSACTIONREASONCODENAME");
            popupGridReasonCode.Conditions.AddTextBox("P_PLANTID")
                .SetPopupDefaultByGridColumnId("PLANTID")
                 .SetLabel("")
                .SetIsHidden();
            popupGridReasonCode.Conditions.AddTextBox("P_TRANSACTIONCODE")
                .SetPopupDefaultByGridColumnId("TRANSACTIONCODE")
                 .SetLabel("")
                .SetIsHidden();

            // 팝업 그리드 설정
            popupGridReasonCode.GridColumns.AddTextBoxColumn("TRANSACTIONREASONCODE", 150);
            popupGridReasonCode.GridColumns.AddTextBoxColumn("TRANSACTIONREASONCODENAME", 200);


        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdMaterial.View.AddingNewRow += View_AddingNewRow;
            grdMaterial.View.CellValueChanged += View_CellValueChanged;
            grdMaterial.View.ShowingEditor += View_ShowingEditor;
            //grdMaterial.View.KeyPress += View_KeyPress;
            btnClear.Click += BtnClear_Click;
            btnSave.Click += BtnSave_Click;

            btnTransactionno.ButtonClick += BtnTransactionno_ButtonClick;
            cboPlantid.EditValueChanged += CboPlantid_EditValueChanged;
            popupOspAreaid.EditValueChanged += popupOspAreaid_EditValueChanged;
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
            if (grdMaterial.View.RowCount == 0) return;
            DataTable dtview = grdMaterial.DataSource as DataTable;

            DataRow[] drviw = dtview.Select("FROMAREAID <> '" + popupOspAreaid.GetValue().ToString() + "' AND CONSUMABLEDEFID <> '' ");
            if (drviw.Length > 0)
            {
                popupOspAreaid.EditValueChanged -= popupOspAreaid_EditValueChanged;
                popupOspAreaid.SetValue(drviw[0]["FROMAREAID"].ToString());
                popupOspAreaid.Text = drviw[0]["FROMAREANAME"].ToString();
                popupOspAreaid.EditValue = drviw[0]["FROMAREANAME"].ToString();
                popupOspAreaid.SelectedText = drviw[0]["FROMAREANAME"].ToString();
                this.ShowMessage(MessageBoxButtons.OK, "InValidCsmData010"); //메세지 

                popupOspAreaid.EditValueChanged += popupOspAreaid_EditValueChanged;
                return;
            }

            for (int i = 0; i < grdMaterial.View.DataRowCount; i++)
            {
                grdMaterial.View.SetRowCellValue(i, "FROMWAREHOUSEID", txtwarehouseid.Text.ToString());
                grdMaterial.View.SetRowCellValue(i, "FROMWAREHOUSENAME", txtwarehousename.Text.ToString());
                grdMaterial.View.SetRowCellValue(i, "FROMAREAID", popupOspAreaid.GetValue().ToString());
                grdMaterial.View.SetRowCellValue(i, "FROMAREANAME", popupOspAreaid.Text.ToString());
            }
        }

        /// <summary>
        /// Plantid 변경 따른 Areaid 변경처리 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboPlantid_EditValueChanged(object sender, EventArgs e)
        {
            if (_dtSite == null) return;
            popupOspAreaid.SetValue("");
            popupOspAreaid.Text = "";
            popupOspAreaid.EditValue = "";
            txtwarehouseid.Text = "";
            txtwarehousename.Text = "";
            DataRow[] rowsPlant = _dtSite.Select("PLANTID = '" + cboPlantid.EditValue.ToString() + "'");

            if (rowsPlant.Length > 0)
            {

                if (rowsPlant[0]["ISUSEPLANTAUTHORITY"].ToString().Equals("Y"))
                {
                    blIsuseplantauthority = true;
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
                    blIsuseplantauthority = false;
                    if (pnlToolbar.Controls["layoutToolbar"] != null)
                    {
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Enabled = false;
                        if (pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"] != null)
                            pnlToolbar.Controls["layoutToolbar"].Controls["Initialization"].Enabled = true;
                    }
                }

            }
            selectOspAreaidPopup(cboPlantid.EditValue.ToString());

        }
        /// <summary>
        /// 저장 -자재기타입고(header)내역을 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (blSave == false)
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidCsmData011"); //메세지 
                return;
            }
            if (blSavePeriodid == false)
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidCsmData009"); //메세지 
                return;
            }
            //작업장 .
            if (popupOspAreaid.GetValue().ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblAreaid.Text); //메세지 
                popupOspAreaid.Focus();
                return;
            }
            string strTempAreaid = popupOspAreaid.GetValue().ToString();
            //입고일시
            //if (_strDateChangeAuth == "true")
            {
                if (dptTransactiondate.Text.ToString().Equals(""))
                {
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblTransactiondate.Text); //메세지 
                    dptTransactiondate.Focus();
                    return;
                }
            }


            grdMaterial.View.FocusedRowHandle = grdMaterial.View.FocusedRowHandle;
            grdMaterial.View.FocusedColumn = grdMaterial.View.Columns["CONSUMABLEDEFNAME"];
            grdMaterial.View.ShowEditor();
            //조정량 재계산처리 
            CalAdjstockQty();
           
            DataTable changed = grdMaterial.GetChangedRows();
            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSave;Data"); //메세지 
                return;
            }
            //lot no (*) 체크 처리 예정.
            SetNullReplaceCombination();
            for (int i = 0; i < grdMaterial.View.DataRowCount; i++)
            {
                DataRow row = grdMaterial.View.GetDataRow(i);
                string strAreaid = grdMaterial.View.GetRowCellValue(i, "FROMAREAID").ToString();
                if (!(strTempAreaid.Equals(strAreaid)))
                {
                    row["FROMAREAID"] = strTempAreaid;
                }
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
                string strconsumablelotid = grdMaterial.View.GetRowCellValue(i, "CONSUMABLELOTID").ToString();
                if (strconsumablelotid.Equals(""))
                {
                    string lblConsumabledefid = grdMaterial.View.Columns["CONSUMABLELOTID"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblConsumabledefid); //메세지
                    return;
                }
                string strrelconsumablelotid = grdMaterial.View.GetRowCellValue(i, "RELCONSUMABLELOTID").ToString();
                if (strrelconsumablelotid.Equals(""))
                {
                    string lblConsumabledefid = grdMaterial.View.Columns["RELCONSUMABLELOTID"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblConsumabledefid); //메세지
                    return;
                }

                string strQty = grdMaterial.View.GetRowCellValue(i, "QTY").ToString();
                double dblQty = (strQty.ToString().Equals("") ? 0 : Convert.ToDouble(strQty)); //
                string strAdjStockQty = grdMaterial.View.GetRowCellValue(i, "ADJSTOCKQTY").ToString();
                double dblAdjStockQty = (strAdjStockQty.ToString().Equals("") ? 0 : Convert.ToDouble(strAdjStockQty)); //
                if (dblQty.Equals(0))
                {
                    string lblQty = grdMaterial.View.Columns["QTY"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                    return;
                }
                if (dblQty < 0)
                {
                    string lblQty = grdMaterial.View.Columns["QTY"].Caption.ToString();
                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblQty); //메세지
                    return;
                }
                if (dblAdjStockQty < 0)
                {

                    this.ShowMessage(MessageBoxButtons.OK, "InValidCsmData005", strConsumabledefid); //다국어 메세지 추가 
                    return;
                }

                //string strTransactionreasoncode = grdMaterial.View.GetRowCellValue(i, "TRANSACTIONREASONCODE").ToString();
                //if (strTransactionreasoncode.Equals(""))
                //{
                //    string lblTransactionreasoncode = grdMaterial.View.Columns["TRANSACTIONREASONCODE"].Caption.ToString();
                //    this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblTransactionreasoncode); //메세지
                //    return;
                //}
                
            }
            if (CheckPriceDateKeyColumns() == false)
            {
                //다국어 교체 처리 
                string lblConsumabledefid = grdMaterial.View.Columns["CONSUMABLEDEFID"].Caption.ToString();
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspData007", lblConsumabledefid);
                return;
            }
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnSave.Text);//저장하시겠습니까? 
            string strRequestno = btnTransactionno.Text;
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    btnClear.Enabled = false;

                    //datatable 생성 
                    DataTable dt = createSaveHeaderDatatable();

                    DataRow dr = dt.NewRow();

                    if (_strDateChangeAuth == "false")
                    {
                        DateTime dateSaveNow = DateTime.Now;
                        dptTransactiondate.EditValue = dateSaveNow.ToString("yyyy-MM-dd HH:mm:ss");//입고시간 
                    }
                    dr["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
                    dr["PLANTID"] = cboPlantid.EditValue.ToString();
                    dr["AREAID"] = popupOspAreaid.GetValue().ToString();
                    dr["DESCRIPTION"] = txtDescription.Text.ToString();
                    dr["TRANSACTIONNO"] = btnTransactionno.Text.ToString();
                    DateTime dtTrans = Convert.ToDateTime(dptTransactiondate.EditValue.ToString());
                    if (btnTransactionno.Text.ToString().Equals(""))
                    {
                        dr["TRANSACTIONDATE"] = dtTrans.ToString("yyyy-MM-dd HH:mm:ss");
                        dr["TRANSACTIONTYPE"] = "Split";
                        dr["USERID"] = UserInfo.Current.Id.ToString();
                        dr["DEPARTMENT"] = UserInfo.Current.Department.ToString();
                        dr["_STATE_"] = "added";

                    }
                    else
                    {
                        dr["_STATE_"] = "modified";
                    }

                    dt.Rows.Add(dr);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    DataTable dtSave = (grdMaterial.GetChangedRows() as DataTable).Clone();
                    dtSave.TableName = "listDetail";
                    dtSave.Columns["CONSUMABLEDEFID"].DataType = typeof(string);
                    dtSave.Columns["CONSUMABLELOTID"].DataType = typeof(string);
                    dtSave.Columns["QTY"].DataType = typeof(double);
                    DataTable dtSavechang = grdMaterial.GetChangedRows();

                    for (int irow = 0; irow < dtSavechang.Rows.Count; irow++)
                    {
                        dr = dtSavechang.Rows[irow];
                        dr["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
                        dr["PLANTID"] = cboPlantid.EditValue.ToString();
                        dr["FROMAREAID"] = popupOspAreaid.GetValue().ToString();
                        dr["FROMWAREHOUSEID"] = txtwarehouseid.EditValue.ToString();
                        dr["TRANSACTIONTYPE"] = "Split";
                        dr["TRANSACTIONCODE"] = "TRANSFER_ISSUE";
                        dr["TRANSACTIONDATE"] = dtTrans.ToString("yyyy-MM-dd HH:mm:ss");

                        dtSave.ImportRow(dr);

                    }

                    dtSave.DefaultView.Sort = "_STATE_ desc ";
                    ds.Tables.Add(dtSave);
                    DataTable saveResult = this.ExecuteRule<DataTable>("MaterialInventorySplit", ds);
                    DataRow resultData = saveResult.Rows[0];
                    string strtempRequestno = resultData.ItemArray[0].ToString();
                    //의뢰번호 셋팅 처리 
                    btnTransactionno.EditValue = strtempRequestno;

                    controlReadOnlyProcess(true);
                    dptTransactiondate.Enabled = false;
                    string stransdate = dtTrans.ToString("yyyy-MM-dd HH:mm:ss");
                    OnSaveConfrimSearch("Save", cboPlantid.EditValue.ToString(), btnTransactionno.EditValue.ToString(), stransdate);
                    ShowMessage("SuccessOspProcess");
                    blSave = false;

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

                }
            }

        }


        /// <summary>
        /// 초기화 - 초기화 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            btnTransactionno.Text = "";//입고번호
            if (_strDateChangeAuth == "true")
            {
                DateTime dateNow = DateTime.Now;
                dptTransactiondate.EditValue = dateNow.ToString("yyyy-MM-dd HH:mm:ss");//입고시간 
                dptTransactiondate.Enabled = true;
            }
            else
            {
                DateTime dateNow = DateTime.Now;
                dptTransactiondate.EditValue = dateNow.ToString("yyyy-MM-dd HH:mm:ss");//입고시간 
                dptTransactiondate.Enabled = false;
            }
            txtUserName.Text = UserInfo.Current.Name.ToString();//입고자
            txtDescription.Text = "";//비고 
            DataTable dtMat = (grdMaterial.DataSource as DataTable).Clone();//그리드 초기화 
            grdMaterial.DataSource = dtMat;
            popupOspAreaid.SetValue("");
            popupOspAreaid.Text = "";
            popupOspAreaid.EditValue = "";
            txtwarehouseid.Text = "";
            txtwarehousename.Text = "";
            controlReadOnlyProcess(false);
            blSave = true;
            blSavePeriodid = true;
        }
        /// <summary>
        /// 입고번호 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void BtnTransactionno_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind.ToString().Equals("Delete"))
            {
                BtnClear_Click(null, null);  //화면 clear 
            }
            else
            {
                PopupShow();
            }
        }


        /// <summary>
        ///  추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            if (blSave == false)
            {
                int intfocusRow = grdMaterial.View.FocusedRowHandle;
                grdMaterial.View.DeleteRow(intfocusRow);
                return;
            }

            if (popupOspAreaid.GetValue().ToString().Equals(""))
            {
                int intfocusRow = grdMaterial.View.FocusedRowHandle;
                grdMaterial.View.DeleteRow(intfocusRow);
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblAreaid.Text); //메세지 
                popupOspAreaid.Focus();
                return;

            }
            grdMaterial.View.SetFocusedRowCellValue("ENTERPRISEID", UserInfo.Current.Enterprise.ToString());// 회사코드
            grdMaterial.View.SetFocusedRowCellValue("PLANTID", cboPlantid.EditValue.ToString());// plantid
            grdMaterial.View.SetFocusedRowCellValue("TRANSACTIONNO", btnTransactionno.EditValue.ToString());
            grdMaterial.View.SetFocusedRowCellValue("FROMWAREHOUSEID", txtwarehouseid.EditValue.ToString());
            grdMaterial.View.SetFocusedRowCellValue("FROMWAREHOUSENAME", txtwarehousename.Text.ToString());
            grdMaterial.View.SetFocusedRowCellValue("FROMAREAID", popupOspAreaid.GetValue().ToString());
            grdMaterial.View.SetFocusedRowCellValue("FROMAREANAME", popupOspAreaid.Text.ToString());
            grdMaterial.View.SetFocusedRowCellValue("TRANSACTIONDATE", dptTransactiondate.EditValue.ToString());
            grdMaterial.View.SetFocusedRowCellValue("TRANSACTIONTYPE", "Split");
            grdMaterial.View.SetFocusedRowCellValue("TRANSACTIONCODE", "TRANSFER_ISSUE");
            grdMaterial.View.SetFocusedRowCellValue("CONSUMABLELOTID", "");
            grdMaterial.View.SetFocusedRowCellValue("RELCONSUMABLELOTID", "");
            grdMaterial.View.SetFocusedRowCellValue("ISLOTMNG", "Y"); 
            grdMaterial.View.SetFocusedRowCellValue("WORKTIME", _sWorkTime);
            DateTime dttransdate = Convert.ToDateTime(dptTransactiondate.EditValue.ToString());
            string strDateMonthFormat = "";
            strDateMonthFormat = "yyyy-MM-01 " + "00:00:00";
            string strStartdate = dttransdate.ToString(strDateMonthFormat);
            string strmonth = dttransdate.ToString("yyyy-MM");

            //DateTime dtstartdate = Convert.ToDateTime(strStartdate);
            ////입고일자 >=월초기 기준일자 
            ////입고일자 >=월초기 기준일자 
            //if (dttransdate >= dtstartdate)
            //{
            //    // DateTime dtmonth = dttransdate.AddMonths(-1);//당월 -1
            //    strmonth = dttransdate.ToString("yyyy-MM");
            //}
            //else
            //{
            //    DateTime dtmonth = dtstartdate.AddMonths(-1);// 당월 -2
            //    strmonth = dtmonth.ToString("yyyy-MM");
            //    dtmonth = dttransdate.AddMonths(-1);// 당월 -2
            //    strStartdate = dtmonth.ToString(strDateMonthFormat);
            //}
            string strEnddate = dttransdate.ToString("yyyy-MM-dd HH:mm:ss");
            grdMaterial.View.SetFocusedRowCellValue("YEARMONTH", strmonth);
            grdMaterial.View.SetFocusedRowCellValue("STARTDATE", strStartdate);
            grdMaterial.View.SetFocusedRowCellValue("ENDDATE", strEnddate);


        }

        /// <summary>
        /// 조정재고량 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = grdMaterial.View.GetFocusedDataRow();
            if (row == null) return;
            grdMaterial.View.CellValueChanged -= View_CellValueChanged;
            if (e.Column.FieldName == "QTY")
            {

                string strStockqty = row["STOCKQTY"].ToString();
                decimal decStockqty = (strStockqty.ToString().Equals("") ? 0 : Convert.ToDecimal(strStockqty)); //

                string strQty = row["QTY"].ToString();
                decimal decQty = (strQty.ToString().Equals("") ? 0 : Convert.ToDecimal(strQty)); //

                decimal decAdjstockqty = decStockqty - decQty;
                row["ADJSTOCKQTY"] = decAdjstockqty;
            }


            grdMaterial.View.CellValueChanged += View_CellValueChanged;


        }

        /// <summary>
        /// 셀 ReadOnly 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (!(grdMaterial.View.GetFocusedRowCellValue("TRANSACTIONSEQUENCE").ToString().Equals("")))
            {
                e.Cancel = true;
            }
            //if (grdMaterial.View.FocusedColumn.FieldName.Equals("QTY") && grdMaterial.View.GetFocusedRowCellValue("ISLOTMNG").ToString().Equals("Y"))
            //{
            //    e.Cancel = true;
            //}

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
        }
        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();


        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();


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
            dt.Columns.Add("TRANSACTIONNO");
            dt.Columns.Add("TRANSACTIONDATE");
            dt.Columns.Add("TRANSACTIONTYPE");
            dt.Columns.Add("USERID");
            dt.Columns.Add("DEPARTMENT");
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
            //  cboPlantid.ReadOnly = blProcess;
            // btnTransactionno.ReadOnly = blProcess;
            if (blProcess == true)
            {
                popupOspAreaid.Enabled = false;
            }
            else
            {
                popupOspAreaid.Enabled = true;
            }
            cboPlantid.ReadOnly = blProcess;
            popupOspAreaid.ReadOnly = blProcess;
           //  dptTransactiondate.ReadOnly = blProcess;
            //txtUserName.ReadOnly = blProcess;
        }
        /// <summary>
        /// 자재기타입고내역 (LINE )LOTNO  값 대체(*) 처리 
        /// </summary>
        /// <param name="strRequestno"></param>
        private void SetNullReplaceCombination()
        {

            if (grdMaterial.View.DataRowCount == 0)
            {
                return;
            }
            for (int irow = 0; irow < grdMaterial.View.DataRowCount; irow++)
            {
                DataRow dr = grdMaterial.View.GetDataRow(irow);

                string sproductdefid = dr["CONSUMABLELOTID"].ToString();

                string strAreaid = dr["FROMAREAID"].ToString();
                if (sproductdefid.Equals(""))
                {
                    dr["CONSUMABLELOTID"] = "*";

                }
                if (!(strAreaid.Equals(popupOspAreaid.GetValue().ToString())))
                {
                    dr["FROMAREAID"] = popupOspAreaid.GetValue().ToString();
                }
                string strPlantid = dr["PLANTID"].ToString();
                if (!(strPlantid.Equals(cboPlantid.EditValue.ToString())))
                {
                    dr["PLANTID"] = cboPlantid.EditValue.ToString();
                }
            }
        }

        /// <summary>
        /// 자재 재고 조정 – Popup
        /// </summary>
        private void PopupShow()
        {
            string sAreaid = "";
            string sAreaName = "";
            if (!(popupOspAreaid.GetValue().ToString().Equals("")))
            {
                sAreaid = popupOspAreaid.GetValue().ToString();
                sAreaName = popupOspAreaid.Text.ToString();
            }
            string sPlantid = "";
            if (cboPlantid.EditValue != null)
            {
                sPlantid = cboPlantid.EditValue.ToString();
            }
            MaterialInventorySplitPopup ReceiptPopup = new MaterialInventorySplitPopup(sPlantid, sAreaid, sAreaName);
            ReceiptPopup.write_handler += ReceiptPopup_write_handler;
            if (ReceiptPopup.CurrentDataRow != null)
            {
                if (ReceiptPopup.CurrentDataRow.Table.Rows.Count == 1)
                {
                    ReceiptPopup.Close();

                }
                else
                {
                    ReceiptPopup.ShowDialog(this);
                }
            }
            else
            {
                ReceiptPopup.ShowDialog(this);
            }
        }
        /// <summary>
        /// 자재 기타 입고 내역 – 결과값 가져오기 
        /// </summary>
        /// <param name="row"></param>
        private void ReceiptPopup_write_handler(DataRow row)
        {
            //다시 조회 하기....
            if (row != null)
            {
                popupOspAreaid.EditValueChanged -= popupOspAreaid_EditValueChanged;
                OnSaveConfrimSearch("Search", row["PLANTID"].ToString(), row["TRANSACTIONNOISSUE"].ToString(), row["TRANSACTIONDATEISSUE"].ToString());
                OnPlantidInformationSearch(row["PLANTID"].ToString());
                popupOspAreaid.EditValueChanged += popupOspAreaid_EditValueChanged;
                blSave = false;
            }

        }

        /// <summary>
        /// 입고번호 재조회시 사용  
        /// </summary>
        /// <param name="workgubun"></param>
        /// <param name="sPlantid"></param>
        /// <param name="sTransactionDate"></param>
        private void OnSaveConfrimSearch(string workgubun, string sPlantid, string sTransActionNo, string sTransactionDate)
        {

            DateTime dttransdate = Convert.ToDateTime(sTransactionDate);
            string strDateMonthFormat = "";
            strDateMonthFormat = "yyyy-MM-01 " + _sWorkTime;
            strDateMonthFormat = "yyyy-MM-01 " + "00:00:00";
            string strStartdate = dttransdate.ToString(strDateMonthFormat);
            string strmonth = dttransdate.ToString("yyyy-MM");

            //DateTime dtstartdate = Convert.ToDateTime(strStartdate);
            ////입고일자 >= 월초기 기준일자
            //if (dttransdate >= dtstartdate)
            //{
            //    strmonth = dttransdate.ToString("yyyy-MM");
            //////}
            //else
            //{
            //    DateTime dtmonth = dtstartdate.AddMonths(-1);// 당월 -2
            //    strmonth = dtmonth.ToString("yyyy-MM");
            //    dtmonth = dttransdate.AddMonths(-1);// 당월 -2
            //    strStartdate = dtmonth.ToString(strDateMonthFormat);
            //}
            string strEnddate = dttransdate.ToString("yyyy-MM-dd HH:mm:ss");
            grdMaterial.View.SetFocusedRowCellValue("YEARMONTH", strmonth);
            grdMaterial.View.SetFocusedRowCellValue("STARTDATE", strStartdate);
            grdMaterial.View.SetFocusedRowCellValue("ENDDATE", strEnddate);
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("P_STARTDATE", strStartdate);
            Param.Add("P_YEARMONTH", strmonth);
            Param.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise.ToString());
            Param.Add("P_PLANTID", sPlantid);
            Param.Add("P_TRANSACTIONNO", sTransActionNo);
            Param.Add("P_TRANSACTIONTYPE", "Split");
            Param.Add("P_TRANSACTIONCODE", "TRANSFER_ISSUE");
            Param.Add("USERID", UserInfo.Current.Id.ToString());
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dtReceipt = SqlExecuter.Query("GetMaterialOtherGoodsIssueSearch", "10001", Param);
            grdMaterial.DataSource = dtReceipt;

            # region 재고실사및 재고마감여부 확인
            string strAreaid = dtReceipt.Rows[0]["FROMAREAID"].ToString();
            string strcheckdate = dtReceipt.Rows[0]["TRANSACTIONDATE"].ToString();
            DateTime dtcheckdate = Convert.ToDateTime(strcheckdate);
            strcheckdate = strEnddate = dtcheckdate.ToString("yyyy-MM-dd HH:mm:ss");
            Dictionary<string, object> Paramcheck = new Dictionary<string, object>();
            Paramcheck.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise.ToString());
            Paramcheck.Add("P_PLANTID", sPlantid);
            Paramcheck.Add("P_AREAID", strAreaid);
            Paramcheck.Add("P_CHECKDATE", strcheckdate);
            Paramcheck = Commons.CommonFunction.ConvertParameter(Paramcheck);
            DataTable dtcheck = SqlExecuter.Query("GetCheckPeriodCloseCsm", "10001", Paramcheck);
            int intcheck = int.Parse(dtcheck.Rows[0]["CNTPERIODID"].ToString());
            if (intcheck > 0)
            {
                blSavePeriodid = false;
            }
            #endregion
            //입고내역 header 정보 
            if (workgubun.Equals("Search"))
            {
                //화면 lock 처리
                controlReadOnlyProcess(true);
                dptTransactiondate.Enabled = false;
                if (dtReceipt.Rows.Count > 0)
                {
                    DataRow drhead = dtReceipt.Rows[0];
                    cboPlantid.EditValue = drhead["PLANTID"].ToString();
                    btnTransactionno.EditValue = drhead["TRANSACTIONNO"].ToString();
                    popupOspAreaid.SetValue(drhead["FROMAREAID"].ToString());
                    popupOspAreaid.Text = drhead["FROMAREANAME"].ToString();
                    popupOspAreaid.EditValue = drhead["FROMAREANAME"].ToString();
                    txtwarehouseid.Text = drhead["FROMWAREHOUSEID"].ToString();
                    txtwarehousename.Text = drhead["FROMWAREHOUSENAME"].ToString();
                    DateTime dtdate = Convert.ToDateTime(drhead["TRANSACTIONDATE"].ToString());
                    dptTransactiondate.EditValue = dtdate;
                    txtUserName.EditValue = drhead["USERNAME"].ToString();
                }

            }

        }

        /// <summary>
        /// plant정보에서 WORKTIME 
        /// </summary>
        private void OnPlantidInformationSearch(string sPlantid)
        {

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("P_PLANTID", sPlantid);
            DataTable dtPlantInfo = SqlExecuter.Query("GetPlantidInformatByCsm", "10001", dicParam);

            if (dtPlantInfo.Rows.Count == 1)
            {
                DataRow drPlant = dtPlantInfo.Rows[0];
                _sWorkTime = drPlant["WORKTIME"].ToString();
            }
            else
            {

                return;
            }

        }

        /// <summary>
        /// 자재기준  key 중복 체크 
        /// </summary>
        /// <returns></returns>
        private bool CheckPriceDateKeyColumns()
        {
            bool blcheck = true;
            if (grdMaterial.View.DataRowCount == 0)
            {
                return blcheck;
            }


            for (int irow = 0; irow < grdMaterial.View.DataRowCount; irow++)
            {

                DataRow row = grdMaterial.View.GetDataRow(irow);

                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    string strConsumabledefid = row["CONSUMABLEDEFID"].ToString();
                    string strConsumabledefversion = row["CONSUMABLEDEFVERSION"].ToString();
                    string strConsumablelotid = row["CONSUMABLELOTID"].ToString();

                    if (SearchPriceDateKey(strConsumabledefid, strConsumabledefversion, strConsumablelotid, irow) < 0)
                    {
                        blcheck = true;

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return blcheck;
        }

        /// <summary>
        /// 자재 중복 체크 함수
        /// </summary>
        /// <param name="dateStartdate"></param>
        /// <param name="dateEnddate"></param>
        /// <param name="icurRow"></param>
        /// <returns></returns>
        private int SearchPriceDateKey(string strConsumabledefid, string strConsumabledefversion, string strConsumablelotid, int icurRow)
        {
            int iresultRow = -1;

            for (int irow = 0; irow < grdMaterial.View.DataRowCount; irow++)
            {
                if (icurRow != irow)
                {
                    DataRow row = grdMaterial.View.GetDataRow(irow);

                    // 행 삭제만 제외 
                    if (grdMaterial.View.IsDeletedRow(row) == false)
                    {
                        string strTempConsumabledefid = row["CONSUMABLEDEFID"].ToString();
                        string strTempConsumabledefversion = row["CONSUMABLEDEFVERSION"].ToString();
                        string strTempConsumablelotid = row["CONSUMABLELOTID"].ToString();
                        if (strTempConsumabledefid.Equals(strConsumabledefid) && strTempConsumabledefversion.Equals(strConsumabledefversion) && strTempConsumablelotid.Equals(strConsumablelotid))
                        {
                            return irow;
                        }

                    }
                }
            }
            return iresultRow;
        }


        private void CalAdjstockQty()
        {


            for (int irow = 0; irow < grdMaterial.View.DataRowCount; irow++)
            {
                DataRow row = grdMaterial.View.GetDataRow(irow);
                string strStockqty = row["STOCKQTY"].ToString();
                decimal decStockqty = (strStockqty.ToString().Equals("") ? 0 : Convert.ToDecimal(strStockqty)); //

                string strQty = row["QTY"].ToString();
                decimal decQty = (strQty.ToString().Equals("") ? 0 : Convert.ToDecimal(strQty)); //

                decimal decAdjstockqty = decStockqty - decQty;
                row["ADJSTOCKQTY"] = decAdjstockqty;

            }


        }
        /// <summary>
        /// 품목코드 2개이상 선택시 
        /// </summary>
        /// <param name="dr"></param>
        private void popAddGrid(DataRow dr)
        {

            string sConsumabledefid = dr["CONSUMABLEDEFID"].ToString();
            string sConsumabledefversion = dr["CONSUMABLEDEFVERSION"].ToString();
            string sConsumablelotid = dr["CONSUMABLELOTID"].ToString();

            int icheck = checkGridConsumabledefid(sConsumabledefid, sConsumabledefversion, sConsumablelotid);
            if (icheck == -1)
            {
                grdMaterial.View.AddNewRow();
                int irow = grdMaterial.View.RowCount - 1;
                DataRow classRow = grdMaterial.View.GetDataRow(irow);
                classRow["CONSUMABLEDEFID"] = dr["CONSUMABLEDEFID"];
                classRow["CONSUMABLEDEFNAME"] = dr["CONSUMABLEDEFNAME"];
                classRow["CONSUMABLEDEFVERSION"] = dr["CONSUMABLEDEFVERSION"];
                classRow["CONSUMABLELOTID"] = dr["CONSUMABLELOTID"];
                classRow["ISLOTMNG"] = dr["ISLOTMNG"];
                classRow["UNIT"] = dr["UNIT"];
                classRow["STOCKQTY"] = dr["STOCKQTY"];
                classRow["AVAILABLEQTY"] = dr["AVAILABLEQTY"];
                classRow["QTY"] = 0d;
                classRow["ADJSTOCKQTY"] = dr["STOCKQTY"];
                grdMaterial.View.RaiseValidateRow(irow);
            }


        }
        /// <summary>
        /// Process 체크 처리 
        /// </summary>
        /// <param name="sLotId"></param>
        /// <returns></returns>
        private int checkGridConsumabledefid(string sConsumabledefid, string sConsumabledefversion, string sConsumablelotid)
        {

            for (int i = 0; i < grdMaterial.View.DataRowCount; i++)
            {
                if (grdMaterial.View.GetRowCellValue(i, "CONSUMABLEDEFID").ToString().Equals(sConsumabledefid)
                    && grdMaterial.View.GetRowCellValue(i, "CONSUMABLEDEFVERSION").ToString().Equals(sConsumabledefversion)
                    && grdMaterial.View.GetRowCellValue(i, "CONSUMABLELOTID").ToString().Equals(sConsumablelotid))
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion
    }
}
