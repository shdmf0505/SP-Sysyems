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
    /// 프 로 그 램 명  :  외주관리 > 외주가공비마감 > 기타외주 작업의록 등록
    /// 업  무  설  명  : 기타외주 작업의뢰 내역을 등록한다.
    /// 생    성    자  : choisstar
    /// 생    성    일  : 2019-06-13
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OtherSubcontractWorkCommission : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        private bool _blchang = false; //임시작업그리드 통제용 
        private string _strPlantid = ""; // plant 변경시 작업 
        private string _strOspVendorid = ""; //
        private bool _blSaveAuth = false;   //저장버튼 권한 체크 처리 
        #endregion

        #region 생성자

        public OtherSubcontractWorkCommission()
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

            // 콤보박스 셋팅 
            InitializeComboBox();
            // 작업용 plant 셋팅 (조회시 다시 셋팅)
            _strPlantid = UserInfo.Current.Plant.ToString();
            _blSaveAuth = true;
            InitializeEvent();    // 이벤트 
            selectOspAreaidPopup(_strPlantid);
            //공정 선택팝업
            selectProcesssegmentidPopup();
            // 기타외주작업목록 grid 셋팅
            InitializegOtherSubcontractWorkCommissionGrid();
            // 화면 초기화 처리 
            BtnClear_Click(null, null);
        }

        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
            // 작업구분값 정의 
            cboOspetctype.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboOspetctype.ValueMember = "CODEID";
            cboOspetctype.DisplayMember = "CODENAME";
            cboOspetctype.EditValue = "1";
            DataTable dtOspetctype = SqlExecuter.Query("GetCodeList", "00001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "OSPEtcType" } });
           
            DataRow[] filteredDataRows = dtOspetctype.Select("CODEID NOT IN ('MajorVendorDiffrence' ,'Claim')");
            DataTable filteredDataTable = new DataTable();

            if (filteredDataRows.Length != 0)
            {
                filteredDataTable = filteredDataRows.CopyToDataTable();
                cboOspetctype.DataSource = filteredDataTable;
            }
            else
            {
                cboOspetctype.DataSource = dtOspetctype;
            }
            cboOspetctype.ShowHeader = false;
            //단위 콤보 값 저의
            cboUnit.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboUnit.ValueMember = "UOMDEFID";
            cboUnit.DisplayMember = "UOMDEFNAME";
            cboUnit.EditValue = "PCS";
            
            cboUnit.DataSource = SqlExecuter.Query("GetUomDefinitionMapListByOsp", "10001"
             , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "UOMCATEGORY", "OSP" } });

            cboUnit.ShowHeader = false;
            // 양산구분값
            cboLotproducttype.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboLotproducttype.ValueMember = "CODEID";
            cboLotproducttype.DisplayMember = "CODENAME";
            //cboLotproducttype.EditValue = "1";  // 2021-03-15 오근영 주석처리 278건이 ct_ospetcwork.lotproducttype 컬럼에 1로 들어갔음.

            cboLotproducttype.DataSource = SqlExecuter.Query("GetCodeList", "00001"
              , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "ProductionType" } });

            cboLotproducttype.ShowHeader = false;
            //진행상태콤보값 정의
            cboOspetcprogressstatus.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboOspetcprogressstatus.ValueMember = "CODEID";
            cboOspetcprogressstatus.DisplayMember = "CODENAME";
            
            cboOspetcprogressstatus.DataSource = SqlExecuter.Query("GetCodeList", "00001"
                , new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType },{ "CODECLASSID","OSPEtcProgressStatus" } });
            cboOspetcprogressstatus.EditValue = "Request";
            cboOspetcprogressstatus.ShowHeader = false;
        }

        #endregion

        #region Popup

        /// <summary>
        /// 작업장 
        /// </summary>
        /// <param name="sPlantid"></param>
        private void selectOspAreaidPopup(string sPlantid)
        {
            Dictionary<string, object> dicParam;
            dicParam = new Dictionary<string, object> {
                { "P_ENTERPRISEID", UserInfo.Current.Enterprise }
                , { "LANGUAGETYPE", UserInfo.Current.LanguageType }
                , { "P_PLANTID", sPlantid}
            };

            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();
            popup.SetPopupLayoutForm(820, 700, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false);
            popup.Id = "AREANAME";
            popup.LabelText = "AREANAME";
            popup.SearchQuery = new SqlQuery("GetAreaidPopupListByOsp", "10001", dicParam);
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

     

        /// <summary>
        /// 공정 선택팝업
        /// </summary>
        private void selectProcesssegmentidPopup()
        {
            Dictionary<string, object> dicParam;
            dicParam = new Dictionary<string, object> {
                { "ENTERPRISEID", UserInfo.Current.Enterprise }
                , { "LANGUAGETYPE", UserInfo.Current.LanguageType }
            };

            ConditionItemSelectPopup popupProc = new ConditionItemSelectPopup();
            popupProc.SetPopupLayoutForm(500, 600, FormBorderStyle.FixedDialog);
            popupProc.SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, false);
            popupProc.Id = "PROCESSSEGMENTID";
            popupProc.LabelText = "PROCESSSEGMENTID";
            popupProc.SearchQuery = new SqlQuery("GetProcessSegmentListByOsp", "10001", dicParam);
            popupProc.IsMultiGrid = false;

            popupProc.DisplayFieldName = "PROCESSSEGMENTNAME";
            popupProc.ValueFieldName = "PROCESSSEGMENTID";
            popupProc.LanguageKey = "PROCESSSEGMENTID";

            popupProc.Conditions.AddTextBox("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT");

            popupProc.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetLabel("PROCESSSEGMENTID");
            popupProc.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200)
                .SetLabel("PROCESSSEGMENTNAME");

            popupProcesssegmentid.SelectPopupCondition = popupProc;
        }


        #endregion

        #region 외주작업의뢰 목록 리스트 그리드
        /// <summary>        
        /// 외주작업의뢰 목록 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializegOtherSubcontractWorkCommissionGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdOtherSubcontractWorkCommission.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;
            grdOtherSubcontractWorkCommission.View.SetIsReadOnly();
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("ENTERPRISEID", 120)
                .SetIsHidden();                                                          //  회사 ID
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();                                                          //  공장 ID
            grdOtherSubcontractWorkCommission.View.AddComboBoxColumn("OSPETCPROGRESSSTATUS", 100
                , new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPEtcProgressStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));  // 진행상태
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("REQUESTNO", 120);               // 의뢰번호
            grdOtherSubcontractWorkCommission.View.AddComboBoxColumn("OSPETCTYPE", 100
                , new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPEtcType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));       //  작업구분           

            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("CUSTOMERID", 120)
                .SetIsHidden();             //  고객사 ID
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("CUSTOMERNAME", 120);            //  고객사 명
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("AREAID", 120).SetIsHidden(); 
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("AREANAME", 200);
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("OSPVENDORID", 120)
                .SetIsHidden();                 //  협력사 ID
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("OSPVENDORNAME", 120);              //  협력사 명
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("REQUESTDEPARTMENT", 120)
                .SetIsHidden();    //    요청부서ID
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("OSPREQUESTUSER", 120)
                .SetIsHidden();                                                                      //    요청자ID
            grdOtherSubcontractWorkCommission.View.AddComboBoxColumn("LOTPRODUCTTYPE", 100
                , new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));   //   양산구분
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("PRODUCTDEFID", 120);            //  제품 정의 ID
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);       //  제품 정의 Version
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);          //  제품명
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("PROCESSSEGMENTID", 120)
                .SetIsHidden();          //  공정 ID

            // 2021-01-05 오근영 (96) 공정명 필수 변경
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120);    //  공정명
         
            grdOtherSubcontractWorkCommission.View.AddComboBoxColumn("UNIT", 60
                , new SqlQuery("GetUomDefinitionMapListByOsp", "10001", "UOMCATEGORY=OSP", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "UOMDEFID", "UOMDEFID"); //  단위
            grdOtherSubcontractWorkCommission.View.AddSpinEditColumn("REQUESTQTY", 120)
                 .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);                                       // 의뢰수량
            grdOtherSubcontractWorkCommission.View.AddSpinEditColumn("REQUESTPRICE", 120) 
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsHidden();                                                            // 의뢰단가
            grdOtherSubcontractWorkCommission.View.AddSpinEditColumn("REQUESTAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsHidden();                                                       // 의뢰금액
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("REQUESTDESCRIPTION", 200);       // 의뢰설명
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("OSPREQUESTUSERNAME", 120);       // 의뢰자명

            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("REQUESTDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd");                                                        //  의뢰일자
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("ISCHARGE", 80);                  // 유상여부 Check AddCheckBoxColumn
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("ISREQUESTDEFECTACCEPT", 80);    // 불량적용여부_의뢰
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("ETCLOTID", 150);                 // 기타작업_LOT_ID
            grdOtherSubcontractWorkCommission.View.AddSpinEditColumn("ACTUALQTY", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsHidden();                                         // 수량_실적
            grdOtherSubcontractWorkCommission.View.AddSpinEditColumn("ACTUALDEFECTQTY", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsHidden();                                        //  불량수량_실적
            grdOtherSubcontractWorkCommission.View.AddSpinEditColumn("ACTUALPRICE", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsHidden();                                        //  단가_실적
            grdOtherSubcontractWorkCommission.View.AddSpinEditColumn("ACTUALAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsHidden();                                          // 금액_실적
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("ISACTUALDEFECTACCEPT", 80)
                 .SetIsHidden();        // 불량적용여부_실적
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("ACTUALUSER", 120)
                .SetIsHidden();                                                                        // 작업자
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("ACTUALUSERNAME", 120)
               .SetIsHidden();                                                                        // 작업자
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("ACTUALDATE", 80)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetIsHidden();  //   작업일자
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("ACTUALDESCRIPTION", 200)
                .SetIsHidden();          // 작업설명
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("APPROVALDATE", 80)
                .SetDisplayFormat("yyyy-MM-dd")
                .SetIsHidden();                                                         // 승인일
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("APPROVALUSERNAME", 120)
               .SetIsHidden();                                                                          // 승인자
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("ISAPPROVALDEFECTACCEP", 80)
                .SetIsHidden();        // 불량수량적용여부_승인
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("SETTLEUSER", 120)
                .SetIsHidden();                                                                          //확정자ID
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("SETTLEUSERNAME", 120)
               .SetIsHidden();                                                                          //확정자ID
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("SETTLEDATE", 80) 
                .SetDisplayFormat("yyyy-MM-dd")
                .SetIsHidden();                                                         // 확정일자
            grdOtherSubcontractWorkCommission.View.AddSpinEditColumn("SETTLEQTY", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsHidden();                                          //수량_확정
            grdOtherSubcontractWorkCommission.View.AddSpinEditColumn("SETTLEDEFECTQTY", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsHidden();                                          //  불량수량_확정
            grdOtherSubcontractWorkCommission.View.AddSpinEditColumn("SETTLEPRICE", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsHidden();                                         //  단가_확정
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("ISSETTLEDEFECTACCEPT", 80)
                .SetIsHidden();        //  불량수량적용여부_확정
            grdOtherSubcontractWorkCommission.View.AddSpinEditColumn("SETTLEAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsHidden();                                                                        //  금액_확정
            grdOtherSubcontractWorkCommission.View.AddTextBoxColumn("SETTLEDESCRIPTION", 200)
                .SetIsHidden();                                                                        //  확정설명
            grdOtherSubcontractWorkCommission.View.PopulateColumns();

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
                .SetIsHidden();                                                          //  회사 ID
            grdWorkTemp.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();                                                          //  공장 ID
            grdWorkTemp.View.AddComboBoxColumn("OSPETCPROGRESSSTATUS", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPEtcProgressStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));  // 진행상태
            grdWorkTemp.View.AddTextBoxColumn("REQUESTNO", 120);              // 의뢰번호
            grdWorkTemp.View.AddComboBoxColumn("OSPETCTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPEtcType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));          //  작업구분           

            grdWorkTemp.View.AddTextBoxColumn("CUSTOMERID", 120);             //  고객사 ID
            grdWorkTemp.View.AddTextBoxColumn("CUSTOMERNAME", 120);           //  고객사 명
            grdWorkTemp.View.AddTextBoxColumn("AREAID", 120).SetIsHidden();
            grdWorkTemp.View.AddTextBoxColumn("AREANAME", 200);
            grdWorkTemp.View.AddTextBoxColumn("OSPVENDORID", 120);               //  협력사 ID
            grdWorkTemp.View.AddTextBoxColumn("OSPVENDORNAME", 120);             //  협력사 명
            grdWorkTemp.View.AddTextBoxColumn("REQUESTDEPARTMENT", 120);     //    요청부서ID
            grdWorkTemp.View.AddTextBoxColumn("OSPREQUESTUSER", 120)
                .SetIsHidden();                                               //    요청자ID
            grdWorkTemp.View.AddTextBoxColumn("OSPREQUESTUSERNAME", 120);          //    요청자명
            grdWorkTemp.View.AddTextBoxColumn("REQUESTDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd");                              //  의뢰일자
            grdWorkTemp.View.AddComboBoxColumn("LOTPRODUCTTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));   //   양산구분
            grdWorkTemp.View.AddTextBoxColumn("PRODUCTDEFID", 120);            //     제품 정의 ID
            grdWorkTemp.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);        //    제품 정의 Version
            grdWorkTemp.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);           //    제품명
            grdWorkTemp.View.AddTextBoxColumn("PROCESSSEGMENTID", 120);         //     공정 ID

            // 2021-01-05 오근영 (96) 공정명 필수 변경
            grdWorkTemp.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120);     //     공정명

            grdWorkTemp.View.AddSpinEditColumn("REQUESTQTY", 120)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);                                         //   의뢰수량
            grdWorkTemp.View.AddComboBoxColumn("UNIT", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Unit", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")); //  단위
            grdWorkTemp.View.AddSpinEditColumn("REQUESTPRICE", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                          //  의뢰단가
            grdWorkTemp.View.AddSpinEditColumn("REQUESTAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                         //  의뢰금액
            grdWorkTemp.View.AddCheckBoxColumn("ISCHARGE", 80);                  // 유상여부 Check
            grdWorkTemp.View.AddTextBoxColumn("REQUESTDESCRIPTION", 200);        //  의뢰설명
            grdWorkTemp.View.AddTextBoxColumn("ISREQUESTDEFECTACCEPT", 80);     // 불량적용여부_의뢰
            grdWorkTemp.View.AddTextBoxColumn("ETCLOTID", 150);                  //     기타작업_LOT_ID
            grdWorkTemp.View.AddSpinEditColumn("ACTUALQTY", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                     //    수량_실적
            grdWorkTemp.View.AddSpinEditColumn("ACTUALDEFECTQTY", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                   //  불량수량_실적
            grdWorkTemp.View.AddSpinEditColumn("ACTUALPRICE", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                 //  단가_실적
            grdWorkTemp.View.AddSpinEditColumn("ACTUALAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                           //     금액_실적
            grdWorkTemp.View.AddTextBoxColumn("ISACTUALDEFECTACCEPT", 80); // 불량적용여부_실적
            grdWorkTemp.View.AddTextBoxColumn("ACTUALUSER", 120)
                .SetIsHidden();   //  작업자
            grdWorkTemp.View.AddTextBoxColumn("ACTUALUSERNAME", 120)
             .SetIsHidden();   //  작업자
            grdWorkTemp.View.AddTextBoxColumn("ACTUALDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd"); //   작업일자
            grdWorkTemp.View.AddTextBoxColumn("ACTUALDESCRIPTION", 200);     //    작업설명
            grdWorkTemp.View.AddTextBoxColumn("APPROVALUSERNAME", 120);     //    작업설명
            grdWorkTemp.View.AddTextBoxColumn("APPROVALDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd");                            //      승인일
            grdWorkTemp.View.AddTextBoxColumn("ISAPPROVALDEFECTACCEP", 80);  // 불량수량적용여부_승인
            grdWorkTemp.View.AddTextBoxColumn("SETTLEUSER", 120)
                .SetIsHidden();                                                                  //     확정자ID
            grdWorkTemp.View.AddTextBoxColumn("SETTLEUSERNAME", 120)
              .SetIsHidden();                                                                  //     확정자ID
            grdWorkTemp.View.AddTextBoxColumn("SETTLEDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd");                                                //   확정일자
            grdWorkTemp.View.AddSpinEditColumn("SETTLEQTY", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                            //     수량_확정
            grdWorkTemp.View.AddSpinEditColumn("SETTLEDEFECTQTY", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                    //  불량수량_확정
            grdWorkTemp.View.AddSpinEditColumn("SETTLEPRICE", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                             //  단가_확정
            grdWorkTemp.View.AddTextBoxColumn("ISSETTLEDEFECTACCEPT", 80); //  불량수량적용여부_확정
            grdWorkTemp.View.AddSpinEditColumn("SETTLEAMOUNT", 120)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetIsHidden();   //     금액_확정
            grdWorkTemp.View.AddTextBoxColumn("SETTLEDESCRIPTION", 200)
                .SetIsHidden();  //    확정설명
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
            grdOtherSubcontractWorkCommission.View.FocusedRowChanged += View_FocusedRowChanged;
            grdOtherSubcontractWorkCommission.View.RowCellClick += View_RowCellClick;
            // 화면컨트롤 이벤트 처리 
            cboOspetctype.EditValueChanged += DetailControl_EditValueChanged;
            usrProductdefid.CODE.EditValueChanged+= DetailControl_EditValueChanged;
            popupOspAreaid.EditValueChanged += DetailControl_EditValueChanged;
            popupProcesssegmentid.EditValueChanged += DetailControl_EditValueChanged;
            cboLotproducttype.EditValueChanged += DetailControl_EditValueChanged;
            txtEtclotid.EditValueChanged += DetailControl_EditValueChanged;
            cboUnit.EditValueChanged += DetailControl_EditValueChanged;
            txtRequestprice.EditValueChanged += DetailControl_EditValueChanged;
            txtRequestqty.EditValueChanged += DetailControl_EditValueChanged;
            chkIscharge.EditValueChanged += DetailControl_EditValueChanged;
            chkIsrequestdefectaccept.EditValueChanged += DetailControl_EditValueChanged;
            txtRequestdescription.EditValueChanged += DetailControl_EditValueChanged;
            usrProductdefid.CODE.EditValueChanged += usrProductdefid_CODE_EditValueChanged;

        }
        /// <summary>
        /// 품목코드변경시 고객사 id ,고객사명 ...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void usrProductdefid_CODE_EditValueChanged(object sender, EventArgs e)
        {
            if (usrProductdefid.CODE.Text.Trim().ToString().Equals(""))
            {
                txtCustomerid.EditValue = "";

            }
            else
            {
                //품목에 해당하는 고객사명 가져오기 
                SearchCustomeridValue();
            }
        }

        /// <summary>
        /// 기타외주작업목록의 수정여부및 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailControl_EditValueChanged(object sender, EventArgs e)
        {

            if (_blchang == false) return;
            if (grdWorkTemp.View.DataRowCount != 1)
            {
                DataTable dt = createSaveDatatable();
                grdWorkTemp.DataSource = dt;
                grdWorkTemp.View.AddNewRow();
                grdWorkTemp.View.SetRowCellValue(0,"_STATE_", "added");
            }
            else if (!(grdWorkTemp.View.GetRowCellValue(0, "_STATE_").ToString().Equals("added")))
            { 
                grdWorkTemp.View.SetRowCellValue(0, "_STATE_", "modified");
            }
            DataRow dr = grdWorkTemp.View.GetDataRow(grdWorkTemp.View.FocusedRowHandle);

            decimal decRequestqty = 0;
            if (!(txtRequestqty.EditValue.Equals("")))
            {
                decRequestqty = Convert.ToDecimal(txtRequestqty.EditValue);
            }
           
            decimal decRequestprice = 0;
            if (!(txtRequestprice.EditValue.Equals("")))
            {
                decRequestprice = Convert.ToDecimal(txtRequestprice.EditValue);
            }
            
            decimal decRequestamount = 0;
            dr["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
            var values = Conditions.GetValues();
            string _strPlantid = values["P_PLANTID"].ToString();
            dr["PLANTID"] = _strPlantid;
            dr["REQUESTNO"] = txtRequestno.EditValue;
            dr["REQUESTDATE"] = "";
            dr["REQUESTDEPARTMENT"] = UserInfo.Current.Department.ToString();

            dr["OSPREQUESTUSERNAME"] = txtRequestorname.Text;
            dr["OSPETCTYPE"] = cboOspetctype.EditValue;
            dr["PRODUCTDEFID"] = usrProductdefid.CODE.EditValue;
            dr["PRODUCTDEFVERSION"] = usrProductdefid.VERSION.EditValue;

          
            dr["CUSTOMERNAME"] = txtCustomerid.Text;
            dr["PROCESSSEGMENTID"] = popupProcesssegmentid.GetValue().ToString();
           
            dr["PROCESSSEGMENTNAME"] = popupProcesssegmentid.Text;
            dr["LOTPRODUCTTYPE"] = cboLotproducttype.EditValue;
            dr["AREAID"] = popupOspAreaid.GetValue().ToString();
            dr["OSPVENDORID"] = _strOspVendorid;
            dr["OSPVENDORNAME"] = txtOspVendorName.Text;
            dr["REQUESTQTY"] = txtRequestqty.EditValue;
            dr["UNIT"] = cboUnit.EditValue;
            dr["REQUESTPRICE"] = txtRequestprice.EditValue;
            dr["ETCLOTID"] = txtEtclotid.EditValue;
            dr["REQUESTDESCRIPTION"] = txtRequestdescription.EditValue;

            // 진행상태값 수정및 체크
            if (cboOspetcprogressstatus.EditValue.Equals(null) || cboOspetcprogressstatus.EditValue.Equals(""))
            {
                cboOspetcprogressstatus.EditValue = "Request";
            }
            dr["OSPETCPROGRESSSTATUS"] = cboOspetcprogressstatus.EditValue;

            // 유무상 체크 값처리에 따른 의뢰금액 계산처리 
            if (chkIscharge.Checked)
            {
                dr["ISCHARGE"] = "Y";
                decRequestamount =Math.Truncate(decRequestqty * decRequestprice);
                txtRequestamount.EditValue = decRequestamount;
            }
            else
            {
                dr["ISCHARGE"] = "N";
                decRequestamount = 0;
                txtRequestamount.EditValue = 0;
            }
            dr["REQUESTAMOUNT"] = txtRequestamount.EditValue;

            //불량여부 체크값 처리 
            if (chkIsrequestdefectaccept.Checked)
            {
                dr["ISREQUESTDEFECTACCEPT"] = "Y";
            }
            else
            {
                dr["ISREQUESTDEFECTACCEPT"] = "N";
            }
           
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
        /// 그리드의 포커스 행 변경 이벤트
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
            txtRequestno.EditValue = ""; //의뢰번호 
            DateTime dateNow = DateTime.Now;
            dtpRequestdate.EditValue = dateNow.ToString("yyyy-MM-dd"); 
            txtRequestdepartment.EditValue = UserInfo.Current.Department.ToString();//현재로그인 부서
            txtRequestorname.EditValue = UserInfo.Current.Name.ToString();//  현재로그인 유저정보
            if (!(cboOspetctype.Text.ToString().Equals("")))
            {
                cboOspetctype.EditValue = "";
            }
            usrProductdefid.CODE.EditValue = "";
            usrProductdefid.VERSION.EditValue = "";
            usrProductdefid.NAME.EditValue = "";
            txtCustomerid.EditValue = "";
            popupProcesssegmentid.SetValue("");
            popupProcesssegmentid.EditValue = "";
            if (!(cboLotproducttype.Text.ToString().Equals("")))
            {
                cboLotproducttype.EditValue = "Production";
            }
            //cboLotproducttype.EditValue = "Production";
            popupOspAreaid.SetValue("");
            popupOspAreaid.EditValue = "";
            _strOspVendorid = "";
            txtOspVendorName.EditValue = "";
            chkIscharge.Checked = true;
            chkIsrequestdefectaccept.Checked = false;
            txtRequestqty.EditValue = 0D;
         
            txtRequestprice.EditValue = 0D;
            txtRequestamount.EditValue = 0;
            txtEtclotid.EditValue = "";
            txtRequestdescription.EditValue = "";
           
            cboOspetcprogressstatus.EditValue = "Request";
          
            if (!(cboUnit.Text.ToString().Equals("")))
            {
                cboUnit.EditValue = "PCS";
            }
            //cboOspetcprogressstatus.EditValue = "Request";
            //cboUnit.EditValue = "PCS";

            controlEnableProcess(true);
           
            grdWorkTemp.View.ClearDatas();
            _blchang = true;
        }
       
        /// <summary>
        /// 저장 -기타 외주 작업 내역을 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {

            ////// 필수값 체크 로직 추가 처리 
            // 양산구분 값 체크 
            if (cboOspetctype.EditValue.ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField",lblOspetctype.Text); //메세지 
                txtRequestqty.Focus();
                return;
            }
            //////1.품목코드
            if (usrProductdefid.CODE.Text.ToString().Equals("")
                || usrProductdefid.VERSION.Text.Equals("")
                || usrProductdefid.NAME.Text.Equals(""))
            {

                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblProductdefid.Text); //메세지 
                usrProductdefid.Focus();
                return;
            }
            //3.협력사 필수값 체크 여부 
            if (popupOspAreaid.GetValue().ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblOspAreaid.Text); //메세지 
                popupOspAreaid.Focus();
                return;
            }

            // 2020-12-21 오근영 (96) 공정명 필수 추가
            //3.공정명 필수값 체크 여부 
            if (popupProcesssegmentid.GetValue().ToString().Equals(""))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblProcesssegmentname.Text); //메세지 
                popupProcesssegmentid.Focus();
                return;
            }


            //2.수량체크(의뢰수량)
            if (txtRequestqty.Text.ToString().Equals("") || txtRequestqty.Text.ToString().Equals("0.") || txtRequestqty.Text.ToString().Equals("0"))
            {
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblRequestqty.Text); //메세지 
                txtRequestqty.Focus();
                return;
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
                    DataTable dt = createSaveDatatable();
                    _blchang = true;
                    DetailControl_EditValueChanged(null, null);
                    DataRow dr = grdWorkTemp.View.GetDataRow(grdWorkTemp.View.FocusedRowHandle);
                 
                    if (txtRequestno.Text.ToString().Equals(""))
                    {
                        var values = Conditions.GetValues();
                        String strPlantid = values["P_PLANTID"].ToString();
                        dr["_STATE_"] = "added";
                        dr["PLANTID"] = strPlantid;
                        dr["OSPREQUESTUSER"] = UserInfo.Current.Id.ToString();
                        dr["OSPETCPROGRESSSTATUS"] = "Request";
                    }
                    else
                    {
                        dr["_STATE_"] = "modified";
                    }
                    dr["VALIDSTATE"] = "Valid";
                    dt.ImportRow(dr);

                    DataTable saveResult = this.ExecuteRule<DataTable>("SaveOtherSubcontractWorkCommission", dt);
                    DataRow resultData = saveResult.Rows[0];
                    string strtempRequestno = resultData.ItemArray[0].ToString();
                    //의뢰번호 셋팅 처리 
                    txtRequestno.EditValue = strtempRequestno;
                    ShowMessage("SuccessOspProcess");
                    // 수정된 작업 리스트 가져오기및 행 이동 처리 
                    if (!(strRequestno.Equals("")))
                    {
                        //재조회하기..
                        OnSaveConfrimSearch();
                        //해당row 값 가져오기 
                        int irow = GetGridRowSearch(strRequestno);
                        if (irow >= 0)
                        {
                            grdOtherSubcontractWorkCommission.View.FocusedRowHandle = irow;
                            grdOtherSubcontractWorkCommission.View.SelectRow(irow);
                            focusedRowChanged();
                        }
                        else if (grdOtherSubcontractWorkCommission.View.DataRowCount > 0)
                        {
                            grdOtherSubcontractWorkCommission.View.FocusedRowHandle = 0;
                            grdOtherSubcontractWorkCommission.View.SelectRow(0);
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
                    DataTable dt = createSaveDatatable();

                    _blchang = true;
                    DetailControl_EditValueChanged(null, null);
                    DataRow dr = grdWorkTemp.View.GetDataRow(grdWorkTemp.View.FocusedRowHandle);
                    _blchang = false;
                    dr["REQUESTNO"] = txtRequestno.EditValue.ToString();
                    dr["_STATE_"] = "deleted";
                    dr["VALIDSTATE"] = "Invalid";

                    dt.ImportRow(dr);

                    this.ExecuteRule("SaveOtherSubcontractWorkCommission", dt);

                    ShowMessage("SuccessOspProcess"); //삭제 처리 하고 
                    ///this.OnSearchAsync();  //재조회 
                    BtnClear_Click(null, null);  //초기화
                                                 // 의뢰번호 삭제 처리된 목록 가져오기 
                    if (!(strRequestno.Equals("")))
                    {
                        //재조회하기..
                        OnSaveConfrimSearch();
                        if (grdOtherSubcontractWorkCommission.View.DataRowCount > 0)
                        {
                            grdOtherSubcontractWorkCommission.View.FocusedRowHandle = 0;
                            grdOtherSubcontractWorkCommission.View.SelectRow(0);
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

            #region 품목코드 전환 처리 
            if (!(values["P_PRODUCTCODE"] == null))
            {
                string sproductcode = values["P_PRODUCTCODE"].ToString();
                // 품목코드값이 있으면
                if (!(sproductcode.Equals("")))
                {
                    string[] sproductd = sproductcode.Split('|');
                    // plant 정보 다시 가져오기 
                    values.Add("P_PRODUCTDEFID", sproductd[0].ToString());
                    values.Add("P_PRODUCTDEFVERSION", sproductd[1].ToString());
                }
            }

            #endregion

            #region 기간 검색형 전환 처리 
            if (!(values["P_REQUESTDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_REQUESTDATE_PERIODFR"]);
                values.Remove("P_REQUESTDATE_PERIODFR");
                values.Add("P_REQUESTDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_REQUESTDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_REQUESTDATE_PERIODTO"]);
                values.Remove("P_REQUESTDATE_PERIODTO");
                //requestDateTo = requestDateTo.AddDays(1);
                values.Add("P_REQUESTDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }
           
        
            #endregion

            _strPlantid =  values["P_PLANTID"].ToString();
            usrProductdefid.strTempPlantid = _strPlantid;
            // 초기화 
            BtnClear_Click(null, null);
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dt = await SqlExecuter.QueryAsync("GetOtherSubcontractWorkCommission", "10001", values);

            if (dt.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
               
            }
            grdOtherSubcontractWorkCommission.DataSource = dt;
            // grdList.DataSource = dtCodeClass;
            //selectOspVendoridPopup();
            if (dt.Rows.Count > 0)
            {
                grdOtherSubcontractWorkCommission.View.FocusedRowHandle = 0;
                grdOtherSubcontractWorkCommission.View.SelectRow(0);
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
           // InitializeConditionPopup_Plant();
            // 작업자 
            InitializeConditionPopup_Requestor();

            InitializeConditionPopup_OspAreaid();
            // 작업업체 
            InitializeConditionPopup_OspVendorid();
            //진행상태
            InitializeConditionPopup_Ospetcprogressstatus();
            // 품목코드 
            InitializeConditionPopup_ProductDefId();

        }
       
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {

            var planttxtbox = Conditions.AddComboBox("p_plantid", new SqlQuery("GetPlantList", "00001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
               .SetPosition(0.1)
               .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
            ;
               //   .SetIsReadOnly(true);
           
        }
        /// <summary>
        /// 의뢰자 조회조건
        /// </summary>
        private void InitializeConditionPopup_Requestor()
        {
            // 팝업 컬럼설정
            var requesterPopupColumn = Conditions.AddSelectPopup("p_requestuser", new SqlQuery("GetRequestorListByOsp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "REQUESTUSERNAME", "REQUESTUSER")
               .SetPopupLayout("OSPREQUESTUSER", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("OSPREQUESTUSER")
               .SetPopupResultCount(1)
               .SetRelationIds("p_plantid")
               .SetPosition(2.2);
            requesterPopupColumn.Conditions.AddTextBox("REQUESTUSERNAME")
             .SetLabel("OSPREQUESTUSERNAME");

            // 팝업 그리드
            requesterPopupColumn.GridColumns.AddTextBoxColumn("REQUESTUSER", 150)
                .SetLabel("OSPREQUESTUSER")
                .SetValidationKeyColumn();
            requesterPopupColumn.GridColumns.AddTextBoxColumn("REQUESTUSERNAME", 200)
                .SetLabel("OSPREQUESTUSERNAME");
            ////// 팝업 조회조건
            ////requesterPopupColumn.Conditions.AddTextBox("OSPREQUESTUSERNAME")
            ////    .SetLabel("OSPREQUESTUSERNAME");

            ////// 팝업 그리드
            ////requesterPopupColumn.GridColumns.AddTextBoxColumn("OSPREQUESTUSER", 150)
            ////    .SetValidationKeyColumn();
            ////requesterPopupColumn.GridColumns.AddTextBoxColumn("OSPREQUESTUSERNAME", 200);
        }
        /// <summary>
        /// 작업장
        /// </summary>
        private void InitializeConditionPopup_OspAreaid()
        {
            // 팝업 컬럼설정
            var areaidPopupColumn = Conditions.AddSelectPopup("p_areaid", new SqlQuery("GetAreaidPopupListByOsp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(520, 600)
               .SetLabel("AREANAME")
               .SetRelationIds("p_plantId")
               .SetPopupResultCount(1)
               .SetPosition(2.3);
            // 팝업 조회조건
            areaidPopupColumn.Conditions.AddTextBox("P_AREANAME")
                .SetLabel("AREANAME");

            // 팝업 그리드
            areaidPopupColumn.GridColumns.AddTextBoxColumn("AREAID", 120);
            areaidPopupColumn.GridColumns.AddTextBoxColumn("AREANAME", 200);


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
               .SetPopupResultCount(1)
              .SetRelationIds("p_plantid", "p_areaid")
               .SetPosition(2.4);

            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("OSPVENDORNAME")
                .SetLabel("OSPVENDORNAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);

            var txtOspVendor = Conditions.AddTextBox("P_OSPVENDORNAME")
               .SetLabel("OSPVENDORNAME")
               .SetPosition(2.5);
           
        }
        /// <summary>
        /// 진행상태
        /// </summary>
        private void InitializeConditionPopup_Ospetcprogressstatus()
        {

            var Ospetcprogressstatuscbobox = Conditions.AddComboBox("p_ospetcprogressstatus", new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPEtcProgressStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
               .SetLabel("OSPETCPROGRESSSTATUS")
              // .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
               .SetPosition(2.6)
              .SetEmptyItem("", "")
               .SetDefault("Request") //기본값 설정 UserInfo.Current.Plant
             ;
            //   .SetIsReadOnly(true);

        }
        /// <summary>
        /// 품목코드 조회조건
        /// </summary>
        private void InitializeConditionPopup_ProductDefId()
        {
            var popupProduct = Conditions.AddSelectPopup("p_productcode",
                                                                new SqlQuery("GetProductdefidlistByOsp", "10001"
                                                                                , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                                , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                ), "PRODUCTDEFNAME", "PRODUCTDEFCODE")
                 .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                 .SetPopupLayoutForm(650, 600)
                 .SetLabel("PRODUCTDEFID")
                 .SetPopupResultCount(1)
                 .SetPosition(5.1);
            // 팝업 조회조건
            popupProduct.Conditions.AddComboBox("P_PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTDEFTYPE")
                .SetDefault("Product");
           
            popupProduct.Conditions.AddTextBox("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFID");

           
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFCODE", 150)
                .SetIsHidden();
            popupProduct.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150).SetIsReadOnly(); 
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsReadOnly(); 
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();

            var txtProductName  =Conditions.AddTextBox("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFNAME")
                .SetPosition(5.2);
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
            clearControl();
            if (btnSave.Enabled == false)
            {
                _blSaveAuth = false;
            }
            else
            {
                _blSaveAuth = true;
            }
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
            if (grdOtherSubcontractWorkCommission.View.FocusedRowHandle < 0) return; 

            var row = grdOtherSubcontractWorkCommission.View.GetDataRow(grdOtherSubcontractWorkCommission.View.FocusedRowHandle);

            _blchang = false;
            txtRequestno.EditValue   = row["REQUESTNO"].ToString();
            DateTime dateRequestdat = Convert.ToDateTime(row["REQUESTDATE"].ToString());
            dtpRequestdate.EditValue = dateRequestdat.ToString("yyyy-MM-dd"); 
            txtRequestdepartment.EditValue = row["REQUESTDEPARTMENT"].ToString();
            txtRequestorname.EditValue = row["OSPREQUESTUSERNAME"].ToString();
            cboOspetctype.EditValue = row["OSPETCTYPE"].ToString();

            usrProductdefid.CODE.EditValue = row["PRODUCTDEFID"].ToString();
            usrProductdefid.VERSION.EditValue = row["PRODUCTDEFVERSION"].ToString();
            usrProductdefid.NAME.EditValue = row["PRODUCTDEFNAME"].ToString();
            txtCustomerid.EditValue = row["CUSTOMERNAME"].ToString();
            popupProcesssegmentid.SetValue(row["PROCESSSEGMENTID"].ToString());
            popupProcesssegmentid.Text = row["PROCESSSEGMENTNAME"].ToString();
            popupProcesssegmentid.EditValue = row["PROCESSSEGMENTNAME"].ToString();
            cboLotproducttype.EditValue = row["LOTPRODUCTTYPE"].ToString();
            popupOspAreaid.SetValue(row["AREAID"].ToString());
            popupOspAreaid.Text = row["AREANAME"].ToString();
            popupOspAreaid.EditValue = row["AREANAME"].ToString();
            _strOspVendorid = row["OSPVENDORID"].ToString();
            txtOspVendorName.Text = row["OSPVENDORNAME"].ToString();
           
           
            //유무상 체크 값 셋팅 
            if (row["ISCHARGE"].ToString().Equals("Y") )
            {
                chkIscharge.Checked = true;
            }
            else
            {
                chkIscharge.Checked = false;
            }

            // 불량여부 체크 값 세팅 
            if (row["ISREQUESTDEFECTACCEPT"].ToString().Equals("Y"))
            {
                chkIsrequestdefectaccept.Checked = true;
            }
            else
            {
                chkIsrequestdefectaccept.Checked = false;
            }
         
            txtRequestqty.EditValue = row["REQUESTQTY"].ToString();
            cboUnit.EditValue = row["UNIT"].ToString();
            txtRequestprice.EditValue = row["REQUESTPRICE"].ToString();
            txtRequestamount.EditValue = row["REQUESTAMOUNT"].ToString();
            txtEtclotid.EditValue = row["ETCLOTID"].ToString();
            txtRequestdescription.EditValue = row["REQUESTDESCRIPTION"].ToString();
            cboOspetcprogressstatus.EditValue = row["OSPETCPROGRESSSTATUS"].ToString();
            DataTable dtWorkTemp = createSaveDatatable();
            dtWorkTemp.ImportRow(row);
            grdWorkTemp.DataSource = dtWorkTemp;

            // 진행 상태값에 따른 컨트롤 lock 처리 
            if (row["OSPETCPROGRESSSTATUS"].ToString().Equals("Request")) 
            {
                //
                controlEnableProcess(true);
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

            #region 품목코드 전환 처리 
            if (!(values["P_PRODUCTCODE"] == null))
            {
                string sproductcode = values["P_PRODUCTCODE"].ToString();
                // 품목코드값이 있으면
                if (!(sproductcode.Equals("")))
                {
                    string[] sproductd = sproductcode.Split('|');
                    // plant 정보 다시 가져오기 
                    values.Add("P_PRODUCTDEFID", sproductd[0].ToString());
                    values.Add("P_PRODUCTDEFVERSION", sproductd[1].ToString());
                }
            }
            #endregion

            #region 기간 검색형 전환 처리 
            if (!(values["P_REQUESTDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_REQUESTDATE_PERIODFR"]);
                values.Remove("P_REQUESTDATE_PERIODFR");
                values.Add("P_REQUESTDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_REQUESTDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_REQUESTDATE_PERIODTO"]);
                values.Remove("P_REQUESTDATE_PERIODTO");
                //requestDateTo = requestDateTo.AddDays(1);
                values.Add("P_REQUESTDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }

            #endregion

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dt = SqlExecuter.Query("GetOtherSubcontractWorkCommission", "10001", values);

            grdOtherSubcontractWorkCommission.DataSource = dt;
               
           
        }
        /// <summary>
        /// 그리드 이동에 필요한 row 찾기
        /// </summary>
        /// <param name="strRequestno"></param>
        private int GetGridRowSearch(string strRequestno)
        {
            int iRow = -1;
            if (grdOtherSubcontractWorkCommission.View.DataRowCount == 0)
            {
                return iRow;
            }
            for (int i = 0; i < grdOtherSubcontractWorkCommission.View.DataRowCount; i++)
            {
                if (grdOtherSubcontractWorkCommission.View.GetRowCellValue(i, "REQUESTNO").ToString().Equals(strRequestno))
                {
                    return i;
                }
            }
            return iRow;
        }
        ///
        /// <summary>
        /// 진행상태값에 따른 입력 항목 lock 처리
        /// </summary>
        /// <param name="blProcess"></param>
        private void controlEnableProcess(bool blProcess)
        {
            txtRequestno.Enabled = false; 
            dtpRequestdate.Enabled = false;  
            txtRequestdepartment.Enabled = false; 
            txtRequestorname.Enabled = false; 
            txtRequestamount.Enabled = false;  
            cboOspetcprogressstatus.Enabled = false;
            txtCustomerid.Enabled = false;

            cboOspetctype.Enabled = blProcess;  // 작업구분
            usrProductdefid.Enabled = blProcess;
            usrProductdefid.CODE.Enabled = blProcess;
            usrProductdefid.btnSearch.Enabled = blProcess;
            usrProductdefid.VERSION.Enabled = false;  //강제 처리 
            usrProductdefid.NAME.Enabled = blProcess;
            cboLotproducttype.Enabled = blProcess;

          
            popupProcesssegmentid.Enabled = blProcess;
          
            popupOspAreaid.Enabled = blProcess;
            chkIscharge.Enabled = blProcess;
           
            chkIsrequestdefectaccept.Enabled = blProcess;
            txtRequestqty.Enabled = blProcess;
            cboUnit.Enabled = blProcess;
            txtRequestprice.Enabled = blProcess;
            
            txtEtclotid.Enabled = blProcess;
            txtRequestdescription.Enabled = blProcess;
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
            dt.Columns.Add("REQUESTNO");
            dt.Columns.Add("REQUESTDATE");
            dt.Columns.Add("REQUESTDEPARTMENT");
            
            dt.Columns.Add("OSPREQUESTUSER");
            dt.Columns.Add("OSPREQUESTUSERNAME");
            dt.Columns.Add("OSPETCTYPE");
            dt.Columns.Add("PRODUCTDEFID");
            dt.Columns.Add("PRODUCTDEFVERSION");
            dt.Columns.Add("PRODUCTDEFNAME");
            dt.Columns.Add("CUSTOMERID");
            dt.Columns.Add("CUSTOMERNAME");
            dt.Columns.Add("PROCESSSEGMENTID");
            dt.Columns.Add("PROCESSSEGMENTNAME");
            dt.Columns.Add("LOTPRODUCTTYPE");
            dt.Columns.Add("AREAID");
            dt.Columns.Add("OSPVENDORID");
            dt.Columns.Add("OSPVENDORNAME");
            dt.Columns.Add("ISCHARGE");
            dt.Columns.Add("ISREQUESTDEFECTACCEPT");
            dt.Columns.Add("REQUESTQTY");
            dt.Columns.Add("UNIT");
            dt.Columns.Add("REQUESTPRICE");
            dt.Columns.Add("REQUESTAMOUNT");
            dt.Columns.Add("ETCLOTID");
            dt.Columns.Add("REQUESTDESCRIPTION");
            dt.Columns.Add("OSPETCPROGRESSSTATUS");
            
            dt.Columns.Add("VALIDSTATE");
            dt.Columns.Add("_STATE_");
            return dt;
        }

        /// <summary>
        /// 기간 포맷 재정의 
        /// </summary>
        private void InitializeDatePeriodSetting(string sPeriodname)
        {
            // 기간 포맷 재정의 
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add(sPeriodname, "CUSTOM");
            values.Add(sPeriodname +"_PERIODFR", "");
            values.Add(sPeriodname + "_PERIODTO", "");

            Conditions.GetControl<SmartPeriodEdit>(sPeriodname).SetValue(values);

        }
        /// <summary>
        ///  목록 내역 clear (조회 건수가 없을때)
        /// </summary>
        private void clearControl()
        {
            _blchang = false;
            txtRequestno.EditValue = "";
            dtpRequestdate.EditValue = "";
            txtRequestdepartment.EditValue = "";
            txtRequestorname.EditValue = "";
            cboOspetctype.EditValue = "";
            usrProductdefid.CODE.EditValue = "";
            usrProductdefid.VERSION.EditValue = "";
            usrProductdefid.NAME.EditValue = "";
            txtCustomerid.EditValue = "";
            popupOspAreaid.SetValue("");
            popupOspAreaid.EditValue = "";
            popupProcesssegmentid.SetValue("");
            popupProcesssegmentid.EditValue = "";
            cboLotproducttype.EditValue = "";
            _strOspVendorid = "";
            txtOspVendorName.EditValue = "";
            chkIscharge.Checked = false;
            chkIsrequestdefectaccept.Checked = false;
            txtRequestqty.EditValue = "";
            cboUnit.EditValue = "";
            txtRequestprice.EditValue = "";
            txtRequestamount.EditValue = "";
            txtEtclotid.EditValue = "";
            txtRequestdescription.EditValue = "";
            cboOspetcprogressstatus.EditValue = "";
            _blchang = true;

        }
        /// <summary>
        /// 제품코드에 해당하는 고객사 명 찾기 
        /// </summary>
        private void SearchCustomeridValue()
        {

            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("P_PRODUCTDEFID", usrProductdefid.CODE.EditValue);
            Param.Add("P_PRODUCTDEFVERSION", usrProductdefid.VERSION.EditValue);
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            DataTable dtCustomerid = SqlExecuter.Query("GetCustomerByOspProduct", "10001", Param);
            
            if (dtCustomerid.Rows.Count > 0)
            {
                DataRow row = dtCustomerid.Rows[0];
               
                txtCustomerid.EditValue = row["CUSTOMERNAME"].ToString();
            }
            else
            {
                txtCustomerid.EditValue = "";
            }
           
        }

        #endregion

    }
}
