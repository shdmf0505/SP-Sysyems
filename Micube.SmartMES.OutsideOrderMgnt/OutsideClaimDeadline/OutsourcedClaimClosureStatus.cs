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

#endregion

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리 > 외주 Claim 마감 > Claim 마감 현황
    /// 업  무  설  명  :  Claim 마감 현황
    /// 생    성    자  :  choisstar
    /// 생    성    일  : 2019-08-24
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcedClaimClosureStatus : SmartConditionManualBaseForm
    {
        #region Local Variables


        string  _strPlantid  ="";
        string  _strPeriodid = "";
        string  _strAreaid = "";

        #endregion

        #region 생성자

        public OutsourcedClaimClosureStatus()
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
            _strPlantid = UserInfo.Current.Plant.ToString();
            InitializeEvent();
            //협력사 선택팝업
            selectOspVendoridPopup();
            //공정 선택팝업
            selectProcesssegmentidPopup();
            selectOspAreaidPopup();
            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
          
            InitializeComboBox();
            InitializeComboAreaid(UserInfo.Current.Plant.ToString());
        }
        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
            // 작업구분값 정의 
            cboexpensegubun.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboexpensegubun.ValueMember = "CODEID";
            cboexpensegubun.DisplayMember = "CODENAME";
           
            cboexpensegubun.DataSource = SqlExecuter.Query("GetCodeAllListByOsp", "10001"
              , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "ClaimType" } });
            cboexpensegubun.EditValue = "*";
            cboexpensegubun.ShowHeader = false;
         

        }

        #endregion

        #region 작업장 combox
        /// <summary>
        /// 작업장 combox 
        /// </summary>
        /// <param name="sPlantid"></param>
        private void InitializeComboAreaid(string sPlantid)
        {
           
        }
        #endregion
        /// <summary>
        /// 협력사 선택팝업
        /// </summary>
        private void selectOspVendoridPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(550, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("OSPVENDORID", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "OSPVENDORID";
            popup.LabelText = "OSPVENDORID";
            popup.SearchQuery = new SqlQuery("GetVendorListAuthorityByOsp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                , $"USERID={UserInfo.Current.Id}"
                , $"P_PLANTID={_strPlantid}"
                , $"P_AREAID={_strAreaid}");
            popup.IsMultiGrid = false;

            popup.DisplayFieldName = "OSPVENDORNAME";
            popup.ValueFieldName = "OSPVENDORID";
            popup.LanguageKey = "OSPVENDORID";

            popup.Conditions.AddTextBox("OSPVENDORNAME");

            popup.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetLabel("OSPVENDORID");
            popup.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200)
                .SetLabel("OSPVENDORNAME");

            popupOspVendorid.SelectPopupCondition = popup;
        }
        /// <summary>
        /// 작업장
        /// </summary>
        private void selectOspAreaidPopup()
        {
            _strAreaid = "";
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(500, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "AREANAME";
            popup.LabelText = "AREANAME";
            popup.SearchQuery = new SqlQuery("GetAreaidListAuthorityByOsp", "10001", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                          , $"USERID={UserInfo.Current.Id}"
                                                                          , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                          , $"P_PLANTID={_strPlantid}");
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
                        _strAreaid = row["AREAID"].ToString();
                    }

                }
            });
            popup.Conditions.AddTextBox("P_AREANAME")
                .SetLabel("AREANAME");
            popup.GridColumns.AddTextBoxColumn("AREAID", 120)
                .SetLabel("AREAID");
            popup.GridColumns.AddTextBoxColumn("AREANAME", 200)
                .SetLabel("AREANAME");

            popupOspAreaid.SelectPopupCondition = popup;

            //selectOspVendoridPopup();
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

            popup.Conditions.AddTextBox("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT"); 

            popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetLabel("PROCESSSEGMENTID");
            popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200)
                .SetLabel("PROCESSSEGMENTNAME");

            popupProcesssegmentid.SelectPopupCondition = popup;
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            InitializeGrid_Summary();
            InitializeGrid_List();
        }
        private void InitializeGrid_Summary()
        {
            grdClaimStatus.GridButtonItem = GridButtonItem.Export;
            grdClaimStatus.View.SetIsReadOnly();
            grdClaimStatus.View.AddTextBoxColumn("ENTERPRISEID", 120).SetIsHidden();            //  
            grdClaimStatus.View.AddTextBoxColumn("PERIODID", 120).SetIsHidden();            //  
            grdClaimStatus.View.AddTextBoxColumn("AREAID", 120).SetIsHidden();            //  
            grdClaimStatus.View.AddTextBoxColumn("VENDORID", 120).SetIsHidden();            //  
            grdClaimStatus.View.AddTextBoxColumn("PLANTID", 120).SetIsHidden();            // 
            grdClaimStatus.View.AddTextBoxColumn("AREANAME", 150);
            grdClaimStatus.View.AddTextBoxColumn("OSPVENDORNAME", 150);
            grdClaimStatus.View.AddTextBoxColumn("PRODEFECTAMOUNT", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdClaimStatus.View.AddTextBoxColumn("SELAMOUNT", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdClaimStatus.View.AddTextBoxColumn("MATERLOSTAMOUNT", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdClaimStatus.View.AddTextBoxColumn("ETCAMOUNT", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdClaimStatus.View.AddTextBoxColumn("TOTAMOUNT", 100)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            grdClaimStatus.View.PopulateColumns();

        }
        private void InitializeGrid_List()
        {
            // TODO : 그리드 초기화 로직 추가
            grdClaimConfirm.GridButtonItem = GridButtonItem.Export;
            grdClaimConfirm.View.AddTextBoxColumn("ENTERPRISEID", 120).SetIsHidden();            //  
            grdClaimConfirm.View.AddTextBoxColumn("PERIODID", 120).SetIsHidden();            //  
            grdClaimConfirm.View.AddTextBoxColumn("VENDORID", 120).SetIsHidden();            //  
            grdClaimConfirm.View.AddTextBoxColumn("PLANTID", 120).SetIsHidden();            // 
            grdClaimConfirm.View.AddTextBoxColumn("WORKGUBUN", 120).SetIsHidden();            // 
            grdClaimConfirm.View.AddTextBoxColumn("CLAIMSEQUENCE", 120).SetIsHidden();            //
            grdClaimConfirm.View.AddTextBoxColumn("LOTHISTKEY", 120).SetIsHidden();            // 
          
            grdClaimConfirm.View.AddComboBoxColumn("CLAIMTYPE", 120, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ClaimType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsHidden();       // 
            grdClaimConfirm.View.AddDateEditColumn("DEFINETIME", 120)
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime)
                .SetIsHidden();       // 
            grdClaimConfirm.View.AddDateEditColumn("OCCURTIME", 120)
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime)
                .SetIsHidden();       // 
            grdClaimConfirm.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 120)
                .SetIsReadOnly();// 
            grdClaimConfirm.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120)
                .SetIsReadOnly();       // 
            grdClaimConfirm.View.AddTextBoxColumn("REASONAREANAME", 120)
                .SetIsReadOnly();       // 
            grdClaimConfirm.View.AddTextBoxColumn("OSPVENDORNAME", 120)
                .SetIsReadOnly();       // 
            grdClaimConfirm.View.AddTextBoxColumn("PRODUCTDEFID", 100)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("LOTID", 200)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("CLAIMPRICE", 120).SetIsHidden();       // 
            grdClaimConfirm.View.AddTextBoxColumn("DEFECTCODE", 100)
                .SetIsReadOnly();

            grdClaimConfirm.View.AddTextBoxColumn("DEFECTNAME", 120)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("DEFECTQTY", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("DEFECTAMOUNT", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("OSPREDUCEQTY", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("REDUCEQTYAMOUNT", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();        // 
            grdClaimConfirm.View.AddTextBoxColumn("OSPREDUCERATE", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("REDUCERATEAMOUNT", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();

            grdClaimConfirm.View.AddTextBoxColumn("CLAIMAMOUNT", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("FINDPROCESSSEGMENTNAME", 120)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("CREATORNAME", 120)
                .SetIsReadOnly();

            grdClaimConfirm.View.AddTextBoxColumn("DEFINEUSERNAME", 120)
                .SetIsReadOnly();
            grdClaimConfirm.View.AddTextBoxColumn("DESCRIPTION", 200)
                .SetIsReadOnly();
            grdClaimConfirm.View.PopulateColumns();

           
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            //
            btnDetailSearch.Click += BtnDetailSearch_Click;
            //마감집계 그리드 click
            popupOspAreaid.EditValueChanging += popupOspAreaid_EditValueChanging;
            popupOspAreaid.ButtonClick += popupOspAreaid_buttonClick;
            grdClaimStatus.View.RowCellClick += View_RowCellClick;
        }
        private void popupOspAreaid_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
           

            selectOspVendoridPopup();
           
        }
        /// <summary>
        /// 품목 Popup 삭제했을때 Lot Popup, Customer Popup 조회조건 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupOspAreaid_buttonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            popupOspVendorid.SetValue("");
            popupOspVendorid.EditValue = "";
            popupOspVendorid.Text = "";
            if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
            {
                _strAreaid = "";
            }


        }
        /// <summary>
        /// 품목코드 변경시 처리 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupOspAreaid_EditValueChanged(object sender, EventArgs e)
        {
            _strAreaid = popupOspAreaid.GetValue().ToString();
            
            selectOspVendoridPopup();
        }
        /// <summary>
        /// 그리드의 Row Click시 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (grdClaimStatus.View.FocusedRowHandle < 0) return;
            string strVendorid = grdClaimStatus.View.GetFocusedRowCellValue("VENDORID").ToString();
            string strOspvendorname = grdClaimStatus.View.GetFocusedRowCellValue("OSPVENDORNAME").ToString();
            popupOspVendorid.SetValue(strVendorid);
            popupOspVendorid.Text = strOspvendorname;
            popupOspVendorid.EditValue = strOspvendorname;
            
            string strAreaid = grdClaimStatus.View.GetFocusedRowCellValue("AREAID").ToString();
            string strAreaname = grdClaimStatus.View.GetFocusedRowCellValue("AREANAME").ToString();

            popupOspAreaid.SetValue(strAreaid);
            popupOspAreaid.Text = strAreaname;
            popupOspAreaid.EditValue = strAreaname;
          
            if (grdClaimStatus.View.FocusedColumn.FieldName.Equals("PRODEFECTAMOUNT"))
            {
                cboexpensegubun.EditValue = "DefectScrap";
            }
            if (grdClaimStatus.View.FocusedColumn.FieldName.Equals("SELAMOUNT"))
            {
                cboexpensegubun.EditValue = "Selection";
            }
            if (grdClaimStatus.View.FocusedColumn.FieldName.Equals("MATERLOSTAMOUNT"))
            {
                cboexpensegubun.EditValue = "MaterialLoss";
            }
            if (grdClaimStatus.View.FocusedColumn.FieldName.Equals("ETCAMOUNT"))
            {
                cboexpensegubun.EditValue = "Etc";
            }
            if (grdClaimStatus.View.FocusedColumn.FieldName.Equals("TOTAMOUNT"))
            {
                cboexpensegubun.EditValue = "*";
            }
        }
        /// <summary>
        /// 조회 클릭 - 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDetailSearch_Click(object sender, EventArgs e)
        {
           
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("P_PLANTID", _strPlantid);
            Param.Add("P_PERIODID", _strPeriodid);
            Param.Add("P_OSPVENDORID",   popupOspVendorid.GetValue().ToString());
            Param.Add("P_PROCESSSEGMENTID", popupProcesssegmentid.GetValue().ToString());
           
            Param.Add("P_AREAID", popupOspAreaid.GetValue().ToString());
            Param.Add("USERID", UserInfo.Current.Id.ToString());
            if (!(cboexpensegubun.Text.Trim().ToString().Equals("")))
            {
                Param.Add("P_CLAIMTYPE", cboexpensegubun.EditValue.ToString());
            }

            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dtpop = SqlExecuter.Query("GetOutsourcedClaimClosureStatusDetail", "10001", Param);

            grdClaimConfirm.DataSource = dtpop;

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

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

          
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();
            var values = Conditions.GetValues();
            string stryearmonth = values["P_YEARMONTH"].ToString();
            DateTime dtyearmonth = Convert.ToDateTime(stryearmonth);
            values.Add("P_PERIODNAME", dtyearmonth.ToString("yyyy-MM"));
            values.Add("P_PERIODTYPE", "Claim");
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtPeriod = await SqlExecuter.QueryAsync("GetOutsourcedClaimPeriod", "10001", values);
            if (dtPeriod.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                // 다국어 마감년 정보가 없습니다.
                ShowMessage("InValidOspData008"); // 
                DataTable dtPeriodClaim = (grdClaimConfirm.DataSource as DataTable).Clone();
                grdClaimConfirm.DataSource = dtPeriodClaim;
                txtperiod.Text = "";
                _strPeriodid = "";
                return;
            }
          
            string strWorktime = dtPeriod.Rows[0]["WORKTIME"].ToString();
            txtperiod.Text = dtPeriod.Rows[0]["PERIODSTATE"].ToString();
            _strPeriodid = dtPeriod.Rows[0]["PERIODID"].ToString();
            values.Add("P_PERIODID", _strPeriodid);

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("USERID", UserInfo.Current.Id.ToString());
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtClaim = await SqlExecuter.QueryAsync("GetOutsourcedClaimClosureStatus", "10001", values);
            if (dtClaim.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.

            }
            grdClaimStatus.DataSource = dtClaim;
            
            DataTable dtSearch = (grdClaimConfirm.DataSource as DataTable).Clone();

            grdClaimConfirm.DataSource = dtSearch;
            _strPlantid = values["P_PLANTID"].ToString();
            InitializeComboAreaid(_strPlantid);
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // site
            //InitializeConditionPopup_Plant();
            // 기간 
            InitializeCondition_Closeyear();

            InitializeConditionPopup_OspAreaid();
            //협력사
            InitializeConditionPopup_OspVendorid();

        }
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {
            var plantCbobox = Conditions.AddComboBox("p_plantid", new SqlQuery("GetPlantList", "00001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
              .SetLabel("PLANT")
              .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
              .SetPosition(0.1)
              .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
              .SetValidationIsRequired()
              .SetIsReadOnly();

        }
        /// <summary>
        /// 마감년월
        /// </summary>
        private void InitializeCondition_Closeyear()
        {
            DateTime dateNow = DateTime.Now;
            string strym = dateNow.ToString("yyyy-MM");
            var YearmonthDT = Conditions.AddDateEdit("p_yearmonth")
               .SetLabel("CLOSEYM")
               .SetDisplayFormat("yyyy-MM")
               .SetPosition(0.4)
               .SetDefault(strym)
               .SetValidationIsRequired()
            ;

        }
        /// <summary>
        /// 작업장
        /// </summary>
        private void InitializeConditionPopup_OspAreaid()
        {
            // 팝업 컬럼설정
            var vendoridPopupColumn = Conditions.AddSelectPopup("p_areaid", new SqlQuery("GetAreaidListAuthorityByOsp", "10001"
                , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                , $"USERID={UserInfo.Current.Id}"
                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(520, 600)
               .SetLabel("AREANAME")
               .SetRelationIds("p_plantId")
               .SetPopupResultCount(1)
               .SetPosition(1.2);
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
            var vendoridPopupColumn = Conditions.AddSelectPopup("p_ospvendorid", new SqlQuery("GetVendorListAuthorityByOsp", "10001"
                , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                , $"USERID={UserInfo.Current.Id}"), "OSPVENDORNAME", "OSPVENDORID")
               .SetPopupLayout("OSPVENDORID", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("OSPVENDORID")
               .SetPopupResultCount(1)
               .SetRelationIds("p_plantid", "p_areaid")
               .SetPosition(2.3);

            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("OSPVENDORNAME")
                .SetLabel("OSPVENDORNAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);

            var txtOspVendor = Conditions.AddTextBox("P_OSPVENDORNAME")
               .SetLabel("OSPVENDORNAME")
               .SetPosition(2.4);

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
            popupOspAreaid.SetValue("");
            popupOspAreaid.EditValue = "";
            popupOspAreaid.Text = "";
            popupOspVendorid.SetValue("");
            popupOspVendorid.EditValue = "";
            popupOspVendorid.Text = "";
            _strAreaid = "";
            selectOspAreaidPopup();

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

        #endregion
    }
}
